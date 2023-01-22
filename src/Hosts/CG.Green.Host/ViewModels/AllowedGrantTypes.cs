namespace CG.Green.Host.ViewModels;

/// <summary>
/// This enumerations lists the valid grant types.
/// </summary>
public enum AllowedGrantTypes
{
    /// <summary>
    /// CIBA grant type.
    /// </summary>
    Ciba,

    /// <summary>
    /// Client credential grant type.
    /// </summary>
    ClientCredentials,

    /// <summary>
    /// code grant type.
    /// </summary>
    Code,

    /// <summary>
    /// Code and client credential grant type.
    /// </summary>
    CodeAndClientCredentials,

    /// <summary>
    /// Device flow grant type.
    /// </summary>
    DeviceFlow,

    /// <summary>
    /// Hybrid grant type.
    /// </summary>
    Hybrid,

    /// <summary>
    /// Hybrid and client credential grant type.
    /// </summary>
    HybridAndClientCredentials,

    /// <summary>
    /// Implicit grant type.
    /// </summary>
    Implicit,

    /// <summary>
    /// Implicit and client credential grant type.
    /// </summary>
    ImplicitAndClientCredentials,

    /// <summary>
    /// Resource owner grant type.
    /// </summary>
    ResourceOwnerPassword,

    /// <summary>
    /// Resource owner and client credential grant type.
    /// </summary>
    ResourceOwnerPasswordAndClientCredentials
}


/// <summary>
/// This class contains extensions for the <see cref="AllowedGrantTypes"/>
/// type.
/// </summary>
public static class AllowedGrantTypesExtensions
{
    // *******************************************************************
    // Public methods.
    // *******************************************************************

    #region Public methods

    /// <summary>
    /// This method converts between a collection of grant type strings 
    /// and a corresponding <see cref="AllowedGrantTypes"/> value.
    /// </summary>
    /// <param name="values">The values to use for the operation.</param>
    /// <returns>A <see cref="AllowedGrantTypes"/> value.</returns>
    public static AllowedGrantTypes ToAllowedGrantTypes(
        this ICollection<string> values
        )
    {
        // Look for the implicit grant.
        if (values.Contains(GrantType.Implicit) &&
            !values.Contains(GrantType.ClientCredentials))
        {
            return AllowedGrantTypes.Implicit;
        }

        // Look for the implicit and client credentials grant.
        if (values.Contains(GrantType.Implicit) &&
            values.Contains(GrantType.ClientCredentials))
        {
            return AllowedGrantTypes.ImplicitAndClientCredentials;
        }

        // Look for the code grant.
        if (values.Contains(GrantType.AuthorizationCode) &&
            !values.Contains(GrantType.ClientCredentials))
        {
            return AllowedGrantTypes.Code;
        }

        // Look for the code and client credentials grant.
        if (values.Contains(GrantType.AuthorizationCode) &&
            values.Contains(GrantType.ClientCredentials))
        {
            return AllowedGrantTypes.CodeAndClientCredentials;
        }

        // Look for the hybrid grant.
        if (values.Contains(GrantType.Hybrid) &&
            !values.Contains(GrantType.ClientCredentials))
        {
            return AllowedGrantTypes.Hybrid;
        }

        // Look for the hybrid and client credentials grant.
        if (values.Contains(GrantType.Hybrid) &&
            values.Contains(GrantType.ClientCredentials))
        {
            return AllowedGrantTypes.HybridAndClientCredentials;
        }

        // Look for the client credentials grant.
        if (values.Contains(GrantType.ClientCredentials))
        {
            return AllowedGrantTypes.ClientCredentials;
        }

        // Look for the resource owner grant.
        if (values.Contains(GrantType.ResourceOwnerPassword) &&
            !values.Contains(GrantType.ClientCredentials))
        {
            return AllowedGrantTypes.ResourceOwnerPassword;
        }

        // Look for the resource owner and client credentials grant.
        if (values.Contains(GrantType.ResourceOwnerPassword) &&
            values.Contains(GrantType.ClientCredentials))
        {
            return AllowedGrantTypes.ResourceOwnerPasswordAndClientCredentials;
        }

        // Look for the device flow grant.
        if (values.Contains(GrantType.DeviceFlow))
        {
            return AllowedGrantTypes.DeviceFlow;
        }

        // Default to the CIBA grant.
        return AllowedGrantTypes.Ciba;
    }

    // *******************************************************************

    /// <summary>
    /// This method converts between a <see cref="AllowedGrantTypes"/>
    /// value and a corresponding collection of Duende grant type strings.
    /// </summary>
    /// <param name="value">The value to use for the operation.</param>
    /// <returns>A collection of <see cref="GrantTypes"/> strings.</returns>
    public static ICollection<string> FromAllowedGrantTypes(
        this AllowedGrantTypes value
        )
    {
        // Map back the Duende grant types.
        switch (value)
        {
            case AllowedGrantTypes.Implicit:
                return GrantTypes.Implicit;
            case AllowedGrantTypes.ImplicitAndClientCredentials:
                return GrantTypes.ImplicitAndClientCredentials;
            case AllowedGrantTypes.Hybrid:
                return GrantTypes.Hybrid;
            case AllowedGrantTypes.HybridAndClientCredentials:
                return GrantTypes.HybridAndClientCredentials;
            case AllowedGrantTypes.Code:
                return GrantTypes.Code;
            case AllowedGrantTypes.CodeAndClientCredentials:
                return GrantTypes.CodeAndClientCredentials;
            case AllowedGrantTypes.DeviceFlow:
                return GrantTypes.DeviceFlow;
            case AllowedGrantTypes.ResourceOwnerPassword:
                return GrantTypes.ResourceOwnerPassword;
            case AllowedGrantTypes.ResourceOwnerPasswordAndClientCredentials:
                return GrantTypes.ResourceOwnerPasswordAndClientCredentials;
            case AllowedGrantTypes.ClientCredentials:
                return GrantTypes.ClientCredentials;
            default:
                return GrantTypes.Ciba;
        }
    }

    #endregion
}
