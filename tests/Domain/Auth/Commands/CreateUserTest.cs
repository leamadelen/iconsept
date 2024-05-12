using Xunit;
using Xunit.Abstractions;
using iconcept.Domain.Auth.Pipelines;
using iconcept.Infrastructure;
using iconcept.tests.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace iconcept.Domain.Auth.Tests.Pipelines
{
    public class RegisterUserHandlerTests : DbTest
    {
        private readonly ITestOutputHelper _output;
        private readonly IServiceProvider _serviceProvider;

        public RegisterUserHandlerTests(ITestOutputHelper output, IServiceProvider serviceProvider) : base(output)
        {
            _output = output;
            _serviceProvider = serviceProvider;
        }

        [Fact]
        public async Task RegisterUserHandler_ShouldAssignUserRoleAsync()
        {
            // Arrange
            using var scope = _serviceProvider.CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

            var command = new RegisterUserCommand
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john@example.com",
                Password = "P@ssw0rd"
            };

            var handler = new RegisterUserHandler(userManager, null, null);

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            Assert.True(result.IsSuccess);

            // Verify that the correct role was assigned
            var user = await userManager.FindByEmailAsync("john@example.com");
            var roles = await userManager.GetRolesAsync(user);
            Assert.Contains("Bruker", roles);
        }
    }
}
