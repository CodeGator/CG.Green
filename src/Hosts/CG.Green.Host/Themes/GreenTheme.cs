
using CG.Blazor.Themes;

namespace CG.Green.Host.Themes;

/// <summary>
/// This class represents the default MudBlazor UI theme.
/// </summary>
public class GreenTheme : DefaultTheme<GreenTheme>
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
        // Create the Purple default palette
        Palette.Primary = Colors.Orange.Darken2;
        Palette.Secondary = Colors.Blue.Default;
        Palette.Tertiary = Colors.Purple.Default;
        Palette.AppbarBackground = Colors.Green.Default;
    }

    #endregion
}
