using System.Net.Http;
using SGA.Desktop.Api;
using SGA.Desktop.DTOs.Autobus;
using SGA.Desktop.DTOs.FotoAutobus;
using SGA.Desktop.Services;
using SGA.Desktop.UI;

namespace SGA.Desktop.Forms
{
    public partial class CatalogoTransporteForm : Form
    {
        private List<AutobusPresentacionDto> _autobuses = new();
        private AutobusPresentacionDto? _seleccionado;

        private static readonly HttpClient _httpImagenes = new()
        {
            BaseAddress = new Uri("https://localhost:7168/")
        };

        public CatalogoTransporteForm()
        {
            InitializeComponent();
            ConstruirFormulario();
            Load += async (_, _) => await CargarListaAsync();
        }

        private async Task CargarListaAsync()
        {
            var resultado = await SgaApiClient.GetAsync<List<AutobusPresentacionDto>>("api/autobuses");
            if (!resultado.EsExitoso)
            {
                CampoBuilder.MostrarMensaje(lblMensajeFicha, resultado.Error!, true);
                return;
            }

            _autobuses = resultado.Valor!;
            dgv.DataSource = null;
            dgv.DataSource = _autobuses;
            if (dgv.Columns["Id"] is { } col) col.Visible = false;
        }

        private async void Dgv_SelectionChanged(object sender, EventArgs e)
        {
            if (dgv.CurrentRow?.DataBoundItem is not AutobusPresentacionDto seleccionado) return;

            _seleccionado = seleccionado;
            MostrarFicha();

            txtPlaca.Text = seleccionado.Placa;
            txtMarca.Text = seleccionado.Marca;
            txtModelo.Text = seleccionado.Modelo;
            numCapacidad.Value = Math.Max(1, seleccionado.Capacidad);
            cmbEstado.SelectedItem = seleccionado.Estado;

            lblFichaPlaca.Text = seleccionado.Placa;
            lblFichaSubtitulo.Text = $"{seleccionado.Marca} {seleccionado.Modelo} · {seleccionado.Capacidad} pasajeros";
            lblMensajeFicha.Text = string.Empty;

            await CargarFotoAsync(seleccionado.Id);
        }

        private async Task CargarFotoAsync(int autobusId)
        {
            picFoto.Image = null;

            var resultado = await SgaApiClient.GetAsync<List<FotoAutobusPresentacionDto>>($"api/fotosautobus/autobus/{autobusId}");
            if (!resultado.EsExitoso || resultado.Valor is null || resultado.Valor.Count == 0)
                return;

            var url = resultado.Valor.First().UrlPublica;
            try
            {
                var bytes = await _httpImagenes.GetByteArrayAsync(url);
                using var ms = new MemoryStream(bytes);
                picFoto.Image = Image.FromStream(ms);
            }
            catch
            {
                // si la imagen no carga, se deja el placeholder gris
            }
        }

        // === Ficha (ver/editar seleccionado) ===

        private void MostrarFicha()
        {
            lblPlaceholder.Visible = false;
            pnlCrear.Visible = false;
            pnlFicha.Visible = true;
        }

        private async void BtnSubirFoto_Click(object sender, EventArgs e)
        {
            if (_seleccionado is null) return;

            using var dialogo = new OpenFileDialog
            {
                Title = "Seleccionar foto del autobús",
                Filter = "Imágenes (*.jpg;*.jpeg;*.png;*.bmp;*.gif)|*.jpg;*.jpeg;*.png;*.bmp;*.gif",
                CheckFileExists = true,
                Multiselect = false
            };

            if (dialogo.ShowDialog() != DialogResult.OK) return;

            var ruta = dialogo.FileName;

            using var archivo = System.IO.File.OpenRead(ruta);
            using var contenido = new MultipartFormDataContent();

            contenido.Add(new StringContent(_seleccionado.Id.ToString()), "autobusId");

            var contenidoArchivo = new StreamContent(archivo);
            contenidoArchivo.Headers.ContentType =
                new System.Net.Http.Headers.MediaTypeHeaderValue(ObtenerContentType(Path.GetExtension(ruta)));
            contenido.Add(contenidoArchivo, "archivo", Path.GetFileName(ruta));

            var resultado = await SgaApiClient.PostContentAsync<FotoAutobusPresentacionDto>("api/fotosautobus", contenido);

            CampoBuilder.MostrarMensaje(lblMensajeFicha,
                resultado.EsExitoso ? "Foto subida correctamente." : resultado.Error!,
                !resultado.EsExitoso);

            if (resultado.EsExitoso)
                await CargarFotoAsync(_seleccionado.Id);
        }

        private static string ObtenerContentType(string extension)
        {
            switch (extension.ToLowerInvariant())
            {
                case ".png": return "image/png";
                case ".bmp": return "image/bmp";
                case ".gif": return "image/gif";
                case ".webp": return "image/webp";
                default: return "image/jpeg";
            }
        }

