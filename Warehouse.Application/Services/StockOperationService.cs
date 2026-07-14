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
    public class StockOperationService
    {
        private readonly IStockItemRepository _stockItemRepository;
        private readonly IMovementRepository _movementRepository;
        private readonly IUnitOfWork _unitOfWork;

        public StockOperationService(
            IStockItemRepository stockItemRepository,
            IMovementRepository movementRepository,
            IUnitOfWork unitOfWork)
        {
            _stockItemRepository = stockItemRepository;
            _movementRepository = movementRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> ReceiveAsync(
            Guid productId, int quantity, Guid destinationStorehouseId, Guid userId, string? comment, CancellationToken ct)
        {
            var stockItem = await _stockItemRepository.GetOrCreateAsync(productId, destinationStorehouseId, ct);
            stockItem.Increase(quantity);

            var movement = Movement.CreateReceipt(productId, quantity, destinationStorehouseId, userId, comment);
            await _movementRepository.AddAsync(movement, ct);

            await _unitOfWork.SaveChangesAsync(ct);
            return movement.Id;
        }

        public async Task<Guid> ShipAsync(
            Guid productId, int quantity, Guid sourceStorehouseId, Guid userId, string? comment, CancellationToken ct)
        {
            var stockItem = await _stockItemRepository.GetAsync(productId, sourceStorehouseId, ct)
                ?? throw new StockItemNotFoundException(productId, sourceStorehouseId);

            stockItem.Decrease(quantity); // выбросит InsufficientStockException при нехватке

            var movement = Movement.CreateShipment(productId, quantity, sourceStorehouseId, userId, comment);
            await _movementRepository.AddAsync(movement, ct);

            await _unitOfWork.SaveChangesAsync(ct);
            return movement.Id;
        }

        public async Task<Guid> TransferAsync(
            Guid productId, int quantity, Guid sourceStorehouseId, Guid destinationStorehouseId,
            Guid userId, string? comment, CancellationToken ct)
        {
            var sourceStock = await _stockItemRepository.GetAsync(productId, sourceStorehouseId, ct)
                ?? throw new StockItemNotFoundException(productId, sourceStorehouseId);

            var destStock = await _stockItemRepository.GetOrCreateAsync(productId, destinationStorehouseId, ct);

            sourceStock.Decrease(quantity);
            destStock.Increase(quantity);

            var movement = Movement.CreateTransfer(productId, quantity, sourceStorehouseId, destinationStorehouseId, userId, comment);
            await _movementRepository.AddAsync(movement, ct);

            await _unitOfWork.SaveChangesAsync(ct);
            return movement.Id;
        }

        public async Task<Guid> AdjustAsync(
            Guid productId, int quantity, Guid storehouseId, bool isIncrease, Guid userId, string? comment, CancellationToken ct)
        {
            var stockItem = await _stockItemRepository.GetOrCreateAsync(productId, storehouseId, ct);

            if (isIncrease)
                stockItem.Increase(quantity);
            else
                stockItem.Decrease(quantity);

            var movement = Movement.CreateAdjustment(productId, quantity, storehouseId, isIncrease, userId, comment);
            await _movementRepository.AddAsync(movement, ct);

            await _unitOfWork.SaveChangesAsync(ct);
            return movement.Id;
        }
    }
}
