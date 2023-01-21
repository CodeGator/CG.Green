
namespace CG.Green.Options;

/// <summary>
/// This class contains configuration settings for seeding user role 
/// assignments.
/// </summary>
public class UserRoleOptions
{
    // *******************************************************************
    // Properties.
    // *******************************************************************

    #region Properties

    /// <summary>
    /// This property contains a list of <see cref="UserRoleAssignmentOptions"/> objects.
    /// </summary>
    [Required]
    public List<UserRoleAssignmentOptions> UserRoles { get; set; } = new();

    #endregion
}


/// <summary>
/// This class contains configuration settings for seeding a user role 
/// assignment.
/// </summary>
public class UserRoleAssignmentOptions
{
    // *******************************************************************
    // Properties.
    // *******************************************************************

    #region Properties

    /// <summary>
    /// This property contains the email address for a user.
    /// </summary>
    [Required]
    [EmailAddress]
    public string UserEmail { get; set; } = null!;

    /// <summary>
    /// This property contains the list of assigned role names.
    /// </summary>
    [Required]
    public List<string> RoleNames { get; set; } = new();

    #endregion
}
