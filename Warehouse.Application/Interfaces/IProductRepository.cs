using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Domain.Models;

namespace Warehouse.Application.Interfaces
{
    public interface IProductRepository
    {
        Task<Product?> GetByIdAsync(Guid id, CancellationToken ct);
        Task<List<Product>> GetAllAsync(CancellationToken ct);
        Task<Product?> GetBySkuAsync(string sku, CancellationToken ct);
        Task AddAsync(Product product, CancellationToken ct);
        void Remove(Product product);
    }
}
