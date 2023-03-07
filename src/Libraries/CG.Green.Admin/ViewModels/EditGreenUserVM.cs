
namespace CG.Green.ViewModels;

// <summary>
/// This class is a view-model for editing a GreenUser.
/// </summary>
public class EditGreenUserVM
{
	// *******************************************************************
	// Properties.
	// *******************************************************************

	#region Properties

	/// <summary>
	/// This property contains the identifier for the user.
	/// </summary>
	public string Id { get; set; } = null!;

	/// <summary>
	/// This property contains the number of failed login attempts for 
	/// the user.
	/// </summary>
	[Display(ShortName = "AccessFailedCount")]
	public int AccessFailedCount { get; set; }

	/// <summary>
	/// This property indicates whether or not the user has confirmed their 
	/// email address.
	/// </summary>
	[Display(ShortName = "EmailConfirmed")]
	public bool EmailConfirmed { get; set; }

	/// <summary>
	/// This property indicates whether or not the user can be locked out.
	/// </summary>
	[Display(ShortName = "LockoutEnabled")]
	public bool LockoutEnabled { get; set; }

	/// <summary>
	/// This property contains the date and time, in UTC, when the user's 
	/// lockout ends.
	/// </summary>
	[Display(ShortName = "LockoutEnd")]
	public DateTime? LockoutEnd { get; set; }

	/// <summary>
	/// This property indicates whether or not two factor authentication 
	/// is enabled for the user.
	/// </summary>
	[Display(ShortName = "TwoFactorEnabled")]
	public bool TwoFactorEnabled { get; set; }

	/// <summary>
	/// This property contains the name for the user.
	/// </summary>
	[Required]
	[MaxLength(Globals.Models.Users.UserNameLength)]
	[Display(ShortName = "UserName")]
	public string UserName { get; set; } = null!;

	/// <summary>
	/// This property contains the email for the user.
	/// </summary>
	[Required]
	[MaxLength(Globals.Models.Users.EmailLength)]
	[EmailAddress]
	[Display(ShortName = "Email")]
	public string Email { get; set; } = null!;

	#endregion
}
