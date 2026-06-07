using System;
using System.Collections.Generic;
using System.Text;

namespace SGA.Domain.Entities.Usuarios
{
    public class Empleado : UsuarioTransporte
    {
        string codigoEmpleado { get; set; }
    }
}
