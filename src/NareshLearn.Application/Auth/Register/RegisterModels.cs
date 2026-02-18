using System;
using System.Collections.Generic;
using System.Text;

namespace NareshLearn.Application.Auth.Register
{
    public sealed record RegisterRequest(
        string FirstName,
        string LastName,
        string Email,
        string Password,
        int Role // 1 Student, 2 Instructor (Admin usually not public)
    );

    public sealed record RegisterResponse(
        Guid UserId,
        string Email,
        string Role
    );
}
