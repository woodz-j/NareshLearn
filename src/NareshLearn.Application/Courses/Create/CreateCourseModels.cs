using System;
using System.Collections.Generic;
using System.Text;

namespace NareshLearn.Application.Courses.Create
{
    public sealed record CreateCourseRequest(string Title, string Description);

    public sealed record CourseResponse(
        Guid Id,
        string Title,
        string Description,
        Guid InstructorId,
        bool IsPublished
    );
}
