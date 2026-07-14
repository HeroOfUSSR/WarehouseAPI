using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse.Domain.Exceptions
{
    public class StockItemNotFoundException : Exception
    {
        public Guid ProductId { get; }
        public Guid StorehouseId { get; }

        public StockItemNotFoundException(Guid productId, Guid storehouseId)
            : base($"Остаток для товара {productId} на складе {storehouseId} не найден")
        {
            ProductId = productId;
            StorehouseId = storehouseId;
        }
    }
}
