
namespace Microsoft.AspNetCore.Builder;

/// <summary>
/// This class contains extension methods related to the <see cref="WebApplication"/>
/// type.
/// </summary>
public static class WebApplicationExtensions002
{
    // *******************************************************************
    // Public methods.
    // *******************************************************************

    #region Public methods

    /// <summary>
    /// This method performs startup operations for this MySql provider.
    /// </summary>
    /// <param name="webApplication">The web application to use for the 
    /// operation.</param>
    /// <returns>The value of the <paramref name="webApplication"/>
    /// parameter, for chaining calls together, Fluent style.</returns>
    /// <remarks>
    /// <para>
    /// This method must NOT have its signature changed! The method follows 
    /// a convention used by the <see cref="CG.EntityFrameworkCore"/> 
    /// package.
    /// </para>
    /// </remarks>
    public static WebApplication UseMySqlDataAccess(
        this WebApplication webApplication
        )
    {
        // Validate the parameters before attempting to use them.
        Guard.Instance().ThrowIfNull(webApplication, nameof(webApplication));

        // Log what we are about to do.
        webApplication.Logger.LogDebug(
            "Checking the application's environment for DAL startup."
            );

        // We only touch the database in a development environment.
        if (webApplication.Environment.IsDevelopment())
        {
            // Log what we are about to do.
            webApplication.Logger.LogDebug(
                "Fetching the DAL startup options from the DI container."
                );

            // Get the DAL startup options.
            var dalStartOptions = webApplication.Services.GetRequiredService<
                IOptions<DataAcessLayerOptions>
                >();

            // Should we drop the underlying database?
            if (dalStartOptions.Value.DropDatabaseOnStartup)
            {
                // Drop (and re-create) the databases.
                webApplication.DropAndRecreateDatabaseAsync().Wait();
            }
            else
            {
                // Log what we didn't do.
                webApplication.Logger.LogWarning(
                    "Skipping drop and recreate of databases because " +
                    "the '{flag}' flag is either false, or missing.",
                    nameof(dalStartOptions.Value.DropDatabaseOnStartup)
                    );

                // Should we apply any pending migrations?
                if (dalStartOptions.Value.MigrateDatabaseOnStartup)
                {
                    // Migrate the databases.
                    webApplication.MigrateDatabaseAsync().Wait();
                }
                else
                {
                    // Log what we didn't do.
                    webApplication.Logger.LogWarning(
                        "Skipping migration because the '{flag}' flag is either " +
                        "false, or missing.",
                        nameof(dalStartOptions.Value.MigrateDatabaseOnStartup)
                        );
                }
            }
        }
        else
        {
            // Log what we didn't do.
            webApplication.Logger.LogInformation(
                "Ignoring DAL startup because we aren't in a development " +
                "environment."
                );
        }

        // Return the application.
        return webApplication;
    }

    #endregion

    // *******************************************************************
    // Private methods.
    // *******************************************************************

    #region Private methods

    /// <summary>
    /// This method drops and recreates the database.
    /// </summary>
    /// <param name="webApplication">The web application to use for the 
    /// operation.</param>
    /// <param name="cancellationToken">A cancellation token that is monitored
    /// throughout the lifetime of the method.</param>
    /// <returns>A task to perform the operation.</returns>
    private static async Task DropAndRecreateDatabaseAsync(
        this WebApplication webApplication,
        CancellationToken cancellationToken = default
        )
    {
        // Here we only need to drop and re-create a single underlying 
        //   database, since all our data-contexts point to the same
        //   db. If that ever changes we'll have to rethink this method.

        // Log what we are about to do.
        webApplication.Logger.LogDebug(
            "Creating a DI scope."
            );

        // Create a DI scope.
        using var scope = webApplication.Services.CreateScope();

        // Log what we are about to do.
        webApplication.Logger.LogDebug(
            "Creating a {ctx} instance.",
            nameof(AspNetDbContext)
            );

        // Create a data-context.
        var greenDbContext = scope.ServiceProvider.GetRequiredService<
            AspNetDbContext
            >();

        // Log what we are about to do.
        webApplication.Logger.LogInformation(
            "Dropping the database '{db}', on server '{srv}'",
            greenDbContext.Database.GetDatabaseName(),
            greenDbContext.Database.GetServerName()
            );

        // Drop any existing database.
        await greenDbContext.Database.EnsureDeletedAsync(
            cancellationToken
            ).ConfigureAwait(false);

        // Log what we are about to do.
        webApplication.Logger.LogTrace(
            "Deferring to {name}",
            nameof(WebApplicationExtensions002.MigrateDatabaseAsync)
            );

        // Migrate the database (and create if it doesn't exist).
        await webApplication.MigrateDatabaseAsync(
            cancellationToken
            ).ConfigureAwait(false);
    }

    // *******************************************************************

    /// <summary>
    /// This method migrates the database.
    /// project.
    /// </summary>
    /// <param name="webApplication">The web application to use for the 
    /// operation.</param>
    /// <param name="cancellationToken">A cancellation token that is monitored
    /// for the life of the operation.</param>
    /// <returns>A task to perform the operation.</returns>
    private static async Task MigrateDatabaseAsync(
        this WebApplication webApplication,
        CancellationToken cancellationToken = default
        )
    {
        // Here we have to ensure that all data-contexts get migrated in
        //   turn, so the underlying database has a complete schema to 
        //   with.

        // Log what we are about to do.
        webApplication.Logger.LogDebug(
            "Creating a DI scope."
            );

        // Create a DI scope.
        using var scope = webApplication.Services.CreateScope();

        // Log what we are about to do.
        webApplication.Logger.LogDebug(
            "Creating a {ctx} instance.",
            nameof(AspNetDbContext)
            );

        // Create the green data-context.
        var greenDbContext = scope.ServiceProvider.GetRequiredService<
            AspNetDbContext
            >();

        // Log what we are about to do.
        webApplication.Logger.LogInformation(
            "Migrating the {ctx} on database '{db}', on server '{srv}'",
            nameof(AspNetDbContext),
            greenDbContext.Database.GetDatabaseName(),
            greenDbContext.Database.GetServerName()
            );

        // Migrate the data-context.
        await greenDbContext.Database.MigrateAsync()
            .ConfigureAwait(false);

        // Create the duende configurations data-context.
        var configurationDbContext = scope.ServiceProvider.GetRequiredService<
            ConfigurationDbContext
            >();

        // Log what we are about to do.
        webApplication.Logger.LogInformation(
            "Migrating the {ctx} on database '{db}', on server '{srv}'",
            nameof(ConfigurationDbContext),
            configurationDbContext.Database.GetDatabaseName(),
            configurationDbContext.Database.GetServerName()
            );

        // Migrate the data-context.
        await configurationDbContext.Database.MigrateAsync()
            .ConfigureAwait(false);

        // Create the duende persisted grant data-context.
        var persistedGrantDbContext = scope.ServiceProvider.GetRequiredService<
            PersistedGrantDbContext
            >();

        // Log what we are about to do.
        webApplication.Logger.LogInformation(
            "Migrating the {ctx} on database '{db}', on server '{srv}'",
            nameof(PersistedGrantDbContext),
            persistedGrantDbContext.Database.GetDatabaseName(),
            persistedGrantDbContext.Database.GetServerName()
            );

        // Migrate the data-context.
        await persistedGrantDbContext.Database.MigrateAsync()
            .ConfigureAwait(false);
    }

    #endregion
}
