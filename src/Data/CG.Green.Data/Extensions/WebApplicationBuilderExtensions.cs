﻿
namespace Microsoft.AspNetCore.Builder;

/// <summary>
/// This class contains extension methods related to the <see cref="WebApplicationBuilder"/>
/// type.
/// </summary>
public static class WebApplicationBuilderExtensions006
{
    // *******************************************************************
    // Public methods.
    // *******************************************************************

    #region Public methods

    /// <summary>
    /// This method adds repositories and related services, for the <see cref="CG.Green"/>
    /// data-access layer.
    /// </summary>
    /// <param name="webApplicationBuilder">The web application builder to
    /// use for the operation.</param>
    /// <param name="sectionName">The configuration section to use for the 
    /// operation. Defaults to <c>DAL</c>.</param>
    /// <param name="bootstrapLogger">A bootstrap logger to use for the
    /// operation.</param>
    /// <returns>The value of the <paramref name="webApplicationBuilder"/>
    /// parameter, for chaining calls together, Fluent style.</returns>
    /// <exception cref="ArgumentException">This exception is thrown whenever
    /// one or more arguments are missing, or invalid.</exception>
    public static WebApplicationBuilder AddGreenRepositories(
        this WebApplicationBuilder webApplicationBuilder,
        string sectionName = "DAL",
        ILogger? bootstrapLogger = null
        )
    {
        // Validate the parameters before attempting to use them.
        Guard.Instance().ThrowIfNull(webApplicationBuilder, nameof(webApplicationBuilder))
            .ThrowIfNullOrEmpty(sectionName, nameof(sectionName));

        // Tell the world what we are about to do.
        bootstrapLogger?.LogDebug(
            "Wiring up the Green repositories for the DAL"
            );

        // Add the repositories
        webApplicationBuilder.Services.AddScoped<IApiScopeRepository, ApiScopeRepository>();
        webApplicationBuilder.Services.AddScoped<IClientRepository, ClientRepository>();
        webApplicationBuilder.Services.AddScoped<IGreenRoleClaimRepository, GreenRoleClaimRepository>();
        webApplicationBuilder.Services.AddScoped<IGreenUserClaimRepository, GreenUserClaimRepository>();
        webApplicationBuilder.Services.AddScoped<IGreenUserRoleRepository, GreenUserRoleRepository>();
        webApplicationBuilder.Services.AddScoped<IIdentityResourceRepository, IdentityResourceRepository>();

        // Tell the world what we are about to do.
        bootstrapLogger?.LogDebug(
            "Wiring up the auto-mapper for the DAL"
            );

        // Wire up the auto-mapper.
        webApplicationBuilder.Services.AddAutoMapper(cfg =>
        {
            // Wire up the conversion maps.
            //cfg.CreateMap<UserRole, UserRoleEntity>().ReverseMap();
        });

        // Return the application builder.
        return webApplicationBuilder;
    }

    #endregion
}
