using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ToDoApp.WebApi.Authentication;

[ApiController]
[Route("[controller]")]
public class TokensController : ControllerBase, ITokensController
{
    private readonly SignInManager<UserEntity> _signInManager;

    public TokensController(SignInManager<UserEntity> signInManager)
    {
        _signInManager = signInManager;
    }

    [HttpPost]
    [Route("")]
    public async Task<IActionResult> MakeAsync([FromBody] TokenMakeRequest tokenMakeRequest)
    {
        if (tokenMakeRequest.UserName == null)
        {
            return BadRequest("UserName is missing.");
        }

        var userName = tokenMakeRequest.UserName.Trim();

        if (String.IsNullOrEmpty(userName))
        {
            return BadRequest("UserName is missing.");
        }

        if (tokenMakeRequest.Password == null)
        {
            return BadRequest("Password is missing.");
        }

        var password = tokenMakeRequest.Password;

        if (String.IsNullOrEmpty(password))
        {
            return BadRequest("Password is missing.");
        }

        var result = await _signInManager.PasswordSignInAsync(userName, password, false, false);

        if (!result.Succeeded)
        {
            return Unauthorized("Invalid UserName or Password.");
        }

        return Ok("");
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> RemoveAsync(string id)
    {
        return Ok("");
    }
}
