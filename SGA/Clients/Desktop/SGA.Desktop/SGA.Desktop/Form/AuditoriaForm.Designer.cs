using SGA.Desktop.UI;

namespace SGA.Desktop.Forms
{
    partial class AuditoriaForm
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
            this.Text = "Auditoría — SGA-ITLA";
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

            this.pnlGrid = new Panel();
            this._dgv = new DataGridView();
            this._lblMensaje = new Label();

            ((System.ComponentModel.ISupportInitialize)(this._dgv)).BeginInit();
            this.SuspendLayout();

            // === Encabezado ===
            this.pnlHeader.Dock = DockStyle.Top;
            this.pnlHeader.Height = 55;
            this.pnlHeader.BackColor = AppTheme.AzulOscuro;
            this.lblTitulo.Text = "Auditoría del sistema (solo lectura)";
            this.lblTitulo.ForeColor = AppTheme.Blanco;
            this.lblTitulo.Font = AppTheme.FuenteTitulo;
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Location = new System.Drawing.Point(20, 15);
            this.pnlHeader.Controls.Add(this.lblTitulo);

            // === Filtros (solo período) ===
            var pnlFiltrosFondo = new Panel { Dock = DockStyle.Top, Height = 150, Padding = new Padding(16), BackColor = AppTheme.GrisClaro };
            var pnlFiltrosCard = new Panel { Dock = DockStyle.Fill, BackColor = AppTheme.Blanco, Padding = new Padding(14) };

            this.pnlFiltros.Dock = DockStyle.Fill;
            this.pnlFiltros.ColumnCount = 3;
            this.pnlFiltros.RowCount = 1;
            this.pnlFiltros.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
            this.pnlFiltros.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
            this.pnlFiltros.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 130F));

            this._dtpDesde = AgregarFechaEnCelda(this.pnlFiltros, 0, "Desde");
            this._dtpHasta = AgregarFechaEnCelda(this.pnlFiltros, 1, "Hasta");

            this.btnPeriodo = AgregarBotonEnCelda(this.pnlFiltros, 2, "Buscar por período");
            this.btnPeriodo.Click += new System.EventHandler(this.BtnPeriodo_Click);

            pnlFiltrosCard.Controls.Add(this.pnlFiltros);
            pnlFiltrosFondo.Controls.Add(pnlFiltrosCard);

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

        /// <summary>Botón alineado con la fila de campos (espaciador de 26px = altura del label
        /// de las celdas vecinas, y botón con alto fijo de 36px, no Dock=Fill).</summary>
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

        private Panel pnlGrid;
        private DataGridView _dgv;
        private Label _lblMensaje;
    }
}