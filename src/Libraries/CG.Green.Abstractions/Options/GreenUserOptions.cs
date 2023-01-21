
namespace CG.Green.Options;

/// <summary>
/// This class contains configuration settings for seeding users.
/// </summary>
public class GreenUserOptions
{
    // *******************************************************************
    // Properties.
    // *******************************************************************

    #region Properties

    /// <summary>
    /// This property contains a list of <see cref="GreenUser"/> objects.
    /// </summary>
    [Required]
    public List<GreenUser> Users { get; set; } = new();

    #endregion
}
