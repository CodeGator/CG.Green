
namespace CG.Green.Areas.Admin.Pages.Clients.Panels;

/// <summary>
/// This class is the code-behind for the <see cref="ClientAdvancedPanel"/> component.
/// </summary>
public partial class ClientAdvancedPanel
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
	public EditClientVM Model { get; set; } = null!;

	/// <summary>
	/// This property contains the logger for the component.
	/// </summary>
	[Inject]
	protected ILogger<ClientAdvancedPanel> Logger { get; set; } = null!;

	#endregion
}
