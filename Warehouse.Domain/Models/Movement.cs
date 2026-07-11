using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse.Domain.Models
{
    public class Movement
    {
        public Guid Id { get; private set; }
        public Guid ProductId { get; private set; }
        public int Quantity { get; private set; }
        public MovementType Type { get; private set; }
        public Guid? SourceStorehouseId { get; private set; }
        public Guid? DestinationStorehouseId { get; private set; }
        public Guid CreatedByUserId { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public string? Comment { get; private set; }

        private Movement() { }

        private Movement(
            Guid productId,
            int quantity,
            MovementType type,
            Guid? sourceStorehouseId,
            Guid? destinationStorehouseId,
            Guid createdByUserId,
            string? comment)
        {
            if (quantity <= 0)
                throw new ArgumentException("Количество должно быть положительным", nameof(quantity));

            Id = Guid.NewGuid();
            ProductId = productId;
            Quantity = quantity;
            Type = type;
            SourceStorehouseId = sourceStorehouseId;
            DestinationStorehouseId = destinationStorehouseId;
            CreatedByUserId = createdByUserId;
            CreatedAt = DateTime.UtcNow;
            Comment = comment;
        }

        public static Movement CreateReceipt(
            Guid productId, int quantity, Guid destinationStorehouseId, Guid createdByUserId, string? comment = null)
            => new(productId, quantity, MovementType.Receipt, null, destinationStorehouseId, createdByUserId, comment);

        public static Movement CreateShipment(
            Guid productId, int quantity, Guid sourceStorehouseId, Guid createdByUserId, string? comment = null)
            => new(productId, quantity, MovementType.Shipment, sourceStorehouseId, null, createdByUserId, comment);

        public static Movement CreateTransfer(
            Guid productId, int quantity, Guid sourceStorehouseId, Guid destinationStorehouseId,
            Guid createdByUserId, string? comment = null)
        {
            if (sourceStorehouseId == destinationStorehouseId)
                throw new ArgumentException("Склад-источник и склад-назначение не могут совпадать");

            return new(productId, quantity, MovementType.Transfer, sourceStorehouseId, destinationStorehouseId, createdByUserId, comment);
        }

        public static Movement CreateAdjustment(
            Guid productId, int quantity, Guid storehouseId, bool isIncrease, Guid createdByUserId, string? comment = null)
            => new(
                productId, quantity, MovementType.Adjustment,
                sourceStorehouseId: isIncrease ? null : storehouseId,
                destinationStorehouseId: isIncrease ? storehouseId : null,
                createdByUserId, comment);
    }

    public enum MovementType
    {
        Receipt,
        Shipment,
        Transfer,
        Adjustment
    }
}
