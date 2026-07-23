using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse.Domain.Exceptions
{
    public class DuplicateSkuException : ConflictException
    {
        public DuplicateSkuException(string sku) : base($"Товар с артикулом '{sku}' уже существует") { }
    }
}
