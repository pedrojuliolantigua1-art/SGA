using SGA.Desktop.Api;
using SGA.Desktop.DTOs.Autobus;
using SGA.Desktop.DTOs.Pago;
using SGA.Desktop.DTOs.Viaje;
using SGA.Desktop.Services;

namespace SGA.Desktop.Forms
{
    public partial class MainMenuForm : Form
    {
        private Form? _formularioActual;
        private Button? _botonActivo;

        public MainMenuForm()
        {
            InitializeComponent();
            EstilizarMenuLateral();
        }

        private void EstilizarMenuLateral()
        {
            foreach (var boton in new[]
            {
                btnAutobuses, btnConductores, btnRutas,
                btnViajes, btnAccesos,
                btnPagos,
                btnUsuarios, btnNotificaciones,
                btnReportes, btnAuditoria
            })
            {
                SGA.Desktop.UI.AppTheme.ComoBotonMenuLateral(boton);
            }
        }

        private async void MainMenuForm_Load(object sender, EventArgs e)
        {
            var usuario = SesionActual.Usuario;
            lblUsuario.Text = usuario is null
                ? "Usuario"
                : $"{usuario.Nombre} {usuario.Apellido}\n{usuario.TipoUsuario}";

            lblAvatar.Text = usuario is null
                ? "?"
                : ObtenerIniciales(usuario.Nombre, usuario.Apellido);

            await CargarIndicadoresAsync();
        }

        /// <summary>
        /// Carga los valores reales de las tarjetas KPI del dashboard consultando la API.
        /// "Incidencias abiertas" queda en "N/D" porque todavía no existe un endpoint
        /// en la API para listar incidencias (solo existe para crearlas).
        /// </summary>
        private async Task CargarIndicadoresAsync()
        {
            lblKpiViajesValor.Text = "…";
            lblKpiAutobusesValor.Text = "…";
            lblKpiIncidenciasValor.Text = "…";
            lblKpiIngresosValor.Text = "…";

            var hoy = DateTime.Today;

            // --- Viajes hoy ---
            var viajes = await SgaApiClient.GetAsync<List<ViajePresentacionDto>>(
                $"api/viajes/por-fecha?fecha={hoy:yyyy-MM-dd}");
            lblKpiViajesValor.Text = viajes.EsExitoso && viajes.Valor is not null
                ? viajes.Valor.Count.ToString()
                : "—";

            // --- Autobuses activos (Disponible o Activo) ---
            var autobuses = await SgaApiClient.GetAsync<List<AutobusPresentacionDto>>("api/autobuses");
            lblKpiAutobusesValor.Text = autobuses.EsExitoso && autobuses.Valor is not null
                ? autobuses.Valor.Count(a =>
                    string.Equals(a.Estado, "Disponible", StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(a.Estado, "Activo", StringComparison.OrdinalIgnoreCase)).ToString()
                : "—";

            //  Implementar ese Endpoint
            lblKpiIncidenciasValor.Text = "Ninguna";

            // --- Ingresos del día (pagos Registrados o Aplicados, excluye Rechazado/Anulado) ---
            var finDelDia = hoy.AddDays(1).AddSeconds(-1);
            var pagos = await SgaApiClient.GetAsync<List<PagoPresentacionDto>>(
                $"api/pagos/por-periodo?desde={hoy:yyyy-MM-dd}&hasta={finDelDia:yyyy-MM-ddTHH:mm:ss}");
            lblKpiIngresosValor.Text = pagos.EsExitoso && pagos.Valor is not null
                ? pagos.Valor.Where(p => p.Estado != 3 && p.Estado != 4).Sum(p => p.Monto).ToString("C2")
                : "—";
        }

        private static string ObtenerIniciales(string nombre, string apellido)
        {
            var n = string.IsNullOrWhiteSpace(nombre) ? "" : nombre[0].ToString();
            var a = string.IsNullOrWhiteSpace(apellido) ? "" : apellido[0].ToString();
            return (n + a).ToUpperInvariant();
        }

        /// <summary>
        /// Muestra el formulario del módulo seleccionado embebido dentro del panel de
        /// contenido de la ventana principal, en lugar de abrirlo en una ventana aparte.
        /// </summary>
        private void AbrirModulo(Form formulario, string titulo, Button botonSeleccionado)
        {
            if (_formularioActual is not null)
            {
                pnlContenido.Controls.Remove(_formularioActual);
                _formularioActual.Close();
                _formularioActual.Dispose();
                _formularioActual = null;
            }

            pnlDashboard.Visible = false;
            lblBreadcrumb.Text = titulo;

            formulario.TopLevel = false;
            formulario.FormBorderStyle = FormBorderStyle.None;
            formulario.Dock = DockStyle.Fill;

            pnlContenido.Controls.Add(formulario);
            pnlContenido.Controls.SetChildIndex(formulario, 0);

            formulario.Show();
            formulario.BringToFront();

            _formularioActual = formulario;
            ResaltarBoton(botonSeleccionado);
        }

        private async void lblMarca_Click(object sender, EventArgs e)
        {
            if (_formularioActual is not null)
            {
                pnlContenido.Controls.Remove(_formularioActual);
                _formularioActual.Close();
                _formularioActual.Dispose();
                _formularioActual = null;
            }

            if (_botonActivo is not null)
            {
                SGA.Desktop.UI.AppTheme.ComoBotonMenuLateral(_botonActivo, activo: false);
                _botonActivo = null;
            }

            lblBreadcrumb.Text = "Inicio";
            pnlDashboard.Visible = true;
            
        }

        private void ResaltarBoton(Button seleccionado)
        {
            if (_botonActivo is not null)
                SGA.Desktop.UI.AppTheme.ComoBotonMenuLateral(_botonActivo, activo: false);

            SGA.Desktop.UI.AppTheme.ComoBotonMenuLateral(seleccionado, activo: true);
            _botonActivo = seleccionado;
        }

        private void btnAutobuses_Click(object sender, EventArgs e)
            => AbrirModulo(new CatalogoTransporteForm(), "Catálogo › Autobuses", btnAutobuses);

        private void btnConductores_Click(object sender, EventArgs e)
            => AbrirModulo(new ConductoresForm(), "Catálogo › Conductores", btnConductores);

        private void btnRutas_Click(object sender, EventArgs e)
            => AbrirModulo(new RutasForm(), "Catálogo › Rutas y horarios", btnRutas);

        private void btnPagos_Click(object sender, EventArgs e)
            => AbrirModulo(new PagosForm(), "Autorizaciones › Pagos y tarjetas", btnPagos);

        private void btnViajes_Click(object sender, EventArgs e)
            => AbrirModulo(new ViajesForm(), "Operación › Viajes", btnViajes);

        private void btnUsuarios_Click(object sender, EventArgs e)
            => AbrirModulo(new UsuariosForm(), "Administración › Usuarios", btnUsuarios);

        private void btnAuditoria_Click(object sender, EventArgs e)
            => AbrirModulo(new AuditoriaForm(), "Auditoría › Log de auditoría", btnAuditoria);

        private void btnAccesos_Click(object sender, EventArgs e)
            => AbrirModulo(new AccesosForm(), "Operación › Accesos", btnAccesos);

        private void btnNotificaciones_Click(object sender, EventArgs e)
            => AbrirModulo(new NotificacionesForm(), "Administración › Notificaciones", btnNotificaciones);

        private void btnReportes_Click(object sender, EventArgs e)
            => AbrirModulo(new ReportesForm(), "Reportes › Incidencias de viajes", btnReportes);

        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            SesionActual.CerrarSesion();
            Close();
        }

        private void MainMenuForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}