
namespace CG.Green.Host.ViewModels;

/// <summary>
/// This class is a view-model for creating a new client.
/// </summary>
public class NewClientVM
{
    // *******************************************************************
    // Properties.
    // *******************************************************************

    #region Properties

    /// <summary>
    /// This property contains the identifier for the client.
    /// </summary>
    [Required]
    [MaxLength(Globals.Models.Clients.ClientNameLength)]
    public string ClientId { get; set; } = null!;

    #endregion
}
