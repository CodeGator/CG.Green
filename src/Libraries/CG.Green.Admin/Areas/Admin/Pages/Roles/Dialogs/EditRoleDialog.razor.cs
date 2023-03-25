﻿
namespace CG.Green.Areas.Admin.Pages.Roles.Dialogs;

// <summary>
/// This class is the code-behind for the <see cref="EditRoleDialog"/> dialog.
/// </summary>
public partial class EditRoleDialog
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
	public EditGreenRoleVM Model { get; set; } = null!;

	/// <summary>
	/// This property contains the logger for this dialog.
	/// </summary>
	[Inject]
	protected ILogger<EditRoleDialog> Logger { get; set; } = null!;

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

	#endregion
}
