
namespace CG.Green.ViewModels;

// <summary>
/// This class is a view-model for create a new ASP.NET user.
/// </summary>
public class NewGreenUserVM
{
	// *******************************************************************
	// Properties.
	// *******************************************************************

	#region Properties

	/// <summary>
	/// This property contains the name for the user.
	/// </summary>
	[Required]
	[StringLength(Globals.Models.Users.UserNameLength, MinimumLength = 6, ErrorMessageResourceName = "LengthError", ErrorMessageResourceType = typeof(Properties.Resources))]
	[Display(ShortName = "UserName")]
	public string UserName { get; set; } = null!;

	/// <summary>
	/// This property contains the email for the user.
	/// </summary>
	[Required]
	[StringLength(Globals.Models.Users.EmailLength, MinimumLength = 7, ErrorMessageResourceName = "LengthError", ErrorMessageResourceType = typeof(Properties.Resources))]
	[EmailAddress]
	[Display(ShortName = "Email")]
	public string Email { get; set; } = null!;

	/// <summary>
	/// This property contains the password for the user.
	/// </summary>
	[Required]
	[StringLength(Globals.Models.Users.PasswordHashLength, MinimumLength = 6, ErrorMessageResourceName ="LengthError", ErrorMessageResourceType = typeof(Properties.Resources))]
	[DataType(DataType.Password)]
	[OneOrMoreDigits(ErrorMessageResourceName = "PasswordDigitsError", ErrorMessageResourceType = typeof(Properties.Resources))]
	[OneOrMoreNonAlpha(ErrorMessageResourceName = "PasswordNonAlphaError", ErrorMessageResourceType = typeof(Properties.Resources))]
	[OneOrMoreUpperCase(ErrorMessageResourceName = "PasswordCaseError", ErrorMessageResourceType = typeof(Properties.Resources))]
	[Display(ShortName = "Password")]
	public string Password { get; set; } = null!;

	/// <summary>
	/// This property contains the password for the user.
	/// </summary>
	[Required]
	[Compare("Password", ErrorMessageResourceName = "ConfirmPasswordError", ErrorMessageResourceType = typeof(Properties.Resources))]
	[Display(ShortName = "ConfirmPassword")]
	public string ConfirmPassword { get; set; } = null!;

	#endregion
}
