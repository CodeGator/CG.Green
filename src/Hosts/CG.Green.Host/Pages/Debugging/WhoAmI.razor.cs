
namespace CG.Green.Host.Pages.Debugging;

/// <summary>
/// This class is the code-behind for the <see cref="WhoAmI"/> page.
/// </summary>
public partial class WhoAmI
{
    // *******************************************************************
    // Fields.
    // *******************************************************************

    #region Fields

    /// <summary>
    /// This field contains a reference to breadcrumbs for the view.
    /// </summary>
    internal protected readonly List<BreadcrumbItem> _crumbs = new()
    {
        new BreadcrumbItem("Home", href: "/"),
        new BreadcrumbItem("Debugging", href: "/debugging", disabled: true),
        new BreadcrumbItem("WhoAmI", href: "/debugging/whoami")
    };

    #endregion

    // *******************************************************************
    // Properties.
    // *******************************************************************

    #region Properties

    /// <summary>
    /// This property contains the HTTP context accessor.
    /// </summary>
    [Inject]
    protected IHttpContextAccessor HttpContextAccessor { get; set; } = null!;

    #endregion
}
