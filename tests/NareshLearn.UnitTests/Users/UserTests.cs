using FluentAssertions;
using NareshLearn.Domain.Common;
using NareshLearn.Domain.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace NareshLearn.UnitTests.Users
{
    public class UserTests
    {
        [Fact]
        public void Creating_User_With_Invalid_Email_Should_Throw()
        {
            Action act = () => new User(
                "Naresh",
                "Kumar",
                "invalid-email",
                "hashedpassword",
                Role.Student);

            act.Should().Throw<DomainException>()
                .WithMessage("Email format is invalid.");
        }

        [Fact]
        public void Creating_User_With_Empty_FirstName_Should_Throw()
        {
            Action act = () => new User(
                "",
                "Kumar",
                "naresh@email.com",
                "hashedpassword",
                Role.Student);

            act.Should().Throw<DomainException>();
        }
    }
}
