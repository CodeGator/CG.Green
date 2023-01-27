
namespace CG.Green.Host.ViewModels;

/// <summary>
/// This class is a view-model for creating a new user.
/// </summary>
public class NewUserVM
{
    // *******************************************************************
    // Properties.
    // *******************************************************************

    #region Properties

    /// <summary>
    /// This property contains the email for the user.
    /// </summary>
    [Required]
    [EmailAddress]
    [MaxLength(Globals.Models.Users.EmailLength)]
    [Display(Name = "Email")]
    public string Email { get; set; } = null!;

    /// <summary>
    /// This property contains the name for the user.
    /// </summary>
    [Required]
    [MaxLength(Globals.Models.Users.UserNameLength)]
    [Display(Name = "User Name")]
    public string UserName { get; set; } = null!;

    /// <summary>
    /// This property contains the password for the user.
    /// </summary>
    [Required]
    [DataType(DataType.Password)]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
    [OneOrMoreDigits]
    [OneOrMoreNonAlpha]
    [OneOrMoreUpperCase]
    [Display(Name = "Password")]
    public string Password { get; set; } = null!;

    /// <summary>
    /// This property contains the verification password for the user.
    /// </summary>
    [Required]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    [Display(Name = "Confirm password")]
    public string ConfirmPassword { get; set; } = null!;

    #endregion
}
