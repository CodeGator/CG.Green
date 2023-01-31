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
    /// This property contains the id for the role.
    /// </summary>
    [MaxLength(Globals.Models.Roles.IdLength)]
    [Display(Name = "Role Id")]
    public string Id { get; set; } = null!;

    /// <summary>
    /// This property contains the name for the role.
    /// </summary>
    [Required]
    [MaxLength(Globals.Models.Roles.NameLength)]
    [Display(Name = "Role Name")]
    public string Name { get; set; } = null!;

    /// <summary>
    /// This property contains the assigned claims for the role.
    /// </summary>
    [Required]
    [Display(Name = "Role Claims")]
    public List<EditClaimVM> AssignedClaims { get; set; } = new();

    #endregion
}
