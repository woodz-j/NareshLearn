using NareshLearn.Application.Common;
using NareshLearn.Application.Users;
using NareshLearn.Domain.Common;
using NareshLearn.Domain.Users;

namespace NareshLearn.Application.Auth.Register
{
    public sealed class RegisterUserService
    {
        private readonly IUserRepository _users;
        private readonly IPasswordHasher _hasher;

        public RegisterUserService(IUserRepository users, IPasswordHasher hasher)
        {
            _users = users;
            _hasher = hasher;
        }

        public async Task<Result<RegisterResponse>> RegisterAsync(RegisterRequest request, CancellationToken ct)
        {
            // App-layer validation (input-focused)
            if (string.IsNullOrWhiteSpace(request.Password))
                return Result<RegisterResponse>.Failure("Password cannot be empty.");

            // Prevent public registration as Admin (common SaaS rule)
            if (request.Role == (int)Role.Admin)
                return Result<RegisterResponse>.Failure("Admin registration is not allowed.");

            var email = request.Email?.Trim().ToLowerInvariant() ?? string.Empty;

            if (await _users.EmailExistsAsync(email, ct))
                return Result<RegisterResponse>.Failure("Email already exists.");

            Role role;
            try
            {
                role = (Role)request.Role;
                if (!Enum.IsDefined(typeof(Role), role))
                    return Result<RegisterResponse>.Failure("Role is invalid.");
            }
            catch
            {
                return Result<RegisterResponse>.Failure("Role is invalid.");
            }

            // Hash password (implementation comes from Infrastructure later)
            var passwordHash = _hasher.Hash(request.Password);

            try
            {
                // Domain enforces invariants (names/email/passwordHash)
                var user = new User(
                    request.FirstName,
                    request.LastName,
                    email,
                    passwordHash,
                    role);

                await _users.AddAsync(user, ct);

                return Result<RegisterResponse>.Success(
                    new RegisterResponse(user.Id, user.Email, user.Role.ToString()));
            }
            catch (DomainException ex)
            {
                // Domain rule failure surfaced cleanly
                return Result<RegisterResponse>.Failure(ex.Message);
            }
        }
    }

}
