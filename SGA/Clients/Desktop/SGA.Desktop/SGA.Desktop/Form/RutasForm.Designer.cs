using SGA.Desktop.UI;

namespace SGA.Desktop.Forms
{
    partial class RutasForm
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
            this.ClientSize = new System.Drawing.Size(1060, 740);
            this.MinimumSize = new System.Drawing.Size(920, 620);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Rutas y horarios — SGA-ITLA";
            this.ResumeLayout(false);
        }

        private void ConstruirFormulario()
        {
            this.pnlHeader = new Panel();
            this.lblTitulo = new Label();
            this.btnNuevo = new Button();

            this.tblRaiz = new TableLayoutPanel();

            // --- Columna izquierda: lista + ficha de ruta ---
            this.pnlIzquierda = new Panel();
            this._dgvRutas = new DataGridView();
            this.lblFichaTitulo = new Label();
            this._txtNombre = new TextBox();
            this._txtDescripcion = new TextBox();
            this._chkActiva = new CheckBox();
            this.btnGuardarRuta = new Button();
            this.btnEliminarRuta = new Button();
            this._lblMensajeRuta = new Label();

            // --- Columna derecha: paradas + horarios / crear ---
            this.pnlDerecha = new Panel();
            this.lblPlaceholder = new Label();

            this.pnlGestionRuta = new Panel();
            this.pnlParadas = new Panel();
            this.lblParadasTitulo = new Label();
            this._dgvParadas = new DataGridView();
            this.pnlAgregarParada = new TableLayoutPanel();
            this._numParadaOrden = new NumericUpDown();
            this._txtParadaNombre = new TextBox();
            this._txtParadaReferencia = new TextBox();
            this.btnAgregarParada = new Button();
            this.btnEliminarParada = new Button();

            this.pnlHorarios = new Panel();
            this.lblHorariosTitulo = new Label();
            this._dgvHorarios = new DataGridView();
            this.pnlAgregarHorario = new TableLayoutPanel();
            this._dtpSalida = new DateTimePicker();
            this._dtpLlegada = new DateTimePicker();
            this.btnAgregarHorario = new Button();
            this.btnEliminarHorario = new Button();
            this._lblMensajeDetalle = new Label();

            this.pnlCrear = new Panel();
            this.lblCrearTitulo = new Label();
            this._txtCNombre = new TextBox();
            this._txtCDescripcion = new TextBox();
            this._chkCActiva = new CheckBox();
            this.btnGuardarNueva = new Button();
            this.btnCancelarCrear = new Button();
            this._lblMensajeCrear = new Label();

            ((System.ComponentModel.ISupportInitialize)(this._dgvRutas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._dgvParadas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._dgvHorarios)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._numParadaOrden)).BeginInit();
            this.SuspendLayout();

            // === Encabezado ===
            this.pnlHeader.Dock = DockStyle.Top;
            this.pnlHeader.Height = 55;
            this.pnlHeader.BackColor = AppTheme.AzulOscuro;

            this.lblTitulo.Text = "Catálogo de transporte — Rutas y horarios";
            this.lblTitulo.ForeColor = AppTheme.Blanco;
            this.lblTitulo.Font = AppTheme.FuenteTitulo;
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Location = new System.Drawing.Point(20, 15);

            this.btnNuevo.Text = "+ Nueva ruta";
            this.btnNuevo.Size = new System.Drawing.Size(130, 32);
            this.btnNuevo.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            AppTheme.ComoBotonPrimario(this.btnNuevo);
            this.btnNuevo.Click += new System.EventHandler(this.BtnNuevo_Click);

            this.pnlHeader.Controls.Add(this.btnNuevo);
            this.pnlHeader.Controls.Add(this.lblTitulo);
            this.pnlHeader.Resize += (s, e) =>
                this.btnNuevo.Location = new System.Drawing.Point(this.pnlHeader.Width - 150, 12);

            // === Layout raíz: 2 columnas (40% / 60%) ===
            this.tblRaiz.Dock = DockStyle.Fill;
            this.tblRaiz.ColumnCount = 2;
            this.tblRaiz.RowCount = 1;
            this.tblRaiz.BackColor = AppTheme.GrisClaro;
            this.tblRaiz.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            this.tblRaiz.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
            this.tblRaiz.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            // === Columna izquierda ===
            this.pnlIzquierda.Dock = DockStyle.Fill;
            this.pnlIzquierda.Margin = new Padding(16, 16, 8, 16);
            this.pnlIzquierda.BackColor = AppTheme.Blanco;
            this.pnlIzquierda.Padding = new Padding(12);
            this.pnlIzquierda.AutoScroll = true;

            AppTheme.ComoGrillaEstandar(this._dgvRutas);
            this._dgvRutas.Dock = DockStyle.Top;
            this._dgvRutas.Height = 220;
            this._dgvRutas.SelectionChanged += new System.EventHandler(this.DgvRutas_SelectionChanged);

            this.lblFichaTitulo.Text = "Editar ruta seleccionada";
            this.lblFichaTitulo.Font = AppTheme.FuenteSubtitulo;
            this.lblFichaTitulo.ForeColor = AppTheme.AzulOscuro;
            this.lblFichaTitulo.Dock = DockStyle.Top;
            this.lblFichaTitulo.Height = 30;
            this.lblFichaTitulo.Margin = new Padding(0, 10, 0, 0);

            var lblNombre = new Label { Text = "Nombre", Dock = DockStyle.Top, Height = 18, Font = AppTheme.FuenteBase, ForeColor = AppTheme.GrisTexto };
            this._txtNombre.Dock = DockStyle.Top;
            this._txtNombre.Font = AppTheme.FuenteBase;
            this._txtNombre.Margin = new Padding(0, 0, 0, 8);

            var lblDescripcion = new Label { Text = "Descripción", Dock = DockStyle.Top, Height = 18, Font = AppTheme.FuenteBase, ForeColor = AppTheme.GrisTexto };
            this._txtDescripcion.Dock = DockStyle.Top;
            this._txtDescripcion.Font = AppTheme.FuenteBase;
            this._txtDescripcion.Margin = new Padding(0, 0, 0, 8);

            this._chkActiva.Text = "Ruta activa";
            this._chkActiva.Dock = DockStyle.Top;
            this._chkActiva.Height = 26;
            this._chkActiva.Font = AppTheme.FuenteBase;
            this._chkActiva.Margin = new Padding(0, 0, 0, 8);

            var pnlBotonesRuta = new TableLayoutPanel { Dock = DockStyle.Top, Height = 40, ColumnCount = 2 };
            pnlBotonesRuta.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            pnlBotonesRuta.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));

            this.btnGuardarRuta.Text = "Guardar";
            this.btnGuardarRuta.Dock = DockStyle.Fill;
            this.btnGuardarRuta.Margin = new Padding(0, 0, 3, 0);
            AppTheme.ComoBotonPrimario(this.btnGuardarRuta);
            this.btnGuardarRuta.Click += new System.EventHandler(this.BtnGuardarRuta_Click);

            this.btnEliminarRuta.Text = "Eliminar";
            this.btnEliminarRuta.Dock = DockStyle.Fill;
            this.btnEliminarRuta.Margin = new Padding(3, 0, 0, 0);
            AppTheme.ComoBotonPeligro(this.btnEliminarRuta);
            this.btnEliminarRuta.Click += new System.EventHandler(this.BtnEliminarRuta_Click);

            pnlBotonesRuta.Controls.Add(this.btnGuardarRuta, 0, 0);
            pnlBotonesRuta.Controls.Add(this.btnEliminarRuta, 1, 0);

            this._lblMensajeRuta.Dock = DockStyle.Top;
            this._lblMensajeRuta.Height = 30;
            this._lblMensajeRuta.Font = AppTheme.FuenteBaseNegrita;
            this._lblMensajeRuta.Margin = new Padding(0, 6, 0, 0);

            // Orden: agregado en reversa para que quede top-to-bottom correcto con Dock=Top.
            this.pnlIzquierda.Controls.Add(this._lblMensajeRuta);
            this.pnlIzquierda.Controls.Add(pnlBotonesRuta);
            this.pnlIzquierda.Controls.Add(this._chkActiva);
            this.pnlIzquierda.Controls.Add(this._txtDescripcion);
            this.pnlIzquierda.Controls.Add(lblDescripcion);
            this.pnlIzquierda.Controls.Add(this._txtNombre);
            this.pnlIzquierda.Controls.Add(lblNombre);
            this.pnlIzquierda.Controls.Add(this.lblFichaTitulo);
            this.pnlIzquierda.Controls.Add(this._dgvRutas);

            // === Columna derecha ===
            this.pnlDerecha.Dock = DockStyle.Fill;
            this.pnlDerecha.Margin = new Padding(8, 16, 16, 16);
            this.pnlDerecha.BackColor = AppTheme.GrisClaro;

            this.lblPlaceholder.Dock = DockStyle.Fill;
            this.lblPlaceholder.Text = "Selecciona una ruta de la lista para ver sus paradas y horarios,\no usa \"+ Nueva ruta\" para crear una.";
            this.lblPlaceholder.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblPlaceholder.ForeColor = System.Drawing.Color.FromArgb(158, 158, 158);
            this.lblPlaceholder.Font = AppTheme.FuenteBase;
            this.lblPlaceholder.BackColor = AppTheme.Blanco;

            // --- Gestión de ruta (paradas + horarios) ---
            this.pnlGestionRuta.Dock = DockStyle.Fill;
            this.pnlGestionRuta.Visible = false;
            this.pnlGestionRuta.AutoScroll = true;

            // Paradas
            this.pnlParadas.Dock = DockStyle.Top;
            this.pnlParadas.Height = 260;
            this.pnlParadas.Margin = new Padding(0, 0, 0, 14);
            this.pnlParadas.BackColor = AppTheme.Blanco;
            this.pnlParadas.Padding = new Padding(12);

            this.lblParadasTitulo.Text = "Paradas";
            this.lblParadasTitulo.Dock = DockStyle.Top;
            this.lblParadasTitulo.Height = 24;
            this.lblParadasTitulo.Font = AppTheme.FuenteSubtitulo;
            this.lblParadasTitulo.ForeColor = AppTheme.AzulOscuro;

            AppTheme.ComoGrillaEstandar(this._dgvParadas);
            this._dgvParadas.Dock = DockStyle.Fill;
            this._dgvParadas.CellDoubleClick += new DataGridViewCellEventHandler(this.DgvParadas_CellDoubleClick);

            this.pnlAgregarParada.Dock = DockStyle.Bottom;
            this.pnlAgregarParada.Height = 50;
            this.pnlAgregarParada.ColumnCount = 5;
            this.pnlAgregarParada.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 70F));
            this.pnlAgregarParada.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 38F));
            this.pnlAgregarParada.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 38F));
            this.pnlAgregarParada.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80F));
            this.pnlAgregarParada.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 90F));

            this._numParadaOrden.Dock = DockStyle.Fill;
            this._numParadaOrden.Margin = new Padding(0, 22, 4, 0);
            this._numParadaOrden.Minimum = 1;
            this._numParadaOrden.Maximum = 100;

            this._txtParadaNombre.Dock = DockStyle.Fill;
            this._txtParadaNombre.Margin = new Padding(4, 22, 4, 0);
            this._txtParadaNombre.PlaceholderText = "Nombre de la parada";

            this._txtParadaReferencia.Dock = DockStyle.Fill;
            this._txtParadaReferencia.Margin = new Padding(4, 22, 4, 0);
            this._txtParadaReferencia.PlaceholderText = "Referencia";

            this.btnAgregarParada.Text = "Agregar";
            this.btnAgregarParada.Dock = DockStyle.Fill;
            this.btnAgregarParada.Margin = new Padding(4, 22, 4, 0);
            AppTheme.ComoBotonPrimario(this.btnAgregarParada);
            this.btnAgregarParada.Click += new System.EventHandler(this.BtnAgregarParada_Click);

            this.btnEliminarParada.Text = "Eliminar";
            this.btnEliminarParada.Dock = DockStyle.Fill;
            this.btnEliminarParada.Margin = new Padding(4, 22, 0, 0);
            AppTheme.ComoBotonPeligro(this.btnEliminarParada);
            this.btnEliminarParada.Click += new System.EventHandler(this.BtnEliminarParada_Click);

            this.pnlAgregarParada.Controls.Add(this._numParadaOrden, 0, 0);
            this.pnlAgregarParada.Controls.Add(this._txtParadaNombre, 1, 0);
            this.pnlAgregarParada.Controls.Add(this._txtParadaReferencia, 2, 0);
            this.pnlAgregarParada.Controls.Add(this.btnAgregarParada, 3, 0);
            this.pnlAgregarParada.Controls.Add(this.btnEliminarParada, 4, 0);

            this.pnlParadas.Controls.Add(this._dgvParadas);
            this.pnlParadas.Controls.Add(this.pnlAgregarParada);
            this.pnlParadas.Controls.Add(this.lblParadasTitulo);

            // Horarios
            this.pnlHorarios.Dock = DockStyle.Top;
            this.pnlHorarios.Height = 220;
            this.pnlHorarios.BackColor = AppTheme.Blanco;
            this.pnlHorarios.Padding = new Padding(12);

            this.lblHorariosTitulo.Text = "Horarios";
            this.lblHorariosTitulo.Dock = DockStyle.Top;
            this.lblHorariosTitulo.Height = 24;
            this.lblHorariosTitulo.Font = AppTheme.FuenteSubtitulo;
            this.lblHorariosTitulo.ForeColor = AppTheme.AzulOscuro;

            AppTheme.ComoGrillaEstandar(this._dgvHorarios);
            this._dgvHorarios.Dock = DockStyle.Fill;
            this._dgvHorarios.CellFormatting += new DataGridViewCellFormattingEventHandler(this.DgvHorarios_CellFormatting);
            this._dgvHorarios.CellDoubleClick += new DataGridViewCellEventHandler(this.DgvHorarios_CellDoubleClick);

            this.pnlAgregarHorario.Dock = DockStyle.Bottom;
            this.pnlAgregarHorario.Height = 50;
            this.pnlAgregarHorario.ColumnCount = 4;
            this.pnlAgregarHorario.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 32F));
            this.pnlAgregarHorario.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 32F));
            this.pnlAgregarHorario.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 90F));
            this.pnlAgregarHorario.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 90F));

            this._dtpSalida.Dock = DockStyle.Fill;
            this._dtpSalida.Margin = new Padding(0, 22, 4, 0);
            this._dtpSalida.Format = DateTimePickerFormat.Custom;
            this._dtpSalida.CustomFormat = "HH:mm";
            this._dtpSalida.ShowUpDown = true;

            this._dtpLlegada.Dock = DockStyle.Fill;
            this._dtpLlegada.Margin = new Padding(4, 22, 4, 0);
            this._dtpLlegada.Format = DateTimePickerFormat.Custom;
            this._dtpLlegada.CustomFormat = "HH:mm";
            this._dtpLlegada.ShowUpDown = true;

            this.btnAgregarHorario.Text = "Agregar";
            this.btnAgregarHorario.Dock = DockStyle.Fill;
            this.btnAgregarHorario.Margin = new Padding(4, 22, 4, 0);
            AppTheme.ComoBotonPrimario(this.btnAgregarHorario);
            this.btnAgregarHorario.Click += new System.EventHandler(this.BtnAgregarHorario_Click);

            this.btnEliminarHorario.Text = "Eliminar";
            this.btnEliminarHorario.Dock = DockStyle.Fill;
            this.btnEliminarHorario.Margin = new Padding(4, 22, 0, 0);
            AppTheme.ComoBotonPeligro(this.btnEliminarHorario);
            this.btnEliminarHorario.Click += new System.EventHandler(this.BtnEliminarHorario_Click);

            this.pnlAgregarHorario.Controls.Add(this._dtpSalida, 0, 0);
            this.pnlAgregarHorario.Controls.Add(this._dtpLlegada, 1, 0);
            this.pnlAgregarHorario.Controls.Add(this.btnAgregarHorario, 2, 0);
            this.pnlAgregarHorario.Controls.Add(this.btnEliminarHorario, 3, 0);

            this.pnlHorarios.Controls.Add(this._dgvHorarios);
            this.pnlHorarios.Controls.Add(this.pnlAgregarHorario);
            this.pnlHorarios.Controls.Add(this.lblHorariosTitulo);

            this._lblMensajeDetalle.Dock = DockStyle.Top;
            this._lblMensajeDetalle.Height = 24;
            this._lblMensajeDetalle.Font = AppTheme.FuenteBaseNegrita;
            this._lblMensajeDetalle.Margin = new Padding(0, 0, 0, 8);

            this.pnlGestionRuta.Controls.Add(this.pnlHorarios);
            this.pnlGestionRuta.Controls.Add(this.pnlParadas);
            this.pnlGestionRuta.Controls.Add(this._lblMensajeDetalle);

            // --- Crear ruta ---
            this.pnlCrear.Dock = DockStyle.Fill;
            this.pnlCrear.Visible = false;
            this.pnlCrear.BackColor = AppTheme.Blanco;
            this.pnlCrear.Padding = new Padding(14);

            this.lblCrearTitulo.Text = "Nueva ruta";
            this.lblCrearTitulo.Dock = DockStyle.Top;
            this.lblCrearTitulo.Height = 30;
            this.lblCrearTitulo.Font = AppTheme.FuenteSubtitulo;
            this.lblCrearTitulo.ForeColor = AppTheme.AzulOscuro;

            var lblCNombre = new Label { Text = "Nombre", Dock = DockStyle.Top, Height = 18, Font = AppTheme.FuenteBase, ForeColor = AppTheme.GrisTexto };
            this._txtCNombre.Dock = DockStyle.Top;
            this._txtCNombre.Font = AppTheme.FuenteBase;
            this._txtCNombre.Margin = new Padding(0, 0, 0, 8);

            var lblCDescripcion = new Label { Text = "Descripción", Dock = DockStyle.Top, Height = 18, Font = AppTheme.FuenteBase, ForeColor = AppTheme.GrisTexto };
            this._txtCDescripcion.Dock = DockStyle.Top;
            this._txtCDescripcion.Font = AppTheme.FuenteBase;
            this._txtCDescripcion.Margin = new Padding(0, 0, 0, 8);

            this._chkCActiva.Text = "Ruta activa";
            this._chkCActiva.Dock = DockStyle.Top;
            this._chkCActiva.Height = 26;
            this._chkCActiva.Checked = true;
            this._chkCActiva.Font = AppTheme.FuenteBase;
            this._chkCActiva.Margin = new Padding(0, 0, 0, 12);

            var pnlBotonesCrear = new TableLayoutPanel { Dock = DockStyle.Top, Height = 40, ColumnCount = 2 };
            pnlBotonesCrear.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            pnlBotonesCrear.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));

            this.btnGuardarNueva.Text = "Crear ruta";
            this.btnGuardarNueva.Dock = DockStyle.Fill;
            this.btnGuardarNueva.Margin = new Padding(0, 0, 3, 0);
            AppTheme.ComoBotonPrimario(this.btnGuardarNueva);
            this.btnGuardarNueva.Click += new System.EventHandler(this.BtnGuardarNueva_Click);

            this.btnCancelarCrear.Text = "Cancelar";
            this.btnCancelarCrear.Dock = DockStyle.Fill;
            this.btnCancelarCrear.Margin = new Padding(3, 0, 0, 0);
            AppTheme.ComoBotonSecundario(this.btnCancelarCrear);
            this.btnCancelarCrear.Click += new System.EventHandler(this.BtnCancelarCrear_Click);

            pnlBotonesCrear.Controls.Add(this.btnGuardarNueva, 0, 0);
            pnlBotonesCrear.Controls.Add(this.btnCancelarCrear, 1, 0);

            this._lblMensajeCrear.Dock = DockStyle.Top;
            this._lblMensajeCrear.Height = 30;
            this._lblMensajeCrear.Font = AppTheme.FuenteBaseNegrita;
            this._lblMensajeCrear.Margin = new Padding(0, 8, 0, 0);

            this.pnlCrear.Controls.Add(this._lblMensajeCrear);
            this.pnlCrear.Controls.Add(pnlBotonesCrear);
            this.pnlCrear.Controls.Add(this._chkCActiva);
            this.pnlCrear.Controls.Add(this._txtCDescripcion);
            this.pnlCrear.Controls.Add(lblCDescripcion);
            this.pnlCrear.Controls.Add(this._txtCNombre);
            this.pnlCrear.Controls.Add(lblCNombre);
            this.pnlCrear.Controls.Add(this.lblCrearTitulo);

            this.pnlDerecha.Controls.Add(this.pnlCrear);
            this.pnlDerecha.Controls.Add(this.pnlGestionRuta);
            this.pnlDerecha.Controls.Add(this.lblPlaceholder);

            this.tblRaiz.Controls.Add(this.pnlIzquierda, 0, 0);
            this.tblRaiz.Controls.Add(this.pnlDerecha, 1, 0);

            this.Controls.Add(this.tblRaiz);
            this.Controls.Add(this.pnlHeader);

            ((System.ComponentModel.ISupportInitialize)(this._dgvRutas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._dgvParadas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._dgvHorarios)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._numParadaOrden)).EndInit();
            this.ResumeLayout(false);
        }

        private Panel pnlHeader;
        private Label lblTitulo;
        private Button btnNuevo;
        private TableLayoutPanel tblRaiz;

        private Panel pnlIzquierda;
        private DataGridView _dgvRutas;
        private Label lblFichaTitulo;
        private TextBox _txtNombre;
        private TextBox _txtDescripcion;
        private CheckBox _chkActiva;
        private Button btnGuardarRuta;
        private Button btnEliminarRuta;
        private Label _lblMensajeRuta;

        private Panel pnlDerecha;
        private Label lblPlaceholder;

        private Panel pnlGestionRuta;
        private Panel pnlParadas;
        private Label lblParadasTitulo;
        private DataGridView _dgvParadas;
        private TableLayoutPanel pnlAgregarParada;
        private NumericUpDown _numParadaOrden;
        private TextBox _txtParadaNombre;
        private TextBox _txtParadaReferencia;
        private Button btnAgregarParada;
        private Button btnEliminarParada;

        private Panel pnlHorarios;
        private Label lblHorariosTitulo;
        private DataGridView _dgvHorarios;
        private TableLayoutPanel pnlAgregarHorario;
        private DateTimePicker _dtpSalida;
        private DateTimePicker _dtpLlegada;
        private Button btnAgregarHorario;
        private Button btnEliminarHorario;
        private Label _lblMensajeDetalle;

        private Panel pnlCrear;
        private Label lblCrearTitulo;
        private TextBox _txtCNombre;
        private TextBox _txtCDescripcion;
        private CheckBox _chkCActiva;
        private Button btnGuardarNueva;
        private Button btnCancelarCrear;
        private Label _lblMensajeCrear;
    }
}