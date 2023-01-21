using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using ToDoApp.Tests.Helpers;
using ToDoApp.WebApi.Authentication;

namespace ToDoApp.Tests.WebApi.Authentication;

public class TestUsersControllerHappyPath
{
    private UsersController _usersController;

    public TestUsersControllerHappyPath()
    {
        _usersController = new UsersController(new FakeUserManager());
    }

    [Fact]
    public async Task HappyPathAsync()
    {
        // Arrange
        var userEditRequest = new UserEditRequest
        {
            Confirm = "editedP4$$w0rd",
            Email = "edited@email.com",
            UserName = "editedUserName",
            Password = "editedP4$$w0rd",
        };
        var userMakeRequest = new UserMakeRequest
        {
            Confirm = "someP4$$w0rd",
            Email = "some@email.com",
            UserName = "someUserName",
            Password = "someP4$$w0rd",
        };

        // Act 1
        var actualResult1 = await _usersController.AllAsync();

        // Assert 1
        actualResult1.Should().BeOfType<OkObjectResult>();
        var okObjectResult1 = (OkObjectResult)actualResult1;
        okObjectResult1.StatusCode.Should().Be(200);

        // Act 2
        var actualResult2 = await _usersController.MakeAsync(userMakeRequest);

        // Assert 2
        actualResult2.Should().BeOfType<OkObjectResult>();
        var okObjectResult2 = (OkObjectResult)actualResult2;
        okObjectResult2.StatusCode.Should().Be(200);

        // Act 3
        var actualResult3 = await _usersController.AllAsync();

        // Assert 3
        actualResult3.Should().BeOfType<OkObjectResult>();
        var okObjectResult3 = (OkObjectResult)actualResult3;
        okObjectResult3.StatusCode.Should().Be(200);

        // Act 4
        var actualResult4 = await _usersController.LoadAsync("someid");

        // Assert 4
        actualResult4.Should().BeOfType<OkObjectResult>();
        var okObjectResult4 = (OkObjectResult)actualResult4;
        okObjectResult4.StatusCode.Should().Be(200);

        // Act 5
        var actualResult5 = await _usersController.EditAsync("someid", userEditRequest);

        // Assert 5
        actualResult5.Should().BeOfType<OkObjectResult>();
        var okObjectResult5 = (OkObjectResult)actualResult5;
        okObjectResult5.StatusCode.Should().Be(200);

        // Act 6
        var actualResult6 = await _usersController.AllAsync();

        // Assert 6
        actualResult6.Should().BeOfType<OkObjectResult>();
        var okObjectResult6 = (OkObjectResult)actualResult6;
        okObjectResult6.StatusCode.Should().Be(200);

        // Act 7
        var actualResult7 = await _usersController.LoadAsync("someid");

        // Assert 7
        actualResult7.Should().BeOfType<OkObjectResult>();
        var okObjectResult7 = (OkObjectResult)actualResult7;
        okObjectResult7.StatusCode.Should().Be(200);

        // Act 8
        var actualResult8 = await _usersController.RemoveAsync("someid");

        // Assert 8
        actualResult8.Should().BeOfType<OkObjectResult>();
        var okObjectResult8 = (OkObjectResult)actualResult8;
        okObjectResult8.StatusCode.Should().Be(200);

        // Act 9
        var actualResult9 = await _usersController.AllAsync();

        // Assert 9
        actualResult9.Should().BeOfType<OkObjectResult>();
        var okObjectResult9 = (OkObjectResult)actualResult9;
        okObjectResult9.StatusCode.Should().Be(200);
    }
}
