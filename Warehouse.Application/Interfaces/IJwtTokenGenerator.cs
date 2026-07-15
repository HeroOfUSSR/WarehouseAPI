using Warehouse.Domain.Models;

namespace Warehouse.Application.Interfaces
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(AppUser user);
    }
}