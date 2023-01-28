
namespace CG.Green.Host.Pages.Admin.Users;

/// <summary>
/// This class is the code-behind for the <see cref="Detail"/> page.
/// </summary>
public partial class Detail
{
    // *******************************************************************
    // Fields.
    // *******************************************************************

    #region Fields

    /// <summary>
    /// This field contains a reference to breadcrumbs for the page.
    /// </summary>
    internal protected List<BreadcrumbItem> _crumbs = new();

    /// <summary>
    /// This field contains the model for the page.
    /// </summary>
    internal protected EditUserVM? _model;

    /// <summary>
    /// This field indicates the page is loading data.
    /// </summary>
    internal protected bool _isLoading;

    /// <summary>
    /// This field contains the list of available user roles.
    /// </summary>
    internal protected List<string> _roles = new();

    #endregion

    // *******************************************************************
    // Properties.
    // *******************************************************************

    #region Properties

    /// <summary>
    /// This property contains the identifier for the user.
    /// </summary>
    [Parameter]
    public string? UserId { get; set; }

    /// <summary>
    /// This property contains the dialog service for this page.
    /// </summary>
    [Inject]
    protected IDialogService DialogService { get; set; } = null!;

    /// <summary>
    /// This property contains the snackbar service for the page.
    /// </summary>
    [Inject]
    protected ISnackbar SnackbarService { get; set; } = null!;

    /// <summary>
    /// This property contains the HTTP context accessor.
    /// </summary>
    [Inject]
    protected IHttpContextAccessor HttpContextAccessor { get; set; } = null!;

    /// <summary>
    /// This property contains the navigation manager for the page.
    /// </summary>
    [Inject]
    protected NavigationManager NavigationManager { get; set; } = null!;

    /// <summary>
    /// This property contains the green API for the page.
    /// </summary>
    [Inject]
    protected IGreenApi GreenApi { get; set; } = null!;

    /// <summary>
    /// This property contains the name of the current user, or the word
    /// 'anonymous' if nobody is currently authenticated.
    /// </summary>
    protected string UserName => HttpContextAccessor.HttpContext?.User?.Identity?.Name ?? "anonymous";

    /// <summary>
    /// This property contains the logger for this page.
    /// </summary>
    [Inject]
    protected ILogger<Detail> Logger { get; set; } = null!;

    #endregion

    // *******************************************************************
    // Protected methods.
    // *******************************************************************

    #region Protected methods

    /// <summary>
    /// This method is called to create a new claim.
    /// </summary>
    /// <returns>A task to perform the operation.</returns>
    protected async Task OnCreateClaimAsync()
    {

    }

    // *******************************************************************

    /// <summary>
    /// This method is called to delete a claim.
    /// </summary>
    /// <param name="claim">The claim to use for the operation.</param>
    /// <returns>A task to perform the operation.</returns>
    protected async Task OnDeleteClaimAsync(
        EditClaimVM claim
        )
    {

    }

    // *******************************************************************

    /// <summary>
    /// This method is called to initialize the page.
    /// </summary>
    /// <returns>A task to perform the operation.</returns>
    protected override async Task OnInitializedAsync()
    {
        try
        {
            // Log what we are about to do.
            Logger.LogDebug(
                "Creating bread crumbs."
                );

            // Create the bread crumbs for the page.
            _crumbs = new()
            {
                new BreadcrumbItem("Home", href: "/"),
                new BreadcrumbItem("Admin", href: "/admin", disabled: true),
                new BreadcrumbItem("Users", href: "/admin/users"),
                new BreadcrumbItem("Details", href: $"/admin/users/detail/{UserId}")
            };

            // Log what we are about to do.
            Logger.LogDebug(
                "Loading data for the page."
                );

            // Load the user.
            await LoadDataAsync();

            // Give the base class a chance.
            await base.OnInitializedAsync();
        }
        catch (Exception ex)
        {
            // Log what happened.
            Logger.LogError(
                ex.GetBaseException(),
                "Failed to initialize the page."
                );

            // Log what we are about to do.
            Logger.LogDebug(
                "Showing the message box"
                );

            // Tell the world what happened.
            await DialogService.ShowErrorBox(ex);
        }
    }

    // *******************************************************************

