using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using ToDoApp.WebApi.Authentication;

namespace ToDoApp.Tests.Helpers;

public class FakeUserManager : UserManager<UserEntity>
{
    public FakeUserManager()
        : base(new Mock<IUserStore<UserEntity>>().Object,
          new Mock<IOptions<IdentityOptions>>().Object,
          new Mock<IPasswordHasher<UserEntity>>().Object,
          new IUserValidator<UserEntity>[0],
          new IPasswordValidator<UserEntity>[0],
          new Mock<ILookupNormalizer>().Object,
          new Mock<IdentityErrorDescriber>().Object,
          new Mock<IServiceProvider>().Object,
          new Mock<ILogger<UserManager<UserEntity>>>().Object)
    { }

    public override Task<IdentityResult> CreateAsync(UserEntity user, string password)
    {
        return Task.FromResult(IdentityResult.Success);
    }

    public override Task<IdentityResult> AddToRoleAsync(UserEntity user, string role)
    {
        return Task.FromResult(IdentityResult.Success);
    }

    public override Task<string> GenerateEmailConfirmationTokenAsync(UserEntity user)
    {
        return Task.FromResult(Guid.NewGuid().ToString());
    }
}
