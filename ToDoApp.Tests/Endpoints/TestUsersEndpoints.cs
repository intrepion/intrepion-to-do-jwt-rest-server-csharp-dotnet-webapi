using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using ToDoApp.WebApi.Authentication;

namespace ToDoApp.Tests.WebApi.Endpoints;

public class TestUsersEndpoints : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public TestUsersEndpoints(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task Get_Users_EndpointReturnSuccessAndCorrectContentType()
    {
        // Arrange
        var client = _factory.CreateClient();
        var userMakeRequest = new UserMakeRequest
        {
            Confirm = "someP4$$w0rd",
            Email = "some@email.com",
            UserName = (Guid.NewGuid()).ToString(),
            Password = "someP4$$w0rd",
        };
        var userEditRequest = new UserEditRequest
        {
            Confirm = "editedP4$$w0rd",
            Email = "edited@email.com",
            UserName = (Guid.NewGuid()).ToString(),
            Password = "editedP4$$w0rd",
        };

        // Act 1
        var response1 = await client.GetAsync("/Users");
        var actual1 = response1.Content.Headers.ContentType?.ToString();

        // Assert 1
        Assert.NotNull(response1);
        response1.EnsureSuccessStatusCode();

        // Act 2
        var content2 = new StringContent(JsonSerializer.Serialize(userMakeRequest), Encoding.UTF8, "application/json");
        var response2 = await client.PostAsync("/Users", content2);
        var actual2 = response2.Content.Headers.ContentType?.ToString();

        // Assert 2
        Assert.NotNull(response2);
        response2.EnsureSuccessStatusCode();

        // Act 3
        var response3 = await client.GetAsync("/Users");
        var actual3 = response3.Content.Headers.ContentType?.ToString();

        // Assert 3
        Assert.NotNull(response3);
        response3.EnsureSuccessStatusCode();

        // Act 4
        var response4 = await client.GetAsync("/Users/SomeId");
        var actual4 = response4.Content.Headers.ContentType?.ToString();

        // Assert 4
        Assert.NotNull(response4);
        response4.EnsureSuccessStatusCode();

        // Act 5
        var content5 = new StringContent(JsonSerializer.Serialize(userEditRequest), Encoding.UTF8, "application/json");
        var response5 = await client.PutAsync("/Users/SomeId", content5);
        var actual5 = response5.Content.Headers.ContentType?.ToString();

        // Assert 5
        Assert.NotNull(response5);
        response5.EnsureSuccessStatusCode();

        // Act 6
        var response6 = await client.GetAsync("/Users");
        var actual6 = response6.Content.Headers.ContentType?.ToString();

        // Assert 6
        Assert.NotNull(response6);
        response6.EnsureSuccessStatusCode();

        // Act 7
        var response7 = await client.GetAsync("/Users/SomeId");
        var actual7 = response7.Content.Headers.ContentType?.ToString();

        // Assert 7
        Assert.NotNull(response7);
        response7.EnsureSuccessStatusCode();

        // Act 8
        var response8 = await client.DeleteAsync("/Users/SomeId");
        var actual8 = response8.Content.Headers.ContentType?.ToString();

        // Assert 8
        Assert.NotNull(response8);
        response8.EnsureSuccessStatusCode();

        // Act 9
        var response9 = await client.GetAsync("/Users");
        var actual9 = response9.Content.Headers.ContentType?.ToString();

        // Assert 9
        Assert.NotNull(response9);
        response9.EnsureSuccessStatusCode();
    }
}
