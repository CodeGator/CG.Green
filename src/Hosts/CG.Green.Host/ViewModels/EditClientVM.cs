
namespace CG.Green.Host.ViewModels;

/// <summary>
/// This class is a view-model for editing a client.
/// </summary>
public class EditClientVM
{
    // *******************************************************************
    // Properties.
    // *******************************************************************

    #region Properties

    /// <summary>
    /// This property indicates the lifetime of a refresh token.
    /// </summary>
    [Display(Name ="Absolute Refresh Token Lifetime")]
    public int AbsoluteRefreshTokenLifetime { get; set; }

    /// <summary>
    /// This property indicates the lifetime of an access token.
    /// </summary>
    [Display(Name = "Access Token Lifetime")]
    public int AccessTokenLifetime { get; set; }

    /// <summary>
    /// This property indicates whether or not the access token is a 
    /// reference token or a self contained JWT token.
    /// </summary>
    [Display(Name = "Access Token Type")]
    public AccessTokenType AccessTokenType { get; set; }

    /// <summary>
    /// This property indicates whether or not access tokens
    /// </summary>
    [Display(Name = "Allow Access Tokens Via Browser")]
    public bool AllowAccessTokensViaBrowser { get; set; }

    /// <summary>
    /// This property indicates the allowed CORS origins for javascript 
    /// clients.
    /// </summary>
    [Display(Name = "Allowed CORS Origins")]
    public List<_Wrapper> AllowedCorsOrigins { get; set; } = new();

    /// <summary>
    /// This property contains the allowed grant types for the client.
    /// </summary>
    [Display(Name = "Allowed Grant Types")]
    public AllowedGrantTypes AllowedGrantTypes { get; set; }

    /// <summary>
    /// This property contains the algorithms to use for signing tokens.
    /// </summary>
    [Display(Name = "Allowed Identity Token Signing Algorithms")]
    public List<_Wrapper> AllowedIdentityTokenSigningAlgorithms { get; set; } = new();

    /// <summary>
    /// This property contains the api scopes that the client is allowed
    /// to request.
    /// </summary>
    [Display(Name = "Allowed Scopes")]
    public List<_Wrapper> AllowedScopes { get; set; } = new();

    /// <summary>
    /// This property indicates whether or not to allow offline access.
    /// </summary>
    [Display(Name = "Allow Offline Access")]
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
    [Display(Name = "Allow Remember Consent")]
    public bool AllowRememberConsent { get; set; }

    /// <summary>
    /// This property indicates whether or not the user's claims should 
    /// always be added to the id token, instead of requiring the client to
    /// use the user info endpoint.
    /// </summary>
    [Display(Name = "Always Include User Claims in Id Token")]
    public bool AlwaysIncludeUserClaimsInIdToken { get; set; }

    /// <summary>
    /// This property indicates whether or not client claims should always
    /// be included in access tokens.
    /// </summary>
    [Display(Name = "Always Send Client Claims")]
    public bool AlwaysSendClientClaims { get; set; }

    /// <summary>
    /// This property indicates the lifetime of an authorization code.
    /// </summary>
    [Display(Name = "Authorization Code Lifetime")]
    public int AuthorizationCodeLifetime { get; set; }

    /// <summary>
    /// This property indicates whether or not the user's session id should 
    /// be sent to the BackChannelLogoutUri.
    /// </summary>
    [Display(Name = "Back Channel Logout Session Required")]
    public bool BackChannelLogoutSessionRequired { get; set; }

    /// <summary>
    /// This property contains the logout URI for HTTP back-channel based 
    /// logout.
    /// </summary>
    [Display(Name = "Back Channel Logout URI")]
    public string BackChannelLogoutUri { get; set; } = null!;

    /// <summary>
    /// This property indicates the backchannel authentication request
    /// lifetime, in seconds.
    /// </summary>
    [Display(Name = "CIBA Lifetime")]
    public int? CibaLifetime { get; set; }

