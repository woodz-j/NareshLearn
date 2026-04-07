using NareshLearn.Application.Courses;
using NareshLearn.Domain.Courses;
using NareshLearn.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace NareshLearn.Infrastructure.Courses
{
    public sealed class CourseRepository : ICourseRepository
    {
        private readonly AppDbContext _db;

        public CourseRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task AddAsync(Course course, CancellationToken ct)
        {
            _db.Courses.Add(course);
            await _db.SaveChangesAsync(ct);
        }

        public async Task<IReadOnlyList<Course>> ListPublicAsync(CancellationToken ct)
        {
            // For now: return all courses (published/unpublished). We can refine later.
            return await _db.Courses
                .OrderByDescending(c => c.CreatedAtUtc)
                .ToListAsync(ct);
        }
    }
}
