
namespace CG.Green.Directors;

/// <summary>
/// This class is a default implementation of the <see cref="ISeedDirector"/>
/// interface.
/// </summary>
public class SeedDirector : SeedDirectorBase<SeedDirector>, ISeedDirector
{
    // *******************************************************************
    // Fields.
    // *******************************************************************

    #region Fields

    /// <summary>
    /// This field contains the Green API for this director.
    /// </summary>
    internal protected readonly IGreenApi _greenApi = null!;

    #endregion

    // *******************************************************************
    // Constructors.
    // *******************************************************************

    #region Constructors

    /// <summary>
    /// This constructor creates a new instance of the <see cref="SeedDirector"/>
    /// class.
    /// </summary>
    /// <param name="greenApi">The Green API to use with this director.</param>
    /// <param name="logger">The logger to use with this director.</param>
    public SeedDirector(
        IGreenApi greenApi,
        ILogger<SeedDirector> logger
        ) : base(logger)
    {
        // Validate the arguments before attempting to use them.
        Guard.Instance().ThrowIfNull(greenApi, nameof(greenApi));

        // Save the reference(s).
        _greenApi = greenApi;
    }

    #endregion

    // *******************************************************************
    // Public methods.
    // *******************************************************************

    #region Public methods

    /// <inheritdoc/>
    public virtual async Task SeedApiScopesAsync(
        List<ApiScope> apiScopes,
        string userName,
        bool force = false,
        CancellationToken cancellationToken = default
        )
    {
        // Validate the parameters before attempting to use them.
        Guard.Instance().ThrowIfNull(apiScopes, nameof(apiScopes))
            .ThrowIfNullOrEmpty(userName, nameof(userName));

        try
        {
            // Log what we are about to do.
            _logger.LogDebug(
                "Checking the force flag"
                );

            // Should we check for existing data?
            if (!force)
            {
                // Log what we are about to do.
                _logger.LogDebug(
                    "Checking for existing api scopes"
                    );

                // Are there existing api scopes?
                var hasExistingData = await _greenApi.ApiScopes.AnyAsync(
                    cancellationToken
                    ).ConfigureAwait(false);

                // Should we stop?
                if (hasExistingData)
                {
                    // Log what we didn't do.
                    _logger.LogWarning(
                        "Skipping seeding api scopes because the 'force' flag " +
                        "was not specified and there are existing rows in the " +
                        "database."
                        );
                    return; // Nothing else to do!
                }
            }

            // Start by counting how many objects are already there.
            var originalApiScopeCount = await _greenApi.ApiScopes.CountAsync(
                cancellationToken
                ).ConfigureAwait(false);

            // Log what we are about to do.
            _logger.LogDebug(
                "Seeding '{count}' api scopes",
                apiScopes.Count()
                );

            // Loop through the objects.
            foreach (var apiScope in apiScopes)
            {
                // Log what we are about to do.
                _logger.LogDebug(
                    "Creating an api scope: {e}",
                    apiScope.Name
                    );

                // Create the api scope.
                _ = await _greenApi.ApiScopes.CreateAsync(
                    apiScope,
                    userName,
                    cancellationToken
                    ).ConfigureAwait(false);
            }

            // Count how many objects there are now.
            var finalApiScopeCount = await _greenApi.ApiScopes.CountAsync(
                cancellationToken
                ).ConfigureAwait(false);

            // Log what we did.
            _logger.LogInformation(
                "Seeded a total of {count} api scopes",
                finalApiScopeCount - originalApiScopeCount
                );
        }
        catch (Exception ex)
        {
            // Log what happened.
            _logger.LogError(
                ex,
                "Failed to seed one or more api scopes!"
                );

            // Provider better context.
            throw new DirectorException(
                message: $"The director failed to seed one or more " +
                "api scopess!",
                innerException: ex
                );
        }
    }

    // *******************************************************************

