using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using ToDoApp.Tests.Helpers;
using ToDoApp.WebApi.Authentication;

namespace ToDoApp.Tests.WebApi.Authentication;

public class TestUsersControllerMakeErrors
{
    private UsersController _usersController;

    public TestUsersControllerMakeErrors()
    {
        // Arrange
        _usersController = new UsersController(new FakeUserManager());
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public async Task MakeError_ConfirmIsMissingAsync(string confirm)
    {
        // Arrange
        var userName = (Guid.NewGuid()).ToString();
        var userMakeRequest = new UserMakeRequest
        {
            Confirm = confirm,
            Email = "some@email.com",
            UserName = userName,
            Password = "someP4$$w0rd",
        };

        // Act
        var actualResult = await _usersController.MakeAsync(userMakeRequest);

        // Assert
        actualResult.Should().BeOfType<BadRequestObjectResult>();
        var badRequestObjectResult = (BadRequestObjectResult)actualResult;
        badRequestObjectResult.StatusCode.Should().Be(400);
    }

    [Fact]
    public async Task MakeError_ConfirmDoesNotMatchPasswordAsync()
    {
        // Arrange
        var userName = (Guid.NewGuid()).ToString();
        var userMakeRequest = new UserMakeRequest
        {
            Confirm = "some(0nf!rm",
            Email = "some@email.com",
            UserName = userName,
            Password = "someP4$$w0rd",
        };

        // Act
        var actualResult = await _usersController.MakeAsync(userMakeRequest);

        // Assert
        actualResult.Should().BeOfType<BadRequestObjectResult>();
        var badRequestObjectResult = (BadRequestObjectResult)actualResult;
        badRequestObjectResult.StatusCode.Should().Be(400);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public async Task MakeError_EmailIsMissingAsync(string email)
    {
        // Arrange
        var userName = (Guid.NewGuid()).ToString();
        var userMakeRequest = new UserMakeRequest
        {
            Confirm = "someP4$$w0rd",
            Email = email,
            UserName = userName,
            Password = "someP4$$w0rd",
        };

        // Act
        var actualResult = await _usersController.MakeAsync(userMakeRequest);

        // Assert
        actualResult.Should().BeOfType<BadRequestObjectResult>();
        var badRequestObjectResult = (BadRequestObjectResult)actualResult;
        badRequestObjectResult.StatusCode.Should().Be(400);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public async Task MakeError_UserNameIsMissingAsync(string userName)
    {
        // Arrange
        var userMakeRequest = new UserMakeRequest
        {
            Confirm = "someP4$$w0rd",
            Email = "some@email.com",
            UserName = userName,
            Password = "someP4$$w0rd",
        };

        // Act
        var actualResult = await _usersController.MakeAsync(userMakeRequest);

        // Assert
        actualResult.Should().BeOfType<BadRequestObjectResult>();
        var badRequestObjectResult = (BadRequestObjectResult)actualResult;
        badRequestObjectResult.StatusCode.Should().Be(400);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public async Task MakeError_PasswordIsMissingAsync(string password)
    {
        // Arrange
        var userName = (Guid.NewGuid()).ToString();
        var userMakeRequest = new UserMakeRequest
        {
            Confirm = "someP4$$w0rd",
            Email = "some@email.com",
            UserName = userName,
            Password = password,
        };

        // Act
        var actualResult = await _usersController.MakeAsync(userMakeRequest);

        // Assert
        actualResult.Should().BeOfType<BadRequestObjectResult>();
        var badRequestObjectResult = (BadRequestObjectResult)actualResult;
        badRequestObjectResult.StatusCode.Should().Be(400);
    }
}
