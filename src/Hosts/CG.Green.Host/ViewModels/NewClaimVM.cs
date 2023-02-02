namespace CG.Green.Host.ViewModels;

/// <summary>
/// This class is a view-model for creating a claim.
/// </summary>
public class NewClaimVM
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
    [MaxLength(Globals.Models.Claims.TypeLength)]
    [Display(Name = "Claim Type")]
    public string ClaimType { get; set; } = null!;

    /// <summary>
    /// This property contains the value for the claim.
    /// </summary>
    [Required]
    [EmailAddress]
    [MaxLength(Globals.Models.Claims.ValueLength)]
    [Display(Name = "Claim Value")]
    public string ClaimValue { get; set; } = null!;

    #endregion
}
