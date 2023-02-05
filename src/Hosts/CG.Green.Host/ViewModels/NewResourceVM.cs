
namespace CG.Green.Host.ViewModels;

/// <summary>
/// This class is a view-model for creating an identity resource.
/// </summary>
public class NewResourceVM
{
    // *******************************************************************
    // Properties.
    // *******************************************************************

    #region Properties

    /// <summary>
    /// This property contains the name of the identity resource.
    /// </summary>
    [MaxLength(Globals.Models.Resources.NameLength)]
    [Display(Name = "Name")]
    public string Name { get; set; } = null!;

    #endregion
}
