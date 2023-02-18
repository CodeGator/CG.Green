
namespace Microsoft.AspNetCore.Builder;

/// <summary>
/// This class contains extension methods related to the <see cref="WebApplicationBuilder"/>
/// type.
/// </summary>
public static class WebApplicationBuilderExtensions003
{
    // *******************************************************************
    // Public methods.
    // *******************************************************************

    #region Public methods

    /// <summary>
    /// This method adds required services for this in-memory provider.
    /// </summary>
    /// <param name="webApplicationBuilder">The web application builder to
    /// use for the operation.</param>
    /// <param name="sectionName">The configuration section to use for the 
    /// operation. Defaults to <c>DAL:InMemory</c>.</param>
    /// <param name="bootstrapLogger">A bootstrap logger to use for the
    /// operation.</param>
    /// <returns>The value of the <paramref name="webApplicationBuilder"/>
    /// parameter, for chaining calls together, Fluent style.</returns>
    /// <exception cref="ArgumentException">This exception is thrown whenever
    /// one or more arguments are missing, or invalid.</exception>
    /// <remarks>
    /// <para>
    /// This method must NOT have its signature changed! The method follows 
    /// a convention used by the <see cref="CG.EntityFrameworkCore"/> 
    /// package.
    /// </para>
    /// </remarks>
    public static WebApplicationBuilder AddInMemoryDataAccess(
        this WebApplicationBuilder webApplicationBuilder,
        string sectionName = "DAL:InMemory",
        ILogger? bootstrapLogger = null
        )
    {
        // Validate the parameters before attempting to use them.
        Guard.Instance().ThrowIfNull(webApplicationBuilder, nameof(webApplicationBuilder))
            .ThrowIfNullOrEmpty(sectionName, nameof(sectionName));

        // Tell the world what we are about to do.
        bootstrapLogger?.LogDebug(
            "Recovering the ASP.NET identity builder from cache, for the DAL",
            nameof(AspNetDbContext)
            );

        // Recover the identity builder.
        var identityBuilder = BuilderCache.Builders["AspNet"] as IdentityBuilder;

        // Did we fail?
        if (identityBuilder is null)
        {
            // Panic!!
            throw new InvalidOperationException(
                "Failed to locate the ASP.NET identity builder in the builder cache!"
                );
        }

        // Tell the world what we are about to do.
        bootstrapLogger?.LogDebug(
            "Recovering the Duende identity builder from cache, for the DAL",
            nameof(AspNetDbContext)
            );

        // Recover the Duende builder.
        var identityServerBuilder = BuilderCache.Builders["Duende"] as IIdentityServerBuilder;

        // Did we fail?
        if (identityServerBuilder is null)
        {
            // Panic!!
            throw new InvalidOperationException(
                "Failed to locate the Duende identity builder in the builder cache!"
                );
        }

        // Tell the world what we are about to do.
        bootstrapLogger?.LogDebug(
            "Fetching the database name from the configuration"
            );

        // Get the configuration section.
        var section = webApplicationBuilder.Configuration.GetSection(sectionName);

        // Get the database name.
        var databaseName = section["DatabaseName"];

        // Sanity check the connection string.
        if (string.IsNullOrEmpty(databaseName))
        {
            // Panic!!
            throw new ArgumentException(
                message: $"The database name at '{sectionName}:DatabaseName', " +
                "in the configuration, is required for migrations but is " +
                "currently missing, or empty!"
                );
        }

        // Tell the world what we are about to do.
        bootstrapLogger?.LogDebug(
            "Fetching the migration assembly name"
            );

        // Get the name of the migration assembly.
        var migrationAssemblyName = Assembly.GetExecutingAssembly().GetName().Name;

        // Tell the world what we are about to do.
        bootstrapLogger?.LogDebug(
            "Registering the {ctx} data context, for the DAL",
            nameof(AspNetDbContext)
            );

        // Add the AspNet data-context.
        webApplicationBuilder.Services.AddDbContext<AspNetDbContext>(options =>
        {
            // Use the in memory provider with our database name.
            options.UseInMemoryDatabase(databaseName);
        });

        // Tell the world what we are about to do.
        bootstrapLogger?.LogDebug(
            "Adding the {ctx} data context as the ASP.NET identity store, for the DAL",
            nameof(AspNetDbContext)
            );

        // Add the stores for ASP.NET identity.
        identityBuilder.AddEntityFrameworkStores<AspNetDbContext>();

        // Tell the world what we are about to do.
        bootstrapLogger?.LogDebug(
            "Adding the {ctx} data context as the Duende identity stores, for the DAL",
            nameof(AspNetDbContext)
            );

        // Add the Duende stores.
        identityServerBuilder.AddOperationalStore(options =>
        {
            // Use the in memory provider with our database name.
            options.ConfigureDbContext = b => b.UseInMemoryDatabase(databaseName);

            // Always add these options.
            options.EnableTokenCleanup = true;
            options.RemoveConsumedTokens = true;

        }).AddConfigurationStore(options =>
        {
            // Use the in memory provider with our database name.
            options.ConfigureDbContext = b => b.UseInMemoryDatabase(databaseName);
        });

        // Clear the builder cache.
        BuilderCache.Builders.Clear();

        // Return the application builder.
        return webApplicationBuilder;
    }

    #endregion
}
