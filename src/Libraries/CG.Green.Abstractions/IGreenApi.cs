
namespace CG.Green;

/// <summary>
/// This interface represents the API for the <see cref="CG.Green"/> microservice.
/// </summary>
public interface IGreenApi
{
    /// <summary>
    /// This property contains an object for performing api scope related operations.
    /// </summary>
    IApiScopeManager ApiScopes { get; }

    /// <summary>
    /// This property contains an object for performing client related operations.
    /// </summary>
    IClientManager Clients { get; }

    /// <summary>
    /// This property contains an object for performing identity resource operations.
    /// </summary>
    IIdentityResourceManager IdentityResources { get; }

    /// <summary>
    /// This property contains an object for performing role operations.
    /// </summary>
    IGreenRoleManager Roles { get; }

    /// <summary>
    /// This property contains an object for performing role claim operations.
    /// </summary>
    IGreenRoleClaimManager RoleClaims { get; }

    /// <summary>
    /// This property contains an object for performing user operations.
    /// </summary>
    IGreenUserManager Users { get; }

    /// <summary>
    /// This property contains an object for performing user claim operations.
    /// </summary>
    IGreenUserClaimManager UserClaims { get; }

    /// <summary>
    /// This property contains an object for performing user role operations.
    /// </summary>
    IGreenUserRoleManager UserRoles { get; }
}