        private void BtnVerDetalle_Click(object sender, EventArgs e)
        {
            if (_seleccionado is null) return;

            var detalle = _seleccionado;
            var texto = $"Placa: {detalle.Placa}\r\n" +
                        $"Marca: {detalle.Marca}\r\n" +
                        $"Modelo: {detalle.Modelo}\r\n" +
                        $"Capacidad: {detalle.Capacidad} pasajeros\r\n" +
                        $"Estado: {detalle.Estado}";

            MessageBox.Show(texto, "Detalle del autobús", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private async void BtnGuardarCambios_Click(object sender, EventArgs e)
        {
            if (_seleccionado is null) return;

            var dto = new ActualizarAutobusPresentacionDto
            {
                Placa = txtPlaca.Text.Trim(),
                Marca = txtMarca.Text.Trim(),
                Modelo = txtModelo.Text.Trim(),
                Capacidad = (int)numCapacidad.Value
            };

            var resultado = await SgaApiClient.PutAsync<AutobusPresentacionDto>($"api/autobuses/{_seleccionado.Id}", dto);
            CampoBuilder.MostrarMensaje(lblMensajeFicha,
                resultado.EsExitoso ? "Cambios guardados." : resultado.Error!, !resultado.EsExitoso);

            if (resultado.EsExitoso) await CargarListaAsync();
        }

        private async void BtnCambiarEstado_Click(object sender, EventArgs e)
        {
            if (_seleccionado is null || cmbEstado.SelectedItem is null) return;

            var dto = new CambiarEstadoAutobusPresentacionDto { NuevoEstado = cmbEstado.SelectedItem.ToString()! };
            var resultado = await SgaApiClient.PatchAsync<AutobusPresentacionDto>($"api/autobuses/{_seleccionado.Id}/estado", dto);

            CampoBuilder.MostrarMensaje(lblMensajeFicha,
                resultado.EsExitoso ? "Estado actualizado." : resultado.Error!, !resultado.EsExitoso);

            if (resultado.EsExitoso) await CargarListaAsync();
        }

        private async void BtnEliminar_Click(object sender, EventArgs e)
        {
            if (_seleccionado is null) return;

            var confirmar = MessageBox.Show(
                $"¿Eliminar el autobús {_seleccionado.Placa}?", "Confirmar",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirmar != DialogResult.Yes) return;

            var dto = new EliminarAutobusPresentacionDto
            {
                Motivo = "Eliminado desde el módulo de Transporte",
                EliminadoPor = SesionActual.Usuario?.Correo
            };

            var resultado = await SgaApiClient.DeleteAsync<object>($"api/autobuses/{_seleccionado.Id}", dto);
            CampoBuilder.MostrarMensaje(lblMensajeFicha,
                resultado.EsExitoso ? "Autobús eliminado." : resultado.Error!, !resultado.EsExitoso);

            if (resultado.EsExitoso)
            {
                picFoto.Image = null;
                _seleccionado = null;
                pnlFicha.Visible = false;
                lblPlaceholder.Visible = true;
                await CargarListaAsync();
            }
        }

        // === Crear ===

        private void BtnNuevo_Click(object sender, EventArgs e)
        {
            lblPlaceholder.Visible = false;
            pnlFicha.Visible = false;
            pnlCrear.Visible = true;

            txtPlacaNueva.Clear();
            txtMarcaNueva.Clear();
            txtModeloNueva.Clear();
            numCapacidadNueva.Value = 1;
            lblMensajeCrear.Text = string.Empty;
        }

        private void BtnCancelarCrear_Click(object sender, EventArgs e)
        {
            pnlCrear.Visible = false;
            if (_seleccionado is not null) pnlFicha.Visible = true;
            else lblPlaceholder.Visible = true;
        }

        private async void BtnGuardarNuevo_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPlacaNueva.Text) ||
                string.IsNullOrWhiteSpace(txtMarcaNueva.Text) ||
                string.IsNullOrWhiteSpace(txtModeloNueva.Text))
            {
                CampoBuilder.MostrarMensaje(lblMensajeCrear, "Placa, marca y modelo son obligatorios.", true);
                return;
            }

            btnGuardarNuevo.Enabled = false;
            try
            {
                var dto = new CrearAutobusPresentacionDto
                {
                    Placa = txtPlacaNueva.Text.Trim(),
                    Marca = txtMarcaNueva.Text.Trim(),
                    Modelo = txtModeloNueva.Text.Trim(),
                    Capacidad = (int)numCapacidadNueva.Value,
                    CreadoPor = SesionActual.Usuario?.Correo
                };

                var resultado = await SgaApiClient.PostAsync<AutobusPresentacionDto>("api/autobuses", dto);
                CampoBuilder.MostrarMensaje(lblMensajeCrear,
                    resultado.EsExitoso ? $"Autobús {resultado.Valor!.Placa} registrado correctamente." : resultado.Error!,
                    !resultado.EsExitoso);

                if (resultado.EsExitoso)
                {
                    txtPlacaNueva.Clear(); txtMarcaNueva.Clear(); txtModeloNueva.Clear();
                    numCapacidadNueva.Value = 1;
                    await CargarListaAsync();
                }
            }
            finally
            {
                btnGuardarNuevo.Enabled = true;
            }
        }
    }
}