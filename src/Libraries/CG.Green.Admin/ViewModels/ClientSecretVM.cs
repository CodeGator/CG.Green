
namespace CG.Green.ViewModels;

/// <summary>
/// This class is a view-model that represents a Duende client secret.
/// </summary>
public class ClientSecretVM
{
    // *******************************************************************
    // Properties.
    // *******************************************************************

    #region Properties

    /// <summary>
    /// This property contains the expiration date/time for the secret.
    /// </summary>
    public DateTime? Expiration { get; set; }

    /// <summary>
    /// This property contains the description for the secret.
    /// </summary>
    [MaxLength(Globals.Models.Secrets.DescriptionLength)]
    [Display(Name = "Description")]
    public string Description { get; set; } = null!;

    /// <summary>
    /// This property contains the value for the secret.
    /// </summary>
    [Required]
    [MaxLength(Globals.Models.Secrets.ValueLength)]
    [Display(Name = "Value")]
    public string Value { get; set; } = null!;

    /// <summary>
    /// This property indicates whether the secret is hashed, or not.
    /// </summary>
    public bool IsHashed { get; set; }

    #endregion
}
