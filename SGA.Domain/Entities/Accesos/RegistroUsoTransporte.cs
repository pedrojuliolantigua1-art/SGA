using SGA.Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace SGA.Domain.Entities.Accesos
{
    public class RegistroUsoTransporte : BaseEntity
    {
        public int UsuarioTransporteId { get; set; }
        public int ViajeId { get; set; }
        public int? AutorizacionTransporteId { get; set; }

        public string? ResultadoAcceso { get; set; }
        public string? MotivoRechazo { get; set; }
        public DateTime FechaHora {  get; set; }
        public int ValidadoPorUsuarioId { get; set; }
    }
}
