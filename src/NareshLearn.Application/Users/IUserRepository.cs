using NareshLearn.Domain.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace NareshLearn.Application.Users
{
    public interface IUserRepository
    {
        Task<bool> EmailExistsAsync(string email, CancellationToken ct);
        Task AddAsync(User user, CancellationToken ct);
    }
}