    /// <inheritdoc/>
    public virtual async Task SeedClientsAsync(
        List<Client> clients,
        string userName,
        bool force = false,
        CancellationToken cancellationToken = default
        )
    {
        // Validate the parameters before attempting to use them.
        Guard.Instance().ThrowIfNull(clients, nameof(clients))
            .ThrowIfNullOrEmpty(userName, nameof(userName));

        try
        {
            // Log what we are about to do.
            _logger.LogDebug(
                "Checking the force flag"
                );

            // Should we check for existing data?
            if (!force)
            {
                // Log what we are about to do.
                _logger.LogDebug(
                    "Checking for existing clients"
                    );

                // Are there existing clients?
                var hasExistingData = await _greenApi.Clients.AnyAsync(
                    cancellationToken
                    ).ConfigureAwait(false);

                // Should we stop?
                if (hasExistingData)
                {
                    // Log what we didn't do.
                    _logger.LogWarning(
                        "Skipping seeding clients because the 'force' flag " +
                        "was not specified and there are existing rows in the " +
                        "database."
                        );
                    return; // Nothing else to do!
                }
            }

            // Start by counting how many objects are already there.
            var originalClientCount = await _greenApi.Clients.CountAsync(
                cancellationToken
                ).ConfigureAwait(false);

            // Log what we are about to do.
            _logger.LogDebug(
                "Seeding '{count}' clients",
                clients.Count()
                );

            // Loop through the objects.
            foreach (var client in clients)
            {
                // Log what we are about to do.
                _logger.LogDebug(
                    "Looping through '{count}' secrets for client: '{client}'",
                    client.ClientSecrets.Count(),
                    client.ClientId
                    );

                // Loop through the secrets.
                foreach (var secret in client.ClientSecrets)
                {
                    // Hash the secret.
                    secret.Value = secret.Value.ToSha256();
                }

                // Log what we are about to do.
                _logger.LogDebug(
                    "Creating a client: {e}",
                    client.ClientName
                    );

                // Create the client.
                _ = await _greenApi.Clients.CreateAsync(
                    client,
                    userName,
                    cancellationToken
                    ).ConfigureAwait(false);
            }

            // Count how many objects there are now.
            var finalClientCount = await _greenApi.Clients.CountAsync(
                cancellationToken
                ).ConfigureAwait(false);

            // Log what we did.
            _logger.LogInformation(
                "Seeded a total of {count} clients",
                finalClientCount - originalClientCount
                );
        }
        catch (Exception ex)
        {
            // Log what happened.
            _logger.LogError(
                ex,
                "Failed to seed one or more clients!"
                );

            // Provider better context.
            throw new DirectorException(
                message: $"The director failed to seed one or more " +
                "clients!",
                innerException: ex
                );
        }
    }

    // *******************************************************************

    /// <inheritdoc/>
    public virtual async Task SeedIdentityResourcesAsync(
        List<IdentityResource> identityResources,
        string userName,
        bool force = false,
        CancellationToken cancellationToken = default
        )
    {
        // Validate the parameters before attempting to use them.
        Guard.Instance().ThrowIfNull(identityResources, nameof(identityResources))
            .ThrowIfNullOrEmpty(userName, nameof(userName));

        try
        {
            // Log what we are about to do.
            _logger.LogDebug(
                "Checking the force flag"
                );

            // Should we check for existing data?
            if (!force)
            {
                // Log what we are about to do.
                _logger.LogDebug(
                    "Checking for existing identity resources"
                    );

                // Are there existing identity resources?
                var hasExistingData = await _greenApi.IdentityResources.AnyAsync(
                    cancellationToken
                    ).ConfigureAwait(false);

                // Should we stop?
                if (hasExistingData)
                {
                    // Log what we didn't do.
                    _logger.LogWarning(
                        "Skipping seeding identity resources because the 'force' flag " +
                        "was not specified and there are existing rows in the " +
                        "database."
                        );
                    return; // Nothing else to do!
                }
            }

            // Start by counting how many objects are already there.
            var originalIdentityResourceCount = await _greenApi.IdentityResources.CountAsync(
                cancellationToken
                ).ConfigureAwait(false);

            // Log what we are about to do.
            _logger.LogDebug(
                "Seeding '{count}' identity resources",
                identityResources.Count()
                );

            // Loop through the objects.
            foreach (var identityResource in identityResources)
            {
                // Log what we are about to do.
                _logger.LogDebug(
                    "Creating an identity resource: {e}",
                    identityResource.Name
                    );

                // Create the identity resource.
                _ = await _greenApi.IdentityResources.CreateAsync(
                    identityResource,
                    userName,
                    cancellationToken
                    ).ConfigureAwait(false);
            }

            // Count how many objects there are now.
            var finalIdentityResourceCount = await _greenApi.IdentityResources.CountAsync(
                cancellationToken
                ).ConfigureAwait(false);

            // Log what we did.
            _logger.LogInformation(
                "Seeded a total of {count} identity resources",
                finalIdentityResourceCount - originalIdentityResourceCount
                );
        }
        catch (Exception ex)
        {
            // Log what happened.
            _logger.LogError(
                ex,
                "Failed to seed one or more identity resources!"
                );

            // Provider better context.
            throw new DirectorException(
                message: $"The director failed to seed one or more " +
                "identity resources!",
                innerException: ex
                );
        }
    }

    // *******************************************************************

