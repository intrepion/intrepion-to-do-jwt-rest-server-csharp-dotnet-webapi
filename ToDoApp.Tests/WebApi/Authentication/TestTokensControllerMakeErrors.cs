using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using ToDoApp.Tests.Helpers;
using ToDoApp.WebApi.Authentication;

namespace ToDoApp.Tests.WebApi.Authentication;

public class TestTokensControllerMakeErrors
{
    private ITokensController _tokensController;
    private IUsersController _usersController;

    public TestTokensControllerMakeErrors()
    {
        // Arrange
        _tokensController = new TokensController(new FakeSignInManagerFailed());
        _usersController = new UsersController(new FakeUserManager());
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public async Task MakeError_UserNameIsMissing_BadRequestAsync(string userName)
    {
        // Arrange

        var tokenMakeRequest = new TokenMakeRequest
        {
            UserName = userName,
            Password = "somePassword",
        };

        // Act
        var actualResult = await _tokensController.MakeAsync(tokenMakeRequest);

        // Assert
        actualResult.Should().BeOfType<BadRequestObjectResult>();
        var badRequestObjectResult = (BadRequestObjectResult)actualResult;
        badRequestObjectResult.StatusCode.Should().Be(400);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public async Task MakeError_PasswordIsMissing_BadRequestAsync(string password)
    {
        // Arrange
        var userName = (Guid.NewGuid()).ToString();
        var tokenMakeRequest = new TokenMakeRequest
        {
            UserName = userName,
            Password = password,
        };

        // Act
        var actualResult = await _tokensController.MakeAsync(tokenMakeRequest);

        // Assert
        actualResult.Should().BeOfType<BadRequestObjectResult>();
        var badRequestObjectResult = (BadRequestObjectResult)actualResult;
        badRequestObjectResult.StatusCode.Should().Be(400);
    }

    [Fact]
    public async Task MakeError_WrongUserName_UnauthorizedAsync()
    {
        // Arrange
        var userName = (Guid.NewGuid()).ToString();
        var userMakeRequest = new UserMakeRequest
        {
            Confirm = "someP4$$w0rd",
            Email = "some@email.com",
            UserName = userName,
            Password = "someP4$$w0rd",
        };
        var result = _usersController.MakeAsync(userMakeRequest);
        var tokenMakeRequest = new TokenMakeRequest
        {
            UserName = "wrongUserName",
            Password = "someP4$$w0rd",
        };

        // Act
        var actualResult = await _tokensController.MakeAsync(tokenMakeRequest);

        // Assert
        actualResult.Should().BeOfType<UnauthorizedObjectResult>();
        var unauthorizedObjectResult = (UnauthorizedObjectResult)actualResult;
        unauthorizedObjectResult.StatusCode.Should().Be(401);
    }
}
