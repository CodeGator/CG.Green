
namespace CG.Green.Host.Pages.Admin.Roles;

/// <summary>
/// This class is the code-behind for the <see cref="RoleDetail"/> page.
/// </summary>
public partial class RoleDetail
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
    internal protected EditRoleVM? _model;

    /// <summary>
    /// This field indicates the page is loading data.
    /// </summary>
    internal protected bool _isLoading;

    /// <summary>
    /// This field contains a temporary claim, for editing purposes.
    /// </summary>
    internal protected EditClaimVM? _tempClaim;

    #endregion

    // *******************************************************************
    // Properties.
    // *******************************************************************

    #region Properties

    /// <summary>
    /// This property contains the identifier for the claim.
    /// </summary>
    [Parameter]
    public string? RoleId { get; set; }

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
    /// This property contains the name of the current claim, or the word
    /// 'anonymous' if nobody is currently authenticated.
    /// </summary>
    protected string UserName => HttpContextAccessor.HttpContext?.User?.Identity?.Name ?? "anonymous";

    /// <summary>
    /// This property contains the logger for this page.
    /// </summary>
    [Inject]
    protected ILogger<RoleDetail> Logger { get; set; } = null!;

    #endregion

    // *******************************************************************
    // Protected methods.
    // *******************************************************************

    #region Protected methods

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
                new BreadcrumbItem("Roles", href: "/admin/roles"),
                new BreadcrumbItem("Details", href: $"/admin/roles/detail/{RoleId}")
            };

            // Log what we are about to do.
            Logger.LogDebug(
                "Loading data for the page."
                );

            // Load the claim.
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
    /// This method is called to create a new claim.
    /// </summary>
    /// <returns>A task to perform the operation.</returns>
    protected async Task OnCreateClaimAsync()
    {
        try
        {
            // Sanity check the model.
            if (_model is null)
            {
                return; // Nothing to do!
            }

            // Add the new claim.
            _model.AssignedClaims.Add(new EditClaimVM()
            {
                ClaimType = $"claim type {_model.AssignedClaims.Count+1}",
                ClaimValue = $"value {_model.AssignedClaims.Count+1}"
            });
        }
        catch (Exception ex)
        {
            // Log what happened.
            Logger.LogError(
                ex.GetBaseException(),
                "Failed to add a claim!"
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
    /// This method is called to delete a claim.
    /// </summary>
    /// <param name="claim">The claim to use for the operation.</param>
    /// <returns>A task to perform the operation.</returns>
    protected async Task OnDeleteClaimAsync(
        EditClaimVM claim
        )
    {
        try
        {
            // Sanity check the model.
            if (_model is null)
            {
                return; // Nothing to do!
            }

            // Prompt the claim.
            var result = await DialogService.ShowMessageBox(
                title: Globals.Caption,
                markupMessage: new MarkupString("This will delete the claim " +
                $"<b>'{claim.ClaimType}'</b> <br /> <br /> Are you <i>sure</i> " +
                "you want to do that?"),
                noText: "Cancel"
                );

            // Did the user cancel?
            if (result.HasValue && !result.Value)
            {
                return; // Nothing more to do.
            }

            // Log what we are about to do.
            Logger.LogDebug(
                "Deleting a claim"
                );

            // Remove the claim.
            _model.AssignedClaims.Remove(claim);
        }
        catch (Exception ex)
        {
            // Log what happened.
            Logger.LogError(
                ex.GetBaseException(),
                "Failed to delete a claim!"
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
    /// This method is called to cancel an in-progress claim edit.
    /// </summary>
    /// <param name="element">The claim to use for the operation.</param>
    protected void OnEditClaimCancel(object element)
    {
        // Log what we are about to do.
        Logger.LogDebug(
            "Restoring claim from backup"
            );

        // Restore the claim from our backup.
        ((EditClaimVM)element).ClaimType = _tempClaim?.ClaimType ?? "";
        ((EditClaimVM)element).ClaimValue = _tempClaim?.ClaimValue ?? "";

        // Log what we are about to do.
        Logger.LogDebug(
            "Releasing the backup"
            );

        // We don't need this now.
        _tempClaim = null;

        // Log what we are about to do.
        Logger.LogDebug(
            "Forcing a blazor update"
            );

        // Tell blazor to update.
        StateHasChanged();
    }

    // *******************************************************************

    /// <summary>
    /// This method is called to commit an in-progress claim edit.
    /// </summary>
    /// <param name="element">The claim to use for the operation.</param>
    protected void OnEditClaimCommit(object element)
    {
        // Log what we are about to do.
        Logger.LogDebug(
            "Releasing the backup"
            );

        // We don't need this now.
        _tempClaim = null;

        // Log what we are about to do.
        Logger.LogDebug(
            "Forcing a blazor update"
            );

        // Tell blazor to update.
        StateHasChanged();
    }

    // *******************************************************************

    /// <summary>
    /// This method is called to start a claim edit.
    /// </summary>
    /// <param name="element">The claim to use for the operation.</param>
    protected void OnEditClaimStart(object element)
    {
        // Log what we are about to do.
        Logger.LogDebug(
            "Making a claim backup"
            );

        // Copy the claim.
        _tempClaim = new EditClaimVM()
        { 
            ClaimType = ((EditClaimVM)element).ClaimType,
            ClaimValue = ((EditClaimVM)element).ClaimValue
        };

        // Log what we are about to do.
        Logger.LogDebug(
            "Forcing a blazor update"
            );

        // Tell blazor to update.
        StateHasChanged();
    }

    // *******************************************************************

    /// <summary>
    /// This method is called when the caller submits the form.
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
                "Saving the role changes"
                );

            // Update the backend.
            var updatedRole = await GreenApi.Roles.UpdateAsync(
                new GreenRole()
                {
                    Id = _model.Id,
                    Name = _model.Name 
                },
                UserName
                );

            // Log what we are about to do.
            Logger.LogDebug(
                "Saving the role claims"
                );

            // Update the backend.
            await GreenApi.RoleClaims.UpdateAsync(
                updatedRole,
                _model.AssignedClaims.Select(x => new GreenRoleClaim()
                {
                    ClaimType = x.ClaimType,
                    ClaimValue = x.ClaimValue   
                }),
                UserName
                );

            // Log what we are about to do.
            Logger.LogDebug(
                "Showing the snackbar"
                );

            // Tell the world what we did.
            SnackbarService.Add(
                $"Saved changes to role"
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

    #endregion

    // *******************************************************************
    // Private methods.
    // *******************************************************************

    #region Private methods

    /// <summary>
    /// This method loads the data for the page.
    /// </summary>
    /// <returns>A task to perform the operation.</returns>
    private async Task LoadDataAsync()
    {
        try
        {
            // Sanity check the id.
            if (string.IsNullOrEmpty(RoleId))
            {
                _model = null; // no claim!
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
                "Fetching role."
                );

            // Force defaults since we don't have a model yet.
            _model = null;

            // Get the claim.
            var role = await GreenApi.Roles.FindByIdAsync(
                RoleId ?? ""
                );

            // Did we succeed?
            if (role is not null)
            {
                // Look for associated claims.
                var claims = (await GreenApi.RoleClaims.FindByRoleIdAsync(
                    role.Id
                    )).Select(x => new EditClaimVM()
                    {
                        ClaimType = x.ClaimType ?? "",
                        ClaimValue = x.ClaimValue ?? ""
                    }).ToList();

                // Wrap up the model.
                _model = new EditRoleVM()
                {
                    Id = role.Id,
                    Name = role.Name ?? "",
                    AssignedClaims = claims
                };
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
