namespace CG.Green.Host.Shared;

/// <summary>
/// This class is the code-behind for the <see cref="MainLayout"/> page.
/// </summary>
public partial class MainLayout
{
	// *******************************************************************
	// Fields.
	// *******************************************************************

	#region Fields

	/// <summary>
	/// This field indicates when the drawer is open.
	/// </summary>
	internal protected bool _drawerOpen;
		
	#endregion

	// *******************************************************************
	// Properties.
	// *******************************************************************

	#region Properties

	/// <summary>
	/// This property contains the MudBlazor theme for this component.
	/// </summary>
	[CascadingParameter(Name = "Theme")]
	public MudTheme Theme { get; set; } = null!;

	/// <summary>
	/// This property contains the localizer for this component.
	/// </summary>
	[Inject]
	protected IStringLocalizer<MainLayout> Localizer { get; set; } = null!;

	/// <summary>
	/// This property contains the logger for this component.
	/// </summary>
	[Inject]
	protected ILogger<MainLayout> Logger { get; set; } = null!;

	#endregion

	// *******************************************************************
	// Private methods.
	// *******************************************************************

	#region Private methods

	/// <summary>
	/// This method toggles the drawer open and closed.
	/// </summary>
	void DrawerToggle()
	{
		_drawerOpen = !_drawerOpen;
	}

	#endregion
}
