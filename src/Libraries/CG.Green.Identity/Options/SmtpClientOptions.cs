namespace CG.Green.Identity.Options;

/// <summary>
/// This class contains configuration settings for SMTP
/// email operations.
/// </summary>
public class SmtpClientOptions
{
    // *******************************************************************
    // Properties.
    // *******************************************************************

    #region Properties

    /// <summary>
    /// This property contains the default base address for the remote 
    /// SMTP server host.
    /// </summary>
    [Required]
    public string DefaultBaseAddress { get; set; } = null!;

    /// <summary>
    /// This property contains the optional user name for the client.
    /// </summary>
    public string? UserName { get; set; }

    /// <summary>
    /// This property contains the optional password for the client.
    /// </summary>
    public string? Password { get; set; }

    #endregion
}
