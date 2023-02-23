
namespace CG.Green.ViewModels;

/// <summary>
/// This class is a view-model for editing a Duende client.
/// </summary>
public class EditClientVM
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
    [Display(Name = "Client Id")]
    public string ClientId { get; set; } = null!;

    /// <summary>
    /// This property contains the name for the client.
    /// </summary>
    [Required]
    [MaxLength(Globals.Models.Clients.ClientNameLength)]
    [Display(Name = "Client Name")]
    public string ClientName { get; set; } = null!;

    /// <summary>
    /// This property indicates whether or not the client is enabled.
    /// </summary>
    [Display(Name = "Enabled")]
    public bool Enabled { get; set; }

    /// <summary>
    /// This property contains the description for the client.
    /// </summary>
    [MaxLength(Globals.Models.Clients.DescriptionLength)]
    [Display(Name = "Description")]
    public string Description { get; set; } = null!;

    /// <summary>
    /// This property indicates whether or not the client is allows
    /// offline accesss.
    /// </summary>
    [Display(Name = "Offline Access")]
    public bool AllowOfflineAccess { get; set; }

    /// <summary>
    /// This property indicates whether or not the client is required
    /// to send signed requests only.
    /// </summary>
    [Display(Name = "Required Request Object")]
    public bool RequireRequestObject { get; set; }

    /// <summary>
    /// This property indicates whether or not the client is required
    /// to know one or more secrets.
    /// </summary>
    [Display(Name = "Require Client Secret")]
    public bool RequireClientSecret { get; set; }

    /// <summary>
    /// This property contains a list of secrets for the client.
    /// </summary>
    [Display(Name = "Client Secrets")]
    public List<ClientSecretVM> ClientSecrets { get; set; } = new();

    /// <summary>
    /// This property contains a list of redirect URIs for the client.
    /// </summary>
    [Display(Name = "Redirect Uris")]
    public List<string> RedirectUris { get; set; } = new();

	/// <summary>
	/// This property contains a list of post logout redirect URIs for the client.
	/// </summary>
	[Display(Name = "Post Logout Uris")]
	public List<string> PostLogoutRedirectUris { get; set; } = new();

	/// <summary>
	/// This property contains a list of front channel logout URIs for the client.
	/// </summary>
	[Display(Name = "Front Channel Logout Uris")]
	public List<string> FrontChannelLogoutUris { get; set; } = new();

	/// <summary>
	/// This property contains a list of claims for the client.
	/// </summary>
	[Display(Name = "Claims")]
	public List<ClientClaimVM> Claims { get; set; } = new();

	#endregion
}
