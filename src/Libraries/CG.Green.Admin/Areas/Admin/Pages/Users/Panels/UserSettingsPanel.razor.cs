
namespace CG.Green.Areas.Admin.Pages.Users.Panels;

/// <summary>
/// This class is the code-behind for the <see cref="UserSettingsPanel"/> component.
/// </summary>
public partial class UserSettingsPanel
{
	// *******************************************************************
	// Fields.
	// *******************************************************************

	#region Fields

	/// <summary>
	/// This field indicates the panel is loading data.
	/// </summary>
	internal protected bool _isLoading;

	#endregion

	// *******************************************************************
	// Properties.
	// *******************************************************************

	#region Properties

	/// <summary>
	/// This property contains the model for the component.
	/// </summary>
	[Parameter]
	public EditGreenUserVM Model { get; set; } = null!;

	/// <summary>
	/// This property contains the MudBlazor them for the component.
	/// </summary>
	[CascadingParameter(Name = "Theme")]
	public MudTheme Theme { get; set; } = null!;

	/// <summary>
	/// This property contains the snackbar service for the component.
	/// </summary>
	[Inject]
	protected ISnackbar Snackbar { get; set; } = null!;

	/// <summary>
	/// This property contains the clipboard service for the component.
	/// </summary>
	[Inject]
	protected ClipboardService Clipboard { get; set; } = null!;

	/// <summary>
	/// This property contains the dialog service for the component.
	/// </summary>
	[Inject]
	protected IDialogService Dialog { get; set; } = null!;

	/// <summary>
	/// This property contains the logger for the component.
	/// </summary>
	[Inject]
	protected ILogger<UserSettingsPanel> Logger { get; set; } = null!;

	#endregion

	// *******************************************************************
	// Protected methods.
	// *******************************************************************

	#region Protected methods
	/// <summary>
	/// This method copies the user id to the cipboard.
	/// </summary>
	protected async Task OnCopyUserId()
	{
		try
		{
			// Sanity check the model.
			if (Model is null)
			{
				return;
			}

			// Log what we are about to do.
			Logger.LogDebug(
				"Copying user id to the clipboard"
				);

			// Copy the value.
			await Clipboard.CopyToClipboard(Model.Id);

			// Log what we are about to do.
			Logger.LogDebug(
				"Showing the snackbar"
				);

			// Tell the world what we did.
			Snackbar.Add(
				"Id copied to clipboard"
				);
		}
		catch (Exception ex)
		{
			// Log what happened.
			Logger.LogError(
				ex.GetBaseException(),
				"Failed to copy the id!"
				);

			// Tell the world what happened.
			await Dialog.ShowErrorBox(
				exception: ex,
				title: "Something Broke!"
				);
		}
	}

	#endregion
}

