using FluentAssertions;
using Moq;
using NareshLearn.Application.Auth;
using NareshLearn.Application.Auth.Register;
using NareshLearn.Application.Users;

namespace NareshLearn.UnitTests.Auth
{
    public class RegisterUserServiceTests
    {
        [Fact]
        public async Task RegisterAsync_When_Email_Already_Exists_Should_Fail()
        {
            var repo = new Mock<IUserRepository>();
            repo.Setup(r => r.EmailExistsAsync("test@email.com", It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            var hasher = new Mock<IPasswordHasher>();
            hasher.Setup(h => h.Hash(It.IsAny<string>())).Returns("hash");

            var svc = new RegisterUserService(repo.Object, hasher.Object);

            var result = await svc.RegisterAsync(
                new RegisterRequest("A", "B", "test@email.com", "password", 1),
                CancellationToken.None);

            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Be("Email already exists.");
        }

        [Fact]
        public async Task RegisterAsync_When_Valid_Should_Create_User()
        {
            var repo = new Mock<IUserRepository>();
            repo.Setup(r => r.EmailExistsAsync("test@email.com", It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            var hasher = new Mock<IPasswordHasher>();
            hasher.Setup(h => h.Hash("password")).Returns("hashed");

            var svc = new RegisterUserService(repo.Object, hasher.Object);

            var result = await svc.RegisterAsync(
                new RegisterRequest("A", "B", "test@email.com", "password", 1),
                CancellationToken.None);

            result.IsSuccess.Should().BeTrue();
            result.Value!.Email.Should().Be("test@email.com");

            repo.Verify(r => r.AddAsync(It.IsAny<NareshLearn.Domain.Users.User>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }

}
