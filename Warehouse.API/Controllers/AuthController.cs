using Microsoft.AspNetCore.Mvc;
using Warehouse.Application.Services;
using Warehouse.Domain.Models;

namespace Warehouse.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService) => _authService = authService;

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request, CancellationToken ct)
        {
            var id = await _authService.RegisterAsync(request.Email, request.Password, request.Role, ct);
            return Ok(new { id });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request, CancellationToken ct)
        {
            var token = await _authService.LoginAsync(request.Email, request.Password, ct);
            return Ok(new { token });
        }
    }

    public record RegisterRequest(string Email, string Password, UserRole Role);
    public record LoginRequest(string Email, string Password);
}
