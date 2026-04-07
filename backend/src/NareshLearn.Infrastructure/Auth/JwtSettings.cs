using System;
using System.Collections.Generic;
using System.Text;

namespace NareshLearn.Infrastructure.Auth
{
    public sealed class JwtSettings
    {
        public string Issuer { get; init; } = "";
        public string Audience { get; init; } = "";
        public string Key { get; init; } = "";
        public int ExpiryMinutes { get; init; } = 60;
    }
}
