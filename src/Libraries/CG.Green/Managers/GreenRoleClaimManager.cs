
using CG.Green.Identity.Models;
using CG.Green.Repositories;

namespace CG.Green.Managers;

/// <summary>
/// This class is a default implementation of the <see cref="IGreenRoleClaimManager"/>
/// interface.
/// </summary>
internal class GreenRoleClaimManager : IGreenRoleClaimManager
{
    // *******************************************************************
    // Fields.
    // *******************************************************************

    #region Fields

    /// <summary>
    /// This field contains the green role claim repository for this manager.
    /// </summary>
    internal protected readonly IGreenRoleClaimRepository _greenRoleClaimRepository = null!;

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
    internal protected readonly ILogger<IGreenRoleClaimManager> _logger = null!;

    #endregion

    // *******************************************************************
    // Constructors.
    // *******************************************************************

    #region Constructors

    /// <summary>
    /// This constructor creates a new instance of the <see cref="GreenRoleClaimManager"/>
    /// class.
    /// </summary>
    /// <param name="greenRoleClaimRepository">The green role claim repository
    /// to use with this manager.</param>
    /// <param name="roleManager">The ASP.NET claim manager to use with this
    /// manager.</param>
    /// <param name="signInManager">The ASP.NET sign-in manager to use with 
    /// this manager.</param>
    /// <param name="logger">The logger to use with this manager.</param>
    public GreenRoleClaimManager(
        IGreenRoleClaimRepository greenRoleClaimRepository,
        RoleManager<GreenRole> roleManager,
        SignInManager<GreenUser> signInManager,
        ILogger<IGreenRoleClaimManager> logger
        )
    {
        // Validate the arguments before attempting to use them.
        Guard.Instance().ThrowIfNull(greenRoleClaimRepository, nameof(greenRoleClaimRepository))
            .ThrowIfNull(roleManager, nameof(roleManager))
            .ThrowIfNull(signInManager, nameof(signInManager))
            .ThrowIfNull(logger, nameof(logger));

        // Save the reference(s).
        _greenRoleClaimRepository = greenRoleClaimRepository;
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
            var result = await _greenRoleClaimRepository.AnyAsync(
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
                "Failed to search for role claims!"
                );

            // Provider better context.
            throw new ManagerException(
                message: $"The manager failed to search for role claims!",
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
            var result = await _greenRoleClaimRepository.CountAsync(
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
                "Failed to count role claims!"
                );

            // Provider better context.
            throw new ManagerException(
                message: $"The manager failed to count role claims!",
                innerException: ex
                );
        }
    }

    // *******************************************************************

    /// <inheritdoc/>
    public virtual async Task<GreenRoleClaim> CreateAsync(
        GreenRoleClaim roleClaim,
        string userName,
        CancellationToken cancellationToken = default
        )
    {
        // Validate the arguments before attempting to use them.
        Guard.Instance().ThrowIfNull(roleClaim, nameof(roleClaim))
            .ThrowIfNullOrEmpty(userName, nameof(userName));

        try
        {
            // Look for the role.
            var role = await _roleManager.FindByIdAsync(
                roleClaim.RoleId
                ).ConfigureAwait(false);

            // Did we fail?
            if (role is null)
            {
                // Panic!!
                throw new KeyNotFoundException(
                    $"Failed to find role: {roleClaim.RoleId}"
                    );
            }

            // Assign the claim to the role.
            var result = await _roleManager.AddClaimAsync(
                role,
                new Claim(
                    roleClaim.ClaimType ?? "", 
                    roleClaim.ClaimValue ?? ""
                    )
                );

            // Did we fail?
            if (!result.Succeeded)
            {
                // Panic!!
                throw new ManagerException(
                    string.Join("|", result.Errors.Select(x => x.Description))
                    );
            }

            // Return the results.
            return roleClaim;
        }
        catch (Exception ex)
        {
            // Log what happened.
            _logger.LogError(
                ex,
                "Failed to create a role claim!"
                );

            // Provider better context.
            throw new ManagerException(
                message: $"The manager failed to create a role claim!",
                innerException: ex
                );
        }
    }

