using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Domain.Exceptions;

namespace Warehouse.Domain.Models
{
    public class StockItem
    {
        public Guid Id { get; private set; }
        public Guid ProductId { get; private set; }
        public Guid StorehouseId { get; private set; }
        public int Quantity { get; private set; }
        public int MinQuantity { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        private StockItem() { }

        public StockItem(Guid productId, Guid storehouseId, int initialQuantity = 0, int minQuantity = 0)
        {
            if (initialQuantity < 0)
                throw new ArgumentException("Начальное количество не может быть отрицательным", nameof(initialQuantity));

            if (minQuantity < 0)
                throw new ArgumentException("Минимальное количество не может быть отрицательным", nameof(minQuantity));

            Id = Guid.NewGuid();
            ProductId = productId;
            StorehouseId = storehouseId;
            Quantity = initialQuantity;
            MinQuantity = minQuantity;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Increase(int amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Количество для увеличения должно быть положительным", nameof(amount));

            Quantity += amount;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Decrease(int amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Количество для списания должно быть положительным", nameof(amount));

            if (amount > Quantity)
                throw new InsufficientStockException(ProductId, StorehouseId, Quantity, amount);

            Quantity -= amount;
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetMinQuantity(int minQuantity)
        {
            if (minQuantity < 0)
                throw new ArgumentException("Минимальное количество не может быть отрицательным", nameof(minQuantity));

            MinQuantity = minQuantity;
        }

        public bool IsBelowMinimum => Quantity < MinQuantity;
    }
}
