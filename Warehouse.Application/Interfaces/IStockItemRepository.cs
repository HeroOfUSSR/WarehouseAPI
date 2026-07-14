using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Domain.Models;

namespace Warehouse.Application.Interfaces
{
    public interface IStockItemRepository
    {
        Task<StockItem?> GetAsync(Guid productId, Guid storehouseId, CancellationToken ct);
        Task<StockItem> GetOrCreateAsync(Guid productId, Guid storehouseId, CancellationToken ct);
        Task<List<StockItem>> GetByStorehouseAsync(Guid storehouseId, CancellationToken ct);
        Task<List<StockItem>> GetByProductAsync(Guid productId, CancellationToken ct);
        Task<List<StockItem>> GetBelowMinimumAsync(CancellationToken ct);
    }
}
