using SGA.Desktop.UI;

namespace SGA.Desktop.Forms
{
    partial class UsuariosForm
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
            this.ClientSize = new System.Drawing.Size(1040, 720);
            this.MinimumSize = new System.Drawing.Size(880, 560);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Usuarios — SGA-ITLA";
            this.ResumeLayout(false);
        }

        private void ConstruirFormulario()
        {
            this.pnlHeader = new Panel();
            this.lblTitulo = new Label();

            this.tblRaiz = new TableLayoutPanel();
            this.pnlListado = new Panel();
            this.pnlNuevoUsuario = new Panel();

            // --- Listado ---
            this.pnlFiltro = new TableLayoutPanel();
            this.lblFiltro = new Label();
            this.cmbFiltroTipo = new ComboBox();
            this.btnRefrescar = new Button();
            this.dgv = new DataGridView();
            this.pnlAccionesLista = new Panel();
            this.btnBaja = new Button();
            this.lblMensajeListado = new Label();

            // --- Nuevo usuario ---
            this.pnlScroll = new Panel();
            this.lblTituloForm = new Label();
            this.pnlTipoSelector = new TableLayoutPanel();
            this.btnTipoEstudiante = new Button();
            this.btnTipoDocente = new Button();
            this.btnTipoAdministrativo = new Button();
            this.pnlComunes = new TableLayoutPanel();
            this.pnlEspecificos = new Panel();
            this.grpEstudiante = new TableLayoutPanel();
            this.grpDocente = new TableLayoutPanel();
            this.grpAdmin = new TableLayoutPanel();
            this.lblMensajeNuevo = new Label();
            this.btnRegistrar = new Button();

            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.SuspendLayout();

            // === Encabezado ===
            this.pnlHeader.Dock = DockStyle.Top;
            this.pnlHeader.Height = 55;
            this.pnlHeader.BackColor = AppTheme.AzulOscuro;
            this.lblTitulo.Text = "Gestión de usuarios";
            this.lblTitulo.ForeColor = AppTheme.Blanco;
            this.lblTitulo.Font = AppTheme.FuenteTitulo;
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Location = new System.Drawing.Point(20, 15);
            this.pnlHeader.Controls.Add(this.lblTitulo);

            // === Layout raíz: 2 columnas responsivas (55% / 45%) ===
            this.tblRaiz.Dock = DockStyle.Fill;
            this.tblRaiz.ColumnCount = 2;
            this.tblRaiz.RowCount = 1;
            this.tblRaiz.BackColor = AppTheme.GrisClaro;
            this.tblRaiz.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 55F));
            this.tblRaiz.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45F));
            this.tblRaiz.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            // --- Panel Listado ---
            this.pnlListado.Dock = DockStyle.Fill;
            this.pnlListado.Margin = new Padding(16, 16, 8, 16);
            this.pnlListado.BackColor = AppTheme.Blanco;
            this.pnlListado.Padding = new Padding(14);

            this.pnlFiltro.Dock = DockStyle.Top;
            this.pnlFiltro.Height = 40;
            this.pnlFiltro.ColumnCount = 3;
            this.pnlFiltro.RowCount = 1;
            this.pnlFiltro.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            this.pnlFiltro.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            this.pnlFiltro.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));

            this.lblFiltro.Text = "Filtrar por tipo";
            this.lblFiltro.Font = AppTheme.FuenteBaseNegrita;
            this.lblFiltro.ForeColor = AppTheme.GrisTexto;
            this.lblFiltro.AutoSize = true;
            this.lblFiltro.Anchor = AnchorStyles.Left;
            this.lblFiltro.Margin = new Padding(0, 10, 8, 0);

            this.cmbFiltroTipo.Dock = DockStyle.Fill;
            this.cmbFiltroTipo.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbFiltroTipo.Font = AppTheme.FuenteBase;
            this.cmbFiltroTipo.Margin = new Padding(0, 5, 8, 5);
            this.cmbFiltroTipo.Items.AddRange(new object[] { "Todos", "Estudiante", "Docente", "Administrativo" });
            this.cmbFiltroTipo.SelectedIndexChanged += new System.EventHandler(this.CmbFiltroTipo_SelectedIndexChanged);

            this.btnRefrescar.Text = "Refrescar";
            this.btnRefrescar.Dock = DockStyle.Fill;
            this.btnRefrescar.Margin = new Padding(0, 3, 0, 3);
            AppTheme.ComoBotonSecundario(this.btnRefrescar);
            this.btnRefrescar.Click += new System.EventHandler(this.BtnRefrescar_Click);

            this.pnlFiltro.Controls.Add(this.lblFiltro, 0, 0);
            this.pnlFiltro.Controls.Add(this.cmbFiltroTipo, 1, 0);
            this.pnlFiltro.Controls.Add(this.btnRefrescar, 2, 0);

            AppTheme.ComoGrillaEstandar(this.dgv);
            this.dgv.Dock = DockStyle.Fill;

            this.pnlAccionesLista.Dock = DockStyle.Bottom;
            this.pnlAccionesLista.Height = 74;

            this.btnBaja.Text = "Dar de baja seleccionado";
            this.btnBaja.Dock = DockStyle.Top;
            this.btnBaja.Margin = new Padding(0, 10, 0, 0);
            AppTheme.ComoBotonPeligro(this.btnBaja);
            this.btnBaja.Click += new System.EventHandler(this.BtnBaja_Click);

            this.lblMensajeListado.Dock = DockStyle.Top;
            this.lblMensajeListado.Height = 20;
            this.lblMensajeListado.Font = AppTheme.FuenteBaseNegrita;
            this.lblMensajeListado.Text = string.Empty;

            this.pnlAccionesLista.Controls.Add(this.btnBaja);
            this.pnlAccionesLista.Controls.Add(this.lblMensajeListado);

            this.pnlListado.Controls.Add(this.dgv);
            this.pnlListado.Controls.Add(this.pnlAccionesLista);
            this.pnlListado.Controls.Add(this.pnlFiltro);

            // --- Panel Nuevo usuario ---
            this.pnlNuevoUsuario.Dock = DockStyle.Fill;
            this.pnlNuevoUsuario.Margin = new Padding(8, 16, 16, 16);
            this.pnlNuevoUsuario.BackColor = AppTheme.Blanco;
            this.pnlNuevoUsuario.Padding = new Padding(14);

            this.btnRegistrar.Text = "Registrar usuario";
            this.btnRegistrar.Dock = DockStyle.Bottom;
            this.btnRegistrar.Height = 38;
            this.btnRegistrar.Margin = new Padding(0, 8, 0, 0);
            AppTheme.ComoBotonPrimario(this.btnRegistrar);
            this.btnRegistrar.Click += new System.EventHandler(this.BtnRegistrar_Click);

            this.lblMensajeNuevo.Dock = DockStyle.Bottom;
            this.lblMensajeNuevo.Height = 20;
            this.lblMensajeNuevo.Font = AppTheme.FuenteBaseNegrita;
            this.lblMensajeNuevo.Text = string.Empty;

            // Panel con scroll: si la ventana se achica, aparece scroll en vez de recortar campos.
            this.pnlScroll.Dock = DockStyle.Fill;
            this.pnlScroll.AutoScroll = true;

            this.lblTituloForm.Text = "Nuevo usuario";
            this.lblTituloForm.Font = AppTheme.FuenteSubtitulo;
            this.lblTituloForm.ForeColor = AppTheme.AzulOscuro;
            this.lblTituloForm.Dock = DockStyle.Top;
            this.lblTituloForm.Height = 26;
            this.lblTituloForm.AutoSize = false;

            // Selector de tipo (toggle)
            this.pnlTipoSelector.Dock = DockStyle.Top;
            this.pnlTipoSelector.Height = 36;
            this.pnlTipoSelector.ColumnCount = 3;
            this.pnlTipoSelector.RowCount = 1;
            this.pnlTipoSelector.Margin = new Padding(0, 6, 0, 10);
            for (int i = 0; i < 3; i++)
                this.pnlTipoSelector.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F / 3));

            this.btnTipoEstudiante.Text = "Estudiante";
            this.btnTipoEstudiante.Dock = DockStyle.Fill;
            this.btnTipoEstudiante.Margin = new Padding(0, 0, 3, 0);
            this.btnTipoEstudiante.Click += new System.EventHandler(this.BtnTipoEstudiante_Click);

            this.btnTipoDocente.Text = "Docente";
            this.btnTipoDocente.Dock = DockStyle.Fill;
            this.btnTipoDocente.Margin = new Padding(3, 0, 3, 0);
            this.btnTipoDocente.Click += new System.EventHandler(this.BtnTipoDocente_Click);

            this.btnTipoAdministrativo.Text = "Administrativo";
            this.btnTipoAdministrativo.Dock = DockStyle.Fill;
            this.btnTipoAdministrativo.Margin = new Padding(3, 0, 0, 0);
            this.btnTipoAdministrativo.Click += new System.EventHandler(this.BtnTipoAdministrativo_Click);

            this.pnlTipoSelector.Controls.Add(this.btnTipoEstudiante, 0, 0);
            this.pnlTipoSelector.Controls.Add(this.btnTipoDocente, 1, 0);
            this.pnlTipoSelector.Controls.Add(this.btnTipoAdministrativo, 2, 0);

            // Campos comunes (2 columnas x 3 filas)
            this.pnlComunes.Dock = DockStyle.Top;
            this.pnlComunes.Height = 168;
            this.pnlComunes.ColumnCount = 2;
            this.pnlComunes.RowCount = 3;
            this.pnlComunes.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            this.pnlComunes.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            for (int i = 0; i < 3; i++)
                this.pnlComunes.RowStyles.Add(new RowStyle(SizeType.Percent, 100F / 3));

            this.txtNombre = AgregarCampo(this.pnlComunes, 0, 0, "Nombre");
            this.txtApellido = AgregarCampo(this.pnlComunes, 0, 1, "Apellido");
            this.txtCorreo = AgregarCampo(this.pnlComunes, 1, 0, "Correo");
            this.txtTelefono = AgregarCampo(this.pnlComunes, 1, 1, "Teléfono");
            this.txtPassword = AgregarCampo(this.pnlComunes, 2, 0, "Contraseña inicial", colSpan: 2);
            this.txtPassword.UseSystemPasswordChar = true;

            // Panel contenedor de grupos específicos (altura fija = la del grupo más alto)
            this.pnlEspecificos.Dock = DockStyle.Top;
            this.pnlEspecificos.Height = 150;

            this.grpEstudiante.Dock = DockStyle.Fill;
            this.grpEstudiante.ColumnCount = 2;
            this.grpEstudiante.RowCount = 1;
            this.grpEstudiante.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            this.grpEstudiante.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            this.grpEstudiante.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            this.txtMatricula = AgregarCampo(this.grpEstudiante, 0, 0, "Matrícula");
            this.txtCarrera = AgregarCampo(this.grpEstudiante, 0, 1, "Carrera");

            this.grpDocente.Dock = DockStyle.Fill;
            this.grpDocente.Visible = false;
            this.grpDocente.ColumnCount = 2;
            this.grpDocente.RowCount = 3;
            this.grpDocente.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            this.grpDocente.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            for (int i = 0; i < 3; i++)
                this.grpDocente.RowStyles.Add(new RowStyle(SizeType.Percent, 100F / 3));
            this.txtDocCodigo = AgregarCampo(this.grpDocente, 0, 0, "Código de empleado");
            this.txtDocDepto = AgregarCampo(this.grpDocente, 0, 1, "Departamento");
            this.txtDocCargo = AgregarCampo(this.grpDocente, 1, 0, "Cargo");
            this.txtDocEspecialidad = AgregarCampo(this.grpDocente, 1, 1, "Especialidad");
            this.txtDocContrato = AgregarCampo(this.grpDocente, 2, 0, "Tipo de contrato", colSpan: 2);

            this.grpAdmin.Dock = DockStyle.Fill;
            this.grpAdmin.Visible = false;
            this.grpAdmin.ColumnCount = 2;
            this.grpAdmin.RowCount = 2;
            this.grpAdmin.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            this.grpAdmin.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            for (int i = 0; i < 2; i++)
                this.grpAdmin.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            this.txtAdmCodigo = AgregarCampo(this.grpAdmin, 0, 0, "Código de empleado");
            this.txtAdmDepto = AgregarCampo(this.grpAdmin, 0, 1, "Departamento");
            this.txtAdmCargo = AgregarCampo(this.grpAdmin, 1, 0, "Cargo");
            this.txtAdmArea = AgregarCampo(this.grpAdmin, 1, 1, "Área administrativa");

            // Los 3 grupos comparten el mismo contenedor; solo uno visible a la vez.
            this.pnlEspecificos.Controls.Add(this.grpAdmin);
            this.pnlEspecificos.Controls.Add(this.grpDocente);
            this.pnlEspecificos.Controls.Add(this.grpEstudiante);

            this.pnlScroll.Controls.Add(this.pnlEspecificos);
            this.pnlScroll.Controls.Add(this.pnlComunes);
            this.pnlScroll.Controls.Add(this.pnlTipoSelector);
            this.pnlScroll.Controls.Add(this.lblTituloForm);

            this.pnlNuevoUsuario.Controls.Add(this.pnlScroll);
            this.pnlNuevoUsuario.Controls.Add(this.lblMensajeNuevo);
            this.pnlNuevoUsuario.Controls.Add(this.btnRegistrar);

            this.tblRaiz.Controls.Add(this.pnlListado, 0, 0);
            this.tblRaiz.Controls.Add(this.pnlNuevoUsuario, 1, 0);

            this.Controls.Add(this.tblRaiz);
            this.Controls.Add(this.pnlHeader);

            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ResumeLayout(false);

            SeleccionarTipo(TipoUsuarioNuevo.Estudiante);
        }

        /// <summary>Crea, dentro de una celda de TableLayoutPanel, un mini-panel con etiqueta arriba
        /// y campo de texto abajo (Dock=Top), para que el campo se estire con el ancho de la columna.</summary>
        private static TextBox AgregarCampo(TableLayoutPanel tabla, int fila, int columna, string etiqueta, int colSpan = 1)
        {
            var contenedor = new Panel { Dock = DockStyle.Fill, Margin = new Padding(4) };
            var lbl = new Label
            {
                Text = etiqueta,
                Dock = DockStyle.Top,
                AutoSize = false,
                Height = 26,
                Font = AppTheme.FuenteBase,
                ForeColor = System.Drawing.Color.FromArgb(117, 117, 117),
                TextAlign = ContentAlignment.BottomLeft
            };
            var txt = new TextBox { Dock = DockStyle.Top, Font = AppTheme.FuenteBase };
            contenedor.Controls.Add(txt);
            contenedor.Controls.Add(lbl);
            tabla.Controls.Add(contenedor, columna, fila);
            if (colSpan > 1) tabla.SetColumnSpan(contenedor, colSpan);
            return txt;
        }

        private Panel pnlHeader;
        private Label lblTitulo;
        private TableLayoutPanel tblRaiz;

        private Panel pnlListado;
        private TableLayoutPanel pnlFiltro;
        private Label lblFiltro;
        private ComboBox cmbFiltroTipo;
        private Button btnRefrescar;
        private DataGridView dgv;
        private Panel pnlAccionesLista;
        private Button btnBaja;
        private Label lblMensajeListado;

        private Panel pnlNuevoUsuario;
        private Panel pnlScroll;
        private Label lblTituloForm;
        private TableLayoutPanel pnlTipoSelector;
        private Button btnTipoEstudiante;
        private Button btnTipoDocente;
        private Button btnTipoAdministrativo;
        private TableLayoutPanel pnlComunes;
        private TextBox txtNombre, txtApellido, txtCorreo, txtTelefono, txtPassword;
        private Panel pnlEspecificos;
        private TableLayoutPanel grpEstudiante;
        private TextBox txtMatricula, txtCarrera;
        private TableLayoutPanel grpDocente;
        private TextBox txtDocCodigo, txtDocDepto, txtDocCargo, txtDocEspecialidad, txtDocContrato;
        private TableLayoutPanel grpAdmin;
        private TextBox txtAdmCodigo, txtAdmDepto, txtAdmCargo, txtAdmArea;
        private Label lblMensajeNuevo;
        private Button btnRegistrar;
    }
}