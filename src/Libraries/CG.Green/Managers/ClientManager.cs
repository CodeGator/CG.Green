
namespace CG.Green.Managers;

/// <summary>
/// This class is a default implementation of the <see cref="IClientManager"/>
/// interface.
/// </summary>
internal class ClientManager : IClientManager
{
    // *******************************************************************
    // Fields.
    // *******************************************************************

    #region Fields

    /// <summary>
    /// This field contains the client repository for this manager.
    /// </summary>
    internal protected readonly IClientRepository _clientRepository = null!;

    /// <summary>
    /// This field contains the logger for this manager.
    /// </summary>
    internal protected readonly ILogger<IClientManager> _logger;

    #endregion

    // *******************************************************************
    // Constructors.
    // *******************************************************************

    #region Constructors

    /// <summary>
    /// This constructor creates a new instance of the <see cref="ClientManager"/>
    /// class.
    /// </summary>
    /// <param name="clientRepository">The client repository to use with 
    /// this manager.</param>
    /// <param name="logger">The logger to use with this manager.</param>
    public ClientManager(
        IClientRepository clientRepository,
        ILogger<IClientManager> logger
        )
    {
        // Validate the arguments before attempting to use them.
        Guard.Instance().ThrowIfNull(clientRepository, nameof(clientRepository))
            .ThrowIfNull(logger, nameof(logger));

        // Save the reference(s).
        _clientRepository = clientRepository;
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
                nameof(IClientRepository.AnyAsync)
                );

            // Check the repository for the data.
            var result = await _clientRepository.AnyAsync(
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
                "Failed to search for clients!"
                );

            // Provider better context.
            throw new ManagerException(
                message: $"The manager failed to search for clients!",
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
                nameof(IClientRepository.CountAsync)
                );

            // Perform the search.
            var result = await _clientRepository.CountAsync(
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
                "Failed to count clients!"
                );

            // Provider better context.
            throw new ManagerException(
                message: $"The manager failed to count clients!",
                innerException: ex
                );
        }
    }

    // *******************************************************************

    /// <inheritdoc/>
    public virtual async Task<Client> CreateAsync(
        Client client,
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
                nameof(Client)
                );

            // Ensure the stats are correct.
            //client.CreatedOnUtc = DateTime.UtcNow;
            //client.CreatedBy = userName;
            //client.LastUpdatedBy = null;
            //client.LastUpdatedOnUtc = null;

            // Extensions are always lower case.
            //client.Extension = client.Extension.ToLower().Trim();

            // Extensions always start with a '.' character.
            //if (!client.Extension.StartsWith('.'))
            //{
            //    client.Extension = $".{client.Extension}";
           // }

            // Log what we are about to do.
            _logger.LogTrace(
                "Deferring to {name}",
                nameof(IClientRepository.CreateAsync)
                );

            // Perform the operation.
            var result = await _clientRepository.CreateAsync(
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
                "Failed to create a new client!"
                );

            // Provider better context.
            throw new ManagerException(
                message: $"The manager failed to create a new client!",
                innerException: ex
                );
        }
    }

    // *******************************************************************

    /// <inheritdoc/>
    public virtual async Task DeleteAsync(
        Client client,
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
                nameof(Client)
                );

            // Ensure the stats are correct.
            //client.LastUpdatedOnUtc = DateTime.UtcNow;
            //client.LastUpdatedBy = userName;

            // Log what we are about to do.
            _logger.LogTrace(
                "Deferring to {name}",
                nameof(IClientRepository.DeleteAsync)
                );

            // Perform the operation.
            await _clientRepository.DeleteAsync(
                client,
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
            throw new ManagerException(
                message: $"The manager failed to delete a client!",
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
            _logger.LogTrace(
                "Deferring to {name}",
                nameof(IClientRepository.FindAllAsync)
                );

            // Perform the operation.
            var clients = await _clientRepository.FindAllAsync(
                cancellationToken
                ).ConfigureAwait(false);

            // Return the results.
            return clients;
        }
        catch (Exception ex)
        {
            // Log what happened.
            _logger.LogError(
                ex,
                "Failed to search for clients!"
                );

            // Provider better context.
            throw new ManagerException(
                message: $"The manager failed to search for clients!",
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
            _logger.LogTrace(
                "Deferring to {name}",
                nameof(IClientRepository.FindByIdAsync)
                );

            // Perform the operation.
            var client = await _clientRepository.FindByIdAsync(
                clientId,
                cancellationToken
                ).ConfigureAwait(false);

            // Return the results.
            return client;
        }
        catch (Exception ex)
        {
            // Log what happened.
            _logger.LogError(
                ex,
                "Failed to search for a client by id!"
                );

            // Provider better context.
            throw new ManagerException(
                message: $"The manager failed to search for a client " +
                "by id!",
                innerException: ex
                );
        }
    }

    // *******************************************************************

    /// <inheritdoc/>
    public virtual async Task<Client> UpdateAsync(
        Client client,
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
                nameof(Client)
                );

            // Ensure the stats are correct.
            //client.LastUpdatedOnUtc = DateTime.UtcNow;
            //client.LastUpdatedBy = userName;

            // Extensions are always lower case.
            //client.Extension = client.Extension.ToLower().Trim();

            // Extensions always start with a '.' character.
            //if (!client.Extension.StartsWith('.'))
            //{
            //    client.Extension = $".{client.Extension}";
            //}

            // Log what we are about to do.
            _logger.LogTrace(
                "Deferring to {name}",
                nameof(IClientRepository.UpdateAsync)
                );

            // Perform the operation.
            var updatedClient = await _clientRepository.UpdateAsync(
                client,
                cancellationToken
                ).ConfigureAwait(false);

            // Return the results.
            return updatedClient;
        }
        catch (Exception ex)
        {
            // Log what happened.
            _logger.LogError(
                ex,
                "Failed to update a client!"
                );

            // Provider better context.
            throw new ManagerException(
                message: $"The manager failed to update a client!",
                innerException: ex
                );
        }
    }

    #endregion
}
