
namespace CG.Green.ViewModels;

/// <summary>
/// This class is a view-model for editing a Duende secret.
/// </summary>
public class EditSecretVM
{
    // *******************************************************************
    // Properties.
    // *******************************************************************

    #region Properties

    /// <summary>
    /// This property indicates the expiration date/time for the secret.
    /// </summary>
    public DateTime? Expiration { get; set; }

    /// <summary>
    /// This property contains the description for the secret.
    /// </summary>
    [Required]
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
    /// This property indicates whether or not the secret is hashed.
    /// </summary>
    public bool IsHashed { get; set; }

    #endregion
}
