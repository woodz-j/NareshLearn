using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace NareshLearn.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DebugController : ControllerBase
    {
        [Authorize] // <-- add this
        [HttpGet("headers")]
        public IActionResult Headers()
        {
            var auth = Request.Headers.Authorization.ToString();
            return Ok(new { authorizationHeader = auth });
        }
    }
}
