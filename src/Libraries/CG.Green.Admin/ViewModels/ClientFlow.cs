
namespace CG.Green.Admin.ViewModels;

/// <summary>
/// This enumeration lists the various client flow types.
/// </summary>
public enum ClientFlow
{
    /// <summary>
    /// A custom client flow.
    /// </summary>
    Custom = 4,

    /// <summary>
    /// A machine 2 machine flow.
    /// </summary>
    M2M = 2,

    /// <summary>
    /// A native application flow.
    /// </summary>
    Native = 3,

    /// <summary>
    /// A single page web application flow.
    /// </summary>
    Spa = 1,

    /// <summary>
    /// A traditional web application flow.
    /// </summary>
    Web = 0
}
