
namespace CG.Green.Options;

/// <summary>
/// This class contains configuration settings for seeding role claim 
/// assignments.
/// </summary>
public class RoleClaimOptions
{
    // *******************************************************************
    // Properties.
    // *******************************************************************

    #region Properties

    /// <summary>
    /// This property contains a list of <see cref="RoleClaimAssignmentOptions"/> objects.
    /// </summary>
    [Required]
    public List<RoleClaimAssignmentOptions> RoleClaims { get; set; } = new();

    #endregion
}


/// <summary>
/// This class contains configuration settings for seeding a role claim
/// assignment.
/// </summary>
public class RoleClaimAssignmentOptions
{
    // *******************************************************************
    // Properties.
    // *******************************************************************

    #region Properties

    /// <summary>
    /// This property contains the name for the role.
    /// </summary>
    [Required]
    [MaxLength(Globals.Models.Roles.NameLength)]
    public string RoleName { get; set; } = null!;

    /// <summary>
    /// This property contains the list of claims assigned to the role.
    /// </summary>
    [Required]
    public List<ClaimOptions> Claims { get; set; } = new();

    #endregion
}
