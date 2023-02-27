
namespace CG.Green.ViewModels;

/// <summary>
/// This class is a view-model for creating a new Duende client.
/// </summary>
public class NewClientVM
{
	// *******************************************************************
	// Properties.
	// *******************************************************************

	#region Properties

	/// <summary>
	/// This property contains the client identifier.
	/// </summary>
	[Required]
	[Display(ShortName = "ClientId")]
	[MaxLength(Globals.Models.Clients.ClientIdLength)]
	public string ClientId { get; set; } = null!;

	#endregion
}
