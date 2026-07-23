using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse.Domain.Exceptions
{
    public abstract class UnauthorizedDomainException : DomainException
    {
        protected UnauthorizedDomainException(string message) : base(message) { }
    }
}
