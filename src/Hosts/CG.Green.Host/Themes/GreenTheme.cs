
namespace CG.Green.Host.Themes;

/// <summary>
/// This class represents the default MudBlazor UI theme.
/// </summary>
public class GreenTheme : BaseTheme<GreenTheme>
{
    // *******************************************************************
    // Constructors.
    // *******************************************************************

    #region Constructors

    /// <summary>
    /// This constructor creates a new instance of the <see cref="GreenTheme"/>
    /// class.
    /// </summary>
    public GreenTheme()
    {
        // Create the default palette
        Palette.Primary = Colors.Blue.Default;
        Palette.Secondary = Colors.Orange.Default;
        Palette.Tertiary = Colors.Purple.Lighten2;
        Palette.AppbarBackground = Colors.Green.Default;
    }

    #endregion
}
