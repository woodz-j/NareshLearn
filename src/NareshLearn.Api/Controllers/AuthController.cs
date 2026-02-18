
using Microsoft.AspNetCore.Mvc;
using NareshLearn.Application.Auth.Register;

namespace NareshLearn.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly RegisterUserService _register;

        public AuthController(RegisterUserService register)
        {
            _register = register;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request, CancellationToken ct)
        {
            var result = await _register.RegisterAsync(request, ct);

            if (!result.IsSuccess)
                return BadRequest(new { error = result.Error });

            return Ok(result.Value);
        }
    }
}
