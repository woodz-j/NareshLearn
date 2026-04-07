using NareshLearn.Application.Common;
using NareshLearn.Application.Users;

namespace NareshLearn.Application.Auth.Login
{
    public sealed class LoginUserService
    {
        private readonly IUserRepository _users;
        private readonly IPasswordHasher _hasher;
        private readonly IJwtTokenGenerator _jwt;

        public LoginUserService(IUserRepository users, IPasswordHasher hasher, IJwtTokenGenerator jwt)
        {
            _users = users;
            _hasher = hasher;
            _jwt = jwt;
        }

        public async Task<Result<LoginResponse>> LoginAsync(LoginRequest request, CancellationToken ct)
        {
            var email = request.Email?.Trim().ToLowerInvariant() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(request.Password))
                return Result<LoginResponse>.Failure("Email and password are required.");

            var user = await _users.GetByEmailAsync(email, ct);
            if (user is null)
                return Result<LoginResponse>.Failure("Invalid credentials.");

            if (!_hasher.Verify(request.Password, user.PasswordHash))
                return Result<LoginResponse>.Failure("Invalid credentials.");

            var token = _jwt.GenerateToken(user);

            return Result<LoginResponse>.Success(
                new LoginResponse(user.Id, user.Email, user.Role.ToString(), token));
        }
    }
}
