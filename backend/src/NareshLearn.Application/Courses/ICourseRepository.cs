using NareshLearn.Domain.Courses;

namespace NareshLearn.Application.Courses
{
    public interface ICourseRepository
    {
        Task AddAsync(Course course, CancellationToken ct);
        Task<IReadOnlyList<Course>> ListPublicAsync(CancellationToken ct);
    }
}
