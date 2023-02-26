
namespace CG.Green.Data.Repositories;

/// <summary>
/// This class is a default implementation of the <see cref="IClientRepository"/>
/// interface.
/// </summary>
internal class ClientRepository : IClientRepository
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
    internal protected readonly ILogger<IClientRepository> _logger;

    #endregion

    // *******************************************************************
    // Constructors.
    // *******************************************************************

    #region Constructors

    /// <summary>
    /// This constructor creates a new instance of the <see cref="ClientRepository"/>
    /// class.
    /// </summary>
    /// <param name="configurationDbContext">The Duende configuration 
    /// data-context to use with this repository.</param>
    /// <param name="logger">The logger to use with this repository.</param>
    public ClientRepository(
        ConfigurationDbContext configurationDbContext,
        ILogger<IClientRepository> logger
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
                "Searching for clients"
                );

            // Search for any entities in the data-store.
            var data = await _configurationDbContext.Clients.AnyAsync(
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
                "Failed to search for clients!"
                );

            // Provider better context.
            throw new RepositoryException(
                message: $"The repository failed to search for clients!",
                innerException: ex
                );
        }
    }

    // *******************************************************************

    /// <inheritdoc/>
    public virtual async Task<bool> AnyByIdAsync(
        string clientId,
        CancellationToken cancellationToken = default
        )
    {
        // Validate the parameters before attempting to use them.
        Guard.Instance().ThrowIfNull(clientId, nameof(clientId));

        try
        {
            // Log what we are about to do.
            _logger.LogDebug(
                "Searching for clients by id"
                );

            // Search for any entities in the data-store.
            var data = await _configurationDbContext.Clients.AnyAsync(
                x => x.ClientId == clientId,
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
                "Failed to search for clients by id!"
                );

            // Provider better context.
            throw new RepositoryException(
                message: $"The repository failed to search for clients by id!",
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
                "Searching for clients"
                );

            // Search for any entities in the data-store.
            var data = await _configurationDbContext.Clients.CountAsync(
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
                "Failed to count clients!"
                );

            // Provider better context.
            throw new RepositoryException(
                message: $"The repository failed to count clients!",
                innerException: ex
                );
        }
    }

    // *******************************************************************

    /// <inheritdoc/>
    public virtual async Task<Client> CreateAsync(
        Client client,
        CancellationToken cancellationToken = default
        )
    {
        // Validate the parameters before attempting to use them.
        Guard.Instance().ThrowIfNull(client, nameof(client));

        try
        {
            // Log what we are about to do.
            _logger.LogDebug(
                "Converting a {entity} model to an entity",
                nameof(Client)
                );

            // Convert the model to an entity.
            var entity = client.ToEntity();

            // Log what we are about to do.
            _logger.LogDebug(
                "Adding the {entity} to the {ctx} data-context.",
                nameof(Client),
                nameof(ConfigurationDbContext)
                );

            // Add the entity to the data-store.
            _configurationDbContext.Clients.Attach(entity);

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

            // Look for the complete entity (with sub-objects).
            var result = await FindByIdAsync(
                entity.ClientId
                );

            // Did we fail?
            if(result  is null)
            {
                // Panic!!
                throw new KeyNotFoundException(
                    $"The client: '{entity.ClientId}' was not found!"
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
                "Failed to create a client!"
                );

            // Provider better context.
            throw new RepositoryException(
                message: $"The repository failed to create a client!",
                innerException: ex
                );
        }
    }

    // *******************************************************************

    /// <inheritdoc/>
    public virtual async Task DeleteAsync(
        Client client,
        CancellationToken cancellationToken = default
        )
    {
        // Validate the parameters before attempting to use them.
        Guard.Instance().ThrowIfNull(client, nameof(client));

        try
        {
            // Log what we are about to do.
            _logger.LogDebug(
                "looking for the tracked {entity} instance from the {ctx} data-context",
                nameof(Client),
                nameof(ConfigurationDbContext)
                );

            // Find the tracked entity (if any).
            var entity = await _configurationDbContext.Clients.Where(x =>
                x.ClientId == client.ClientId
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
                nameof(Client),
                nameof(ConfigurationDbContext)
                );

            // Delete from the data-store.
            _configurationDbContext.Clients.Remove(
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
                "Failed to delete a client!"
                );

            // Provider better context.
            throw new RepositoryException(
                message: $"The repository failed to delete a client!",
                innerException: ex
                );
        }
    }

    // *******************************************************************

    /// <inheritdoc/>
    public virtual async Task<IEnumerable<Client>> FindAllAsync(
        CancellationToken cancellationToken = default
        )
    {
        try
        {
            // Log what we are about to do.
            _logger.LogDebug(
                "Searching clients."
                );

            // Perform the client search.
            var clients = await _configurationDbContext.Clients
                .Include(x => x.AllowedGrantTypes)
                .Include(x => x.AllowedCorsOrigins)
                .Include(x => x.AllowedScopes)
                .Include(x => x.ClientSecrets)
                .Include(x => x.RedirectUris)
                .Include(x => x.PostLogoutRedirectUris)
                .Include(x => x.Claims)
                .ToListAsync(
                cancellationToken
                ).ConfigureAwait(false);

            // Convert the entities to models.
            var models = clients.Select(x => x.ToModel());

            // Return the results.
            return models;
        }
        catch (Exception ex)
        {
            // Log what happened.
            _logger.LogError(
                ex,
                "Failed to search for clients!"
                );

            // Provider better context.
            throw new RepositoryException(
                message: $"The repository failed to search for clients!",
                innerException: ex
                );
        }
    }

    // *******************************************************************

    /// <inheritdoc/>
    public virtual async Task<Client?> FindByIdAsync(
        string clientId,
        CancellationToken cancellationToken = default
        )
    {
        // Validate the parameters before attempting to use them.
        Guard.Instance().ThrowIfNullOrEmpty(clientId, nameof(clientId));

        try
        {
            // Log what we are about to do.
            _logger.LogDebug(
                "Searching clients."
                );

            // Perform the client search.
            var client = await _configurationDbContext.Clients.Where(
                 x => x.ClientId == clientId
                 ).Include(x => x.AllowedGrantTypes)
                  .Include(x => x.AllowedCorsOrigins)
                  .Include(x => x.AllowedScopes)
                  .Include(x => x.ClientSecrets)
                  .Include(x => x.RedirectUris)
                  .Include(x => x.PostLogoutRedirectUris)
                  .Include(x => x.Claims)
                  .FirstOrDefaultAsync(
                        cancellationToken
                        ).ConfigureAwait(false);

            // Did we fail?
            if (client is null)
            {
                return null;
            }

            // Convert the entity to a model.
            var result = client.ToModel();

            // Return the results.
            return result;
        }
        catch (Exception ex)
        {
            // Log what happened.
            _logger.LogError(
                ex,
                "Failed to search for a client by id!"
                );

            // Provider better context.
            throw new RepositoryException(
                message: $"The repository failed to search for a client " +
                "by id!",
                innerException: ex
                );
        }
    }

    // *******************************************************************

    /// <inheritdoc/>
    public virtual async Task<Client> UpdateAsync(
        Client client,
        CancellationToken cancellationToken = default
        )
    {
        // Validate the parameters before attempting to use them.
        Guard.Instance().ThrowIfNull(client, nameof(client));

        try
        {
            // Log what we are about to do.
            _logger.LogDebug(
                "Converting a {model} model to an entity.",
                nameof(Client)
                );

            // Change the incoming model to an entity.
            var clientEntity = client.ToEntity();

            // Log what we are about to do.
            _logger.LogDebug(
                "Looking for a matching {entity} entity in the {ctx} data-context.",
                nameof(Client),
                nameof(ConfigurationDbContext)
                );

            // Look for the given entity.
            var entity = await _configurationDbContext.Clients.Where(x =>
                x.ClientId == client.ClientId
                ).Include(x => x.AllowedGrantTypes)
                  .Include(x => x.AllowedCorsOrigins)
                  .Include(x => x.AllowedScopes)
                  .Include(x => x.ClientSecrets)
                  .Include(x => x.RedirectUris)
                  .Include(x => x.PostLogoutRedirectUris)
                  .Include(x => x.Claims)
                  .FirstOrDefaultAsync(
                    cancellationToken
                    ).ConfigureAwait(false);

            // Did we fail?
            if (entity is null)
            {
                // Panic!!
                throw new KeyNotFoundException(
                    $"The client: {client.ClientId} was not found!"
                    );
            }

            // Log what we are about to do.
            _logger.LogDebug(
                "Updating editable properties for {entity} entity in the {ctx} data-context.",
                nameof(Client),
                nameof(ConfigurationDbContext)
                );

            // Update the editable properties.
            entity.ClientId = clientEntity.ClientId;
            entity.ClientName = clientEntity.ClientName;
            entity.AllowedGrantTypes = clientEntity.AllowedGrantTypes;    
            entity.Enabled = clientEntity.Enabled;
            entity.AllowOfflineAccess = clientEntity.AllowOfflineAccess;    
            entity.RequireRequestObject = clientEntity.RequireRequestObject;    
            entity.AccessTokenLifetime =  clientEntity.AccessTokenLifetime;
            entity.AllowedScopes = clientEntity.AllowedScopes;
            entity.RequireClientSecret = clientEntity.RequireClientSecret;  
            entity.ClientSecrets = clientEntity.ClientSecrets;
            entity.RedirectUris = clientEntity.RedirectUris;
            entity.FrontChannelLogoutUri = clientEntity.FrontChannelLogoutUri;
            entity.PostLogoutRedirectUris = clientEntity.PostLogoutRedirectUris;
            entity.AllowedCorsOrigins = clientEntity.AllowedCorsOrigins;
            entity.Claims = clientEntity.Claims;
			entity.AlwaysSendClientClaims = clientEntity.AlwaysSendClientClaims;
			entity.AlwaysIncludeUserClaimsInIdToken = clientEntity.AlwaysIncludeUserClaimsInIdToken;
			entity.ClientClaimsPrefix = clientEntity.ClientClaimsPrefix;

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
                nameof(Client)
                );

            // Convert the entity to a model.
            var result = entity.ToModel();

            // Did we fail?
            if (result is null)
            {
                // Panic!!
                throw new AutoMapperMappingException(
                    $"Failed to map the {nameof(Client)} entity " +
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
                "Failed to update a client!"
                );

            // Provider better context.
            throw new RepositoryException(
                message: $"The repository failed to update a client!",
                innerException: ex
                );
        }
    }

    #endregion
}
