using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ToDoApp.WebApi.Authentication;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase, IUsersController
{
    private readonly UserManager<UserEntity> _userManager;

    public UsersController(UserManager<UserEntity> userManager)
    {
        _userManager = userManager;
    }

    [HttpGet]
    [Route("")]
    public async Task<IActionResult> AllAsync()
    {
        return Ok("");
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> EditAsync(string id, [FromBody] UserEditRequest userEditRequest)
    {
        return Ok("");
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> LoadAsync(string id)
    {
        return Ok("");
    }

    [HttpPost]
    [Route("")]
    public async Task<IActionResult> MakeAsync([FromBody] UserMakeRequest userMakeRequest)
    {
        if (userMakeRequest.UserName == null)
        {
            return BadRequest("UserName is missing.");
        }

        var userName = userMakeRequest.UserName.Trim();

        if (String.IsNullOrEmpty(userName))
        {
            return BadRequest("UserName is missing.");
        }

        if (userMakeRequest.Email == null)
        {
            return BadRequest("Email is missing.");
        }

        var email = userMakeRequest.Email.Trim();

        if (String.IsNullOrEmpty(email))
        {
            return BadRequest("Email is missing.");
        }

        if (userMakeRequest.Password == null)
        {
            return BadRequest("Password is missing.");
        }

        var password = userMakeRequest.Password;

        if (String.IsNullOrEmpty(password))
        {
            return BadRequest("Password is missing.");
        }

        if (userMakeRequest.Confirm == null)
        {
            return BadRequest("Confirm is missing.");
        }

        var confirm = userMakeRequest.Confirm;

        if (String.IsNullOrEmpty(confirm))
        {
            return BadRequest("Confirm is missing.");
        }

        if (password != confirm)
        {
            return BadRequest("Confirm does not match Password.");
        }

        var user = new UserEntity
        {
            Email = email,
            UserName = userName,
        };

        var result = await _userManager.CreateAsync(user, password);

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        return Ok("{}");
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> RemoveAsync(string id)
    {
        return Ok("");
    }
}