    /// <inheritdoc/>
    public virtual async Task SeedRolesAsync(
        List<GreenRole> roles,
        string userName,
        bool force = false,
        CancellationToken cancellationToken = default
        )
    {
        // Validate the parameters before attempting to use them.
        Guard.Instance().ThrowIfNull(roles, nameof(roles))
            .ThrowIfNullOrEmpty(userName, nameof(userName));

        try
        {
            // Log what we are about to do.
            _logger.LogDebug(
                "Checking the force flag"
                );

            // Should we check for existing data?
            if (!force)
            {
                // Log what we are about to do.
                _logger.LogDebug(
                    "Checking for existing roles"
                    );

                // Are there existing identity resources?
                var hasExistingData = await _greenApi.Roles.AnyAsync(
                    cancellationToken
                    ).ConfigureAwait(false);

                // Should we stop?
                if (hasExistingData)
                {
                    // Log what we didn't do.
                    _logger.LogWarning(
                        "Skipping seeding roles because the 'force' flag " +
                        "was not specified and there are existing rows in the " +
                        "database."
                        );
                    return; // Nothing else to do!
                }
            }

            // Start by counting how many objects are already there.
            var originalRoleCount = await _greenApi.Roles.CountAsync(
                cancellationToken
                ).ConfigureAwait(false);

            // Log what we are about to do.
            _logger.LogDebug(
                "Seeding '{count}' roles",
                roles.Count()
                );

            // Loop through the objects.
            foreach (var role in roles)
            {
                // Log what we are about to do.
                _logger.LogDebug(
                    "Creating a role: {e}",
                    role.Name
                    );

                // Create the role.
                _ = await _greenApi.Roles.CreateAsync(
                    role,
                    userName,
                    cancellationToken
                    ).ConfigureAwait(false);
            }

            // Count how many objects there are now.
            var finalRoleCount = await _greenApi.Users.CountAsync(
                cancellationToken
                ).ConfigureAwait(false);

            // Log what we did.
            _logger.LogInformation(
                "Seeded a total of {count} roles",
                finalRoleCount - originalRoleCount
                );
        }
        catch (Exception ex)
        {
            // Log what happened.
            _logger.LogError(
                ex,
                "Failed to seed one or more roles!"
                );

            // Provider better context.
            throw new DirectorException(
                message: $"The director failed to seed one or more " +
                "roles!",
                innerException: ex
                );
        }
    }

    // *******************************************************************

    /// <inheritdoc/>
    public virtual async Task SeedRoleClaimsAsync(
        List<RoleClaimAssignmentOptions> roleClaims,
        string userName,
        bool force = false,
        CancellationToken cancellationToken = default
        )
    {
        // Validate the parameters before attempting to use them.
        Guard.Instance().ThrowIfNull(roleClaims, nameof(roleClaims))
            .ThrowIfNullOrEmpty(userName, nameof(userName));

        try
        {
            // Log what we are about to do.
            _logger.LogDebug(
                "Checking the force flag"
                );

            // Should we check for existing data?
            if (!force)
            {
                // Log what we are about to do.
                _logger.LogDebug(
                    "Checking for existing role claims"
                    );

                // Are there existing role claims?
                var hasExistingData = await _greenApi.RoleClaims.AnyAsync(
                    cancellationToken
                    ).ConfigureAwait(false);

                // Should we stop?
                if (hasExistingData)
                {
                    // Log what we didn't do.
                    _logger.LogWarning(
                        "Skipping seeding role claims because the 'force' flag " +
                        "was not specified and there are existing rows in the " +
                        "database."
                        );
                    return; // Nothing else to do!
                }
            }

            // Start by counting how many objects are already there.
            var originalRoleClaimCount = await _greenApi.RoleClaims.CountAsync(
                cancellationToken
                ).ConfigureAwait(false);

            // Log what we are about to do.
            _logger.LogDebug(
                "Seeding '{count}' role claims",
                roleClaims.Count()
                );

            // Loop through the objects.
            foreach (var roleClaim in roleClaims)
            {
                // Look for the associated role.
                var role = await _greenApi.Roles.FindByNameAsync(
                    roleClaim.RoleName
                    );

                // Did we fail?
                if (role is null)
                {
                    // Panic!!
                    throw new KeyNotFoundException(
                        $"Role: {roleClaim.RoleName} was not found!"
                        );
                }

                // Log what we are about to do.
                _logger.LogDebug(
                    "Seeding '{count}' claim assignments to role: {e}",
                    roleClaim.Claims.Count(),
                    roleClaim.RoleName
                    );

                // Loop through the objects.
                foreach (var claim in roleClaim.Claims)
                {
                    // Log what we are about to do.
                    _logger.LogDebug(
                        "Creating claim: {e}",
                        claim.ClaimType
                        );

                    // Assign the claim.
                    await _greenApi.RoleClaims.CreateAsync(
                        new GreenRoleClaim()
                        { 
                            RoleId = role.Id,
                            ClaimType = claim.ClaimType,    
                            ClaimValue = claim.ClaimValue
                        },
                        userName,
                        cancellationToken
                        ).ConfigureAwait(false);
                }
            }

            // Count how many objects there are now.
            var finalRoleClaimCount = await _greenApi.RoleClaims.CountAsync(
                cancellationToken
                ).ConfigureAwait(false);

            // Log what we did.
            _logger.LogInformation(
                "Seeded a total of {count} role claims",
                finalRoleClaimCount - originalRoleClaimCount
                );
        }
        catch (Exception ex)
        {
            // Log what happened.
            _logger.LogError(
                ex,
                "Failed to seed one or more role claims!"
                );

            // Provider better context.
            throw new DirectorException(
                message: $"The director failed to seed one or more " +
                "role claims!",
                innerException: ex
                );
        }
    }

