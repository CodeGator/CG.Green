
namespace CG.Green.Data.Repositories;

/// <summary>
/// This class is a default implementation of the <see cref="IGreenUserRoleRepository"/>
/// interface.
/// </summary>
internal class GreenUserRoleRepository : IGreenUserRoleRepository
{
    // *******************************************************************
    // Fields.
    // *******************************************************************

    #region Fields

    /// <summary>
    /// This field contains the green data-context for this repository.
    /// </summary>
    internal protected readonly AspNetDbContext _greenDbContext = null!;

    /// <summary>
    /// This field contains the logger for this repository.
    /// </summary>
    internal protected readonly ILogger<IGreenUserRoleRepository> _logger;

    #endregion

    // *******************************************************************
    // Constructors.
    // *******************************************************************

    #region Constructors

    /// <summary>
    /// This constructor creates a new instance of the <see cref="GreenUserRoleRepository"/>
    /// class.
    /// </summary>
    /// <param name="greenDbContext">The Duende configuration 
    /// data-context to use with this repository.</param>
    /// <param name="logger">The logger to use with this repository.</param>
    public GreenUserRoleRepository(
        AspNetDbContext greenDbContext,
        ILogger<IGreenUserRoleRepository> logger
        )
    {
        // Validate the arguments before attempting to use them.
        Guard.Instance().ThrowIfNull(greenDbContext, nameof(greenDbContext))
            .ThrowIfNull(logger, nameof(logger));

        // Save the reference(s).
        _greenDbContext = greenDbContext;
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
            // Log what we are about to do.
            _logger.LogDebug(
                "Searching for clients"
                );

            // Search for any entities in the data-store.
            var data = await _greenDbContext.UserRoles.AnyAsync(
                cancellationToken
                ).ConfigureAwait(false);

            // Return the results.
            return data;
        }
        catch (Exception ex)
        {
            // Log what happened.
            _logger.LogError(
                ex,
                "Failed to search for user roles!"
                );

            // Provider better context.
            throw new RepositoryException(
                message: $"The repository failed to search for user roles!",
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
            // Log what we are about to do.
            _logger.LogDebug(
                "Searching for user roles"
                );

            // Search for any entities in the data-store.
            var data = await _greenDbContext.UserRoles.CountAsync(
                cancellationToken
                ).ConfigureAwait(false);

            // Return the results.
            return data;
        }
        catch (Exception ex)
        {
            // Log what happened.
            _logger.LogError(
                ex,
                "Failed to count user roles!"
                );

            // Provider better context.
            throw new RepositoryException(
                message: $"The repository failed to count user roles!",
                innerException: ex
                );
        }
    }

    #endregion
}
