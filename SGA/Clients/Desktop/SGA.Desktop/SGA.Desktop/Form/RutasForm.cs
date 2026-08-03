using SGA.Desktop.Api;
using SGA.Desktop.DTOs.Common;
using SGA.Desktop.DTOs.Horario;
using SGA.Desktop.DTOs.Parada;
using SGA.Desktop.DTOs.Ruta;
using SGA.Desktop.Services;
using SGA.Desktop.UI;

namespace SGA.Desktop.Forms
{
    public partial class RutasForm : Form
    {
        private List<RutaPresentacionDto> _rutas = new();
        private RutaPresentacionDto? _rutaSeleccionada;
        private List<ParadaPresentacionDto> _paradasActuales = new();

        public RutasForm()
        {
            InitializeComponent();
            ConstruirFormulario();
            Load += async (_, _) => await CargarRutasAsync();
        }

        private async Task CargarRutasAsync()
        {
            var resultado = await SgaApiClient.GetAsync<List<RutaPresentacionDto>>("api/rutas");
            if (!resultado.EsExitoso)
            {
                CampoBuilder.MostrarMensaje(_lblMensajeRuta, resultado.Error!, true);
                return;
            }

            _rutas = resultado.Valor!;
            _dgvRutas.DataSource = null;
            _dgvRutas.DataSource = _rutas;
            if (_dgvRutas.Columns["Id"] is { } col) col.Visible = false;
        }

        private async void DgvRutas_SelectionChanged(object sender, EventArgs e)
        {
            if (_dgvRutas.CurrentRow?.DataBoundItem is not RutaPresentacionDto ruta) return;

            _rutaSeleccionada = ruta;
            _txtNombre.Text = ruta.Nombre;
            _txtDescripcion.Text = ruta.Descripcion;
            _chkActiva.Checked = ruta.Activa;
            _lblMensajeRuta.Text = string.Empty;

            MostrarGestionRuta();
            await CargarDetalleAsync();
        }

        private void MostrarGestionRuta()
        {
            lblPlaceholder.Visible = false;
            pnlCrear.Visible = false;
            pnlGestionRuta.Visible = true;
        }

        private async void BtnGuardarRuta_Click(object sender, EventArgs e)
        {
            if (_rutaSeleccionada is null)
            {
                CampoBuilder.MostrarMensaje(_lblMensajeRuta, "Selecciona una ruta de la lista.", true);
                return;
            }

            var dto = new ActualizarRutaPresentacionDto
            {
                Nombre = _txtNombre.Text.Trim(),
                Descripcion = _txtDescripcion.Text.Trim(),
                Activa = _chkActiva.Checked
            };

            var resultado = await SgaApiClient.PutAsync<RutaPresentacionDto>($"api/rutas/{_rutaSeleccionada.Id}", dto);
            CampoBuilder.MostrarMensaje(_lblMensajeRuta,
                resultado.EsExitoso ? "Cambios guardados." : resultado.Error!, !resultado.EsExitoso);

            if (resultado.EsExitoso) await CargarRutasAsync();
        }

