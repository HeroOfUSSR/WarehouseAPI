using Microsoft.AspNetCore.Mvc;
using Warehouse.Application.Services;

namespace Warehouse.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StockOperationController : ControllerBase
    {
        private readonly StockOperationService _stockOperationService;

        public StockOperationController(StockOperationService stockOperationService) => _stockOperationService = stockOperationService;

        // Временная заглушка вместо реального userId из токена - уберу когда роли настрою
        private static readonly Guid TempUserId = Guid.Parse("00000000-0000-0000-0000-000000000001");

        [HttpPost("receive")]
        public async Task<IActionResult> Receive([FromBody] ReceiveRequest request, CancellationToken ct)
        {
            var movementId = await _stockOperationService.ReceiveAsync(
                request.ProductId, request.Quantity, request.DestinationStorehouseId,
                TempUserId, request.Comment, ct);

            return Ok(new { movementId });
        }

        [HttpPost("ship")]
        public async Task<IActionResult> Ship([FromBody] ShipRequest request, CancellationToken ct)
        {
            var movementId = await _stockOperationService.ShipAsync(
                request.ProductId, request.Quantity, request.SourceStorehouseId,
                TempUserId, request.Comment, ct);

            return Ok(new { movementId });
        }

        [HttpPost("transfer")]
        public async Task<IActionResult> Transfer([FromBody] TransferRequest request, CancellationToken ct)
        {
            var movementId = await _stockOperationService.TransferAsync(
                request.ProductId, request.Quantity, request.SourceStorehouseId,
                request.DestinationStorehouseId, TempUserId, request.Comment, ct);

            return Ok(new { movementId });
        }

        [HttpPost("adjust")]
        public async Task<IActionResult> Adjust([FromBody] AdjustRequest request, CancellationToken ct)
        {
            var movementId = await _stockOperationService.AdjustAsync(
                request.ProductId, request.Quantity, request.StorehouseId,
                request.IsIncrease, TempUserId, request.Comment, ct);

            return Ok(new { movementId });
        }
    }

    public record ReceiveRequest(Guid ProductId, int Quantity, Guid DestinationStorehouseId, string? Comment);
    public record ShipRequest(Guid ProductId, int Quantity, Guid SourceStorehouseId, string? Comment);
    public record TransferRequest(Guid ProductId, int Quantity, Guid SourceStorehouseId, Guid DestinationStorehouseId, string? Comment);
    public record AdjustRequest(Guid ProductId, int Quantity, Guid StorehouseId, bool IsIncrease, string? Comment);


}
