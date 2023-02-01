
namespace CG.Green.Host.Pages.Admin.Roles;

/// <summary>
/// This class is the code-behind for the <see cref="NewRoleDialog"/> page.
/// </summary>
public partial class NewRoleDialog
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
    /// This property contains the existing roles.
    /// </summary>
    [Parameter]
    public IEnumerable<EditRoleVM> Roles { get; set; } = null!;

    /// <summary>
    /// This property contains the edit form's model.
    /// </summary>
    [Parameter]
    public EditRoleVM Model { get; set; } = null!;

    /// <summary>
    /// This property contains the dialog service for this page.
    /// </summary>
    [Inject]
    protected IDialogService DialogService { get; set; } = null!;

    /// <summary>
    /// This property contains the snackbar service for this page.
    /// </summary>
    [Inject]
    protected ISnackbar SnackbarService { get; set; } = null!;

    /// <summary>
    /// This property contains the logger for this page.
    /// </summary>
    [Inject]
    protected ILogger<NewRoleDialog> Logger { get; set; } = null!;

    #endregion

    // *******************************************************************
    // Protected methods.
    // *******************************************************************

    #region Protected methods

    /// <summary>
    /// This method submits the dialog.
    /// </summary>
    protected void OnValidSubmit(EditContext editContext)
    {
        // Sanity check the new role name.
        if (Roles.Any(x => x.Name == Model.Name))
        {
            var messageStore = new ValidationMessageStore(editContext);
            messageStore.Add(editContext.Field("Name"), $"Role: '{Model.Name}' is already used.");
            editContext.NotifyValidationStateChanged();
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
}

