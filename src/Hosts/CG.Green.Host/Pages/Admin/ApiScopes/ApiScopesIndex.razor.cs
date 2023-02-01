
namespace CG.Green.Host.Pages.Admin.ApiScopes;

/// <summary>
/// This class is the code-behind for the <see cref="ApiScopesIndex"/> page.
/// </summary>
public partial class ApiScopesIndex
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
        new BreadcrumbItem("ApiScopes", href: "/admin/apiscopes")
    };

    /// <summary>
    /// This field contains the list of API scopes.
    /// </summary>
    internal protected List<EditApiScopeVM> _apiScopes = new();

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
    protected ILogger<ApiScopesIndex> Logger { get; set; } = null!;

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
                "Failed to fetch users!"
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
    protected bool FilterFunc1(EditApiScopeVM element) =>
        FilterFunc(element, _gridSearchString);

    // *******************************************************************

    /// <summary>
    /// This method performs a search of the roles.
    /// </summary>
    /// <param name="element">The element to uses for the operation.</param>
    /// <param name="searchString">The search string to use for the operation.</param>
    /// <returns><c>true</c> if a match was found; <c>false</c> otherwise.</returns>
    protected bool FilterFunc(
        EditApiScopeVM element,
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
    /// This method is called to create a new API scope.
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
                { "Model", new NewApiScopeVM() }
            };

            // Log what we are about to do.
            Logger.LogDebug(
                "Creating new dialog."
                );

            // Create the dialog.
            var dialog = DialogService.Show<NewApiScopeDialog>(
                "Create API Scope",
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
            var model = (NewApiScopeVM)result.Data;

            // Log what we are about to do.
            Logger.LogDebug(
                "Creating the new API scope"
                );

            // Create the new API scope.
            var newScope = await GreenApi.ApiScopes.CreateAsync(
                new ApiScope()
                {
                    Name = model.Name
                },
                UserName
                );

            // Log what we are about to do.
            Logger.LogDebug(
                "Navigating to the API scope detail page."
                );

            // Go to the detail page.
            NavigationManager.NavigateTo(
                $"/admin/apiscopes/detail/{Uri.EscapeDataString(newScope.Name)}"
                );
        }
        catch (Exception ex)
        {
            // Log what happened.
            Logger.LogError(
                ex.GetBaseException(),
                "Failed to create an API scope!"
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
    /// This method is called to delete an API scope.
    /// </summary>
    /// <param name="apiScope">The API scope to use for the operation.</param>
    /// <returns>A task to perform the operation.</returns>
    protected async Task OnDeleteAsync(
        EditApiScopeVM apiScope
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
                markupMessage: new MarkupString("This will delete the API scope " +
                $"<b>'{apiScope.Name}'</b> <br /> <br /> Are you <i>sure</i> " +
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
                "Deleting an API scope."
                );

            // Delete the API scope.
            await GreenApi.ApiScopes.DeleteAsync(
                new ApiScope()
                {
                    Name = apiScope.Name
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
                "Failed to delete an API scope!"
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
    /// This method is called to edit an API scope.
    /// </summary>
    /// <param name="apiScope">The API scope to use for the operation.</param>
    /// <returns>A task to perform the operation.</returns>
    protected async Task OnEditAsync(
        EditApiScopeVM apiScope
        )
    {
        // Log what we are about to do.
        Logger.LogDebug(
            "Navigating to the api scope detail page."
            );

        // Go to the detail page.
        NavigationManager.NavigateTo(
            $"/admin/apiscopes/detail/{Uri.EscapeDataString(apiScope.Name)}"
            );
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
                "Clearing any old roles."
                );

            // Get rid of any old API scopes.
            _apiScopes.Clear();

            // Log what we are about to do.
            Logger.LogDebug(
                "Fetching API scopes from the API."
                );

            // Get the list of API scopes.
            _apiScopes = (await GreenApi.ApiScopes.FindAllAsync())
                .Select(x => new EditApiScopeVM()
                {
                    Name = x.Name ?? "",
                    DisplayName = x.DisplayName ?? "",
                    Required = x.Required,
                    Emphasize = x.Emphasize,
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
