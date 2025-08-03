using Moq;
using LMS.Services;
using Microsoft.AspNetCore.Mvc;
using LMS.Models;
using LMS.Models.Entities;


namespace LMS.Test
{
    public class CourseServiceTests
    {
        private readonly Mock<ICourseService> _mockCourseService;

        public CourseServiceTests()
        {
            _mockCourseService = new Mock<ICourseService>();
        }

        [Fact]
        public async Task GetCourseByIdAsync_ReturnsCourse_WhenExists()
        {
            // Arrange
            _mockCourseService
                .Setup(service => service.GetCourseByIdAsync(1))
                .ReturnsAsync(new Course { Id = 1, Title = "Math" });

            // Act
            var result = await _mockCourseService.Object.GetCourseByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
        }
    }
}
