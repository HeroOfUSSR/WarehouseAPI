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
    public class StorehouseService
    {
        private readonly IStorehouseRepository _storehouseRepository;
        private readonly IUnitOfWork _unitOfWork;

        public StorehouseService(IStorehouseRepository storehouseRepository, IUnitOfWork unitOfWork)
        {
            _storehouseRepository = storehouseRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> CreateAsync(string name, string? address, CancellationToken ct)
        {
            var storehouse = new Storehouse(name, address);
            await _storehouseRepository.AddAsync(storehouse, ct);
            await _unitOfWork.SaveChangesAsync(ct);

            return storehouse.Id;
        }

        public async Task<Storehouse> GetByIdAsync(Guid id, CancellationToken ct)
            => await _storehouseRepository.GetByIdAsync(id, ct)
                ?? throw new StorehouseNotFoundException(id);

        public Task<List<Storehouse>> GetAllAsync(CancellationToken ct)
            => _storehouseRepository.GetAllAsync(ct);

        public async Task UpdateAsync(Guid id, string name, string? address, CancellationToken ct)
        {
            var storehouse = await GetByIdAsync(id, ct);
            storehouse.UpdateDetails(name, address);
            await _unitOfWork.SaveChangesAsync(ct);
        }

        public async Task DeactivateAsync(Guid id, CancellationToken ct)
        {
            var storehouse = await GetByIdAsync(id, ct);
            storehouse.Deactivate();
            await _unitOfWork.SaveChangesAsync(ct);
        }
    }
}
