
namespace CG.Green.Options;

/// <summary>
/// This class contains configuration settings for seeding clients.
/// </summary>
public class ClientOptions
{
    // *******************************************************************
    // Properties.
    // *******************************************************************

    #region Properties

    /// <summary>
    /// This property contains a list of <see cref="Client"/> objects.
    /// </summary>
    [Required]
    public List<Client> Clients { get; set; } = new();

    #endregion
}
