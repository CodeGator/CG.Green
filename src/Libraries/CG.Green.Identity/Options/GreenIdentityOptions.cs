
namespace CG.Green.Identity.Options;

/// <summary>
/// This class contains configuration settings for the identity layer
/// of the <see cref="CG.Green"/> microservice.
/// </summary>
public class GreenIdentityOptions 
{
    // *******************************************************************
    // Properties.
    // *******************************************************************

    #region Properties

    /// <summary>
    /// This property contains optional ASP.NET identity options.
    /// </summary>
    public IdentityOptions? AspNet { get; set; }

    /// <summary>
    /// This property contains optional Duende identity options.
    /// </summary>
    public IdentityServerOptions? Duende { get; set; }

    /// <summary>
    /// This property contains optional settings for email operations.
    /// </summary>
    public EmailOptions? Email { get; set; }

    #endregion
}
