
namespace CG.Green.Areas.Admin.Pages.Clients.Dialogs;

/// <summary>
/// This class is the code-behind for the <see cref="UriDialog"/> dialog.
/// </summary>
public partial class UriDialog
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
    public EditUriVM Model { get; set; } = null!;

    /// <summary>
    /// This property contains the dialog service for this dialog.
    /// </summary>
    [Inject]
    protected IDialogService Dialog { get; set; } = null!;
        
    /// <summary>
    /// This property contains the logger for this dialog.
    /// </summary>
    [Inject]
    protected ILogger<UriDialog> Logger { get; set; } = null!;

    #endregion

    // *******************************************************************
    // Protected methods.
    // *******************************************************************

    #region Protected methods

    /// <summary>
    /// This method submits the dialog.
    /// </summary>
    protected void OnValidSubmit() => MudDialog.Close(DialogResult.Ok(Model));

    // *******************************************************************

    /// <summary>
    /// This method cancels the dialog.
    /// </summary>
    protected void Cancel() => MudDialog.Cancel();

    #endregion
}
