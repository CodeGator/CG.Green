

namespace CG.Green.Areas.Admin.Pages.Clients.Panels;

/// <summary>
/// This class is the code-behind for the <see cref="PropertiesPanel"/> component.
/// </summary>
public partial class ClientPropertiesPanel
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
	protected ILogger<ClientPropertiesPanel> Logger { get; set; } = null!;

	#endregion

	// *******************************************************************
	// Protected methods.
	// *******************************************************************

	#region Protected methods

	/// <summary>
	/// This method creates a new property.
	/// </summary>
	/// <returns>A task to perform the operation.</returns>
	protected async Task OnCreatePropertyAsync()
	{
		// TODO : write the code for this.
	}

	// *******************************************************************

	/// <summary>
	/// This method edits the given property.
	/// </summary>
	/// <param name="property">The property to use for the operation.</param>
	/// <returns>A task to perform the operation.</returns>
	protected async Task OnEditPropertyAsync(
		EditPropertyVM property
		)
	{
		// TODO : write the code for this.
	}

	// *******************************************************************

	/// <summary>
	/// This method deletes the given property.
	/// </summary>
	/// <param name="property">The property to use for the operation.</param>
	/// <returns>A task to perform the operation.</returns>	
	protected async Task OnDeletePropertyAsync(
		EditPropertyVM property
		)
	{
		// TODO : write the code for this.
	}

	#endregion
}
