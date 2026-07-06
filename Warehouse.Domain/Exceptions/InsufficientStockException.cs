using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse.Domain.Exceptions
{
    public class InsufficientStockException : Exception
    {
        public Guid ProductId { get; }
        public Guid WarehouseId { get; }
        public int Available { get; }
        public int Requested { get; }

        public InsufficientStockException(Guid productId, Guid warehouseId, int available, int requested)
            : base($"Недостаточно товара на складе. Доступно: {available}, запрошено: {requested}")
        {
            ProductId = productId;
            WarehouseId = warehouseId;
            Available = available;
            Requested = requested;
        }
    }
}
