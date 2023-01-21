
namespace CG.Green.Identity.Options;

/// <summary>
/// This class contains configuration settings for email operations 
/// of the <see cref="CG.Green"/> microservice.
/// </summary>
public class EmailOptions
{
    // *******************************************************************
    // Properties.
    // *******************************************************************

    #region Properties

    /// <summary>
    /// This property contains the 'From' address, for the client.
    /// </summary>
    [Required]
    [EmailAddress]
    public string From { get; set; } = null!;

    /// <summary>
    /// This property contains the name of the default email strategy.
    /// </summary>
    [Required]
    public string DefaultStrategy { get; set; } = null!;

    /// <summary>
    /// This property contains options SMTP client options.
    /// </summary>
    public SmtpClientOptions? Smtp { get; set; }

    /// <summary>
    /// This property contains options CG.Purple client options.
    /// </summary>
    public PurpleClientOptions? Purple { get; set; }

    #endregion
}
