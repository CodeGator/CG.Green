
namespace Microsoft.AspNetCore.Builder;

/// <summary>
/// This class contains extension methods related to the <see cref="WebApplicationBuilder"/>
/// type.
/// </summary>
public static class WebApplicationBuilderExtensions002
{
    // *******************************************************************
    // Public methods.
    // *******************************************************************

    #region Public methods

    /// <summary>
    /// This method adds REST controllers for the <see cref="CG.Green"/> 
    /// service layer.
    /// </summary>
    /// <param name="webApplicationBuilder">The web application builder to
    /// use for the operation.</param>
    /// <param name="sectionName">The configuration section to use for the 
    /// operation. Defaults to <c>CTRL</c>.</param>
    /// <param name="bootstrapLogger">The bootstrap logger to use for the 
    /// operation.</param>
    /// <returns>The value of the <paramref name="webApplicationBuilder"/>
    /// parameter, for chaining calls together, Fluent style.</returns>
    /// <exception cref="ArgumentException">This exception is thrown whenever
    /// one or more arguments are missing, or invalid.</exception>
    public static WebApplicationBuilder AddGreenControllers(
        this WebApplicationBuilder webApplicationBuilder,
        string sectionName = "CTRL",
        ILogger? bootstrapLogger = null
        )
    {
        // Validate the parameters before attempting to use them.
        Guard.Instance().ThrowIfNull(webApplicationBuilder, nameof(webApplicationBuilder));

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

        // Return the application builder.
        return webApplicationBuilder;
    }

    #endregion
}