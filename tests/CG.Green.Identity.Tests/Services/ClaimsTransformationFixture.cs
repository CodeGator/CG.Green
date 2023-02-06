using Castle.Core.Logging;
using CG.Green.Identity.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Moq;
using System.Security.Claims;
using System.Security.Principal;

namespace CG.Green.Identity.Services;

/// <summary>
/// This class is a test fixture for the <see cref="ClaimsTransformation"/>
/// class.
/// </summary>
[TestClass]
public class ClaimsTransformationFixture
{
    // *******************************************************************
    // Public methods.
    // *******************************************************************

    #region Public methods

    /// <summary>
    /// This method ensures the <see cref="ClaimsTransformation.ClaimsTransformation(Microsoft.AspNetCore.Identity.SignInManager{Models.GreenUser}, Microsoft.Extensions.Logging.ILogger{Microsoft.AspNetCore.Authentication.IClaimsTransformation})"/>
    /// constructor properly initializes object instances.
    /// </summary>
    [TestMethod]
    public void ClaimsTransformation_ctor()
    {
        // Arrange ...
        var users = new List<GreenUser>() { new GreenUser() { Id = "1" } };
        var userManager = MockUserManager<GreenUser>(users);
        var signInManager = MockSignInManger<GreenUser>(userManager);
        var logger = new Mock<ILogger<IClaimsTransformation>>();

        // Act ...
        var result = new ClaimsTransformation(
            signInManager.Object,
            logger.Object
            );

        // Assert ...
        Assert.IsTrue(
            result._signInManager == signInManager.Object,
            "The _signInManager field was not initialized"
            );
        Assert.IsTrue(
            result._logger == logger.Object,
            "The _logger field was not initialized"
            );

        Mock.Verify(
            userManager,
            signInManager,
            logger
            );
    }

    // *******************************************************************

    /// <summary>
    /// This method ensures the <see cref="ClaimsTransformation.TransformAsync(System.Security.Claims.ClaimsPrincipal)"/>
    /// method properly transforms claims.
    /// </summary>
    [TestMethod]
    public async Task ClaimsTransformation_TransformAsync()
    {
        // Arrange ...
        var users = new List<GreenUser>() { new GreenUser() { UserName = "test", Id = "1" } };
        var userManager = MockUserManager<GreenUser>(users);
        var signInManager = MockSignInManger<GreenUser>(userManager);
        var logger = new Mock<ILogger<IClaimsTransformation>>();

        userManager.Setup(x => x.FindByNameAsync(
            It.IsAny<string>()
            )).ReturnsAsync(users.FirstOrDefault())
            .Verifiable();

        userManager.Setup(x => x.GetClaimsAsync(It.IsAny<GreenUser>()))
            .ReturnsAsync(new[] { new Claim("test", "test") })
            .Verifiable();

        var ct = new ClaimsTransformation(
            signInManager.Object,
            logger.Object
            );

        var claimsPrincipal = new Mock<ClaimsPrincipal>();
        var identity = new Mock<IIdentity>();
        
        claimsPrincipal.Setup(x => x.Identity)
            .Returns(identity.Object)
            .Verifiable();

        var claimsIdentities = new List<ClaimsIdentity>();
        claimsPrincipal.Setup(x => x.Identities)
            .Returns(claimsIdentities)
            .Verifiable();
        claimsPrincipal.Setup(x => x.AddIdentity(It.IsAny<ClaimsIdentity>()))
            .Callback((ClaimsIdentity x) => { claimsIdentities.Add(x); })
            .Verifiable();

        identity.Setup(x => x.Name)
            .Returns("test")
            .Verifiable();

        // Act ...
        var result = await ct.TransformAsync(
            claimsPrincipal.Object
            );

        // Assert ...
        Assert.IsTrue(
            result.Identities.Any(x => x.Claims.Any()),
            "The claims were not transformed"
            );

        Mock.Verify(
            userManager,
            signInManager,
            logger,
            claimsPrincipal,
            identity
            );
    }

    #endregion

    // *******************************************************************
    // Private methods.
    // *******************************************************************

    #region Private methods

    // I found this method here: https://stackoverflow.com/questions/49165810/how-to-mock-usermanager-in-net-core-testing

    private static Mock<UserManager<TUser>> MockUserManager<TUser>(
        List<TUser> users
        ) where TUser : class
    {
        var store = new Mock<IUserStore<TUser>>();
        var mgr = new Mock<UserManager<TUser>>(store.Object, null, null, null, null, null, null, null, null);
        mgr.Object.UserValidators.Add(new UserValidator<TUser>());
        mgr.Object.PasswordValidators.Add(new PasswordValidator<TUser>());

        mgr.Setup(x => x.DeleteAsync(It.IsAny<TUser>())).ReturnsAsync(IdentityResult.Success);
        mgr.Setup(x => x.CreateAsync(It.IsAny<TUser>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success).Callback<TUser, string>((x, y) => users.Add(x));
        mgr.Setup(x => x.UpdateAsync(It.IsAny<TUser>())).ReturnsAsync(IdentityResult.Success);

        return mgr;
    }

    // *******************************************************************

    private static Mock<SignInManager<TUser>> MockSignInManger<TUser>(
        Mock<UserManager<TUser>> userManager
        ) where TUser : class
    {
        var httpContextAccessor = new Mock<IHttpContextAccessor>();
        var claimsFactory = new Mock<IUserClaimsPrincipalFactory<TUser>>();

        var mgr = new Mock<SignInManager<TUser>>(
            userManager.Object,
            httpContextAccessor.Object,
            claimsFactory.Object,
            null,
            null,
            null,
            null
            );

        return mgr;
    }

    #endregion
}