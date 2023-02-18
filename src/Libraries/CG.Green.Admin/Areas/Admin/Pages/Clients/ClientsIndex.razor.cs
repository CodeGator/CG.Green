
namespace CG.Green.Areas.Admin.Pages.Clients;

/// <summary>
/// This class is the code-behind for the <see cref="ClientsIndex"/> page.
/// </summary>
public partial class ClientsIndex
{
    // *******************************************************************
    // Fields.
    // *******************************************************************

    #region Fields

    // <summary>
    /// This field contains a reference to breadcrumbs for the view.
    /// </summary>
    internal protected readonly List<BreadcrumbItem> _crumbs = new()
    {
        new BreadcrumbItem("Home", href: "/"),
        new BreadcrumbItem("Admin", href: "/admin", disabled: true),
        new BreadcrumbItem("Clients", href: "/admin/clients")
    };

    /// <summary>
    /// This field contains the list of clients.
    /// </summary>
    internal protected List<EditClientVM> _clients = new();

    /// <summary>
    /// This field indicates when the page is loading data.
    /// </summary>
    internal protected bool _isLoading;

    /// <summary>
    /// This field contains the current search string.
    /// </summary>
    protected string _gridSearchString = "";

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
    /// This property contains the MudBalazor theme for this page.
    /// </summary>
    [CascadingParameter(Name = "Theme")]
    public MudTheme Theme { get; set; } = null!;

    /// <summary>
    /// This property contains the auto mapper for this page.
    /// </summary>
    [Inject]
    protected IMapper AutoMapper { get; set; } = null!;

    /// <summary>
    /// This property contains the dialog service for this page.
    /// </summary>
    [Inject]
    protected IDialogService Dialog { get; set; } = null!;

    /// <summary>
    /// This property contains the snackbar service for this page.
    /// </summary>
    [Inject]
    protected ISnackbar Snackbar { get; set; } = null!;

    /// <summary>
    /// This property contains the navigation manager for the page.
    /// </summary>
    [Inject]
    protected NavigationManager Navigation { get; set; } = null!;

    /// <summary>
    /// This property contains the HTTP context accessor.
    /// </summary>
    [Inject]
    protected IHttpContextAccessor HttpContext { get; set; } = null!;

    /// <summary>
    /// This property contains the name of the current user, or the word
    /// 'anonymous' if nobody is currently authenticated.
    /// </summary>
    protected string UserName => HttpContext.HttpContext?.User?.Identity?.Name ?? "anonymous";

    /// <summary>
    /// This property contains the logger for this page.
    /// </summary>
    [Inject]
    protected ILogger<ClientsIndex> Logger { get; set; } = null!;

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

