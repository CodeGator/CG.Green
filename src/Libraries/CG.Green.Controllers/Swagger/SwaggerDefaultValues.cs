
namespace CG.Green.Swagger;

/// <summary>
/// This class is a default implementation of the <see cref="IOperationFilter"/>
/// interface.
/// </summary>
internal class SwaggerDefaultValues : IOperationFilter
{
    // *******************************************************************
    // Public methods.
    // *******************************************************************

    #region Public methods
    
    /// <summary>
    /// This method applies a filter to the specified operation using the 
    /// given context.
    /// </summary>
    /// <param name="operation">The operation to apply the filter to.</param>
    /// <param name="context">The current operation filter context.</param>
    public void Apply(
        OpenApiOperation operation, 
        OperationFilterContext context
        )
    {
        // Get the API description.
        var apiDescription = context.ApiDescription;

        // Is it deprecated?
        operation.Deprecated |= apiDescription.IsDeprecated();

        // Loop through the response types.
        foreach (var responseType in context.ApiDescription.SupportedResponseTypes)
        {
            // Get the response key.
            var responseKey = responseType.IsDefaultResponse 
                ? "default" 
                : responseType.StatusCode.ToString();

            // Get the response.
            var response = operation.Responses[responseKey];

            // Loop through the content types.
            foreach (var contentType in response.Content.Keys)
            {
                // Are we not using this?
                if (!responseType.ApiResponseFormats.Any(x => x.MediaType == contentType))
                {
                    // Remove the content type.
                    response.Content.Remove(contentType);
                }
            }
        }

        // Are the parameters missing?
        if (operation.Parameters == null)
        {
            return;
        }

        // Loop through the parameters.
        foreach (var parameter in operation.Parameters)
        {
            // Get the description for this parameter.
            var description = apiDescription.ParameterDescriptions.First(p => 
                p.Name == parameter.Name
                );

            // Try to find a good description.
            parameter.Description ??= description.ModelMetadata?.Description;

            // Is the default value missing, or empty?
            if (parameter.Schema.Default == null && description.DefaultValue != null)
            {
                // Create the default value.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                var json = JsonSerializer.Serialize(
                    description.DefaultValue, 
                    description.ModelMetadata.ModelType
                    );
#pragma warning restore CS8602 // Dereference of a possibly null reference.

                // Set the default value.
                parameter.Schema.Default = OpenApiAnyFactory.CreateFromJson(json);
            }

            // Is the parameter required?
            parameter.Required |= description.IsRequired;
        }
    }

    #endregion
}
