using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Application.Interfaces;

namespace Warehouse.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly WarehouseDbContext _context;

        public UnitOfWork(WarehouseDbContext context)
        {
            _context = context;
        }

        public Task<int> SaveChangesAsync(CancellationToken ct)
            => _context.SaveChangesAsync(ct);
    }
}
