using Microsoft.AspNetCore.Mvc;

namespace ToDoApp.WebApi.Authentication;

public interface IUsersController
{
    Task<IActionResult> AllAsync();
    Task<IActionResult> EditAsync(string id, UserEditRequest userEditRequest);
    Task<IActionResult> LoadAsync(string id);
    Task<IActionResult> MakeAsync(UserMakeRequest userMakeRequest);
}
