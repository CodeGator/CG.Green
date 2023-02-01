
namespace CG.Green.Data.Repositories;

/// <summary>
/// This class is a default implementation of the <see cref="IApiScopeRepository"/>
/// interface.
/// </summary>
internal class ApiScopeRepository : IApiScopeRepository
{
    // *******************************************************************
    // Fields.
    // *******************************************************************

    #region Fields

    /// <summary>
    /// This field contains the configuration data-context for this repository.
    /// </summary>
    internal protected readonly ConfigurationDbContext _configurationDbContext = null!;

    /// <summary>
    /// This field contains the logger for this repository.
    /// </summary>
    internal protected readonly ILogger<IApiScopeRepository> _logger;

    #endregion

    // *******************************************************************
    // Constructors.
    // *******************************************************************

    #region Constructors

    /// <summary>
    /// This constructor creates a new instance of the <see cref="ApiScopeRepository"/>
    /// class.
    /// </summary>
    /// <param name="configurationDbContext">The Duende configuration 
    /// data-context to use with this repository.</param>
    /// <param name="logger">The logger to use with this repository.</param>
    public ApiScopeRepository(
        ConfigurationDbContext configurationDbContext,
        ILogger<IApiScopeRepository> logger
        )
    {
        // Validate the arguments before attempting to use them.
        Guard.Instance().ThrowIfNull(configurationDbContext, nameof(configurationDbContext))
            .ThrowIfNull(logger, nameof(logger));

        // Save the reference(s).
        _configurationDbContext = configurationDbContext;
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
                "Searching for api scopes!"
                );

            // Search for any entities in the data-store.
            var data = await _configurationDbContext.ApiScopes.AnyAsync(
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
                "Failed to search for api scopes!"
                );

            // Provider better context.
            throw new RepositoryException(
                message: $"The repository failed to search for api scopes!",
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
                "Searching for api scopes"
                );

            // Search for any entities in the data-store.
            var data = await _configurationDbContext.ApiScopes.CountAsync(
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
                "Failed to count api scopes!"
                );

