using SGA.Desktop.Api;
using SGA.Desktop.DTOs.Acceso;
using SGA.Desktop.DTOs.Notificacion;
using SGA.Desktop.DTOs.Usuario;
using SGA.Desktop.DTOs.Viaje;
using SGA.Desktop.Services;
using SGA.Desktop.UI;

namespace SGA.Desktop.Forms
{
    public partial class NotificacionesForm : Form
    {
        private bool _modoGeneral = true;
        private List<ViajePresentacionDto> _viajesRecientes = new();

        public NotificacionesForm()
        {
            InitializeComponent();
            ConstruirFormulario();
            _dtpDesde.Value = DateTime.Today.AddDays(-7);
            _dtpHasta.Value = DateTime.Today;
            _cmbTipo.SelectedIndex = 0;
            Load += async (_, _) =>
            {
                await CargarViajesRecientesAsync();
                await BuscarPorPeriodoAsync();
            };
        }

        // === Selector de destinatario ===

        private void BtnDestinatarioGeneral_Click(object sender, EventArgs e)
        {
            _modoGeneral = true;
            MostrarModoDestinatario(general: true);
        }

        private void BtnDestinatarioPorViaje_Click(object sender, EventArgs e)
        {
            _modoGeneral = false;
            MostrarModoDestinatario(general: false);
        }

        private async Task CargarViajesRecientesAsync()
        {
            var resultado = await SgaApiClient.GetAsync<List<ViajePresentacionDto>>("api/viajes/activos");
            if (!resultado.EsExitoso) return;

            _viajesRecientes = resultado.Valor!;
            _cmbViaje.DataSource = null;
            _cmbViaje.DataSource = _viajesRecientes;
            _cmbViaje.DisplayMember = nameof(ViajePresentacionDto.Id);
            _cmbViaje.ValueMember = nameof(ViajePresentacionDto.Id);
        }

        // === Envío ===

        private async void BtnEnviar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_txtTitulo.Text) || string.IsNullOrWhiteSpace(_txtMensaje.Text))
            {
                CampoBuilder.MostrarMensaje(_lblMensajeEnvio, "Título y mensaje son obligatorios.", true);
                return;
            }

            List<int> destinatarios;

            if (_modoGeneral)
            {
                var usuarios = await SgaApiClient.GetAsync<List<UsuarioResumenPresentacionDto>>("api/usuarios");
                if (!usuarios.EsExitoso)
                {
                    CampoBuilder.MostrarMensaje(_lblMensajeEnvio, usuarios.Error!, true);
                    return;
                }

                destinatarios = usuarios.Valor!
                    .Where(u => string.Equals(u.Estado, "Activo", StringComparison.OrdinalIgnoreCase) &&
                                (string.Equals(u.TipoUsuario, "Estudiante", StringComparison.OrdinalIgnoreCase) ||
                                 string.Equals(u.TipoUsuario, "Docente", StringComparison.OrdinalIgnoreCase) ||
                                 string.Equals(u.TipoUsuario, "Administrativo", StringComparison.OrdinalIgnoreCase)))
                    .Select(u => u.Id)
                    .ToList();
            }
            else
            {
                if (_cmbViaje.SelectedItem is not ViajePresentacionDto viaje)
                {
                    CampoBuilder.MostrarMensaje(_lblMensajeEnvio, "Selecciona un viaje.", true);
                    return;
                }

                var accesos = await SgaApiClient.GetAsync<List<AccesoPresentacionDto>>($"api/accesos/por-viaje/{viaje.Id}");
                if (!accesos.EsExitoso)
                {
                    CampoBuilder.MostrarMensaje(_lblMensajeEnvio, accesos.Error!, true);
                    return;
                }

                destinatarios = accesos.Valor!.Select(a => a.UsuarioTransporteId).Distinct().ToList();
            }

            if (destinatarios.Count == 0)
            {
                CampoBuilder.MostrarMensaje(_lblMensajeEnvio, "No se encontraron destinatarios para enviar la notificación.", true);
                return;
            }

            btnEnviar.Enabled = false;
            var enviados = 0;
            try
            {
                foreach (var usuarioId in destinatarios)
                {
                    var dto = new CrearNotificacionPresentacionDto
                    {
                        UsuarioTransporteId = usuarioId,
                        Tipo = _cmbTipo.Text,
                        Titulo = _txtTitulo.Text.Trim(),
                        Mensaje = _txtMensaje.Text.Trim(),
                        FechaHora = DateTime.Now,
                        CreadoPor = SesionActual.Usuario?.Correo
                    };

                    var resultado = await SgaApiClient.PostAsync<NotificacionPresentacionDto>("api/notificaciones", dto);
                    if (resultado.EsExitoso) enviados++;
                }
            }
            finally
            {
                btnEnviar.Enabled = true;
            }

            var destino = _modoGeneral ? "estudiantes y empleados activos" : "usuarios vinculados al viaje";
            CampoBuilder.MostrarMensaje(_lblMensajeEnvio, $"Notificación enviada a {enviados} {destino}.", enviados == 0);

            if (enviados > 0)
            {
                _txtTitulo.Clear();
                _txtMensaje.Clear();
                await BuscarPorPeriodoAsync();
            }
        }

        // === Historial ===

        private async void BtnBuscar_Click(object sender, EventArgs e) => await BuscarPorPeriodoAsync();

        private async Task BuscarPorPeriodoAsync()
        {
            var desde = _dtpDesde.Value.Date.ToString("yyyy-MM-dd");
            var hasta = _dtpHasta.Value.Date.ToString("yyyy-MM-dd");

            var resultado = await SgaApiClient.GetAsync<List<NotificacionPresentacionDto>>(
                $"api/notificaciones/por-periodo?desde={desde}&hasta={hasta}");

            if (!resultado.EsExitoso)
            {
                CampoBuilder.MostrarMensaje(_lblMensajeGrid, resultado.Error!, true);
                return;
            }

            var vista = resultado.Valor!.Select(n => new
            {
                Usuario = $"#{n.UsuarioTransporteId}",
                n.Tipo,
                n.Titulo,
                n.Mensaje,
                Fecha = n.FechaHora.ToString("dd/MM HH:mm"),
                Leida = n.Leida ? "Sí" : "No"
            }).ToList();

            _dgv.DataSource = null;
            _dgv.DataSource = vista;
            CampoBuilder.MostrarMensaje(_lblMensajeGrid, $"{resultado.Valor!.Count} registro(s) encontrado(s).", false);
        }

        private void Dgv_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (_dgv.Columns[e.ColumnIndex].Name != "Leida") return;
            var texto = e.Value?.ToString();

            e.CellStyle!.ForeColor = texto == "Sí"
                ? System.Drawing.Color.FromArgb(59, 109, 17)
                : System.Drawing.Color.FromArgb(158, 158, 158);
        }
    }
}