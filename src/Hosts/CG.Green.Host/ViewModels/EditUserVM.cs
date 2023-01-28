
namespace CG.Green.Host.ViewModels;

/// <summary>
/// This class is a view-model for editing an existing user.
/// </summary>
public class EditUserVM
{
    // *******************************************************************
    // Properties.
    // *******************************************************************

    #region Properties

    /// <summary>
    /// This property contains the associated user.
    /// </summary>
    [Required]
    public GreenUser User { get; set; } = null!;

    /// <summary>
    /// This property contains the email for the user.
    /// </summary>
    [Required]
    [EmailAddress]
    [MaxLength(Globals.Models.Users.EmailLength)]
    [Display(Name = "Email")]
    public string Email { get; set; } = null!;

    /// <summary>
    /// This property indicates the email is confirmed.
    /// </summary>
    [Display(Name = "Email Confirmed")]
    public bool EmailConfirmed { get; set; }

    /// <summary>
    /// This property contains the name for the user.
    /// </summary>
    [Required]
    [MaxLength(Globals.Models.Users.UserNameLength)]
    [Display(Name = "User Name")]
    public string UserName { get; set; } = null!;

    /// <summary>
    /// This property contains the failed login count for the user.
    /// </summary>
    [Display(Name = "Access File Count")]
    public int AccessFailedCount { get; set; }

    /// <summary>
    /// This property indicates whether the user can be locked out.
    /// </summary>
    [Display(Name = "Lockout Enabled")]
    public bool LockoutEnabled { get; set; }

    /// <summary>
    /// This property indicates whether two factor authentication is enabled 
    /// for the user.
    /// </summary>
    [Display(Name = "Two Factor Enabled")]
    public bool TwoFactorEnabled { get; set; }

    /// <summary>
    /// This property contains the list of assigned roles for the user.
    /// </summary>
    [Display(Name = "Assigned Roles")]
    public IEnumerable<string> AssignedRoles { get; set; } = new List<string>();

    /// <summary>
    /// This property contains the list of assigned roles for the user.
    /// </summary>
    [Display(Name = "Assigned Claims")]
    public List<EditClaimVM> AssignedClaims { get; set; } = new();

    #endregion
}
