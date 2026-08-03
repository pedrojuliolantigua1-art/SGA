using SGA.Desktop.Api;
using SGA.Desktop.DTOs.Autobus;
using SGA.Desktop.DTOs.Horario;
using SGA.Desktop.DTOs.Ruta;
using SGA.Desktop.DTOs.Viaje;
using SGA.Desktop.Services;
using SGA.Desktop.UI;

namespace SGA.Desktop.Forms
{
    public partial class ViajesForm : Form
    {
        public ViajesForm()
        {
            InitializeComponent();
            ConstruirFormulario();
            _dtpFiltroFecha.Value = DateTime.Today;
            Load += async (_, _) => await CargarCombosAsync();
        }

        private async Task CargarCombosAsync()
        {
            var rutas = await SgaApiClient.GetAsync<List<RutaPresentacionDto>>("api/rutas/activas");
            if (rutas.EsExitoso)
            {
                _cmbRuta.DataSource = null;
                _cmbRuta.DataSource = rutas.Valor!.ToList();
                _cmbRuta.DisplayMember = nameof(RutaPresentacionDto.Nombre);
                _cmbRuta.ValueMember = nameof(RutaPresentacionDto.Id);
            }

            var autobuses = await SgaApiClient.GetAsync<List<AutobusPresentacionDto>>("api/autobuses/disponibles");
            if (autobuses.EsExitoso)
            {
                _cmbAutobus.DataSource = null;
                _cmbAutobus.DataSource = autobuses.Valor!.ToList();
                _cmbAutobus.DisplayMember = nameof(AutobusPresentacionDto.Placa);
                _cmbAutobus.ValueMember = nameof(AutobusPresentacionDto.Id);

                // Autocompletado por placa (combo editable: se puede escribir o elegir de la lista).
                var placas = new AutoCompleteStringCollection();
                placas.AddRange(autobuses.Valor!.Select(a => a.Placa).Where(p => !string.IsNullOrWhiteSpace(p)).ToArray()!);
                _cmbAutobus.AutoCompleteCustomSource = placas;
            }

            var todos = await SgaApiClient.GetAsync<List<UsuarioResumenApi>>("api/usuarios");
            if (todos.EsExitoso)
            {
                var conductores = todos.Valor!
                    .Where(u => string.Equals(u.TipoUsuario, "Conductor", StringComparison.OrdinalIgnoreCase))
                    .ToList();

                _cmbConductor.DataSource = null;
                _cmbConductor.DataSource = conductores;
                _cmbConductor.DisplayMember = nameof(UsuarioResumenApi.NombreCompleto);
                _cmbConductor.ValueMember = nameof(UsuarioResumenApi.Id);
            }

            await CargarHorariosDeRutaAsync();
        }

        private async void CmbRuta_SelectedIndexChanged(object sender, EventArgs e) => await CargarHorariosDeRutaAsync();

        private async Task CargarHorariosDeRutaAsync()
        {
            if (_cmbRuta.SelectedItem is not RutaPresentacionDto ruta) return;

            var horarios = await SgaApiClient.GetAsync<List<HorarioRutaPresentacionDto>>($"api/horariosruta?rutaId={ruta.Id}");
            if (!horarios.EsExitoso) return;

            _cmbHorario.DataSource = null;
            _cmbHorario.DataSource = horarios.Valor!.Where(h => h.Activo).ToList();
            _cmbHorario.DisplayMember = nameof(HorarioRutaPresentacionDto.HoraSalida);
            _cmbHorario.ValueMember = nameof(HorarioRutaPresentacionDto.Id);

            _cmbHorario.FormattingEnabled = true;
            _cmbHorario.Format += (s, e) =>
            {
                if (e.Value is TimeSpan hora)
                    e.Value = hora.ToString(@"hh\:mm");
            };
        }

        private void ChkSemana_CheckedChanged(object sender, EventArgs e)
        {
            pnlDiasSemana.Visible = _chkSemana.Checked;
            btnProgramar.Text = _chkSemana.Checked ? "Programar semana" : "Programar";
        }

