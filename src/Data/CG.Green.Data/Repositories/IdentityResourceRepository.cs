﻿
namespace CG.Green.Data.Repositories;

/// <summary>
/// This class is a default implementation of the <see cref="IIdentityResourceRepository"/>
/// interface.
/// </summary>
internal class IdentityResourceRepository : IIdentityResourceRepository
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
    internal protected readonly ILogger<IIdentityResourceRepository> _logger;

    #endregion

    // *******************************************************************
    // Constructors.
    // *******************************************************************

    #region Constructors

    /// <summary>
    /// This constructor creates a new instance of the <see cref="IdentityResourceRepository"/>
    /// class.
    /// </summary>
    /// <param name="configurationDbContext">The Duende configuration 
    /// data-context to use with this repository.</param>
    /// <param name="logger">The logger to use with this repository.</param>
    public IdentityResourceRepository(
        ConfigurationDbContext configurationDbContext,
        ILogger<IIdentityResourceRepository> logger
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
                "Searching for identity resources!"
                );

            // Search for any entities in the data-store.
            var data = await _configurationDbContext.IdentityResources.AnyAsync(
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
                "Failed to search for identity resources!"
                );

            // Provider better context.
            throw new RepositoryException(
                message: $"The repository failed to search for identity resources!",
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
                "Searching for identity resources"
                );

            // Search for any entities in the data-store.
            var data = await _configurationDbContext.IdentityResources.CountAsync(
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
                "Failed to count identity resources!"
                );

            // Provider better context.
            throw new RepositoryException(
                message: $"The repository failed to count identity resources!",
                innerException: ex
                );
        }
    }

    // *******************************************************************

    /// <inheritdoc/>
    public virtual async Task<IdentityResource> CreateAsync(
        IdentityResource identityResource,
        CancellationToken cancellationToken = default
        )
    {
        // Validate the parameters before attempting to use them.
        Guard.Instance().ThrowIfNull(identityResource, nameof(identityResource));

        try
        {
            // Log what we are about to do.
            _logger.LogDebug(
                "Converting a {entity} model to an entity",
                nameof(IdentityResource)
                );

            // Convert the model to an entity.
            var entity = identityResource.ToEntity();

            // Log what we are about to do.
            _logger.LogDebug(
                "Adding the {entity} to the {ctx} data-context.",
                nameof(IdentityResource),
                nameof(ConfigurationDbContext)
                );

            // Add the entity to the data-store.
            _configurationDbContext.IdentityResources.Attach(entity);

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
                nameof(IdentityResource)
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
                "Failed to create an identity resource!"
                );

            // Provider better context.
            throw new RepositoryException(
                message: $"The repository failed to create an identity resource!",
                innerException: ex
                );
        }
    }

    // *******************************************************************

    /// <inheritdoc/>
    public virtual async Task DeleteAsync(
        IdentityResource identityResource,
        CancellationToken cancellationToken = default
        )
    {
        // Validate the parameters before attempting to use them.
        Guard.Instance().ThrowIfNull(identityResource, nameof(identityResource));

        try
        {
            // Log what we are about to do.
            _logger.LogDebug(
                "looking for the tracked {entity} instance from the {ctx} data-context",
                nameof(IdentityResource),
                nameof(ConfigurationDbContext)
                );

            // Find the tracked entity (if any).
            var entity = await _configurationDbContext.IdentityResources.Where(x =>
                x.Name == identityResource.Name
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
                nameof(IdentityResource),
                nameof(ConfigurationDbContext)
                );

            // Delete from the data-store.
            _configurationDbContext.IdentityResources.Remove(
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
                "Failed to delete an identity resource!"
                );

            // Provider better context.
            throw new RepositoryException(
                message: $"The repository failed to delete an identity resource!",
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
            _logger.LogDebug(
                "Searching identity resources."
                );

            // Perform the identityResource search.
            var clients = await _configurationDbContext.IdentityResources
                .Include(x => x.UserClaims)
                .Include(x => x.Properties)
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
                "Failed to search for identity resources!"
                );

            // Provider better context.
            throw new RepositoryException(
                message: $"The repository failed to search for identity resources!",
                innerException: ex
                );
        }
    }

    // *******************************************************************

    /// <inheritdoc/>
    public virtual async Task<IdentityResource?> FindByNameAsync(
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

            // Perform the resource search.
            var resource = await _configurationDbContext.IdentityResources.Where(
                x => x.Name == name
                ).Include(x => x.UserClaims)
                .Include(x => x.Properties)
                .FirstOrDefaultAsync(
                cancellationToken
                ).ConfigureAwait(false);

            // Did we fail?
            if (resource is null)
            {
                return null;
            }

            // Convert the entity to a model.
            var model = resource.ToModel();

            // Return the results.
            return model;
        }
        catch (Exception ex)
        {
            // Log what happened.
            _logger.LogError(
                ex,
                "Failed to search for a resource by name!"
                );

            // Provider better context.
            throw new RepositoryException(
                message: $"The repository failed to search for a resource " +
                "by name!",
                innerException: ex
                );
        }
    }

    // *******************************************************************

    /// <inheritdoc/>
    public virtual async Task<IdentityResource> UpdateAsync(
        IdentityResource identityResource,
        CancellationToken cancellationToken = default
        )
    {
        // Validate the parameters before attempting to use them.
        Guard.Instance().ThrowIfNull(identityResource, nameof(identityResource));

        try
        {
            // Log what we are about to do.
            _logger.LogDebug(
                "Looking for a matching {entity} entity in the {ctx} data-context.",
                nameof(IdentityResource),
                nameof(ConfigurationDbContext)
                );

            // Look for the given entity.
            var entity = await _configurationDbContext.IdentityResources.Where(x =>
                x.Name == identityResource.Name
                ).Include(x => x.UserClaims)
                 .Include(x => x.Properties)
                 .FirstOrDefaultAsync(
                    cancellationToken
                    ).ConfigureAwait(false);

            // Did we fail?
            if (entity is null)
            {
                // Panic!!
                throw new KeyNotFoundException(
                    $"The api scope: {identityResource.Name} was not found!"
                    );
            }

            // Log what we are about to do.
            _logger.LogDebug(
                "Updating a {entity} entity in the {ctx} data-context.",
                nameof(IdentityResource),
                nameof(ConfigurationDbContext)
                );

            // Update the editable properties.
            entity.Name = identityResource.Name;
            entity.DisplayName = identityResource.DisplayName;
            entity.Description = identityResource.Description;
            entity.Required = identityResource.Required;
            entity.Enabled = identityResource.Enabled;
            entity.Emphasize = identityResource.Emphasize;

            // Find any claims that were deleted.
            var deletedClaims = entity.UserClaims.Where(p1 =>
                identityResource.UserClaims.All(p2 => p2 != p1.Type)
                ).ToList();

            // Loop and remove claims.
            foreach (var claim in deletedClaims)
            {
                entity.UserClaims.Remove(claim);
            }

            // Find any claims that were added.
            var addedClaims = identityResource.UserClaims.Where(p1 =>
                entity.UserClaims.All(p2 => p2.Type != p1)
                ).ToList();

            // Loop and add claims.
            foreach (var claim in addedClaims)
            {
                entity.UserClaims.Add(new Duende.IdentityServer.EntityFramework.Entities.IdentityResourceClaim()
                {
                    IdentityResourceId = entity.Id,
                    Type = claim
                });
            }
            
            // Find any properties that were deleted.
            var deletedProps = entity.Properties.Where(p1 =>
                identityResource.Properties.All(p2 => p2.Key != p1.Key)
                ).ToList();

            // Loop and remove properties.
            foreach (var prop in deletedProps)
            {
                entity.Properties.Remove(prop);
            }

            // Find any properties that were added.
            var addedProps = identityResource.Properties.Where(p1 =>
                entity.Properties.All(p2 => p2.Key != p1.Key)
                ).ToList();

            // Loop and add properties.
            foreach (var prop in addedProps)
            {
                entity.Properties.Add(new Duende.IdentityServer.EntityFramework.Entities.IdentityResourceProperty()
                {
                    IdentityResourceId = entity.Id,
                    Key = prop.Key,
                    Value = prop.Value
                });
            }
            
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
                nameof(IdentityResource)
                );

            // Convert the entity to a model.
            var result = entity.ToModel();

            // Did we fail?
            if (result is null)
            {
                // Panic!!
                throw new AutoMapperMappingException(
                    $"Failed to map the {nameof(IdentityResource)} entity " +
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
                "Failed to update an identity resource"
                );

            // Provider better context.
            throw new RepositoryException(
                message: $"The repository failed to update an identity resource!",
                innerException: ex
                );
        }
    }

    #endregion
}
