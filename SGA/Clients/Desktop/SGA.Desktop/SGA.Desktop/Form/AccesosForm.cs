using SGA.Desktop.Api;
using SGA.Desktop.DTOs.Acceso;
using SGA.Desktop.UI;

namespace SGA.Desktop.Forms
{
    public partial class AccesosForm : Form
    {
        private List<AccesoPresentacionDto> _accesos = new();

        public AccesosForm()
        {
            InitializeComponent();
            ConstruirFormulario();
            _dtpDesde.Value = DateTime.Today.AddDays(-7);
            _dtpHasta.Value = DateTime.Today;
            _cmbResultado.SelectedIndex = 0;
            Load += async (_, _) => await BuscarPorPeriodoAsync();
        }

        private async void BtnBuscar_Click(object sender, EventArgs e) => await BuscarPorPeriodoAsync();

        private async Task BuscarPorPeriodoAsync()
        {
            var desde = _dtpDesde.Value.Date.ToString("yyyy-MM-dd");
            var hasta = _dtpHasta.Value.Date.ToString("yyyy-MM-dd");

            var resultado = await SgaApiClient.GetAsync<List<AccesoPresentacionDto>>(
                $"api/accesos/por-periodo?desde={desde}&hasta={hasta}");

            if (!resultado.EsExitoso)
            {
                CampoBuilder.MostrarMensaje(_lblMensaje, resultado.Error!, true);
                return;
            }

            _accesos = resultado.Valor!;
            AplicarFiltroResultado();
        }

        private void CmbResultado_SelectedIndexChanged(object sender, EventArgs e) => AplicarFiltroResultado();

        private void AplicarFiltroResultado()
        {
            IEnumerable<AccesoPresentacionDto> vista = _accesos;

            var filtro = _cmbResultado.SelectedItem?.ToString();
            if (filtro == "Permitido")
                vista = vista.Where(a => a.ResultadoAcceso == 1);
            else if (filtro == "Rechazado")
                vista = vista.Where(a => a.ResultadoAcceso != 1);

            var lista = vista.ToList();

            _dgv.DataSource = null;
            _dgv.DataSource = lista.Select(a => new
            {
                Usuario = $"Usuario #{a.UsuarioTransporteId}",
                Viaje = $"#{a.ViajeId}",
                Resultado = DescribirResultado(a.ResultadoAcceso),
                Motivo = string.IsNullOrWhiteSpace(a.MotivoRechazo) ? "—" : a.MotivoRechazo,
                Fecha = a.FechaHora.ToString("dd/MM/yyyy hh:mm tt")
            }).ToList();

            lblKpiTotalValor.Text = _accesos.Count.ToString();
            lblKpiPermitidosValor.Text = _accesos.Count(a => a.ResultadoAcceso == 1).ToString();
            lblKpiRechazadosValor.Text = _accesos.Count(a => a.ResultadoAcceso != 1).ToString();

            CampoBuilder.MostrarMensaje(_lblMensaje, $"{lista.Count} registro(s) encontrado(s).", false);
        }

        private static string DescribirResultado(int resultado) => resultado switch
        {
            1 => "Permitido",
            2 => "Denegado",
            3 => "Autorización vencida",
            4 => "Sin cupo",
            5 => "Usuario inactivo",
            6 => "Saldo insuficiente",
            7 => "Sin autorización",
            8 => "Viaje no disponible",
            9 => "Autorización inválida",
            _ => "Desconocido"
        };

        private void Dgv_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (_dgv.Columns[e.ColumnIndex].Name != "Resultado") return;
            var texto = e.Value?.ToString();

            e.CellStyle!.BackColor = texto == "Permitido"
                ? System.Drawing.Color.FromArgb(234, 243, 222)
                : System.Drawing.Color.FromArgb(252, 235, 235);
            e.CellStyle.ForeColor = texto == "Permitido"
                ? System.Drawing.Color.FromArgb(59, 109, 17)
                : System.Drawing.Color.FromArgb(163, 45, 45);
        }
    }
}