using SGA.Desktop.UI;

namespace SGA.Desktop.Forms
{
    partial class AccesosForm
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
            this.Text = "Registro de accesos — SGA-ITLA";
            this.ResumeLayout(false);
        }

        private void ConstruirFormulario()
        {
            this.pnlHeader = new Panel();
            this.lblTitulo = new Label();

            this.pnlFiltros = new TableLayoutPanel();
            this._dtpDesde = new DateTimePicker();
            this._dtpHasta = new DateTimePicker();
            this._cmbResultado = new ComboBox();
            this.btnBuscar = new Button();

            this.pnlKpis = new TableLayoutPanel();
            this.lblKpiTotalValor = new Label();
            this.lblKpiPermitidosValor = new Label();
            this.lblKpiRechazadosValor = new Label();

            this.pnlGrid = new Panel();
            this._dgv = new DataGridView();
            this._lblMensaje = new Label();

            ((System.ComponentModel.ISupportInitialize)(this._dgv)).BeginInit();
            this.SuspendLayout();

            // === Encabezado ===
            this.pnlHeader.Dock = DockStyle.Top;
            this.pnlHeader.Height = 55;
            this.pnlHeader.BackColor = AppTheme.AzulOscuro;
            this.lblTitulo.Text = "Operación — Registro de accesos";
            this.lblTitulo.ForeColor = AppTheme.Blanco;
            this.lblTitulo.Font = AppTheme.FuenteTitulo;
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Location = new System.Drawing.Point(20, 15);
            this.pnlHeader.Controls.Add(this.lblTitulo);

            // === Filtros ===
            var pnlFiltrosFondo = new Panel { Dock = DockStyle.Top, Height = 150, Padding = new Padding(16), BackColor = AppTheme.GrisClaro };
            var pnlFiltrosCard = new Panel { Dock = DockStyle.Fill, BackColor = AppTheme.Blanco, Padding = new Padding(14) };

            this.pnlFiltros.Dock = DockStyle.Fill;
            this.pnlFiltros.ColumnCount = 4;
            this.pnlFiltros.RowCount = 1;
            this.pnlFiltros.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            this.pnlFiltros.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            this.pnlFiltros.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
            this.pnlFiltros.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));

            this._dtpDesde = AgregarFechaEnCelda(this.pnlFiltros, 0, "Desde");
            this._dtpHasta = AgregarFechaEnCelda(this.pnlFiltros, 1, "Hasta");
            this._cmbResultado = AgregarComboEnCelda(this.pnlFiltros, 2, "Resultado", new[] { "Todos", "Permitido", "Rechazado" });
            this._cmbResultado.SelectedIndexChanged += new System.EventHandler(this.CmbResultado_SelectedIndexChanged);

            this.btnBuscar = AgregarBotonEnCelda(this.pnlFiltros, 3, "Buscar");
            this.btnBuscar.Click += new System.EventHandler(this.BtnBuscar_Click);

            pnlFiltrosCard.Controls.Add(this.pnlFiltros);
            pnlFiltrosFondo.Controls.Add(pnlFiltrosCard);

            // === KPIs ===
            var pnlKpisFondo = new Panel { Dock = DockStyle.Top, Height = 84, Padding = new Padding(16, 0, 16, 14), BackColor = AppTheme.GrisClaro };

            this.pnlKpis.Dock = DockStyle.Fill;
            this.pnlKpis.ColumnCount = 3;
            this.pnlKpis.RowCount = 1;
            this.pnlKpis.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3F));
            this.pnlKpis.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3F));
            this.pnlKpis.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3F));

            this.lblKpiTotalValor = CrearTarjetaKpi(this.pnlKpis, 0, "Total de intentos", AppTheme.AzulOscuro);
            this.lblKpiPermitidosValor = CrearTarjetaKpi(this.pnlKpis, 1, "Permitidos", System.Drawing.Color.FromArgb(59, 109, 17));
            this.lblKpiRechazadosValor = CrearTarjetaKpi(this.pnlKpis, 2, "Rechazados", AppTheme.Rojo);

            pnlKpisFondo.Controls.Add(this.pnlKpis);

            // === Grid ===
            var pnlGridFondo = new Panel { Dock = DockStyle.Fill, Padding = new Padding(16, 0, 16, 16), BackColor = AppTheme.GrisClaro };
            this.pnlGrid.Dock = DockStyle.Fill;
            this.pnlGrid.BackColor = AppTheme.Blanco;
            this.pnlGrid.Padding = new Padding(12);
            pnlGridFondo.Controls.Add(this.pnlGrid);

            AppTheme.ComoGrillaEstandar(this._dgv);
            this._dgv.Dock = DockStyle.Fill;
            this._dgv.CellFormatting += new DataGridViewCellFormattingEventHandler(this.Dgv_CellFormatting);

            this._lblMensaje.Dock = DockStyle.Bottom;
            this._lblMensaje.Height = 24;
            this._lblMensaje.Font = AppTheme.FuenteBaseNegrita;
            this._lblMensaje.ForeColor = AppTheme.GrisTexto;

            this.pnlGrid.Controls.Add(this._dgv);
            this.pnlGrid.Controls.Add(this._lblMensaje);

            this.Controls.Add(pnlGridFondo);
            this.Controls.Add(pnlKpisFondo);
            this.Controls.Add(pnlFiltrosFondo);
            this.Controls.Add(this.pnlHeader);

            ((System.ComponentModel.ISupportInitialize)(this._dgv)).EndInit();
            this.ResumeLayout(false);
        }

        private static Label CrearTarjetaKpi(TableLayoutPanel contenedor, int columna, string titulo, System.Drawing.Color colorValor)
        {
            var tarjeta = new Panel { Dock = DockStyle.Fill, Margin = new Padding(columna == 1 ? 6 : 0, 0, columna == 1 ? 6 : 0, 0), BackColor = AppTheme.Blanco, Padding = new Padding(14, 12, 14, 12) };

            var lblTitulo = new Label
            {
                Text = titulo,
                Dock = DockStyle.Top,
                AutoSize = false,
                Height = 20,
                Font = AppTheme.FuenteBase,
                ForeColor = System.Drawing.Color.FromArgb(117, 117, 117),
                TextAlign = ContentAlignment.BottomLeft
            };
            var lblValor = new Label
            {
                Text = "—",
                Dock = DockStyle.Top,
                AutoSize = false,
                Height = 34,
                Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold),
                ForeColor = colorValor,
                TextAlign = ContentAlignment.BottomLeft
            };

            tarjeta.Controls.Add(lblValor);
            tarjeta.Controls.Add(lblTitulo);
            contenedor.Controls.Add(tarjeta, columna, 0);
            return lblValor;
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

        private static ComboBox AgregarComboEnCelda(TableLayoutPanel tabla, int columna, string etiqueta, string[] opciones)
        {
            var contenedor = new Panel { Dock = DockStyle.Fill, Margin = new Padding(6, 8, 6, 8) };
            var lbl = new Label { Text = etiqueta, Dock = DockStyle.Top, AutoSize = false, Height = 26, Font = AppTheme.FuenteBase, ForeColor = AppTheme.GrisTexto, TextAlign = ContentAlignment.BottomLeft };
            var cmb = new ComboBox { Dock = DockStyle.Top, Font = AppTheme.FuenteBase, DropDownStyle = ComboBoxStyle.DropDownList };
            cmb.Items.AddRange(opciones);
            contenedor.Controls.Add(cmb);
            contenedor.Controls.Add(lbl);
            tabla.Controls.Add(contenedor, columna, 0);
            return cmb;
        }

        /// <summary>Botón dentro de una celda del filtro, alineado con la fila de campos:
        /// un espaciador de la misma altura que los labels (26px) para que el botón quede
        /// a la misma altura que los TextBox/Combo/Fecha de al lado, con tamaño fijo
        /// (no Dock=Fill) para que no se estire ni se vea "gigante".</summary>
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
        private ComboBox _cmbResultado;
        private Button btnBuscar;

        private TableLayoutPanel pnlKpis;
        private Label lblKpiTotalValor, lblKpiPermitidosValor, lblKpiRechazadosValor;

        private Panel pnlGrid;
        private DataGridView _dgv;
        private Label _lblMensaje;
    }
}