
namespace CG.Green.Managers;

/// <summary>
/// This class is a default implementation of the <see cref="IGreenUserManager"/>
/// interface.
/// </summary>
internal class GreenUserManager : IGreenUserManager
{
    // *******************************************************************
    // Fields.
    // *******************************************************************

    #region Fields

    /// <summary>
    /// This field contains the ASP.NET sign in manager for this manager.
    /// </summary>
    internal protected readonly SignInManager<GreenUser> _signInManager = null!;

    /// <summary>
    /// This field contains the logger for this manager.
    /// </summary>
    internal protected readonly ILogger<IGreenUserManager> _logger = null!;

    #endregion

    // *******************************************************************
    // Constructors.
    // *******************************************************************

    #region Constructors

    /// <summary>
    /// This constructor creates a new instance of the <see cref="GreenUserManager"/>
    /// class.
    /// </summary>
    /// <param name="signInManager">The ASP.NET sign-in manager to use with 
    /// this manager.</param>
    /// <param name="logger">The logger to use with this manager.</param>
    public GreenUserManager(
        SignInManager<GreenUser> signInManager,
        ILogger<IGreenUserManager> logger
        )
    {
        // Validate the arguments before attempting to use them.
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

    /// <inheritdoc/>
    public virtual Task<bool> AnyAsync(
        CancellationToken cancellationToken = default
        )
    {
        try
        {
            // Check the repository for the data.
            var result = _signInManager.UserManager.Users.Any();

            // Return the results,
            return Task.FromResult(result);
        }
        catch (Exception ex)
        {
            // Log what happened.
            _logger.LogError(
                ex,
                "Failed to search for users!"
                );

            // Provider better context.
            throw new ManagerException(
                message: $"The manager failed to search for users!",
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
            var result = _signInManager.UserManager.Users.Count();

            // Return the results.
            return Task.FromResult(result);
        }
        catch (Exception ex)
        {
            // Log what happened.
            _logger.LogError(
                ex,
                "Failed to count users!"
                );

            // Provider better context.
            throw new ManagerException(
                message: $"The manager failed to count users!",
                innerException: ex
                );
        }
    }

    // *******************************************************************

    /// <inheritdoc/>
    public virtual async Task<GreenUser> CreateAsync(
        GreenUser greenUser,
        string password,
        string userName,
        CancellationToken cancellationToken = default
        )
    {
        // Validate the parameters before attempting to use them.
        Guard.Instance().ThrowIfNull(greenUser, nameof(greenUser))
            .ThrowIfNullOrEmpty(password, nameof(password))
            .ThrowIfNullOrEmpty(userName, nameof(userName));

        try
        {
            // Create the new user.
            var result = await _signInManager.UserManager.CreateAsync(
                greenUser,
                password
                ).ConfigureAwait(false);

            // Did we fail?
            if (!result.Succeeded)
            {
                // Panic!!
                throw new ManagerException(
                    $"ASP.NET user manager errors: {string.Join(",", result.Errors)}"
                    );
            }

            // Look for the new user.
            var newUser = await _signInManager.UserManager.FindByEmailAsync(
                greenUser.Email ?? ""
                ).ConfigureAwait(false);

            // Did we fail?
            if (newUser is null)
            {
                // Panic!!
                throw new KeyNotFoundException(
                    $"Failed to find user: {greenUser.Email}"
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
                "Failed to create a new user!"
                );

            // Provider better context.
            throw new ManagerException(
                message: $"The manager failed to create a new user!",
                innerException: ex
                );
        }
    }

    // *******************************************************************

    /// <inheritdoc/>
    public virtual async Task DeleteAsync(
        GreenUser greenUser,
        string userName,
        CancellationToken cancellationToken = default
        )
    {
        // Validate the parameters before attempting to use them.
        Guard.Instance().ThrowIfNull(greenUser, nameof(greenUser))
            .ThrowIfNullOrEmpty(userName, nameof(userName));

        try
        {
            // Look for the user.
            var user = await _signInManager.UserManager.FindByEmailAsync(
                greenUser.Email ?? ""
                ).ConfigureAwait(false);

            // Did we fail?
            if (user is null)
            {
                // Panic!!
                throw new KeyNotFoundException(
                    $"Failed to find user: {greenUser.Email}"
                    );
            }

            // Delete the user.
            var result = await _signInManager.UserManager.DeleteAsync(
                user
                ).ConfigureAwait(false);

            // Did we fail?
            if (!result.Succeeded)
            {
                // Panic!!
                throw new ManagerException(
                    $"ASP.NET user manager errors: {string.Join(",", result.Errors)}"
                    );
            }
        }
        catch (Exception ex)
        {
            // Log what happened.
            _logger.LogError(
                ex,
                "Failed to delete a user"
                );

            // Provider better context.
            throw new ManagerException(
                message: $"The manager failed to delete a user!",
                innerException: ex
                );
        }
    }

    // *******************************************************************

    /// <inheritdoc/>
    public virtual Task<IEnumerable<GreenUser>> FindAllAsync(
        CancellationToken cancellationToken = default
        )
    {
        try
        {
            // Look for the users.
            var users = _signInManager.UserManager.Users.AsEnumerable();

            // Return the results.
            return Task.FromResult(users);
        }
        catch (Exception ex)
        {
            // Log what happened.
            _logger.LogError(
                ex,
                "Failed to search for users!"
                );

            // Provider better context.
            throw new ManagerException(
                message: $"The manager failed to search for users!",
                innerException: ex
                );
        }
    }

    // *******************************************************************

    /// <inheritdoc/>
    public virtual async Task<GreenUser> UpdateAsync(
        GreenUser greenUser,
        string userName,
        CancellationToken cancellationToken = default
        )
    {
        // Validate the parameters before attempting to use them.
        Guard.Instance().ThrowIfNull(greenUser, nameof(greenUser))
            .ThrowIfNullOrEmpty(userName, nameof(userName));

        try
        {
            // Update the user.
            var result = await _signInManager.UserManager.UpdateAsync(
                greenUser
                ).ConfigureAwait(false);

            // Did we fail?
            if (!result.Succeeded)
            {
                // Panic!!
                throw new ManagerException(
                    $"ASP.NET user manager errors: {string.Join(",", result.Errors)}"
                    );
            }

            // Look for the user.
            var user = await _signInManager.UserManager.FindByEmailAsync(
                greenUser.Email ?? ""
                ).ConfigureAwait(false);

            // Did we fail?
            if (user is null)
            {
                // Panic!!
                throw new KeyNotFoundException(
                    $"Failed to find user: {greenUser.Email}"
                    );
            }

            // Return the results.
            return user;
        }
        catch (Exception ex)
        {
            // Log what happened.
            _logger.LogError(
                ex,
                "Failed to update a user!"
                );

            // Provider better context.
            throw new ManagerException(
                message: $"The manager failed to update a user!",
                innerException: ex
                );
        }
    }

    #endregion
}
