using System;
using System.Collections.Generic;
using System.Text;

namespace NareshLearn.Application.Auth.Login
{
    public sealed record LoginRequest(string Email, string Password);

    public sealed record LoginResponse(
        Guid UserId,
        string Email,
        string Role,
        string AccessToken
    );
}