    // *******************************************************************

    /// <inheritdoc/>
    public virtual async Task SeedUsersAsync(
        List<GreenUser> users,
        string userName,
        bool force = false,
        CancellationToken cancellationToken = default
        )
    {
        // Validate the parameters before attempting to use them.
        Guard.Instance().ThrowIfNull(users, nameof(users))
            .ThrowIfNullOrEmpty(userName, nameof(userName));

        try
        {
            // Log what we are about to do.
            _logger.LogDebug(
                "Checking the force flag"
                );

            // Should we check for existing data?
            if (!force)
            {
                // Log what we are about to do.
                _logger.LogDebug(
                    "Checking for existing users"
                    );

                // Are there existing identity resources?
                var hasExistingData = await _greenApi.Users.AnyAsync(
                    cancellationToken
                    ).ConfigureAwait(false);

                // Should we stop?
                if (hasExistingData)
                {
                    // Log what we didn't do.
                    _logger.LogWarning(
                        "Skipping seeding users because the 'force' flag " +
                        "was not specified and there are existing rows in the " +
                        "database."
                        );
                    return; // Nothing else to do!
                }
            }

            // Start by counting how many objects are already there.
            var originalUserCount = await _greenApi.Users.CountAsync(
                cancellationToken
                ).ConfigureAwait(false);

            // Log what we are about to do.
            _logger.LogDebug(
                "Seeding '{count}' users",
                users.Count()
                );

            // Loop through the objects.
            foreach (var user in users)
            {
                // Log what we are about to do.
                _logger.LogDebug(
                    "Creating a user: {e}",
                    user.UserName
                    );

                // Create the user.
                _ = await _greenApi.Users.CreateAsync(
                    user,
                    user.PasswordHash ?? "",
                    userName,
                    cancellationToken
                    ).ConfigureAwait(false);
            }

            // Count how many objects there are now.
            var finalUserCount = await _greenApi.Users.CountAsync(
                cancellationToken
                ).ConfigureAwait(false);

            // Log what we did.
            _logger.LogInformation(
                "Seeded a total of {count} users",
                finalUserCount - originalUserCount
                );
        }
        catch (Exception ex)
        {
            // Log what happened.
            _logger.LogError(
                ex,
                "Failed to seed one or more users!"
                );

            // Provider better context.
            throw new DirectorException(
                message: $"The director failed to seed one or more " +
                "users!",
                innerException: ex
                );
        }
    }

    // *******************************************************************

    /// <inheritdoc/>
    public virtual async Task SeedUserClaimsAsync(
        List<UserClaimAssignmentOptions> userClaims,
        string userName,
        bool force = false,
        CancellationToken cancellationToken = default
        )
    {
        // Validate the parameters before attempting to use them.
        Guard.Instance().ThrowIfNull(userClaims, nameof(userClaims))
            .ThrowIfNullOrEmpty(userName, nameof(userName));

        try
        {
            // Log what we are about to do.
            _logger.LogDebug(
                "Checking the force flag"
                );

            // Should we check for existing data?
            if (!force)
            {
                // Log what we are about to do.
                _logger.LogDebug(
                    "Checking for existing user claims"
                    );

                // Are there existing user claims?
                var hasExistingData = await _greenApi.UserClaims.AnyAsync(
                    cancellationToken
                    ).ConfigureAwait(false);

                // Should we stop?
                if (hasExistingData)
                {
                    // Log what we didn't do.
                    _logger.LogWarning(
                        "Skipping seeding user claims because the 'force' flag " +
                        "was not specified and there are existing rows in the " +
                        "database."
                        );
                    return; // Nothing else to do!
                }
            }

            // Start by counting how many objects are already there.
            var originalUserClaimCount = await _greenApi.UserClaims.CountAsync(
                cancellationToken
                ).ConfigureAwait(false);

            // Log what we are about to do.
            _logger.LogDebug(
                "Seeding '{count}' user claims",
                userClaims.Count()
                );

            // Loop through the objects.
            foreach (var userClaim in userClaims)
            {
                // Log what we are about to do.
                _logger.LogDebug(
                    "Seeding '{count}' claim assignments to user: {e}",
                    userClaim.Claims.Count(),
                    userClaim.UserEmail
                    );

                // Loop through the objects.
                foreach (var claim in userClaim.Claims)
                {
                    // Log what we are about to do.
                    _logger.LogDebug(
                        "Creating claim: {e}",
                        claim.ClaimType
                        );

                    // Assign the claim.
                    await _greenApi.UserClaims.CreateAsync(
                        userClaim.UserEmail,
                        claim.ClaimType,
                        claim.ClaimValue ?? "",
                        userName,
                        cancellationToken
                        ).ConfigureAwait(false);
                }                
            }

            // Count how many objects there are now.
            var finalUserClaimCount = await _greenApi.UserClaims.CountAsync(
                cancellationToken
                ).ConfigureAwait(false);

            // Log what we did.
            _logger.LogInformation(
                "Seeded a total of {count} user claims",
                finalUserClaimCount - originalUserClaimCount
                );
        }
        catch (Exception ex)
        {
            // Log what happened.
            _logger.LogError(
                ex,
                "Failed to seed one or more user claims!"
                );

            // Provider better context.
            throw new DirectorException(
                message: $"The director failed to seed one or more " +
                "user claims!",
                innerException: ex
                );
        }
    }

