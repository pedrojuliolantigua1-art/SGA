using SGA.Desktop.UI;

namespace SGA.Desktop.Forms
{
    partial class CatalogoTransporteForm
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
            this.Text = "Autobuses — SGA-ITLA";
            this.ResumeLayout(false);
        }

        private void ConstruirFormulario()
        {
            this.pnlHeader = new Panel();
            this.lblTitulo = new Label();
            this.btnNuevo = new Button();

            this.tblRaiz = new TableLayoutPanel();
            this.pnlListado = new Panel();
            this.dgv = new DataGridView();

            this.pnlDetalle = new Panel();
            this.lblPlaceholder = new Label();

            this.pnlFicha = new Panel();
            this.picFoto = new PictureBox();
            this.lblFichaPlaca = new Label();
            this.lblFichaSubtitulo = new Label();
            this.btnSubirFoto = new Button();
            this.btnVerDetalle = new Button();
            this.pnlCamposFicha = new TableLayoutPanel();
            this.lblEstado = new Label();
            this.cmbEstado = new ComboBox();
            this.btnGuardarCambios = new Button();
            this.btnCambiarEstado = new Button();
            this.btnEliminar = new Button();
            this.lblMensajeFicha = new Label();

            this.pnlCrear = new Panel();
            this.lblCrearTitulo = new Label();
            this.pnlCamposCrear = new TableLayoutPanel();
            this.btnGuardarNuevo = new Button();
            this.btnCancelarCrear = new Button();
            this.lblMensajeCrear = new Label();

            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picFoto)).BeginInit();
            this.SuspendLayout();

            // === Encabezado ===
            this.pnlHeader.Dock = DockStyle.Top;
            this.pnlHeader.Height = 55;
            this.pnlHeader.BackColor = AppTheme.AzulOscuro;

            this.lblTitulo.Text = "Catálogo de transporte — Autobuses";
            this.lblTitulo.ForeColor = AppTheme.Blanco;
            this.lblTitulo.Font = AppTheme.FuenteTitulo;
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Location = new System.Drawing.Point(20, 15);

            this.btnNuevo.Text = "+ Nuevo autobús";
            this.btnNuevo.Size = new System.Drawing.Size(140, 32);
            this.btnNuevo.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            AppTheme.ComoBotonPrimario(this.btnNuevo);
            this.btnNuevo.Click += new System.EventHandler(this.BtnNuevo_Click);

            this.pnlHeader.Controls.Add(this.btnNuevo);
            this.pnlHeader.Controls.Add(this.lblTitulo);
            this.pnlHeader.Resize += (s, e) =>
                this.btnNuevo.Location = new System.Drawing.Point(this.pnlHeader.Width - 160, 12);

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

            AppTheme.ComoGrillaEstandar(this.dgv);
            this.dgv.Dock = DockStyle.Fill;
            this.dgv.SelectionChanged += new System.EventHandler(this.Dgv_SelectionChanged);

            this.pnlListado.Controls.Add(this.dgv);

            // --- Detalle (contenedor de ficha / crear / placeholder) ---
            this.pnlDetalle.Dock = DockStyle.Fill;
            this.pnlDetalle.Margin = new Padding(8, 16, 16, 16);
            this.pnlDetalle.BackColor = AppTheme.Blanco;
            this.pnlDetalle.Padding = new Padding(14);

            this.lblPlaceholder.Dock = DockStyle.Fill;
            this.lblPlaceholder.Text = "Selecciona un autobús de la lista para ver su ficha,\no usa \"+ Nuevo autobús\" para registrar uno.";
            this.lblPlaceholder.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblPlaceholder.ForeColor = System.Drawing.Color.FromArgb(158, 158, 158);
            this.lblPlaceholder.Font = AppTheme.FuenteBase;

            // ============================================================
            // --- Ficha (ver/editar seleccionado) — 100% responsiva:
            // todo con Dock=Top apilado, sin anchos fijos, para que use
            // todo el ancho disponible del panel derecho.
            // ============================================================
            this.pnlFicha.Dock = DockStyle.Fill;
            this.pnlFicha.Visible = false;
            this.pnlFicha.AutoScroll = true;

            // --- Encabezado de ficha: foto (más grande) + placa/subtítulo/botón ---
            var pnlFotoHeader = new Panel { Dock = DockStyle.Top, Height = 150, Margin = new Padding(0, 0, 0, 10) };

            this.picFoto.Dock = DockStyle.Left;
            this.picFoto.Width = 170;
            this.picFoto.Margin = new Padding(0, 0, 14, 0);
            this.picFoto.BackColor = AppTheme.GrisClaro;
            this.picFoto.BorderStyle = BorderStyle.FixedSingle;
            this.picFoto.SizeMode = PictureBoxSizeMode.Zoom;

            var pnlFichaInfo = new Panel { Dock = DockStyle.Fill };

            this.lblFichaPlaca.Font = AppTheme.FuenteSubtitulo;
            this.lblFichaPlaca.ForeColor = AppTheme.AzulOscuro;
            this.lblFichaPlaca.Dock = DockStyle.Top;
            this.lblFichaPlaca.Height = 32;

            this.lblFichaSubtitulo.Font = AppTheme.FuenteBase;
            this.lblFichaSubtitulo.ForeColor = System.Drawing.Color.FromArgb(117, 117, 117);
            this.lblFichaSubtitulo.Dock = DockStyle.Top;
            this.lblFichaSubtitulo.Height = 28;

            this.btnSubirFoto.Text = "Subir foto";
            this.btnSubirFoto.Size = new System.Drawing.Size(120, 32);
            this.btnSubirFoto.Location = new System.Drawing.Point(0, 68);
            this.btnSubirFoto.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            AppTheme.ComoBotonSecundario(this.btnSubirFoto);
            this.btnSubirFoto.Click += new System.EventHandler(this.BtnSubirFoto_Click);

            this.btnVerDetalle.Text = "Ver detalle";
            this.btnVerDetalle.Size = new System.Drawing.Size(120, 32);
            this.btnVerDetalle.Location = new System.Drawing.Point(130, 68);
            this.btnVerDetalle.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            AppTheme.ComoBotonSecundario(this.btnVerDetalle);
            this.btnVerDetalle.Click += new System.EventHandler(this.BtnVerDetalle_Click);

            pnlFichaInfo.Controls.Add(this.btnVerDetalle);
            pnlFichaInfo.Controls.Add(this.btnSubirFoto);
            pnlFichaInfo.Controls.Add(this.lblFichaSubtitulo);
            pnlFichaInfo.Controls.Add(this.lblFichaPlaca);

            pnlFotoHeader.Controls.Add(pnlFichaInfo);
            pnlFotoHeader.Controls.Add(this.picFoto);

            // --- Campos (Placa / Capacidad / Marca / Modelo) — ahora ancho completo ---
            this.pnlCamposFicha.Dock = DockStyle.Top;
            this.pnlCamposFicha.Height = 150;
            this.pnlCamposFicha.Margin = new Padding(0, 0, 0, 8);
            this.pnlCamposFicha.ColumnCount = 2;
            this.pnlCamposFicha.RowCount = 2;
            this.pnlCamposFicha.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            this.pnlCamposFicha.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            this.pnlCamposFicha.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            this.pnlCamposFicha.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            this.txtPlaca = CampoBuilder.CampoEnTabla(this.pnlCamposFicha, 0, 0, "Placa");
            this.numCapacidad = AgregarNumero(this.pnlCamposFicha, 0, 1, "Capacidad");
            this.txtMarca = CampoBuilder.CampoEnTabla(this.pnlCamposFicha, 1, 0, "Marca");
            this.txtModelo = CampoBuilder.CampoEnTabla(this.pnlCamposFicha, 1, 1, "Modelo");

            this.lblEstado.Text = "Estado operativo";
            this.lblEstado.Font = AppTheme.FuenteBase;
            this.lblEstado.ForeColor = AppTheme.GrisTexto;
            this.lblEstado.Dock = DockStyle.Top;
            this.lblEstado.Height = 22;

            this.cmbEstado.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbEstado.Font = AppTheme.FuenteBase;
            this.cmbEstado.Dock = DockStyle.Top;
            this.cmbEstado.Margin = new Padding(0, 0, 0, 12);
            this.cmbEstado.Items.AddRange(new object[] { "Disponible", "Activo", "Mantenimiento", "FueraServicio" });

            var pnlBotonesFicha = new TableLayoutPanel { Dock = DockStyle.Top, Height = 40, ColumnCount = 2, Margin = new Padding(0, 0, 0, 8) };
            pnlBotonesFicha.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            pnlBotonesFicha.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));

            this.btnGuardarCambios.Text = "Guardar cambios";
            this.btnGuardarCambios.Dock = DockStyle.Fill;
            this.btnGuardarCambios.Margin = new Padding(0, 0, 3, 0);
            AppTheme.ComoBotonPrimario(this.btnGuardarCambios);
            this.btnGuardarCambios.Click += new System.EventHandler(this.BtnGuardarCambios_Click);

            this.btnCambiarEstado.Text = "Cambiar estado";
            this.btnCambiarEstado.Dock = DockStyle.Fill;
            this.btnCambiarEstado.Margin = new Padding(3, 0, 0, 0);
            AppTheme.ComoBotonSecundario(this.btnCambiarEstado);
            this.btnCambiarEstado.Click += new System.EventHandler(this.BtnCambiarEstado_Click);

            pnlBotonesFicha.Controls.Add(this.btnGuardarCambios, 0, 0);
            pnlBotonesFicha.Controls.Add(this.btnCambiarEstado, 1, 0);

            this.btnEliminar.Text = "Eliminar autobús";
            this.btnEliminar.Dock = DockStyle.Top;
            this.btnEliminar.Height = 34;
            this.btnEliminar.Margin = new Padding(0, 0, 0, 12);
            AppTheme.ComoBotonPeligro(this.btnEliminar);
            this.btnEliminar.Click += new System.EventHandler(this.BtnEliminar_Click);

            this.lblMensajeFicha.Dock = DockStyle.Top;
            this.lblMensajeFicha.Height = 40;
            this.lblMensajeFicha.Font = AppTheme.FuenteBaseNegrita;

            this.pnlFicha.Controls.Add(this.lblMensajeFicha);
            this.pnlFicha.Controls.Add(this.btnEliminar);
            this.pnlFicha.Controls.Add(pnlBotonesFicha);
            this.pnlFicha.Controls.Add(this.cmbEstado);
            this.pnlFicha.Controls.Add(this.lblEstado);
            this.pnlFicha.Controls.Add(this.pnlCamposFicha);
            this.pnlFicha.Controls.Add(pnlFotoHeader);

            // ============================================================
            // --- Crear (registro de autobús nuevo) — mismo criterio ---
            // ============================================================
            this.pnlCrear.Dock = DockStyle.Fill;
            this.pnlCrear.Visible = false;
            this.pnlCrear.AutoScroll = true;

            this.lblCrearTitulo.Text = "Nuevo autobús";
            this.lblCrearTitulo.Font = AppTheme.FuenteSubtitulo;
            this.lblCrearTitulo.ForeColor = AppTheme.AzulOscuro;
            this.lblCrearTitulo.Dock = DockStyle.Top;
            this.lblCrearTitulo.Height = 32;
            this.lblCrearTitulo.Margin = new Padding(0, 0, 0, 8);

            this.pnlCamposCrear.Dock = DockStyle.Top;
            this.pnlCamposCrear.Height = 150;
            this.pnlCamposCrear.Margin = new Padding(0, 0, 0, 12);
            this.pnlCamposCrear.ColumnCount = 2;
            this.pnlCamposCrear.RowCount = 2;
            this.pnlCamposCrear.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            this.pnlCamposCrear.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            this.pnlCamposCrear.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            this.pnlCamposCrear.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            this.txtPlacaNueva = CampoBuilder.CampoEnTabla(this.pnlCamposCrear, 0, 0, "Placa");
            this.numCapacidadNueva = AgregarNumero(this.pnlCamposCrear, 0, 1, "Capacidad");
            this.txtMarcaNueva = CampoBuilder.CampoEnTabla(this.pnlCamposCrear, 1, 0, "Marca");
            this.txtModeloNueva = CampoBuilder.CampoEnTabla(this.pnlCamposCrear, 1, 1, "Modelo");

            var pnlBotonesCrear = new TableLayoutPanel { Dock = DockStyle.Top, Height = 40, ColumnCount = 2, Margin = new Padding(0, 0, 0, 8) };
            pnlBotonesCrear.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            pnlBotonesCrear.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));

            this.btnGuardarNuevo.Text = "Registrar autobús";
            this.btnGuardarNuevo.Dock = DockStyle.Fill;
            this.btnGuardarNuevo.Margin = new Padding(0, 0, 3, 0);
            AppTheme.ComoBotonPrimario(this.btnGuardarNuevo);
            this.btnGuardarNuevo.Click += new System.EventHandler(this.BtnGuardarNuevo_Click);

            this.btnCancelarCrear.Text = "Cancelar";
            this.btnCancelarCrear.Dock = DockStyle.Fill;
            this.btnCancelarCrear.Margin = new Padding(3, 0, 0, 0);
            AppTheme.ComoBotonSecundario(this.btnCancelarCrear);
            this.btnCancelarCrear.Click += new System.EventHandler(this.BtnCancelarCrear_Click);

            pnlBotonesCrear.Controls.Add(this.btnGuardarNuevo, 0, 0);
            pnlBotonesCrear.Controls.Add(this.btnCancelarCrear, 1, 0);

            this.lblMensajeCrear.Dock = DockStyle.Top;
            this.lblMensajeCrear.Height = 40;
            this.lblMensajeCrear.Font = AppTheme.FuenteBaseNegrita;

            this.pnlCrear.Controls.Add(this.lblMensajeCrear);
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

            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picFoto)).EndInit();
            this.ResumeLayout(false);
        }

        private static NumericUpDown AgregarNumero(TableLayoutPanel tabla, int fila, int columna, string etiqueta)
        {
            var contenedor = new Panel { Dock = DockStyle.Fill, Margin = new Padding(6, 8, 6, 14) };
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
            var num = new NumericUpDown { Dock = DockStyle.Top, Font = AppTheme.FuenteBase, Minimum = 1, Maximum = 200 };
            contenedor.Controls.Add(num);
            contenedor.Controls.Add(lbl);
            tabla.Controls.Add(contenedor, columna, fila);
            return num;
        }

        private Panel pnlHeader;
        private Label lblTitulo;
        private Button btnNuevo;

        private TableLayoutPanel tblRaiz;
        private Panel pnlListado;
        private DataGridView dgv;

        private Panel pnlDetalle;
        private Label lblPlaceholder;

        private Panel pnlFicha;
        private PictureBox picFoto;
        private Label lblFichaPlaca;
        private Label lblFichaSubtitulo;
        private Button btnSubirFoto;
        private Button btnVerDetalle;
        private TableLayoutPanel pnlCamposFicha;
        private TextBox txtPlaca, txtMarca, txtModelo;
        private NumericUpDown numCapacidad;
        private Label lblEstado;
        private ComboBox cmbEstado;
        private Button btnGuardarCambios;
        private Button btnCambiarEstado;
        private Button btnEliminar;
        private Label lblMensajeFicha;

        private Panel pnlCrear;
        private Label lblCrearTitulo;
        private TableLayoutPanel pnlCamposCrear;
        private TextBox txtPlacaNueva, txtMarcaNueva, txtModeloNueva;
        private NumericUpDown numCapacidadNueva;
        private Button btnGuardarNuevo;
        private Button btnCancelarCrear;
        private Label lblMensajeCrear;
    }
}