    /// <summary>
    /// This method is called when the user submits the form.
    /// </summary>
    /// <returns>A task to perform the operation.</returns>
    protected async Task OnValidSubmitAsync()
    {
        try
        {
            // Sanity check the model.
            if (_model is null)
            {
                return; // Nothing to do!
            }

            // Log what we are about to do.
            Logger.LogDebug(
                "Building the user from the model"
                );

            // Update the original use with changes.
            _model.User.UserName = _model.UserName;
            _model.User.Email = _model.Email;
            _model.User.EmailConfirmed = _model.EmailConfirmed;
            _model.User.LockoutEnabled = _model.LockoutEnabled;
            _model.User.TwoFactorEnabled = _model.TwoFactorEnabled;
            _model.User.AccessFailedCount = _model.AccessFailedCount;

            // Log what we are about to do.
            Logger.LogDebug(
                "Saving the claim changes"
                );

            // Save the assigned claims.
            await GreenApi.UserClaims.UpdateAsync(
                _model.User,
                _model.AssignedClaims.Select(x => new GreenUserClaim()
                {
                    ClaimType = x.ClaimType,
                    ClaimValue = x.ClaimValue
                }),
                UserName
                );

            // Log what we are about to do.
            Logger.LogDebug(
                "Saving the role changes"
                );

            // Save the assigned roles.
            await GreenApi.UserRoles.UpdateAsync(
                _model.User,
                _model.AssignedRoles,
                UserName
                );

            // Log what we are about to do.
            Logger.LogDebug(
                "Saving the user changes"
                );

            // Save the model.
            await GreenApi.Users.UpdateAsync(
                _model.User,
                UserName
                );

            // Log what we are about to do.
            Logger.LogDebug(
                "Showing the snackbar"
                );

            // Tell the world what we did.
            SnackbarService.Add(
                $"Saved changes to user",
                Severity.Info
                );
        }
        catch (Exception ex)
        {
            // Log what happened.
            Logger.LogError(
                ex.GetBaseException(),
                "Failed to update the model."
                );

            // Log what we are about to do.
            Logger.LogDebug(
                "Showing the message box"
                );

            // Tell the world what happened.
            await DialogService.ShowErrorBox(ex);
        }
    }

    // *******************************************************************

    /// <summary>
    /// This method loads the data for the page.
    /// </summary>
    /// <returns>A task to perform the operation.</returns>
    private async Task LoadDataAsync()
    {
        try
        {
            // Sanity check the id.
            if (string.IsNullOrEmpty(UserId))
            {
                _model = null; // no user!
                return;
            }

            // Log what we are about to do.
            Logger.LogDebug(
                "Marking the page as busy."
                );

            // We're now officially busy.
            _isLoading = true;

            // Log what we are about to do.
            Logger.LogDebug(
                "Fetching user."
                );

            // Force defaults since we don't have a model yet.
            _model = null;
            _roles.Clear();

            // Get the user.
            var user = await GreenApi.Users.FindByIdAsync(
                UserId ?? ""
                );

            // Did we succeed?
            if (user is not null)
            {
                // Create the model.
                _model = new EditUserVM()
                {
                    User = user,
                    UserName = user.UserName ?? "",
                    Email = user.Email ?? "",
                    EmailConfirmed = user.EmailConfirmed,
                    TwoFactorEnabled = user.TwoFactorEnabled,
                    AccessFailedCount = user.AccessFailedCount,
                    LockoutEnabled = user.LockoutEnabled,
                };

                // Save the list of available roles.
                _roles = (await GreenApi.Roles.FindAllAsync()).Select(x => x.Name ?? "").ToList();

                // Save the list of user roles.
                _model.AssignedRoles = (await GreenApi.UserRoles.FindByUserIdAsync(
                    user.Id ?? ""
                    )).ToList();

                // Save the list of user claims.
                _model.AssignedClaims = (await GreenApi.UserClaims.FindByUserIdAsync(
                    user.Id ?? ""
                    )).Select(x => new EditClaimVM()
                    {
                        ClaimType = x.ClaimType ?? "",
                        ClaimValue = x.ClaimValue ?? ""
                    }).ToList();
            }
        }
        finally
        {
            // Log what we are about to do.
            Logger.LogDebug(
                "Marking the page as idle."
                );

            // We're now officially idle.
            _isLoading = false;
        }
    }

    #endregion
}
