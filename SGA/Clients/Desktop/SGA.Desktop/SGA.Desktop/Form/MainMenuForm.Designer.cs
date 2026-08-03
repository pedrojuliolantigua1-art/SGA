namespace SGA.Desktop.Forms
{
    partial class MainMenuForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlLateral = new Panel();
            this.lblMarca = new Label();
            this.pnlDivisorUsuario = new Panel();
            this.lblUsuario = new Label();

            this.lblSeccionCatalogo = SGA.Desktop.UI.AppTheme.EtiquetaSeccionLateral("CATÁLOGO");
            this.btnAutobuses = new Button();
            this.btnConductores = new Button();
            this.btnRutas = new Button();

            this.lblSeccionOperacion = SGA.Desktop.UI.AppTheme.EtiquetaSeccionLateral("OPERACIÓN");
            this.btnViajes = new Button();
            this.btnAccesos = new Button();

            this.lblSeccionAutorizaciones = SGA.Desktop.UI.AppTheme.EtiquetaSeccionLateral("AUTORIZACIONES");
            this.btnPagos = new Button();

            this.lblSeccionAdministracion = SGA.Desktop.UI.AppTheme.EtiquetaSeccionLateral("ADMINISTRACIÓN");
            this.btnUsuarios = new Button();
            this.btnNotificaciones = new Button();

            this.lblSeccionAuditoria = SGA.Desktop.UI.AppTheme.EtiquetaSeccionLateral("AUDITORÍA");
            this.btnReportes = new Button();
            this.btnAuditoria = new Button();

            this.btnCerrarSesion = new Button();

            this.pnlEncabezado = new Panel();
            this.lblBreadcrumb = new Label();
            this.lblAvatar = new Label();
            this.pnlDivisorEncabezado = new Panel();

            this.pnlPrincipal = new Panel();
            this.pnlDashboard = new Panel();
            this.lblBienvenida = new Label();
            this.pnlKpis = new Panel();
            this.lblKpiViajesTitulo = new Label();
            this.lblKpiViajesValor = new Label();
            this.lblKpiAutobusesTitulo = new Label();
            this.lblKpiAutobusesValor = new Label();
            this.lblKpiIncidenciasTitulo = new Label();
            this.lblKpiIncidenciasValor = new Label();
            this.lblKpiIngresosTitulo = new Label();
            this.lblKpiIngresosValor = new Label();
            this.lblPlaceholderModulo = new Label();
            this.pnlContenido = new Panel();

            this.SuspendLayout();

            // === pnlLateral ===
            this.pnlLateral.Dock = DockStyle.Left;
            this.pnlLateral.Width = 260;
            this.pnlLateral.BackColor = SGA.Desktop.UI.AppTheme.AzulOscuro;

            // --- Marca ---
            this.lblMarca.Text = "SGA-ITLA";
            this.lblMarca.ForeColor = SGA.Desktop.UI.AppTheme.Blanco;
            this.lblMarca.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblMarca.AutoSize = true;
            this.lblMarca.Location = new System.Drawing.Point(20, 16);
            this.lblMarca.Cursor = Cursors.Hand;
            this.lblMarca.Click += new System.EventHandler(this.lblMarca_Click);


            // --- Divisor + usuario ---
            this.pnlDivisorUsuario.BackColor = System.Drawing.Color.FromArgb(30, 55, 95);
            this.pnlDivisorUsuario.Location = new System.Drawing.Point(0, 96);
            this.pnlDivisorUsuario.Size = new System.Drawing.Size(260, 1);

            this.lblUsuario.ForeColor = System.Drawing.Color.FromArgb(200, 216, 240);
            this.lblUsuario.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblUsuario.Location = new System.Drawing.Point(20, 52);
            this.lblUsuario.Size = new System.Drawing.Size(220, 36);
            this.lblUsuario.Text = "Usuario";

            // --- Sección Catálogo ---
            this.lblSeccionCatalogo.Location = new System.Drawing.Point(20, 108);
            this.lblSeccionCatalogo.Width = 220;

            this.btnAutobuses.Text = "Autobuses";
            this.btnAutobuses.Location = new System.Drawing.Point(0, 132);
            this.btnAutobuses.Size = new System.Drawing.Size(260, 34);
            this.btnAutobuses.Click += new System.EventHandler(this.btnAutobuses_Click);

            this.btnConductores.Text = "Conductores";
            this.btnConductores.Location = new System.Drawing.Point(0, 166);
            this.btnConductores.Size = new System.Drawing.Size(260, 34);
            this.btnConductores.Click += new System.EventHandler(this.btnConductores_Click);

            this.btnRutas.Text = "Rutas y horarios";
            this.btnRutas.Location = new System.Drawing.Point(0, 200);
            this.btnRutas.Size = new System.Drawing.Size(260, 34);
            this.btnRutas.Click += new System.EventHandler(this.btnRutas_Click);

            // --- Sección Operación ---
            this.lblSeccionOperacion.Location = new System.Drawing.Point(20, 244);
            this.lblSeccionOperacion.Width = 220;

            this.btnViajes.Text = "Viajes";
            this.btnViajes.Location = new System.Drawing.Point(0, 268);
            this.btnViajes.Size = new System.Drawing.Size(260, 34);
            this.btnViajes.Click += new System.EventHandler(this.btnViajes_Click);

            this.btnAccesos.Text = "Accesos";
            this.btnAccesos.Location = new System.Drawing.Point(0, 302);
            this.btnAccesos.Size = new System.Drawing.Size(260, 34);
            this.btnAccesos.Click += new System.EventHandler(this.btnAccesos_Click);

            // --- Sección Autorizaciones ---
            this.lblSeccionAutorizaciones.Location = new System.Drawing.Point(20, 346);
            this.lblSeccionAutorizaciones.Width = 220;

            this.btnPagos.Text = "Pagos y tarjetas";
            this.btnPagos.Location = new System.Drawing.Point(0, 370);
            this.btnPagos.Size = new System.Drawing.Size(260, 34);
            this.btnPagos.Click += new System.EventHandler(this.btnPagos_Click);

            // --- Sección Administración ---
            this.lblSeccionAdministracion.Location = new System.Drawing.Point(20, 414);
            this.lblSeccionAdministracion.Width = 220;

            this.btnUsuarios.Text = "Usuarios";
            this.btnUsuarios.Location = new System.Drawing.Point(0, 438);
            this.btnUsuarios.Size = new System.Drawing.Size(260, 34);
            this.btnUsuarios.Click += new System.EventHandler(this.btnUsuarios_Click);

            this.btnNotificaciones.Text = "Notificaciones";
            this.btnNotificaciones.Location = new System.Drawing.Point(0, 472);
            this.btnNotificaciones.Size = new System.Drawing.Size(260, 34);
            this.btnNotificaciones.Click += new System.EventHandler(this.btnNotificaciones_Click);

            // --- Sección Auditoría ---
            this.lblSeccionAuditoria.Location = new System.Drawing.Point(20, 516);
            this.lblSeccionAuditoria.Width = 220;

            this.btnReportes.Text = "Reportes";
            this.btnReportes.Location = new System.Drawing.Point(0, 540);
            this.btnReportes.Size = new System.Drawing.Size(260, 34);
            this.btnReportes.Click += new System.EventHandler(this.btnReportes_Click);

            this.btnAuditoria.Text = "Log de auditoría";
            this.btnAuditoria.Location = new System.Drawing.Point(0, 574);
            this.btnAuditoria.Size = new System.Drawing.Size(260, 34);
            this.btnAuditoria.Click += new System.EventHandler(this.btnAuditoria_Click);

            // --- Cerrar sesión (anclado abajo) ---
            this.btnCerrarSesion.Text = "Cerrar sesión";
            this.btnCerrarSesion.Anchor = AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Right;
            this.btnCerrarSesion.Location = new System.Drawing.Point(20, 700);
            this.btnCerrarSesion.Size = new System.Drawing.Size(220, 36);
            this.btnCerrarSesion.FlatStyle = FlatStyle.Flat;
            this.btnCerrarSesion.FlatAppearance.BorderSize = 1;
            this.btnCerrarSesion.FlatAppearance.BorderColor = SGA.Desktop.UI.AppTheme.Rojo;
            this.btnCerrarSesion.BackColor = SGA.Desktop.UI.AppTheme.AzulOscuro;
            this.btnCerrarSesion.ForeColor = System.Drawing.Color.FromArgb(245, 184, 184);
            this.btnCerrarSesion.Font = SGA.Desktop.UI.AppTheme.FuenteBaseNegrita;
            this.btnCerrarSesion.Click += new System.EventHandler(this.btnCerrarSesion_Click);

            this.pnlLateral.Controls.Add(this.btnCerrarSesion);
            this.pnlLateral.Controls.Add(this.btnAuditoria);
            this.pnlLateral.Controls.Add(this.btnReportes);
            this.pnlLateral.Controls.Add(this.lblSeccionAuditoria);
            this.pnlLateral.Controls.Add(this.btnNotificaciones);
            this.pnlLateral.Controls.Add(this.btnUsuarios);
            this.pnlLateral.Controls.Add(this.lblSeccionAdministracion);
            this.pnlLateral.Controls.Add(this.btnPagos);
            this.pnlLateral.Controls.Add(this.lblSeccionAutorizaciones);
            this.pnlLateral.Controls.Add(this.btnAccesos);
            this.pnlLateral.Controls.Add(this.btnViajes);
            this.pnlLateral.Controls.Add(this.lblSeccionOperacion);
            this.pnlLateral.Controls.Add(this.btnRutas);
            this.pnlLateral.Controls.Add(this.btnConductores);
            this.pnlLateral.Controls.Add(this.btnAutobuses);
            this.pnlLateral.Controls.Add(this.lblSeccionCatalogo);
            this.pnlLateral.Controls.Add(this.lblUsuario);
            this.pnlLateral.Controls.Add(this.pnlDivisorUsuario);
            this.pnlLateral.Controls.Add(this.lblMarca);

            // === pnlEncabezado (barra superior) ===
            this.pnlEncabezado.Dock = DockStyle.Top;
            this.pnlEncabezado.Height = 52;
            this.pnlEncabezado.BackColor = SGA.Desktop.UI.AppTheme.Blanco;

            this.lblBreadcrumb.Text = "Inicio";
            this.lblBreadcrumb.Font = SGA.Desktop.UI.AppTheme.FuenteSubtitulo;
            this.lblBreadcrumb.ForeColor = SGA.Desktop.UI.AppTheme.AzulOscuro;
            this.lblBreadcrumb.AutoSize = true;
            this.lblBreadcrumb.Location = new System.Drawing.Point(24, 16);

            this.lblAvatar.Text = string.Empty;
            this.lblAvatar.BackColor = SGA.Desktop.UI.AppTheme.AzulClaro;
            this.lblAvatar.ForeColor = SGA.Desktop.UI.AppTheme.Azul;
            this.lblAvatar.Font = SGA.Desktop.UI.AppTheme.FuenteBaseNegrita;
            this.lblAvatar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblAvatar.Size = new System.Drawing.Size(32, 32);
            this.lblAvatar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.lblAvatar.Location = new System.Drawing.Point(844, 10);

            this.pnlDivisorEncabezado.Dock = DockStyle.Bottom;
            this.pnlDivisorEncabezado.Height = 1;
            this.pnlDivisorEncabezado.BackColor = SGA.Desktop.UI.AppTheme.Borde;

            this.pnlEncabezado.Controls.Add(this.lblAvatar);
            this.pnlEncabezado.Controls.Add(this.lblBreadcrumb);
            this.pnlEncabezado.Controls.Add(this.pnlDivisorEncabezado);

            // === pnlPrincipal (todo lo que no es sidebar) ===
            this.pnlPrincipal.Dock = DockStyle.Fill;
            this.pnlPrincipal.BackColor = SGA.Desktop.UI.AppTheme.GrisClaro;

            // === pnlDashboard (KPIs + bienvenida, visible cuando no hay módulo abierto) ===
            this.pnlDashboard.Dock = DockStyle.Fill;
            this.pnlDashboard.BackColor = SGA.Desktop.UI.AppTheme.GrisClaro;
            this.pnlDashboard.Padding = new Padding(24, 20, 24, 20);

            this.lblBienvenida.Text = "Bienvenido(a) al SGA-ITLA";
            this.lblBienvenida.Font = SGA.Desktop.UI.AppTheme.FuenteTitulo;
            this.lblBienvenida.ForeColor = SGA.Desktop.UI.AppTheme.AzulOscuro;
            this.lblBienvenida.AutoSize = true;
            this.lblBienvenida.Location = new System.Drawing.Point(24, 20);

            // --- Tarjetas KPI ---
            
            this.pnlKpis.Location = new System.Drawing.Point(24, 64);
            this.pnlKpis.Size = new System.Drawing.Size(830, 90);

            CrearTarjetaKpi(this.pnlKpis, 0, "Viajes hoy", this.lblKpiViajesTitulo, this.lblKpiViajesValor);
            CrearTarjetaKpi(this.pnlKpis, 1, "Autobuses activos", this.lblKpiAutobusesTitulo, this.lblKpiAutobusesValor);
            CrearTarjetaKpi(this.pnlKpis, 2, "Incidencias abiertas", this.lblKpiIncidenciasTitulo, this.lblKpiIncidenciasValor);
            CrearTarjetaKpi(this.pnlKpis, 3, "Ingresos del día", this.lblKpiIngresosTitulo, this.lblKpiIngresosValor);

            this.lblPlaceholderModulo.Text = "Selecciona un módulo desde el menú lateral";
            this.lblPlaceholderModulo.ForeColor = System.Drawing.Color.FromArgb(158, 158, 158);
            this.lblPlaceholderModulo.Font = SGA.Desktop.UI.AppTheme.FuenteBase;
            this.lblPlaceholderModulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblPlaceholderModulo.BackColor = SGA.Desktop.UI.AppTheme.Blanco;
            this.lblPlaceholderModulo.Location = new System.Drawing.Point(24, 168);
            this.lblPlaceholderModulo.Size = new System.Drawing.Size(830, 280);

            this.pnlDashboard.Controls.Add(this.lblPlaceholderModulo);
            this.pnlDashboard.Controls.Add(this.pnlKpis);
            this.pnlDashboard.Controls.Add(this.lblBienvenida);

            // === pnlContenido (host de módulos embebidos) ===
            this.pnlContenido.Dock = DockStyle.Fill;
            this.pnlContenido.BackColor = SGA.Desktop.UI.AppTheme.GrisClaro;
            this.pnlContenido.Controls.Add(this.pnlDashboard);

            this.pnlPrincipal.Controls.Add(this.pnlContenido);
            this.pnlPrincipal.Controls.Add(this.pnlEncabezado);

            // === MainMenuForm ===
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1180, 760);
            this.Controls.Add(this.pnlPrincipal);
            this.Controls.Add(this.pnlLateral);
            this.MinimumSize = new System.Drawing.Size(1000, 640);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "SGA-ITLA";
            this.Load += new System.EventHandler(this.MainMenuForm_Load);
            this.FormClosed += new FormClosedEventHandler(this.MainMenuForm_FormClosed);

            this.ResumeLayout(false);
        }

        /// <summary>Crea una tarjeta KPI (título + valor) dentro del panel de indicadores.</summary>
        private void CrearTarjetaKpi(Panel contenedor, int indice, string titulo, Label lblTitulo, Label lblValor)
        {
            const int ancho = 200;
            const int espacio = 10;

            var tarjeta = new Panel
            {
                Location = new System.Drawing.Point(indice * (ancho + espacio), 0),
                Size = new System.Drawing.Size(ancho, 90),
                BackColor = SGA.Desktop.UI.AppTheme.Blanco,
                BorderStyle = BorderStyle.FixedSingle
            };

            lblTitulo.Text = titulo;
            lblTitulo.ForeColor = System.Drawing.Color.FromArgb(117, 117, 117);
            lblTitulo.Font = SGA.Desktop.UI.AppTheme.FuenteBase;
            lblTitulo.AutoSize = true;
            lblTitulo.Location = new System.Drawing.Point(14, 14);

            lblValor.Text = "—";
            lblValor.ForeColor = SGA.Desktop.UI.AppTheme.AzulOscuro;
            lblValor.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            lblValor.AutoSize = true;
            lblValor.Location = new System.Drawing.Point(14, 40);

            tarjeta.Controls.Add(lblValor);
            tarjeta.Controls.Add(lblTitulo);
            contenedor.Controls.Add(tarjeta);
        }

        private Panel pnlLateral;
        private Label lblMarca;
        private Panel pnlDivisorUsuario;
        private Label lblUsuario;

        private Label lblSeccionCatalogo;
        private Button btnAutobuses;
        private Button btnConductores;
        private Button btnRutas;

        private Label lblSeccionOperacion;
        private Button btnViajes;
        private Button btnAccesos;

        private Label lblSeccionAutorizaciones;
        private Button btnPagos;

        private Label lblSeccionAdministracion;
        private Button btnUsuarios;
        private Button btnNotificaciones;

        private Label lblSeccionAuditoria;
        private Button btnReportes;
        private Button btnAuditoria;

        private Button btnCerrarSesion;

        private Panel pnlEncabezado;
        private Label lblBreadcrumb;
        private Label lblAvatar;
        private Panel pnlDivisorEncabezado;

        private Panel pnlPrincipal;
        private Panel pnlDashboard;
        private Label lblBienvenida;
        private Panel pnlKpis;
        private Label lblKpiViajesTitulo;
        private Label lblKpiViajesValor;
        private Label lblKpiAutobusesTitulo;
        private Label lblKpiAutobusesValor;
        private Label lblKpiIncidenciasTitulo;
        private Label lblKpiIncidenciasValor;
        private Label lblKpiIngresosTitulo;
        private Label lblKpiIngresosValor;
        private Label lblPlaceholderModulo;
        private Panel pnlContenido;
    }
}