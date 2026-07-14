using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Application.Interfaces;
using Warehouse.Domain.Exceptions;
using Warehouse.Domain.Models;

namespace Warehouse.Application.Services
{
    public class ProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> CreateAsync(
            string sku, string name, UnitOfMeasure unit, string? description, CancellationToken ct)
        {
            var existing = await _productRepository.GetBySkuAsync(sku, ct);
            if (existing is not null)
                throw new DuplicateSkuException(sku);

            var product = new Product(sku, name, unit, description);
            await _productRepository.AddAsync(product, ct);
            await _unitOfWork.SaveChangesAsync(ct);

            return product.Id;
        }

        public async Task<Product> GetByIdAsync(Guid id, CancellationToken ct)
            => await _productRepository.GetByIdAsync(id, ct)
                ?? throw new ProductNotFoundException(id);

        public Task<List<Product>> GetAllAsync(CancellationToken ct)
            => _productRepository.GetAllAsync(ct);

        public async Task UpdateAsync(Guid id, string name, string? description, CancellationToken ct)
        {
            var product = await GetByIdAsync(id, ct);
            product.UpdateDetails(name, description);
            await _unitOfWork.SaveChangesAsync(ct);
        }

        public async Task DeactivateAsync(Guid id, CancellationToken ct)
        {
            var product = await GetByIdAsync(id, ct);
            product.Deactivate();
            await _unitOfWork.SaveChangesAsync(ct);
        }
    }
}
