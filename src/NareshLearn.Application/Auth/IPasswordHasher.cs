using System;
using System.Collections.Generic;
using System.Text;

namespace NareshLearn.Application.Auth
{
    public interface IPasswordHasher
    {
        string Hash(string password);
    }
}
