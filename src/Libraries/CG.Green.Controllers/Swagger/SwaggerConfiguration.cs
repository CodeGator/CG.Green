
namespace CG.Green.Plugins.Api.Swagger;

/// <summary>
/// This class is a default implementation of the <see cref="IConfigureOptions{SwaggerGenOptions}"/>
/// interface.
/// </summary>
internal class SwaggerConfiguration : IConfigureOptions<SwaggerGenOptions>
{
    // *******************************************************************
    // Fields.
    // *******************************************************************

    #region Fields

    /// <summary>
    /// This field contains the _provider for this configurator.
    /// </summary>
    internal protected readonly IApiVersionDescriptionProvider _provider;

	#endregion

	// *******************************************************************
	// Constructors.
	// *******************************************************************

	#region Constructors

	/// <summary>
	/// This constructor creates a new instance of the <see cref="SwaggerConfiguration"/>
	/// class.
	/// </summary>
	/// <param name="provider">The <see cref="IApiVersionDescriptionProvider"> 
	/// _provider</see> to use with this configurator.</param>
	public SwaggerConfiguration(
		IApiVersionDescriptionProvider provider
		)
	{
		// Validate the parameters before attempting to use them.
		Guard.Instance().ThrowIfNull(provider, nameof(provider));

		// Save the reference(s).
		_provider = provider;
	}

    #endregion

    // *******************************************************************
    // Public methods.
    // *******************************************************************

    #region Public methods

    /// <inheritdoc />
    public void Configure(
        SwaggerGenOptions options
        )
    {
		// Validate the parameters before attempting to use them.
		Guard.Instance().ThrowIfNull(options, nameof(options));

        // Loop through the discovered API versions
        foreach (var description in _provider.ApiVersionDescriptions)
        {
            // Add a document this version.
            options.SwaggerDoc(
                description.GroupName, 
                CreateInfoForApiVersion(description)
                );
        }
    }

    #endregion

    // *******************************************************************
    // Private methods.
    // *******************************************************************

    #region Private methods

    /// <summary>
    /// This method creates a Swagger document for the given API version.
    /// </summary>
    /// <param name="description">The swagger description to use for the 
    /// operation.</param>
    /// <returns>An <see cref="OpenApiInfo"/> instance.</returns>
    private static OpenApiInfo CreateInfoForApiVersion(
        ApiVersionDescription description
        )
    {
        // Create the purpose of the API.
        var text = new StringBuilder(
            "An API for the CG.Green microservice."
            );

        // Create the info link.
        var info = new OpenApiInfo()
        {
            Title = "CG.Green API",
            Version = description.ApiVersion.ToString(),
            Contact = new OpenApiContact() 
            { 
                Name = "CodeGator", 
                Email = "info@codegator.com" 
            },
            License = new OpenApiLicense() 
            { 
                Name = "MIT", 
                Url = new Uri("https://opensource.org/licenses/MIT")
            }
        };

        // Is this version deprecated?
        if (description.IsDeprecated)
        {
            text.Append(" This API version has been deprecated.");
        }

        // Is this version sunsetting?
        if (description.SunsetPolicy is SunsetPolicy policy)
        {
            // Is there a date?
            if (policy.Date is DateTimeOffset when)
            {
                text.Append(" The API will be sunset on ")
                    .Append(when.Date.ToShortDateString())
                    .Append('.');
            }

            // Are there links?
            if (policy.HasLinks)
            {
                // Append a line.
                text.AppendLine();

                // Loop through the links.
                for (var i = 0; i < policy.Links.Count; i++)
                {
                    // Create the link.
                    var link = policy.Links[i];

                    // IS the link HTML?
                    if (link.Type == "text/html")
                    {
                        // Append a line.
                        text.AppendLine();

                        // Is there a title?
                        if (link.Title.HasValue)
                        {
                            // Append the title.
                            text.Append(
                                link.Title.Value
                                ).Append(": ");
                        }

                        // Append the line.
                        text.Append(
                            link.LinkTarget.OriginalString
                            );
                    }
                }
            }
        }

        // Add the description.
        info.Description = text.ToString();

        // Return the results.
        return info;
    }

    #endregion
}
