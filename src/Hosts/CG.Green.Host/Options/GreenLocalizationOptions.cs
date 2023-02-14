
namespace CG.Green.Host.Options;

/// <summary>
/// This class contains configuration settings for UI localization.
/// </summary>
public class GreenLocalizationOptions
{
    // *******************************************************************
    // Properties.
    // *******************************************************************

    #region Properties

    /// <summary>
    /// This property contains the default culture for the website.
    /// </summary>
    [Required]
    public string Default { get; set; } = null!;

    /// <summary>
    /// This property contains a list of available cultures.
    /// </summary>
    [Required]
    public List<string> Cultures { get; set; } = new();

    #endregion
}