    /// <summary>
    /// This property indicates which external identity providers can be
    /// used with this client.
    /// </summary>
    [Display(Name = "Claims")]
    public List<EditClaimVM> Claims { get; set; } = new();

    /// <summary>
    /// This property indicates the value to prefix client claims with.
    /// </summary>
    [Display(Name = "Client Claims Prefix")]
    public string ClientClaimsPrefix { get; set; } = null!;

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
    /// This property contains the secrets for the client.
    /// </summary>
    [Display(Name = "Client Secrets")]
    public List<EditSecretVM> ClientSecrets { get; set; } = new();

    /// <summary>
    /// This property contains the URI for the client.
    /// </summary>
    [Required]
    [MaxLength(Globals.Models.Clients.ClientUriLength)]
    [Display(Name = "Client URI")]
    public string ClientUri { get; set; } = null!;

    /// <summary>
    /// This property indicates the lifetime of user consent.
    /// </summary>
    [Display(Name = "Consent Lifetime")]
    public int? ConsentLifetime { get; set; }

    /// <summary>
    /// This property indicates whether or not the client's token lifetime
    /// (e.g. refresh tokens) will be tied to the user's session lifetime.
    /// </summary>
    [Display(Name = "Coordinate Lifetime with User Session")]
    public bool? CoordinateLifetimeWithUserSession { get; set; }

    /// <summary>
    /// This property contains the description for the client.
    /// </summary>
    [MaxLength(Globals.Models.Clients.DescriptionLength)]
    [Display(Name = "Description")]
    public string Description { get; set; } = null!;

    /// <summary>
    /// This property indicates the device code lifetime.
    /// </summary>
    [Display(Name = "Device Code Lifetime")]
    public int DeviceCodeLifetime { get; set; }

    /// <summary>
    /// This property indicates whether or not the client is enabled.
    /// </summary>
    [Display(Name = "Enabled")]
    public bool Enabled { get; set; }

    /// <summary>
    /// This property indicates whether or not local logins are allowed.
    /// </summary>
    [Display(Name = "Enable Local Login")]
    public bool EnableLocalLogin { get; set; }

    /// <summary>
    /// This property indicates whether or not the user's session id should 
    /// be sent to the FrontChannelLogoutUri.
    /// </summary>
    [Display(Name = "Description")]
    public bool FrontChannelLogoutSessionRequired { get; set; }

    /// <summary>
    /// This property contains the logout URI for HTTP front-channel based 
    /// logout.
    /// </summary>
    [Display(Name = "Front Channel Logout URI")]
    public string FrontChannelLogoutUri { get; set; } = null!;

    /// <summary>
    /// This property indicates which external identity providers can be
    /// used with this client.
    /// </summary>
    [Display(Name = "Identity Provider Restrictions")]
    public List<_Wrapper> IdentityProviderRestrictions { get; set; } = new();

    /// <summary>
    /// This property indicates the lifetime of a token, in seconds.
    /// </summary>
    [Display(Name = "Identity Token Lifetime")]
    public int IdentityTokenLifetime { get; set; }

    /// <summary>
    /// This property indicates whether or not JWT access tokens should
    /// include an identifier.
    /// </summary>
    [Display(Name = "Include JWT Id")]
    public bool IncludeJwtId { get; set; }

    /// <summary>
    /// This property contains the URI for the client.
    /// </summary>
    [Required]
    [MaxLength(Globals.Models.Clients.LogoUriLength)]
    [Display(Name = "Logo URI")]
    public string LogoUri { get; set; } = null!;

    /// <summary>
    /// This property indicates the salt value to use in pair-wise subjectId
    /// generation, for users of this client.
    /// </summary>
    [Display(Name = "Pair Wise Subject Salt")]
    public string PairWiseSubjectSalt { get; set; } = null!;

    /// <summary>
    /// This property indicates the backchannel polling interval, in 
    /// seconds.
    /// </summary>
    [Display(Name = "Polling Interval")]
    public int? PollingInterval { get; set; }

