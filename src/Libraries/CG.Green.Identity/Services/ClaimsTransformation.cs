
namespace CG.Green.Identity.Services;

/// <summary>
/// This class is a default implementation of the <see cref="IClaimsTransformation"/>
/// interface.
/// </summary>
internal class ClaimsTransformation : IClaimsTransformation
{
    // *******************************************************************
    // Fields.
    // *******************************************************************

    #region Fields

    /// <summary>
    /// This field contains the sign in manager for the service.
    /// </summary>
    internal protected readonly SignInManager<GreenUser> _signInManager = null!;

    /// <summary>
    /// This field contains the logger for the service.
    /// </summary>
    internal protected readonly ILogger<IClaimsTransformation> _logger = null!;

    #endregion

    // *******************************************************************
    // Constructors.
    // *******************************************************************

    #region Constructors

    /// <summary>
    /// This constructor creates a new instance of the <see cref="ClaimsTransformation"/>
    /// class.
    /// </summary>
    /// <param name="signInManager">The sign in manager to use with the service.</param>
    /// <param name="logger">The logger to use with this service.</param>
    public ClaimsTransformation(
        SignInManager<GreenUser> signInManager,
        ILogger<IClaimsTransformation> logger
        )
    {
        // Validate the parameters before attempting to use them.
        Guard.Instance().ThrowIfNull(signInManager, nameof(signInManager))
            .ThrowIfNull(logger, nameof(logger));

        // Save the reference(s).
        _signInManager = signInManager;
        _logger = logger;
    }

    #endregion

    // *******************************************************************
    // Public methods.
    // *******************************************************************

    #region Public methods

    /// <summary>
    /// This method performs a claims transformation operation.
    /// </summary>
    /// <param name="principal">The principal to use for the operation.</param>
    /// <returns>A task to perform the operation that returns a <see cref="ClaimsPrincipal"/>
    /// object containing custom claims.</returns>
    public virtual async Task<ClaimsPrincipal> TransformAsync(
        ClaimsPrincipal principal
        )
    {
        // Validate the parameters before attempting to use them.
        Guard.Instance().ThrowIfNull(principal, nameof(principal));

        try
        {
            // Log what we are about to do.
            _logger.LogDebug(
                "Fetching the current user name"
                );

            // Get the user name.
            var userName = principal.Identity?.Name ?? "";

            // Log what we are about to do.
            _logger.LogDebug(
                "Looking for user: {userName}",
                userName
                );

            // Look for the user.
            var user = await _signInManager.UserManager.FindByNameAsync(
                userName
                );

            // Did we fail?
            if (user is null)
            {
                // Log what happened.
                _logger.LogWarning(
                    "Failed to find user: {userName}!",
                    userName
                    );
                return principal; // Nothing left to do!
            }

            // Log what we are about to do.
            _logger.LogDebug(
                "Looking for claims for user: {userName}",
                userName
                );

            // Look for any user claims.
            var userClaims = await _signInManager.UserManager.GetClaimsAsync(
                user
                );

            // Did we fail?
            if (!userClaims.Any())
            {
                // Log what we are about to do.
                _logger.LogDebug(
                    "No claims were found for user: {userName}",
                    userName
                    );

                return principal; // Nothing left to do!
            }

            // If we get here, the use has custom claims so we should inject
            //   them into an identity for this principal.

            // Log what we are about to do.
            _logger.LogDebug(
                "Looping through {count} custom claims for user: {user}",
                userClaims.Count,
                userName
                );

            // Loop and add claims to the identity.
            var claimsIdentity = new ClaimsIdentity();
            foreach (var userClaim in userClaims)
            {
                // Check for duplicates.
                if (!principal.HasClaim(claim => claim.Type == userClaim.Type))
                {
                    // Add the claim.
                    claimsIdentity.AddClaim(
                        new Claim(userClaim.Type, userClaim.Value)
                        );
                }
            }

            // Add the identity to the principal.
            principal.AddIdentity(claimsIdentity);
        }
        catch (Exception ex)
        {
            // Log what happened.
            _logger.LogError(
                ex,
                "Failed to transform claims!"
                );
        }

        // Return the results.
        return principal;
    }

    #endregion
}
