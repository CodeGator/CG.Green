
namespace CG.Green.Options;

/// <summary>
/// This class contains configuration settings for seeding a claim.
/// </summary>
public class ClaimOptions
{
    // *******************************************************************
    // Properties.
    // *******************************************************************

    #region Properties

    /// <summary>
    /// This property contains the type of the claim.
    /// </summary>
    [Required]
    public string ClaimType { get; set; } = null!;

    /// <summary>
    /// This property contains the value of the claim.
    /// </summary>
    public string? ClaimValue { get; set; }

    #endregion
}