    // *******************************************************************

    /// <inheritdoc/>
    public virtual async Task SeedUserRolesAsync(
        List<UserRoleAssignmentOptions> userRoles,
        string userName,
        bool force = false,
        CancellationToken cancellationToken = default
        )
    {
        // Validate the parameters before attempting to use them.
        Guard.Instance().ThrowIfNull(userRoles, nameof(userRoles))
            .ThrowIfNullOrEmpty(userName, nameof(userName));

        try
        {
            // Log what we are about to do.
            _logger.LogDebug(
                "Checking the force flag"
                );

            // Should we check for existing data?
            if (!force)
            {
                // Log what we are about to do.
                _logger.LogDebug(
                    "Checking for existing user roles"
                    );

                // Are there existing identity resources?
                var hasExistingData = await _greenApi.UserRoles.AnyAsync(
                    cancellationToken
                    ).ConfigureAwait(false);

                // Should we stop?
                if (hasExistingData)
                {
                    // Log what we didn't do.
                    _logger.LogWarning(
                        "Skipping seeding user roles because the 'force' flag " +
                        "was not specified and there are existing rows in the " +
                        "database."
                        );
                    return; // Nothing else to do!
                }
            }

            // Start by counting how many objects are already there.
            var originalUserRoleCount = await _greenApi.UserRoles.CountAsync(
                cancellationToken
                ).ConfigureAwait(false);

            // Log what we are about to do.
            _logger.LogDebug(
                "Seeding '{count}' user roles",
                userRoles.Count()
                );

            // Loop through the objects.
            foreach (var userRole in userRoles)
            {
                // Log what we are about to do.
                _logger.LogDebug(
                    "Seeding '{count}' role names for user: {user}",
                    userRole.RoleNames.Count()
                    );

                // Loop through the objects.
                foreach (var roleName in userRole.RoleNames)
                {
                    // Log what we are about to do.
                    _logger.LogDebug(
                        "Assigning role: {e} to user: {y}",
                        roleName,
                        userRole.UserEmail
                        );

                    // Assign the role.
                    await _greenApi.UserRoles.CreateAsync(
                        userRole.UserEmail,
                        roleName,
                        userName,
                        cancellationToken
                        ).ConfigureAwait(false);
                }                
            }

            // Count how many objects there are now.
            var finalUserRoleCount = await _greenApi.UserRoles.CountAsync(
                cancellationToken
                ).ConfigureAwait(false);

            // Log what we did.
            _logger.LogInformation(
                "Seeded a total of {count} user roles",
                finalUserRoleCount - originalUserRoleCount
                );
        }
        catch (Exception ex)
        {
            // Log what happened.
            _logger.LogError(
                ex,
                "Failed to seed one or more user roles!"
                );

            // Provider better context.
            throw new DirectorException(
                message: $"The director failed to seed one or more " +
                "user roles!",
                innerException: ex
                );
        }
    }

    #endregion

    // *******************************************************************
    // Protected methods.
    // *******************************************************************

    #region Protected methods

    /// <inheritdoc/>

