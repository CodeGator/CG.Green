
namespace CG.Green.Host.Shared;

/// <summary>
/// This class is the code-behind for the <see cref="CultureSelector"/> component.
/// </summary>
public partial class CultureSelector
{
    // *******************************************************************
    // Properties.
    // *******************************************************************

    #region Properties

    /// <summary>
    /// This property contains the navigation manager for this component.
    /// </summary>
    [Inject]
    internal protected NavigationManager Navigation { get; set; } = null!;

    // *******************************************************************

    /// <summary>
    /// This property contains the localization options for this component.
    /// </summary>
    [Inject]
    internal protected IOptions<GreenLocalizationOptions> Options { get; set; } = null!;

    // *******************************************************************

    /// <summary>
    /// This property contains the current culture.
    /// </summary>
    protected CultureInfo Culture
    {
        get => CultureInfo.CurrentCulture;
        set
        {
            if (CultureInfo.CurrentCulture != value)
            {
                var uri = new Uri(Navigation.Uri)
                    .GetComponents(UriComponents.PathAndQuery, UriFormat.Unescaped);
                var cultureEscaped = Uri.EscapeDataString(value.Name);
                var uriEscaped = Uri.EscapeDataString(uri);

                Navigation.NavigateTo(
                    $"api/Culture/Set?culture={cultureEscaped}&redirectUri={uriEscaped}",
                    forceLoad: true
                );
            }
        }
    }

	#endregion

	// *******************************************************************
	// Protected methods.
	// *******************************************************************

	#region Protected methods

	/// <summary>
	/// This method is called to initialize the component.
	/// </summary>
	protected override void OnInitialized()
    {
        Culture = CultureInfo.CurrentCulture;
    }

    #endregion
}
