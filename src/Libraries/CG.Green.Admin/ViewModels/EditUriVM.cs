
namespace CG.Green.ViewModels;

/// <summary>
/// This class is a view-model that represents a URI.
/// </summary>
public class EditUriVM
{
    // *******************************************************************
    // Properties.
    // *******************************************************************

    #region Properties

    /// <summary>
    /// This property contains the value of the URI.
    /// </summary>
    [Required]
	[Display(ShortName = "Value")]
	[MaxLength(Globals.Models.UriLength)]
    public string Value { get; set; } = "";

    #endregion
}
