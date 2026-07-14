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
    public class ProductRepository : IProductRepository
    {
        private readonly WarehouseDbContext _context;

        public ProductRepository(WarehouseDbContext context) => _context = context;

        public Task<Product?> GetByIdAsync(Guid id, CancellationToken ct)
            => _context.Products.FirstOrDefaultAsync(p => p.Id == id, ct);

        public Task<List<Product>> GetAllAsync(CancellationToken ct)
            => _context.Products.ToListAsync(ct);

        public Task<Product?> GetBySkuAsync(string sku, CancellationToken ct)
            => _context.Products.FirstOrDefaultAsync(p => p.Sku == sku, ct);

        public async Task AddAsync(Product product, CancellationToken ct)
            => await _context.Products.AddAsync(product, ct);

        public void Remove(Product product)
            => _context.Products.Remove(product);
    }
}