    /// <summary>
    /// This property contains allowed URIs to redirect to, after logout.
    /// </summary>
    [Display(Name = "Post Logout Redirect URIs")]
    public List<_Wrapper> PostLogoutRedirectUris { get; set; } = new();

    /// <summary>
    /// This property indicates the custom properties for the client.
    /// </summary>
    [Display(Name = "Properties")]
    public List<EditPropertyVM> Properties { get; set; } = new();

    /// <summary>
    /// This property contains the protocol type for the client.
    /// </summary>
    [Display(Name = "Protocol Type")]
    public string ProtocolType { get; set; } = null!;

    /// <summary>
    /// This property contains the allowed URIs to return tokens or authorization
    /// code to.
    /// </summary>
    [Display(Name = "Redirect URIs")]
    public List<_Wrapper> RedirectUris { get; set; } = new();

    /// <summary>
    /// This property indicates whether or not the access token will expire
    /// on a fixed point in time (specified by the AbsoluteRefreshTokenLifetime
    /// property)
    /// </summary>
    [Display(Name = "Refresh Token Expiration")]
    public TokenExpiration RefreshTokenExpiration { get; set; } 

    /// <summary>
    /// This property indicates whether or not refresh token handle will stay
    /// the same when refreshing tokens. 
    /// </summary>
    [Display(Name = "Refresh Token Usage")]
    public TokenUsage RefreshTokenUsage { get; set; }

    /// <summary>
    /// This property indicates whether or not the client requires a secret.
    /// </summary>
    [Display(Name = "Require Client Secret")]
    public bool RequireClientSecret { get; set; }

    /// <summary>
    /// This property indicates whether or not the client requires consent.
    /// </summary>
    [Display(Name = "Require Consent")]
    public bool RequireConsent { get; set; }

    /// <summary>
    /// This property indicates whether or not the client required PKCE.
    /// </summary>
    [Display(Name = "Require PKCE")]
    public bool RequirePkce { get; set; }

    /// <summary>
    /// This property indicates whether or not the client must use a request
    /// object on authorize requests.
    /// </summary>
    [Display(Name = "Require Request Object")]
    public bool RequireRequestObject { get; set; }

    /// <summary>
    /// This property indicates the lifetime of a sliding refresh token.
    /// </summary>
    [Display(Name = "Sliding Refresh Token Lifetime")]
    public int SlidingRefreshTokenLifetime { get; set; }

    /// <summary>
    /// This property indicates whether or not the access token (and its claims)
    /// should be updated on a refresh token request.
    /// </summary>
    [Display(Name = "Update Access Token Claims on Refresh")]
    public bool UpdateAccessTokenClaimsOnRefresh { get; set; }

    /// <summary>
    /// This property indicates the type of device flow user code.
    /// </summary>
    [Display(Name = "User Code Type")]
    public string UserCodeType { get; set; } = null!;

    /// <summary>
    /// This property indicates the maximum duration (in seconds) since
    /// to last time the user authenticated.
    /// </summary>
    [Display(Name = "User SSO lifetime")]
    public int? UserSsoLifetime { get; set; }

    #endregion

    // *******************************************************************
    // Public methods.
    // *******************************************************************

    #region Public methods

