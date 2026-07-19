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
    public class MovementRepository : IMovementRepository
    {
        private readonly WarehouseDbContext _context;

        public MovementRepository(WarehouseDbContext context) => _context = context;

        public Task<Movement?> GetByIdAsync(Guid id, CancellationToken ct)
            => _context.Movements.FirstOrDefaultAsync(m => m.Id == id, ct);

        public async Task<List<Movement>> GetHistoryAsync(
            Guid? productId, Guid? storehouseId, DateTime? from, DateTime? to,
            int page, int pageSize, CancellationToken ct)
        {
            var query = _context.Movements.AsQueryable();

            if (productId.HasValue)
                query = query.Where(m => m.ProductId == productId);

            if (storehouseId.HasValue)
                query = query.Where(m =>
                    m.SourceStorehouseId == storehouseId || m.DestinationStorehouseId == storehouseId);

            if (from.HasValue)
            {
                var fromUtc = DateTime.SpecifyKind(from.Value, DateTimeKind.Utc);
                query = query.Where(m => m.CreatedAt >= fromUtc);
            }

            if (to.HasValue)
            {
                var toUtc = DateTime.SpecifyKind(to.Value, DateTimeKind.Utc);
                query = query.Where(m => m.CreatedAt <= toUtc);
            }

            return await query
                .OrderByDescending(m => m.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(ct);
        }

        public async Task AddAsync(Movement movement, CancellationToken ct)
            => await _context.Movements.AddAsync(movement, ct);
    }
}
