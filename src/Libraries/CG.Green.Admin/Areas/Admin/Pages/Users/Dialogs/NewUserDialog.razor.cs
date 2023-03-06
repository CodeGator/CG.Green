
namespace CG.Green.Areas.Admin.Pages.Users.Dialogs;

// <summary>
/// This class is the code-behind for the <see cref="NewUserDialog"/> dialog.
/// </summary>
public partial class NewUserDialog
{
	// *******************************************************************
	// Fields.
	// *******************************************************************

	#region Fields

	/// <summary>
	/// This field indicates the type of the password control.
	/// </summary>
	private InputType _passwordInput = InputType.Password;

	/// <summary>
	/// This field contains the icon for the password control.
	/// </summary>
	private string _passwordInputIcon = Icons.Material.Filled.VisibilityOff;

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
	public NewUserVM Model { get; set; } = null!;
		
	/// <summary>
	/// This property contains the localizer for this dialog.
	/// </summary>
	[Inject]
	protected IStringLocalizer<NewUserDialog> Localizer { get; set; } = null!;

	/// <summary>
	/// This property contains the logger for this dialog.
	/// </summary>
	[Inject]
	protected ILogger<NewUserDialog> Logger { get; set; } = null!;

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
		// Close the dialog.
		MudDialog.Close(DialogResult.Ok(Model));
	}

	// *******************************************************************

	/// <summary>
	/// This method cancels the dialog.
	/// </summary>
	protected void Cancel() => MudDialog.Cancel();

	// *******************************************************************

	/// <summary>
	/// This method toggles the password control between secure and not secure.
	/// </summary>
	protected void TogglePasswordVisibility()
	{
		if (_passwordInput != InputType.Password)
		{
			_passwordInputIcon = Icons.Material.Filled.VisibilityOff;
			_passwordInput = InputType.Password;
		}
		else
		{
			_passwordInputIcon = Icons.Material.Filled.Visibility;
			_passwordInput = InputType.Text;
		}
	}

	#endregion
}
