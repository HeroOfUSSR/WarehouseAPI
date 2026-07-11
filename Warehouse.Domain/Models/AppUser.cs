using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse.Domain.Models
{

    public class AppUser
    {
        public Guid Id { get; private set; }
        public string Email { get; private set; }
        public string PasswordHash { get; private set; }
        public UserRole Role { get; private set; }
        public bool IsActive { get; private set; }

        private AppUser() { }

        public AppUser(string email, string passwordHash, UserRole role)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email не может быть пустым", nameof(email));

            if (string.IsNullOrWhiteSpace(passwordHash))
                throw new ArgumentException("PasswordHash не может быть пустым", nameof(passwordHash));

            Id = Guid.NewGuid();
            Email = email.ToLowerInvariant();
            PasswordHash = passwordHash;
            Role = role;
            IsActive = true;
        }

        public void ChangeRole(UserRole role) => Role = role;
        public void Deactivate() => IsActive = false;
        public void Activate() => IsActive = true;
    }

    public enum UserRole
    {
        Viewer,
        Manager,
        Admin
    }
}
