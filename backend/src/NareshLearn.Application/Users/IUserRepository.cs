using NareshLearn.Domain.Users;

namespace NareshLearn.Application.Users
{
    public interface IUserRepository
    {
        Task<bool> EmailExistsAsync(string email, CancellationToken ct);
        Task AddAsync(User user, CancellationToken ct);
        Task<User?> GetByEmailAsync(string email, CancellationToken ct);

    }
}
