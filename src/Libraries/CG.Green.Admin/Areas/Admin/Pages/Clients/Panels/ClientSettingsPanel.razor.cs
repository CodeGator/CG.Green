
namespace CG.Green.Areas.Admin.Pages.Clients.Panels;

/// <summary>
/// This class is the code-behind for the <see cref="ClientSettingsPanel"/> component.
/// </summary>
public partial class ClientSettingsPanel
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
	public EditClientVM Model { get; set; } = null!;

	/// <summary>
	/// This property contains the MudBlazor them for the component.
	/// </summary>
	[CascadingParameter(Name = "Theme")]
	public MudTheme Theme { get; set; } = null!;

	/// <summary>
	/// This property contains the localizer for the component.
	/// </summary>
	[Inject]
	protected IStringLocalizer<ClientSettingsPanel> Localizer { get; set; } = null!;

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
	protected ILogger<ClientSettingsPanel> Logger { get; set; } = null!;

	#endregion

	// *******************************************************************
	// Protected methods.
	// *******************************************************************

	#region Protected methods
	/// <summary>
	/// This method copies the client id to the cipboard.
	/// </summary>
	protected async Task OnCopyClientId()
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
				"Copying client id to the clipboard"
				);

			// Copy the value.
			await Clipboard.CopyToClipboard(Model.ClientId);

			// Log what we are about to do.
			Logger.LogDebug(
				"Showing the snackbar"
				);

			// Tell the world what we did.
			Snackbar.Add(
				Localizer["CopyClipboard"]
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
				title: Localizer["Broke"]
				);
		}
	}

	#endregion
}
