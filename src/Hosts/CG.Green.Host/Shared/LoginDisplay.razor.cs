namespace CG.Green.Host.Shared;

/// <summary>
/// This class is the code-behind for the <see cref="LoginDisplay"/> component.
/// </summary>
public partial class LoginDisplay
{
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
	protected IStringLocalizer<LoginDisplay> Localizer { get; set; } = null!;

	/// <summary>
	/// This property contains the logger for this component.
	/// </summary>
	[Inject]
	protected ILogger<LoginDisplay> Logger { get; set; } = null!;

	#endregion
}
