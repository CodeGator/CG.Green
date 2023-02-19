
namespace CG.Green.Areas.Admin.Dialogs;

/// <summary>
/// This class is the code-behind for the <see cref="DeleteDialog"/> dialog.
/// </summary>
public partial class DeleteDialog
{
    // *******************************************************************
    // Fields.
    // *******************************************************************

    #region Fields

    /// <summary>
    /// This field contains the confirmation string.
    /// </summary>
    [Required] 
    internal protected string _model = "";

    #endregion

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
    public string ObjectName { get; set; } = null!;

    /// <summary>
    /// This property contains the text for the ok button.
    /// </summary>
    [Parameter]
    public string YesText { get; set; } = null!;

    /// <summary>
    /// This property contains the text for the cancel button.
    /// </summary>
    [Parameter]
    public string NoText { get; set; } = null!;

    /// <summary>
    /// This property indicates the caller should be forced to 
    /// confirm the operation by entering matching text.
    /// </summary>
    [Parameter]
    public bool Confirm { get; set; }

    /// <summary>
    /// This property contains the dialog service for this dialog.
    /// </summary>
    [Inject]
    protected IDialogService Dialog { get; set; } = null!;

    /// <summary>
    /// This property contains the localizer for this dialog.
    /// </summary>
    [Inject]
    protected IStringLocalizer<DeleteDialog> Localizer { get; set; } = null!;

    /// <summary>
    /// This property contains the logger for this dialog.
    /// </summary>
    [Inject]
    protected ILogger<DeleteDialog> Logger { get; set; } = null!;

    #endregion

    // *******************************************************************
    // Protected methods.
    // *******************************************************************

    #region Protected methods

    /// <summary>
    /// This method submits the dialog.
    /// </summary>
    protected void OnValidSubmit(
        EditContext editContext
        )
    {
        // Close the dialog.
        MudDialog.Close();
    }

    // *******************************************************************

    /// <summary>
    /// This method cancels the dialog.
    /// </summary>
    protected void Cancel() => MudDialog.Cancel();

    #endregion
}
