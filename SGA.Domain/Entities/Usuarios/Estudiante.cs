using System;
using System.Collections.Generic;
using System.Text;

namespace SGA.Domain.Entities.Usuarios
{
    public class Estudiante: UsuarioTransporte
    {
        string codigoInstitucional { get; set; }
    }
}
