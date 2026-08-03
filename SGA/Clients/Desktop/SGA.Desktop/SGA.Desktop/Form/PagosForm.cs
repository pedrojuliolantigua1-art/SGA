using SGA.Desktop.Api;
using SGA.Desktop.DTOs.Autorizacion;
using SGA.Desktop.DTOs.Pago;
using SGA.Desktop.Services;
using SGA.Desktop.UI;

namespace SGA.Desktop.Forms
{
    public partial class PagosForm : Form
    {
        private int _usuarioIdResuelto;
        private string _nombreResuelto = string.Empty;
        private string _tipoUsuarioResuelto = string.Empty;
        private int? _tarjetaIdActual;
        private string? _numeroTarjetaActual;

        public PagosForm()
        {
            InitializeComponent();
            ConstruirFormulario();
        }

        // ===== BÚSQUEDA ÚNICA (matrícula o correo) =====

        private async void BtnBuscar_Click(object sender, EventArgs e)
        {
            var texto = _txtBuscar.Text.Trim();
            if (string.IsNullOrWhiteSpace(texto))
            {
                LimpiarResultado("Ingresa una matrícula o un correo institucional.");
                return;
            }

            var esCorreo = texto.Contains('@');

            if (!esCorreo)
            {
                // La búsqueda por matrícula SOLO existe para estudiantes — no depende de TipoUsuario.
                var resultadoEstudiante = await SgaApiClient.GetAsync<UsuarioBusquedaApi>(
                    $"api/usuarios/estudiantes/por-matricula?matricula={Uri.EscapeDataString(texto)}");

                if (!resultadoEstudiante.EsExitoso || resultadoEstudiante.Valor is null)
                {
                    LimpiarResultado("No se encontró ningún estudiante con esa matrícula.");
                    return;
                }

                MostrarResultado(resultadoEstudiante.Valor, "Estudiante");
                return;
            }

            // Búsqueda por correo: aquí SÍ confiamos en TipoUsuario, porque puede ser estudiante o empleado.
            var resultado = await SgaApiClient.GetAsync<UsuarioBusquedaApi>(
                $"api/usuarios/por-correo?correo={Uri.EscapeDataString(texto)}");

            if (!resultado.EsExitoso || resultado.Valor is null)
            {
                LimpiarResultado("No se encontró ningún usuario con ese correo.");
                return;
            }

            MostrarResultado(resultado.Valor, resultado.Valor.TipoUsuario ?? string.Empty);
        }

        private async void MostrarResultado(UsuarioBusquedaApi usuario, string tipoUsuario)
        {
            _usuarioIdResuelto = usuario.Id;
            _nombreResuelto = $"{usuario.Nombre} {usuario.Apellido}".Trim();
            _tipoUsuarioResuelto = tipoUsuario;

            var esEstudiante = _tipoUsuarioResuelto.Equals("Estudiante", StringComparison.OrdinalIgnoreCase);

            CampoBuilder.MostrarMensaje(_lblResultado,
                $"✔ {_nombreResuelto} — {DescribirTipo(_tipoUsuarioResuelto)}", false);

            pnlBilletera.Visible = esEstudiante;
            pnlPermiso.Visible = !esEstudiante;

            if (esEstudiante)
                await CargarBilleteraAsync();

            await CargarHistorialAsync();
        }

        private void LimpiarResultado(string mensaje)
        {
            _usuarioIdResuelto = 0;
            _tarjetaIdActual = null;
            _numeroTarjetaActual = null;
            pnlBilletera.Visible = false;
            pnlPermiso.Visible = false;
            CampoBuilder.MostrarMensaje(_lblResultado, mensaje, true);
        }

        private static string DescribirTipo(string tipo) => tipo switch
        {
            "Estudiante" => "Estudiante",
            "EmpleadoDocente" => "Empleado docente",
            "EmpleadoAdministrativo" => "Empleado administrativo",
            _ => tipo
        };

        // ===== BILLETERA (estudiante) =====

        private async Task CargarBilleteraAsync()
        {
            _tarjetaIdActual = null;
            _numeroTarjetaActual = null;

            var resumen = await SgaApiClient.GetAsync<AutorizacionResumenPresentacionDto>(
                $"api/autorizaciones/por-usuario/{_usuarioIdResuelto}");

            if (resumen.EsExitoso && resumen.Valor is not null && resumen.Valor.TipoAutorizacion == "TarjetaRecargable")
            {
                var detalle = await SgaApiClient.GetAsync<TarjetaRecargablePresentacionDto>(
                    $"api/autorizaciones/{resumen.Valor.Id}");

                if (detalle.EsExitoso && detalle.Valor is not null)
                {
                    _tarjetaIdActual = detalle.Valor.Id;
                    _numeroTarjetaActual = detalle.Valor.NumeroTarjeta;

                    _lblSaldoActual.Text = $"Tarjeta N° {detalle.Valor.NumeroTarjeta}  —  Saldo: RD$ {detalle.Valor.SaldoDisponible:N2}";
                    btnRecargar.Text = $"Recargar RD$ {_numMonto.Value:N2}";
                    _numMonto.ValueChanged += ActualizarTextoBoton;
                    ActualizarTextoBoton(this, EventArgs.Empty);
                    return;
                }
            }

            _lblSaldoActual.Text = "Este estudiante todavía no tiene billetera.";
            ActualizarTextoBoton(this, EventArgs.Empty);
        }

