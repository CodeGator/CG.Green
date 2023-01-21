
namespace Microsoft.AspNetCore.Builder;

/// <summary>
/// This class contains extension methods related to the <see cref="WebApplication"/>
/// type.
/// </summary>
public static class WebApplicationExtensions001
{
    // *******************************************************************
    // Public methods.
    // *******************************************************************

    #region Public methods

    /// <summary>
    /// This method registers any middleware or startup code required for
    /// the <see cref="CG.Green"/> service layer.
    /// </summary>
    /// <param name="webApplication">The web application builder to 
    /// use for the operation.</param>
    /// <returns>The value of the <paramref name="webApplication"/>
    /// parameter, for chaining calls together, Fluent style</returns>
    public static WebApplication UseGreenControllers(
        this WebApplication webApplication
        )
    {
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

        // Return the application.
        return webApplication;
    }

    #endregion
}
