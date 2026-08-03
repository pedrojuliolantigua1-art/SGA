using SGA.Desktop.Api;
using SGA.Desktop.DTOs.Viaje;
using SGA.Desktop.Services;
using SGA.Desktop.UI;

namespace SGA.Desktop.Forms
{
    public partial class ReportesForm : Form
    {
        private List<ViajePresentacionDto> _viajesActivos = new();

        public ReportesForm()
        {
            InitializeComponent();
            ConstruirFormulario();
            _dtpDesde.Value = DateTime.Today.AddDays(-30);
            _dtpHasta.Value = DateTime.Today;
            Load += async (_, _) =>
            {
                await CargarViajesActivosAsync();
                await BuscarPorPeriodoAsync();
            };
        }

        private async Task CargarViajesActivosAsync()
        {
            var resultado = await SgaApiClient.GetAsync<List<ViajePresentacionDto>>("api/viajes/activos");
            if (!resultado.EsExitoso)
            {
                CampoBuilder.MostrarMensaje(_lblMensajeReportar, "No se pudo cargar los viajes: " + resultado.Error, true);
                return;
            }

            _viajesActivos = resultado.Valor ?? new();
            _cmbViaje.DataSource = null;
            _cmbViaje.Items.Clear();
            foreach (var viaje in _viajesActivos.OrderBy(v => v.Id))
            {
                _cmbViaje.Items.Add($"Viaje #{viaje.Id} — Conductor #{viaje.ConductorId}");
            }
            _cmbViaje.Enabled = _viajesActivos.Count > 0;
        }

        private async void BtnPeriodo_Click(object sender, EventArgs e) => await BuscarPorPeriodoAsync();

        private async Task BuscarPorPeriodoAsync()
        {
            var desde = _dtpDesde.Value.Date.ToString("yyyy-MM-dd");
            var hasta = _dtpHasta.Value.Date.ToString("yyyy-MM-dd");

            var resultado = await SgaApiClient.GetAsync<List<IncidenciaPresentacionDto>>(
                $"api/viajes/incidencias/por-periodo?desde={desde}&hasta={hasta}");

            if (!resultado.EsExitoso)
            {
                CampoBuilder.MostrarMensaje(_lblMensaje, resultado.Error!, true);
                return;
            }

            var vista = resultado.Valor!.Select(i => new
            {
                i.Id,
                Viaje = $"Viaje #{i.ViajeId}",
                Conductor = string.IsNullOrWhiteSpace(i.ConductorNombre) ? $"Conductor #{i.ConductorId}" : i.ConductorNombre,
                i.Tipo,
                i.Descripcion,
                Fecha = i.FechaHora.ToString("dd/MM/yyyy hh:mm tt")
            }).ToList();

            _dgv.DataSource = null;
            _dgv.DataSource = vista;
            if (_dgv.Columns["Id"] is { } col) col.Visible = false;
            CampoBuilder.MostrarMensaje(_lblMensaje, $"{resultado.Valor!.Count} incidencia(s) en el período.", false);
        }

        // === Reportar incidencia ===

        private void BtnReportar_Click(object sender, EventArgs e)
        {
            pnlReportar.Visible = !pnlReportar.Visible;
            if (pnlReportar.Visible)
            {
                _txtTipo.Clear();
                _txtDescripcion.Clear();
                _lblMensajeReportar.Text = string.Empty;
            }
        }

        private void BtnCancelarIncidencia_Click(object sender, EventArgs e) => pnlReportar.Visible = false;

        private async void BtnGuardarIncidencia_Click(object sender, EventArgs e)
        {
            if (_cmbViaje.SelectedIndex < 0)
            {
                CampoBuilder.MostrarMensaje(_lblMensajeReportar, "No hay un viaje en curso seleccionado.", true);
                return;
            }

            var viaje = _viajesActivos.OrderBy(v => v.Id).ElementAt(_cmbViaje.SelectedIndex);
            var tipo = _txtTipo.Text.Trim();
            var descripcion = _txtDescripcion.Text.Trim();

            if (tipo.Length < 3)
            {
                CampoBuilder.MostrarMensaje(_lblMensajeReportar, "El tipo debe tener al menos 3 caracteres.", true);
                return;
            }

            if (descripcion.Length < 5)
            {
                CampoBuilder.MostrarMensaje(_lblMensajeReportar, "La descripción debe tener al menos 5 caracteres.", true);
                return;
            }

            var dto = new ReportarIncidenciaPresentacionDto
            {
                ViajeId = viaje.Id,
                ConductorId = viaje.ConductorId,
                Tipo = tipo,
                Descripcion = descripcion,
                FechaHora = _dtpFechaHora.Value,
                CreadoPor = SesionActual.Usuario?.Correo
            };

            btnGuardarIncidencia.Enabled = false;
            try
            {
                var resultado = await SgaApiClient.PostAsync<IncidenciaPresentacionDto>("api/viajes/incidencias", dto);
                CampoBuilder.MostrarMensaje(_lblMensajeReportar,
                    resultado.EsExitoso ? "Incidencia registrada correctamente." : resultado.Error!, !resultado.EsExitoso);

                if (resultado.EsExitoso)
                {
                    pnlReportar.Visible = false;
                    await BuscarPorPeriodoAsync();
                }
            }
            finally
            {
                btnGuardarIncidencia.Enabled = true;
            }
        }
    }
}