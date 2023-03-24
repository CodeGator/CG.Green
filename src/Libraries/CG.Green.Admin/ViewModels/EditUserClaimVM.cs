
namespace CG.Green.ViewModels;

/// <summary>
/// This class is a view-model that represents an ASP.NET user claim.
/// </summary>
public class EditUserClaimVM
{
	// *******************************************************************
	// Properties.
	// *******************************************************************

	#region Properties

	/// <summary>
	/// This property contains the value of the claim.
	/// </summary>
	[Display(ShortName = "ClaimValue")]
    [MaxLength(Globals.Models.Claims.ValueLength)]
    public string ClaimValue { get; set; } = "";

	/// <summary>
	/// This property contains the type of the claim.
	/// </summary>
	[Required]
	[Display(ShortName = "ClaimType")]
	[MaxLength(Globals.Models.Claims.TypeLength)]
	public string ClaimType { get; set; } = "";

	#endregion
}
