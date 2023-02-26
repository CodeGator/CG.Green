
namespace Microsoft.AspNetCore.Builder;

/// <summary>
/// This class contains extension methods related to the <see cref="WebApplicationBuilder"/>
/// type.
/// </summary>
public static class WebApplicationBuilderExtensions015
{
    // *******************************************************************
    // Public methods.
    // *******************************************************************

    #region Public methods

    /// <summary>
    /// This method localization support for the <see cref="CG.Green"/> microservice.
    /// </summary>
    /// <param name="webApplicationBuilder">The web application builder to
    /// use for the operation.</param>
    /// <param name="sectionName">The configuration section to use for the 
    /// operation. Defaults to <c>Localization</c>.</param>
    /// <param name="bootstrapLogger">The bootstrap logger to use for the 
    /// operation.</param>
    /// <returns>The value of the <paramref name="webApplicationBuilder"/>
    /// parameter, for chaining calls together, Fluent style.</returns>
    /// <exception cref="ArgumentException">This exception is thrown whenever
    /// one or more arguments are missing, or invalid.</exception>
    public static WebApplicationBuilder AddGreenLocalization(
        this WebApplicationBuilder webApplicationBuilder,
        string sectionName = "Localization",
        ILogger? bootstrapLogger = null
        )
    {
        // Validate the parameters before attempting to use them.
        Guard.Instance().ThrowIfNull(webApplicationBuilder, nameof(webApplicationBuilder));

        // Tell the world what we are about to do.
        bootstrapLogger?.LogDebug(
            "Configuring localization options from the {section} section",
            sectionName
            );

        // Configure the culture options.
        webApplicationBuilder.Services.ConfigureOptions<GreenLocalizationOptions>(
            webApplicationBuilder.Configuration.GetSection(sectionName),
            out var greenLocalizationOptions
            );

		// Tell the world what we are about to do.
		bootstrapLogger?.LogDebug(
			"Adding localization services"
			);

		// Add localization support.
		webApplicationBuilder.Services.AddLocalization();

		// Tell the world what we are about to do.
		bootstrapLogger?.LogDebug(
			"Configuring the health checks"
			);

		// Add health checks.
		webApplicationBuilder.Services.AddHealthChecks();

		// Return the application builder.
		return webApplicationBuilder;
    }

    #endregion
}
