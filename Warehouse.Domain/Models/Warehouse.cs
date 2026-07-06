using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse.Domain.Models
{
    public class Warehouse
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string? Address { get; private set; }
        public bool IsActive { get; private set; }

        private Warehouse() { }

        public Warehouse(string name, string? address = null)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name не может быть пустым", nameof(name));

            Id = Guid.NewGuid();
            Name = name;
            Address = address;
            IsActive = true;
        }

        public void UpdateDetails(string name, string? address)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name не может быть пустым", nameof(name));

            Name = name;
            Address = address;
        }

        public void Deactivate() => IsActive = false;
        public void Activate() => IsActive = true;
    }
}
