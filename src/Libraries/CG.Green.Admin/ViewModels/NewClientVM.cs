
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
    [MaxLength(Globals.Models.Clients.ClientIdLength)]
    public string ClientId { get; set; } = null!;

    /// <summary>
    /// This property contains the client flow type.
    /// </summary>
    [Required]
    public ClientFlow ClientFlow { get; set; }

    /// <summary>
    /// This property contains helper text for client flows.
    /// </summary>
    public Dictionary<ClientFlow, string> HelpText { get; } = new()
    {
        { ClientFlow.Custom, "A completely custom client" },
        { ClientFlow.M2M, "Console apps, services or daemons running on your server" },
        { ClientFlow.Native, "iOS, Android, TV or IoT devices in the wild" },
        { ClientFlow.Spa, "Single page web application, running in a browser" },
        { ClientFlow.Web, "Traditional web application, running on your server" }
    };

    #endregion
}
