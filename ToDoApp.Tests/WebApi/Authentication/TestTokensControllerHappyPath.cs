using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ToDoApp.Tests.Helpers;
using ToDoApp.WebApi.Authentication;

namespace ToDoApp.Tests.WebApi.Authentication;

public class TestTokensControllerHappyPath
{
    private ITokensController _tokensController;
    private IUsersController _usersController;

    public TestTokensControllerHappyPath()
    {
        _tokensController = new TokensController(new FakeSignInManagerSuccess());
        _usersController = new UsersController(new FakeUserManager());
    }

    [Fact]
    public async Task HappyPathAsync()
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
        var tokenMakeRequest = new TokenMakeRequest
        {
            UserName = userName,
            Password = "somePassword",
        };
        var result = _usersController.MakeAsync(userMakeRequest);

        // Act
        var actualResult = await _tokensController.MakeAsync(tokenMakeRequest);

        // Assert
        actualResult.Should().BeOfType<OkObjectResult>();
        var okObjectResult = (OkObjectResult)actualResult;
        okObjectResult.StatusCode.Should().Be(200);
    }
}
