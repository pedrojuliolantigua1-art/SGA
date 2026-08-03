using SGA.Desktop.UI;

namespace SGA.Desktop.Forms
{
    partial class ViajesForm
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
            this.ClientSize = new System.Drawing.Size(1040, 760);
            this.MinimumSize = new System.Drawing.Size(920, 640);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Viajes — SGA-ITLA";
            this.ResumeLayout(false);
        }

        private void ConstruirFormulario()
        {
            this.pnlHeader = new Panel();
            this.lblTitulo = new Label();

            this.pnlProgramar = new Panel();
            this.lblProgramarTitulo = new Label();
            this.pnlCamposProgramar = new TableLayoutPanel();
            this._cmbRuta = new ComboBox();
            this._cmbHorario = new ComboBox();
            this._cmbAutobus = new ComboBox();
            this._cmbConductor = new ComboBox();
            this._dtpFecha = new DateTimePicker();
            this.btnProgramar = new Button();
            this._lblMensajeProgramar = new Label();

            this._chkSemana = new CheckBox();
            this.pnlDiasSemana = new FlowLayoutPanel();
            this._chkLun = new CheckBox();
            this._chkMar = new CheckBox();
            this._chkMie = new CheckBox();
            this._chkJue = new CheckBox();
            this._chkVie = new CheckBox();
            this._chkSab = new CheckBox();
            this._chkDom = new CheckBox();

            this.pnlCuerpo = new TableLayoutPanel();

            this.pnlDia = new Panel();
            this.lblDiaTitulo = new Label();
            this._dtpFiltroFecha = new DateTimePicker();
            this.btnBuscarDia = new Button();
            this._dgvDia = new DataGridView();

            this.pnlActivos = new Panel();
            this.lblActivosTitulo = new Label();
            this.btnVerActivos = new Button();
            this._dgvActivos = new DataGridView();

            this.pnlCancelar = new Panel();
            this.btnCancelar = new Button();
            this._lblMensajeListado = new Label();

            ((System.ComponentModel.ISupportInitialize)(this._dgvDia)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._dgvActivos)).BeginInit();
            this.SuspendLayout();

            // === Encabezado ===
            this.pnlHeader.Dock = DockStyle.Top;
            this.pnlHeader.Height = 55;
            this.pnlHeader.BackColor = AppTheme.AzulOscuro;
            this.lblTitulo.Text = "Operación — Viajes";
            this.lblTitulo.ForeColor = AppTheme.Blanco;
            this.lblTitulo.Font = AppTheme.FuenteTitulo;
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Location = new System.Drawing.Point(20, 15);
            this.pnlHeader.Controls.Add(this.lblTitulo);

            // === Programar viaje ===
            this.pnlProgramar.Dock = DockStyle.Top;
            this.pnlProgramar.Height = 196;
            this.pnlProgramar.Margin = new Padding(16);
            this.pnlProgramar.BackColor = AppTheme.Blanco;
            this.pnlProgramar.Padding = new Padding(14);

            var pnlProgramarFondo = new Panel { Dock = DockStyle.Top, Height = 196, Padding = new Padding(16, 12, 16, 4), BackColor = AppTheme.GrisClaro };
            pnlProgramarFondo.Controls.Add(this.pnlProgramar);
            this.pnlProgramar.Dock = DockStyle.Fill;

            this.lblProgramarTitulo.Text = "Programar viaje";
            this.lblProgramarTitulo.Font = AppTheme.FuenteSubtitulo;
            this.lblProgramarTitulo.ForeColor = AppTheme.AzulOscuro;
            this.lblProgramarTitulo.Dock = DockStyle.Top;
            this.lblProgramarTitulo.Height = 26;

            this.pnlCamposProgramar.Dock = DockStyle.Top;
            this.pnlCamposProgramar.Height = 66;
            this.pnlCamposProgramar.ColumnCount = 6;
            this.pnlCamposProgramar.RowCount = 1;
            this.pnlCamposProgramar.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 19F));
            this.pnlCamposProgramar.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 17F));
            this.pnlCamposProgramar.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 19F));
            this.pnlCamposProgramar.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 19F));
            this.pnlCamposProgramar.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 14F));
            this.pnlCamposProgramar.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));

            this._cmbRuta = AgregarComboEnCelda(this.pnlCamposProgramar, 0, "Ruta");
            this._cmbRuta.SelectedIndexChanged += new System.EventHandler(this.CmbRuta_SelectedIndexChanged);

            this._cmbHorario = AgregarComboEnCelda(this.pnlCamposProgramar, 1, "Horario");

            // Autobús: combo editable con autocompletado (se puede escribir la placa o elegir de la lista).
            this._cmbAutobus = AgregarComboEnCelda(this.pnlCamposProgramar, 2, "Autobús");
            this._cmbAutobus.DropDownStyle = ComboBoxStyle.DropDown;
            this._cmbAutobus.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            this._cmbAutobus.AutoCompleteSource = AutoCompleteSource.ListItems;

            this._cmbConductor = AgregarComboEnCelda(this.pnlCamposProgramar, 3, "Conductor");

            this._dtpFecha = AgregarFechaEnCelda(this.pnlCamposProgramar, 4, "Fecha");

            var contenedorBoton = new Panel { Dock = DockStyle.Fill, Margin = new Padding(6, 8, 0, 8) };
            var espaciador = new Label { Dock = DockStyle.Top, Height = 20 };
            this.btnProgramar.Text = "Programar";
            this.btnProgramar.Dock = DockStyle.None;
            this.btnProgramar.Size = new Size(90, 32);
            this.btnProgramar.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            this.btnProgramar.Location = new Point(0, 20);
            AppTheme.ComoBotonPrimario(this.btnProgramar);
            this.btnProgramar.Click += new System.EventHandler(this.BtnProgramar_Click);
            contenedorBoton.Controls.Add(this.btnProgramar);
            contenedorBoton.Controls.Add(espaciador);
            this.pnlCamposProgramar.Controls.Add(contenedorBoton, 5, 0);

            this._lblMensajeProgramar.Dock = DockStyle.Top;
            this._lblMensajeProgramar.Height = 24;
            this._lblMensajeProgramar.Font = AppTheme.FuenteBaseNegrita;
            this._lblMensajeProgramar.Margin = new Padding(0, 4, 0, 0);

            // --- Fila: programar para toda la semana en vez de un solo día ---
            this._chkSemana.Text = "Programar toda la semana (mismo autobús/conductor cada día marcado)";
            this._chkSemana.Dock = DockStyle.Top;
            this._chkSemana.Height = 26;
            this._chkSemana.Font = AppTheme.FuenteBase;
            this._chkSemana.Margin = new Padding(0, 4, 0, 0);
            this._chkSemana.CheckedChanged += new System.EventHandler(this.ChkSemana_CheckedChanged);

            this.pnlDiasSemana.Dock = DockStyle.Top;
            this.pnlDiasSemana.Height = 30;
            this.pnlDiasSemana.WrapContents = false;
            this.pnlDiasSemana.Visible = false;

            void ConfigurarDia(CheckBox chk, string texto)
            {
                chk.Text = texto;
                chk.AutoSize = true;
                chk.Font = AppTheme.FuenteBase;
                chk.Margin = new Padding(0, 4, 14, 0);
            }
            ConfigurarDia(this._chkLun, "Lun");
            ConfigurarDia(this._chkMar, "Mar");
            ConfigurarDia(this._chkMie, "Mié");
            ConfigurarDia(this._chkJue, "Jue");
            ConfigurarDia(this._chkVie, "Vie");
            ConfigurarDia(this._chkSab, "Sáb");
            ConfigurarDia(this._chkDom, "Dom");
            this._chkLun.Checked = this._chkMar.Checked = this._chkMie.Checked =
                this._chkJue.Checked = this._chkVie.Checked = true;

            this.pnlDiasSemana.Controls.Add(this._chkLun);
            this.pnlDiasSemana.Controls.Add(this._chkMar);
            this.pnlDiasSemana.Controls.Add(this._chkMie);
            this.pnlDiasSemana.Controls.Add(this._chkJue);
            this.pnlDiasSemana.Controls.Add(this._chkVie);
            this.pnlDiasSemana.Controls.Add(this._chkSab);
            this.pnlDiasSemana.Controls.Add(this._chkDom);

            this.pnlProgramar.Controls.Add(this._lblMensajeProgramar);
            this.pnlProgramar.Controls.Add(this.pnlDiasSemana);
            this.pnlProgramar.Controls.Add(this._chkSemana);
            this.pnlProgramar.Controls.Add(this.pnlCamposProgramar);
            this.pnlProgramar.Controls.Add(this.lblProgramarTitulo);

            // === Cuerpo: dos grids lado a lado ===
            this.pnlCuerpo.Dock = DockStyle.Fill;
            this.pnlCuerpo.BackColor = AppTheme.GrisClaro;
            this.pnlCuerpo.ColumnCount = 2;
            this.pnlCuerpo.RowCount = 1;
            this.pnlCuerpo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            this.pnlCuerpo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));

            // --- Viajes del día ---
            this.pnlDia.Dock = DockStyle.Fill;
            this.pnlDia.Margin = new Padding(16, 0, 8, 0);
            this.pnlDia.BackColor = AppTheme.Blanco;
            this.pnlDia.Padding = new Padding(12);

            var pnlHeaderDia = new TableLayoutPanel { Dock = DockStyle.Top, Height = 34, ColumnCount = 3 };
            pnlHeaderDia.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            pnlHeaderDia.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));
            pnlHeaderDia.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80F));

            this.lblDiaTitulo.Text = "Viajes del día";
            this.lblDiaTitulo.Font = AppTheme.FuenteSubtitulo;
            this.lblDiaTitulo.ForeColor = AppTheme.AzulOscuro;
            this.lblDiaTitulo.Dock = DockStyle.Fill;
            this.lblDiaTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            this._dtpFiltroFecha.Dock = DockStyle.Fill;
            this._dtpFiltroFecha.Format = DateTimePickerFormat.Short;

            this.btnBuscarDia.Text = "Buscar";
            this.btnBuscarDia.Dock = DockStyle.Fill;
            this.btnBuscarDia.Margin = new Padding(6, 0, 0, 0);
            AppTheme.ComoBotonSecundario(this.btnBuscarDia);
            this.btnBuscarDia.Click += new System.EventHandler(this.BtnBuscarDia_Click);

            pnlHeaderDia.Controls.Add(this.lblDiaTitulo, 0, 0);
            pnlHeaderDia.Controls.Add(this._dtpFiltroFecha, 1, 0);
            pnlHeaderDia.Controls.Add(this.btnBuscarDia, 2, 0);

            AppTheme.ComoGrillaEstandar(this._dgvDia);
            this._dgvDia.Dock = DockStyle.Fill;

            this.pnlDia.Controls.Add(this._dgvDia);
            this.pnlDia.Controls.Add(pnlHeaderDia);

            // --- Viajes activos ---
            this.pnlActivos.Dock = DockStyle.Fill;
            this.pnlActivos.Margin = new Padding(8, 0, 16, 0);
            this.pnlActivos.BackColor = AppTheme.Blanco;
            this.pnlActivos.Padding = new Padding(12);

            var pnlHeaderActivos = new TableLayoutPanel { Dock = DockStyle.Top, Height = 34, ColumnCount = 2 };
            pnlHeaderActivos.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            pnlHeaderActivos.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 90F));

            this.lblActivosTitulo.Text = "Viajes activos";
            this.lblActivosTitulo.Font = AppTheme.FuenteSubtitulo;
            this.lblActivosTitulo.ForeColor = AppTheme.AzulOscuro;
            this.lblActivosTitulo.Dock = DockStyle.Fill;
            this.lblActivosTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            this.btnVerActivos.Text = "Refrescar";
            this.btnVerActivos.Dock = DockStyle.Fill;
            AppTheme.ComoBotonSecundario(this.btnVerActivos);
            this.btnVerActivos.Click += new System.EventHandler(this.BtnVerActivos_Click);

            pnlHeaderActivos.Controls.Add(this.lblActivosTitulo, 0, 0);
            pnlHeaderActivos.Controls.Add(this.btnVerActivos, 1, 0);

            AppTheme.ComoGrillaEstandar(this._dgvActivos);
            this._dgvActivos.Dock = DockStyle.Fill;

            this.pnlActivos.Controls.Add(this._dgvActivos);
            this.pnlActivos.Controls.Add(pnlHeaderActivos);

            this.pnlCuerpo.Controls.Add(this.pnlDia, 0, 0);
            this.pnlCuerpo.Controls.Add(this.pnlActivos, 1, 0);

            // === Barra de cancelación ===
            this.pnlCancelar.Dock = DockStyle.Bottom;
            this.pnlCancelar.Height = 66;
            this.pnlCancelar.Margin = new Padding(16);
            this.pnlCancelar.BackColor = AppTheme.Blanco;
            this.pnlCancelar.Padding = new Padding(12);

            var pnlCancelarFondo = new Panel { Dock = DockStyle.Bottom, Height = 100, Padding = new Padding(16, 8, 16, 12), BackColor = AppTheme.GrisClaro };
            this.pnlCancelar.Dock = DockStyle.Fill;
            pnlCancelarFondo.Controls.Add(this.pnlCancelar);

            this.btnCancelar.Text = "Cancelar viaje seleccionado";
            this.btnCancelar.Dock = DockStyle.Left;
            this.btnCancelar.Width = 220;
            AppTheme.ComoBotonPeligro(this.btnCancelar);
            this.btnCancelar.Click += new System.EventHandler(this.BtnCancelar_Click);

            this._lblMensajeListado.Dock = DockStyle.Fill;
            this._lblMensajeListado.Font = AppTheme.FuenteBaseNegrita;
            this._lblMensajeListado.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._lblMensajeListado.Padding = new Padding(12, 0, 0, 0);

            this.pnlCancelar.Controls.Add(this._lblMensajeListado);
            this.pnlCancelar.Controls.Add(this.btnCancelar);

            this.Controls.Add(this.pnlCuerpo);
            this.Controls.Add(pnlCancelarFondo);
            this.Controls.Add(pnlProgramarFondo);
            this.Controls.Add(this.pnlHeader);

            ((System.ComponentModel.ISupportInitialize)(this._dgvDia)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._dgvActivos)).EndInit();
            this.ResumeLayout(false);
        }

        private static ComboBox AgregarComboEnCelda(TableLayoutPanel tabla, int columna, string etiqueta)
        {
            var contenedor = new Panel { Dock = DockStyle.Fill, Margin = new Padding(6, 8, 6, 8) };
            var lbl = new Label { Text = etiqueta, Dock = DockStyle.Top, AutoSize = false, Height = 26, Font = AppTheme.FuenteBase, ForeColor = AppTheme.GrisTexto, TextAlign = ContentAlignment.BottomLeft }; var cmb = new ComboBox { Dock = DockStyle.Top, Font = AppTheme.FuenteBase, DropDownStyle = ComboBoxStyle.DropDownList };
            contenedor.Controls.Add(cmb);
            contenedor.Controls.Add(lbl);
            tabla.Controls.Add(contenedor, columna, 0);
            return cmb;
        }

        private static DateTimePicker AgregarFechaEnCelda(TableLayoutPanel tabla, int columna, string etiqueta)
        {
            var contenedor = new Panel { Dock = DockStyle.Fill, Margin = new Padding(6, 8, 6, 8) };
            var lbl = new Label { Text = etiqueta, Dock = DockStyle.Top, AutoSize = false, Height = 26, Font = AppTheme.FuenteBase, ForeColor = AppTheme.GrisTexto, TextAlign = ContentAlignment.BottomLeft }; var dtp = new DateTimePicker { Dock = DockStyle.Top, Font = AppTheme.FuenteBase, Format = DateTimePickerFormat.Short };
            contenedor.Controls.Add(dtp);
            contenedor.Controls.Add(lbl);
            tabla.Controls.Add(contenedor, columna, 0);
            return dtp;
        }

        private Panel pnlHeader;
        private Label lblTitulo;

        private Panel pnlProgramar;
        private Label lblProgramarTitulo;
        private TableLayoutPanel pnlCamposProgramar;
        private ComboBox _cmbRuta, _cmbHorario, _cmbAutobus, _cmbConductor;
        private DateTimePicker _dtpFecha;
        private Button btnProgramar;
        private Label _lblMensajeProgramar;

        private CheckBox _chkSemana;
        private FlowLayoutPanel pnlDiasSemana;
        private CheckBox _chkLun, _chkMar, _chkMie, _chkJue, _chkVie, _chkSab, _chkDom;

        private TableLayoutPanel pnlCuerpo;

        private Panel pnlDia;
        private Label lblDiaTitulo;
        private DateTimePicker _dtpFiltroFecha;
        private Button btnBuscarDia;
        private DataGridView _dgvDia;

        private Panel pnlActivos;
        private Label lblActivosTitulo;
        private Button btnVerActivos;
        private DataGridView _dgvActivos;

        private Panel pnlCancelar;
        private Button btnCancelar;
        private Label _lblMensajeListado;
    }
}