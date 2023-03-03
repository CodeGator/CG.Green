
namespace CG.Green.ViewModels;

/// <summary>
/// This class is a view-model for editing a Duende property.
/// </summary>
public class EditPropertyVM
{
	// *******************************************************************
	// Properties.
	// *******************************************************************

	#region Properties

	/// <summary>
	/// This property contains the property key.
	/// </summary>
	[Required]
	[Display(ShortName = "Key")]
	[MaxLength(Globals.Models.Clients.PropertyKeyLength)]
	public string Key { get; set; } = null!;

	/// <summary>
	/// This property contains the property value.
	/// </summary>
	[Display(ShortName = "Value")]
	[MaxLength(Globals.Models.Clients.PropertyValueLength)]
	public string Value { get; set; } = null!;

	#endregion
}
