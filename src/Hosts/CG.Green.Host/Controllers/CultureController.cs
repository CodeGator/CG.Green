
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace CG.Green.Host.Controllers;

/// <summary>
/// This class is an MVC controller for cultures.
/// </summary>
[Route("api/[controller]/[action]")]
public class CultureController : Controller
{
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
    [HttpGet]
    public IActionResult Set(
        string culture, 
        string redirectUri
        )
    {
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

    #endregion
}
