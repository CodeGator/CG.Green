
namespace CG.Green.ViewModels;

/// <summary>
/// This class is a view-model for listing a Duende client.
/// </summary>
public class ListClientVM
{
    // *******************************************************************
    // Properties.
    // *******************************************************************

    #region Properties

    /// <summary>
    /// This property contains the identifier for the client.
    /// </summary>
    [Required]
    [MaxLength(Globals.Models.Clients.ClientIdLength)]
    [Display(ShortName = "ClientId")]
    public string ClientId { get; set; } = null!;

    /// <summary>
    /// This property contains the name for the client.
    /// </summary>
    [Required]
    [MaxLength(Globals.Models.Clients.ClientNameLength)]
    [Display(ShortName = "ClientName")]
    public string ClientName { get; set; } = null!;

    /// <summary>
    /// This property indicates whether or not the client is enabled.
    /// </summary>
    [Display(ShortName = "Enabled")]
    public bool Enabled { get; set; }

    #endregion
}
