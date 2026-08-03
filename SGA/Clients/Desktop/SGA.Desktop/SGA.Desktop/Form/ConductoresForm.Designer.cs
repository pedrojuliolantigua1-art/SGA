using SGA.Desktop.UI;

namespace SGA.Desktop.Forms
{
    partial class ConductoresForm
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
            this.ClientSize = new System.Drawing.Size(1000, 700);
            this.MinimumSize = new System.Drawing.Size(860, 560);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Conductores — SGA-ITLA";
            this.ResumeLayout(false);
        }

        private void ConstruirFormulario()
        {
            this.pnlHeader = new Panel();
            this.lblTitulo = new Label();
            this.btnNuevo = new Button();

            this.tblRaiz = new TableLayoutPanel();
            this.pnlListado = new Panel();
            this._dgv = new DataGridView();

            this.pnlDetalle = new Panel();
            this.lblPlaceholder = new Label();

            this.pnlFicha = new Panel();
            this.lblFichaNombre = new Label();
            this.lblFichaSubtitulo = new Label();
            this.pnlCamposFicha = new TableLayoutPanel();
            this.lblDisponible = new Label();
            this._cmbDisponible = new ComboBox();
            this.btnGuardar = new Button();
            this.btnAplicarDisponibilidad = new Button();
            this.btnEliminar = new Button();
            this._lblMensajeDetalle = new Label();

            this.pnlCrear = new Panel();
            this.lblCrearTitulo = new Label();
            this.pnlCamposCrear = new TableLayoutPanel();
            this.btnRegistrar = new Button();
            this.btnCancelarCrear = new Button();
            this._lblMensajeCrear = new Label();

            ((System.ComponentModel.ISupportInitialize)(this._dgv)).BeginInit();
            this.SuspendLayout();

            // === Encabezado ===
            this.pnlHeader.Dock = DockStyle.Top;
            this.pnlHeader.Height = 55;
            this.pnlHeader.BackColor = AppTheme.AzulOscuro;

            this.lblTitulo.Text = "Catálogo de transporte — Conductores";
            this.lblTitulo.ForeColor = AppTheme.Blanco;
            this.lblTitulo.Font = AppTheme.FuenteTitulo;
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Location = new System.Drawing.Point(20, 15);

            this.btnNuevo.Text = "+ Nuevo conductor";
            this.btnNuevo.Size = new System.Drawing.Size(150, 32);
            this.btnNuevo.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            AppTheme.ComoBotonPrimario(this.btnNuevo);
            this.btnNuevo.Click += new System.EventHandler(this.BtnNuevo_Click);

            this.pnlHeader.Controls.Add(this.btnNuevo);
            this.pnlHeader.Controls.Add(this.lblTitulo);
            this.pnlHeader.Resize += (s, e) =>
                this.btnNuevo.Location = new System.Drawing.Point(this.pnlHeader.Width - 170, 12);

            // === Layout raíz: 2 columnas responsivas (58% / 42%) ===
            this.tblRaiz.Dock = DockStyle.Fill;
            this.tblRaiz.ColumnCount = 2;
            this.tblRaiz.RowCount = 1;
            this.tblRaiz.BackColor = AppTheme.GrisClaro;
            this.tblRaiz.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 58F));
            this.tblRaiz.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 42F));
            this.tblRaiz.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            // --- Listado ---
            this.pnlListado.Dock = DockStyle.Fill;
            this.pnlListado.Margin = new Padding(16, 16, 8, 16);
            this.pnlListado.BackColor = AppTheme.Blanco;
            this.pnlListado.Padding = new Padding(12);

            AppTheme.ComoGrillaEstandar(this._dgv);
            this._dgv.Dock = DockStyle.Fill;
            this._dgv.SelectionChanged += new System.EventHandler(this.Dgv_SelectionChanged);

            this.pnlListado.Controls.Add(this._dgv);

            // --- Detalle (contenedor de ficha / crear / placeholder) ---
            this.pnlDetalle.Dock = DockStyle.Fill;
            this.pnlDetalle.Margin = new Padding(8, 16, 16, 16);
            this.pnlDetalle.BackColor = AppTheme.Blanco;
            this.pnlDetalle.Padding = new Padding(14);

            this.lblPlaceholder.Dock = DockStyle.Fill;
            this.lblPlaceholder.Text = "Selecciona un conductor de la lista para ver su ficha,\no usa \"+ Nuevo conductor\" para registrar uno.";
            this.lblPlaceholder.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblPlaceholder.ForeColor = System.Drawing.Color.FromArgb(158, 158, 158);
            this.lblPlaceholder.Font = AppTheme.FuenteBase;

            // ============================================================
            // --- Ficha (ver/editar seleccionado) — responsiva, Dock=Top ---
            // ============================================================
            this.pnlFicha.Dock = DockStyle.Fill;
            this.pnlFicha.Visible = false;
            this.pnlFicha.AutoScroll = true;

            this.lblFichaNombre.Font = AppTheme.FuenteSubtitulo;
            this.lblFichaNombre.ForeColor = AppTheme.AzulOscuro;
            this.lblFichaNombre.Dock = DockStyle.Top;
            this.lblFichaNombre.Height = 28;

            this.lblFichaSubtitulo.Font = AppTheme.FuenteBase;
            this.lblFichaSubtitulo.ForeColor = System.Drawing.Color.FromArgb(117, 117, 117);
            this.lblFichaSubtitulo.Dock = DockStyle.Top;
            this.lblFichaSubtitulo.Height = 26;
            this.lblFichaSubtitulo.Margin = new Padding(0, 0, 0, 8);

            this.pnlCamposFicha.Dock = DockStyle.Top;
            this.pnlCamposFicha.Height = 225;
            this.pnlCamposFicha.Margin = new Padding(0, 0, 0, 8);
            this.pnlCamposFicha.ColumnCount = 2;
            this.pnlCamposFicha.RowCount = 3;
            this.pnlCamposFicha.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            this.pnlCamposFicha.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            for (int i = 0; i < 3; i++)
                this.pnlCamposFicha.RowStyles.Add(new RowStyle(SizeType.Percent, 100F / 3));

            this._txtNombre = CampoBuilder.CampoEnTabla(this.pnlCamposFicha, 0, 0, "Nombre");
            this._txtApellido = CampoBuilder.CampoEnTabla(this.pnlCamposFicha, 0, 1, "Apellido");
            this._txtCorreo = CampoBuilder.CampoEnTabla(this.pnlCamposFicha, 1, 0, "Correo");
            this._txtTelefono = CampoBuilder.CampoEnTabla(this.pnlCamposFicha, 1, 1, "Teléfono");
            this._txtLicencia = CampoBuilder.CampoEnTabla(this.pnlCamposFicha, 2, 0, "Número de licencia");
            this._dtpVencimiento = CampoBuilder.CampoFechaEnTabla(this.pnlCamposFicha, 2, 1, "Vencimiento");

            this.lblDisponible.Text = "Disponibilidad";
            this.lblDisponible.Font = AppTheme.FuenteBase;
            this.lblDisponible.ForeColor = AppTheme.GrisTexto;
            this.lblDisponible.Dock = DockStyle.Top;
            this.lblDisponible.Height = 22;

            this._cmbDisponible.DropDownStyle = ComboBoxStyle.DropDownList;
            this._cmbDisponible.Font = AppTheme.FuenteBase;
            this._cmbDisponible.Dock = DockStyle.Top;
            this._cmbDisponible.Margin = new Padding(0, 0, 0, 12);
            this._cmbDisponible.Items.AddRange(new object[] { "Disponible", "No disponible" });

            var pnlBotonesFicha = new TableLayoutPanel { Dock = DockStyle.Top, Height = 40, ColumnCount = 2, Margin = new Padding(0, 0, 0, 8) };
            pnlBotonesFicha.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            pnlBotonesFicha.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));

            this.btnGuardar.Text = "Guardar cambios";
            this.btnGuardar.Dock = DockStyle.Fill;
            this.btnGuardar.Margin = new Padding(0, 0, 3, 0);
            AppTheme.ComoBotonPrimario(this.btnGuardar);
            this.btnGuardar.Click += new System.EventHandler(this.BtnGuardar_Click);

            this.btnAplicarDisponibilidad.Text = "Aplicar disponibilidad";
            this.btnAplicarDisponibilidad.Dock = DockStyle.Fill;
            this.btnAplicarDisponibilidad.Margin = new Padding(3, 0, 0, 0);
            AppTheme.ComoBotonSecundario(this.btnAplicarDisponibilidad);
            this.btnAplicarDisponibilidad.Click += new System.EventHandler(this.BtnAplicarDisponibilidad_Click);

            pnlBotonesFicha.Controls.Add(this.btnGuardar, 0, 0);
            pnlBotonesFicha.Controls.Add(this.btnAplicarDisponibilidad, 1, 0);

            this.btnEliminar.Text = "Dar de baja";
            this.btnEliminar.Dock = DockStyle.Top;
            this.btnEliminar.Height = 34;
            this.btnEliminar.Margin = new Padding(0, 0, 0, 12);
            AppTheme.ComoBotonPeligro(this.btnEliminar);
            this.btnEliminar.Click += new System.EventHandler(this.BtnEliminar_Click);

            this._lblMensajeDetalle.Dock = DockStyle.Top;
            this._lblMensajeDetalle.Height = 40;
            this._lblMensajeDetalle.Font = AppTheme.FuenteBaseNegrita;

            this.pnlFicha.Controls.Add(this._lblMensajeDetalle);
            this.pnlFicha.Controls.Add(this.btnEliminar);
            this.pnlFicha.Controls.Add(pnlBotonesFicha);
            this.pnlFicha.Controls.Add(this._cmbDisponible);
            this.pnlFicha.Controls.Add(this.lblDisponible);
            this.pnlFicha.Controls.Add(this.pnlCamposFicha);
            this.pnlFicha.Controls.Add(this.lblFichaSubtitulo);
            this.pnlFicha.Controls.Add(this.lblFichaNombre);

            // ============================================================
            // --- Crear (registro de conductor nuevo) — responsiva ---
            // ============================================================
            this.pnlCrear.Dock = DockStyle.Fill;
            this.pnlCrear.Visible = false;
            this.pnlCrear.AutoScroll = true;

            this.lblCrearTitulo.Text = "Nuevo conductor";
            this.lblCrearTitulo.Font = AppTheme.FuenteSubtitulo;
            this.lblCrearTitulo.ForeColor = AppTheme.AzulOscuro;
            this.lblCrearTitulo.Dock = DockStyle.Top;
            this.lblCrearTitulo.Height = 32;
            this.lblCrearTitulo.Margin = new Padding(0, 0, 0, 8);

            this.pnlCamposCrear.Dock = DockStyle.Top;
            this.pnlCamposCrear.Height = 300;
            this.pnlCamposCrear.Margin = new Padding(0, 0, 0, 12);
            this.pnlCamposCrear.ColumnCount = 2;
            this.pnlCamposCrear.RowCount = 4;
            this.pnlCamposCrear.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            this.pnlCamposCrear.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            for (int i = 0; i < 4; i++)
                this.pnlCamposCrear.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));

            this._txtNNombre = CampoBuilder.CampoEnTabla(this.pnlCamposCrear, 0, 0, "Nombre");
            this._txtNApellido = CampoBuilder.CampoEnTabla(this.pnlCamposCrear, 0, 1, "Apellido");
            this._txtNCorreo = CampoBuilder.CampoEnTabla(this.pnlCamposCrear, 1, 0, "Correo");
            this._txtNTelefono = CampoBuilder.CampoEnTabla(this.pnlCamposCrear, 1, 1, "Teléfono");
            this._txtNLicencia = CampoBuilder.CampoEnTabla(this.pnlCamposCrear, 2, 0, "Número de licencia");
            this._dtpNVencimiento = CampoBuilder.CampoFechaEnTabla(this.pnlCamposCrear, 2, 1, "Vencimiento");
            this._txtNPassword = CampoBuilder.CampoEnTabla(this.pnlCamposCrear, 3, 0, "Contraseña inicial", colSpan: 2);
            this._txtNPassword.UseSystemPasswordChar = true;

            var pnlBotonesCrear = new TableLayoutPanel { Dock = DockStyle.Top, Height = 40, ColumnCount = 2, Margin = new Padding(0, 0, 0, 8) };
            pnlBotonesCrear.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            pnlBotonesCrear.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));

            this.btnRegistrar.Text = "Registrar conductor";
            this.btnRegistrar.Dock = DockStyle.Fill;
            this.btnRegistrar.Margin = new Padding(0, 0, 3, 0);
            AppTheme.ComoBotonPrimario(this.btnRegistrar);
            this.btnRegistrar.Click += new System.EventHandler(this.BtnRegistrar_Click);

            this.btnCancelarCrear.Text = "Cancelar";
            this.btnCancelarCrear.Dock = DockStyle.Fill;
            this.btnCancelarCrear.Margin = new Padding(3, 0, 0, 0);
            AppTheme.ComoBotonSecundario(this.btnCancelarCrear);
            this.btnCancelarCrear.Click += new System.EventHandler(this.BtnCancelarCrear_Click);

            pnlBotonesCrear.Controls.Add(this.btnRegistrar, 0, 0);
            pnlBotonesCrear.Controls.Add(this.btnCancelarCrear, 1, 0);

            this._lblMensajeCrear.Dock = DockStyle.Top;
            this._lblMensajeCrear.Height = 40;
            this._lblMensajeCrear.Font = AppTheme.FuenteBaseNegrita;

            this.pnlCrear.Controls.Add(this._lblMensajeCrear);
            this.pnlCrear.Controls.Add(pnlBotonesCrear);
            this.pnlCrear.Controls.Add(this.pnlCamposCrear);
            this.pnlCrear.Controls.Add(this.lblCrearTitulo);

            this.pnlDetalle.Controls.Add(this.pnlCrear);
            this.pnlDetalle.Controls.Add(this.pnlFicha);
            this.pnlDetalle.Controls.Add(this.lblPlaceholder);

            this.tblRaiz.Controls.Add(this.pnlListado, 0, 0);
            this.tblRaiz.Controls.Add(this.pnlDetalle, 1, 0);

            this.Controls.Add(this.tblRaiz);
            this.Controls.Add(this.pnlHeader);

            ((System.ComponentModel.ISupportInitialize)(this._dgv)).EndInit();
            this.ResumeLayout(false);
        }

        private Panel pnlHeader;
        private Label lblTitulo;
        private Button btnNuevo;

        private TableLayoutPanel tblRaiz;
        private Panel pnlListado;
        private DataGridView _dgv;

        private Panel pnlDetalle;
        private Label lblPlaceholder;

        private Panel pnlFicha;
        private Label lblFichaNombre;
        private Label lblFichaSubtitulo;
        private TableLayoutPanel pnlCamposFicha;
        private TextBox _txtNombre, _txtApellido, _txtCorreo, _txtTelefono, _txtLicencia;
        private DateTimePicker _dtpVencimiento;
        private Label lblDisponible;
        private ComboBox _cmbDisponible;
        private Button btnGuardar;
        private Button btnAplicarDisponibilidad;
        private Button btnEliminar;
        private Label _lblMensajeDetalle;

        private Panel pnlCrear;
        private Label lblCrearTitulo;
        private TableLayoutPanel pnlCamposCrear;
        private TextBox _txtNNombre, _txtNApellido, _txtNCorreo, _txtNTelefono, _txtNLicencia, _txtNPassword;
        private DateTimePicker _dtpNVencimiento;
        private Button btnRegistrar;
        private Button btnCancelarCrear;
        private Label _lblMensajeCrear;
    }
}