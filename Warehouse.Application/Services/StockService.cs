using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Application.Interfaces;
using Warehouse.Domain.Models;

namespace Warehouse.Application.Services
{
    public class StockService
    {
        private readonly IStockItemRepository _stockItemRepository;

        public StockService(IStockItemRepository stockItemRepository)
            => _stockItemRepository = stockItemRepository;

        public Task<List<StockItem>> GetByStorehouseAsync(Guid storehouseId, CancellationToken ct)
            => _stockItemRepository.GetByStorehouseAsync(storehouseId, ct);

        public Task<List<StockItem>> GetByProductAsync(Guid productId, CancellationToken ct)
            => _stockItemRepository.GetByProductAsync(productId, ct);

        public Task<List<StockItem>> GetBelowMinimumAsync(CancellationToken ct)
            => _stockItemRepository.GetBelowMinimumAsync(ct);
    }
}