        private async void BtnProgramar_Click(object sender, EventArgs e)
        {
            // Con el combo de autobús editable, buscamos por texto escrito si no hay item seleccionado.
            var autobusSeleccionado = _cmbAutobus.SelectedItem as AutobusPresentacionDto;
            if (autobusSeleccionado is null && _cmbAutobus.DataSource is List<AutobusPresentacionDto> lista)
            {
                autobusSeleccionado = lista.FirstOrDefault(a =>
                    string.Equals(a.Placa, _cmbAutobus.Text, StringComparison.OrdinalIgnoreCase));
            }

            if (_cmbRuta.SelectedItem is not RutaPresentacionDto ruta ||
                _cmbHorario.SelectedItem is not HorarioRutaPresentacionDto horario ||
                autobusSeleccionado is null ||
                _cmbConductor.SelectedItem is not UsuarioResumenApi conductor)
            {
                CampoBuilder.MostrarMensaje(_lblMensajeProgramar, "Completa ruta, horario, autobús y conductor.", true);
                return;
            }

            if (_chkSemana.Checked)
            {
                await ProgramarSemanaAsync(ruta.Id, horario.Id, autobusSeleccionado.Id, conductor.Id);
                return;
            }

            var dto = new ProgramarViajePresentacionDto
            {
                RutaId = ruta.Id,
                HorarioRutaId = horario.Id,
                AutobusId = autobusSeleccionado.Id,
                ConductorId = conductor.Id,
                Fecha = _dtpFecha.Value.Date,
                CreadoPor = SesionActual.Usuario?.Correo
            };

            var resultado = await SgaApiClient.PostAsync<ViajePresentacionDto>("api/viajes", dto);
            CampoBuilder.MostrarMensaje(_lblMensajeProgramar,
                resultado.EsExitoso ? "Viaje programado correctamente." : resultado.Error!, !resultado.EsExitoso);
        }

        private async Task ProgramarSemanaAsync(int rutaId, int horarioId, int autobusId, int conductorId)
        {
            var dias = new List<int>();
            if (_chkDom.Checked) dias.Add((int)DayOfWeek.Sunday);
            if (_chkLun.Checked) dias.Add((int)DayOfWeek.Monday);
            if (_chkMar.Checked) dias.Add((int)DayOfWeek.Tuesday);
            if (_chkMie.Checked) dias.Add((int)DayOfWeek.Wednesday);
            if (_chkJue.Checked) dias.Add((int)DayOfWeek.Thursday);
            if (_chkVie.Checked) dias.Add((int)DayOfWeek.Friday);
            if (_chkSab.Checked) dias.Add((int)DayOfWeek.Saturday);

            if (dias.Count == 0)
            {
                CampoBuilder.MostrarMensaje(_lblMensajeProgramar, "Marca al menos un día de la semana.", true);
                return;
            }

            var dto = new ProgramarSemanaPresentacionDto
            {
                RutaId = rutaId,
                HorarioRutaId = horarioId,
                AutobusId = autobusId,
                ConductorId = conductorId,
                FechaReferenciaSemana = _dtpFecha.Value.Date,
                Dias = dias,
                CreadoPor = SesionActual.Usuario?.Correo
            };

            var resultado = await SgaApiClient.PostAsync<ProgramarSemanaResultadoPresentacionDto>("api/viajes/programar-semana", dto);

            if (!resultado.EsExitoso || resultado.Valor is null)
            {
                CampoBuilder.MostrarMensaje(_lblMensajeProgramar, resultado.Error!, true);
                return;
            }

            var creados = resultado.Valor.Creados.Count;
            var errores = resultado.Valor.Errores;
            var mensaje = errores.Count == 0
                ? $"Se programaron {creados} viaje(s) para la semana."
                : $"Se programaron {creados} viaje(s). Fallaron: {string.Join(" | ", errores)}";

            CampoBuilder.MostrarMensaje(_lblMensajeProgramar, mensaje, errores.Count > 0);

            if (creados > 0)
            {
                await CargarViajesDelDiaAsync();
                await CargarViajesActivosAsync();
            }
        }

        private async void BtnBuscarDia_Click(object sender, EventArgs e) => await CargarViajesDelDiaAsync();

        private async void BtnVerActivos_Click(object sender, EventArgs e) => await CargarViajesActivosAsync();

