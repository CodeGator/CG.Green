﻿
namespace Microsoft.AspNetCore.Builder;

/// <summary>
/// This class contains extension methods related to the <see cref="WebApplicationBuilder"/>
/// type.
/// </summary>
public static class WebApplicationBuilderExtensions001
{
    // *******************************************************************
    // Public methods.
    // *******************************************************************

    #region Public methods

    /// <summary>
    /// This method adds managers, directors, and related services, for 
    /// the <see cref="CG.Green"/> business logic layer.
    /// </summary>
    /// <param name="webApplicationBuilder">The web application builder to
    /// use for the operation.</param>
    /// <param name="sectionName">The configuration section to use for the 
    /// operation. Defaults to <c>BLL</c>.</param>
    /// <param name="bootstrapLogger">The bootstrap logger to use for the 
    /// operation.</param>
    /// <returns>The value of the <paramref name="webApplicationBuilder"/>
    /// parameter, for chaining calls together, Fluent style.</returns>
    /// <exception cref="ArgumentException">This exception is thrown whenever
    /// one or more arguments are missing, or invalid.</exception>
    public static WebApplicationBuilder AddGreenManagers(
        this WebApplicationBuilder webApplicationBuilder,
        string sectionName = "BLL",
        ILogger? bootstrapLogger = null
        )
    {
        // Validate the parameters before attempting to use them.
        Guard.Instance().ThrowIfNull(webApplicationBuilder, nameof(webApplicationBuilder));

        // Tell the world what we are about to do.
        bootstrapLogger?.LogDebug(
            "Configuring BLL options from the {section} section",
            sectionName
            );

        // Configure the BLL options.
        webApplicationBuilder.Services.ConfigureOptions<GreenBllOptions>(
            webApplicationBuilder.Configuration.GetSection(sectionName),
            out var bllOptions
            );

        // Tell the world what we are about to do.
        bootstrapLogger?.LogDebug(
            "Wiring up the Green managers"
            );

        // Add the managers.
        webApplicationBuilder.Services.AddScoped<IApiScopeManager, ApiScopeManager>();
        webApplicationBuilder.Services.AddScoped<IClientManager, ClientManager>();
        webApplicationBuilder.Services.AddScoped<IGreenRoleManager, GreenRoleManager>();
        webApplicationBuilder.Services.AddScoped<IGreenRoleClaimManager, GreenRoleClaimManager>();
        webApplicationBuilder.Services.AddScoped<IGreenUserManager, GreenUserManager>();
        webApplicationBuilder.Services.AddScoped<IGreenUserRoleManager, GreenUserRoleManager>();
        webApplicationBuilder.Services.AddScoped<IGreenUserClaimManager, GreenUserClaimManager>();
        webApplicationBuilder.Services.AddScoped<IIdentityResourceManager, IdentityResourceManager>();

        // Tell the world what we are about to do.
        bootstrapLogger?.LogDebug(
            "Wiring up the shared cryptographers"
            );

        // Add the shared cryptographers
        webApplicationBuilder.AddCryptographyWithSharedKeys(
            sectionName: sectionName,
            bootstrapLogger: bootstrapLogger
            );

        // Tell the world what we are about to do.
        bootstrapLogger?.LogDebug(
            "Wiring up the Green API"
            );

        // Add the API.
        webApplicationBuilder.Services.AddScoped<IGreenApi, GreenApi>();

        // Return the application builder.
        return webApplicationBuilder;
    }

    #endregion
}
