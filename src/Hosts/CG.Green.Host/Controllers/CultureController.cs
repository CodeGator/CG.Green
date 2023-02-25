
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace CG.Green.Host.Controllers;

/// <summary>
/// This class is an MVC controller for cultures.
/// </summary>
[Route("api/[controller]/[action]")]
public class CultureController : Controller
{
	// *******************************************************************
	// Fields.
	// *******************************************************************

	#region Fields

	/// <summary>
	/// This field contains the logger for this controller.
	/// </summary>
	internal protected readonly ILogger<CultureController> _logger = null!;

	#endregion

	// *******************************************************************
	// Constructors.
	// *******************************************************************

	#region Constructors

	/// <summary>
	/// This constructor creates a new instance of the <see cref="CultureController"/>
	/// </summary>
	/// <param name="logger">The logger to use with this controller.</param>
	public CultureController(
		ILogger<CultureController> logger
		)
	{
		// Validate the parameters before attempting to use them.
		Guard.Instance().ThrowIfNull(logger, nameof(logger));

		// Save the reference(s).
		_logger = logger;
	}

	#endregion

	// *******************************************************************
	// Public methods.
	// *******************************************************************

	#region Public methods

	/// <summary>
	/// This method sets the current culture.
	/// </summary>
	/// <param name="culture">The culture to use for the operation.</param>
	/// <param name="redirectUri">The URI to use for the operation.</param>
	/// <returns>The results of the action.</returns>
	[HttpGet(Name = nameof(Set))]
    public IActionResult Set(
        string culture, 
        string redirectUri
        )
    {
		try
		{
			// Validate the parameters before attempting to use them.
			Guard.Instance().ThrowIfNullOrEmpty(culture, nameof(culture))
				.ThrowIfNullOrEmpty(redirectUri, nameof(redirectUri));

			// Was a culture specified?
			if (culture != null)
			{
				// Set the culture cookie.
				HttpContext.Response.Cookies.Append(
					CookieRequestCultureProvider.DefaultCookieName,
					CookieRequestCultureProvider.MakeCookieValue(
						new RequestCulture(culture, culture)
						)
					);
			}

			// Local redirect to prevent open redirect attacks.
			// https://learn.microsoft.com/en-us/aspnet/core/security/preventing-open-redirects?view=aspnetcore-7.0
			return LocalRedirect(redirectUri);
		}
		catch (Exception ex)
		{
			// Log the error in detail.
			_logger.LogError(
				ex,
				"Failed to set the culture type!"
				);

			// Return an overview of the problem.
			return Problem(
				statusCode: StatusCodes.Status500InternalServerError,
				detail: "The controller failed to set the culture type!"
				);
		}
    }

    #endregion
}
