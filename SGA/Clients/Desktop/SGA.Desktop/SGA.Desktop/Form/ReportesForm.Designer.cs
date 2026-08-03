using SGA.Desktop.UI;

namespace SGA.Desktop.Forms
{
    partial class ReportesForm
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
            this.ClientSize = new System.Drawing.Size(1000, 720);
            this.MinimumSize = new System.Drawing.Size(880, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Incidencias — SGA-ITLA";
            this.ResumeLayout(false);
        }

        private void ConstruirFormulario()
        {
            this.pnlHeader = new Panel();
            this.lblTitulo = new Label();

            this.pnlFiltros = new TableLayoutPanel();
            this._dtpDesde = new DateTimePicker();
            this._dtpHasta = new DateTimePicker();
            this.btnPeriodo = new Button();
            this.btnReportar = new Button();

            this.pnlReportar = new Panel();
            this.lblReportarTitulo = new Label();
            this._cmbViaje = new ComboBox();
            this._txtTipo = new TextBox();
            this._txtDescripcion = new TextBox();
            this._dtpFechaHora = new DateTimePicker();
            this.btnGuardarIncidencia = new Button();
            this.btnCancelarIncidencia = new Button();
            this._lblMensajeReportar = new Label();

            this.pnlGrid = new Panel();
            this._dgv = new DataGridView();
            this._lblMensaje = new Label();

            ((System.ComponentModel.ISupportInitialize)(this._dgv)).BeginInit();
            this.SuspendLayout();

            // === Encabezado ===
            this.pnlHeader.Dock = DockStyle.Top;
            this.pnlHeader.Height = 55;
            this.pnlHeader.BackColor = AppTheme.AzulOscuro;
            this.lblTitulo.Text = "Reportes — Incidencias de viajes";
            this.lblTitulo.ForeColor = AppTheme.Blanco;
            this.lblTitulo.Font = AppTheme.FuenteTitulo;
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Location = new System.Drawing.Point(20, 15);
            this.pnlHeader.Controls.Add(this.lblTitulo);

            // === Filtros (período) ===
            var pnlFiltrosFondo = new Panel { Dock = DockStyle.Top, Height = 160, Padding = new Padding(16), BackColor = AppTheme.GrisClaro };
            var pnlFiltrosCard = new Panel { Dock = DockStyle.Fill, BackColor = AppTheme.Blanco, Padding = new Padding(14) };

            this.pnlFiltros.Dock = DockStyle.Fill;
            this.pnlFiltros.ColumnCount = 4;
            this.pnlFiltros.RowCount = 1;
            this.pnlFiltros.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 28F));
            this.pnlFiltros.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 28F));
            this.pnlFiltros.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 160F));
            this.pnlFiltros.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 180F));

            this._dtpDesde = AgregarFechaEnCelda(this.pnlFiltros, 0, "Desde");
            this._dtpHasta = AgregarFechaEnCelda(this.pnlFiltros, 1, "Hasta");

            this.btnPeriodo = AgregarBotonEnCelda(this.pnlFiltros, 2, "Buscar por período");
            this.btnPeriodo.Click += new System.EventHandler(this.BtnPeriodo_Click);

            this.btnReportar = AgregarBotonEnCelda(this.pnlFiltros, 3, "+ Reportar incidencia");
            this.btnReportar.Click += new System.EventHandler(this.BtnReportar_Click);

            pnlFiltrosCard.Controls.Add(this.pnlFiltros);
            pnlFiltrosFondo.Controls.Add(pnlFiltrosCard);

            // === Panel reportar incidencia (oculto por defecto) ===
            this.pnlReportar.Dock = DockStyle.Top;
            this.pnlReportar.Visible = false;
            this.pnlReportar.Height = 300;
            this.pnlReportar.Margin = new Padding(0);
            this.pnlReportar.Padding = new Padding(16, 0, 16, 12);
            this.pnlReportar.BackColor = AppTheme.GrisClaro;

            var pnlReportarCard = new Panel { Dock = DockStyle.Fill, BackColor = AppTheme.Blanco, Padding = new Padding(14) };

            this.lblReportarTitulo.Text = "Registrar incidencia de un viaje";
            this.lblReportarTitulo.Font = AppTheme.FuenteSubtitulo;
            this.lblReportarTitulo.ForeColor = AppTheme.AzulOscuro;
            this.lblReportarTitulo.Dock = DockStyle.Top;
            this.lblReportarTitulo.Height = 30;

            var tblReportar = new TableLayoutPanel { Dock = DockStyle.Top, Height = 120, ColumnCount = 2, RowCount = 2 };
            tblReportar.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tblReportar.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tblReportar.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tblReportar.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));

            this._cmbViaje = AgregarComboEnTabla(tblReportar, 0, 0, "Viaje (en curso)");

            this._txtTipo = CampoBuilder.CampoEnTabla(tblReportar, 0, 1, "Tipo");
            this._txtTipo.PlaceholderText = "Ej: Retraso, Avería, Queja...";

            this._txtDescripcion = CampoBuilder.CampoEnTabla(tblReportar, 1, 0, "Descripción", colSpan: 1);
            this._txtDescripcion.Multiline = true;
            this._txtDescripcion.Height = 44;

            this._dtpFechaHora = CampoBuilder.CampoFechaEnTabla(tblReportar, 1, 1, "Fecha y hora");
            this._dtpFechaHora.Format = DateTimePickerFormat.Custom;
            this._dtpFechaHora.CustomFormat = "dd/MM/yyyy hh:mm tt";
            this._dtpFechaHora.ShowUpDown = true;

            var pnlBotonesReportar = new TableLayoutPanel { Dock = DockStyle.Top, Height = 40, ColumnCount = 2, Margin = new Padding(0) };
            pnlBotonesReportar.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            pnlBotonesReportar.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));

            this.btnGuardarIncidencia.Text = "Registrar incidencia";
            this.btnGuardarIncidencia.Dock = DockStyle.Fill;
            this.btnGuardarIncidencia.Margin = new Padding(0, 0, 3, 0);
            AppTheme.ComoBotonPrimario(this.btnGuardarIncidencia);
            this.btnGuardarIncidencia.Click += new System.EventHandler(this.BtnGuardarIncidencia_Click);

            this.btnCancelarIncidencia.Text = "Cancelar";
            this.btnCancelarIncidencia.Dock = DockStyle.Fill;
            this.btnCancelarIncidencia.Margin = new Padding(3, 0, 0, 0);
            AppTheme.ComoBotonSecundario(this.btnCancelarIncidencia);
            this.btnCancelarIncidencia.Click += new System.EventHandler(this.BtnCancelarIncidencia_Click);

            pnlBotonesReportar.Controls.Add(this.btnGuardarIncidencia, 0, 0);
            pnlBotonesReportar.Controls.Add(this.btnCancelarIncidencia, 1, 0);

            this._lblMensajeReportar.Dock = DockStyle.Top;
            this._lblMensajeReportar.Height = 20;
            this._lblMensajeReportar.Font = AppTheme.FuenteBaseNegrita;

            pnlReportarCard.Controls.Add(pnlBotonesReportar);
            pnlReportarCard.Controls.Add(tblReportar);
            pnlReportarCard.Controls.Add(this._lblMensajeReportar);
            pnlReportarCard.Controls.Add(this.lblReportarTitulo);
            this.pnlReportar.Controls.Add(pnlReportarCard);

            // === Grid ===
            var pnlGridFondo = new Panel { Dock = DockStyle.Fill, Padding = new Padding(16), BackColor = AppTheme.GrisClaro };
            this.pnlGrid.Dock = DockStyle.Fill;
            this.pnlGrid.BackColor = AppTheme.Blanco;
            this.pnlGrid.Padding = new Padding(12);
            pnlGridFondo.Controls.Add(this.pnlGrid);

            AppTheme.ComoGrillaEstandar(this._dgv);
            this._dgv.Dock = DockStyle.Fill;

            this._lblMensaje.Dock = DockStyle.Bottom;
            this._lblMensaje.Height = 24;
            this._lblMensaje.Font = AppTheme.FuenteBaseNegrita;
            this._lblMensaje.ForeColor = AppTheme.GrisTexto;

            this.pnlGrid.Controls.Add(this._dgv);
            this.pnlGrid.Controls.Add(this._lblMensaje);

            this.Controls.Add(pnlGridFondo);
            this.Controls.Add(this.pnlReportar);
            this.Controls.Add(pnlFiltrosFondo);
            this.Controls.Add(this.pnlHeader);

            ((System.ComponentModel.ISupportInitialize)(this._dgv)).EndInit();
            this.ResumeLayout(false);
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

        private static ComboBox AgregarComboEnTabla(TableLayoutPanel tabla, int fila, int columna, string etiqueta)
        {
            var contenedor = new Panel { Dock = DockStyle.Fill, Margin = new Padding(6, 8, 6, 14) };
            var lbl = new Label { Text = etiqueta, Dock = DockStyle.Top, AutoSize = false, Height = 26, Font = AppTheme.FuenteBase, ForeColor = AppTheme.GrisTexto, TextAlign = ContentAlignment.BottomLeft };
            var cmb = new ComboBox { Dock = DockStyle.Top, Font = AppTheme.FuenteBase, DropDownStyle = ComboBoxStyle.DropDownList };
            contenedor.Controls.Add(cmb);
            contenedor.Controls.Add(lbl);
            tabla.Controls.Add(contenedor, columna, fila);
            return cmb;
        }

        private static Button AgregarBotonEnCelda(TableLayoutPanel tabla, int columna, string texto)
        {
            var contenedor = new Panel { Dock = DockStyle.Fill, Margin = new Padding(6, 8, 0, 8) };
            var espaciador = new Label { Dock = DockStyle.Top, AutoSize = false, Height = 26 };
            var btn = new Button { Text = texto, Dock = DockStyle.Top, Height = 36 };
            AppTheme.ComoBotonPrimario(btn);
            contenedor.Controls.Add(btn);
            contenedor.Controls.Add(espaciador);
            tabla.Controls.Add(contenedor, columna, 0);
            return btn;
        }

        private Panel pnlHeader;
        private Label lblTitulo;

        private TableLayoutPanel pnlFiltros;
        private DateTimePicker _dtpDesde, _dtpHasta;
        private Button btnPeriodo;
        private Button btnReportar;

        private Panel pnlReportar;
        private Label lblReportarTitulo;
        private ComboBox _cmbViaje;
        private TextBox _txtTipo;
        private TextBox _txtDescripcion;
        private DateTimePicker _dtpFechaHora;
        private Button btnGuardarIncidencia;
        private Button btnCancelarIncidencia;
        private Label _lblMensajeReportar;

        private Panel pnlGrid;
        private DataGridView _dgv;
        private Label _lblMensaje;
    }
}
