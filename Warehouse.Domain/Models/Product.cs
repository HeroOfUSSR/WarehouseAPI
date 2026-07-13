using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse.Domain.Models
{
    public class Product
    {
        public Guid Id { get; private set; }
        public string Sku { get; private set; }
        public string Name { get; private set; }
        public string? Description { get; private set; }
        public UnitOfMeasure Unit { get; private set; }
        public bool IsActive { get; private set; }

        private Product() { } 

        public Product(string sku, string name, UnitOfMeasure unit, string? description = null)
        {
            if (string.IsNullOrWhiteSpace(sku))
                throw new ArgumentException("Sku не может быть пустым", nameof(sku));

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name не может быть пустым", nameof(name));

            Id = Guid.NewGuid();
            Sku = sku;
            Name = name;
            Unit = unit;
            Description = description;
            IsActive = true;
        }

        public void UpdateDetails(string name, string? description)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name не может быть пустым", nameof(name));

            Name = name;
            Description = description;
        }

        public void Deactivate() => IsActive = false;
        public void Activate() => IsActive = true;
    }

    public enum UnitOfMeasure
    {
        Piece,
        Kg,
        Liter,
        Box
    }
}
