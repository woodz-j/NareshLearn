using NareshLearn.Application.Common;
using NareshLearn.Domain.Common;
using NareshLearn.Domain.Courses;

namespace NareshLearn.Application.Courses.Create
{

    public sealed class CreateCourseService
    {
        private readonly ICourseRepository _courses;

        public CreateCourseService(ICourseRepository courses)
        {
            _courses = courses;
        }

        public async Task<Result<CourseResponse>> CreateAsync(Guid instructorId, CreateCourseRequest request, CancellationToken ct)
        {
            try
            {
                var course = new Course(instructorId, request.Title, request.Description);
                await _courses.AddAsync(course, ct);

                return Result<CourseResponse>.Success(new CourseResponse(
                    course.Id,
                    course.Title,
                    course.Description,
                    course.InstructorId,
                    course.IsPublished
                ));
            }
            catch (DomainException ex)
            {
                return Result<CourseResponse>.Failure(ex.Message);
            }
        }
    }
}
