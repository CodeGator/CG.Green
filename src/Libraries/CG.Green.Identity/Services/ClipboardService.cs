
namespace CG.Green.Services;

// I found this here: https://code-maze.com/copy-to-clipboard-in-blazor-webassembly/

/// <summary>
/// This class performs clipboard operations.
/// </summary>
public class ClipboardService
{
    // *******************************************************************
    // Fields.
    // *******************************************************************

    #region Fields

    /// <summary>
    /// This field contains the JS runtime for the service.
    /// </summary>
    internal protected readonly IJSRuntime _jsInterop;

    #endregion

    // *******************************************************************
    // Constructors.
    // *******************************************************************

    #region Constructors

    /// <summary>
    /// This constructor creates a new instance of the <see cref="ClipboardService"/>
    /// class.
    /// </summary>
    /// <param name="jsInterop">The JS runtime to use with this service.</param>
    public ClipboardService(
        IJSRuntime jsInterop
        )
    {
        // Validate the arguments before attempting to use them.
        Guard.Instance().ThrowIfNull(jsInterop, nameof(jsInterop));

        // Save the reference(s).
        _jsInterop = jsInterop;
    }

    #endregion

    // *******************************************************************
    // Public methods.
    // *******************************************************************

    #region Public methods

    /// <summary>
    /// This method copies the given text to the clipboard.
    /// </summary>
    /// <param name="text">The text to use for the operation.</param>
    /// <returns>A task to perform the operation.</returns>
    public async Task CopyToClipboard(string text)
    {
        // Call the JS function.
        await _jsInterop.InvokeVoidAsync(
            "navigator.clipboard.writeText",
            text
            );
    }

    #endregion
}
