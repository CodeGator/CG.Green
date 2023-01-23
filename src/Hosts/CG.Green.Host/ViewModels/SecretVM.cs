namespace CG.Green.Host.ViewModels;

/// <summary>
/// This class is a view-model for a Duende client secret.
/// </summary>
public class SecretVM
{
    // *******************************************************************
    // Properties.
    // *******************************************************************

    #region Properties

    /// <summary>
    /// This property contains the Duende client secret.
    /// </summary>
    [Required]
    public Secret Secret { get; set; } = null!;

    /// <summary>
    /// This property indicates whether the secret has been hashed, or not.
    /// </summary>
    public bool IsHashed { get; set; }

    #endregion
}
