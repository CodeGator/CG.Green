
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
	/// This property contains the maximum lifetime of a refresh token, in
	/// seconds, for the client.
	/// </summary>
	[Required]
	[Display(Name = "Absolute Refresh Token Lifetime")]
	public int AbsoluteRefreshTokenLifetime { get; set; }

	/// <summary>
	/// This property contains the allowed CORS origins for the client.
	/// </summary>
	[Required]
	[Display(Name = "Allow CORS Origins")]
	public List<string> AllowedCorsOrigins { get; set; } = new();

	/// <summary>
	/// This property indicates whether the client can access tokens via browser.
	/// </summary>
	[Required]
	[Display(Name = "Allow Access Tokens Via Browser")]
	public bool AllowAccessTokensViaBrowser { get; set; }

	/// <summary>
	/// This property contains the allowed grant types for the client.
	/// </summary>
	[Required]
	[Display(Name = "Allowed Grant Types")]
	public IEnumerable<string> AllowedGrantTypes { get; set; } = new List<string>();

	/// <summary>
	/// This property contains the allowed identity token signing algorithms,
	/// for the client.
	/// </summary>
	[Required]
	[Display(Name = "Allow Access Tokens Via Browser")]
	public List<string> AllowedIdentityTokenSigningAlgorithms { get; set; } = new();

	/// <summary>
	/// This property contains the allowed scopes for the client.
	/// </summary>
	[Required]
	[Display(Name = "Allowed Scopes")]
	public IEnumerable<string> AllowedScopes { get; set; } = new List<string>();

	/// <summary>
	/// This property indicates whether the identity token should always 
	/// include user claims.
	/// </summary>
	[Required]
	[Display(Name = "Allow Include User Claims In Id Token")]
	public bool AlwaysIncludeUserClaimsInIdToken { get; set; } = false;

	/// <summary>
	/// This property indicates whether client claims should always be sent.
	/// </summary>
	[Required]
	[Display(Name = "Allow Send Client Claims")]
	public bool AlwaysSendClientClaims { get; set; } = false;

	/// <summary>
	/// This property contains the lifetime of an access token, for the client.
	/// </summary>
	[Required]
	[Display(Name = "Access Token Lifetime")]
	public int AccessTokenLifetime { get; set; }

	/// <summary>
	/// This property contains the access token type, for the client.
	/// </summary>
	[Required]
	[Display(Name = "Access Token Type")]
	public AccessTokenType AccessTokenType { get; set; }

	/// <summary>
	/// This property indicates whether or not the client is allows
	/// offline accesss.
	/// </summary>
	[Display(Name = "Offline Access")]
	public bool AllowOfflineAccess { get; set; }

	/// <summary>
	/// This property contains a list of claims for the client.
	/// </summary>
	[Display(Name = "Claims")]
	public List<ClientClaimVM> Claims { get; set; } = new();

	/// <summary>
	/// This property contains the identifier for the client.
	/// </summary>
	[Required]
    [MaxLength(Globals.Models.Clients.ClientIdLength)]
    [Display(Name = "Client Id")]
    public string ClientId { get; set; } = null!;

	/// <summary>
	/// This property contains the client claims prefix for the client.
	/// </summary>
	[MaxLength(Globals.Models.Clients.ClientClaimsPrefixLength)]
	[Display(Name = "Client ClaimsPrefix")]
	public string ClientClaimsPrefix { get; set; } = null!;

	/// <summary>
	/// This property contains a list of secrets for the client.
	/// </summary>
	[Display(Name = "Client Secrets")]
	public List<ClientSecretVM> ClientSecrets { get; set; } = new();

	/// <summary>
	/// This property contains the name for the client.
	/// </summary>
	[Required]
    [MaxLength(Globals.Models.Clients.ClientNameLength)]
    [Display(Name = "Client Name")]
    public string ClientName { get; set; } = null!;

	/// <summary>
	/// This property contains the sliding refresh token lifetime, 
	/// in seconds, for the client.
	/// </summary>
	[Required]
	[Display(Name = "Sliding Refresh Token Lifetime")]
	public int SlidingRefreshTokenLifetime { get; set; }

	/// <summary>
	/// This property contains the description for the client.
	/// </summary>
	[MaxLength(Globals.Models.Clients.DescriptionLength)]
    [Display(Name = "Description")]
    public string Description { get; set; } = null!;

	/// <summary>
	/// This property indicates whether or not the client is enabled.
	/// </summary>
	[Display(Name = "Enabled")]
	public bool Enabled { get; set; }

	/// <summary>
	/// This property contains a list of front channel logout URIs for the client.
	/// </summary>
	[Display(Name = "Front Channel Logout Uris")]
	public List<string> FrontChannelLogoutUris { get; set; } = new();

	/// <summary>
	/// This property contains the lifetime of an identity token, for the client.
	/// </summary>
	[Required]
	[Display(Name = "Identity Token Lifetime")]
	public int IdentityTokenLifetime { get; set; }

	/// <summary>
	/// This property indicates the view-model has unsaved changes.
	/// </summary>
	public bool IsDirty { get; set; }

	/// <summary>
	/// This property contains a list of post logout redirect URIs for the client.
	/// </summary>
	[Display(Name = "Post Logout Uris")]
	public List<string> PostLogoutRedirectUris { get; set; } = new();

	/// <summary>
	/// This property contains the expiration of a refresh token, for the client.
	/// </summary>
	[Required]
	[Display(Name = "Refresh Token Expiration")]
	public TokenExpiration RefreshTokenExpiration { get; set; }

	/// <summary>
	/// This property contains the refresh token usage, for the client.
	/// </summary>
	[Required]
	[Display(Name = "Refresh Token Usage")]
	public TokenUsage RefreshTokenUsage { get; set; }

	/// <summary>
	/// This property indicates whether or not a consent screen is required
	/// for the client.
	/// </summary>
	[Display(Name = "Require Consent")]
	public bool RequireConsent { get; set; }

    /// <summary>
    /// This property indicates whether or not the client is required
    /// to know one or more secrets.
    /// </summary>
    [Display(Name = "Require Client Secret")]
    public bool RequireClientSecret { get; set; }

	/// <summary>
	/// This property indicates whether or not the client is required
	/// to send signed requests only.
	/// </summary>
	[Display(Name = "Required Request Object")]
	public bool RequireRequestObject { get; set; }

	/// <summary>
	/// This property contains a list of redirect URIs for the client.
	/// </summary>
	[Display(Name = "Redirect Uris")]
    public List<string> RedirectUris { get; set; } = new();

	/// <summary>
	/// This property indicates whether an access token should be updated
	/// on a refresh token.
	/// </summary>
	[Display(Name = "Update Access Token Claims On Refresh")]
	public bool UpdateAccessTokenClaimsOnRefresh { get; set; }

	/// <summary>
	/// This property contains a list of valid grant type combinations.
	/// </summary>
	[Display(Name = "Valid Grant Types")]
	public List<string> ValidGrantTypes { get; set; } = new()
	{
		"Ciba",
		"ClientCredentials",
		"Code",
		"CodeAndClientCredentials",
		"DeviceFlow",
		"Hybrid",
		"HybridAndClientCredentials",
		"Implicit",
		"ImplicitAndClientCredentials",
		"ResourceOwnerPassword",
	};

	/// <summary>
	/// This property contains a list of valid scopes.
	/// </summary>
	[Display(Name = "Valid Scopes")]
	public List<string> ValidScopes { get; set; } = new();

	#endregion
}
