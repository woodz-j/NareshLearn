using NareshLearn.Application.Courses.Create;
using System;
using System.Collections.Generic;
using System.Text;

namespace NareshLearn.Application.Courses.List
{
    public sealed class ListCoursesService
    {
        private readonly ICourseRepository _courses;

        public ListCoursesService(ICourseRepository courses)
        {
            _courses = courses;
        }

        public async Task<IReadOnlyList<CourseResponse>> ListPublicAsync(CancellationToken ct)
        {
            var courses = await _courses.ListPublicAsync(ct);

            return courses
                .Select(c => new CourseResponse(c.Id, c.Title, c.Description, c.InstructorId, c.IsPublished))
                .ToList();
        }
    }
}
