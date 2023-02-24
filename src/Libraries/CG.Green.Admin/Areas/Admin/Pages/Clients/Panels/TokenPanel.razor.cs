
namespace CG.Green.Areas.Admin.Pages.Clients.Panels;

/// <summary>
/// This class is the code-behind for the <see cref="TokenPanel"/> component.
/// </summary>
public partial class TokenPanel
{
	// *******************************************************************
	// Properties.
	// *******************************************************************

	#region Properties

	/// <summary>
	/// This property contains the model for the component.
	/// </summary>
	[CascadingParameter]
	public EditClientVM Model { get; set; } = null!;

	/// <summary>
	/// This property contains the MudBlazor them for the component.
	/// </summary>
	[CascadingParameter]
	public MudTheme Theme { get; set; } = null!;

	/// <summary>
	/// This property contains the green API for the component.
	/// </summary>
	[CascadingParameter]
	protected IGreenApi GreenApi { get; set; } = null!;

	/// <summary>
	/// This property contains the localizer for the component.
	/// </summary>
	[CascadingParameter]
	protected IStringLocalizer<TokenPanel> Localizer { get; set; } = null!;

	#endregion
}
