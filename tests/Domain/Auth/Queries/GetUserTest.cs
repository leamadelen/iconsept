using Xunit;
using Xunit.Abstractions;
using iconcept.Domain.Auth.Pipelines.Queries;
using iconcept.Infrastructure;
using iconcept.tests.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using Moq;
using iconcept.Domain.Auth;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using System;

namespace iconcept.Domain.Auth.Tests.Pipelines.Queries
{
    public class GetUsersTests : DbTest
    {
        private readonly ITestOutputHelper _output;

        public GetUsersTests(ITestOutputHelper output) : base(output)
        {
            _output = output;
        }

        [Fact]
        public async Task GetUsers_ShouldReturnCorrectRolesAsync()
        {
            // Arrange
            using var context = new ConceptDbContext(ContextOptions);

            // Mocking UserManager<User>
            var userManagerMock = new Mock<UserManager<User>>(MockBehavior.Strict);
            var usersWithRoles = new List<User>
            {
                new User { Id = "1", Email = "john@example.com" },
                new User { Id = "2", Email = "jane@example.com" }
            };

            // Add test users to the database
            await context.Users.AddRangeAsync(usersWithRoles);
            await context.SaveChangesAsync();

            userManagerMock.Setup(m => m.GetRolesAsync(It.IsAny<User>()))
                .ReturnsAsync((User user) =>
                {
                    var roles = GetRolesForUser(user.Id);
                    return roles != null ? roles : new List<string>();
                });

            var query = new GetUsers.Request();
            var handler = new GetUsers.Handler(userManagerMock.Object);

            // Act
            var result = await handler.Handle(query, default);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count); // Assuming two users are returned

            // Cast the elements of result to the expected type
            var usersWithRolesResult = result.Cast<dynamic>().ToList();
            Assert.Equal("Admin", ((List<string>)usersWithRolesResult[0].Roles).First()); // Assuming the first user has "Admin" role
            Assert.Equal("User", ((List<string>)usersWithRolesResult[1].Roles).First()); // Assuming the second user has "User" role
        }

        // Mock method to simulate getting roles for each user
        private List<string> GetRolesForUser(string userId)
        {
            // Logic to retrieve roles based on user id
            // For the sake of testing, return hardcoded roles
            if (userId == "1")
            {
                return new List<string> { "Admin" };
            }
            else if (userId == "2")
            {
                return new List<string> { "User" };
            }
            else
            {
                return null; // or return an empty list
            }
        }
    }
}
