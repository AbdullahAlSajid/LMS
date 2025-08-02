using LMS.Controllers;
using LMS.Data;
using LMS.Models;
using LMS.Models.Entities;
using LMS.Services.UserModule;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;

namespace LMS.Test.UserModule
{
    public class UsersControllerTests
    {
        private readonly Mock<IUserService> _mockService;
        private readonly UsersController _controller;

        public UsersControllerTests()
        {
            _mockService = new Mock<IUserService>();
            _controller = new UsersController(_mockService.Object);
        }

        // Naming Convention : Method_ExpectedResult
        [Fact]
        public async Task GetAllUsers_ReturnsOKWithUsers()
        {
            // Arrange
            var users = new List<User>
            {
                new User 
                { 
                    Id = 1, 
                    FirstName = "TestF", 
                    LastName = "TestL", 
                    Email = "test@gmail.com", 
                    PasswordHash = "password1",
                    CreatedAt = DateTime.UtcNow 
                },
                new User 
                { 
                    Id = 2, 
                    FirstName = "TestF2",   
                    LastName = "TestL2",  
                    Email = "test1@gmail.com",   
                    PasswordHash = "password2",
                    CreatedAt = DateTime.UtcNow 
                }
            };

            _mockService.Setup(s => s.GetAllUsers()).ReturnsAsync(users);

            // Act
            var result = await _controller.GetAllUsers();

            // Assert
            var ok = Assert.IsType<OkObjectResult>(result.Result);

            var returned = Assert.IsType<List<User>>(ok.Value);

            Assert.Equal(2, returned.Count);
        }

    }
}
