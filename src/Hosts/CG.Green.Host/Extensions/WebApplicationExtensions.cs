
namespace Microsoft.AspNetCore.Builder;

/// <summary>
/// This class contains extension methods related to the <see cref="WebApplication"/>
/// type.
/// </summary>
public static class WebApplicationExtensions015
{
    // *******************************************************************
    // Public methods.
    // *******************************************************************

    #region Public methods

    /// <summary>
    /// This method adds middleware required for localization support.
    /// </summary>
    /// <param name="webApplication">The web application to use for the 
    /// operation.</param>
    /// <returns>The value of the <paramref name="webApplication"/>
    /// parameter, for chaining calls together, Fluent style.</returns>
    public static WebApplication UseGreenLocalization(
        this WebApplication webApplication
        )
    {
        // Validate the parameters before attempting to use them.
        Guard.Instance().ThrowIfNull(webApplication, nameof(webApplication));

        // Log what we are about to do.
        webApplication.Logger.LogDebug(
            "Fetching the localization options."
            );

        // Get the culture options.
        var cultureOptions = webApplication.Services.GetRequiredService<IOptions<GreenLocalizationOptions>>();

        // Log what we are about to do.
        webApplication.Logger.LogDebug(
            "Adding localization middleware."
            );

        // Add the localization middleware.
        webApplication.UseRequestLocalization(
            new RequestLocalizationOptions()
                .AddSupportedCultures(cultureOptions.Value.Cultures.ToArray())
                .AddSupportedUICultures(cultureOptions.Value.Cultures.ToArray())
                .SetDefaultCulture(cultureOptions.Value.Default)
        );

		// Log what we are about to do.
		webApplication.Logger.LogDebug(
			"Adding health checks."
			);

		// Add health checks.
		webApplication.UseHealthChecks("/health");

		// Log what we are about to do.
		//webApplication.Logger.LogDebug(
	//		"Mapping health checks."
//			);

		// Map the health checks
		//webApplication.MapHealthChecks("/health")
		//	.RequireAuthorization();

		// Return the application.
		return webApplication;
    }

    #endregion
}