    /// <summary>
    /// This method converts the given Duende client to a <see cref="EditClientVM"/>
    /// object.
    /// </summary>
    /// <param name="client">The Duende client to use for the operation.</param>
    /// <returns>A <see cref="EditClientVM"/> object.</returns>
    public static EditClientVM FromDuende(
        Client client
        )
    {
        var obj = new EditClientVM()
        {
            AbsoluteRefreshTokenLifetime = client.AbsoluteRefreshTokenLifetime,
            AccessTokenLifetime = client.AccessTokenLifetime,
            AccessTokenType = client.AccessTokenType,
            AllowAccessTokensViaBrowser = client.AllowAccessTokensViaBrowser,
            AllowedCorsOrigins = client.AllowedCorsOrigins.Select(x => new _Wrapper() { Value = x }).ToList(),
            AllowedGrantTypes = client.AllowedGrantTypes.ToAllowedGrantTypes(),
            AllowedIdentityTokenSigningAlgorithms = client.AllowedIdentityTokenSigningAlgorithms.Select(x => new _Wrapper() { Value = x }).ToList(),
            AllowedScopes = client.AllowedScopes.Select(x => new _Wrapper() { Value = x }).ToList(),
            AllowOfflineAccess = client.AllowOfflineAccess,
            AllowPlainTextPkce = client.AllowPlainTextPkce,
            AllowRememberConsent = client.AllowRememberConsent,
            AlwaysIncludeUserClaimsInIdToken = client.AlwaysIncludeUserClaimsInIdToken,
            AlwaysSendClientClaims = client.AlwaysSendClientClaims,
            AuthorizationCodeLifetime = client.AuthorizationCodeLifetime,
            BackChannelLogoutSessionRequired = client.BackChannelLogoutSessionRequired,
            BackChannelLogoutUri = client.BackChannelLogoutUri,
            CibaLifetime = client.CibaLifetime,
            Claims = client.Claims.Select(x => new EditClaimVM()
            {
                ClaimType = x.Type,
                ClaimValue = x.Value,
            }).ToList(),
            ClientClaimsPrefix = client.ClientClaimsPrefix,
            ClientId = client.ClientId,
            ClientName = client.ClientName,
            ClientSecrets = client.ClientSecrets.Select(x => new EditSecretVM()
            {
                Type = x.Type,
                Value = x.Value,
                Description = x.Description,
                Expiration = x.Expiration,
                IsHashed = true  // Always hashed coming from Duende
            }).ToList(),
            ClientUri = client.ClientUri,
            ConsentLifetime = client.ConsentLifetime,
            CoordinateLifetimeWithUserSession = client.CoordinateLifetimeWithUserSession,
            Description = client.Description,
            DeviceCodeLifetime = client.DeviceCodeLifetime,
            Enabled = client.Enabled,
            EnableLocalLogin = client.EnableLocalLogin,
            FrontChannelLogoutSessionRequired = client.FrontChannelLogoutSessionRequired,
            FrontChannelLogoutUri = client.FrontChannelLogoutUri,
            IdentityProviderRestrictions = client.IdentityProviderRestrictions.Select(x => new _Wrapper() { Value = x }).ToList(),
            IdentityTokenLifetime = client.IdentityTokenLifetime,
            IncludeJwtId = client.IncludeJwtId,
            LogoUri = client.LogoUri,
            PairWiseSubjectSalt = client.PairWiseSubjectSalt,
            PollingInterval = client.PollingInterval,
            PostLogoutRedirectUris = client.PostLogoutRedirectUris.Select(x => new _Wrapper() { Value = x }).ToList(),
            Properties = client.Properties.Select(x => new EditPropertyVM()
            {
                Key = x.Key,
                Value = x.Value,
            }).ToList(),
            ProtocolType = client.ProtocolType,
            RedirectUris = client.RedirectUris.Select(x => new _Wrapper() { Value = x }).ToList(),
            RefreshTokenExpiration = client.RefreshTokenExpiration,
            RefreshTokenUsage = client.RefreshTokenUsage,
            RequireClientSecret = client.RequireClientSecret,
            RequireConsent = client.RequireConsent,
            RequirePkce = client.RequirePkce,
            RequireRequestObject = client.RequireRequestObject,
            SlidingRefreshTokenLifetime = client.SlidingRefreshTokenLifetime,
            UpdateAccessTokenClaimsOnRefresh = client.UpdateAccessTokenClaimsOnRefresh,
            UserCodeType = client.UserCodeType,
            UserSsoLifetime = client.UserSsoLifetime
        };
        return obj;
    }

    // *******************************************************************

