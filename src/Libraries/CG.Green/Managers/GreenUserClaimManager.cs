
namespace CG.Green.Managers;

/// <summary>
/// This class is a default implementation of the <see cref="IGreenUserClaimManager"/>
/// interface.
/// </summary>
internal class GreenUserClaimManager : IGreenUserClaimManager
{
    // *******************************************************************
    // Fields.
    // *******************************************************************

    #region Fields

    /// <summary>
    /// This field contains the green user claim repository for this manager.
    /// </summary>
    internal protected readonly IGreenUserClaimRepository _greenUserClaimRepository = null!;

    /// <summary>
    /// This field contains the ASP.NET claim manager for this manager.
    /// </summary>
    internal protected readonly RoleManager<GreenRole> _roleManager = null!;

    /// <summary>
    /// This field contains the ASP.NET sign in manager for this manager.
    /// </summary>
    internal protected readonly SignInManager<GreenUser> _signInManager = null!;

    /// <summary>
    /// This field contains the logger for this manager.
    /// </summary>
    internal protected readonly ILogger<IGreenUserClaimManager> _logger = null!;

    #endregion

    // *******************************************************************
    // Constructors.
    // *******************************************************************

    #region Constructors

    /// <summary>
    /// This constructor creates a new instance of the <see cref="GreenUserClaimManager"/>
    /// class.
    /// </summary>
    /// <param name="greenUserClaimRepository">The green user claim repository
    /// to use with this manager.</param>
    /// <param name="roleManager">The ASP.NET claim manager to use with this
    /// manager.</param>
    /// <param name="signInManager">The ASP.NET sign-in manager to use with 
    /// this manager.</param>
    /// <param name="logger">The logger to use with this manager.</param>
    public GreenUserClaimManager(
        IGreenUserClaimRepository greenUserClaimRepository,
        RoleManager<GreenRole> roleManager,
        SignInManager<GreenUser> signInManager,
        ILogger<IGreenUserClaimManager> logger
        )
    {
        // Validate the arguments before attempting to use them.
        Guard.Instance().ThrowIfNull(greenUserClaimRepository, nameof(greenUserClaimRepository))
            .ThrowIfNull(roleManager, nameof(roleManager))
            .ThrowIfNull(signInManager, nameof(signInManager))
            .ThrowIfNull(logger, nameof(logger));

        // Save the reference(s).
        _greenUserClaimRepository = greenUserClaimRepository;
        _roleManager = roleManager;
        _signInManager = signInManager;
        _logger = logger;
    }

    #endregion

    // *******************************************************************
    // Public methods.
    // *******************************************************************

    #region Public methods

    /// <inheritdoc/>
    public virtual async Task<bool> AnyAsync(
        CancellationToken cancellationToken = default
        )
    {
        try
        {
            // Perform the search.
            var result = await _greenUserClaimRepository.AnyAsync(
                cancellationToken
                ).ConfigureAwait(false);

            // Return the results,
            return result;
        }
        catch (Exception ex)
        {
            // Log what happened.
            _logger.LogError(
                ex,
                "Failed to search for user claims!"
                );

            // Provider better context.
            throw new ManagerException(
                message: $"The manager failed to search for user claims!",
                innerException: ex
                );
        }
    }

    // *******************************************************************

    /// <inheritdoc/>
    public virtual async Task<long> CountAsync(
        CancellationToken cancellationToken = default
        )
    {
        try
        {
            // Perform the search.
            var result = await _greenUserClaimRepository.CountAsync(
                cancellationToken
                ).ConfigureAwait(false);

            // Return the results.
            return result;
        }
        catch (Exception ex)
        {
            // Log what happened.
            _logger.LogError(
                ex,
                "Failed to count user claims!"
                );

            // Provider better context.
            throw new ManagerException(
                message: $"The manager failed to count user claims!",
                innerException: ex
                );
        }
    }

    // *******************************************************************

    /// <inheritdoc/>
    public virtual async Task<GreenUserClaim> CreateAsync(
        string userEmail,
        string claimType,
        string claimValue,
        string userName,
        CancellationToken cancellationToken = default
        )
    {
        // Validate the arguments before attempting to use them.
        Guard.Instance().ThrowIfNullOrEmpty(userEmail, nameof(userEmail))
            .ThrowIfNullOrEmpty(claimType, nameof(claimType))
            .ThrowIfNullOrEmpty(claimValue, nameof(claimValue))
            .ThrowIfNullOrEmpty(userName, nameof(userName));

        try
        {
            // Look for the user.
            var user = await _signInManager.UserManager.FindByEmailAsync(
                userEmail
                ).ConfigureAwait(false);

            // Did we fail?
            if (user is null)
            {
                // Panic!!
                throw new KeyNotFoundException(
                    $"Failed to find user: {userEmail}"
                    );
            }

            // Create the user role assignment.
            var result = await _signInManager.UserManager.AddClaimAsync(
                user,
                new Claim(claimType, claimValue)
                );

            // Did we fail?
            if (!result.Succeeded)
            {
                // Panic!!
                throw new ManagerException(
                    $"ASP.NET user manager errors: {string.Join(",", result.Errors)}"
                    );
            }

            // Return the results.
            return new GreenUserClaim()
            {
                ClaimType = claimType,
                ClaimValue = claimValue,
                UserId = user.Id,   
            };
        }
        catch (Exception ex)
        {
            // Log what happened.
            _logger.LogError(
                ex,
                "Failed to create a user claim!"
                );

            // Provider better context.
            throw new ManagerException(
                message: $"The manager failed to create a user claim!",
                innerException: ex
                );
        }
    }

