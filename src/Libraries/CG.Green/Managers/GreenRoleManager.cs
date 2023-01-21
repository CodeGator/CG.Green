
namespace CG.Green.Managers;

/// <summary>
/// This class is a default implementation of the <see cref="IGreenRoleManager"/>
/// interface.
/// </summary>
internal class GreenRoleManager : IGreenRoleManager
{
    // *******************************************************************
    // Fields.
    // *******************************************************************

    #region Fields

    /// <summary>
    /// This field contains the ASP.NET role manager for this manager.
    /// </summary>
    internal protected readonly RoleManager<GreenRole> _roleManager;

    /// <summary>
    /// This field contains the logger for this manager.
    /// </summary>
    internal protected readonly ILogger<IGreenRoleManager> _logger;

    #endregion

    // *******************************************************************
    // Constructors.
    // *******************************************************************

    #region Constructors

    /// <summary>
    /// This constructor creates a new instance of the <see cref="GreenRoleManager"/>
    /// class.
    /// </summary>
    /// <param name="roleManager">The ASP.NET role manager to use with 
    /// this manager.</param>
    /// <param name="logger">The logger to use with this manager.</param>
    public GreenRoleManager(
        RoleManager<GreenRole> roleManager,
        ILogger<IGreenRoleManager> logger
        )
    {
        // Validate the arguments before attempting to use them.
        Guard.Instance().ThrowIfNull(roleManager, nameof(roleManager))
            .ThrowIfNull(logger, nameof(logger));

        // Save the reference(s).
        _roleManager = roleManager;
        _logger = logger;
    }

    #endregion

    // *******************************************************************
    // Public methods.
    // *******************************************************************

    #region Public methods

    /// <inheritdoc/>
    public virtual Task<bool> AnyAsync(
        CancellationToken cancellationToken = default
        )
    {
        try
        {
            // Check the repository for the data.
            var result = _roleManager.Roles.Any();

            // Return the results,
            return Task.FromResult(result);
        }
        catch (Exception ex)
        {
            // Log what happened.
            _logger.LogError(
                ex,
                "Failed to search for roles!"
                );

            // Provider better context.
            throw new ManagerException(
                message: $"The manager failed to search for roles!",
                innerException: ex
                );
        }
    }

    // *******************************************************************

    /// <inheritdoc/>
    public virtual Task<int> CountAsync(
        CancellationToken cancellationToken = default
        )
    {
        try
        {
            // Perform the search.
            var result = _roleManager.Roles.Count();

            // Return the results.
            return Task.FromResult(result);
        }
        catch (Exception ex)
        {
            // Log what happened.
            _logger.LogError(
                ex,
                "Failed to count roles!"
                );

            // Provider better context.
            throw new ManagerException(
                message: $"The manager failed to count roles!",
                innerException: ex
                );
        }
    }

    // *******************************************************************

    /// <inheritdoc/>
    public virtual async Task<GreenRole> CreateAsync(
        GreenRole greenRole,
        string roleName,
        CancellationToken cancellationToken = default
        )
    {
        // Validate the parameters before attempting to use them.
        Guard.Instance().ThrowIfNull(greenRole, nameof(greenRole))
            .ThrowIfNullOrEmpty(roleName, nameof(roleName));

        try
        {
            // Create the new role.
            var result = await _roleManager.CreateAsync(
                greenRole
                ).ConfigureAwait(false);

            // Did we fail?
            if (!result.Succeeded)
            {
                // Panic!!
                throw new ManagerException(
                    $"ASP.NET role manager errors: {string.Join(",", result.Errors)}"
                    );
            }

            // Look for the new role.
            var newUser = await _roleManager.FindByNameAsync(
                greenRole.Name ?? ""
                ).ConfigureAwait(false);

            // Did we fail?
            if (newUser is null)
            {
                // Panic!!
                throw new KeyNotFoundException(
                    $"Failed to find role: {greenRole.Name}"
                    );
            }

            // Return the results.
            return newUser;
        }
        catch (Exception ex)
        {
            // Log what happened.
            _logger.LogError(
                ex,
                "Failed to create a new role!"
                );

            // Provider better context.
            throw new ManagerException(
                message: $"The manager failed to create a new role!",
                innerException: ex
                );
        }
    }

    // *******************************************************************

    /// <inheritdoc/>
    public virtual async Task DeleteAsync(
        GreenRole greenRole,
        string roleName,
        CancellationToken cancellationToken = default
        )
    {
        // Validate the parameters before attempting to use them.
        Guard.Instance().ThrowIfNull(greenRole, nameof(greenRole))
            .ThrowIfNullOrEmpty(roleName, nameof(roleName));

        try
        {
            // Look for the role.
            var role = await _roleManager.FindByNameAsync(
                greenRole.Name ?? ""
                ).ConfigureAwait(false);

            // Did we fail?
            if (role is null)
            {
                // Panic!!
                throw new KeyNotFoundException(
                    $"Failed to find role: {greenRole.Name}"
                    );
            }

            // Delete the role.
            var result = await _roleManager.DeleteAsync(
                role
                ).ConfigureAwait(false);

            // Did we fail?
            if (!result.Succeeded)
            {
                // Panic!!
                throw new ManagerException(
                    $"ASP.NET role manager errors: {string.Join(",", result.Errors)}"
                    );
            }
        }
        catch (Exception ex)
        {
            // Log what happened.
            _logger.LogError(
                ex,
                "Failed to delete a role"
                );

            // Provider better context.
            throw new ManagerException(
                message: $"The manager failed to delete a role!",
                innerException: ex
                );
        }
    }

    // *******************************************************************

    /// <inheritdoc/>
    public virtual Task<IEnumerable<GreenRole>> FindAllAsync(
        CancellationToken cancellationToken = default
        )
    {
        try
        {
            // Look for the roles.
            var roles = _roleManager.Roles.AsEnumerable();

            // Return the results.
            return Task.FromResult(roles);
        }
        catch (Exception ex)
        {
            // Log what happened.
            _logger.LogError(
                ex,
                "Failed to search for roles!"
                );

            // Provider better context.
            throw new ManagerException(
                message: $"The manager failed to search for roles!",
                innerException: ex
                );
        }
    }

    // *******************************************************************

    /// <inheritdoc/>
    public virtual async Task<GreenRole> UpdateAsync(
        GreenRole greenRole,
        string userName,
        CancellationToken cancellationToken = default
        )
    {
        // Validate the parameters before attempting to use them.
        Guard.Instance().ThrowIfNull(greenRole, nameof(greenRole))
            .ThrowIfNullOrEmpty(userName, nameof(userName));

        try
        {
            // Update the role.
            var result = await _roleManager.UpdateAsync(
                greenRole
                ).ConfigureAwait(false);

            // Did we fail?
            if (!result.Succeeded)
            {
                // Panic!!
                throw new ManagerException(
                    $"ASP.NET role manager errors: {string.Join(",", result.Errors)}"
                    );
            }

            // Look for the role.
            var role = await _roleManager.FindByNameAsync(
                greenRole.Name ?? ""
                ).ConfigureAwait(false);

            // Did we fail?
            if (role is null)
            {
                // Panic!!
                throw new KeyNotFoundException(
                    $"Failed to find role: {greenRole.Name}"
                    );
            }

            // Return the results.
            return role;
        }
        catch (Exception ex)
        {
            // Log what happened.
            _logger.LogError(
                ex,
                "Failed to update a role!"
                );

            // Provider better context.
            throw new ManagerException(
                message: $"The manager failed to update a role!",
                innerException: ex
                );
        }
    }

    #endregion
}