    /// <summary>
    /// This method converts the object to a Duende <see cref="Client"/>
    /// object.
    /// </summary>
    /// <returns>A <see cref="Client"/> object.</returns>
    public Client ToDuende()
    {
        var obj = new Client()
        {
            AbsoluteRefreshTokenLifetime = AbsoluteRefreshTokenLifetime,
            AccessTokenLifetime = AccessTokenLifetime,
            AccessTokenType = AccessTokenType,
            AllowAccessTokensViaBrowser = AllowAccessTokensViaBrowser,
            AllowedCorsOrigins = AllowedCorsOrigins.Select(x => x.Value).ToList(),
            AllowedGrantTypes = AllowedGrantTypes.FromAllowedGrantTypes(),
            AllowedIdentityTokenSigningAlgorithms = AllowedIdentityTokenSigningAlgorithms.Select(x => x.Value).ToList(),
            AllowedScopes = AllowedScopes.Select(x => x.Value).ToList(),
            AllowOfflineAccess = AllowOfflineAccess,
            AllowPlainTextPkce = AllowPlainTextPkce,
            AllowRememberConsent = AllowRememberConsent,
            AlwaysIncludeUserClaimsInIdToken = AlwaysIncludeUserClaimsInIdToken,
            AlwaysSendClientClaims = AlwaysSendClientClaims,
            AuthorizationCodeLifetime = AuthorizationCodeLifetime,
            BackChannelLogoutSessionRequired = BackChannelLogoutSessionRequired,
            BackChannelLogoutUri = BackChannelLogoutUri,
            CibaLifetime = CibaLifetime,
            Claims = Claims.Select(x => new ClientClaim()
            {
                Type = x.ClaimType,
                Value = x.ClaimValue
            }).ToList(),
            ClientClaimsPrefix = ClientClaimsPrefix,
            ClientId = ClientId,
            ClientName = ClientName,
            ClientSecrets = ClientSecrets.Select(x => new Secret()
            {
                Type = x.Type,
                Value = x.Value,
                Description = x.Description,
                Expiration = x.Expiration,  
            }).ToList(),
            ClientUri = ClientUri,
            ConsentLifetime = ConsentLifetime,
            CoordinateLifetimeWithUserSession = CoordinateLifetimeWithUserSession,
            Description = Description,
            DeviceCodeLifetime = DeviceCodeLifetime,
            Enabled = Enabled,
            EnableLocalLogin = EnableLocalLogin,
            FrontChannelLogoutSessionRequired = FrontChannelLogoutSessionRequired,
            FrontChannelLogoutUri = FrontChannelLogoutUri,
            IdentityProviderRestrictions = IdentityProviderRestrictions.Select(x => x.Value).ToList(),
            IdentityTokenLifetime = IdentityTokenLifetime,
            IncludeJwtId = IncludeJwtId,
            LogoUri = LogoUri,
            PairWiseSubjectSalt = PairWiseSubjectSalt,
            PollingInterval = PollingInterval,
            PostLogoutRedirectUris = PostLogoutRedirectUris.Select(x => x.Value).ToList(),
            Properties = Properties.ToDictionary(x => x.Key, y => y.Value),
            ProtocolType = ProtocolType,
            RedirectUris = RedirectUris.Select(x => x.Value).ToList(),
            RefreshTokenExpiration = RefreshTokenExpiration,
            RefreshTokenUsage = RefreshTokenUsage,
            RequireClientSecret = RequireClientSecret,
            RequireConsent = RequireConsent,
            RequirePkce = RequirePkce,
            RequireRequestObject = RequireRequestObject,
            SlidingRefreshTokenLifetime = SlidingRefreshTokenLifetime,
            UpdateAccessTokenClaimsOnRefresh = UpdateAccessTokenClaimsOnRefresh,
            UserCodeType = UserCodeType,
            UserSsoLifetime = UserSsoLifetime
        };
        return obj;
    }

    #endregion
}
