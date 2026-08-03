using SGA.Desktop.Api;
using SGA.Desktop.Services;

namespace SGA.Desktop.Forms
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            txtCorreo.Text = "admin@itla.edu.do";
            txtPassword.Text = "Admin123!";
        }

        private async void btnIngresar_Click(object sender, EventArgs e)
        {
            lblError.Text = string.Empty;

            if (string.IsNullOrWhiteSpace(txtCorreo.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                lblError.Text = "Correo y contraseña son obligatorios.";
                return;
            }

            btnIngresar.Enabled = false;
            try
            {
                
                var body = new { Correo = txtCorreo.Text.Trim(), Password = txtPassword.Text };
                var resultado = await SgaApiClient.PostAsync<LoginResponseDto>("api/auth/login", body);

                if (!resultado.EsExitoso || resultado.Valor?.Usuario is null)
                {
                    lblError.Text = resultado.Error ?? "Credenciales incorrectas.";
                    return;
                }

                SesionActual.IniciarSesion(resultado.Valor.Usuario, resultado.Valor.Token);

                var menu = new MainMenuForm();
                menu.Show();
                Hide();
            }
            finally
            {
                btnIngresar.Enabled = true;
            }
        }
    }
}
