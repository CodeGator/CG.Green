
namespace CG.Green.Host.Shared;

/// <summary>
/// This class is the code-behind for the <see cref="SiteFooter"/> component.
/// </summary>
public partial class SiteFooter
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
	protected IStringLocalizer<SiteFooter> Localizer { get; set; } = null!;

	/// <summary>
	/// This property contains the navigation service for this component.
	/// </summary>
	[Inject]
	protected NavigationManager Navigation { get; set; } = null!;

	/// <summary>
	/// This property contains the logger for this component.
	/// </summary>
	[Inject]
	protected ILogger<SiteFooter> Logger { get; set; } = null!;

	#endregion
}

