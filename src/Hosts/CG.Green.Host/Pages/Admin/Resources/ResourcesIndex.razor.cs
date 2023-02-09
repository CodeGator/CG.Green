﻿
namespace CG.Green.Host.Pages.Admin.Resources;

/// <summary>
/// This class is the code-behind for the <see cref="ResourcesIndex"/> page.
/// </summary>
public partial class ResourcesIndex
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
        new BreadcrumbItem("Admin", href: "/admin", disabled: true),
        new BreadcrumbItem("Resources", href: "/admin/resources")
    };

    /// <summary>
    /// This field contains the list of identity resources.
    /// </summary>
    internal protected List<EditResourceVM> _resources = new();

    /// <summary>
    /// This field indicates when the page is loading data.
    /// </summary>
    internal protected bool _isLoading;

    /// <summary>
    /// This field contains the current search string.
    /// </summary>
    internal protected string _gridSearchString = "";

    #endregion

    // *******************************************************************
    // Properties.
    // *******************************************************************

    #region Properties

    /// <summary>
    /// This property contains the green api for this page.
    /// </summary>
    [Inject]
    protected IGreenApi GreenApi { get; set; } = null!;

    /// <summary>
    /// This property contains the dialog service for this page.
    /// </summary>
    [Inject]
    protected IDialogService DialogService { get; set; } = null!;

    /// <summary>
    /// This property contains the snackbar service for this page.
    /// </summary>
    [Inject]
    protected ISnackbar SnackbarService { get; set; } = null!;

    /// <summary>
    /// This property contains the navigation manager for the page.
    /// </summary>
    [Inject]
    protected NavigationManager NavigationManager { get; set; } = null!;

    /// <summary>
    /// This property contains the HTTP context accessor.
    /// </summary>
    [Inject]
    protected IHttpContextAccessor HttpContextAccessor { get; set; } = null!;

    /// <summary>
    /// This property contains the name of the current user, or the word
    /// 'anonymous' if nobody is currently authenticated.
    /// </summary>
    protected string UserName => HttpContextAccessor.HttpContext?.User?.Identity?.Name ?? "anonymous";

    /// <summary>
    /// This property contains the logger for this page.
    /// </summary>
    [Inject]
    protected ILogger<ResourcesIndex> Logger { get; set; } = null!;

    #endregion

    // *******************************************************************
    // Protected methods.
    // *******************************************************************

    #region Protected methods

    /// <summary>
    /// This method initializes the page.
    /// </summary>
    /// <returns>A task to perform the operation.</returns>
    protected override async Task OnInitializedAsync()
    {
        try
        {
            // Log what we are about to do.
            Logger.LogDebug(
                "Loading data for the page."
                );

            // Load the users.
            await LoadDataAsync();

            // Log what we are about to do.
            Logger.LogDebug(
                "Initializing the page."
                );

            // Initialize the page.
            await base.OnInitializedAsync();
        }
        catch (Exception ex)
        {
            // Log what happened.
            Logger.LogError(
                ex.GetBaseException(),
                "Failed to fetch identity resources!"
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
    /// This method adapts the <see cref="FilterFunc"/> method for use with 
    /// a <see cref="MudTable{T}"/> control.
    /// </summary>
    /// <param name="element">The element to use for the operation.</param>
    /// <returns><c>true</c> if a match was found; <c>false</c> otherwise.</returns>
    protected bool FilterFunc1(EditResourceVM element) =>
        FilterFunc(element, _gridSearchString);

    // *******************************************************************

    /// <summary>
    /// This method performs a search of the roles.
    /// </summary>
    /// <param name="element">The element to uses for the operation.</param>
    /// <param name="searchString">The search string to use for the operation.</param>
    /// <returns><c>true</c> if a match was found; <c>false</c> otherwise.</returns>
    protected bool FilterFunc(
        EditResourceVM element,
        string searchString
        )
    {
        if (string.IsNullOrWhiteSpace(searchString))
        {
            return true;
        }
        if ((element.Name ?? "").Contains(
            searchString,
            StringComparison.OrdinalIgnoreCase)
            )
        {
            return true;
        }
        if ((element.DisplayName ?? "").Contains(
            searchString,
            StringComparison.OrdinalIgnoreCase)
            )
        {
            return true;
        }
        return false;
    }

    // *******************************************************************

    /// <summary>
    /// This method is called to create a new identity resource.
    /// </summary>
    /// <returns>A task to perform the operation.</returns>
    protected async Task OnCreateAsync()
    {
        try
        {
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
                { "Model", new NewResourceVM() }
            };

            // Log what we are about to do.
            Logger.LogDebug(
                "Creating new dialog."
                );

            // Create the dialog.
            var dialog = DialogService.Show<NewResourceDialog>(
                "Create Resource",
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
            var model = (NewResourceVM)result.Data;

            // Log what we are about to do.
            Logger.LogDebug(
                "Creating the new identity resource"
                );

            // Create the new identity resource.
            var resource = await GreenApi.IdentityResources.CreateAsync(
                new IdentityResource()
                {
                    Name = model.Name
                },
                UserName
                );

            // Log what we are about to do.
            Logger.LogDebug(
                "Navigating to the identity resource detail page."
                );

            // Go to the detail page.
            NavigationManager.NavigateTo(
                $"/admin/resources/detail/{Uri.EscapeDataString(resource.Name)}"
                );
        }
        catch (Exception ex)
        {
            // Log what happened.
            Logger.LogError(
                ex.GetBaseException(),
                "Failed to create an identity resource!"
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
    /// This method is called to delete an identity resource.
    /// </summary>
    /// <returns>A task to perform the operation.</returns>
    protected async Task OnDeleteAsync(
        EditResourceVM resource
        )
    {
        try
        {
            // Log what we are about to do.
            Logger.LogDebug(
                "Prompting the caller."
                );

            // Prompt the user.
            var result = await DialogService.ShowMessageBox(
                title: Globals.Caption,
                markupMessage: new MarkupString("This will delete the resource " +
                $"<b>'{resource.Name}'</b> <br /> <br /> Are you <i>sure</i> " +
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
                "Deleting an identity resource."
                );

            // Delete the resource.
            await GreenApi.IdentityResources.DeleteAsync(
                new IdentityResource()
                {
                    Name = resource.Name
                },
                UserName
                );

            // Log what we are about to do.
            Logger.LogDebug(
                "Loading data for the page."
                );

            // Load the API scopes.
            await LoadDataAsync();
        }
        catch (Exception ex)
        {
            // Log what happened.
            Logger.LogError(
                ex.GetBaseException(),
                "Failed to delete an identity resource!"
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
    /// This method is called to edit an identity resource.
    /// </summary>
    /// <returns>A task to perform the operation.</returns>
    protected Task OnEditAsync(
        EditResourceVM resource
        )
    {
        // Log what we are about to do.
        Logger.LogDebug(
            "Navigating to the resource detail page."
            );

        // Go to the detail page.
        NavigationManager.NavigateTo(
            $"/admin/resources/detail/{Uri.EscapeDataString(resource.Name)}"
            );

        // Return the task.
        return Task.CompletedTask;
    }

    #endregion

    // *******************************************************************
    // Private methods.
    // *******************************************************************

    #region Private methods

    /// <summary>
    /// This method loads API scopes for the page.
    /// </summary>
    /// <returns>A task to perform the operation.</returns>
    private async Task LoadDataAsync()
    {
        try
        {
            // Log what we are about to do.
            Logger.LogDebug(
                "Marking the page as busy."
                );

            // We're now officially busy.
            _isLoading = true;

            // Log what we are about to do.
            Logger.LogDebug(
                "Clearing any old identity resources."
                );

            // Get rid of any old identity resources.
            _resources.Clear();

            // Log what we are about to do.
            Logger.LogDebug(
                "Fetching identity resources from the API."
                );

            // Get the list of identity resources.
            _resources = (await GreenApi.IdentityResources.FindAllAsync())
                .Select(x => new EditResourceVM()
                {
                   Name = x.Name,
                   DisplayName = x.DisplayName,
                   Required = x.Required,
                   Emphasize = x.Emphasize
                }).ToList();
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
