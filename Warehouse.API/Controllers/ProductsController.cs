using Microsoft.AspNetCore.Mvc;
using Warehouse.Application.Services;
using Warehouse.Domain.Models;

namespace Warehouse.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductsController(ProductService productService) => _productService = productService;

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] CreateProductRequest request, CancellationToken ct)
        {
            var id = await _productService.CreateAsync(
                request.Sku, request.Name, request.Unit, request.Description, ct);

            return CreatedAtAction(nameof(GetById), new { id }, id);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
        {
            var product = await _productService.GetByIdAsync(id, ct);
            return Ok(product);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken ct)
        {
            var products = await _productService.GetAllAsync(ct);
            return Ok(products);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(
            Guid id, [FromBody] UpdateProductRequest request, CancellationToken ct)
        {
            await _productService.UpdateAsync(id, request.Name, request.Description, ct);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Deactivate(Guid id, CancellationToken ct)
        {
            await _productService.DeactivateAsync(id, ct);
            return NoContent();
        }
    }

    public record CreateProductRequest(string Sku, string Name, UnitOfMeasure Unit, string? Description);
    public record UpdateProductRequest(string Name, string? Description);
}
