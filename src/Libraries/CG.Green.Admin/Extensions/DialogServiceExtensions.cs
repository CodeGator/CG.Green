
using CG.Green.Areas.Admin.Dialogs;

namespace MudBlazor;

/// <summary>
/// This class contains extension methods related to the <see cref="IDialogService"/>
/// type.
/// </summary>
internal static class DialogServiceExtensions
{
    // *******************************************************************
    // Public methods.
    // *******************************************************************

    #region Public methods

    /// <summary>
    /// This method displays a delete confirmation dialog.
    /// </summary>
    /// <param name="dialogService">The dialog service to use for the operation.</param>
    /// <param name="objectName">The name of the object to be deleted.</param>
    /// <param name="confirm"><c>True</c> to require the caller to confirm 
    /// the operation by entering matching text to proceed.</param>
    /// <param name="title">The title for the confirmation box.</param>
    /// <param name="yesText">The text for the yes button.</param>
    /// <param name="noText">The text for the no button.</param>
    /// <returns>A task to perform the operation that returns <c>True</c>
    /// if the called press the Yes button, or <c>False</c> otherwise.</returns>
    public static async Task<bool> ShowDeleteBox(
        this IDialogService dialogService,
        string objectName,
        bool confirm = false,
        string title = "Confirm Delete",
        string yesText = "Delete",
        string noText = "No, don't delete"
        )
    {
        // Create the dialog.
        var dialog = dialogService.Show<DeleteDialog>(
            title: title,
            new DialogParameters()
            {
                { "ObjectName", objectName },
                { "YesText", yesText },
                { "NoText", noText },
                { "Confirm", confirm }
            },
            new DialogOptions()
            {
                CloseOnEscapeKey = true,
                MaxWidth = MaxWidth.ExtraSmall,
                FullWidth = true
            });

        // Show the dialog.
        var result = await dialog.Result;

        // Return the results.
        return !result.Canceled;
    }

    #endregion
}
