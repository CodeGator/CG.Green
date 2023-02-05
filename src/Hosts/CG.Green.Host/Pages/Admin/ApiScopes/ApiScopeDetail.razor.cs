
namespace CG.Green.Host.Pages.Admin.ApiScopes;

/// <summary>
/// This class is the code-behind for the <see cref="ApiScopeDetail"/> page.
/// </summary>
public partial class ApiScopeDetail
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
    internal protected EditApiScopeVM? _model;

    /// <summary>
    /// This field indicates the page is loading data.
    /// </summary>
    internal protected bool _isLoading;

    /// <summary>
    /// This field contains a temporary claim, for editing purposes.
    /// </summary>
    internal protected _Wrapper? _tempClaim;

    /// <summary>
    /// This field contains a temporary property, for editing purposes.
    /// </summary>
    internal protected EditPropertyVM? _tempProperty;

    #endregion

    // *******************************************************************
    // Properties.
    // *******************************************************************

    #region Properties

    /// <summary>
    /// This property contains the name for the API scope.
    /// </summary>
    [Parameter]
    public string? ApiScopeName { get; set; }

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
    protected ILogger<ApiScopeDetail> Logger { get; set; } = null!;

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
                new BreadcrumbItem("ApiScopes", href: "/admin/apiscopes"),
                new BreadcrumbItem("Details", href: $"/admin/apiscopes/detail/{Uri.EscapeDataString(ApiScopeName)}")
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
                "Saving the API scope changes"
                );

            // Update the scope in the backend.
            await GreenApi.ApiScopes.UpdateAsync(
                _model.ToDuende(),
                UserName
                );

            // Log what we are about to do.
            Logger.LogDebug(
                "Showing the snackbar"
                );

            // Tell the world what we did.
            SnackbarService.Add(
                $"Saved changes to the API scope"
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
    /// This method is called to create a new claim for an api scope.
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

            // Log what we are about to do.
            Logger.LogDebug(
                "Creating dialog options."
                );

            // Create the dialog options.
            var options = new DialogOptions
            {
                MaxWidth = MaxWidth.Small,
                CloseOnEscapeKey = true,
                FullWidth = true
            };

            // Log what we are about to do.
            Logger.LogDebug(
                "Creating dialog parameters."
                );

            // Create the dialog parameters.
            var parameters = new DialogParameters()
            {
                { "Model", new _Wrapper() }
            };

            // Log what we are about to do.
            Logger.LogDebug(
                "Creating new dialog."
                );

            // Create the dialog.
            var dialog = DialogService.Show<NewClaimDialog>(
                "Create Claim",
                parameters,
                options
                );

            // Get the results of the dialog.
            var result = await dialog.Result;

            // Did the user cancel?
            if (result.Canceled)
            {
                return;
            }

            // Log what we are about to do.
            Logger.LogDebug(
                "Recovering the dialog model."
                );

            // Recover the model.
            var model = (_Wrapper)result.Data;

            // Log what we are about to do.
            Logger.LogDebug(
                "Creating the new claim"
                );

            // Add the new claim.
            _model.UserClaims.Add(model);
        }
        catch (Exception ex)
        {
            // Log what happened.
            Logger.LogError(
                ex.GetBaseException(),
                "Failed to create a claim!"
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
    /// This method is called to delete a claim from an api scope.
    /// </summary>
    /// <param name="claim">The claim to use for the operation.</param>
    /// <returns>A task to perform the operation.</returns>
    protected async Task OnDeleteClaimAsync(
        _Wrapper claim
        )
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
                "Prompting the caller."
                );

            // Prompt the user.
            var result = await DialogService.ShowMessageBox(
                title: Globals.Caption,
                markupMessage: new MarkupString("This will delete the claim " +
                $"<b>'{claim.Value}'</b> <br /> <br /> Are you <i>sure</i> " +
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
                "Deleting a claim."
                );

            // Delete the claim.
            _model.UserClaims.Remove(claim);
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
        ((_Wrapper)element).Value = _tempClaim?.Value ?? "";

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
        _tempClaim = new _Wrapper()
        {
            Value = ((_Wrapper)element).Value
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
    /// This method is called to create a new property.
    /// </summary>
    /// <returns>A task to perform the operation.</returns>
    protected async Task OnCreatePropertyAsync()
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
                "Creating dialog options."
                );

            // Create the dialog options.
            var options = new DialogOptions
            {
                MaxWidth = MaxWidth.Small,
                CloseOnEscapeKey = true,
                FullWidth = true
            };

            // Log what we are about to do.
            Logger.LogDebug(
                "Creating dialog parameters."
                );

            // Create the dialog parameters.
            var parameters = new DialogParameters()
            {
                { "Model", new EditPropertyVM() }
            };

            // Log what we are about to do.
            Logger.LogDebug(
                "Creating new dialog."
                );

            // Create the dialog.
            var dialog = DialogService.Show<NewPropertyDialog>(
                "Create Property",
                parameters,
                options
                );

            // Get the results of the dialog.
            var result = await dialog.Result;

            // Did the user cancel?
            if (result.Canceled)
            {
                return;
            }

            // Log what we are about to do.
            Logger.LogDebug(
                "Recovering the dialog model."
                );

            // Recover the model.
            var model = (EditPropertyVM)result.Data;

            // Log what we are about to do.
            Logger.LogDebug(
                "Creating the new property"
                );

            // Add the new claim.
            _model.Properties.Add(model);
        }
        catch (Exception ex)
        {
            // Log what happened.
            Logger.LogError(
                ex.GetBaseException(),
                "Failed to create a property!"
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
    /// This method is called to delete a property.
    /// </summary>
    /// <param name="property">The property to use for the operation.</param>
    /// <returns>A task to perform the operation.</returns>
    protected async Task OnDeletePropertyAsync(
        EditPropertyVM property
        )
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
                "Prompting the caller."
                );

            // Prompt the user.
            var result = await DialogService.ShowMessageBox(
                title: Globals.Caption,
                markupMessage: new MarkupString("This will delete the property " +
                $"<b>'{property.Key}'</b> <br /> <br /> Are you <i>sure</i> " +
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
                "Deleting a property."
                );

            // Delete the property.
            _model.Properties.Remove(property);
        }
        catch (Exception ex)
        {
            // Log what happened.
            Logger.LogError(
                ex.GetBaseException(),
                "Failed to delete a property!"
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
    /// This method is called to cancel an in-progress property edit.
    /// </summary>
    /// <param name="element">The claim to use for the operation.</param>
    protected void OnEditPropertyCancel(object element)
    {
        // Log what we are about to do.
        Logger.LogDebug(
            "Restoring claim from backup"
            );

        // Restore the property from our backup.
        ((EditPropertyVM)element).Key = _tempProperty?.Key ?? "";
        ((EditPropertyVM)element).Value = _tempProperty?.Value ?? "";

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
    /// This method is called to commit an in-progress property edit.
    /// </summary>
    /// <param name="element">The property to use for the operation.</param>
    protected void OnEditPropertyCommit(object element)
    {
        // Log what we are about to do.
        Logger.LogDebug(
            "Releasing the backup"
            );

        // We don't need this now.
        _tempProperty = null;

        // Log what we are about to do.
        Logger.LogDebug(
            "Forcing a blazor update"
            );

        // Tell blazor to update.
        StateHasChanged();
    }

    // *******************************************************************

    /// <summary>
    /// This method is called to start a property edit.
    /// </summary>
    /// <param name="element">The claim to use for the operation.</param>
    protected void OnEditPropertyStart(object element)
    {
        // Log what we are about to do.
        Logger.LogDebug(
            "Making a property backup"
            );

        // Copy the property.
        _tempProperty = new EditPropertyVM()
        {
            Key = ((EditPropertyVM)element).Key,
            Value = ((EditPropertyVM)element).Value
        };

        // Log what we are about to do.
        Logger.LogDebug(
            "Forcing a blazor update"
            );

        // Tell blazor to update.
        StateHasChanged();
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
            // Sanity check the scope name.
            if (string.IsNullOrEmpty(ApiScopeName))
            {
                _model = null; // no api scope!
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
                "Clearing any previous model."
                );

            // Force defaults since we don't have a model yet.
            _model = null;

            // Log what we are about to do.
            Logger.LogDebug(
                "Fetching api scope."
                );

            // Get the api scope.
            var apiScope = await GreenApi.ApiScopes.FindByNameAsync(
                ApiScopeName ?? ""
                );

            // Did we succeed?
            if (apiScope is not null)
            {
                // Log what we are about to do.
                Logger.LogDebug(
                    "Building the model."
                    );

                // Wrap up the model.
                _model = new EditApiScopeVM()
                {
                    Name = apiScope.Name ?? "",
                    DisplayName = apiScope.DisplayName ?? "",
                    Description = apiScope.Description ?? "",
                    Emphasize = apiScope.Emphasize,
                    Enabled = apiScope.Enabled,
                    Properties = apiScope.Properties.Select(x => 
                        new EditPropertyVM()
                        {
                            Key = x.Key,
                            Value = x.Value,
                        }).ToList(),
                    Required = apiScope.Required,
                    ShowInDiscoveryDocument = apiScope.ShowInDiscoveryDocument,
                    UserClaims = apiScope.UserClaims.Select(x => 
                        new _Wrapper()
                        {
                            Value = x
                        }).ToList()
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
