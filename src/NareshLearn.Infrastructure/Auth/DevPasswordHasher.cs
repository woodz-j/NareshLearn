using NareshLearn.Application.Auth;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace NareshLearn.Infrastructure.Auth
{
    // TEMPORARY FOR DEV ONLY: Replace with BCrypt/Argon2 later
    public sealed class DevPasswordHasher : IPasswordHasher
    {
        public string Hash(string password)
        {
            // Not production-grade; only to unblock wiring
            var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }
    }
}
