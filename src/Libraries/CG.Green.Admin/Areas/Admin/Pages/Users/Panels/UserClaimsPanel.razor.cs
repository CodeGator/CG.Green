
namespace CG.Green.Areas.Admin.Pages.Users.Panels;

/// <summary>
/// This class is the code-behind for the <see cref="UserClaimsPanel"/> component.
/// </summary>
public partial class UserClaimsPanel
{
	// *******************************************************************
	// Fields.
	// *******************************************************************

	#region Fields

	/// <summary>
	/// This field indicates whether or not the panel is busy.
	/// </summary>
	internal protected bool _isBusy;

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
	/// This property contains the dialog service for the component.
	/// </summary>
	[Inject]
	protected IDialogService Dialog { get; set; } = null!;

	/// <summary>
	/// This property contains the logger for the component.
	/// </summary>
	[Inject]
	protected ILogger<UserClaimsPanel> Logger { get; set; } = null!;

	#endregion

	// *******************************************************************
	// Protected methods.
	// *******************************************************************

	#region Protected methods

	/// <summary>
	/// This method creates a new claim for the user.
	/// </summary>
	/// <returns>A task to perform the operation.</returns>
	protected async Task OnCreateClaimAsync()
	{
		try
		{
			// Sanity check the model.
			if (Model is null)
			{
				return;
			}

			// Create the dialog options.
			var options = new DialogOptions
			{
				MaxWidth = MaxWidth.Small,
				CloseOnEscapeKey = true,
				FullWidth = true
			};

			// Log what we are about to do.
			Logger.LogDebug(
				"Creating dialog parameters."
				);

			// Create the dialog parameters.
			var parameters = new DialogParameters()
			{
				{ "Model", new EditUserClaimVM() }
			};

			// Log what we are about to do.
			Logger.LogDebug(
				"Creating new dialog."
				);

			// Create the dialog.
			var dialog = Dialog.Show<UserClaimDialog>(
				"Create Claim",
				parameters,
				options
				);

			// Get the results of the dialog.
			var result = await dialog.Result;

			// Did the user cancel?
			if (result.Canceled)
			{
				return;
			}

			// Log what we are about to do.
			Logger.LogDebug(
				"Recovering the dialog model."
				);

			// Recover the model.
			var model = (EditUserClaimVM)result.Data;

			// Log what we are about to do.
			Logger.LogDebug(
				"Adding the claim to the user."
			);

			// Add the claim.
			Model.Claims.Add(model);
		}
		catch (Exception ex)
		{
			// Log what happened.
			Logger.LogError(
				ex.GetBaseException(),
				"Failed to add a claim to the user!"
				);

			// Tell the world what happened.
			await Dialog.ShowErrorBox(
				exception: ex,
				title: "Something Broke!"
				);
		}
	}

	// *******************************************************************

	/// <summary>
	/// This method edits the given claim.
	/// </summary>
	/// <param name="claim">The claim to use for the operation.</param>
	/// <returns>A task to perform the operation.</returns>
	protected async Task OnEditClaimAsync(
		EditUserClaimVM claim
		)
	{
		try
		{
			// Sanity check the model.
			if (Model is null)
			{
				return;
			}

			// Create the dialog options.
			var options = new DialogOptions
			{
				MaxWidth = MaxWidth.Small,
				CloseOnEscapeKey = true,
				FullWidth = true
			};

			// Log what we are about to do.
			Logger.LogDebug(
				"Creating dialog parameters."
				);

			// Create the dialog parameters.
			var parameters = new DialogParameters()
			{
				{ "Model", claim }
			};

			// Log what we are about to do.
			Logger.LogDebug(
				"Creating new dialog."
				);

			// Create the dialog.
			var dialog = Dialog.Show<UserClaimDialog>(
				"Edit Claim",
				parameters,
				options
				);

			// Get the results of the dialog.
			var result = await dialog.Result;

			// Did the user cancel?
			if (result.Canceled)
			{
				return;
			}

			// Log what we are about to do.
			Logger.LogDebug(
				"Recovering the dialog model."
				);

			// Recover the model.
			var model = (EditUserClaimVM)result.Data;

			// Remove the original.
			Model.Claims.Remove(claim);

			// Add the modified.
			Model.Claims.Add(model);
		}
		catch (Exception ex)
		{
			// Log what happened.
			Logger.LogError(
				ex.GetBaseException(),
				"Failed to edit a claim!"
				);

			// Tell the world what happened.
			await Dialog.ShowErrorBox(
				exception: ex,
				title: "Something Broke!"
				);
		}
	}

	// *******************************************************************

	/// <summary>
	/// This method deletes the given claim from the user.
	/// </summary>
	/// <param name="claim">The claim to use for the operation.</param>
	/// <returns>A task to perform the operation.</returns>
	protected async Task OnDeleteClaimAsync(
		EditUserClaimVM claim
		)
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
				"Prompting the caller."
				);

			// Prompt the user.
			var result = await Dialog.ShowDeleteBox(
				claim.ClaimType ?? ""
				);

			// Did the user cancel?
			if (!result)
			{
				return; // Nothing more to do.
			}

			// Log what we are about to do.
			Logger.LogDebug(
				"Deleting a claim."
			);

			// Delete the claim
			Model.Claims.Remove(claim);
		}
		catch (Exception ex)
		{
			// Log what happened.
			Logger.LogError(
				ex.GetBaseException(),
				"Failed to delete a claim!"
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
