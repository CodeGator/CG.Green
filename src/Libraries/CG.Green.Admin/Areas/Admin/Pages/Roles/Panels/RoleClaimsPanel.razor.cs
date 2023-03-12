
namespace CG.Green.Areas.Admin.Pages.Roles.Panels;

// <summary>
/// This class is the code-behind for the <see cref="RoleClaimsPanel"/> page.
/// </summary>
public partial class RoleClaimsPanel
{
	// *******************************************************************
	// Fields.
	// *******************************************************************

	#region Fields

	/// <summary>
	/// This field indicates the panel is busy.
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
	public EditGreenRoleVM Model { get; set; } = null!;

	/// <summary>
	/// This property contains the MudBlazor them for the component.
	/// </summary>
	[CascadingParameter(Name = "Theme")]
	public MudTheme Theme { get; set; } = null!;

	/// <summary>
	/// This property contains the localizer for the component.
	/// </summary>
	[Inject]
	protected IStringLocalizer<RoleClaimsPanel> Localizer { get; set; } = null!;

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
	protected ILogger<RoleClaimsPanel> Logger { get; set; } = null!;

	#endregion

	// *******************************************************************
	// Protected methods.
	// *******************************************************************

	#region Protected methods

	/// <summary>
	/// This method creates a new role claim.
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
				{ "Model", new GreenRoleClaim() }
			};

			// Log what we are about to do.
			Logger.LogDebug(
				"Creating new dialog."
				);

			// Create the dialog.
			var dialog = Dialog.Show<RoleClaimDialog>(
				Localizer["CreateRoleClaim"],
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
			var model = (GreenRoleClaim)result.Data;

			// Log what we are about to do.
			Logger.LogDebug(
				"Adding the claim to the role."
			);

			// Add the claim.
			Model.Claims.Add(model);
		}
		catch (Exception ex)
		{
			// Log what happened.
			Logger.LogError(
				ex.GetBaseException(),
				"Failed to add a claim to the role!"
				);

			// Tell the world what happened.
			await Dialog.ShowErrorBox(
				exception: ex,
				title: Localizer["Broke"]
				);
		}
	}

	// *******************************************************************

	/// <summary>
	/// This method edits the given role claim.
	/// </summary>
	/// <param name="roleClaim">The role claim to use for the operation.</param>
	/// <returns>A task to perform the operation.</returns>
	protected async Task OnEditClaimAsync(
		GreenRoleClaim roleClaim
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
				{ "Model", roleClaim }
			};

			// Log what we are about to do.
			Logger.LogDebug(
				"Creating new dialog."
				);

			// Create the dialog.
			var dialog = Dialog.Show<RoleClaimDialog>(
				Localizer["EditRoleClaim"],
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
			var model = (GreenRoleClaim)result.Data;

			// Remove the original.
			Model.Claims.Remove(roleClaim);

			// Add the modified.
			Model.Claims.Add(model);
		}
		catch (Exception ex)
		{
			// Log what happened.
			Logger.LogError(
				ex.GetBaseException(),
				"Failed to edit a role claim!"
				);

			// Tell the world what happened.
			await Dialog.ShowErrorBox(
				exception: ex,
				title: Localizer["Broke"]
				);
		}
	}

	// *******************************************************************

	/// <summary>
	/// This method deletes the given role claim.
	/// </summary>
	/// <param name="roleClaim">The role claim to use for the operation.</param>
	/// <returns>A task to perform the operation.</returns>
	protected async Task OnDeleteClaimAsync(
		GreenRoleClaim roleClaim
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
				roleClaim.ClaimType ?? ""
				);

			// Did the user cancel?
			if (!result)
			{
				return; // Nothing more to do.
			}

			// Log what we are about to do.
			Logger.LogDebug(
				"Deleting a role claim."
			);

			// Delete the role claim
			Model.Claims.Remove(roleClaim);
		}
		catch (Exception ex)
		{
			// Log what happened.
			Logger.LogError(
				ex.GetBaseException(),
				"Failed to delete a role claim!"
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
