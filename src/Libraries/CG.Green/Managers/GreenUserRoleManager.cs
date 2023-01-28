
using static CG.Green.Globals.Models;

namespace CG.Green.Managers;

/// <summary>
/// This class is a default implementation of the <see cref="IGreenUserRoleManager"/>
/// interface.
/// </summary>
internal class GreenUserRoleManager : IGreenUserRoleManager
{
    // *******************************************************************
    // Fields.
    // *******************************************************************

    #region Fields

    /// <summary>
    /// This field contains the green user role repository for this manager.
    /// </summary>
    internal protected readonly IGreenUserRoleRepository _greenUserRoleRepository = null!;

    /// <summary>
    /// This field contains the ASP.NET role manager for this manager.
    /// </summary>
    internal protected readonly RoleManager<GreenRole> _roleManager = null!;

    /// <summary>
    /// This field contains the ASP.NET sign in manager for this manager.
    /// </summary>
    internal protected readonly SignInManager<GreenUser> _signInManager = null!;

    /// <summary>
    /// This field contains the logger for this manager.
    /// </summary>
    internal protected readonly ILogger<IGreenUserRoleManager> _logger = null!;

    #endregion

    // *******************************************************************
    // Constructors.
    // *******************************************************************

    #region Constructors

    /// <summary>
    /// This constructor creates a new instance of the <see cref="GreenUserRoleManager"/>
    /// class.
    /// </summary>
    /// <param name="greenUserRoleRepository">The green user role repository
    /// to use with this manager.</param>
    /// <param name="roleManager">The ASP.NET role manager to use with this
    /// manager.</param>
    /// <param name="signInManager">The ASP.NET sign-in manager to use with 
    /// this manager.</param>
    /// <param name="logger">The logger to use with this manager.</param>
    public GreenUserRoleManager(
        IGreenUserRoleRepository greenUserRoleRepository,
        RoleManager<GreenRole> roleManager,
        SignInManager<GreenUser> signInManager,
        ILogger<IGreenUserRoleManager> logger
        )
    {
        // Validate the arguments before attempting to use them.
        Guard.Instance().ThrowIfNull(greenUserRoleRepository, nameof(greenUserRoleRepository))
            .ThrowIfNull(roleManager, nameof(roleManager))
            .ThrowIfNull(signInManager, nameof(signInManager))
            .ThrowIfNull(logger, nameof(logger));

        // Save the reference(s).
        _greenUserRoleRepository = greenUserRoleRepository;
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
            var result = await _greenUserRoleRepository.AnyAsync(
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
                "Failed to search for user roles!"
                );

            // Provider better context.
            throw new ManagerException(
                message: $"The manager failed to search for user roles!",
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
            var result = await _greenUserRoleRepository.CountAsync(
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
                "Failed to count user roles!"
                );

            // Provider better context.
            throw new ManagerException(
                message: $"The manager failed to count user roles!",
                innerException: ex
                );
        }
    }

    // *******************************************************************