    // *******************************************************************

    /// <inheritdoc/>
    public virtual async Task<IEnumerable<GreenRoleClaim>> FindByRoleIdAsync(
        string roleId,
        CancellationToken cancellationToken = default
        )
    {
        // Validate the arguments before attempting to use them.
        Guard.Instance().ThrowIfNullOrEmpty(roleId, nameof(roleId));

        try
        {
            // Look for the role.
            var role = await _roleManager.FindByIdAsync(
                roleId
                ).ConfigureAwait(false);

            // Did we fail?
            if (role is null)
            {
                // Panic!!
                throw new KeyNotFoundException(
                    $"Failed to find role: {roleId}"
                    );
            }

            // Get the role's claims.
            var claims = (await _roleManager.GetClaimsAsync(
                role
                )).Select(x => new GreenRoleClaim()
                {
                    ClaimType = x.Type,
                    ClaimValue = x.Value,
                    RoleId = roleId
                }).ToList();

            // Return the results.
            return claims;
        }
        catch (Exception ex)
        {
            // Log what happened.
            _logger.LogError(
                ex,
                "Failed to search for role claims by role id!"
                );

            // Provider better context.
            throw new ManagerException(
                message: $"The manager failed to search for role " +
                "claims by role id",
                innerException: ex
                );
        }
    }

    // *******************************************************************

    /// <inheritdoc/>
    public virtual async Task<GreenRole> UpdateAsync(
       GreenRole greenRole,
       IEnumerable<GreenRoleClaim> roleClaims,
       string userName,
       CancellationToken cancellationToken = default
       )
    {
        // Validate the arguments before attempting to use them.
        Guard.Instance().ThrowIfNull(greenRole, nameof(greenRole))
            .ThrowIfNull(roleClaims, nameof(roleClaims))
            .ThrowIfNullOrEmpty(userName, nameof(userName));

        try
        {
            // Get the role's existing claims.
            var existingClaims = (await _roleManager.GetClaimsAsync(
                greenRole
                )).ToList();

            // Did we fail?
            if (!existingClaims.Any())
            {
                // If we get here then the role doesn't have any claims
                //  assigned already so everything must be an addition.

                // Loop through the assigned claims.
                foreach (var roleClaim in roleClaims)
                {
                    // Assign the claim to the role.
                    await _roleManager.AddClaimAsync(
                        greenRole,
                        new Claim(
                            roleClaim.ClaimType ?? "",
                            roleClaim.ClaimValue ?? ""
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
                    roleClaims.All(p2 => p2.GetHashCode() != p1.GetHashCode())
                    ).ToList();

                // Loop through the deleted claims.
                foreach (var roleClaim in deletedClaims)
                {
                    // Remove the claim from the role.
                    await _roleManager.RemoveClaimAsync(
                        greenRole,
                        new Claim(
                            roleClaim.Type ?? "",
                            roleClaim.Value ?? ""
                            )
                        );
                }

                // Find any claims that were added.
                var addedClaims = roleClaims.Where(p1 =>
                    existingClaims.All(p2 => p2.GetHashCode() != p1.GetHashCode())
                    ).ToList();

                // Loop through the added claims.
                foreach (var roleClaim in addedClaims)
                {
                    // Assign the claim to the role.
                    await _roleManager.AddClaimAsync(
                        greenRole,
                        new Claim(
                            roleClaim.ClaimType ?? "",
                            roleClaim.ClaimValue ?? ""
                            )
                        );
                }
            }

            // Return the result.
            return greenRole;
        }
        catch (Exception ex)
        {
            // Log what happened.
            _logger.LogError(
                ex,
                "Failed to update the claims for a role!"
                );

            // Provider better context.
            throw new ManagerException(
                message: $"The manager failed to update the claims for a role!",
                innerException: ex
                );
        }
    }

    #endregion
}
