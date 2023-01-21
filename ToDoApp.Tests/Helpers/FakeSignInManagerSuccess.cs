using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using ToDoApp.WebApi.Authentication;

namespace ToDoApp.Tests.Helpers;

public class FakeSignInManagerSuccess : SignInManager<UserEntity>
{
    public FakeSignInManagerSuccess()
            : base(new FakeUserManager(),
                 new Mock<IHttpContextAccessor>().Object,
                 new Mock<IUserClaimsPrincipalFactory<UserEntity>>().Object,
                 new Mock<IOptions<IdentityOptions>>().Object,
                 new Mock<ILogger<SignInManager<UserEntity>>>().Object,
                 new Mock<IAuthenticationSchemeProvider>().Object,
                 new Mock<IUserConfirmation<UserEntity>>().Object)
    { }

    public override Task<SignInResult> PasswordSignInAsync(string userName, string password, bool isPersistent, bool lockoutOnFailure)
    {
        return Task.FromResult(SignInResult.Success);
    }
}
