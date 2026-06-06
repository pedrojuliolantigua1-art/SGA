using System;
using System.Collections.Generic;
using System.Text;

namespace SGA.Domain.Entities.Autorizaciones
{
    public class TicketMensual : AutorizacionTransporte
    {
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFinal {  get; set; }
    }
}
