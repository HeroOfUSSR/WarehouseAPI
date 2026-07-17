using Microsoft.AspNetCore.Mvc;
using Warehouse.Application.Services;

namespace Warehouse.API.Controllers
{
    [ApiController]
    [Route("api/stock")]
    public class StockController : ControllerBase
    {
        private readonly StockService _stockService;

        public StockController(StockService stockService) => _stockService = stockService;

        [HttpGet]
        public async Task<IActionResult> Get(
            [FromQuery] Guid? storehouseId, [FromQuery] Guid? productId, CancellationToken ct)
        {
            if (storehouseId.HasValue)
                return Ok(await _stockService.GetByStorehouseAsync(storehouseId.Value, ct));

            if (productId.HasValue)
                return Ok(await _stockService.GetByProductAsync(productId.Value, ct));

            return BadRequest("Укажите storehouseId или productId");
        }

        [HttpGet("below-minimum")]
        public async Task<IActionResult> GetBelowMinimum(CancellationToken ct)
            => Ok(await _stockService.GetBelowMinimumAsync(ct));
    }
}
