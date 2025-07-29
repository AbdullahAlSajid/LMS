using LMS.Controllers;
using LMS.Data;
using LMS.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;

namespace LMS.Test
{
    public class UsersControllerTests
    {


        // Naming Convention : Method_ExpectedResult
        [Fact]
        public async Task GetUsers_ReturnListofUsers()
        {
            // ARRANGE
            var fakeUsers = new List<User>
            {
                new User { Id = 1, FirstName = "Alice", Email = "alice@x.com", Status = UserStatus.Active, TenantId = 1, CreatedAt = System.DateTime.UtcNow },
                new User { Id = 2, FirstName = "Bob",   Email = "bob@x.com",   Status = UserStatus.Invited, TenantId = 1, CreatedAt = System.DateTime.UtcNow }
            };


            var options = new DbContextOptionsBuilder<LMSDbContext>().Options;

            var mockCtx = new Mock<LMSDbContext>(options);
            mockCtx.Setup(c => c.Users).ReturnsDbSet(fakeUsers);

            var controller = new UsersController(mockCtx.Object);


            // ACT
            var actionResult = await controller.GetUsers();


            // ASSERT
            var ok = Assert.IsType<OkObjectResult>(actionResult.Result);
            var returned = Assert.IsType<List<User>>(ok.Value);

            Assert.Equal(2, returned.Count);
            Assert.Contains(returned, u => u.FirstName == "Alice");
            Assert.Contains(returned, u => u.FirstName == "Bob");
        }

        [Fact]
        public async Task GetUser_ReturnUser_WhenUserFound()
        {

            //ARRANGE
            var fakeUser = new User
            {
                Id = 1,
                FirstName = "TestF",
                LastName = "TestL",
                Email = "Test@x.com",
                Status = UserStatus.Active,
                TenantId = 1,
                CreatedAt = DateTime.UtcNow
            };

            var mockSet = new Mock<DbSet<User>>();

            // whenever FindAsync(...) is called on it, return fakeUser
            mockSet.Setup(s => s.FindAsync( It.IsAny<object[]> () )).ReturnsAsync(fakeUser);

            var options = new DbContextOptionsBuilder<LMSDbContext>().Options;

            var mockContext = new Mock<LMSDbContext>(options);
            mockContext.Setup( c => c.Users ).Returns(mockSet.Object);

            var controller = new UsersController(mockContext.Object);


            //ACT
            var actionResult = await controller.GetUser(1);


            //ASSERT
            var ok = Assert.IsType<OkObjectResult>(actionResult.Result);
            var returned = Assert.IsType<User>(ok.Value);

            Assert.Equal("TestF", returned.FirstName);
            Assert.Equal(1, returned.Id);
        }

        [Fact]
        public async Task GetUser_ReturnNotFound_WhenUserNotFound()
        {
            //ARRANGE
            var mockSet = new Mock<DbSet<User>>();
            mockSet.Setup(s => s.FindAsync(It.IsAny<Object[]>())).ReturnsAsync((User)null);


            var options = new DbContextOptionsBuilder<LMSDbContext>().Options;
            var mockContext = new Mock<LMSDbContext>(options);

            mockContext.Setup(c => c.Users).Returns(mockSet.Object);

            var controller = new UsersController(mockContext.Object);

            //ACT
            var actionResult = await controller.GetUser(2);

            //ASSERT
            var notFound = Assert.IsType<NotFoundResult>(actionResult.Result);
            Assert.Null(actionResult.Value);
        }


