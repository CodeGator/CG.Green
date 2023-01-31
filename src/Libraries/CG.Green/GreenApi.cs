
namespace CG.Green;

/// <summary>
/// This class is a default implementation of the <see cref="IGreenApi"/>
/// interface.
/// </summary>
internal class GreenApi : IGreenApi
{
    // *******************************************************************
    // Fields.
    // *******************************************************************

    #region Fields

    /// <summary>
    /// This field backs the <see cref="IGreenApi.ApiScopes"/> property.
    /// </summary>
    internal protected readonly IApiScopeManager _apiScopeManager = null!;

    /// <summary>
    /// This field backs the <see cref="IGreenApi.Clients"/> property.
    /// </summary>
    internal protected readonly IClientManager _clientManager = null!;

    /// <summary>
    /// This field backs the <see cref="IGreenApi.IdentityResources"/> property.
    /// </summary>
    internal protected readonly IIdentityResourceManager _identityResourceManager = null!;

    /// <summary>
    /// This field backs the <see cref="IGreenApi.Roles"/> property.
    /// </summary>
    internal protected readonly IGreenRoleManager _greenRoleManager = null!;

    /// <summary>
    /// This field backs the <see cref="IGreenApi.RoleClaims"/> property.
    /// </summary>
    internal protected readonly IGreenRoleClaimManager _greenRoleClaimManager = null!;

    /// <summary>
    /// This field backs the <see cref="IGreenApi.Users"/> property.
    /// </summary>
    internal protected readonly IGreenUserManager _greenUserManager = null!;

    /// <summary>
    /// This field backs the <see cref="IGreenApi.UserClaims"/> property.
    /// </summary>
    internal protected readonly IGreenUserClaimManager _greenUserClaimManager = null!;

    /// <summary>
    /// This field backs the <see cref="IGreenApi.UserRoles"/> property.
    /// </summary>
    internal protected readonly IGreenUserRoleManager _greenUserRoleManager = null!;

    #endregion

    // *******************************************************************
    // Properties.
    // *******************************************************************

    #region Properties

    /// <inheritdoc/>
    public IApiScopeManager ApiScopes => _apiScopeManager;

    /// <inheritdoc/>
    public IClientManager Clients => _clientManager;

    /// <inheritdoc/>
    public IIdentityResourceManager IdentityResources => _identityResourceManager;

    /// <inheritdoc/>
    public IGreenRoleManager Roles => _greenRoleManager;

    /// <inheritdoc/>
    public IGreenRoleClaimManager RoleClaims => _greenRoleClaimManager;

    /// <inheritdoc/>
    public IGreenUserManager Users => _greenUserManager;

    /// <inheritdoc/>
    public IGreenUserClaimManager UserClaims => _greenUserClaimManager;

    /// <inheritdoc/>
    public IGreenUserRoleManager UserRoles => _greenUserRoleManager;

    #endregion

    // *******************************************************************
    // Constructors.
    // *******************************************************************

    #region Constructors

    /// <summary>
    /// This constructor creates a new instance of the <see cref="GreenApi"/>
    /// class.
    /// </summary>
    /// <param name="apiScopeManager">The API scope manager to use with this API.</param>
    /// <param name="clientManager">The client manager to use with this API.</param>
    /// <param name="greenRoleManager">The green role manager to use with this API.</param>
    /// <param name="greenRoleClaimManager">The green role claim manager to use with this API.</param>
    /// <param name="greenUserManager">The green user manager to use with this API.</param>
    /// <param name="greenUserClaimManager">The green user claim manager to use with this API.</param>
    /// <param name="greenUserRoleManager">The green user role manager to use with this API.</param>
    /// <param name="identityResourceManager">The identity resource manager to use with this API.</param>
    public GreenApi(
        IApiScopeManager apiScopeManager,
        IClientManager clientManager,
        IGreenRoleManager greenRoleManager,
        IGreenRoleClaimManager greenRoleClaimManager,
        IGreenUserManager greenUserManager,
        IGreenUserClaimManager greenUserClaimManager,
        IGreenUserRoleManager greenUserRoleManager,
        IIdentityResourceManager identityResourceManager        
        )
    {
        // Validate the parameters before attempting to use them.
        Guard.Instance().ThrowIfNull(apiScopeManager, nameof(apiScopeManager))
            .ThrowIfNull(clientManager, nameof(clientManager))
            .ThrowIfNull(greenRoleManager, nameof(greenRoleManager))
            .ThrowIfNull(greenRoleClaimManager, nameof(greenRoleClaimManager))
            .ThrowIfNull(greenUserManager, nameof(greenUserManager))
            .ThrowIfNull(greenUserRoleManager, nameof(greenUserRoleManager))
            .ThrowIfNull(greenUserClaimManager, nameof(greenUserClaimManager))
            .ThrowIfNull(identityResourceManager, nameof(identityResourceManager));

        // Save the reference(s).
        _apiScopeManager = apiScopeManager;
        _clientManager = clientManager;
        _greenRoleManager = greenRoleManager;
        _greenRoleClaimManager = greenRoleClaimManager;
        _greenUserManager = greenUserManager;
        _greenUserClaimManager = greenUserClaimManager;
        _greenUserRoleManager = greenUserRoleManager;
        _identityResourceManager = identityResourceManager;
    }

    #endregion
}
