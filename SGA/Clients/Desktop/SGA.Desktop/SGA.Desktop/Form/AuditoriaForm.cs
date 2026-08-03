using SGA.Desktop.Api;
using SGA.Desktop.DTOs.Auditoria;
using SGA.Desktop.UI;

namespace SGA.Desktop.Forms
{
    public partial class AuditoriaForm : Form
    {
        public AuditoriaForm()
        {
            InitializeComponent();
            ConstruirFormulario();
            _dtpDesde.Value = DateTime.Today.AddDays(-30);
            _dtpHasta.Value = DateTime.Today;
            Load += async (_, _) => await BuscarPorPeriodoAsync();
        }

        private async void BtnPeriodo_Click(object sender, EventArgs e) => await BuscarPorPeriodoAsync();

        private async Task BuscarPorPeriodoAsync()
        {
            var desde = _dtpDesde.Value.Date.ToString("yyyy-MM-dd");
            var hasta = _dtpHasta.Value.Date.ToString("yyyy-MM-dd");

            var resultado = await SgaApiClient.GetAsync<List<AuditoriaPresentacionDto>>(
                $"api/auditoria/por-periodo?desde={desde}&hasta={hasta}");

            if (!resultado.EsExitoso)
            {
                CampoBuilder.MostrarMensaje(_lblMensaje, resultado.Error!, true);
                return;
            }

            var vista = resultado.Valor!.Select(a => new
            {
                Actor = string.IsNullOrWhiteSpace(a.UsuarioNombre) ? $"Usuario #{a.UsuarioTransporteId}" : a.UsuarioNombre,
                a.Accion,
                a.EntidadAfectada,
                a.Detalle,
                Fecha = a.FechaHora.ToString("dd/MM/yyyy hh:mm:ss tt")
            }).ToList();

            _dgv.DataSource = null;
            _dgv.DataSource = vista;
            CampoBuilder.MostrarMensaje(_lblMensaje, $"{resultado.Valor!.Count} registro(s) encontrado(s).", false);
        }
    }
}