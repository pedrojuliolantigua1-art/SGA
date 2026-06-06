using SGA.Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace SGA.Domain.Entities.Auditoria
{
    internal class RegistroAuditoria : BaseEntity
    {
        public int UsuarioTransporteId { get; set; }
        public string? Accion {  get; set; }
        public string? EntidadAfectada { get; set; }
        public string? EntidadId {  get; set; }
        public string Detalle { get; set; }
        public DateTime FechaHora { get; set; }
    }
}