        private async Task CargarViajesDelDiaAsync()
        {
            var fecha = _dtpFiltroFecha.Value.Date.ToString("yyyy-MM-dd");
            var resultado = await SgaApiClient.GetAsync<List<ViajePresentacionDto>>($"api/viajes/por-fecha?fecha={fecha}");

            if (!resultado.EsExitoso)
            {
                CampoBuilder.MostrarMensaje(_lblMensajeListado, resultado.Error!, true);
                return;
            }

            MostrarViajesEnGrid(_dgvDia, resultado.Valor!);
        }

        private async Task CargarViajesActivosAsync()
        {
            var resultado = await SgaApiClient.GetAsync<List<ViajePresentacionDto>>("api/viajes/activos");
            if (!resultado.EsExitoso)
            {
                CampoBuilder.MostrarMensaje(_lblMensajeListado, resultado.Error!, true);
                return;
            }

            MostrarViajesEnGrid(_dgvActivos, resultado.Valor!);
        }

        private void MostrarViajesEnGrid(DataGridView grid, List<ViajePresentacionDto> viajes)
        {
            var rutas = (_cmbRuta.DataSource as List<RutaPresentacionDto>) ?? new();
            var autobuses = (_cmbAutobus.DataSource as List<AutobusPresentacionDto>) ?? new();
            var conductores = (_cmbConductor.DataSource as List<UsuarioResumenApi>) ?? new();

            var vista = viajes.Select(v => new
            {
                IdViaje = v.Id,
                Ruta = rutas.FirstOrDefault(r => r.Id == v.RutaId)?.Nombre ?? $"Ruta #{v.RutaId}",
                Autobus = autobuses.FirstOrDefault(a => a.Id == v.AutobusId)?.Placa ?? $"Autobús #{v.AutobusId}",
                Conductor = conductores.FirstOrDefault(c => c.Id == v.ConductorId)?.NombreCompleto ?? $"Conductor #{v.ConductorId}",
                Fecha = v.Fecha.ToShortDateString(),
                Estado = ViajePresentacionDto.DescribirEstado(v.Estado),
                Cupo = $"{v.CupoActual} / {v.CapacidadMaxima}"
            }).ToList();

            grid.DataSource = null;
            grid.DataSource = vista;
            if (grid.Columns["IdViaje"] is { } colId) colId.HeaderText = "Id viaje";
        }

        private async void BtnCancelar_Click(object sender, EventArgs e)
        {
            var grid = _dgvDia.Focused || _dgvDia.SelectedRows.Count > 0 ? _dgvDia : _dgvActivos;
            if (grid.CurrentRow is null)
            {
                CampoBuilder.MostrarMensaje(_lblMensajeListado, "Selecciona un viaje en alguna de las dos listas.", true);
                return;
            }

            var idCelda = grid.CurrentRow.Cells["IdViaje"]?.Value;
            if (idCelda is null || !int.TryParse(idCelda.ToString(), out var viajeId)) return;

            var motivo = Microsoft.VisualBasic.Interaction.InputBox(
                "Motivo de la cancelación:", "Cancelar viaje", "Cancelado por administración");
            if (string.IsNullOrWhiteSpace(motivo)) return;

            var dto = new CancelarViajePresentacionDto
            {
                ViajeId = viajeId,
                Motivo = motivo,
                CreadoPor = SesionActual.Usuario?.Correo
            };

            var resultado = await SgaApiClient.PostAsync<ViajePresentacionDto>("api/viajes/cancelar", dto);
            CampoBuilder.MostrarMensaje(_lblMensajeListado,
                resultado.EsExitoso ? "Viaje cancelado." : resultado.Error!, !resultado.EsExitoso);

            if (resultado.EsExitoso)
            {
                await CargarViajesDelDiaAsync();
                await CargarViajesActivosAsync();
            }
        }

        /// <summary>Forma mínima para leer conductores desde /api/usuarios (listado general).</summary>
        private sealed class UsuarioResumenApi
        {
            public int Id { get; set; }
            public string? Nombre { get; set; }
            public string? Apellido { get; set; }
            public string TipoUsuario { get; set; } = string.Empty;
            public string NombreCompleto => $"{Nombre} {Apellido}".Trim();
        }
    }
}