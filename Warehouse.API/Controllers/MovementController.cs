using Microsoft.AspNetCore.Mvc;
using Warehouse.Application.Interfaces;

namespace Warehouse.API.Controllers
{
    [ApiController]
    [Route("api/movements")]
    public class MovementsController : ControllerBase
    {
        private readonly IMovementRepository _movementRepository;

        public MovementsController(IMovementRepository movementRepository)
            => _movementRepository = movementRepository;

        [HttpGet]
        public async Task<IActionResult> GetHistory(
            [FromQuery] Guid? productId,
            [FromQuery] Guid? storehouseId,
            [FromQuery] DateTime? from,
            [FromQuery] DateTime? to,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 20,
            CancellationToken ct = default)
        {
            var history = await _movementRepository.GetHistoryAsync(
                productId, storehouseId, from, to, page, pageSize, ct);

            return Ok(history);
        }
    }
}
