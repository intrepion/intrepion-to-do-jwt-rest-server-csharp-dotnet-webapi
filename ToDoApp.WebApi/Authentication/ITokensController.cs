using Microsoft.AspNetCore.Mvc;

namespace ToDoApp.WebApi.Authentication;

public interface ITokensController
{
    Task<IActionResult> MakeAsync(TokenMakeRequest tokenMakeRequest);
}
