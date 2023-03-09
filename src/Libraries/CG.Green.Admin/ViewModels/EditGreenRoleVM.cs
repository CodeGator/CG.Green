
namespace CG.Green.ViewModels;

// <summary>
/// This class is a view-model for editing a GreenRole.
/// </summary>
public class EditGreenRoleVM
{
	// *******************************************************************
	// Properties.
	// *******************************************************************

	#region Properties

	/// <summary>
	/// This property contains the identifier for the role.
	/// </summary>
	[MaxLength(Globals.Models.Roles.IdLength)]
	public string Id { get; set; } = null!;

	/// <summary>
	/// This property contains the name for the role.
	/// </summary>
	[Required]
	[MaxLength(Globals.Models.Roles.NameLength)]
	[Display(ShortName = "Name")]
	public string Name { get; set; } = null!;

	#endregion
}
