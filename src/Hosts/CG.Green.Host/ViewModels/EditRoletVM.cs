namespace CG.Green.Host.ViewModels;

/// <summary>
/// This class is a view-model for editing a role.
/// </summary>
public class EditRoleVM
{
    // *******************************************************************
    // Properties.
    // *******************************************************************

    #region Properties

    /// <summary>
    /// This property contains the name for the role.
    /// </summary>
    [Required]
    [EmailAddress]
    [MaxLength(256)]
    [Display(Name = "Role Name")]
    public string Name { get; set; } = null!;

    #endregion
}
