
namespace CG.Green.Host.Pages.Admin.Roles;

/// <summary>
/// This class is the code-behind for the <see cref="Index"/> page.
/// </summary>
public partial class Index
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
        new BreadcrumbItem("Roles", href: "/admin/roles")
    };

    /// <summary>
    /// This field contains the list of roles.
    /// </summary>
    internal protected List<EditRoleVM> _roles = new();

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
    protected ILogger<Index> Logger { get; set; } = null!;

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
    /// This method is called to create a new role.
    /// </summary>
    /// <returns>A task to perform the operation.</returns>
    protected async Task OnCreateAsync()
    {
        try
        {
            // Sanity check the model.
            if (_roles is null)
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
                { "Roles", _roles },
                { "Model", new EditRoleVM() }
            };

            // Log what we are about to do.
            Logger.LogDebug(
                "Creating new dialog."
                );

            // Create the dialog.
            var dialog = DialogService.Show<NewDialog>(
                "Create Role",
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
            var model = (EditRoleVM)result.Data;

            // The model needs a PK here.
            model.Id = $"{Guid.NewGuid():N}";

            // Create the role in the backend.
            await GreenApi.Roles.CreateAsync(
                new GreenRole()
                {
                    Id = model.Id,
                    Name = model.Name
                },
                UserName
                );

            // Create the role in the UI.
            _roles.Insert(0, model);
        }
        catch (Exception ex)
        {
            // Log what happened.
            Logger.LogError(
                ex.GetBaseException(),
                "Failed to add a role!"
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
    /// This method edits the given role.
    /// </summary>
    /// <param name="role">The role to use for the operation.</param>
    /// <returns>A task to perform the operation.</returns>
    protected async Task OnEditAsync(
        EditRoleVM role
        )
    {
        // Log what we are about to do.
        Logger.LogDebug(
            "Navigating to the role detail page."
            );

        // Go to the detail page.
        NavigationManager.NavigateTo(
            $"/admin/roles/detail/{Uri.EscapeDataString(role.Id)}"
            );
    }

    // *******************************************************************

    /// <summary>
    /// This method is called to create a new role.
    /// </summary>
    /// <param name="role">The role to use for the operation.</param>
    /// <returns>A task to perform the operation.</returns>
    protected async Task OnDeleteAsync(
        EditRoleVM role
        )
    {
        try
        {
            // Sanity check the model.
            if (_roles is null)
            {
                return; // Nothing to do!
            }

            // Prompt the user.
            var result = await DialogService.ShowMessageBox(
                title: Globals.Caption,
                markupMessage: new MarkupString("This will delete the role " +
                $"<b>'{role.Name}'</b> <br /> <br /> Are you <i>sure</i> " +
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
                "Deleting role from the backend"
                );

            // Delete the role from the backend.
            await GreenApi.Roles.DeleteAsync(
                new GreenRole() 
                { 
                    Id = role.Id,
                    Name = role.Name
                },
                UserName
                );

            // Log what we are about to do.
            Logger.LogDebug(
                "Deleting role from the UI"
                );

            // Remove the role from the UI.
            _roles.Remove(role);
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
    /// This method adapts the <see cref="FilterFunc"/> method for use with 
    /// a <see cref="MudTable{T}"/> control.
    /// </summary>
    /// <param name="element">The element to use for the operation.</param>
    /// <returns><c>true</c> if a match was found; <c>false</c> otherwise.</returns>
    protected bool FilterFunc1(EditRoleVM element) =>
        FilterFunc(element, _gridSearchString);

    // *******************************************************************

    /// <summary>
    /// This method performs a search of the roles.
    /// </summary>
    /// <param name="element">The element to uses for the operation.</param>
    /// <param name="searchString">The search string to use for the operation.</param>
    /// <returns><c>true</c> if a match was found; <c>false</c> otherwise.</returns>
    protected bool FilterFunc(
        EditRoleVM element,
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
        return false;
    }

    #endregion

    // *******************************************************************
    // Private methods.
    // *******************************************************************

    #region Private methods

    /// <summary>
    /// This method loads roles for the page.
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

            // Get rid of any old roles.
            _roles.Clear();

            // Log what we are about to do.
            Logger.LogDebug(
                "Fetching roles from the API."
                );

            // Get the list of roles.
            _roles = (await GreenApi.Roles.FindAllAsync())
                .Select(x => new EditRoleVM() 
                { 
                    Id = x.Id,
                    Name = x.Name ?? "" 
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
