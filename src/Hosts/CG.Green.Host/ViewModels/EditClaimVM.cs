namespace CG.Green.Host.ViewModels;

/// <summary>
/// This class is a view-model for editing a claim.
/// </summary>
public class EditClaimVM
{
    // *******************************************************************
    // Properties.
    // *******************************************************************

    #region Properties

    /// <summary>
    /// This property contains the type for the claim.
    /// </summary>
    [Required]
    [EmailAddress]
    [MaxLength(100)]
    [Display(Name = "Claim Type")]
    public string ClaimType { get; set; } = null!;

    /// <summary>
    /// This property contains the value for the claim.
    /// </summary>
    [Required]
    [EmailAddress]
    [MaxLength(100)]
    [Display(Name = "Claim Value")]
    public string ClaimValue { get; set; } = null!;

    #endregion
}
