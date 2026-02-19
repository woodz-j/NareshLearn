using NareshLearn.Application.Users;
using NareshLearn.Domain.Users;
using NareshLearn.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace NareshLearn.Infrastructure.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _db;

        public UserRepository(AppDbContext db)
        {
            _db = db;
        }

        public Task<bool> EmailExistsAsync(string email, CancellationToken ct)
        {
            return _db.Users.AnyAsync(u => u.Email == email, ct);
        }

        public async Task AddAsync(User user, CancellationToken ct)
        {
            _db.Users.Add(user);
            await _db.SaveChangesAsync(ct);
        }
    }
}
