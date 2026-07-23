using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse.Domain.Exceptions
{
    public abstract class ConflictException : DomainException
    {
        protected ConflictException(string message) : base(message) { }
    }
}
