
namespace CG.Green.Areas.Admin.Pages.Clients.Panels;

/// <summary>
/// This class is the code-behind for the <see cref="ClientIdentityProviderPanel"/> component.
/// </summary>
public partial class ClientIdentityProviderPanel
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
	protected ILogger<ClientIdentityProviderPanel> Logger { get; set; } = null!;

	#endregion

	// *******************************************************************
	// Protected methods.
	// *******************************************************************

	#region Protected methods

	/// <summary>
	/// This method creates a new identity provider restriction.
	/// </summary>
	/// <returns>A task to perform the operation.</returns>
	protected async Task OnCreateRestrictionAsync()
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
				{ "Model", new EditProviderVM() }
			};

			// Log what we are about to do.
			Logger.LogDebug(
				"Creating new dialog."
				);

			// Create the dialog.
			var dialog = Dialog.Show<ProviderDialog>(
				"Create Restriction",
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
			var model = (EditProviderVM)result.Data;

			// Log what we are about to do.
			Logger.LogDebug(
				"Adding the identity provider restriction to the client."
			);

			// Add the provider.
			Model.IdentityProviderRestrictions.Add(model);
		}
		catch (Exception ex)
		{
			// Log what happened.
			Logger.LogError(
				ex.GetBaseException(),
				"Failed to add an identity provider restriction!"
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
	/// This method edits an identity provider restriction.
	/// </summary>
	/// <param name="provider">The identity provider to use for the operation.</param>
	/// <returns>A task to perform the operation.</returns>
	protected async Task OnEditRestrictionAsync(
		EditProviderVM provider
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
				{ "Model", provider }
			};

			// Log what we are about to do.
			Logger.LogDebug(
				"Creating new dialog."
				);

			// Create the dialog.
			var dialog = Dialog.Show<ProviderDialog>(
				"Edit Restriction",
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
			var model = (EditProviderVM)result.Data;

			// Remove the original.
			Model.IdentityProviderRestrictions.Remove(provider);

			// Add the modified.
			Model.IdentityProviderRestrictions.Add(model);
		}
		catch (Exception ex)
		{
			// Log what happened.
			Logger.LogError(
				ex.GetBaseException(),
				"Failed to edit an identity provider restriction!"
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
	/// This method deletes an identity provider restriction.
	/// </summary>
	/// <param name="provider">The identity provider to use for the operation.</param>
	/// <returns>A task to perform the operation.</returns>
	protected async Task OnDeleteRestrictionAsync(
		EditProviderVM provider
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
				provider.Name
				);

			// Did the user cancel?
			if (!result)
			{
				return; // Nothing more to do.
			}

			// Log what we are about to do.
			Logger.LogDebug(
				"Deleting an identity provider restriction."
			);

			// Delete the restriction
			Model.IdentityProviderRestrictions.Remove(provider);
		}
		catch (Exception ex)
		{
			// Log what happened.
			Logger.LogError(
				ex.GetBaseException(),
				"Failed to delete an identity provider restriction!"
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
