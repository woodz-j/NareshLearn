using NareshLearn.Domain.Users;

namespace NareshLearn.Application.Auth
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user);
    }
}
