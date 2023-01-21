
namespace CG.Green.Options;

/// <summary>
/// This class contains configuration settings for seeding user claim 
/// assignments.
/// </summary>
public class UserClaimOptions
{
    // *******************************************************************
    // Properties.
    // *******************************************************************

    #region Properties

    /// <summary>
    /// This property contains a list of <see cref="UserClaimAssignmentOptions"/> objects.
    /// </summary>
    [Required]
    public List<UserClaimAssignmentOptions> UserClaims { get; set; } = new();

    #endregion
}


/// <summary>
/// This class contains configuration settings for seeding a user claim
/// assignment.
/// </summary>
public class UserClaimAssignmentOptions
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
    /// This property contains the list of claims assigned to the user.
    /// </summary>
    [Required]
    public List<ClaimOptions> Claims { get; set; } = new();

    #endregion
}


/// <summary>
/// This class contains configuration settings for seeding a claim.
/// </summary>
public class ClaimOptions
{
    // *******************************************************************
    // Properties.
    // *******************************************************************

    #region Properties

    /// <summary>
    /// This property contains the type of the claim.
    /// </summary>
    [Required]
    public string ClaimType { get; set; } = null!;

    /// <summary>
    /// This property contains the value of the claim.
    /// </summary>
    public string? ClaimValue { get; set; }

    #endregion
}
