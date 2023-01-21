using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using ToDoApp.WebApi.Authentication;

namespace ToDoApp.Tests.WebApi.Endpoints;

public class TestTokensEndpoints : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public TestTokensEndpoints(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task Get_Users_EndpointReturnSuccessAndCorrectContentType()
    {
        // Arrange
        var client = _factory.CreateClient();
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
            Password = "editedP4$$w0rd",
        };

        // Act 1
        var content1 = new StringContent(JsonSerializer.Serialize(userMakeRequest), Encoding.UTF8, "application/json");
        var response1 = await client.PostAsync("/Users", content1);
        var actual1 = response1.Content.Headers.ContentType?.ToString();

        // Assert 1
        Assert.NotNull(response1);
        response1.EnsureSuccessStatusCode();

        // Act 2
        var content2 = new StringContent(JsonSerializer.Serialize(userMakeRequest), Encoding.UTF8, "application/json");
        var response2 = await client.PostAsync("/Tokens", content2);
        var actual2 = response2.Content.Headers.ContentType?.ToString();

        // Assert 2
        Assert.NotNull(response2);
        response2.EnsureSuccessStatusCode();

        // Act 3
        var response3 = await client.DeleteAsync("/Tokens/SomeId");
        var actual3 = response3.Content.Headers.ContentType?.ToString();

        // Assert 3
        Assert.NotNull(response3);
        response3.EnsureSuccessStatusCode();
    }
}
