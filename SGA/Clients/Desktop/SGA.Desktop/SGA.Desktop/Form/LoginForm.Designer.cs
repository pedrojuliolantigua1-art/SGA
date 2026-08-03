namespace SGA.Desktop.Forms
{
    partial class LoginForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlMarca = new Panel();
            this.lblLogo = new Label();
            this.lblMarcaTitulo = new Label();
            this.lblMarcaSubtitulo = new Label();

            this.pnlFormulario = new Panel();
            this.lblTitulo = new Label();
            this.lblSubtitulo = new Label();
            this.lblCorreo = new Label();
            this.txtCorreo = new TextBox();
            this.lblPassword = new Label();
            this.txtPassword = new TextBox();
            this.lblError = new Label();
            this.btnIngresar = new Button();
            this.lblPie = new Label();

            this.SuspendLayout();

            // === pnlMarca (panel izquierdo, azul institucional) ===
            this.pnlMarca.Dock = DockStyle.Left;
            this.pnlMarca.Width = 260;
            this.pnlMarca.BackColor = SGA.Desktop.UI.AppTheme.AzulOscuro;
            this.pnlMarca.Controls.Add(this.lblLogo);
            this.pnlMarca.Controls.Add(this.lblMarcaTitulo);
            this.pnlMarca.Controls.Add(this.lblMarcaSubtitulo);

            this.lblLogo.Text = "SGA";
            this.lblLogo.ForeColor = SGA.Desktop.UI.AppTheme.Blanco;
            this.lblLogo.BackColor = SGA.Desktop.UI.AppTheme.Azul;
            this.lblLogo.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblLogo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblLogo.Size = new System.Drawing.Size(44, 44);
            this.lblLogo.Location = new System.Drawing.Point(32, 130);

            this.lblMarcaTitulo.Text = "Sistema de Gestión\nde Autobuses";
            this.lblMarcaTitulo.ForeColor = SGA.Desktop.UI.AppTheme.Blanco;
            this.lblMarcaTitulo.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblMarcaTitulo.AutoSize = true;
            this.lblMarcaTitulo.Location = new System.Drawing.Point(32, 184);

            this.lblMarcaSubtitulo.Text = "Instituto Tecnológico\nde las Américas";
            this.lblMarcaSubtitulo.ForeColor = System.Drawing.Color.FromArgb(200, 216, 240);
            this.lblMarcaSubtitulo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblMarcaSubtitulo.AutoSize = true;
            this.lblMarcaSubtitulo.Location = new System.Drawing.Point(32, 236);

            // === pnlFormulario (panel derecho, formulario) ===
            this.pnlFormulario.Dock = DockStyle.Fill;
            this.pnlFormulario.BackColor = SGA.Desktop.UI.AppTheme.Blanco;
            this.pnlFormulario.Controls.Add(this.lblTitulo);
            this.pnlFormulario.Controls.Add(this.lblSubtitulo);
            this.pnlFormulario.Controls.Add(this.lblCorreo);
            this.pnlFormulario.Controls.Add(this.txtCorreo);
            this.pnlFormulario.Controls.Add(this.lblPassword);
            this.pnlFormulario.Controls.Add(this.txtPassword);
            this.pnlFormulario.Controls.Add(this.lblError);
            this.pnlFormulario.Controls.Add(this.btnIngresar);
            this.pnlFormulario.Controls.Add(this.lblPie);

            this.lblTitulo.Text = "Iniciar sesión";
            this.lblTitulo.Font = SGA.Desktop.UI.AppTheme.FuenteSubtitulo;
            this.lblTitulo.ForeColor = SGA.Desktop.UI.AppTheme.AzulOscuro;
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Location = new System.Drawing.Point(44, 60);

            this.lblSubtitulo.Text = "Acceso administrativo del transporte institucional";
            this.lblSubtitulo.Font = SGA.Desktop.UI.AppTheme.FuenteBase;
            this.lblSubtitulo.ForeColor = System.Drawing.Color.FromArgb(117, 117, 117);
            this.lblSubtitulo.AutoSize = true;
            this.lblSubtitulo.Location = new System.Drawing.Point(44, 86);

            this.lblCorreo.Text = "Correo institucional";
            this.lblCorreo.Font = SGA.Desktop.UI.AppTheme.FuenteBaseNegrita;
            this.lblCorreo.ForeColor = SGA.Desktop.UI.AppTheme.GrisTexto;
            this.lblCorreo.AutoSize = true;
            this.lblCorreo.Location = new System.Drawing.Point(44, 128);

            this.txtCorreo.Location = new System.Drawing.Point(44, 148);
            this.txtCorreo.Size = new System.Drawing.Size(280, 27);
            this.txtCorreo.Font = SGA.Desktop.UI.AppTheme.FuenteBase;
            this.txtCorreo.PlaceholderText = "admin@itla.edu.do";

            this.lblPassword.Text = "Contraseña";
            this.lblPassword.Font = SGA.Desktop.UI.AppTheme.FuenteBaseNegrita;
            this.lblPassword.ForeColor = SGA.Desktop.UI.AppTheme.GrisTexto;
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(44, 188);

            this.txtPassword.Location = new System.Drawing.Point(44, 208);
            this.txtPassword.Size = new System.Drawing.Size(280, 27);
            this.txtPassword.Font = SGA.Desktop.UI.AppTheme.FuenteBase;
            this.txtPassword.UseSystemPasswordChar = true;

            this.lblError.Location = new System.Drawing.Point(44, 240);
            this.lblError.Size = new System.Drawing.Size(280, 32);
            this.lblError.ForeColor = SGA.Desktop.UI.AppTheme.Rojo;
            this.lblError.Font = SGA.Desktop.UI.AppTheme.FuenteBaseNegrita;
            this.lblError.Text = string.Empty;

            this.btnIngresar.Text = "Ingresar";
            this.btnIngresar.Location = new System.Drawing.Point(44, 278);
            this.btnIngresar.Size = new System.Drawing.Size(280, 36);
            SGA.Desktop.UI.AppTheme.ComoBotonPrimario(this.btnIngresar);
            this.btnIngresar.Click += new System.EventHandler(this.btnIngresar_Click);

            this.lblPie.Text = "SGA-ITLA v1.0 · Uso exclusivo de personal autorizado";
            this.lblPie.ForeColor = System.Drawing.Color.FromArgb(158, 158, 158);
            this.lblPie.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblPie.AutoSize = true;
            this.lblPie.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblPie.Location = new System.Drawing.Point(44, 336);

            // === LoginForm ===
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(640, 400);
            this.Controls.Add(this.pnlFormulario);
            this.Controls.Add(this.pnlMarca);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Iniciar sesión — SGA-ITLA";

            this.ResumeLayout(false);
        }

        private Panel pnlMarca;
        private Label lblLogo;
        private Label lblMarcaTitulo;
        private Label lblMarcaSubtitulo;

        private Panel pnlFormulario;
        private Label lblTitulo;
        private Label lblSubtitulo;
        private Label lblCorreo;
        private TextBox txtCorreo;
        private Label lblPassword;
        private TextBox txtPassword;
        private Label lblError;
        private Button btnIngresar;
        private Label lblPie;
    }
}