    protected override async Task SeedFromConfiguration(
        string objectName, 
        IConfiguration dataSection, 
        string userName, 
        bool force = false, 
        CancellationToken cancellationToken = default
        )
    {
        // Log what we are about to do.
        _logger.LogTrace(
            "Performing a seeding operation for object: {name}",
            objectName
            );

        // Decide what to do with the incoming data.
        switch (objectName.ToLower().Trim())
        {
            case "apiscopes":
                await SeedApiScopesAsync(
                    dataSection,
                    userName,
                    force,
                    cancellationToken
                    ).ConfigureAwait(false);
                break;
            case "clients":
                await SeedClientsAsync(
                    dataSection,
                    userName,
                    force,
                    cancellationToken
                    ).ConfigureAwait(false);
                break;
            case "identityresources":
                await SeedIdentityResourcesAsync(
                    dataSection,
                    userName,
                    force,
                    cancellationToken
                    ).ConfigureAwait(false);
                break;
            case "roles":
                await SeedRolesAsync(
                    dataSection,
                    userName,
                    force,
                    cancellationToken
                    ).ConfigureAwait(false);
                break;
            case "roleclaims":
                await SeedRoleClaimsAsync(
                    dataSection,
                    userName,
                    force,
                    cancellationToken
                    ).ConfigureAwait(false);
                break;
            case "users":
                await SeedUsersAsync(
                    dataSection,
                    userName,
                    force,
                    cancellationToken
                    ).ConfigureAwait(false);
                break;
            case "userclaims":
                await SeedUserClaimsAsync(
                    dataSection,
                    userName,
                    force,
                    cancellationToken
                    ).ConfigureAwait(false);
                break;
            case "userroles":
                await SeedUserRolesAsync(
                    dataSection,
                    userName,
                    force,
                    cancellationToken
                    ).ConfigureAwait(false);
                break;
            default:
                throw new ArgumentException(
                    $"Don't know how to seed '{objectName}' types!"
                    );
        }
    }

    // *******************************************************************

    /// <summary>
    /// This method performs a seeding operation for <see cref="ApiScopeOptions"/>
    /// objects, from the given configuration.
    /// </summary>
    /// <param name="dataSection">The data to use for the operation.</param>
    /// <param name="userName">The role name of the person performing the 
    /// operation.</param>
    /// <param name="force"><c>true</c> to force the seeding operation when data
    /// already exists in the associated table(s), <c>false</c> to stop the 
    /// operation whenever data is detected in the associated table(s).</param>

    /// <param name="cancellationToken">A cancellation token that is monitored
    /// for the lifetime of the method.</param>
    /// <returns>A task to perform the operation.</returns>
    protected async virtual Task SeedApiScopesAsync(
        IConfiguration dataSection,
        string userName,
        bool force,
        CancellationToken cancellationToken = default
        )
    {
        // Log what we are about to do.
        _logger.LogDebug(
            "Binding the incoming seed data to the Duende model"
            );

        // Bind the incoming data to our options.
        var options = new ApiScopeOptions();
        dataSection.Bind(options);

        // Log what we are about to do.
        _logger.LogDebug(
            "Validating the incoming seed data"
            );

        // Validate the options.
        Guard.Instance().ThrowIfInvalidObject(options, nameof(options));

        // Log what we are about to do.
        _logger.LogTrace(
            "Deferring to the {name} method",
            nameof(ISeedDirector.SeedApiScopesAsync)
            );

        // Call the overload
        await SeedApiScopesAsync(
            options.ApiScopes,
            userName,
            force,
            cancellationToken
            ).ConfigureAwait(false);
    }

    // *******************************************************************

    /// <summary>
    /// This method performs a seeding operation for <see cref="ClientOptions"/>
    /// objects, from the given configuration.
    /// </summary>
    /// <param name="dataSection">The data to use for the operation.</param>
    /// <param name="userName">The role name of the person performing the 
    /// operation.</param>
    /// <param name="force"><c>true</c> to force the seeding operation when data
    /// already exists in the associated table(s), <c>false</c> to stop the 
    /// operation whenever data is detected in the associated table(s).</param>

    /// <param name="cancellationToken">A cancellation token that is monitored
    /// for the lifetime of the method.</param>
    /// <returns>A task to perform the operation.</returns>
    protected async virtual Task SeedClientsAsync(
        IConfiguration dataSection,
        string userName,
        bool force,
        CancellationToken cancellationToken = default
        )
    {
        // Log what we are about to do.
        _logger.LogDebug(
            "Binding the incoming seed data to the Duende model"
            );

        // Bind the incoming data to our options.
        var options = new ClientOptions();
        dataSection.Bind(options);

        // Log what we are about to do.
        _logger.LogDebug(
            "Validating the incoming seed data"
            );

        // Validate the options.
        Guard.Instance().ThrowIfInvalidObject(options, nameof(options));

        // Log what we are about to do.
        _logger.LogTrace(
            "Deferring to the {name} method",
            nameof(ISeedDirector.SeedClientsAsync)
            );

        // Call the overload
        await SeedClientsAsync(
            options.Clients,
            userName,
            force,
            cancellationToken
            ).ConfigureAwait(false);
    }

    // *******************************************************************