        [Fact]
        public async Task CreateUser_ReturnsCreatedAtAction_WhenValidUser()
        {
            // ARRANGE
            var newUser = new User
            {
                Email = "test@x.com",
                FirstName = "New",
                LastName = "User",
                Status = UserStatus.Active,
                TenantId = 1
            };

            var mockSet = new Mock<DbSet<User>>();

            var options = new DbContextOptionsBuilder<LMSDbContext>().Options;
            var mockContext = new Mock<LMSDbContext>(options);

            mockContext.Setup(c => c.Users).Returns(mockSet.Object);
            mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

            var controller = new UsersController(mockContext.Object);

            // ACT
            var actionResult = await controller.CreateUser(newUser);

            // ASSERT
            var returned = Assert.IsType<CreatedAtActionResult>(actionResult.Result);

            Assert.Equal(nameof(UsersController.GetUser), returned.ActionName);
            Assert.True(returned.RouteValues.ContainsKey("id"));
            
            var addedUser = Assert.IsType<User>(returned.Value);
            Assert.Equal("test@x.com", addedUser.Email);
        }

        [Fact]
        public async Task CreateUser_ReturnsBadRequest_WhenUserNull()
        {
            // ARRANGE
            var options = new DbContextOptionsBuilder<LMSDbContext>().Options;
            var mockContext = new Mock<LMSDbContext>(options);

            var controller = new UsersController(mockContext.Object);

            // ACT
            var actionResult = await controller.CreateUser(null);

            // ASSERT
            Assert.IsType<BadRequestResult>(actionResult.Result);
        }


        [Fact]
        public async Task UpdateUser_ReturnNoContent_WhenUserFound()
        {
            // ARRANGE
            var fakeUser = new User
            {
                Id = 1,
                Email = "Test@x.com",
                FirstName = "TestF",
                LastName = "TestL",
                Status = UserStatus.Active,
                TenantId = 1
            };

            var updateUser = new User
            {
                Id = 1,
                Email = "Test@x.com",
                FirstName = "TestFUpdated",
                LastName = "TestL",
                Status = UserStatus.Active,
                TenantId = 1
            };

            var mockSet = new Mock<DbSet<User>>();

            mockSet.Setup(s => s.FindAsync( new object[] { 1 } )).ReturnsAsync(fakeUser);

            var options = new DbContextOptionsBuilder<LMSDbContext>().Options;
            var mockContext = new Mock<LMSDbContext>(options);

            mockContext.Setup(c => c.Users).Returns(mockSet.Object);

            mockContext.Setup(c => c.SaveChangesAsync( It.IsAny<CancellationToken>() )).ReturnsAsync(1);

            var controller = new UsersController (mockContext.Object);

            // ACT

            var actionResult = await controller.UpdateUser(1, updateUser);

            // ASSERT
            var ok = Assert.IsType<NoContentResult>(actionResult);

        }

        [Fact]
        public async Task DeleteUser_ReturnNoContent_WhenUserFound()
        {
            // ARRANGE
            var fakeUser = new User
            {
                Id = 1,
                Email = "Test@x.com",
                FirstName = "TestF",
                LastName = "TestL",
                Status = UserStatus.Active,
                TenantId = 1
            };

            var mockSet = new Mock<DbSet<User>>();
            mockSet.Setup(s => s.FindAsync(new object[] { 1 })).ReturnsAsync(fakeUser);

            var options = new DbContextOptionsBuilder<LMSDbContext>().Options;
            var mockContext = new Mock<LMSDbContext>(options);

            mockContext.Setup(c => c.Users).Returns(mockSet.Object);
            mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

            var controller = new UsersController(mockContext.Object);

            // ACT
            var result = await controller.DeleteUser(1);

            // ASSERT
            Assert.IsType<NoContentResult>(result);
        }

        public async Task DeleteUser_ReturnNotFound_WhenUserNotFound()
        {
            // ARRANGE
            var mockSet = new Mock<DbSet<User>>();
            mockSet.Setup(s => s.FindAsync( It.IsAny<Object[]>() )).ReturnsAsync((User)null);

            var options = new DbContextOptionsBuilder<LMSDbContext>().Options;
            var mockContext = new Mock<LMSDbContext>(options);

            mockContext.Setup(c => c.Users).Returns(mockSet.Object);

            var controller = new UsersController(mockContext.Object);

            // ACT
            var result = await controller.DeleteUser(99);

            // ASSERT
            Assert.IsType<NotFoundResult>(result);
        }

    }
}
