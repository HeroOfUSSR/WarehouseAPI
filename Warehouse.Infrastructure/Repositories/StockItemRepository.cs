using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Application.Interfaces;
using Warehouse.Domain.Models;

namespace Warehouse.Infrastructure.Repositories
{
    public class StockItemRepository : IStockItemRepository
    {
        private readonly WarehouseDbContext _context;

        public StockItemRepository(WarehouseDbContext context) => _context = context;

        public Task<StockItem?> GetAsync(Guid productId, Guid storehouseId, CancellationToken ct)
            => _context.StockItems.FirstOrDefaultAsync(
                s => s.ProductId == productId && s.StorehouseId == storehouseId, ct);

        public async Task<StockItem> GetOrCreateAsync(Guid productId, Guid storehouseId, CancellationToken ct)
        {
            var existing = await GetAsync(productId, storehouseId, ct);
            if (existing is not null)
                return existing;

            var created = new StockItem(productId, storehouseId);
            await _context.StockItems.AddAsync(created, ct);
            return created;
        }

        public Task<List<StockItem>> GetByStorehouseAsync(Guid storehouseId, CancellationToken ct)
            => _context.StockItems.Where(s => s.StorehouseId == storehouseId).ToListAsync(ct);

        public Task<List<StockItem>> GetByProductAsync(Guid productId, CancellationToken ct)
            => _context.StockItems.Where(s => s.ProductId == productId).ToListAsync(ct);

        public Task<List<StockItem>> GetBelowMinimumAsync(CancellationToken ct)
            => _context.StockItems.Where(s => s.Quantity < s.MinQuantity).ToListAsync(ct);
    }
}
