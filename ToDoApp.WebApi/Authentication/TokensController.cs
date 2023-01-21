using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ToDoApp.WebApi.Authentication;

public class TokensController : ControllerBase, ITokensController
{
    private readonly SignInManager<UserEntity> _signInManager;

    public TokensController(SignInManager<UserEntity> signInManager)
    {
        _signInManager = signInManager;
    }

    public async Task<IActionResult> AllAsync()
    {
        return Ok("");
    }

    public async Task<IActionResult> LoadAsync(string id)
    {
        return Ok("");
    }

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

    public async Task<IActionResult> RemoveAsync(string id)
    {
        return Ok("");
    }
}