    /// <summary>
    /// This method performs a seeding operation for <see cref="IdentityResource"/>
    /// objects, from the given configuration.
    /// </summary>
    /// <param name="dataSection">The data to use for the operation.</param>
    /// <param name="userName">The role name of the person performing the 
    /// operation.</param>
    /// <param name="force"><c>true</c> to force the seeding operation when data
    /// already exists in the associated table(s), <c>false</c> to stop the 
    /// operation whenever data is detected in the associated table(s).</param>

    /// <param name="cancellationToken">A cancellation token that is monitored
    /// for the lifetime of the method.</param>
    /// <returns>A task to perform the operation.</returns>
    protected async virtual Task SeedIdentityResourcesAsync(
        IConfiguration dataSection,
        string userName,
        bool force,
        CancellationToken cancellationToken = default
        )
    {
        // Log what we are about to do.
        _logger.LogDebug(
            "Binding the incoming seed data to the Duende model"
            );

        // Bind the incoming data to our options.
        var options = new IdentityResourceOptions();
        dataSection.Bind(options);

        // Log what we are about to do.
        _logger.LogDebug(
            "Validating the incoming seed data"
            );

        // Validate the options.
        Guard.Instance().ThrowIfInvalidObject(options, nameof(options));

        // Log what we are about to do.
        _logger.LogTrace(
            "Deferring to the {name} method",
            nameof(ISeedDirector.SeedClientsAsync)
            );

        // Call the overload
        await SeedIdentityResourcesAsync(
            options.IdentityResources,
            userName,
            force,
            cancellationToken
            ).ConfigureAwait(false);
    }

    // *******************************************************************

    /// <summary>
    /// This method performs a seeding operation for <see cref="GreenRole"/>
    /// objects, from the given configuration.
    /// </summary>
    /// <param name="dataSection">The data to use for the operation.</param>
    /// <param name="userName">The role name of the person performing the 
    /// operation.</param>
    /// <param name="force"><c>true</c> to force the seeding operation when data
    /// already exists in the associated table(s), <c>false</c> to stop the 
    /// operation whenever data is detected in the associated table(s).</param>

    /// <param name="cancellationToken">A cancellation token that is monitored
    /// for the lifetime of the method.</param>
    /// <returns>A task to perform the operation.</returns>
    protected async virtual Task SeedRolesAsync(
        IConfiguration dataSection,
        string userName,
        bool force,
        CancellationToken cancellationToken = default
        )
    {
        // Log what we are about to do.
        _logger.LogDebug(
            "Binding the incoming seed data to the ASP.NET model"
            );

        // Bind the incoming data to our options.
        var options = new GreenRoleOptions();
        dataSection.Bind(options);

        // Log what we are about to do.
        _logger.LogDebug(
            "Validating the incoming seed data"
            );

        // Validate the options.
        Guard.Instance().ThrowIfInvalidObject(options, nameof(options));

        // Log what we are about to do.
        _logger.LogTrace(
            "Deferring to the {name} method",
            nameof(ISeedDirector.SeedRolesAsync)
            );

        // Call the overload
        await SeedRolesAsync(
            options.Roles,
            userName,
            force,
            cancellationToken
            ).ConfigureAwait(false);
    }

    // *******************************************************************

    /// <summary>
    /// This method performs a seeding operation for <see cref="GreenRoleClaim"/>
    /// objects, from the given configuration.
    /// </summary>
    /// <param name="dataSection">The data to use for the operation.</param>
    /// <param name="userName">The role name of the person performing the 
    /// operation.</param>
    /// <param name="force"><c>true</c> to force the seeding operation when data
    /// already exists in the associated table(s), <c>false</c> to stop the 
    /// operation whenever data is detected in the associated table(s).</param>

    /// <param name="cancellationToken">A cancellation token that is monitored
    /// for the lifetime of the method.</param>
    /// <returns>A task to perform the operation.</returns>
    protected async virtual Task SeedRoleClaimsAsync(
        IConfiguration dataSection,
        string userName,
        bool force,
        CancellationToken cancellationToken = default
        )
    {
        // Log what we are about to do.
        _logger.LogDebug(
            "Binding the incoming seed data to a model"
            );

        // Bind the incoming data to our options.
        var options = new RoleClaimOptions();
        dataSection.Bind(options);

        // Log what we are about to do.
        _logger.LogDebug(
            "Validating the incoming seed data"
            );

        // Validate the options.
        Guard.Instance().ThrowIfInvalidObject(options, nameof(options));

        // Log what we are about to do.
        _logger.LogTrace(
            "Deferring to the {name} method",
            nameof(ISeedDirector.SeedRoleClaimsAsync)
            );

        // Call the overload
        await SeedRoleClaimsAsync(
            options.RoleClaims,
            userName,
            force,
            cancellationToken
            ).ConfigureAwait(false);
    }

    // *******************************************************************

