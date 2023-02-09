
namespace CG.Green.Plugins.Api;

/// <summary>
/// This class is the main entry point for the plugin.
/// </summary>
internal class Module : ModuleBase
{
    // *******************************************************************
    // Public methods.
    // *******************************************************************

    #region Public methods

    /// <summary>
    /// This method configures any required services for the plugin.
    /// </summary>
    /// <param name="webApplicationBuilder">The web application builder to 
    /// use for the operation.</param>
    /// <param name="configuration">The plugin configuration to use for the 
    /// operation.</param>
    /// <param name="bootstrapLogger">The bootstrap logger to use for the 
    /// operation.</param>
    public override void ConfigureServices(
        WebApplicationBuilder webApplicationBuilder,
        IConfiguration configuration,
        ILogger? bootstrapLogger
        )
    {
        // Validate the parameters before attempting to use them.
        Guard.Instance().ThrowIfNull(webApplicationBuilder, nameof(webApplicationBuilder))
            .ThrowIfNull(configuration, nameof(configuration));

        // Tell the world what we are about to do.
        bootstrapLogger?.LogDebug(
            "Adding the Green REST controllers"
            );

        // Add this controller assembly an application part.
        webApplicationBuilder.Services.AddControllers()
            .AddApplicationPart(Assembly.GetExecutingAssembly())
            .AddJsonOptions(options =>
            {
                // Use camel case properties in JSON.
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                options.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;

                // Preserve references in JSON.
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
            });

        // Tell the world what we are about to do.
        bootstrapLogger?.LogDebug(
            "Adding support for problem details for the controllers"
            );

        // Add problem details.
        webApplicationBuilder.Services.AddProblemDetails();

        // Tell the world what we are about to do.
        bootstrapLogger?.LogDebug(
            "Adding support for API versioning for the controllers"
            );

        // Add API versioning.
        webApplicationBuilder.Services.AddApiVersioning(options =>
        {
            // Don't require a version.
            options.AssumeDefaultVersionWhenUnspecified = true;

            // Tell the world about our versions.
            options.ReportApiVersions = true;
        }).AddMvc()
        .AddApiExplorer();

        // Tell the world what we are about to do.
        bootstrapLogger?.LogDebug(
            "Adding support for Swagger for the controllers"
            );

        // Add Swagger.
        webApplicationBuilder.Services.AddSwaggerGen(options =>
        {
            // Use these default values.
            options.OperationFilter<SwaggerDefaultValues>();

            // Use our XML comments.
            options.IncludeXmlComments(
                Path.Combine(
                    AppContext.BaseDirectory,
                    $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"
                    ));
        });

        // Tell the world what we are about to do.
        bootstrapLogger?.LogDebug(
            "Adding Swagger configuration for the controllers"
            );

        // Add the swagger configuration
        webApplicationBuilder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, SwaggerConfiguration>();

        // Tell the world what we are about to do.
        bootstrapLogger?.LogDebug(
            "Wiring up the auto-mapper for the controllers"
            );

        // Wire up the auto-mapper.
        webApplicationBuilder.Services.AddAutoMapper(cfg =>
        {
            // Wire up the conversion maps.
            //cfg.CreateMap<BlobModel, Blob>().ReverseMap();
        });
    }

    // *******************************************************************

    /// <summary>
    /// This method wires up any startup or pipeline logic for the plugin.
    /// </summary>
    /// <param name="webApplication">The web application to use for the operation.</param>
    public override void Configure(
        WebApplication webApplication
        )
    {
        // Validate the parameters before attempting to use them.
        Guard.Instance().ThrowIfNull(webApplication, nameof(webApplication));

        // Log what we are about to do.
        webApplication.Logger.LogDebug(
            "Mapping Green controllers"
            );

        // Wire up controller mapping.
        webApplication.MapControllers();

        // Is this as development environment?
        if (webApplication.Environment.IsDevelopment())
        {
            // Log what we are about to do.
            webApplication.Logger.LogDebug(
                "Adding Swagger middleware"
                );

            // Enable Swagger
            webApplication.UseSwagger();

            // Log what we are about to do.
            webApplication.Logger.LogDebug(
                "Adding Swagger UI middleware"
                );

            // Enable Swagger UI.
            webApplication.UseSwaggerUI(options =>
            {
                // Log what we are about to do.
                webApplication.Logger.LogDebug(
                    "Fetching API versions"
                    );

                // Get the API version descriptions.
                var descriptions = webApplication.DescribeApiVersions();

                // Log what we are about to do.
                webApplication.Logger.LogDebug(
                    "Adding endpoints for {count} API versions",
                    descriptions.Count()
                    );

                // build a swagger endpoint for each discovered API version
                foreach (var description in descriptions)
                {
                    // Log what we are about to do.
                    webApplication.Logger.LogDebug(
                        "Adding endpoint API version {desc}",
                        description
                        );

                    // Add the endpoint for this API version.
                    var url = $"/swagger/{description.GroupName}/swagger.json";
                    var name = description.GroupName.ToUpperInvariant();
                    options.SwaggerEndpoint(url, name);
                }
            });
        }
    }

    #endregion
}
