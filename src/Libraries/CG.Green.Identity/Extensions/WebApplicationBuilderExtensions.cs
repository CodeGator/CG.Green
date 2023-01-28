
namespace Microsoft.AspNetCore.Builder;

/// <summary>
/// This class contains extension methods related to the <see cref="WebApplicationBuilder"/>
/// type.
/// </summary>
public static class WebApplicationBuilderExtensions007
{
    // *******************************************************************
    // Public methods.
    // *******************************************************************

    #region Public methods

    /// <summary>
    /// This method adds the ASP.NET and Duende identity services required 
    /// for the operation of the <see cref="CG.Green"/> microservice.
    /// </summary>
    /// <param name="webApplicationBuilder">The web application builder to
    /// use for the operation.</param>
    /// <param name="sectionName">The configuration section to use for the 
    /// operation. Defaults to <c>Identity</c>.</param>
    /// <param name="bootstrapLogger">The bootstrap logger to use for the 
    /// operation.</param>
    /// <returns>The value of the <paramref name="webApplicationBuilder"/>
    /// parameter, for chaining calls together, Fluent style.</returns>
    /// <exception cref="ArgumentException">This exception is thrown whenever
    /// one or more arguments are missing, or invalid.</exception>
    public static WebApplicationBuilder AddGreenIdentity(
        this WebApplicationBuilder webApplicationBuilder,
        string sectionName = "Identity",
        ILogger? bootstrapLogger = null
        )
    {
        // Validate the parameters before attempting to use them.
        Guard.Instance().ThrowIfNull(webApplicationBuilder, nameof(webApplicationBuilder));

        // Tell the world what we are about to do.
        bootstrapLogger?.LogDebug(
            "Configuring Identity options from the {section} section",
            sectionName
            );

        // Configure the identity options.
        webApplicationBuilder.Services.ConfigureOptions<GreenIdentityOptions>(
            webApplicationBuilder.Configuration.GetSection(sectionName),
            out var identityOptions
            );

        // Add the ASP.NET identity services.
        var identityBuilder = webApplicationBuilder.Services.AddDefaultIdentity<GreenUser>(options =>
        {
            // Configure the options.
            webApplicationBuilder.Configuration.GetSection(
                $"{sectionName}:AspNet"
                ).Bind(options);

            // Always set these values.
            options.SignIn.RequireConfirmedEmail = true;
        }).AddRoles<GreenRole>()
        .AddDefaultTokenProviders();

        // Tell the world what we are about to do.
        bootstrapLogger?.LogDebug(
            "Caching the ASP.NET identity builder"
            );

        // Add the builder to the cache
        BuilderCache.Builders["AspNet"] = identityBuilder;

        // Tell the world what we are about to do.
        bootstrapLogger?.LogDebug(
            "Adding the Duende identity services"
            );

        // Add the Duende identity services.
        var identityServerBuilder = webApplicationBuilder.Services.AddIdentityServer(options =>
        {
            // Configure the options.
            webApplicationBuilder.Configuration.GetSection(
                $"{sectionName}:Duende"
                ).Bind(options);

            // Always set these options.
            options.Events.RaiseErrorEvents = true;
            options.Events.RaiseInformationEvents = true;
            options.Events.RaiseFailureEvents = true;
            options.Events.RaiseSuccessEvents = true;
            options.EmitStaticAudienceClaim = true;
        }).AddAspNetIdentity<GreenUser>()
         .AddProfileService<ProfileService>();

        // Is this a development environment?
        if (webApplicationBuilder.Environment.IsDevelopment())
        {
            // Tell the world what we are about to do.
            bootstrapLogger?.LogDebug(
                "Adding the Duende development certificate"
                );

            // Add the developer cert.
            identityServerBuilder.AddDeveloperSigningCredential();
        }

        // Tell the world what we are about to do.
        bootstrapLogger?.LogDebug(
            "Caching the Duende identity builder"
            );

        // Add the builder to the cache
        BuilderCache.Builders["Duende"] = identityServerBuilder;

        // Tell the world what we are about to do.
        bootstrapLogger?.LogDebug(
            "Adding custom authorization policies for the identity layer"
            );

        // Add authorization policies.
        webApplicationBuilder.Services.AddAuthorization(options =>
        {
            // Add the 'standard policy' policy.
            options.AddPolicy(PolicyNameDefaults.StandardPolicy, policy =>
            {
                // This policy requires an authenticated user.
                policy.RequireAuthenticatedUser();
            });

            // Add the 'super admin' policy.
            options.AddPolicy(PolicyNameDefaults.SuperAdminPolicy, policy =>
            {
                // This policy requires these roles.
                policy.RequireRole(RoleNameDefaults.SuperAdmin);
            });

            // Add the 'admin' policy.
            options.AddPolicy(PolicyNameDefaults.AdminPolicy, policy =>
            {
                // This policy requires these roles.
                policy.RequireRole(
                    new[] { RoleNameDefaults.Admin, RoleNameDefaults.SuperAdmin }
                    );
            });
        });

        // Tell the world what we are about to do.
        bootstrapLogger?.LogDebug(
            "Adding a default CORS policy"
            );

        // Add a default CORS policy.
        webApplicationBuilder.Services.AddCors(options =>
            options.AddDefaultPolicy(policy =>
            {
                policy.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            }));

        // Tell the world what we are about to do.
        bootstrapLogger?.LogDebug(
            "Adding support services for the identity layer"
            );

        // Add support services.
        webApplicationBuilder.Services.AddScoped<TemplateProcessor>();
        webApplicationBuilder.Services.AddScoped<ClipboardService>();
        webApplicationBuilder.Services.AddTransient<IClaimsTransformation, ClaimsTransformation>();

        // Which email service should we register?
        switch (identityOptions.Email?.DefaultStrategy.ToLower().Trim())
        {
            case "smtp":
                webApplicationBuilder.Services.AddTransient<IEmailSender, SmtpEmailSender>();
                webApplicationBuilder.Services.AddTransient<SmtpClient>(services =>
                {
                    // Create the SMTP client.
                    var smtpClient = new System.Net.Mail.SmtpClient(
                        identityOptions.Email?.Smtp?.DefaultBaseAddress
                        );

                    // Were credentials specified?
                    if (!string.IsNullOrEmpty(identityOptions.Email?.Smtp?.UserName) &&
                        !string.IsNullOrEmpty(identityOptions.Email?.Smtp?.Password))
                    {
                        // Set the credentials for the client.
                        smtpClient.UseDefaultCredentials = false;
                        smtpClient.Credentials = new System.Net.NetworkCredential(
                           identityOptions.Email?.Smtp?.UserName,
                           identityOptions.Email?.Smtp?.Password
                           );
                    }

                    // Return the results.
                    return smtpClient;
                });
                break;

            case "purple":
                webApplicationBuilder.Services.AddTransient<IEmailSender, PurpleEmailSender>();
                webApplicationBuilder.AddPurpleClients(
                    options =>
                    {
                        options.DefaultBaseAddress =
                        identityOptions.Email?.Purple?.DefaultBaseAddress
                            ?? "https://localhost:7134";
                    },
                    bootstrapLogger
                    );
                break;
        }

        // Return the application builder.
        return webApplicationBuilder;
    }

    #endregion
}
