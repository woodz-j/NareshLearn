using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System.IdentityModel.Tokens.Jwt;
using NareshLearn.Application.Courses.Create;
using NareshLearn.Application.Courses.List;

namespace NareshLearn.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoursesController : ControllerBase
    {
        // Anyone authenticated can list courses
        /*[Authorize]
        [HttpGet]
        public IActionResult GetCourses()
        {
            return Ok("All authenticated users can view courses.");
        }*/
        // Public endpoint
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetCourses([FromServices] ListCoursesService service, CancellationToken ct)
        {
            var result = await service.ListPublicAsync(ct);
            return Ok(result);
        }

        // Only Instructor or Admin can create
        [Authorize(Roles = "Instructor,Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateCourse(
        [FromServices] CreateCourseService service,
        [FromBody] CreateCourseRequest request,
        CancellationToken ct)
        {
            var sub = User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

            if (!Guid.TryParse(sub, out var instructorId))
                return Unauthorized(new { error = "Invalid token subject (sub)." });

            var result = await service.CreateAsync(instructorId, request, ct);

            if (!result.IsSuccess)
                return BadRequest(new { error = result.Error });

            return Ok(result.Value);
        }
        /*public IActionResult CreateCourse()
        {
            return Ok("Course created.");
        }*/

        // Student only
        [Authorize(Roles = "Student")]
        [HttpPost("{id}/enroll")]
        public IActionResult Enroll()
            => Ok("Enrolled");
    }
}
