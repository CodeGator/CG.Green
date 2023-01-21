
namespace CG.Green.Options;

/// <summary>
/// This class contains configuration settings for seeding roles.
/// </summary>
public class GreenRoleOptions
{
    // *******************************************************************
    // Properties.
    // *******************************************************************

    #region Properties

    /// <summary>
    /// This property contains a list of <see cref="GreenRole"/> objects.
    /// </summary>
    [Required]
    public List<GreenRole> Roles { get; set; } = new();

    #endregion
}