            // Provider better context.
            throw new RepositoryException(
                message: $"The repository failed to count api scopes!",
                innerException: ex
                );
        }
    }

    // *******************************************************************

    /// <inheritdoc/>
    public virtual async Task<ApiScope> CreateAsync(
        ApiScope apiScope,
        CancellationToken cancellationToken = default
        )
    {
        // Validate the parameters before attempting to use them.
        Guard.Instance().ThrowIfNull(apiScope, nameof(apiScope));

        try
        {
            // Log what we are about to do.
            _logger.LogDebug(
                "Converting a {entity} model to an entity",
                nameof(ApiScope)
                );

            // Convert the model to an entity.
            var entity = apiScope.ToEntity();

            // Log what we are about to do.
            _logger.LogDebug(
                "Adding the {entity} to the {ctx} data-context.",
                nameof(ApiScope),
                nameof(ConfigurationDbContext)
                );

            // Add the entity to the data-store.
            _configurationDbContext.ApiScopes.Attach(entity);

            // Mark the entity as added so EFCORE will insert it.
            _configurationDbContext.Entry(entity).State = EntityState.Added;

            // Log what we are about to do.
            _logger.LogDebug(
                "Saving changes to the {ctx} data-context",
                nameof(ConfigurationDbContext)
                );

            // Save the changes.
            await _configurationDbContext.SaveChangesAsync(
                cancellationToken
                ).ConfigureAwait(false);

            // Log what we are about to do.
            _logger.LogDebug(
                "Converting a {entity} entity to a model",
                nameof(ApiScope)
                );

            // Convert the entity to a model.
            var result = entity.ToModel();

            // Return the results.
            return result;
        }
        catch (Exception ex)
        {
            // Log what happened.
            _logger.LogError(
                ex,
                "Failed to create an api scope!"
                );

            // Provider better context.
            throw new RepositoryException(
                message: $"The repository failed to create an api scope!",
                innerException: ex
                );
        }
    }

    // *******************************************************************

    /// <inheritdoc/>
    public virtual async Task DeleteAsync(
        ApiScope apiScope,
        CancellationToken cancellationToken = default
        )
    {
        // Validate the parameters before attempting to use them.
        Guard.Instance().ThrowIfNull(apiScope, nameof(apiScope));

        try
        {
            // Log what we are about to do.
            _logger.LogDebug(
                "looking for the tracked {entity} instance from the {ctx} data-context",
                nameof(ApiScope),
                nameof(ConfigurationDbContext)
                );

            // Find the tracked entity (if any).
            var entity = await _configurationDbContext.ApiScopes.Where(x =>
                x.Name == apiScope.Name
                ).FirstOrDefaultAsync(
                    cancellationToken
                    );

            // Did we fail?
            if (entity is null)
            {
                return; // Nothing to do!
            }

            // Log what we are about to do.
            _logger.LogDebug(
                "Deleting an {entity} instance from the {ctx} data-context",
                nameof(ApiScope),
                nameof(ConfigurationDbContext)
                );

            // Delete from the data-store.
            _configurationDbContext.ApiScopes.Remove(
                entity
                );

            // Log what we are about to do.
            _logger.LogDebug(
                "Saving changes to the {ctx} data-context",
                nameof(ConfigurationDbContext)
                );

            // Save the changes.
            await _configurationDbContext.SaveChangesAsync(
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
            throw new RepositoryException(
                message: $"The repository failed to delete an api scope!",
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
            _logger.LogDebug(
                "Searching api scopes."
                );

            // Perform the apiScope search.
            var scopes = await _configurationDbContext.ApiScopes
                .Include(x => x.UserClaims)
                .ToListAsync(
                cancellationToken
                ).ConfigureAwait(false);

            // Convert the entities to models.
            var models = scopes.Select(x => x.ToModel());

            // Return the results.
            return models;
        }
        catch (Exception ex)
        {
            // Log what happened.
            _logger.LogError(
                ex,
                "Failed to search for api scopes!"
                );

            // Provider better context.
            throw new RepositoryException(
                message: $"The repository failed to search for api scopes!",
                innerException: ex
                );
        }
    }

    // *******************************************************************

    /// <inheritdoc/>
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
            _logger.LogDebug(
                "Searching api scopes."
                );

            // Perform the apiScope search.
            var scope = await _configurationDbContext.ApiScopes.Where(
                x => x.Name == name
                ).Include(x => x.UserClaims)
                .FirstOrDefaultAsync(
                cancellationToken
                ).ConfigureAwait(false);

            // Did we fail?
            if (scope is null)
            {
                return null;
            }

            // Convert the entity to a model.
            var model = scope.ToModel();

            // Return the results.
            return model;
        }
        catch (Exception ex)
        {
            // Log what happened.
            _logger.LogError(
                ex,
                "Failed to search for an api scope by name!"
                );

            // Provider better context.
            throw new RepositoryException(
                message: $"The repository failed to search for an api scope " +
                "by name!",
                innerException: ex
                );
        }
    }

    // *******************************************************************

    /// <inheritdoc/>
    public virtual async Task<ApiScope> UpdateAsync(
        ApiScope apiScope,
        CancellationToken cancellationToken = default
        )
    {
        // Validate the parameters before attempting to use them.
        Guard.Instance().ThrowIfNull(apiScope, nameof(apiScope));

        try
        {
            // Log what we are about to do.
            _logger.LogDebug(
                "Looking for a matching {entity} entity in the {ctx} data-context.",
                nameof(ApiScope),
                nameof(ConfigurationDbContext)
                );

            // Look for the given entity.
            var entity = await _configurationDbContext.ApiScopes.Where(x =>
                x.Name == apiScope.Name
                ).FirstOrDefaultAsync(
                    cancellationToken
                    ).ConfigureAwait(false);

            // Did we fail?
            if (entity is null)
            {
                // Panic!!
                throw new KeyNotFoundException(
                    $"The api scope: {apiScope.Name} was not found!"
                    );
            }

            // Log what we are about to do.
            _logger.LogDebug(
                "Updating a {entity} entity in the {ctx} data-context.",
                nameof(ApiScope),
                nameof(ConfigurationDbContext)
                );

            // Log what we are about to do.
            _logger.LogDebug(
                "Saving changes to the {ctx} data-context",
                nameof(ConfigurationDbContext)
                );

            // Save the changes.
            await _configurationDbContext.SaveChangesAsync(
                cancellationToken
                ).ConfigureAwait(false);

            // Log what we are about to do.
            _logger.LogDebug(
                "Converting a {entity} entity to a model",
                nameof(ApiScope)
                );

            // Convert the entity to a model.
            var result = entity.ToModel();

            // Did we fail?
            if (result is null)
            {
                // Panic!!
                throw new AutoMapperMappingException(
                    $"Failed to map the {nameof(ApiScope)} entity " +
                    "to a model."
                    );
            }

            // Return the results.
            return result;
        }
        catch (Exception ex)
        {
            // Log what happened.
            _logger.LogError(
                ex,
                "Failed to update an api scope!"
                );

            // Provider better context.
            throw new RepositoryException(
                message: $"The repository failed to update an api scope!",
                innerException: ex
                );
        }
    }

    #endregion
}
