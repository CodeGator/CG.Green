namespace CG.Green.Host.ViewModels;

/// <summary>
/// This class is a view-model for editing a property.
/// </summary>
public class EditPropertyVM
{
    // *******************************************************************
    // Properties.
    // *******************************************************************

    #region Properties

    /// <summary>
    /// This property contains the property key.
    /// </summary>
    [Required]
    [Display(Name = "Key")]
    public string Key { get; set; } = null!;

    /// <summary>
    /// This property contains the property value.
    /// </summary>
    [Display(Name = "Value")]
    public string Value { get; set; } = null!;

    #endregion
}
