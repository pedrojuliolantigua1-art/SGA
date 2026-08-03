using SGA.Desktop.UI;

namespace SGA.Desktop.Forms
{
    partial class NotificacionesForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 760);
            this.MinimumSize = new System.Drawing.Size(880, 640);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Notificaciones — SGA-ITLA";
            this.ResumeLayout(false);
        }

        private void ConstruirFormulario()
        {
            this.pnlHeader = new Panel();
            this.lblTitulo = new Label();

            // --- Envío ---
            this.pnlEnvio = new Panel();
            this.lblEnvioTitulo = new Label();
            this.pnlDestinatario = new TableLayoutPanel();
            this.btnDestinatarioGeneral = new Button();
            this.btnDestinatarioPorViaje = new Button();
            this._cmbViaje = new ComboBox();
            this.pnlCamposEnvio = new TableLayoutPanel();
            this._cmbTipo = new ComboBox();
            this._txtTitulo = new TextBox();
            this.btnEnviar = new Button();
            this._txtMensaje = new TextBox();
            this._lblMensajeEnvio = new Label();

            // --- Filtros ---
            this.pnlFiltros = new TableLayoutPanel();
            this._dtpDesde = new DateTimePicker();
            this._dtpHasta = new DateTimePicker();
            this.btnBuscar = new Button();

            // --- Grid ---
            this.pnlGrid = new Panel();
            this._dgv = new DataGridView();
            this._lblMensajeGrid = new Label();

            ((System.ComponentModel.ISupportInitialize)(this._dgv)).BeginInit();
            this.SuspendLayout();

            // === Encabezado ===
            this.pnlHeader.Dock = DockStyle.Top;
            this.pnlHeader.Height = 55;
            this.pnlHeader.BackColor = AppTheme.AzulOscuro;
            this.lblTitulo.Text = "Administración — Notificaciones";
            this.lblTitulo.ForeColor = AppTheme.Blanco;
            this.lblTitulo.Font = AppTheme.FuenteTitulo;
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Location = new System.Drawing.Point(20, 15);
            this.pnlHeader.Controls.Add(this.lblTitulo);

            // === Envío ===
            var pnlEnvioFondo = new Panel { Dock = DockStyle.Top, Height = 330, Padding = new Padding(16), BackColor = AppTheme.GrisClaro };
            this.pnlEnvio.Dock = DockStyle.Fill;
            this.pnlEnvio.BackColor = AppTheme.Blanco;
            this.pnlEnvio.Padding = new Padding(14);

            this.lblEnvioTitulo.Text = "Enviar notificación";
            this.lblEnvioTitulo.Font = AppTheme.FuenteSubtitulo;
            this.lblEnvioTitulo.ForeColor = AppTheme.AzulOscuro;
            this.lblEnvioTitulo.Dock = DockStyle.Top;
            this.lblEnvioTitulo.Height = 26;

            // Selector de destinatario (toggle + combo de viaje, oculto en modo General)
            this.pnlDestinatario.Dock = DockStyle.Top;
            this.pnlDestinatario.Height = 34;
            this.pnlDestinatario.ColumnCount = 3;
            this.pnlDestinatario.Margin = new Padding(0, 4, 0, 8);
            this.pnlDestinatario.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 130F));
            this.pnlDestinatario.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 130F));
            this.pnlDestinatario.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));

            this.btnDestinatarioGeneral.Text = "General";
            this.btnDestinatarioGeneral.Dock = DockStyle.Fill;
            this.btnDestinatarioGeneral.Margin = new Padding(0, 0, 3, 0);
            this.btnDestinatarioGeneral.Click += new System.EventHandler(this.BtnDestinatarioGeneral_Click);

            this.btnDestinatarioPorViaje.Text = "Por viaje";
            this.btnDestinatarioPorViaje.Dock = DockStyle.Fill;
            this.btnDestinatarioPorViaje.Margin = new Padding(3, 0, 8, 0);
            this.btnDestinatarioPorViaje.Click += new System.EventHandler(this.BtnDestinatarioPorViaje_Click);

            this._cmbViaje.Dock = DockStyle.Fill;
            this._cmbViaje.Font = AppTheme.FuenteBase;
            this._cmbViaje.DropDownStyle = ComboBoxStyle.DropDownList;
            this._cmbViaje.Visible = false;

            this.pnlDestinatario.Controls.Add(this.btnDestinatarioGeneral, 0, 0);
            this.pnlDestinatario.Controls.Add(this.btnDestinatarioPorViaje, 1, 0);
            this.pnlDestinatario.Controls.Add(this._cmbViaje, 2, 0);

            // Campos de la notificación
            this.pnlCamposEnvio.Dock = DockStyle.Top;
            this.pnlCamposEnvio.Height = 72;
            this.pnlCamposEnvio.ColumnCount = 3;
            this.pnlCamposEnvio.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            this.pnlCamposEnvio.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 55F));
            this.pnlCamposEnvio.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));

            this._cmbTipo = AgregarComboEnCelda(this.pnlCamposEnvio, 0, "Tipo",
                new[] { "General", "Cambio de viaje", "Retraso", "Cancelación", "Vencimiento", "Saldo insuficiente" });

            this._txtTitulo = AgregarTextoEnCelda(this.pnlCamposEnvio, 1, "Título");

            this.btnEnviar = AgregarBotonEnCelda(this.pnlCamposEnvio, 2, "Enviar");
            this.btnEnviar.Click += new System.EventHandler(this.BtnEnviar_Click);

            var lblMensajeEtq = new Label { Text = "Mensaje", Dock = DockStyle.Top, AutoSize = false, Height = 26, Font = AppTheme.FuenteBase, ForeColor = AppTheme.GrisTexto, TextAlign = ContentAlignment.BottomLeft, Margin = new Padding(0, 6, 0, 0) };
            this._txtMensaje.Dock = DockStyle.Top;
            this._txtMensaje.Font = AppTheme.FuenteBase;
            this._txtMensaje.Multiline = true;
            this._txtMensaje.Height = 46;

            this._lblMensajeEnvio.Dock = DockStyle.Top;
            this._lblMensajeEnvio.Height = 24;
            this._lblMensajeEnvio.Font = AppTheme.FuenteBaseNegrita;
            this._lblMensajeEnvio.Margin = new Padding(0, 6, 0, 0);

            this.pnlEnvio.Controls.Add(this._lblMensajeEnvio);
            this.pnlEnvio.Controls.Add(this._txtMensaje);
            this.pnlEnvio.Controls.Add(lblMensajeEtq);
            this.pnlEnvio.Controls.Add(this.pnlCamposEnvio);
            this.pnlEnvio.Controls.Add(this.pnlDestinatario);
            this.pnlEnvio.Controls.Add(this.lblEnvioTitulo);

            pnlEnvioFondo.Controls.Add(this.pnlEnvio);

            // === Filtros ===
            var pnlFiltrosFondo = new Panel { Dock = DockStyle.Top, Height = 180, Padding = new Padding(16, 0, 16, 14), BackColor = AppTheme.GrisClaro };
            var pnlFiltrosCard = new Panel { Dock = DockStyle.Fill, BackColor = AppTheme.Blanco, Padding = new Padding(14) };

            this.pnlFiltros.Dock = DockStyle.Fill;
            this.pnlFiltros.ColumnCount = 3;
            this.pnlFiltros.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            this.pnlFiltros.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            this.pnlFiltros.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));

            this._dtpDesde = AgregarFechaEnCelda(this.pnlFiltros, 0, "Desde");
            this._dtpHasta = AgregarFechaEnCelda(this.pnlFiltros, 1, "Hasta");

            this.btnBuscar = AgregarBotonEnCelda(this.pnlFiltros, 2, "Buscar", secundario: true);
            this.btnBuscar.Click += new System.EventHandler(this.BtnBuscar_Click);

            pnlFiltrosCard.Controls.Add(this.pnlFiltros);
            pnlFiltrosFondo.Controls.Add(pnlFiltrosCard);

            // === Grid ===
            var pnlGridFondo = new Panel { Dock = DockStyle.Fill, Padding = new Padding(16, 0, 16, 16), BackColor = AppTheme.GrisClaro };
            this.pnlGrid.Dock = DockStyle.Fill;
            this.pnlGrid.BackColor = AppTheme.Blanco;
            this.pnlGrid.Padding = new Padding(12);
            pnlGridFondo.Controls.Add(this.pnlGrid);

            AppTheme.ComoGrillaEstandar(this._dgv);
            this._dgv.Dock = DockStyle.Fill;
            this._dgv.CellFormatting += new DataGridViewCellFormattingEventHandler(this.Dgv_CellFormatting);

            this._lblMensajeGrid.Dock = DockStyle.Bottom;
            this._lblMensajeGrid.Height = 24;
            this._lblMensajeGrid.Font = AppTheme.FuenteBaseNegrita;
            this._lblMensajeGrid.ForeColor = AppTheme.GrisTexto;

            this.pnlGrid.Controls.Add(this._dgv);
            this.pnlGrid.Controls.Add(this._lblMensajeGrid);

            this.Controls.Add(pnlGridFondo);
            this.Controls.Add(pnlFiltrosFondo);
            this.Controls.Add(pnlEnvioFondo);
            this.Controls.Add(this.pnlHeader);

            ((System.ComponentModel.ISupportInitialize)(this._dgv)).EndInit();
            this.ResumeLayout(false);

            MostrarModoDestinatario(general: true);
        }

        private static ComboBox AgregarComboEnCelda(TableLayoutPanel tabla, int columna, string etiqueta, string[] opciones)
        {
            var contenedor = new Panel { Dock = DockStyle.Fill, Margin = new Padding(6, 8, 6, 8) };
            var lbl = new Label
            {
                Text = etiqueta,
                Dock = DockStyle.Top,
                AutoSize = false,
                Height = 26,
                Font = AppTheme.FuenteBase,
                ForeColor = AppTheme.GrisTexto,
                TextAlign = ContentAlignment.BottomLeft
            };
            var cmb = new ComboBox { Dock = DockStyle.Top, Font = AppTheme.FuenteBase, DropDownStyle = ComboBoxStyle.DropDownList };
            cmb.Items.AddRange(opciones);
            contenedor.Controls.Add(cmb);
            contenedor.Controls.Add(lbl);
            tabla.Controls.Add(contenedor, columna, 0);
            return cmb;
        }

        private static TextBox AgregarTextoEnCelda(TableLayoutPanel tabla, int columna, string etiqueta)
        {
            var contenedor = new Panel { Dock = DockStyle.Fill, Margin = new Padding(6, 8, 6, 8) };
            var lbl = new Label
            {
                Text = etiqueta,
                Dock = DockStyle.Top,
                AutoSize = false,
                Height = 26,
                Font = AppTheme.FuenteBase,
                ForeColor = AppTheme.GrisTexto,
                TextAlign = ContentAlignment.BottomLeft
            };
            var txt = new TextBox { Dock = DockStyle.Top, Font = AppTheme.FuenteBase };
            contenedor.Controls.Add(txt);
            contenedor.Controls.Add(lbl);
            tabla.Controls.Add(contenedor, columna, 0);
            return txt;
        }

        private static DateTimePicker AgregarFechaEnCelda(TableLayoutPanel tabla, int columna, string etiqueta)
        {
            var contenedor = new Panel { Dock = DockStyle.Fill, Margin = new Padding(6, 8, 6, 8) };
            var lbl = new Label { Text = etiqueta, Dock = DockStyle.Top, AutoSize = false, Height = 26, Font = AppTheme.FuenteBase, ForeColor = AppTheme.GrisTexto, TextAlign = ContentAlignment.BottomLeft };
            var dtp = new DateTimePicker { Dock = DockStyle.Top, Font = AppTheme.FuenteBase, Format = DateTimePickerFormat.Short };
            contenedor.Controls.Add(dtp);
            contenedor.Controls.Add(lbl);
            tabla.Controls.Add(contenedor, columna, 0);
            return dtp;
        }

        /// <summary>Botón alineado con la fila de campos (espaciador de 26px + alto fijo 36px).
        /// <paramref name="secundario"/> usa el estilo de contorno en vez del azul sólido
        /// (para el "Buscar" de la grilla, distinto del "Enviar" primario).</summary>
        private static Button AgregarBotonEnCelda(TableLayoutPanel tabla, int columna, string texto, bool secundario = false)
        {
            var contenedor = new Panel { Dock = DockStyle.Fill, Margin = new Padding(6, 8, 0, 8) };
            var espaciador = new Label { Dock = DockStyle.Top, AutoSize = false, Height = 26 };
            var btn = new Button { Text = texto, Dock = DockStyle.Top, Height = 36 };
            if (secundario) AppTheme.ComoBotonSecundario(btn);
            else AppTheme.ComoBotonPrimario(btn);
            contenedor.Controls.Add(btn);
            contenedor.Controls.Add(espaciador);
            tabla.Controls.Add(contenedor, columna, 0);
            return btn;
        }

        /// <summary>Alterna entre modo "General" (combo de viaje oculto) y "Por viaje" (visible).</summary>
        private void MostrarModoDestinatario(bool general)
        {
            _cmbViaje.Visible = !general;
            AppTheme.ComoBotonToggle(btnDestinatarioGeneral, general);
            AppTheme.ComoBotonToggle(btnDestinatarioPorViaje, !general);
        }

        private Panel pnlHeader;
        private Label lblTitulo;

        private Panel pnlEnvio;
        private Label lblEnvioTitulo;
        private TableLayoutPanel pnlDestinatario;
        private Button btnDestinatarioGeneral;
        private Button btnDestinatarioPorViaje;
        private ComboBox _cmbViaje;
        private TableLayoutPanel pnlCamposEnvio;
        private ComboBox _cmbTipo;
        private TextBox _txtTitulo;
        private Button btnEnviar;
        private TextBox _txtMensaje;
        private Label _lblMensajeEnvio;

        private TableLayoutPanel pnlFiltros;
        private DateTimePicker _dtpDesde, _dtpHasta;
        private Button btnBuscar;

        private Panel pnlGrid;
        private DataGridView _dgv;
        private Label _lblMensajeGrid;
    }
}