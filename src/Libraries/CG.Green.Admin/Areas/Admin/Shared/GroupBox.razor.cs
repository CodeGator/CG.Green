
namespace CG.Green.Areas.Admin.Shared;

/// <summary>
/// This class is the code-behind for the <see cref="GroupBox"/> component.
/// </summary>
public partial class GroupBox : MudComponentBase
{
	// *******************************************************************
	// Properties.
	// *******************************************************************

	#region Properties

	/// <summary>
	/// This property contains the class for the component.
	/// </summary>
	protected string Classname => new CssBuilder("groupbox")
		.AddClass($"groupbox-outlined", Outlined)
		.AddClass($"groupbox-square", Square)
		.AddClass($"groupbox-elevation-{Elevation.ToString()}", !Outlined)
		.AddClass(Class)
		.Build();

	/// <summary>
	/// This property contains the style for the component.
	/// </summary>
	protected string Stylename =>
	new StyleBuilder()
		.AddStyle("height", $"{Height}", !String.IsNullOrEmpty(Height))
		.AddStyle("width", $"{Width}", !String.IsNullOrEmpty(Width))
		.AddStyle("max-height", $"{MaxHeight}", !String.IsNullOrEmpty(MaxHeight))
		.AddStyle("max-width", $"{MaxWidth}", !String.IsNullOrEmpty(MaxWidth))
		.AddStyle("min-height", $"{MinHeight}", !String.IsNullOrEmpty(MinHeight))
		.AddStyle("min-width", $"{MinWidth}", !String.IsNullOrEmpty(MinWidth))
		.AddStyle(Style)
	.Build();

	/// <summary>
	/// This property contains the theme for the component.
	/// </summary>
	[CascadingParameter(Name = "Theme")]
	[Category(CategoryTypes.Paper.Appearance)]
	public MudTheme? Theme { get; set; }

	/// <summary>
	/// This property contains the child content for the component.
	/// </summary>
	[Parameter]
	[Category(CategoryTypes.Paper.Behavior)]
	public RenderFragment ChildContent { get; set; } = null!;

	/// <summary>
	/// The higher the number, the heavier the drop-shadow.
	/// </summary>
	[Parameter]
	[Category(CategoryTypes.Paper.Appearance)]
	public int Elevation { set; get; } = 1;

	/// <summary>
	/// If true, border-radius is set to 0.
	/// </summary>
	[Parameter]
	[Category(CategoryTypes.Paper.Appearance)]
	public bool Square { get; set; }

	/// <summary>
	/// If true, card will be outlined.
	/// </summary>
	[Parameter]
	[Category(CategoryTypes.Paper.Appearance)]
	public bool Outlined { get; set; }

	/// <summary>
	/// Height of the component.
	/// </summary>
	[Parameter]
	[Category(CategoryTypes.Paper.Appearance)]
	public string? Height { get; set; }

	/// <summary>
	/// Width of the component.
	/// </summary>
	[Parameter]
	[Category(CategoryTypes.Paper.Appearance)]
	public string? Width { get; set; }

	/// <summary>
	/// Max-Height of the component.
	/// </summary>
	[Parameter]
	[Category(CategoryTypes.Paper.Appearance)]
	public string? MaxHeight { get; set; }

	/// <summary>
	/// Max-Width of the component.
	/// </summary>
	[Parameter]
	[Category(CategoryTypes.Paper.Appearance)]
	public string? MaxWidth { get; set; }

	/// <summary>
	/// Min-Height of the component.
	/// </summary>
	[Parameter]
	[Category(CategoryTypes.Paper.Appearance)]
	public string? MinHeight { get; set; }

	/// <summary>
	/// Min-Width of the component.
	/// </summary>
	[Parameter]
	[Category(CategoryTypes.Paper.Appearance)]
	public string? MinWidth { get; set; }

	/// <summary>
	/// Title of the component.
	/// </summary>
	[Parameter]
	[Category(CategoryTypes.Paper.Appearance)]
	public string? Title { get; set; }

	/// <summary>
	/// Set the text-align on the component.
	/// </summary>
	[Parameter]
	[Category(CategoryTypes.Text.Appearance)]
	public Align Align { get; set; } = Align.Inherit;

	/// <summary>
	/// The color of the component. It supports the theme colors.
	/// </summary>
	[Parameter]
	[Category(CategoryTypes.Text.Appearance)]
	public Color Color { get; set; } = Color.Inherit;

	/// <summary>
	/// Applies the theme typography styles.
	/// </summary>
	[Parameter]
	[Category(CategoryTypes.Text.Appearance)]
	public Typo Typo { get; set; } = Typo.subtitle2;

	/// <summary>
	/// Applies the right-to-left style.
	/// </summary>
	[CascadingParameter(Name = "RightToLeft")] 
	public bool RightToLeft { get; set; }

	#endregion
}
