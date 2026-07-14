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
    public class StorehouseRepository : IStorehouseRepository
    {
        private readonly WarehouseDbContext _context;

        public StorehouseRepository(WarehouseDbContext context) => _context = context;

        public Task<Storehouse?> GetByIdAsync(Guid id, CancellationToken ct)
            => _context.Storehouses.FirstOrDefaultAsync(s => s.Id == id, ct);

        public Task<List<Storehouse>> GetAllAsync(CancellationToken ct)
            => _context.Storehouses.ToListAsync(ct);

        public async Task AddAsync(Storehouse storehouse, CancellationToken ct)
            => await _context.Storehouses.AddAsync(storehouse, ct);

        public void Remove(Storehouse storehouse)
            => _context.Storehouses.Remove(storehouse);
    }
}