    /// <inheritdoc/>
    public virtual async Task<GreenUserRole> CreateAsync(
        string userEmail,
        string roleName,
        string userName,
        CancellationToken cancellationToken = default
        )
    {
        // Validate the arguments before attempting to use them.
        Guard.Instance().ThrowIfNullOrEmpty(userEmail, nameof(userEmail))
            .ThrowIfNullOrEmpty(roleName, nameof(roleName))
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

            // Look for the role.
            var role = await _roleManager.FindByNameAsync( 
                roleName
                ).ConfigureAwait(false);

            // Did we fail?
            if (role is null)
            {
                // Panic!!
                throw new KeyNotFoundException(
                    $"Failed to find role: {roleName}"
                    );
            }

            // Create the role assignment.
            var result = await _signInManager.UserManager.AddToRoleAsync(
                user,
                roleName
                ).ConfigureAwait(false);

            // Did we fail?
            if (!result.Succeeded)
            {
                // Panic!!
                throw new ManagerException(
                    string.Join("|", result.Errors.Select(x => x.Description))
                    );
            }

            // Return the results.
            return new GreenUserRole()
            {
                UserId = user.Id,
                RoleId = role.Id
            };
        }
        catch (Exception ex)
        {
            // Log what happened.
            _logger.LogError(
                ex,
                "Failed to create a user role!"
                );

            // Provider better context.
            throw new ManagerException(
                message: $"The manager failed to create a user role!",
                innerException: ex
                );
        }
    }

    // *******************************************************************

    /// <inheritdoc/>
    public virtual async Task<GreenUserRole> CreateAsync(
        GreenUserRole userRole,
        string userName,
        CancellationToken cancellationToken = default
        )
    {
        // Validate the arguments before attempting to use them.
        Guard.Instance().ThrowIfNull(userRole, nameof(userRole))
            .ThrowIfNullOrEmpty(userName, nameof(userName));

        try
        {
            // Look for the user.
            var user = await _signInManager.UserManager.FindByIdAsync(
                userRole.UserId
                ).ConfigureAwait(false);

            // Did we fail?
            if (user is null)
            {
                // Panic!!
                throw new KeyNotFoundException(
                    $"Failed to find user: {userRole.UserId}"
                    );
            }

            // Look for the role.
            var role = await _roleManager.FindByIdAsync(
                userRole.RoleId
                ).ConfigureAwait(false);

            // Did we fail?
            if (role is null)
            {
                // Panic!!
                throw new KeyNotFoundException(
                    $"Failed to find role: {userRole.RoleId}"
                    );
            }

            // Create the role assignment.
            var result = await _signInManager.UserManager.AddToRoleAsync(
                user,
                role.Name ?? ""
                ).ConfigureAwait(false);

            // Did we fail?
            if (!result.Succeeded)
            {
                // Panic!!
                throw new ManagerException(
                    string.Join("|", result.Errors.Select(x => x.Description))
                    );
            }

            // Return the results.
            return new GreenUserRole()
            {
                UserId = user.Id,
                RoleId = role.Id
            };
        }
        catch (Exception ex)
        {
            // Log what happened.
            _logger.LogError(
                ex,
                "Failed to create a user role!"
                );

            // Provider better context.
            throw new ManagerException(
                message: $"The manager failed to create a user role!",
                innerException: ex
                );
        }
    }

    // *******************************************************************

    /// <inheritdoc/>
    public virtual async Task<IEnumerable<string>> FindByUserIdAsync(
        string userId,
        CancellationToken cancellationToken = default
        )
    {
        // Validate the arguments before attempting to use them.
        Guard.Instance().ThrowIfNull(userId, nameof(userId));

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

            // Look for the user's roles.
            var userRoles = await _signInManager.UserManager.GetRolesAsync(
                user
                ).ConfigureAwait(false);

            // Return the results.
            return userRoles;
        }
        catch (Exception ex)
        {
            // Log what happened.
            _logger.LogError(
                ex,
                "Failed to search for user roles by user id!"
                );

            // Provider better context.
            throw new ManagerException(
                message: $"The manager failed to search for user roles by user id!",
                innerException: ex
                );
        }
    }

    // *******************************************************************

    /// <inheritdoc/>
    public virtual async Task<GreenUser> UpdateAsync(
        GreenUser greenUser,
        IEnumerable<string> userRoles,
        string userName,
        CancellationToken cancellationToken = default
        )
    {
        // Validate the arguments before attempting to use them.
        Guard.Instance().ThrowIfNull(greenUser, nameof(greenUser))
            .ThrowIfNull(userRoles, nameof(userRoles))
            .ThrowIfNullOrEmpty(userName, nameof(userName));

        try
        {
            // Look for any existing roles for this user.
            var existingRoles = await _signInManager.UserManager.GetRolesAsync(
                greenUser
                );

            // Did we fail?
            if (!existingRoles.Any())
            {
                // If we get here then the user doesn't have any roles
                //  assigned already so everything must be an addition.

                // Loop through the assigned roles.
                foreach (var userRole in userRoles)
                {
                    // Assign the role to the user.
                    await _signInManager.UserManager.AddToRoleAsync(
                        greenUser,
                        userRole
                        );
                }
            }
            else
            {
                // If we get here then we need to check for deletions, as
                //   well as additions.

                // Find any roles that were deleted.
                var deletedRoles = existingRoles.Where(p1 =>
                    userRoles.All(p2 => p2 != p1)
                    ).ToList();

                // Loop through the deleted roles.
                foreach (var userRole in deletedRoles)
                {
                    // Remove the role from the user.
                    await _signInManager.UserManager.RemoveFromRoleAsync(
                        greenUser,
                        userRole
                        );
                }

                // Find any roles that were added.
                var addedRoles = userRoles.Where(p1 =>
                    existingRoles.All(p2 => p2 != p1)
                    ).ToList();

                // Loop through the added roles.
                foreach (var userRole in addedRoles)
                {
                    // Assign the role to the user.
                    await _signInManager.UserManager.AddToRoleAsync(
                        greenUser,
                        userRole
                        );
                }                
            }

            // Return the results.
            return greenUser;
        }
        catch (Exception ex)
        {
            // Log what happened.
            _logger.LogError(
                ex,
                "Failed to update user roles for a given user!"
                );

            // Provider better context.
            throw new ManagerException(
                message: $"The manager failed to update user roles for a given user!",
                innerException: ex
                );
        }
    }

    #endregion
}
