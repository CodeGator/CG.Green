﻿
namespace CG.Green.Host.Pages.Admin.Clients;

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
        new BreadcrumbItem("Clients", href: "/admin/clients")
    };

    /// <summary>
    /// This field contains the list of clients.
    /// </summary>
    internal protected readonly List<Client> _clients = new();

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

            // Load the clients.
            await LoadClientsAsync();

            // Log what we are about to do.
            Logger.LogDebug(
                "Initializing the page."
                );

            // Initialize the page.
            await base.OnInitializedAsync();
        }
        catch (Exception ex)
        {
            // Log what we are about to do.
            Logger.LogError(
                ex,
                "Failed to fetch clients"
                );

            // Log what we are about to do.
            Logger.LogDebug(
                "Showing the snackbar message."
                );

            // Tell the world what happened.
            SnackbarService.Add(
                $"<b>Something broke!</b> " +
                $"<ul><li>{ex.GetBaseException().Message}</li></ul>",
                Severity.Error,
                options => options.CloseAfterNavigation = true
                );
        }
    }

    // *******************************************************************

    /// <summary>
    /// This method deletes the given client.
    /// </summary>
    /// <param name="client">The client to use for the operation.</param>
    /// <returns>A task to perform the operation.</returns>
    protected async Task OnDeleteAsync(
        Client client
        )
    {

    }

    // *******************************************************************

    /// <summary>
    /// This method edits the given client.
    /// </summary>
    /// <param name="client">The client to use for the operation.</param>
    /// <returns>A task to perform the operation.</returns>
    protected async Task OnEditAsync(
        Client client
        )
    {
        // Go to the detail page.
        NavigationManager.NavigateTo(
            $"/admin/clients/detail/{Uri.EscapeDataString(client.ClientId)}"
            );
    }

    // *******************************************************************

    /// <summary>
    /// This method adds a new client.
    /// </summary>
    protected async Task OnCreateAsync()
    {

    }

    // *******************************************************************

    /// <summary>
    /// This method disables the given client.
    /// </summary>
    /// <param name="client">The client to use for the operation.</param>
    /// <returns>A task to perform the operation.</returns>
    protected async Task DisableClientAsync(
        Client client
        )
    {

    }

    // *******************************************************************

    /// <summary>
    /// This method enables the given client.
    /// </summary>
    /// <param name="client">The client to use for the operation.</param>
    /// <returns>A task to perform the operation.</returns>
    protected async Task EnableClientAsync(
        Client client
        )
    {

    }

    // *******************************************************************

    /// <summary>
    /// This method adapts the <see cref="FilterFunc"/> method for use with 
    /// a <see cref="MudTable{T}"/> control.
    /// </summary>
    /// <param name="element">The element to use for the operation.</param>
    /// <returns><c>true</c> if a match was found; <c>false</c> otherwise.</returns>
    protected bool FilterFunc1(Client element) =>
        FilterFunc(element, _gridSearchString);

    // *******************************************************************

    /// <summary>
    /// This method performs a search of the clients.
    /// </summary>
    /// <param name="element">The element to uses for the operation.</param>
    /// <param name="searchString">The search string to use for the operation.</param>
    /// <returns><c>true</c> if a match was found; <c>false</c> otherwise.</returns>
    protected bool FilterFunc(
        Client element,
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
        return false;
    }

    #endregion

    // *******************************************************************
    // Private methods.
    // *******************************************************************

    #region Private methods

    /// <summary>
    /// This method loads clients for the page.
    /// </summary>
    /// <returns>A task to perform the operation.</returns>
    private async Task LoadClientsAsync()
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
            var clients = await GreenApi.Clients.FindAllAsync();

            // Log what we are about to do.
            Logger.LogDebug(
                "Adding clients to the page's list."
                );

            // Add to the page's list.
            _clients.AddRange(clients);
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