    // *******************************************************************

    /// <inheritdoc/>
    public virtual async Task<GreenUserClaim> CreateAsync(
        GreenUserClaim userClaim,
        string userName,
        CancellationToken cancellationToken = default
        )
    {
        // Validate the arguments before attempting to use them.
        Guard.Instance().ThrowIfNull(userClaim, nameof(userClaim))
            .ThrowIfNullOrEmpty(userName, nameof(userName));

        try
        {
            // Look for the user.
            var user = await _signInManager.UserManager.FindByIdAsync(
                userClaim.UserId
                ).ConfigureAwait(false);

            // Did we fail?
            if (user is null)
            {
                // Panic!!
                throw new KeyNotFoundException(
                    $"Failed to find user: {userClaim.UserId}"
                );
            }

            // Assign the claim to the user.
            var result = await _signInManager.UserManager.AddClaimAsync(
                user,
                new Claim(
                    userClaim.ClaimType ?? "",
                    userClaim.ClaimValue ?? ""
                    )
                );

            // Did we fail?
            if (!result.Succeeded)
            {
                // Panic!!
                throw new ManagerException(
                    $"ASP.NET user manager errors: {string.Join(",", result.Errors)}"
                    );
            }

            // Return the results.
            return userClaim;
        }
        catch (Exception ex)
        {
            // Log what happened.
            _logger.LogError(
                ex,
                "Failed to create a user claim!"
                );

            // Provider better context.
            throw new ManagerException(
                message: $"The manager failed to create a user claim!",
                innerException: ex
                );
        }
    }

    // *******************************************************************

    /// <inheritdoc/>
    public virtual async Task<IEnumerable<GreenUserClaim>> FindByUserIdAsync(
        string userId,
        CancellationToken cancellationToken = default
        )
    {
        // Validate the arguments before attempting to use them.
        Guard.Instance().ThrowIfNullOrEmpty(userId, nameof(userId));

        try
        {
            // Look for the user.
            var user = await _signInManager.UserManager.FindByIdAsync(
                userId
                ).ConfigureAwait(false);

            // Did we fail?
            if (user is null)
            {
                // Panic!!
                throw new KeyNotFoundException(
                    $"Failed to find user: {userId}"
                    );
            }

            // Get the user's claims.
            var claims = (await _signInManager.UserManager.GetClaimsAsync(
                user
                )).Select(x => new GreenUserClaim()
                {
                    ClaimType = x.Type,
                    ClaimValue = x.Value,
                    UserId = userId
                }).ToList();

            // Return the results.
            return claims;
        }
        catch (Exception ex)
        {
            // Log what happened.
            _logger.LogError(
                ex,
                "Failed to search for user claims by user id!"
                );

            // Provider better context.
            throw new ManagerException(
                message: $"The manager failed to search for user " +
                "claims by user id",
                innerException: ex
                );
        }
    }

    // *******************************************************************

    /// <inheritdoc/>
    public virtual async Task<GreenUser> UpdateAsync(
        GreenUser greenUser,
        IEnumerable<GreenUserClaim> userClaims,
        string userName,
        CancellationToken cancellationToken = default
        )
    {
        // Validate the arguments before attempting to use them.
        Guard.Instance().ThrowIfNull(greenUser, nameof(greenUser))
            .ThrowIfNull(userClaims, nameof(userClaims))
            .ThrowIfNullOrEmpty(userName, nameof(userName));

        try
        {
            // Get the user's existing claims.
            var existingClaims = (await _signInManager.UserManager.GetClaimsAsync(
                greenUser
                )).ToList();

            // Did we fail?
            if (!existingClaims.Any())
            { 
                // If we get here then the user doesn't have any claims
                //  assigned already so everything must be an addition.

                // Loop through the assigned claims.
                foreach (var userClaim in userClaims)
                {
                    // Assign the claim to the user.
                    await _signInManager.UserManager.AddClaimAsync(
                        greenUser,
                        new Claim(
                            userClaim.ClaimType ?? "",
                            userClaim.ClaimValue ?? ""
                            )
                        );
                }
            }
            else
            {
                // If we get here then we need to check for deletions, as
                //   well as additions.

                // Find any claims that were deleted.
                var deletedClaims = existingClaims.Where(p1 =>
                    userClaims.All(p2 => p2.GetHashCode() != p1.GetHashCode())
                    ).ToList();

                // Loop through the deleted claims.
                foreach (var userClaim in deletedClaims)
                {
                    // Remove the claim from the user.
                    await _signInManager.UserManager .RemoveClaimAsync(
                        greenUser,
                        new Claim(
                            userClaim.Type ?? "",
                            userClaim.Value ?? ""
                            )
                        );
                }

                // Find any claims that were added.
                var addedClaims = userClaims.Where(p1 =>
                    existingClaims.All(p2 => p2.GetHashCode() != p1.GetHashCode())
                    ).ToList();

                // Loop through the added claims.
                foreach (var userClaim in addedClaims)
                {
                    // Assign the claim to the user.
                    await _signInManager.UserManager.AddClaimAsync(
                        greenUser,
                        new Claim(
                            userClaim.ClaimType ?? "",
                            userClaim.ClaimValue ?? ""
                            )
                        );
                }
            }

            // Return the result.
            return greenUser;
        }
        catch (Exception ex)
        {
            // Log what happened.
            _logger.LogError(
                ex,
                "Failed to update the claims for a user!"
                );

            // Provider better context.
            throw new ManagerException(
                message: $"The manager failed to update the claims for a user!",
                innerException: ex
                );
        }
    }

    #endregion
}
