using FluentAssertions;
using Moq;
using NareshLearn.Application.Courses;
using NareshLearn.Application.Courses.Create;
using NareshLearn.Domain.Courses;


namespace NareshLearn.UnitTests.Courses
{
    public class CreateCourseServiceTests
    {
        [Fact]
        public async Task CreateAsync_With_Empty_Title_Should_Fail()
        {
            var repo = new Mock<ICourseRepository>();
            var svc = new CreateCourseService(repo.Object);

            var result = await svc.CreateAsync(Guid.NewGuid(), new CreateCourseRequest("", "desc"), CancellationToken.None);

            result.IsSuccess.Should().BeFalse();
            result.Error.Should().NotBeNull();
            repo.Verify(r => r.AddAsync(It.IsAny<Course>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task CreateAsync_With_Valid_Data_Should_Add_Course()
        {
            var repo = new Mock<ICourseRepository>();
            var svc = new CreateCourseService(repo.Object);

            var instructorId = Guid.NewGuid();
            var result = await svc.CreateAsync(instructorId, new CreateCourseRequest("Intro to C#", "Basics"), CancellationToken.None);

            result.IsSuccess.Should().BeTrue();
            result.Value!.InstructorId.Should().Be(instructorId);
            repo.Verify(r => r.AddAsync(It.IsAny<Course>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
