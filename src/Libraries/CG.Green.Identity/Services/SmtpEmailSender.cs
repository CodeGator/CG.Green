
namespace CG.Green.Services;

/// <summary>
/// This class is an SMTP implementation of the <see cref="IEmailSender"/>
/// interface.
/// </summary>
public class SmtpEmailSender : IEmailSender
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
    /// This field contains the client for this service.
    /// </summary>
    internal protected readonly System.Net.Mail.SmtpClient _smtpClient = null!;

    /// <summary>
    /// This field contains the options for this service.
    /// </summary>
    internal protected readonly ILogger<SmtpEmailSender> _logger = null!;

    #endregion

    // *******************************************************************
    // Constructors.
    // *******************************************************************

    #region Constructors

    /// <summary>
    /// This constructor creates a new instance of the <see cref="SmtpEmailSender"/>
    /// class.
    /// </summary>
    /// <param name="smtpClient">The SMTP client to use with this service.</param>
    /// <param name="options">The options for this service.</param>
    /// <param name="logger">The logger to use with this service.</param>
    public SmtpEmailSender(
        System.Net.Mail.SmtpClient smtpClient,
        IOptions<GreenIdentityOptions> options,
        ILogger<SmtpEmailSender> logger
        )
    {
        // Validate the arguments before attempting to use them.
        Guard.Instance().ThrowIfNull(smtpClient, nameof(smtpClient))
            .ThrowIfNull(options, nameof(options))
            .ThrowIfNull(logger, nameof(logger));

        // Save the reference(s).
        _smtpClient = smtpClient;
        _options = options.Value.Email;
        _logger = logger;
    }

    #endregion

    // *******************************************************************
    // Public methods.
    // *******************************************************************

    #region Public methods

    /// <inheritdoc/>
    public virtual Task SendEmailAsync(
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

            // Log what we are about to do.
            _logger.LogDebug(
                "Creating a System.Net.Mail.MailMessage object"
                );

            // Create the .NET wrapper.
            var dotNetMessage = new System.Net.Mail.MailMessage()
            {
                From = new System.Net.Mail.MailAddress(
                    _options?.From ?? ""
                    ),
                Subject = subject,
                Body = htmlMessage,
                IsBodyHtml = true
            };

            // Log what we are about to do.
            _logger.LogDebug(
                "Setting To address(es) on the mail object"
                );

            // Set the target address(es).
            foreach (var to in email.Split(';'))
            {
                if (!string.IsNullOrEmpty(to))
                {
                    dotNetMessage.To.Add(to);
                }
            }

            // Send the message.
            _smtpClient.Send(dotNetMessage);
        }
        catch (Exception ex)
        {
            // Log what happened.
            _logger.LogError(
                ex,
                "Failed to send an email."
                );
        }

        // Return the task.
        return Task.CompletedTask;
    }

    #endregion
}
