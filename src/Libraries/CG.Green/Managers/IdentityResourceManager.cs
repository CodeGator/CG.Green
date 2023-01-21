
namespace CG.Green.Managers;

/// <summary>
/// This class is a default implementation of the <see cref="IIdentityResourceManager"/>
/// interface.
/// </summary>
internal class IdentityResourceManager : IIdentityResourceManager
{
    // *******************************************************************
    // Fields.
    // *******************************************************************

    #region Fields

    /// <summary>
    /// This field contains the identity resource repository for this manager.
    /// </summary>
    internal protected readonly IIdentityResourceRepository _identityResourceRepository = null!;

    /// <summary>
    /// This field contains the logger for this manager.
    /// </summary>
    internal protected readonly ILogger<IIdentityResourceManager> _logger;

    #endregion

    // *******************************************************************
    // Constructors.
    // *******************************************************************

    #region Constructors

    /// <summary>
    /// This constructor creates a new instance of the <see cref="IdentityResourceManager"/>
    /// class.
    /// </summary>
    /// <param name="identityResourceRepository">The identity resource
    /// repository to use with this manager.</param>
    /// <param name="logger">The logger to use with this manager.</param>
    public IdentityResourceManager(
        IIdentityResourceRepository identityResourceRepository,
        ILogger<IIdentityResourceManager> logger
        )
    {
        // Validate the arguments before attempting to use them.
        Guard.Instance().ThrowIfNull(identityResourceRepository, nameof(identityResourceRepository))
            .ThrowIfNull(logger, nameof(logger));

        // Save the reference(s).
        _identityResourceRepository = identityResourceRepository;
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
            _logger.LogTrace(
                "Deferring to {name}",
                nameof(IIdentityResourceRepository.AnyAsync)
                );

            // Check the repository for the data.
            var result = await _identityResourceRepository.AnyAsync(
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
                "Failed to search for identity resources!"
                );

            // Provider better context.
            throw new ManagerException(
                message: $"The manager failed to search for identity resources!",
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
            _logger.LogTrace(
                "Deferring to {name}",
                nameof(IIdentityResourceRepository.CountAsync)
                );

            // Perform the search.
            var result = await _identityResourceRepository.CountAsync(
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
                "Failed to count identity resources!"
                );

            // Provider better context.
            throw new ManagerException(
                message: $"The manager failed to count identity resources!",
                innerException: ex
                );
        }
    }

    // *******************************************************************

    /// <inheritdoc/>
    public virtual async Task<IdentityResource> CreateAsync(
        IdentityResource identityResource,
        string userName,
        CancellationToken cancellationToken = default
        )
    {
        // Validate the parameters before attempting to use them.
        Guard.Instance().ThrowIfNull(identityResource, nameof(identityResource))
            .ThrowIfNullOrEmpty(userName, nameof(userName));

        try
        {
            // Log what we are about to do.
            _logger.LogDebug(
                "Updating the {name} model stats",
                nameof(IdentityResource)
                );

            // Log what we are about to do.
            _logger.LogTrace(
                "Deferring to {name}",
                nameof(IIdentityResourceRepository.CreateAsync)
                );

            // Perform the operation.
            var result = await _identityResourceRepository.CreateAsync(
                identityResource,
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
                "Failed to create a new identity resource!"
                );

            // Provider better context.
            throw new ManagerException(
                message: $"The manager failed to create an identity resource!",
                innerException: ex
                );
        }
    }

    // *******************************************************************

    /// <inheritdoc/>
    public virtual async Task DeleteAsync(
        IdentityResource identityResource,
        string userName,
        CancellationToken cancellationToken = default
        )
    {
        // Validate the parameters before attempting to use them.
        Guard.Instance().ThrowIfNull(identityResource, nameof(identityResource))
            .ThrowIfNullOrEmpty(userName, nameof(userName));

        try
        {
            // Log what we are about to do.
            _logger.LogDebug(
                "Updating the {name} model stats",
                nameof(IdentityResource)
                );

            // Log what we are about to do.
            _logger.LogTrace(
                "Deferring to {name}",
                nameof(IIdentityResourceRepository.DeleteAsync)
                );

            // Perform the operation.
            await _identityResourceRepository.DeleteAsync(
                identityResource,
                cancellationToken
                ).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            // Log what happened.
            _logger.LogError(
                ex,
                "Failed to delete an identity resource!"
                );

            // Provider better context.
            throw new ManagerException(
                message: $"The manager failed to delete an identity resource!",
                innerException: ex
                );
        }
    }

    // *******************************************************************

    /// <inheritdoc/>
    public virtual async Task<IEnumerable<IdentityResource>> FindAllAsync(
        CancellationToken cancellationToken = default
        )
    {
        try
        {
            // Log what we are about to do.
            _logger.LogTrace(
                "Deferring to {name}",
                nameof(IIdentityResourceRepository.FindAllAsync)
                );

            // Perform the operation.
            var identityResources = await _identityResourceRepository.FindAllAsync(
                cancellationToken
                ).ConfigureAwait(false);

            // Return the results.
            return identityResources;
        }
        catch (Exception ex)
        {
            // Log what happened.
            _logger.LogError(
                ex,
                "Failed to search for identity resources!"
                );

            // Provider better context.
            throw new ManagerException(
                message: $"The manager failed to search for identity resources!",
                innerException: ex
                );
        }
    }

    // *******************************************************************

    /// <inheritdoc/>
    public virtual async Task<IdentityResource> UpdateAsync(
        IdentityResource identityResource,
        string userName,
        CancellationToken cancellationToken = default
        )
    {
        // Validate the parameters before attempting to use them.
        Guard.Instance().ThrowIfNull(identityResource, nameof(identityResource))
            .ThrowIfNullOrEmpty(userName, nameof(userName));

        try
        {
            // Log what we are about to do.
            _logger.LogDebug(
                "Updating the {name} model stats",
                nameof(IdentityResource)
                );

            // Log what we are about to do.
            _logger.LogTrace(
                "Deferring to {name}",
                nameof(IIdentityResourceRepository.UpdateAsync)
                );

            // Perform the operation.
            var updatedIdentityResource = await _identityResourceRepository.UpdateAsync(
                identityResource,
                cancellationToken
                ).ConfigureAwait(false);

            // Return the results.
            return updatedIdentityResource;
        }
        catch (Exception ex)
        {
            // Log what happened.
            _logger.LogError(
                ex,
                "Failed to update an identity resource!"
                );

            // Provider better context.
            throw new ManagerException(
                message: $"The manager failed to update an identity resource!",
                innerException: ex
                );
        }
    }

    #endregion
}
