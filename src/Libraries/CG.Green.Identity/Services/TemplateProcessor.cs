
using CG.Validations;

namespace CG.Templates;

/// <summary>
/// This class performs template processing.
/// </summary>
public class TemplateProcessor
{
    // *******************************************************************
    // Public methods.
    // *******************************************************************

    #region Public methods

    /// <summary>
    /// This method processes the given file, replacing any matching tokens 
    /// from the given dictionary to produce a populated template.
    /// </summary>
    /// <param name="templatePath">The path of the incoming template file.</param>
    /// <param name="tokens">The list of named replacement tokens.</param>
    /// <param name="startDelimiter">The starting symbol(s) used to detect
    /// the start of a replacement token in the template.</param>
    /// <param name="endDelimiter">The ending symbol(s) used to detect
    /// the start of a replacement token in the template.</param>
    /// <param name="cancellationToken">A cancellation token that is monitored
    /// throughout the lifetime of the method.</param>
    /// <returns>A task to perform the operation that returns the processed
    /// template text.</returns>
    public virtual async Task<string> ProcessTemplateFromFileAsync(
        string templatePath,
        Dictionary<string, string> tokens,
        string startDelimiter = "{{",
        string endDelimiter = "}}",
        CancellationToken cancellationToken = default
        )
    {
        // Validate the arguments before attempting to use them.
        Guard.Instance().ThrowIfNullOrEmpty(templatePath, nameof(templatePath))
            .ThrowIfNullOrEmpty(startDelimiter, nameof(startDelimiter))
            .ThrowIfNullOrEmpty(endDelimiter, nameof(endDelimiter))
            .ThrowIfNull(tokens, nameof(tokens));

        // Read the template file.
        var rawText = await File.ReadAllTextAsync(
            templatePath,
            cancellationToken
            ).ConfigureAwait(false);

        // Call the overload.
        var populatedTemplate = ProcessTemplate(
            rawText,
            tokens,
            startDelimiter,
            endDelimiter
            );

        // Return the results.
        return populatedTemplate;
    }

    // *******************************************************************

    /// <summary>
    /// This method processes the given text, replacing any matching tokens 
    /// from the given dictionary to produce a populated template.
    /// </summary>
    /// <param name="template">The incoming template text.</param>
    /// <param name="tokens">The list of named replacement tokens.</param>
    /// <param name="startDelimiter">The starting symbol(s) used to detect
    /// the start of a replacement token in the template.</param>
    /// <param name="endDelimiter">The ending symbol(s) used to detect
    /// the start of a replacement token in the template.</param>
    /// <returns>The populate template string.</returns>
    public virtual string ProcessTemplate(
        string template,
        Dictionary<string, string> tokens,
        string startDelimiter = "{{",
        string endDelimiter = "}}"
        )
    {
        // Validate the arguments before attempting to use them.
        Guard.Instance().ThrowIfNullOrEmpty(template, nameof(template))
            .ThrowIfNullOrEmpty(startDelimiter, nameof(startDelimiter))
            .ThrowIfNullOrEmpty(endDelimiter, nameof(endDelimiter))
            .ThrowIfNull(tokens, nameof(tokens));

        // Loop through any tokens.
        var sb = new StringBuilder(template);
        foreach (var kvp in tokens)
        {
            // Replace the token with the value.
            sb.Replace(
                startDelimiter + kvp.Key + endDelimiter,
                kvp.Value
                );
        }

        // Get the populated template.
        var populatedTemplate = sb.ToString();

        // Return the results.
        return populatedTemplate;   
    }

    #endregion
}
