
namespace CG.Green.Host.ViewModels;

/// <summary>
/// This class is a view-model for editing an existing client.
/// </summary>
public class EditClientVM
{
    // *******************************************************************
    // Properties.
    // *******************************************************************

    #region Properties

    /// <summary>
    /// This property contains the external identifier for the client.
    /// </summary>
    [Required]
    [MaxLength(Globals.Models.Clients.ClientIdLength)]
    public string ClientId { get; set; } = null!;

    /// <summary>
    /// This property contains the name for the client.
    /// </summary>
    [Required]
    [MaxLength(Globals.Models.Clients.ClientNameLength)]
    public string ClientName { get; set; } = null!;

    /// <summary>
    /// This property contains the list of allowed scopes for the client.
    /// </summary>
    public List<EditScopeVM> AllowedScopes { get; set; } = new();

    /// <summary>
    /// This property contains the allowed grant types for the client.
    /// </summary>
    public AllowedGrantTypes AllowedGrantTypes { get; set; } = new();

    /// <summary>
    /// This property contains the list of secrets for the client.
    /// </summary>
    public List<SecretVM> ClientSecrets { get; set; } = new();

    /// <summary>
    /// This property contains the list of redirect uris for the client.
    /// </summary>
    public List<_Wrapper> RedirectUris { get; set; } = new();

    /// <summary>
    /// This property contains the list of post logout redirect uris for the client.
    /// </summary>
    public List<_Wrapper> PostLogoutRedirectUris { get; set; } = new();

    /// <summary>
    /// This property contains the list of legal CORS origins for the client.
    /// </summary>
    public List<_Wrapper> AllowedCorsOrigins { get; set; } = new();

    /// <summary>
    /// This property contains the lost of all available scopes.
    /// </summary>
    public IEnumerable<string> AllScopes { get; set; } = new List<string>();

    #endregion
}
