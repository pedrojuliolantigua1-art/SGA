using System;
using System.Collections.Generic;
using System.Text;

namespace SGA.Domain.Base
{
    public abstract class BaseEntity : Auditable
    {
        public int Id { get; set; }
    }
}
