
namespace CG.Green.Options;

/// <summary>
/// This class contains configuration settings for seeding api scopes.
/// </summary>
public class IdentityResourceOptions
{
    // *******************************************************************
    // Properties.
    // *******************************************************************

    #region Properties

    /// <summary>
    /// This property contains a list of <see cref="IdentityResource"/> objects.
    /// </summary>
    [Required]
    public List<IdentityResource> IdentityResources { get; set; } = new();

    #endregion
}