        private async void BtnEliminarRuta_Click(object sender, EventArgs e)
        {
            if (_rutaSeleccionada is null) return;

            var confirmar = MessageBox.Show(
                $"¿Eliminar la ruta \"{_rutaSeleccionada.Nombre}\"?",
                "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirmar != DialogResult.Yes) return;

            var dto = new EliminarPresentacionDto
            {
                Motivo = "Eliminada desde el módulo de Rutas",
                EliminadoPor = SesionActual.Usuario?.Correo
            };

            var resultado = await SgaApiClient.DeleteAsync<object>($"api/rutas/{_rutaSeleccionada.Id}", dto);
            CampoBuilder.MostrarMensaje(_lblMensajeRuta,
                resultado.EsExitoso ? "Ruta eliminada." : resultado.Error!, !resultado.EsExitoso);

            if (resultado.EsExitoso)
            {
                _rutaSeleccionada = null;
                pnlGestionRuta.Visible = false;
                lblPlaceholder.Visible = true;
                await CargarRutasAsync();
            }
        }

        // === Paradas y horarios de la ruta seleccionada ===

        private async Task CargarDetalleAsync()
        {
            if (_rutaSeleccionada is null) return;
            var rutaId = _rutaSeleccionada.Id;

            var paradas = await SgaApiClient.GetAsync<List<ParadaPresentacionDto>>($"api/paradas?rutaId={rutaId}");
            if (paradas.EsExitoso)
            {
                _paradasActuales = paradas.Valor!.OrderBy(p => p.Orden).ToList();
                _dgvParadas.DataSource = null;
                _dgvParadas.DataSource = _paradasActuales;
                if (_dgvParadas.Columns["Id"] is { } c1) c1.Visible = false;
                if (_dgvParadas.Columns["RutaId"] is { } c2) c2.Visible = false;
            }

            var horarios = await SgaApiClient.GetAsync<List<HorarioRutaPresentacionDto>>($"api/horariosruta?rutaId={rutaId}");
            if (horarios.EsExitoso)
            {
                _dgvHorarios.DataSource = null;
                _dgvHorarios.DataSource = horarios.Valor!.ToList();
                if (_dgvHorarios.Columns["Id"] is { } c3) c3.Visible = false;
                if (_dgvHorarios.Columns["RutaId"] is { } c4) c4.Visible = false;
            }

            lblParadasTitulo.Text = $"Paradas — {_rutaSeleccionada.Nombre}";
            lblHorariosTitulo.Text = $"Horarios — {_rutaSeleccionada.Nombre}";
        }

        private async void BtnAgregarParada_Click(object sender, EventArgs e)
        {
            if (_rutaSeleccionada is null || string.IsNullOrWhiteSpace(_txtParadaNombre.Text))
            {
                CampoBuilder.MostrarMensaje(_lblMensajeDetalle, "Selecciona una ruta e ingresa el nombre de la parada.", true);
                return;
            }

            var ordenNuevo = (int)_numParadaOrden.Value;
            if (_paradasActuales.Any(p => p.Orden == ordenNuevo))
            {
                CampoBuilder.MostrarMensaje(_lblMensajeDetalle, $"Ya existe una parada con el orden {ordenNuevo} en esta ruta.", true);
                return;
            }

            var dto = new CrearParadaPresentacionDto
            {
                RutaId = _rutaSeleccionada.Id,
                Nombre = _txtParadaNombre.Text.Trim(),
                Referencia = _txtParadaReferencia.Text.Trim(),
                Orden = ordenNuevo,
                CreadoPor = SesionActual.Usuario?.Correo
            };

            var resultado = await SgaApiClient.PostAsync<ParadaPresentacionDto>("api/paradas", dto);
            CampoBuilder.MostrarMensaje(_lblMensajeDetalle,
                resultado.EsExitoso ? "Parada agregada." : resultado.Error!, !resultado.EsExitoso);

            if (resultado.EsExitoso)
            {
                _txtParadaNombre.Clear();
                _txtParadaReferencia.Clear();
                await CargarDetalleAsync();
            }
        }

        private async void BtnEliminarParada_Click(object sender, EventArgs e)
        {
            if (_dgvParadas.CurrentRow?.DataBoundItem is not ParadaPresentacionDto parada)
            {
                CampoBuilder.MostrarMensaje(_lblMensajeDetalle, "Selecciona una parada de la lista.", true);
                return;
            }

            var confirmar = MessageBox.Show(
                $"¿Eliminar la parada \"{parada.Nombre}\"?",
                "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirmar != DialogResult.Yes) return;

            var dto = new EliminarPresentacionDto
            {
                Motivo = "Eliminada desde el módulo de Rutas",
                EliminadoPor = SesionActual.Usuario?.Correo
            };

            var resultado = await SgaApiClient.DeleteAsync<object>($"api/paradas/{parada.Id}", dto);
            CampoBuilder.MostrarMensaje(_lblMensajeDetalle,
                resultado.EsExitoso ? "Parada eliminada." : resultado.Error!, !resultado.EsExitoso);

            if (resultado.EsExitoso) await CargarDetalleAsync();
        }

        private async void DgvParadas_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= _dgvParadas.Rows.Count) return;
            if (_dgvParadas.Rows[e.RowIndex].DataBoundItem is not ParadaPresentacionDto parada) return;

            var confirmar = MessageBox.Show(
                $"¿Eliminar la parada \"{parada.Nombre}\"?",
                "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirmar != DialogResult.Yes) return;

            var dto = new EliminarPresentacionDto
            {
                Motivo = "Eliminada desde el módulo de Rutas",
                EliminadoPor = SesionActual.Usuario?.Correo
            };

            var resultado = await SgaApiClient.DeleteAsync<object>($"api/paradas/{parada.Id}", dto);
            CampoBuilder.MostrarMensaje(_lblMensajeDetalle,
                resultado.EsExitoso ? "Parada eliminada." : resultado.Error!, !resultado.EsExitoso);

            if (resultado.EsExitoso) await CargarDetalleAsync();
        }

        private async void BtnEliminarHorario_Click(object sender, EventArgs e)
        {
            if (_dgvHorarios.CurrentRow?.DataBoundItem is not HorarioRutaPresentacionDto horario)
            {
                CampoBuilder.MostrarMensaje(_lblMensajeDetalle, "Selecciona un horario de la lista.", true);
                return;
            }

            var confirmar = MessageBox.Show(
                "¿Eliminar este horario?",
                "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirmar != DialogResult.Yes) return;

            var dto = new EliminarPresentacionDto
            {
                Motivo = "Eliminado desde el módulo de Rutas",
                EliminadoPor = SesionActual.Usuario?.Correo
            };

            var resultado = await SgaApiClient.DeleteAsync<object>($"api/horariosruta/{horario.Id}", dto);
            CampoBuilder.MostrarMensaje(_lblMensajeDetalle,
                resultado.EsExitoso ? "Horario eliminado." : resultado.Error!, !resultado.EsExitoso);

            if (resultado.EsExitoso) await CargarDetalleAsync();
        }

        private async void DgvHorarios_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= _dgvHorarios.Rows.Count) return;
            if (_dgvHorarios.Rows[e.RowIndex].DataBoundItem is not HorarioRutaPresentacionDto horario) return;

            var confirmar = MessageBox.Show(
                "¿Eliminar este horario?",
                "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirmar != DialogResult.Yes) return;

            var dto = new EliminarPresentacionDto
            {
                Motivo = "Eliminado desde el módulo de Rutas",
                EliminadoPor = SesionActual.Usuario?.Correo
            };

            var resultado = await SgaApiClient.DeleteAsync<object>($"api/horariosruta/{horario.Id}", dto);
            CampoBuilder.MostrarMensaje(_lblMensajeDetalle,
                resultado.EsExitoso ? "Horario eliminado." : resultado.Error!, !resultado.EsExitoso);

            if (resultado.EsExitoso) await CargarDetalleAsync();
        }

        private async void BtnAgregarHorario_Click(object sender, EventArgs e)
        {
            if (_rutaSeleccionada is null)
            {
                CampoBuilder.MostrarMensaje(_lblMensajeDetalle, "Selecciona una ruta.", true);
                return;
            }

            if (_dtpLlegada.Value.TimeOfDay <= _dtpSalida.Value.TimeOfDay)
            {
                CampoBuilder.MostrarMensaje(_lblMensajeDetalle, "La hora de llegada debe ser posterior a la hora de salida.", true);
                return;
            }

            var dto = new CrearHorarioRutaPresentacionDto
            {
                RutaId = _rutaSeleccionada.Id,
                HoraSalida = _dtpSalida.Value.TimeOfDay,
                HoraLlegadaEstimada = _dtpLlegada.Value.TimeOfDay,
                CreadoPor = SesionActual.Usuario?.Correo
            };

            var resultado = await SgaApiClient.PostAsync<HorarioRutaPresentacionDto>("api/horariosruta", dto);
            CampoBuilder.MostrarMensaje(_lblMensajeDetalle,
                resultado.EsExitoso ? "Horario agregado." : resultado.Error!, !resultado.EsExitoso);

            if (resultado.EsExitoso) await CargarDetalleAsync();
        }

        // === Crear ruta ===

        private void BtnNuevo_Click(object sender, EventArgs e)
        {
            lblPlaceholder.Visible = false;
            pnlGestionRuta.Visible = false;
            pnlCrear.Visible = true;

            _txtCNombre.Clear();
            _txtCDescripcion.Clear();
            _chkCActiva.Checked = true;
            _lblMensajeCrear.Text = string.Empty;
        }

        private void BtnCancelarCrear_Click(object sender, EventArgs e)
        {
            pnlCrear.Visible = false;
            if (_rutaSeleccionada is not null) pnlGestionRuta.Visible = true;
            else lblPlaceholder.Visible = true;
        }

        private async void BtnGuardarNueva_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_txtCNombre.Text))
            {
                CampoBuilder.MostrarMensaje(_lblMensajeCrear, "El nombre de la ruta es obligatorio.", true);
                return;
            }

            var dto = new CrearRutaPresentacionDto
            {
                Nombre = _txtCNombre.Text.Trim(),
                Descripcion = _txtCDescripcion.Text.Trim(),
                Activa = _chkCActiva.Checked,
                CreadoPor = SesionActual.Usuario?.Correo
            };

            var resultado = await SgaApiClient.PostAsync<RutaPresentacionDto>("api/rutas", dto);
            CampoBuilder.MostrarMensaje(_lblMensajeCrear,
                resultado.EsExitoso ? "Ruta creada correctamente." : resultado.Error!, !resultado.EsExitoso);

            if (resultado.EsExitoso) await CargarRutasAsync();
        }

        // === Formato de la grilla de horarios ===

        private void DgvHorarios_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            var nombreColumna = _dgvHorarios.Columns[e.ColumnIndex].Name;
            if ((nombreColumna == "HoraSalida" || nombreColumna == "HoraLlegadaEstimada") && e.Value is TimeSpan ts)
            {
                e.Value = ts.ToString(@"hh\:mm");
                e.FormattingApplied = true;
            }
        }
    }
}