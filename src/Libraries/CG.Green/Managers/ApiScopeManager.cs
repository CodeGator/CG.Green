
using Duende.IdentityServer.Models;

namespace CG.Green.Managers;

/// <summary>
/// This class is a default implementation of the <see cref="IApiScopeManager"/>
/// interface.
/// </summary>
internal class ApiScopeManager : IApiScopeManager
{
    // *******************************************************************
    // Fields.
    // *******************************************************************

    #region Fields

    /// <summary>
    /// This field contains the api scope repository for this manager.
    /// </summary>
    internal protected readonly IApiScopeRepository _apiScopeRepository = null!;

    /// <summary>
    /// This field contains the logger for this manager.
    /// </summary>
    internal protected readonly ILogger<IApiScopeManager> _logger;

    #endregion

    // *******************************************************************
    // Constructors.
    // *******************************************************************

    #region Constructors

    /// <summary>
    /// This constructor creates a new instance of the <see cref="ApiScopeManager"/>
    /// class.
    /// </summary>
    /// <param name="apiScopeRepository">The api scope repository to use 
    /// with this manager.</param>
    /// <param name="logger">The logger to use with this manager.</param>
    public ApiScopeManager(
        IApiScopeRepository apiScopeRepository,
        ILogger<IApiScopeManager> logger
        )
    {
        // Validate the arguments before attempting to use them.
        Guard.Instance().ThrowIfNull(apiScopeRepository, nameof(apiScopeRepository))
            .ThrowIfNull(logger, nameof(logger));

        // Save the reference(s).
        _apiScopeRepository = apiScopeRepository;
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
                nameof(IApiScopeRepository.AnyAsync)
                );

            // Check the repository for the data.
            var result = await _apiScopeRepository.AnyAsync(
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
                "Failed to search for api scopes!"
                );

            // Provider better context.
            throw new ManagerException(
                message: $"The manager failed to search for api scopes!",
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
                nameof(IApiScopeRepository.CountAsync)
                );

            // Perform the search.
            var result = await _apiScopeRepository.CountAsync(
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
                "Failed to count api scopes!"
                );

            // Provider better context.
            throw new ManagerException(
                message: $"The manager failed to count api scopes!",
                innerException: ex
                );
        }
    }

    // *******************************************************************

    /// <inheritdoc/>
    public virtual async Task<ApiScope> CreateAsync(
        ApiScope client,
        string userName,
        CancellationToken cancellationToken = default
        )
    {
        // Validate the parameters before attempting to use them.
        Guard.Instance().ThrowIfNull(client, nameof(client))
            .ThrowIfNullOrEmpty(userName, nameof(userName));

        try
        {
            // Log what we are about to do.
            _logger.LogDebug(
                "Updating the {name} model stats",
                nameof(ApiScope)
                );

            // Log what we are about to do.
            _logger.LogTrace(
                "Deferring to {name}",
                nameof(IApiScopeRepository.CreateAsync)
                );

            // Perform the operation.
            var result = await _apiScopeRepository.CreateAsync(
                client,
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
                "Failed to create a new api scope!"
                );

            // Provider better context.
            throw new ManagerException(
                message: $"The manager failed to create a new api scope!",
                innerException: ex
                );
        }
    }

    // *******************************************************************

    /// <inheritdoc/>
    public virtual async Task DeleteAsync(
        ApiScope client,
        string userName,
        CancellationToken cancellationToken = default
        )
    {
        // Validate the parameters before attempting to use them.
        Guard.Instance().ThrowIfNull(client, nameof(client))
            .ThrowIfNullOrEmpty(userName, nameof(userName));

        try
        {
            // Log what we are about to do.
            _logger.LogDebug(
                "Updating the {name} model stats",
                nameof(ApiScope)
                );

            // Log what we are about to do.
            _logger.LogTrace(
                "Deferring to {name}",
                nameof(IApiScopeRepository.DeleteAsync)
                );

            // Perform the operation.
            await _apiScopeRepository.DeleteAsync(
                client,
                cancellationToken
                ).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            // Log what happened.
            _logger.LogError(
                ex,
                "Failed to delete an api scope!"
                );

            // Provider better context.
            throw new ManagerException(
                message: $"The manager failed to delete an api scope!",
                innerException: ex
                );
        }
    }

    // *******************************************************************

    /// <inheritdoc/>
    public virtual async Task<IEnumerable<ApiScope>> FindAllAsync(
        CancellationToken cancellationToken = default
        )
    {
        try
        {
            // Log what we are about to do.
            _logger.LogTrace(
                "Deferring to {name}",
                nameof(IApiScopeRepository.FindAllAsync)
                );

            // Perform the operation.
            var scopes = await _apiScopeRepository.FindAllAsync(
                cancellationToken
                ).ConfigureAwait(false);

            // Return the results.
            return scopes;
        }
        catch (Exception ex)
        {
            // Log what happened.
            _logger.LogError(
                ex,
                "Failed to search for api scopes!"
                );

            // Provider better context.
            throw new ManagerException(
                message: $"The manager failed to search for api scopes!",
                innerException: ex
                );
        }
    }

    // *******************************************************************

    public virtual async Task<ApiScope?> FindByNameAsync(
        string name,
        CancellationToken cancellationToken = default
        )
    {
        // Validate the parameters before attempting to use them.
        Guard.Instance().ThrowIfNullOrEmpty(name, nameof(name));

        try
        {
            // Log what we are about to do.
            _logger.LogTrace(
                "Deferring to {name}",
                nameof(IApiScopeRepository.FindByNameAsync)
                );

            // Perform the operation.
            var scopes = await _apiScopeRepository.FindByNameAsync(
                name,
                cancellationToken
                ).ConfigureAwait(false);

            // Return the results.
            return scopes;
        }
        catch (Exception ex)
        {
            // Log what happened.
            _logger.LogError(
                ex,
                "Failed to search for an api scope by name!"
                );

            // Provider better context.
            throw new ManagerException(
                message: $"The manager failed to search for an api scope " +
                "by name!",
                innerException: ex
                );
        }
    }

    // *******************************************************************

    /// <inheritdoc/>
    public virtual async Task<ApiScope> UpdateAsync(
        ApiScope client,
        string userName,
        CancellationToken cancellationToken = default
        )
    {
        // Validate the parameters before attempting to use them.
        Guard.Instance().ThrowIfNull(client, nameof(client))
            .ThrowIfNullOrEmpty(userName, nameof(userName));

        try
        {
            // Log what we are about to do.
            _logger.LogDebug(
                "Updating the {name} model stats",
                nameof(ApiScope)
                );

            // Log what we are about to do.
            _logger.LogTrace(
                "Deferring to {name}",
                nameof(IApiScopeRepository.UpdateAsync)
                );

            // Perform the operation.
            var updatedApiScope = await _apiScopeRepository.UpdateAsync(
                client,
                cancellationToken
                ).ConfigureAwait(false);
                        
            // Return the results.
            return updatedApiScope;
        }
        catch (Exception ex)
        {
            // Log what happened.
            _logger.LogError(
                ex,
                "Failed to update an api scope!"
                );

            // Provider better context.
            throw new ManagerException(
                message: $"The manager failed to update an api scope!",
                innerException: ex
                );
        }
    }

    #endregion
}
