using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Domain.Models;

namespace Warehouse.Application.Interfaces
{
    public interface IMovementRepository
    {
        Task<Movement?> GetByIdAsync(Guid id, CancellationToken ct);
        Task<List<Movement>> GetHistoryAsync(
            Guid? productId, Guid? storehouseId, DateTime? from, DateTime? to,
            int page, int pageSize, CancellationToken ct);
        Task AddAsync(Movement movement, CancellationToken ct);
    }
}
