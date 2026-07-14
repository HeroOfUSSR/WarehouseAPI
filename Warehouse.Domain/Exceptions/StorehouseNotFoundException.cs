using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse.Domain.Exceptions
{
    public class StorehouseNotFoundException : Exception
    {
        public StorehouseNotFoundException(Guid id) : base($"Склад {id} не найден") { }
    }
}
