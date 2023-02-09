namespace CG.Green.Host.ViewModels;

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
    /// This property contains the description for the secret.
    /// </summary>
    [Required]
    [MaxLength(Globals.Models.Secrets.DescriptionLength)]
    [Display(Name = "Description")]
    public string Description { get; set; } = null!;

    /// <summary>
    /// This property contains the type for the secret.
    /// </summary>
    [Required]
    [MaxLength(Globals.Models.Secrets.TypeLength)]
    [Display(Name = "Type")]
    public string Type { get; set; } = null!;

    /// <summary>
    /// This property contains the value for the secret.
    /// </summary>
    [Required]
    [MaxLength(Globals.Models.Secrets.ValueLength)]
    [Display(Name = "Value")]
    public string Value { get; set; } = null!;

    /// <summary>
    /// This property contains the expiration for the secret.
    /// </summary>
    [Display(Name = "Expiration")]
    public DateTime? Expiration { get; set; }

    /// <summary>
    /// This property indicates whether the secret has been hashed, or not.
    /// </summary>
    [Display(Name = "Hashed")]
    public bool IsHashed { get; set; }

    #endregion
}
