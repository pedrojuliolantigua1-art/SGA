using SGA.Desktop.Api;
using SGA.Desktop.DTOs.Common;
using SGA.Desktop.DTOs.Usuario;
using SGA.Desktop.Services;
using SGA.Desktop.UI;

namespace SGA.Desktop.Forms
{
    public enum TipoUsuarioNuevo { Estudiante, Docente, Administrativo }

    public partial class UsuariosForm : Form
    {
        private List<UsuarioResumenPresentacionDto> _usuarios = new();
        private TipoUsuarioNuevo _tipoActual = TipoUsuarioNuevo.Estudiante;

        public UsuariosForm()
        {
            InitializeComponent();
            ConstruirFormulario();
            cmbFiltroTipo.SelectedIndex = 0;
            Load += async (_, _) => await CargarListaAsync();
        }

        // === Listado ===

        private async Task CargarListaAsync()
        {
            var resultado = await SgaApiClient.GetAsync<List<UsuarioResumenPresentacionDto>>("api/usuarios");
            if (!resultado.EsExitoso)
            {
                CampoBuilder.MostrarMensaje(lblMensajeListado, resultado.Error!, true);
                return;
            }

            _usuarios = resultado.Valor!;
            AplicarFiltro();
        }

        private void CmbFiltroTipo_SelectedIndexChanged(object sender, EventArgs e) => AplicarFiltro();

        private void AplicarFiltro()
        {
            IEnumerable<UsuarioResumenPresentacionDto> vista = _usuarios;
            var filtro = cmbFiltroTipo.SelectedItem?.ToString();

            if (!string.IsNullOrWhiteSpace(filtro) && filtro != "Todos")
                vista = vista.Where(u => string.Equals(u.TipoUsuario, filtro, StringComparison.OrdinalIgnoreCase));

            dgv.DataSource = null;
            dgv.DataSource = vista.ToList();
            if (dgv.Columns["RolSistema"] is { } col) col.Visible = false;
            if (dgv.Columns["Id"] is { } colId) colId.Visible = false;
        }

        private async void BtnRefrescar_Click(object sender, EventArgs e) => await CargarListaAsync();

        private async void BtnBaja_Click(object sender, EventArgs e)
        {
            if (dgv.CurrentRow?.DataBoundItem is not UsuarioResumenPresentacionDto usuario)
            {
                CampoBuilder.MostrarMensaje(lblMensajeListado, "Selecciona un usuario en la lista.", true);
                return;
            }

            var confirmar = MessageBox.Show(
                $"¿Dar de baja a {usuario.Nombre} {usuario.Apellido}?",
                "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirmar != DialogResult.Yes) return;

            var dto = new EliminarPresentacionDto
            {
                Motivo = "Dado de baja desde el módulo de Usuarios",
                EliminadoPor = SesionActual.Usuario?.Correo
            };

            var resultado = await SgaApiClient.DeleteAsync<object>($"api/usuarios/{usuario.Id}", dto);
            CampoBuilder.MostrarMensaje(lblMensajeListado,
                resultado.EsExitoso ? "Usuario dado de baja." : resultado.Error!, !resultado.EsExitoso);

            if (resultado.EsExitoso) await CargarListaAsync();
        }

        // === Selector de tipo ===

        private void BtnTipoEstudiante_Click(object sender, EventArgs e) => SeleccionarTipo(TipoUsuarioNuevo.Estudiante);
        private void BtnTipoDocente_Click(object sender, EventArgs e) => SeleccionarTipo(TipoUsuarioNuevo.Docente);
        private void BtnTipoAdministrativo_Click(object sender, EventArgs e) => SeleccionarTipo(TipoUsuarioNuevo.Administrativo);

        private void SeleccionarTipo(TipoUsuarioNuevo tipo)
        {
            _tipoActual = tipo;

            grpEstudiante.Visible = tipo == TipoUsuarioNuevo.Estudiante;
            grpDocente.Visible = tipo == TipoUsuarioNuevo.Docente;
            grpAdmin.Visible = tipo == TipoUsuarioNuevo.Administrativo;

            AppTheme.ComoBotonToggle(btnTipoEstudiante, tipo == TipoUsuarioNuevo.Estudiante);
            AppTheme.ComoBotonToggle(btnTipoDocente, tipo == TipoUsuarioNuevo.Docente);
            AppTheme.ComoBotonToggle(btnTipoAdministrativo, tipo == TipoUsuarioNuevo.Administrativo);

            lblMensajeNuevo.Text = string.Empty;
        }

        // === Registro ===

        private async void BtnRegistrar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) || string.IsNullOrWhiteSpace(txtCorreo.Text))
            {
                CampoBuilder.MostrarMensaje(lblMensajeNuevo, "Nombre y correo son obligatorios.", true);
                return;
            }

