﻿
namespace CG.Green.Host.Pages.Admin.Resources;

/// <summary>
/// This class is the code-behind for the <see cref="ResourceDetail"/> page.
/// </summary>
public partial class ResourceDetail
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
    internal protected EditResourceVM? _model;

    /// <summary>
    /// This field indicates the page is loading data.
    /// </summary>
    internal protected bool _isLoading;

    /// <summary>
    /// This field contains a temporary claim, for editing purposes.
    /// </summary>
    internal protected _Wrapper? _tempClaim;

    #endregion

    // *******************************************************************
    // Properties.
    // *******************************************************************

    #region Properties

    /// <summary>
    /// This property contains the name for the identity resource.
    /// </summary>
    [Parameter]
    public string? ResourceName { get; set; }

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
    protected ILogger<ResourceDetail> Logger { get; set; } = null!;

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
                new BreadcrumbItem("Resources", href: "/admin/resources"),
                new BreadcrumbItem("Details", href: $"/admin/resources/detail/{Uri.EscapeDataString(ResourceName)}")
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
                "Saving the resource changes"
                );

            // Update the resource in the backend.
            await GreenApi.IdentityResources.UpdateAsync(
                _model.ToDuende(),
                UserName
                );

            // Log what we are about to do.
            Logger.LogDebug(
                "Showing the snackbar"
                );

            // Tell the world what we did.
            SnackbarService.Add(
                $"Saved changes to the resource"
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
    /// This method is called to create a new claim for a resource.
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
    /// This method is called to delete a claim from a resource.
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

            // Delete the resource.
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
            // Sanity check the resource name.
            if (string.IsNullOrEmpty(ResourceName))
            {
                _model = null; // no api resource!
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
                "Fetching api resource."
                );

            // Get the resource.
            var resource = await GreenApi.IdentityResources.FindByNameAsync(
                ResourceName ?? ""
                );

            // Did we succeed?
            if (resource is not null)
            {
                // Log what we are about to do.
                Logger.LogDebug(
                    "Building the model."
                    );

                // Wrap up the model.
                _model = new EditResourceVM()
                {
                    Name = resource.Name ?? "",
                    DisplayName = resource.DisplayName ?? "",
                    Description = resource.Description ?? "",
                    Emphasize = resource.Emphasize,
                    Enabled = resource.Enabled,
                    Properties = resource.Properties,
                    Required = resource.Required,
                    ShowInDiscoveryDocument = resource.ShowInDiscoveryDocument,
                    UserClaims = resource.UserClaims.Select(x =>
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
