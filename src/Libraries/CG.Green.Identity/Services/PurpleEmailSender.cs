
namespace CG.Green.Services;

/// <summary>
/// This class is a CG.Purple implementation of the <see cref="IEmailSender"/>
/// interface.
/// </summary>
public class PurpleEmailSender : IEmailSender
{
    // *******************************************************************
    // Fields.
    // *******************************************************************

    #region Fields

    /// <summary>
    /// This field contains the email options for this service.
    /// </summary>
    internal protected readonly EmailOptions? _options;

    /// <summary>
    /// This field contains the CG.Purple client factory for this service.
    /// </summary>
    internal protected readonly IPurpleHttpClientFactory _purpleClientFactory = null!;

    /// <summary>
    /// This field contains the options for this service.
    /// </summary>
    internal protected readonly ILogger<PurpleEmailSender> _logger = null!;

    #endregion

    // *******************************************************************
    // Constructors.
    // *******************************************************************

    #region Constructors

    /// <summary>
    /// This constructor creates a new instance of the <see cref="PurpleEmailSender"/>
    /// class.
    /// </summary>
    /// <param name="purpleClientFactory">The factory for creating CG.Purple
    /// REST clients for this service.</param>
    /// <param name="options">The options for this service.</param>
    /// <param name="logger">The logger to use with this service.</param>
    public PurpleEmailSender(
        IPurpleHttpClientFactory purpleClientFactory,
        IOptions<GreenIdentityOptions> options,
        ILogger<PurpleEmailSender> logger
        )
    {
        // Validate the arguments before attempting to use them.
        Guard.Instance().ThrowIfNull(purpleClientFactory, nameof(purpleClientFactory))
            .ThrowIfNull(options, nameof(options))
            .ThrowIfNull(logger, nameof(logger));

        // Save the reference(s).
        _purpleClientFactory = purpleClientFactory;
        _options = options.Value.Email;
        _logger = logger;
    }

    #endregion

    // *******************************************************************
    // Public methods.
    // *******************************************************************

    #region Public methods

    /// <inheritdoc/>
    public virtual async Task SendEmailAsync(
        string email, 
        string subject, 
        string htmlMessage
        )
    {
        // Validate the arguments before attempting to use them.
        Guard.Instance().ThrowIfNullOrEmpty(email, nameof(email))
            .ThrowIfNullOrEmpty(subject, nameof(subject))
            .ThrowIfNullOrEmpty(htmlMessage, nameof(htmlMessage));

        try
        {
            // Log what we are about to do.
            _logger.LogDebug(
                "Checking the 'From' address"
                );

            // Sanity check the from address.
            if (string.IsNullOrEmpty(_options?.From))
            {
                // Panic!!
                throw new InvalidOperationException(
                    "Unable to send email with missing 'From' address!"
                    );
            }

            // Create the purple client.
            var client = _purpleClientFactory.CreateClient();

            // Send the message.
            await client.SendMailMessageAsync(
                new Purple.Clients.ViewModels.MailStorageRequest()
                {
                    From = _options?.From ?? "",
                    To = email,
                    Subject = subject,
                    Body = htmlMessage,
                    IsHtml = true   
                });
        }
        catch (Exception ex)
        {
            // Log what happened.
            _logger.LogError(
                ex,
                "Failed to send an email."
                );
        }
    }

    #endregion
}
