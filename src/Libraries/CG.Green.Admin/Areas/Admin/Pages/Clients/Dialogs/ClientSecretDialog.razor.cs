
namespace CG.Green.Areas.Admin.Pages.Clients.Dialogs;

/// <summary>
/// This class is the code-behind for the <see cref="ClientSecretDialog"/> dialog.
/// </summary>
public partial class ClientSecretDialog
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
    public ClientSecretVM Model { get; set; } = null!;

    /// <summary>
    /// This property indicates whether the called is editing the secret.
    /// </summary>
    [Parameter]
    public bool IsEditing { get; set; }

    /// <summary>
    /// This property contains the dialog service for this dialog.
    /// </summary>
    [Inject]
    protected IDialogService Dialog { get; set; } = null!;

    /// <summary>
    /// This property contains the logger for this dialog.
    /// </summary>
    [Inject]
    protected ILogger<ClientSecretDialog> Logger { get; set; } = null!;

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

    // *******************************************************************

    /// <summary>
    /// This method generates a new secret value.
    /// </summary>
    protected void OnNewSecretValue()
    {
        // Generate the secret value.
        Model.Value = $"{Guid.NewGuid():N}";
    }

    #endregion
}