    /// <summary>
    /// This method performs a seeding operation for <see cref="GreenUser"/>
    /// objects, from the given configuration.
    /// </summary>
    /// <param name="dataSection">The data to use for the operation.</param>
    /// <param name="userName">The role name of the person performing the 
    /// operation.</param>
    /// <param name="force"><c>true</c> to force the seeding operation when data
    /// already exists in the associated table(s), <c>false</c> to stop the 
    /// operation whenever data is detected in the associated table(s).</param>

    /// <param name="cancellationToken">A cancellation token that is monitored
    /// for the lifetime of the method.</param>
    /// <returns>A task to perform the operation.</returns>
    protected async virtual Task SeedUsersAsync(
        IConfiguration dataSection,
        string userName,
        bool force,
        CancellationToken cancellationToken = default
        )
    {
        // Log what we are about to do.
        _logger.LogDebug(
            "Binding the incoming seed data to the ASP.NET model"
            );

        // Bind the incoming data to our options.
        var options = new GreenUserOptions();
        dataSection.Bind(options);

        // Log what we are about to do.
        _logger.LogDebug(
            "Validating the incoming seed data"
            );

        // Validate the options.
        Guard.Instance().ThrowIfInvalidObject(options, nameof(options));

        // Log what we are about to do.
        _logger.LogTrace(
            "Deferring to the {name} method",
            nameof(ISeedDirector.SeedUsersAsync)
            );

        // Call the overload
        await SeedUsersAsync(
            options.Users,
            userName,
            force,
            cancellationToken
            ).ConfigureAwait(false);
    }

    // *******************************************************************

    /// <summary>
    /// This method performs a seeding operation for <see cref="GreenUserClaim"/>
    /// objects, from the given configuration.
    /// </summary>
    /// <param name="dataSection">The data to use for the operation.</param>
    /// <param name="userName">The role name of the person performing the 
    /// operation.</param>
    /// <param name="force"><c>true</c> to force the seeding operation when data
    /// already exists in the associated table(s), <c>false</c> to stop the 
    /// operation whenever data is detected in the associated table(s).</param>

    /// <param name="cancellationToken">A cancellation token that is monitored
    /// for the lifetime of the method.</param>
    /// <returns>A task to perform the operation.</returns>
    protected async virtual Task SeedUserClaimsAsync(
        IConfiguration dataSection,
        string userName,
        bool force,
        CancellationToken cancellationToken = default
        )
    {
        // Log what we are about to do.
        _logger.LogDebug(
            "Binding the incoming seed data to a model"
            );

        // Bind the incoming data to our options.
        var options = new UserClaimOptions();
        dataSection.Bind(options);

        // Log what we are about to do.
        _logger.LogDebug(
            "Validating the incoming seed data"
            );

        // Validate the options.
        Guard.Instance().ThrowIfInvalidObject(options, nameof(options));

        // Log what we are about to do.
        _logger.LogTrace(
            "Deferring to the {name} method",
            nameof(ISeedDirector.SeedUserClaimsAsync)
            );

        // Call the overload
        await SeedUserClaimsAsync(
            options.UserClaims,
            userName,
            force,
            cancellationToken
            ).ConfigureAwait(false);
    }

    // *******************************************************************

    /// <summary>
    /// This method performs a seeding operation for <see cref="GreenUser"/>
    /// objects, from the given configuration.
    /// </summary>
    /// <param name="dataSection">The data to use for the operation.</param>
    /// <param name="userName">The role name of the person performing the 
    /// operation.</param>
    /// <param name="force"><c>true</c> to force the seeding operation when data
    /// already exists in the associated table(s), <c>false</c> to stop the 
    /// operation whenever data is detected in the associated table(s).</param>

    /// <param name="cancellationToken">A cancellation token that is monitored
    /// for the lifetime of the method.</param>
    /// <returns>A task to perform the operation.</returns>
    protected async virtual Task SeedUserRolesAsync(
        IConfiguration dataSection,
        string userName,
        bool force,
        CancellationToken cancellationToken = default
        )
    {
        // Log what we are about to do.
        _logger.LogDebug(
            "Binding the incoming seed data to a model"
            );

        // Bind the incoming data to our options.
        var options = new UserRoleOptions();
        dataSection.Bind(options);

        // Log what we are about to do.
        _logger.LogDebug(
            "Validating the incoming seed data"
            );

        // Validate the options.
        Guard.Instance().ThrowIfInvalidObject(options, nameof(options));

        // Log what we are about to do.
        _logger.LogTrace(
            "Deferring to the {name} method",
            nameof(ISeedDirector.SeedUserRolesAsync)
            );

        // Call the overload
        await SeedUserRolesAsync(
            options.UserRoles,
            userName,
            force,
            cancellationToken
            ).ConfigureAwait(false);
    }

    #endregion
}
