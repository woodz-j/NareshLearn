using NareshLearn.Application.Users;
using NareshLearn.Domain.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace NareshLearn.Infrastructure.Users
{
    // TEMPORARY: Replace with EF Core repository later
    public sealed class InMemoryUserRepository : IUserRepository
    {
        private readonly List<User> _users = new();

        public Task<bool> EmailExistsAsync(string email, CancellationToken ct)
            => Task.FromResult(_users.Any(u => u.Email == email));

        public Task AddAsync(User user, CancellationToken ct)
        {
            _users.Add(user);
            return Task.CompletedTask;
        }
    }
}
