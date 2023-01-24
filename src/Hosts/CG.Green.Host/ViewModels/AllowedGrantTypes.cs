namespace CG.Green.Host.ViewModels;

/// <summary>
/// This enumerations lists the valid grant types, and combinations of grant types.
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