            bool exito;
            string? error;

            switch (_tipoActual)
            {
                case TipoUsuarioNuevo.Estudiante:
                    var dtoEst = new CrearEstudiantePresentacionDto
                    {
                        Nombre = txtNombre.Text.Trim(),
                        Apellido = txtApellido.Text.Trim(),
                        Correo = txtCorreo.Text.Trim(),
                        Telefono = txtTelefono.Text.Trim(),
                        PasswordHash = txtPassword.Text,
                        Matricula = txtMatricula.Text.Trim(),
                        Carrera = txtCarrera.Text.Trim(),
                        CreadoPor = SesionActual.Usuario?.Correo
                    };
                    var rEst = await SgaApiClient.PostAsync<EstudiantePresentacionDto>("api/usuarios/estudiantes", dtoEst);
                    exito = rEst.EsExitoso; error = rEst.Error;
                    break;

                case TipoUsuarioNuevo.Docente:
                    var dtoDoc = new CrearEmpleadoDocentePresentacionDto
                    {
                        Nombre = txtNombre.Text.Trim(),
                        Apellido = txtApellido.Text.Trim(),
                        Correo = txtCorreo.Text.Trim(),
                        Telefono = txtTelefono.Text.Trim(),
                        PasswordHash = txtPassword.Text,
                        CodigoEmpleado = txtDocCodigo.Text.Trim(),
                        Departamento = txtDocDepto.Text.Trim(),
                        Cargo = txtDocCargo.Text.Trim(),
                        Especialidad = txtDocEspecialidad.Text.Trim(),
                        TipoContrato = txtDocContrato.Text.Trim(),
                        CreadoPor = SesionActual.Usuario?.Correo
                    };
                    var rDoc = await SgaApiClient.PostAsync<EmpleadoDocentePresentacionDto>("api/usuarios/empleados/docentes", dtoDoc);
                    exito = rDoc.EsExitoso; error = rDoc.Error;
                    break;

                case TipoUsuarioNuevo.Administrativo:
                    var dtoAdm = new CrearEmpleadoAdministrativoPresentacionDto
                    {
                        Nombre = txtNombre.Text.Trim(),
                        Apellido = txtApellido.Text.Trim(),
                        Correo = txtCorreo.Text.Trim(),
                        Telefono = txtTelefono.Text.Trim(),
                        PasswordHash = txtPassword.Text,
                        CodigoEmpleado = txtAdmCodigo.Text.Trim(),
                        Departamento = txtAdmDepto.Text.Trim(),
                        Cargo = txtAdmCargo.Text.Trim(),
                        AreaAdministrativa = txtAdmArea.Text.Trim(),
                        CreadoPor = SesionActual.Usuario?.Correo
                    };
                    var rAdm = await SgaApiClient.PostAsync<EmpleadoAdministrativoPresentacionDto>("api/usuarios/empleados/administrativos", dtoAdm);
                    exito = rAdm.EsExitoso; error = rAdm.Error;
                    break;

                default:
                    throw new InvalidOperationException();
            }

            CampoBuilder.MostrarMensaje(lblMensajeNuevo, exito ? "Usuario registrado correctamente." : error!, !exito);

            if (exito)
            {
                LimpiarCamposComunes();
                LimpiarCamposEspecificos();
                await CargarListaAsync();
            }
        }

        private void LimpiarCamposComunes()
        {
            txtNombre.Clear(); txtApellido.Clear(); txtCorreo.Clear();
            txtTelefono.Clear(); txtPassword.Clear();
        }

        private void LimpiarCamposEspecificos()
        {
            txtMatricula.Clear(); txtCarrera.Clear();
            txtDocCodigo.Clear(); txtDocDepto.Clear(); txtDocCargo.Clear();
            txtDocEspecialidad.Clear(); txtDocContrato.Clear();
            txtAdmCodigo.Clear(); txtAdmDepto.Clear(); txtAdmCargo.Clear(); txtAdmArea.Clear();
        }
    }
}