            // Load the data.
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
                "Failed to fetch clients!"
                );

            // Tell the world what happened.
            await Dialog.ShowErrorBox(ex);
        }
    }

    // *******************************************************************

    /// <summary>
    /// This method adapts the <see cref="FilterFunc"/> method for use with 
    /// a <see cref="MudTable{T}"/> control.
    /// </summary>
    /// <param name="element">The element to use for the operation.</param>
    /// <returns><c>true</c> if a match was found; <c>false</c> otherwise.</returns>
    protected bool FilterFunc1(EditClientVM element) =>
        FilterFunc(element, _gridSearchString);

    // *******************************************************************

    /// <summary>
    /// This method performs a search of the clients.
    /// </summary>
    /// <param name="element">The element to uses for the operation.</param>
    /// <param name="searchString">The search string to use for the operation.</param>
    /// <returns><c>true</c> if a match was found; <c>false</c> otherwise.</returns>
    protected bool FilterFunc(
        EditClientVM element,
        string searchString
        )
    {
        if (string.IsNullOrWhiteSpace(searchString))
        {
            return true;
        }
        if ((element.ClientName ?? "").Contains(
            searchString,
            StringComparison.OrdinalIgnoreCase)
            )
        {
            return true;
        }
        if ((element.ClientId ?? "").Contains(
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
    /// This method adds a new client.
    /// </summary>
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
                { "Model", new NewClientVM() }
            };

            // Log what we are about to do.
            Logger.LogDebug(
                "Creating new dialog."
                );

            // Create the dialog.
            var dialog = Dialog.Show<NewClientDialog>(
                "Create Client",
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
            var model = (NewClientVM)result.Data;

            // Log what we are about to do.
            Logger.LogDebug(
                "Creating the new client."
                );

            // Create the new client.
            var newClient = await GreenApi.Clients.CreateAsync(
                AutoMapper.Map<Client>(model),
                UserName
                );

            // Log what we are about to do.
            Logger.LogDebug(
                "Navigating to the client detail page."
                );

            // Go to the detail page.
            Navigation.NavigateTo(
                $"/admin/clients/detail/{Uri.EscapeDataString(newClient.ClientId)}"
                );
        }
        catch (Exception ex)
        {
            // Did the user try to reuse an existing client id?
            if (ex.GetBaseException().Message.Contains("duplicate key"))
            {
                // Log what we are about to do.
                Logger.LogDebug(
                    "Showing the message box."
                    );

                // Prompt the user.
                await Dialog.ShowMessageBox(
                    title: "Something broke!",
                    message: "That client id is already in use!"
                    );
                return;
            }

            // Log what happened.
            Logger.LogError(
                ex.GetBaseException(),
                "Failed to create a client!"
                );

            // Log what we are about to do.
            Logger.LogDebug(
                "Showing the message box"
                );

            // Tell the world what happened.
            await Dialog.ShowErrorBox(ex);
        }
    }

    // *******************************************************************

    /// <summary>
    /// This method deletes the given client.
    /// </summary>
    /// <param name="client">The client to use for the operation.</param>
    /// <returns>A task to perform the operation.</returns>
    protected async Task OnDeleteAsync(
        EditClientVM client
        )
    {
        try
        {
            // Log what we are about to do.
            Logger.LogDebug(
                "Prompting the caller."
                );

            // Prompt the user.
            var result = await Dialog.ShowMessageBox(
                title: Globals.Caption,
                markupMessage: new MarkupString("This will delete the client " +
                $"<b>'{client.ClientId}'</b> <br /> <br /> Are you <i>sure</i> " +
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
                "Deleting a client."
                );

            // Delete the client.
            await GreenApi.Clients.DeleteAsync(
                AutoMapper.Map<Client>(client),
                UserName
                );

            // Log what we are about to do.
            Logger.LogDebug(
                "Loading data for the page."
                );

            // Load the clients.
            await LoadDataAsync();
        }
        catch (Exception ex)
        {
            // Log what happened.
            Logger.LogError(
                ex.GetBaseException(),
                "Failed to delete a client!"
                );

            // Log what we are about to do.
            Logger.LogDebug(
                "Showing the message box"
                );

            // Tell the world what happened.
            await Dialog.ShowErrorBox(ex);
        }
    }

    // *******************************************************************

    /// <summary>
    /// This method edits the given client.
    /// </summary>
    /// <param name="client">The client to use for the operation.</param>
    /// <returns>A task to perform the operation.</returns>
    protected Task OnEditAsync(
        EditClientVM client
        )
    {
        // Log what we are about to do.
        Logger.LogDebug(
            "Navigating to the client detail page."
            );

        // Go to the detail page.
        Navigation.NavigateTo(
            $"/admin/clients/detail/{Uri.EscapeDataString(client.ClientId)}"
            );

        // Return the task.
        return Task.CompletedTask;
    }

    // *******************************************************************

    /// <summary>
    /// This method disables the given client.
    /// </summary>
    /// <param name="client">The client to use for the operation.</param>
    /// <returns>A task to perform the operation.</returns>
    protected async Task DisableClientAsync(
        EditClientVM client
        )
    {
        try
        {
            // Can we take a shortcut?
            if (!client.Enabled)
            {
                return;
            }

            // Clear the flag.
            client.Enabled = false;

            // Update the client.
            await GreenApi.Clients.UpdateAsync(
                AutoMapper.Map<Client>(client),
                UserName
                );
        }
        catch (Exception ex)
        {
            // Log what happened.
            Logger.LogError(
                ex.GetBaseException(),
                "Failed to disable a client!"
                );

            // Log what we are about to do.
            Logger.LogDebug(
                "Showing the message box"
                );

            // Tell the world what happened.
            await Dialog.ShowErrorBox(ex);
        }
    }

    // *******************************************************************

    /// <summary>
    /// This method enables the given client.
    /// </summary>
    /// <param name="client">The client to use for the operation.</param>
    /// <returns>A task to perform the operation.</returns>
    protected async Task EnableClientAsync(
        EditClientVM client
        )
    {
        try
        {
            // Can we take a shortcut?
            if (client.Enabled)
            {
                return;
            }

            // Set the flag.
            client.Enabled = true;

            // Update the client.
            await GreenApi.Clients.UpdateAsync(
                AutoMapper.Map<Client>(client),
                UserName
                );
        }
        catch (Exception ex)
        {
            // Log what happened.
            Logger.LogError(
                ex.GetBaseException(),
                "Failed to enable a client!"
                );

            // Log what we are about to do.
            Logger.LogDebug(
                "Showing the message box"
                );

            // Tell the world what happened.
            await Dialog.ShowErrorBox(ex);
        }
    }

    #endregion

    // *******************************************************************
    // Private methods.
    // *******************************************************************

    #region Private methods

    /// <summary>
    /// This method loads data for the page.
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
                "Clearing any old clients."
                );

            // Get rid of any old clients.
            _clients.Clear();

            // Log what we are about to do.
            Logger.LogDebug(
                "Fetching clients from the API."
                );

            // Get the list of clients.
            _clients = (await GreenApi.Clients.FindAllAsync())
                .Select(x => AutoMapper.Map<EditClientVM>(x))
                    .ToList();
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
