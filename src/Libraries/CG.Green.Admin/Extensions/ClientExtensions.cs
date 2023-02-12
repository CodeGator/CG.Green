
namespace Duende.IdentityServer.Models;

/// <summary>
/// This class contains extension methods related to the <see cref="Client"/>
/// type.
/// </summary>
internal static class ClientExtensions
{
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
        this Client client
        )
    {
        var obj = new EditClientVM()
        {
            //AbsoluteRefreshTokenLifetime = client.AbsoluteRefreshTokenLifetime,
            //AccessTokenLifetime = client.AccessTokenLifetime,
            //AccessTokenType = client.AccessTokenType,
            //AllowAccessTokensViaBrowser = client.AllowAccessTokensViaBrowser,
            //AllowedCorsOrigins = client.AllowedCorsOrigins.Select(x => new _Wrapper() { Value = x }).ToList(),
            //AllowedGrantTypes = client.AllowedGrantTypes.ToAllowedGrantTypes(),
            //AllowedIdentityTokenSigningAlgorithms = client.AllowedIdentityTokenSigningAlgorithms.Select(x => new _Wrapper() { Value = x }).ToList(),
            //AllowedScopes = client.AllowedScopes.Select(x => new _Wrapper() { Value = x }).ToList(),
            //AllowOfflineAccess = client.AllowOfflineAccess,
            //AllowPlainTextPkce = client.AllowPlainTextPkce,
            //AllowRememberConsent = client.AllowRememberConsent,
            //AlwaysIncludeUserClaimsInIdToken = client.AlwaysIncludeUserClaimsInIdToken,
            //AlwaysSendClientClaims = client.AlwaysSendClientClaims,
            //AuthorizationCodeLifetime = client.AuthorizationCodeLifetime,
            //BackChannelLogoutSessionRequired = client.BackChannelLogoutSessionRequired,
            //BackChannelLogoutUri = client.BackChannelLogoutUri,
            //CibaLifetime = client.CibaLifetime,
            //Claims = client.Claims.Select(x => new EditClaimVM()
            //{
            //    ClaimType = x.Type,
            //    ClaimValue = x.Value,
            //}).ToList(),
            //ClientClaimsPrefix = client.ClientClaimsPrefix,
            ClientId = client.ClientId,
            ClientName = client.ClientName,
            //ClientSecrets = client.ClientSecrets.Select(x => new EditSecretVM()
            //{
            //    Type = x.Type,
            //    Value = x.Value,
            //    Description = x.Description,
            //    Expiration = x.Expiration,
            //    IsHashed = true  // Always hashed coming from Duende
            //}).ToList(),
            //ClientUri = client.ClientUri,
            //ConsentLifetime = client.ConsentLifetime,
            //CoordinateLifetimeWithUserSession = client.CoordinateLifetimeWithUserSession,
            //Description = client.Description,
            //DeviceCodeLifetime = client.DeviceCodeLifetime,
            Enabled = client.Enabled,
            //EnableLocalLogin = client.EnableLocalLogin,
            //FrontChannelLogoutSessionRequired = client.FrontChannelLogoutSessionRequired,
            //FrontChannelLogoutUri = client.FrontChannelLogoutUri,
            //IdentityProviderRestrictions = client.IdentityProviderRestrictions.Select(x => new _Wrapper() { Value = x }).ToList(),
            //IdentityTokenLifetime = client.IdentityTokenLifetime,
            //IncludeJwtId = client.IncludeJwtId,
            //LogoUri = client.LogoUri,
            //PairWiseSubjectSalt = client.PairWiseSubjectSalt,
            //PollingInterval = client.PollingInterval,
            //PostLogoutRedirectUris = client.PostLogoutRedirectUris.Select(x => new _Wrapper() { Value = x }).ToList(),
            //Properties = client.Properties.Select(x => new EditPropertyVM()
            //{
            //    Key = x.Key,
            //    Value = x.Value,
            //}).ToList(),
            //ProtocolType = client.ProtocolType,
            //RedirectUris = client.RedirectUris.Select(x => new _Wrapper() { Value = x }).ToList(),
            //RefreshTokenExpiration = client.RefreshTokenExpiration,
            //RefreshTokenUsage = client.RefreshTokenUsage,
            //RequireClientSecret = client.RequireClientSecret,
            //RequireConsent = client.RequireConsent,
            //RequirePkce = client.RequirePkce,
            //RequireRequestObject = client.RequireRequestObject,
            //SlidingRefreshTokenLifetime = client.SlidingRefreshTokenLifetime,
            //UpdateAccessTokenClaimsOnRefresh = client.UpdateAccessTokenClaimsOnRefresh,
            //UserCodeType = client.UserCodeType,
            //UserSsoLifetime = client.UserSsoLifetime
        };
        return obj;
    }

    #endregion
}
