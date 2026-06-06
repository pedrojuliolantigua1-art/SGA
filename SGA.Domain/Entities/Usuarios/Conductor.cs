using System;
using System.Collections.Generic;
using System.Text;

namespace SGA.Domain.Entities.Usuarios
{
    public class Conductor: UsuarioTransporte
    {
        public string? NumeroLicencia { get; set; }
        public bool Disponible { get; set; }
    }
}
