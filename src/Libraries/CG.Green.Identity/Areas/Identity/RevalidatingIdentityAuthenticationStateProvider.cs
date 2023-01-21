
namespace CG.Green.Areas.Identity;

/// <summary>
/// This class receive an authentication state from the host environment, 
/// and revalidates that state at regular intervals.
/// </summary>
/// <typeparam name="TUser">The type of associated user class.</typeparam>
public class RevalidatingIdentityAuthenticationStateProvider<TUser>
    : RevalidatingServerAuthenticationStateProvider where TUser : class
{
    // *******************************************************************
    // Fields.
    // *******************************************************************

    #region Fields
    
    /// <summary>
    /// This field contains the scope factory for the provider.
    /// </summary>
    internal protected readonly IServiceScopeFactory _scopeFactory;

    /// <summary>
    /// This field contains the options for the provider.
    /// </summary>
    internal protected readonly IdentityOptions _options;

    #endregion

    // *******************************************************************
    // Properties.
    // *******************************************************************

    #region Properties

    /// <summary>
    /// This property contains the interval for revalidation.
    /// </summary>
    protected override TimeSpan RevalidationInterval => TimeSpan.FromMinutes(30);

    #endregion

    // *******************************************************************
    // Constructors.
    // *******************************************************************

    #region Constructors

    /// <summary>
    /// This constructor creates a new instance of the <see cref="RevalidatingIdentityAuthenticationStateProvider{TUser}"/>
    /// class.
    /// </summary>
    /// <param name="loggerFactory">The logger factory to use with this
    /// provider.</param>
    /// <param name="scopeFactory">The scope factory to use with this
    /// provider.</param>
    /// <param name="optionsAccessor">The options to use with this provider.</param>
    public RevalidatingIdentityAuthenticationStateProvider(
        ILoggerFactory loggerFactory,
        IServiceScopeFactory scopeFactory,
        IOptions<IdentityOptions> optionsAccessor
        ) : base(loggerFactory)
    {
        // Validate the arguments before attempting to use them.
        Guard.Instance().ThrowIfNull(loggerFactory, nameof(loggerFactory))
            .ThrowIfNull(scopeFactory, nameof(scopeFactory))
            .ThrowIfNull(optionsAccessor, nameof(optionsAccessor));

        // Save the reference(s).
        _scopeFactory = scopeFactory;
        _options = optionsAccessor.Value;
    }

    #endregion

    // *******************************************************************
    // Protected methods.
    // *******************************************************************

    #region Protected methods
    
    /// <summary>
    /// This method performs the validation.
    /// </summary>
    /// <param name="authenticationState">The authentication state to use
    /// for the operation.</param>
    /// <param name="cancellationToken">A cancellation token that is monitored
    /// throughout the lifetime of the method.</param>
    /// <returns>A task to perform the operation that returns <c>true</c>
    /// if the user is authenticated, or <c>false</c> otherwise.</returns>
    protected override async Task<bool> ValidateAuthenticationStateAsync(
        AuthenticationState authenticationState, 
        CancellationToken cancellationToken
        )
    {
        // Validate the arguments before attempting to use them.
        Guard.Instance().ThrowIfNull(authenticationState, nameof(authenticationState));

        // Get the user manager from a new scope to ensure it fetches fresh data
        var scope = _scopeFactory.CreateScope();
        try
        {
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<TUser>>();
            return await ValidateSecurityStampAsync(userManager, authenticationState.User);
        }
        finally
        {
            if (scope is IAsyncDisposable asyncDisposable)
            {
                await asyncDisposable.DisposeAsync();
            }
            else
            {
                scope.Dispose();
            }
        }
    }

    #endregion

    // *******************************************************************
    // Private methods.
    // *******************************************************************

    #region Private methods

    /// <summary>
    /// This method validates whether or not the security stamp for the 
    /// current user has changed.
    /// </summary>
    /// <param name="userManager">The user manager to use for the operation.</param>
    /// <param name="principal">The claims principal to use for the operation.</param>
    /// <returns>A task to perform the operation that returns <c>true</c>
    /// if the user's security stamp has changed, or <c>false</c> otherwise.</returns>
    private async Task<bool> ValidateSecurityStampAsync(
        UserManager<TUser> userManager, 
        ClaimsPrincipal principal
        )
    {
        var user = await userManager.GetUserAsync(principal);
        if (user == null)
        {
            return false;
        }
        else if (!userManager.SupportsUserSecurityStamp)
        {
            return true;
        }
        else
        {
            var principalStamp = principal.FindFirstValue(_options.ClaimsIdentity.SecurityStampClaimType);
            var userStamp = await userManager.GetSecurityStampAsync(user);
            return principalStamp == userStamp;
        }
    }

    #endregion
}