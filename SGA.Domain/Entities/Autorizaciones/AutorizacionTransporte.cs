using SGA.Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace SGA.Domain.Entities.Autorizaciones
{
    public class AutorizacionTransporte : BaseEntity
    {
        public int UsuarioTransporteId {  get; set; }
        public DateTime FechaEmision {  get; set; }
        public string Estado { get; set; } = "Activa";
    }
}
