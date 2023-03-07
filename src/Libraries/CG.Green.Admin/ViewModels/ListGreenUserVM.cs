
namespace CG.Green.ViewModels;

// <summary>
/// This class is a view-model for listing an ASP.NET user.
/// </summary>
public class ListGreenUserVM
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