        private void ActualizarTextoBoton(object? sender, EventArgs e)
        {
            btnRecargar.Text = _tarjetaIdActual is null
                ? $"Crear billetera y cargar RD$ {_numMonto.Value:N2}"
                : $"Recargar RD$ {_numMonto.Value:N2}";
        }

        private async void BtnRecargar_Click(object sender, EventArgs e)
        {
            if (_usuarioIdResuelto == 0)
            {
                CampoBuilder.MostrarMensaje(_lblMensajeBilletera, "Busca primero al estudiante.", true);
                return;
            }
            if (_numMonto.Value <= 0)
            {
                CampoBuilder.MostrarMensaje(_lblMensajeBilletera, "Ingresa el monto recibido en efectivo.", true);
                return;
            }

            var dto = new RecargarBilleteraPresentacionDto
            {
                UsuarioTransporteId = _usuarioIdResuelto,
                Monto = _numMonto.Value,
                RegistradoPorUsuarioId = SesionActual.Usuario?.Id ?? 0,
                CreadoPor = SesionActual.Usuario?.Correo
            };

            var resultado = await SgaApiClient.PostAsync<BilleteraPresentacionDto>("api/autorizaciones/billetera", dto);

            if (!resultado.EsExitoso || resultado.Valor is null)
            {
                CampoBuilder.MostrarMensaje(_lblMensajeBilletera, resultado.Error!, true);
                return;
            }

            var v = resultado.Valor;
            CampoBuilder.MostrarMensaje(_lblMensajeBilletera,
                v.FueCreada
                    ? $"Billetera creada — tarjeta N° {v.NumeroTarjeta}, saldo RD$ {v.SaldoDisponible:N2}."
                    : $"Recarga aplicada — nuevo saldo RD$ {v.SaldoDisponible:N2}.",
                false);

            _numMonto.Value = 0;
            await CargarBilleteraAsync();
            await CargarHistorialAsync();
        }

        // ===== PERMISO ESPECIAL (empleado) =====

        private async void BtnEmitirPermiso_Click(object sender, EventArgs e)
        {
            if (_usuarioIdResuelto == 0)
            {
                CampoBuilder.MostrarMensaje(_lblMensajePermiso, "Busca primero al empleado.", true);
                return;
            }
            if (string.IsNullOrWhiteSpace(_txtCondicion.Text))
            {
                CampoBuilder.MostrarMensaje(_lblMensajePermiso, "Describe la condición institucional.", true);
                return;
            }

            var dto = new CrearPermisoPresentacionDto
            {
                UsuarioTransporteId = _usuarioIdResuelto,
                CondicionInstitucional = _txtCondicion.Text.Trim(),
                FechaVencimiento = _chkSinVencimiento.Checked ? null : _dtpVencimiento.Value.Date,
                CreadoPor = SesionActual.Usuario?.Correo
            };

            var resultado = await SgaApiClient.PostAsync<PermisoPresentacionDto>("api/autorizaciones/permiso", dto);
            CampoBuilder.MostrarMensaje(_lblMensajePermiso,
                resultado.EsExitoso ? "Permiso institucional emitido correctamente." : resultado.Error!,
                !resultado.EsExitoso);

            if (resultado.EsExitoso)
                await CargarHistorialAsync();
        }

        // ===== HISTORIAL (pagos, sirve para estudiante y empleado) =====

        private async Task CargarHistorialAsync()
        {
            if (_usuarioIdResuelto == 0) return;

            var resultado = await SgaApiClient.GetAsync<List<PagoPresentacionDto>>($"api/pagos/por-usuario/{_usuarioIdResuelto}");
            if (!resultado.EsExitoso)
            {
                CampoBuilder.MostrarMensaje(_lblMensajeHistorial, resultado.Error!, true);
                return;
            }

            var vista = resultado.Valor!.Select(p => new
            {
                Usuario = _nombreResuelto,
                Monto = "RD$ " + p.Monto.ToString("N2", System.Globalization.CultureInfo.InvariantCulture),
                p.TipoPago,
                Estado = DescribirEstadoPago(p.Estado),
                Fecha = p.FechaHora.ToString("dd/MM/yyyy hh:mm tt")
            }).ToList();

            _dgvHistorial.DataSource = null;
            _dgvHistorial.DataSource = vista;
            lblHistorialTitulo.Text = $"Historial de pagos — {_nombreResuelto}";
            CampoBuilder.MostrarMensaje(_lblMensajeHistorial, $"{resultado.Valor!.Count} pago(s) de {_nombreResuelto}.", false);
        }

        private static string DescribirEstadoPago(int estado) => estado switch
        {
            1 => "Registrado",
            2 => "Aplicado",
            3 => "Anulado",
            _ => "Desconocido"
        };

        private sealed class UsuarioBusquedaApi
        {
            public int Id { get; set; }
            public string? Nombre { get; set; }
            public string? Apellido { get; set; }
            public string? TipoUsuario { get; set; }
        }
    }
}