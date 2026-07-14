using Microsoft.AspNetCore.Mvc;
using Warehouse.Application.Services;

namespace Warehouse.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StorehousesController : ControllerBase
    {
        private readonly StorehouseService _storehouseService;

        public StorehousesController(StorehouseService storehouseService) => _storehouseService = storehouseService;

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] CreateStorehouseRequest request, CancellationToken ct)
        {
            var id = await _storehouseService.CreateAsync(request.Name, request.Address, ct);
            return CreatedAtAction(nameof(GetById), new { id }, id);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
        {
            var storehouse = await _storehouseService.GetByIdAsync(id, ct);
            return Ok(storehouse);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken ct)
        {
            var storehouses = await _storehouseService.GetAllAsync(ct);
            return Ok(storehouses);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(
            Guid id, [FromBody] UpdateStorehouseRequest request, CancellationToken ct)
        {
            await _storehouseService.UpdateAsync(id, request.Name, request.Address, ct);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Deactivate(Guid id, CancellationToken ct)
        {
            await _storehouseService.DeactivateAsync(id, ct);
            return NoContent();
        }
    }

    public record CreateStorehouseRequest(string Name, string? Address);
    public record UpdateStorehouseRequest(string Name, string? Address);
}
