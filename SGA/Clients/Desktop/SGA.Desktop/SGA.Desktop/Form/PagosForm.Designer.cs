using SGA.Desktop.UI;

namespace SGA.Desktop.Forms
{
    partial class PagosForm
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
            this.ClientSize = new System.Drawing.Size(760, 700);
            this.MinimumSize = new System.Drawing.Size(680, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Autorizaciones — SGA-ITLA";
            this.ResumeLayout(false);
        }

        private void ConstruirFormulario()
        {
            this.pnlHeader = new Panel();
            this.lblTitulo = new Label();

            this.pnlBusqueda = new TableLayoutPanel();
            this._txtBuscar = new TextBox();
            this.btnBuscar = new Button();
            this._lblResultado = new Label();

            // --- Billetera (estudiante) ---
            this.pnlBilletera = new Panel();
            this.lblBilleteraTitulo = new Label();
            this._lblSaldoActual = new Label();
            this.pnlCamposBilletera = new TableLayoutPanel();
            this._numMonto = new NumericUpDown();
            this.btnRecargar = new Button();
            this._lblMensajeBilletera = new Label();

            // --- Permiso (empleado) ---
            this.pnlPermiso = new Panel();
            this.lblPermisoTitulo = new Label();
            this.pnlCamposPermiso = new TableLayoutPanel();
            this._txtCondicion = new TextBox();
            this._dtpVencimiento = new DateTimePicker();
            this._chkSinVencimiento = new CheckBox();
            this.btnEmitirPermiso = new Button();
            this._lblMensajePermiso = new Label();

            // --- Historial ---
            this.pnlHistorial = new Panel();
            this.lblHistorialTitulo = new Label();
            this._dgvHistorial = new DataGridView();
            this._lblMensajeHistorial = new Label();

            ((System.ComponentModel.ISupportInitialize)(this._numMonto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._dgvHistorial)).BeginInit();
            this.SuspendLayout();

            // === Encabezado ===
            this.pnlHeader.Dock = DockStyle.Top;
            this.pnlHeader.Height = 55;
            this.pnlHeader.BackColor = AppTheme.AzulOscuro;
            this.lblTitulo.Text = "Autorizaciones — Billetera y permisos";
            this.lblTitulo.ForeColor = AppTheme.Blanco;
            this.lblTitulo.Font = AppTheme.FuenteTitulo;
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Location = new System.Drawing.Point(20, 15);
            this.pnlHeader.Controls.Add(this.lblTitulo);

            // === Búsqueda única ===
            this.pnlBusqueda.Dock = DockStyle.Top;
            this.pnlBusqueda.Height = 62;
            this.pnlBusqueda.BackColor = AppTheme.Blanco;
            this.pnlBusqueda.Padding = new Padding(16,12,16,12);

            this.pnlBusqueda.BackColor = AppTheme.Blanco;
            this.pnlBusqueda.Padding = new Padding(10);
            this.pnlBusqueda.ColumnCount = 3;
            this.pnlBusqueda.RowCount = 1;
            this.pnlBusqueda.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45F));
            this.pnlBusqueda.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));
            this.pnlBusqueda.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 55F));
            this.pnlBusqueda.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            this._txtBuscar.Dock = DockStyle.Fill;
            this._txtBuscar.Font = AppTheme.FuenteBase;
            this._txtBuscar.Margin = new Padding(0, 6, 8, 6);
            this._txtBuscar.PlaceholderText = "Matrícula del estudiante o correo institucional";

            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.Dock = DockStyle.Fill;
            this.btnBuscar.Margin = new Padding(0, 4, 8, 4);
            AppTheme.ComoBotonPrimario(this.btnBuscar);
            this.btnBuscar.Click += new System.EventHandler(this.BtnBuscar_Click);

            this._lblResultado.Dock = DockStyle.Fill;
            this._lblResultado.Font = AppTheme.FuenteBaseNegrita;
            this._lblResultado.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._lblResultado.AutoEllipsis = true;

            this.pnlBusqueda.Controls.Add(this._txtBuscar, 0, 0);
            this.pnlBusqueda.Controls.Add(this.btnBuscar, 1, 0);
            this.pnlBusqueda.Controls.Add(this._lblResultado, 2, 0);

            

            // === Billetera (estudiante) ===
            this.pnlBilletera.Dock = DockStyle.Top;
            this.pnlBilletera.Height = 240;
            this.pnlBilletera.Margin = new Padding(16, 10, 16, 10);
            this.pnlBilletera.BackColor = AppTheme.Blanco;
            this.pnlBilletera.Padding = new Padding(14);
            this.pnlBilletera.Visible = false;

            this.lblBilleteraTitulo.Text = "Billetera del estudiante";
            this.lblBilleteraTitulo.Font = AppTheme.FuenteSubtitulo;
            this.lblBilleteraTitulo.ForeColor = AppTheme.AzulOscuro;
            this.lblBilleteraTitulo.Dock = DockStyle.Top;
            this.lblBilleteraTitulo.Height = 28;

            this._lblSaldoActual.Dock = DockStyle.Top;
            this._lblSaldoActual.Height = 28;
            this._lblSaldoActual.Font = AppTheme.FuenteBase;
            this._lblSaldoActual.ForeColor = AppTheme.GrisTexto;

            this.pnlCamposBilletera.Dock = DockStyle.Top;
            this.pnlCamposBilletera.Height = 70;
            this.pnlCamposBilletera.ColumnCount = 1;
            this.pnlCamposBilletera.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            this._numMonto = AgregarNumeroDecimal(this.pnlCamposBilletera, 0, 0, "Monto recibido en efectivo");

            this.btnRecargar.Text = "Recargar RD$ 0.00";
            this.btnRecargar.Dock = DockStyle.Top;
            this.btnRecargar.Height = 36;
            this.btnRecargar.Margin = new Padding(0, 8, 0, 0);
            AppTheme.ComoBotonPrimario(this.btnRecargar);
            this.btnRecargar.Click += new System.EventHandler(this.BtnRecargar_Click);

            this._lblMensajeBilletera.Dock = DockStyle.Top;
            this._lblMensajeBilletera.Height = 30;
            this._lblMensajeBilletera.Font = AppTheme.FuenteBaseNegrita;
            this._lblMensajeBilletera.Margin = new Padding(0, 6, 0, 0);

            this.pnlBilletera.Controls.Add(this._lblMensajeBilletera);
            this.pnlBilletera.Controls.Add(this.btnRecargar);
            this.pnlBilletera.Controls.Add(this.pnlCamposBilletera);
            this.pnlBilletera.Controls.Add(this._lblSaldoActual);
            this.pnlBilletera.Controls.Add(this.lblBilleteraTitulo);

            // === Permiso especial (empleado) ===
            this.pnlPermiso.Dock = DockStyle.Top;
            this.pnlPermiso.Height = 260;
            this.pnlPermiso.Margin = new Padding(16, 10, 16, 10);
            this.pnlPermiso.BackColor = AppTheme.Blanco;
            this.pnlPermiso.Padding = new Padding(14);
            this.pnlPermiso.Visible = false;

            this.lblPermisoTitulo.Text = "Permiso de transporte (sin costo)";
            this.lblPermisoTitulo.Font = AppTheme.FuenteSubtitulo;
            this.lblPermisoTitulo.ForeColor = AppTheme.AzulOscuro;
            this.lblPermisoTitulo.Dock = DockStyle.Top;
            this.lblPermisoTitulo.Height = 28;

            this.pnlCamposPermiso.Dock = DockStyle.Top;
            this.pnlCamposPermiso.Height = 130;
            this.pnlCamposPermiso.ColumnCount = 2;
            this.pnlCamposPermiso.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            this.pnlCamposPermiso.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            this._txtCondicion = CampoBuilder.CampoEnTabla(this.pnlCamposPermiso, 0, 0, "Condición institucional", colSpan: 2);
            this._dtpVencimiento = AgregarFecha(this.pnlCamposPermiso, 1, 0, "Vigente hasta");

            this._chkSinVencimiento.Text = "Sin fecha de vencimiento";
            this._chkSinVencimiento.Dock = DockStyle.Fill;
            this._chkSinVencimiento.Margin = new Padding(6, 30, 6, 8);
            this._chkSinVencimiento.Font = AppTheme.FuenteBase;
            this.pnlCamposPermiso.Controls.Add(this._chkSinVencimiento, 1, 1);
            this.pnlCamposPermiso.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            this.pnlCamposPermiso.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));

            this.btnEmitirPermiso.Text = "Emitir permiso";
            this.btnEmitirPermiso.Dock = DockStyle.Top;
            this.btnEmitirPermiso.Height = 36;
            this.btnEmitirPermiso.Margin = new Padding(0, 8, 0, 0);
            AppTheme.ComoBotonPrimario(this.btnEmitirPermiso);
            this.btnEmitirPermiso.Click += new System.EventHandler(this.BtnEmitirPermiso_Click);

            this._lblMensajePermiso.Dock = DockStyle.Top;
            this._lblMensajePermiso.Height = 30;
            this._lblMensajePermiso.Font = AppTheme.FuenteBaseNegrita;
            this._lblMensajePermiso.Margin = new Padding(0, 6, 0, 0);

            this.pnlPermiso.Controls.Add(this._lblMensajePermiso);
            this.pnlPermiso.Controls.Add(this.btnEmitirPermiso);
            this.pnlPermiso.Controls.Add(this.pnlCamposPermiso);
            this.pnlPermiso.Controls.Add(this.lblPermisoTitulo);

            // === Historial ===
            this.pnlHistorial.Dock = DockStyle.Fill;
            this.pnlHistorial.Margin = new Padding(16, 0, 16, 16);
            this.pnlHistorial.BackColor = AppTheme.Blanco;
            this.pnlHistorial.Padding = new Padding(14);

            this.lblHistorialTitulo.Text = "Historial de pagos";
            this.lblHistorialTitulo.Font = AppTheme.FuenteSubtitulo;
            this.lblHistorialTitulo.ForeColor = AppTheme.AzulOscuro;
            this.lblHistorialTitulo.Dock = DockStyle.Top;
            this.lblHistorialTitulo.Height = 28;

            AppTheme.ComoGrillaEstandar(this._dgvHistorial);
            this._dgvHistorial.Dock = DockStyle.Fill;

            this._lblMensajeHistorial.Dock = DockStyle.Bottom;
            this._lblMensajeHistorial.Height = 24;
            this._lblMensajeHistorial.Font = AppTheme.FuenteBaseNegrita;

            this.pnlHistorial.Controls.Add(this._dgvHistorial);
            this.pnlHistorial.Controls.Add(this._lblMensajeHistorial);
            this.pnlHistorial.Controls.Add(this.lblHistorialTitulo);

            this.Controls.Add(this.pnlHistorial);
            this.Controls.Add(this.pnlPermiso);
            this.Controls.Add(this.pnlBilletera);
            this.Controls.Add(this.pnlBusqueda);
            this.Controls.Add(this.pnlHeader);

            ((System.ComponentModel.ISupportInitialize)(this._numMonto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._dgvHistorial)).EndInit();
            this.ResumeLayout(false);
        }

        private static NumericUpDown AgregarNumeroDecimal(TableLayoutPanel tabla, int fila, int columna, string etiqueta)
        {
            var contenedor = new Panel { Dock = DockStyle.Fill, Margin = new Padding(6, 8, 6, 8) };
            var lbl = new Label { Text = etiqueta, Dock = DockStyle.Top, AutoSize = false, Height = 26, Font = AppTheme.FuenteBase, ForeColor = AppTheme.GrisTexto, TextAlign = ContentAlignment.BottomLeft };
            var num = new NumericUpDown { Dock = DockStyle.Top, Font = AppTheme.FuenteBase, DecimalPlaces = 2, Maximum = 999999 };
            contenedor.Controls.Add(num);
            contenedor.Controls.Add(lbl);
            tabla.Controls.Add(contenedor, columna, fila);
            return num;
        }

        private static DateTimePicker AgregarFecha(TableLayoutPanel tabla, int fila, int columna, string etiqueta)
        {
            var contenedor = new Panel { Dock = DockStyle.Fill, Margin = new Padding(6, 8, 6, 8) };
            var lbl = new Label { Text = etiqueta, Dock = DockStyle.Top, AutoSize = false, Height = 26, Font = AppTheme.FuenteBase, ForeColor = AppTheme.GrisTexto, TextAlign = ContentAlignment.BottomLeft };
            var dtp = new DateTimePicker { Dock = DockStyle.Top, Font = AppTheme.FuenteBase, Format = DateTimePickerFormat.Short };
            contenedor.Controls.Add(dtp);
            contenedor.Controls.Add(lbl);
            tabla.Controls.Add(contenedor, columna, fila);
            return dtp;
        }

        private Panel pnlHeader;
        private Label lblTitulo;

        private TableLayoutPanel pnlBusqueda;
        private TextBox _txtBuscar;
        private Button btnBuscar;
        private Label _lblResultado;

        private Panel pnlBilletera;
        private Label lblBilleteraTitulo;
        private Label _lblSaldoActual;
        private TableLayoutPanel pnlCamposBilletera;
        private NumericUpDown _numMonto;
        private Button btnRecargar;
        private Label _lblMensajeBilletera;

        private Panel pnlPermiso;
        private Label lblPermisoTitulo;
        private TableLayoutPanel pnlCamposPermiso;
        private TextBox _txtCondicion;
        private DateTimePicker _dtpVencimiento;
        private CheckBox _chkSinVencimiento;
        private Button btnEmitirPermiso;
        private Label _lblMensajePermiso;

        private Panel pnlHistorial;
        private Label lblHistorialTitulo;
        private DataGridView _dgvHistorial;
        private Label _lblMensajeHistorial;
    }
}