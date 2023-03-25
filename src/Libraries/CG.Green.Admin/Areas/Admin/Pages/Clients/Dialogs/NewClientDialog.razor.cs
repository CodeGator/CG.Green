
namespace CG.Green.Areas.Admin.Pages.Clients.Dialogs;

/// <summary>
/// This class is the code-behind for the <see cref="NewClientDialog"/> dialog.
/// </summary>
public partial class NewClientDialog
{
    // *******************************************************************
    // Properties.
    // *******************************************************************

    #region Properties

    /// <summary>
    /// This property contains the dialog reference.
    /// </summary>
    [CascadingParameter]
    public MudDialogInstance MudDialog { get; set; } = null!;

    /// <summary>
    /// This property contains the edit form's model.
    /// </summary>
    [Parameter]
    public NewClientVM Model { get; set; } = null!;

    /// <summary>
    /// This property contains the dialog service for this dialog.
    /// </summary>
    [Inject]
    protected IDialogService Dialog { get; set; } = null!;

    /// <summary>
    /// This property contains the green api for this dialog.
    /// </summary>
    [Inject]
    protected IGreenApi GreenApi { get; set; } = null!;

    /// <summary>
    /// This property contains the logger for this dialog.
    /// </summary>
    [Inject]
    protected ILogger<NewClientDialog> Logger { get; set; } = null!;

    #endregion

    // *******************************************************************
    // Protected methods.
    // *******************************************************************

    #region Protected methods

    /// <summary>
    /// This method submits the dialog.
    /// </summary>
    protected async Task OnValidSubmitAsync()
    {
        // Check for a conflicting client id.
        if (await GreenApi.Clients.AnyByIdAsync(Model.ClientId))
        {
            // Let the user know what happened.
            await Dialog.ShowMessageBox(
                title: "Something is wrong!",
                markupMessage: new MarkupString("The client identifier " +
                $"<b>{Model.ClientId}</b> has already been used. <br /><br />" +
                "Please use a different value for this client.")
                );
            return;
        }

        // Close the dialog.
        MudDialog.Close(DialogResult.Ok(Model));
    }

    // *******************************************************************

    /// <summary>
    /// This method cancels the dialog.
    /// </summary>
    protected void Cancel() => MudDialog.Cancel();

    #endregion

    // *******************************************************************
    // Protected methods.
    // *******************************************************************

    #region Protected methods

    /// <summary>
    /// This method generates a new random client id.
    /// </summary>
    protected void OnNewClientId()
    {
        // Set the random client id value.
        Model.ClientId = $"{Guid.NewGuid():N}";
    }

    #endregion
}
