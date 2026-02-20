
using Microsoft.AspNetCore.Mvc;
using NareshLearn.Application.Auth.Login;
using NareshLearn.Application.Auth.Register;

namespace NareshLearn.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly RegisterUserService _register;
        private readonly LoginUserService _login;

        public AuthController(RegisterUserService register, LoginUserService login)
        {
            _register = register;
            _login = login;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request, CancellationToken ct)
        {
            var result = await _register.RegisterAsync(request, ct);

            if (!result.IsSuccess)
                return BadRequest(new { error = result.Error });

            return Ok(result.Value);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request, CancellationToken ct)
        {
            var result = await _login.LoginAsync(request, ct);
            if (!result.IsSuccess) return Unauthorized(new { error = result.Error });
            return Ok(result.Value);
        }
    }
}
