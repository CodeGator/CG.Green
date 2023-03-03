
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
	[Display(ShortName = "AbsoluteRefreshTokenLifetime")]
	public int AbsoluteRefreshTokenLifetime { get; set; }

	/// <summary>
	/// This property contains the lifetime of an access token, for the client.
	/// </summary>
	[Required]
	[Display(ShortName = "AccessTokenLifetime")]
	public int AccessTokenLifetime { get; set; }

	/// <summary>
	/// This property contains the access token type, for the client.
	/// </summary>
	[Required]
	[Display(ShortName = "AccessTokenType")]
	public AccessTokenType AccessTokenType { get; set; }

	/// <summary>
	/// This property indicates whether the client can access tokens via browser.
	/// </summary>
	[Required]
	[Display(ShortName = "AllowAccessTokensViaBrowser")]
	public bool AllowAccessTokensViaBrowser { get; set; }

	/// <summary>
	/// This property contains the allowed CORS origins for the client.
	/// </summary>
	[Required]
	[Display(ShortName = "AllowedCORSOrigins")]
	public List<string> AllowedCorsOrigins { get; set; } = new();

	/// <summary>
	/// This property contains the allowed grant types for the client.
	/// </summary>
	[Required]
	[Display(ShortName = "AllowedGrantTypes")]
	public IEnumerable<string> AllowedGrantTypes { get; set; } = new List<string>();

	/// <summary>
	/// This property contains the allowed identity token signing algorithms,
	/// for the client.
	/// </summary>
	[Required]
	[Display(ShortName = "AllowedIdentityTokenSigningAlgorithms")]
	public List<string> AllowedIdentityTokenSigningAlgorithms { get; set; } = new();

	/// <summary>
	/// This property contains the allowed scopes for the client.
	/// </summary>
	[Required]
	[Display(ShortName = "AllowedScopes")]
	public IEnumerable<string> AllowedScopes { get; set; } = new List<string>();

	/// <summary>
	/// This property indicates whether or not the client is allows
	/// offline accesss.
	/// </summary>
	[Display(ShortName = "AllowOfflineAccess")]
	public bool AllowOfflineAccess { get; set; }

	/// <summary>
	/// This property indicates whether or not the client allows plain
	/// text PKCE.
	/// </summary>
	[Display(Name = "Allow Plain Text PKCE")]
	public bool AllowPlainTextPkce { get; set; }

	/// <summary>
	/// This property indicates whether or not the client is allowed to remember
	/// consent.
	/// </summary>
	[Display(ShortName = "AllowRememberConsent")]
	public bool AllowRememberConsent { get; set; }

	/// <summary>
	/// This property indicates whether the identity token should always 
	/// include user claims.
	/// </summary>
	[Required]
	[Display(ShortName = "AllowIncludeUserClaimsInIdToken")]
	public bool AlwaysIncludeUserClaimsInIdToken { get; set; } = false;

	/// <summary>
	/// This property indicates whether client claims should always be sent.
	/// </summary>
	[Required]
	[Display(ShortName = "AllowSendClientClaims")]
	public bool AlwaysSendClientClaims { get; set; } = false;

	/// <summary>
	/// This property indicates the lifetime of an authorization code.
	/// </summary>
	[Display(ShortName = "AuthorizationCodeLifetime")]
	public int AuthorizationCodeLifetime { get; set; }

	/// <summary>
	/// This property indicates whether or not the user's session id should 
	/// be sent to the BackChannelLogoutUri.
	/// </summary>
	[Display(ShortName = "BackChannelLogoutSessionRequired")]
	public bool BackChannelLogoutSessionRequired { get; set; }

	/// <summary>
	/// This property contains the logout URI for HTTP back-channel based 
	/// logout.
	/// </summary>
	[Display(ShortName = "BackChannelLogoutURI")]
	public EditUriVM BackChannelLogoutUri { get; set; } = null!;

	/// <summary>
	/// This property indicates the backchannel authentication request
	/// lifetime, in seconds.
	/// </summary>
	[Display(ShortName = "CIBALifetime")]
	public int? CibaLifetime { get; set; }

	/// <summary>
	/// This property contains a list of claims for the client.
	/// </summary>
	[Display(ShortName = "Claims")]
	public List<ClientClaimVM> Claims { get; set; } = new();

	/// <summary>
	/// This property contains the client claims prefix for the client.
	/// </summary>
	[MaxLength(Globals.Models.Clients.ClientClaimsPrefixLength)]
	[Display(ShortName = "ClientClaimsPrefix")]
	public string ClientClaimsPrefix { get; set; } = null!;

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
	/// This property contains a list of secrets for the client.
	/// </summary>
	[Display(ShortName = "ClientSecrets")]
	public List<ClientSecretVM> ClientSecrets { get; set; } = new();

	/// <summary>
	/// This property contains the URI for the client.
	/// </summary>
	[MaxLength(Globals.Models.Clients.ClientUriLength)]
	[Display(ShortName = "ClientURI")]
	public string ClientUri { get; set; } = null!;

	/// <summary>
	/// This property indicates the lifetime of user consent.
	/// </summary>
	[Display(ShortName = "ConsentLifetime")]
	public int? ConsentLifetime { get; set; }

	/// <summary>
	/// This property indicates whether or not the client's token lifetime
	/// (e.g. refresh tokens) will be tied to the user's session lifetime.
	/// </summary>
	[Display(ShortName = "CoordinateLifetimeWithUserSession")]
	public bool? CoordinateLifetimeWithUserSession { get; set; }

	/// <summary>
	/// This property contains the description for the client.
	/// </summary>
	[MaxLength(Globals.Models.Clients.DescriptionLength)]
	[Display(ShortName = "Description")]
	public string Description { get; set; } = null!;

	/// <summary>
	/// This property indicates the device code lifetime.
	/// </summary>
	[Display(ShortName = "DeviceCodeLifetime")]
	public int DeviceCodeLifetime { get; set; }

	/// <summary>
	/// This property indicates whether or not the client is enabled.
	/// </summary>
	[Display(ShortName = "Enabled")]
	public bool Enabled { get; set; }

	/// <summary>
	/// This property indicates whether or not local logins are allowed.
	/// </summary>
	[Display(ShortName = "EnableLocalLogin")]
	public bool EnableLocalLogin { get; set; }

	/// <summary>
	/// This property indicates whether or not the user's session id should 
	/// be sent to the FrontChannelLogoutUri.
	/// </summary>
	[Display(ShortName = "FrontChannelLogoutSessionRequired")]
	public bool FrontChannelLogoutSessionRequired { get; set; }

	/// <summary>
	/// This property contains a front channel logout URI for the client.
	/// </summary>
	[Display(ShortName = "FrontChannelLogoutUri")]
	public EditUriVM FrontChannelLogoutUri { get; set; } = new();

	/// <summary>
	/// This property indicates which external identity providers can be
	/// used with this client.
	/// </summary>
	[Display(ShortName = "IdentityProviderRestrictions")]
	public List<EditProviderVM> IdentityProviderRestrictions { get; set; } = new();

	/// <summary>
	/// This property contains the lifetime of an identity token, for the client.
	/// </summary>
	[Required]
	[Display(ShortName = "IdentityTokenLifetime")]
	public int IdentityTokenLifetime { get; set; }

	/// <summary>
	/// This property indicates whether or not JWT access tokens should
	/// include an identifier.
	/// </summary>
	[Display(ShortName = "IncludeJWTId")]
	public bool IncludeJwtId { get; set; }

	/// <summary>
	/// This property indicates the view-model has unsaved changes.
	/// </summary>
	public bool IsDirty { get; set; }

	/// <summary>
	/// This property contains the URI for the client consent screen.
	/// </summary>
	[MaxLength(Globals.Models.Clients.LogoUriLength)]
	[Display(ShortName = "LogoURI")]
	public string LogoUri { get; set; } = null!;

	/// <summary>
	/// This property indicates the salt value to use in pair-wise subjectId
	/// generation, for users of this client.
	/// </summary>
	[Display(ShortName = "PairWiseSubjectSalt")]
	public string PairWiseSubjectSalt { get; set; } = null!;

	/// <summary>
	/// This property indicates the backchannel polling interval, in 
	/// seconds.
	/// </summary>
	[Display(ShortName = "PollingInterval")]
	public int? PollingInterval { get; set; }

	/// <summary>
	/// This property contains a list of post logout redirect URIs for the client.
	/// </summary>
	[Display(ShortName = "PostLogoutUris")]
	public List<EditUriVM> PostLogoutRedirectUris { get; set; } = new();
	
	/// <summary>
	/// This property contains any custom properties for the client.
	/// </summary>
	[Display(ShortName = "Properties")]
	public List<EditPropertyVM> Properties { get; set; } = new();

	/// <summary>
	/// This property contains the protocol type for the client.
	/// </summary>
	[Display(ShortName = "ProtocolType")]
	public string ProtocolType { get; set; } = null!;

	/// <summary>
	/// This property contains a list of redirect URIs for the client.
	/// </summary>
	[Display(ShortName = "RedirectUris")]
	public List<EditUriVM> RedirectUris { get; set; } = new();

	/// <summary>
	/// This property contains the expiration of a refresh token, for the client.
	/// </summary>
	[Required]
	[Display(ShortName = "RefreshTokenExpiration")]
	public TokenExpiration RefreshTokenExpiration { get; set; }

	/// <summary>
	/// This property contains the refresh token usage, for the client.
	/// </summary>
	[Required]
	[Display(ShortName = "RefreshTokenUsage")]
	public TokenUsage RefreshTokenUsage { get; set; }

	/// <summary>
	/// This property indicates whether or not the client is required
	/// to know one or more secrets.
	/// </summary>
	[Display(ShortName = "RequireClientSecret")]
	public bool RequireClientSecret { get; set; }

	/// <summary>
	/// This property indicates whether or not a consent screen is required
	/// for the client.
	/// </summary>
	[Display(ShortName = "RequireConsent")]
	public bool RequireConsent { get; set; }

	/// <summary>
	/// This property indicates whether or not PKCE is required for the client.
	/// </summary>
	[Display(ShortName = "RequirePKCE")]
	public bool RequirePkce { get; set; }

	/// <summary>
	/// This property indicates whether or not the client is required
	/// to send signed requests only.
	/// </summary>
	[Display(ShortName = "RequiredRequestObject")]
	public bool RequireRequestObject { get; set; }

	/// <summary>
	/// This property contains the sliding refresh token lifetime, 
	/// in seconds, for the client.
	/// </summary>
	[Required]
	[Display(ShortName = "SlidingRefreshTokenLifetime")]
	public int SlidingRefreshTokenLifetime { get; set; }

	/// <summary>
	/// This property indicates whether an access token should be updated
	/// on a refresh token.
	/// </summary>
	[Display(ShortName = "UpdateAccessTokenClaimsOnRefresh")]
	public bool UpdateAccessTokenClaimsOnRefresh { get; set; }

	/// <summary>
	/// This property indicates the type of device flow user code.
	/// </summary>
	[Display(ShortName = "UserCodeType")]
	public string UserCodeType { get; set; } = null!;

	/// <summary>
	/// This property indicates the maximum duration (in seconds) since
	/// to last time the user authenticated.
	/// </summary>
	[Display(ShortName = "UserSSOLifetime")]
	public int? UserSsoLifetime { get; set; }

	/// <summary>
	/// This property contains a list of valid grant type combinations.
	/// </summary>
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
	public List<string> ValidScopes { get; set; } = new();

	#endregion
}
