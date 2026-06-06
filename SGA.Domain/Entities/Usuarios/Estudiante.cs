using System;
using System.Collections.Generic;
using System.Text;

namespace SGA.Domain.Entities.Usuarios
{
    public class Estudiante: UsuarioTransporte
    {
        public string? CodigoEmpleado {  get; set; }
        public string? Departamente { get; set; }
        public string? Cargo { get; set; }
    }
}
