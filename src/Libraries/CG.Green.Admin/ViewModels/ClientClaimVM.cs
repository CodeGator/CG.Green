
namespace CG.Green.ViewModels;

/// <summary>
/// This class is a view-model that represents a client claim.
/// </summary>
public class ClientClaimVM
{
    // *******************************************************************
    // Properties.
    // *******************************************************************

    #region Properties

    /// <summary>
    /// This property contains the value of the claim.
    /// </summary>
    [MaxLength(Globals.Models.Claims.ValueLength)]
    public string Value { get; set; } = "";

	/// <summary>
	/// This property contains the type of the claim.
	/// </summary>
	[Required]
	[MaxLength(Globals.Models.Claims.TypeLength)]
	public string Type { get; set; } = "";

	#endregion
}
