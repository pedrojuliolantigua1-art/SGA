using SGA.Desktop.Api;
using SGA.Desktop.DTOs.Common;
using SGA.Desktop.DTOs.Conductor;
using SGA.Desktop.Services;
using SGA.Desktop.UI;

namespace SGA.Desktop.Forms
{
    public partial class ConductoresForm : Form
    {
        private List<ConductorPresentacionDto> _conductores = new();
        private ConductorPresentacionDto? _seleccionado;

        public ConductoresForm()
        {
            InitializeComponent();
            ConstruirFormulario();
            Load += async (_, _) => await CargarListaAsync();
        }

        private async Task CargarListaAsync()
        {
            var resultado = await SgaApiClient.GetAsync<List<ConductorPresentacionDto>>("api/usuarios/conductores");
            if (!resultado.EsExitoso)
            {
                CampoBuilder.MostrarMensaje(_lblMensajeDetalle, resultado.Error!, true);
                return;
            }

            _conductores = resultado.Valor ?? new();
            _dgv.DataSource = null;
            _dgv.DataSource = _conductores;
            if (_dgv.Columns["Id"] is { } col) col.Visible = false;
        }

        private void Dgv_SelectionChanged(object? sender, EventArgs e)
        {
            if (_dgv.CurrentRow?.DataBoundItem is not ConductorPresentacionDto c) return;

            _seleccionado = c;
            MostrarFicha();

            _txtNombre.Text = c.Nombre;
            _txtApellido.Text = c.Apellido;
            _txtCorreo.Text = c.Correo;
            _txtTelefono.Text = c.Telefono;
            _txtLicencia.Text = c.NumeroLicencia;
            if (c.FechaVencimientoLicencia is { } fecha) _dtpVencimiento.Value = fecha;
            _cmbDisponible.SelectedIndex = c.Disponible ? 0 : 1;

            lblFichaNombre.Text = $"{c.Nombre} {c.Apellido}";
            lblFichaSubtitulo.Text = $"Licencia {c.NumeroLicencia} · vence {c.FechaVencimientoLicencia:MM/yyyy}";
            _lblMensajeDetalle.Text = string.Empty;
        }

        private void MostrarFicha()
        {
            lblPlaceholder.Visible = false;
            pnlCrear.Visible = false;
            pnlFicha.Visible = true;
        }

        private async void BtnGuardar_Click(object? sender, EventArgs e)
        {
            if (_seleccionado is null) return;

            var dto = new ActualizarConductorPresentacionDto
            {
                Nombre = _txtNombre.Text.Trim(),
                Apellido = _txtApellido.Text.Trim(),
                Correo = _txtCorreo.Text.Trim(),
                Telefono = _txtTelefono.Text.Trim(),
                NumeroLicencia = _txtLicencia.Text.Trim(),
                FechaVencimientoLicencia = _dtpVencimiento.Value
            };

            var resultado = await SgaApiClient.PutAsync<ConductorPresentacionDto>(
                $"api/usuarios/conductores/{_seleccionado.Id}", dto);

            CampoBuilder.MostrarMensaje(_lblMensajeDetalle,
                resultado.EsExitoso ? "Cambios guardados." : resultado.Error!, !resultado.EsExitoso);

            if (resultado.EsExitoso) await CargarListaAsync();
        }

        private async void BtnAplicarDisponibilidad_Click(object? sender, EventArgs e)
        {
            if (_seleccionado is null || _cmbDisponible.SelectedIndex < 0) return;

            var dto = new CambiarDisponibilidadPresentacionDto { Disponible = _cmbDisponible.SelectedIndex == 0 };
            var resultado = await SgaApiClient.PatchAsync<ConductorPresentacionDto>(
                $"api/usuarios/conductores/{_seleccionado.Id}/disponibilidad", dto);

            CampoBuilder.MostrarMensaje(_lblMensajeDetalle,
                resultado.EsExitoso ? "Disponibilidad actualizada." : resultado.Error!, !resultado.EsExitoso);

            if (resultado.EsExitoso) await CargarListaAsync();
        }

        private async void BtnEliminar_Click(object? sender, EventArgs e)
        {
            if (_seleccionado is null) return;

            var confirmar = MessageBox.Show(
                $"¿Dar de baja al conductor {_seleccionado.Nombre} {_seleccionado.Apellido}?",
                "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirmar != DialogResult.Yes) return;

            var dto = new EliminarPresentacionDto
            {
                Motivo = "Dado de baja desde el módulo de Conductores",
                EliminadoPor = SesionActual.Usuario?.Correo
            };

            var resultado = await SgaApiClient.DeleteAsync<object>($"api/usuarios/{_seleccionado.Id}", dto);
            CampoBuilder.MostrarMensaje(_lblMensajeDetalle,
                resultado.EsExitoso ? "Conductor dado de baja." : resultado.Error!, !resultado.EsExitoso);

            if (resultado.EsExitoso)
            {
                _seleccionado = null;
                pnlFicha.Visible = false;
                lblPlaceholder.Visible = true;
                await CargarListaAsync();
            }
        }

        // === Crear ===

        private void BtnNuevo_Click(object? sender, EventArgs e)
        {
            lblPlaceholder.Visible = false;
            pnlFicha.Visible = false;
            pnlCrear.Visible = true;

            _txtNNombre.Clear(); _txtNApellido.Clear(); _txtNCorreo.Clear();
            _txtNTelefono.Clear(); _txtNPassword.Clear(); _txtNLicencia.Clear();
            _lblMensajeCrear.Text = string.Empty;
        }

        private void BtnCancelarCrear_Click(object? sender, EventArgs e)
        {
            pnlCrear.Visible = false;
            if (_seleccionado is not null) pnlFicha.Visible = true;
            else lblPlaceholder.Visible = true;
        }

        private async void BtnRegistrar_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_txtNNombre.Text) || string.IsNullOrWhiteSpace(_txtNCorreo.Text))
            {
                CampoBuilder.MostrarMensaje(_lblMensajeCrear, "Nombre y correo son obligatorios.", true);
                return;
            }

            var dto = new CrearConductorPresentacionDto
            {
                Nombre = _txtNNombre.Text.Trim(),
                Apellido = _txtNApellido.Text.Trim(),
                Correo = _txtNCorreo.Text.Trim(),
                Telefono = _txtNTelefono.Text.Trim(),
                PasswordHash = _txtNPassword.Text,
                NumeroLicencia = _txtNLicencia.Text.Trim(),
                FechaVencimientoLicencia = _dtpNVencimiento.Value,
                CreadoPor = SesionActual.Usuario?.Correo
            };

            var resultado = await SgaApiClient.PostAsync<ConductorPresentacionDto>("api/usuarios/conductores", dto);

            CampoBuilder.MostrarMensaje(_lblMensajeCrear,
                resultado.EsExitoso ? "Conductor registrado correctamente." : resultado.Error!, !resultado.EsExitoso);

            if (resultado.EsExitoso)
            {
                _txtNNombre.Clear(); _txtNApellido.Clear(); _txtNCorreo.Clear();
                _txtNTelefono.Clear(); _txtNPassword.Clear(); _txtNLicencia.Clear();
                await CargarListaAsync();
            }
        }
    }
}