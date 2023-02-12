
namespace CG.Green.Admin.Areas.Admin.Pages.Clients;

/// <summary>
/// This class is the code-behind for the <see cref="ClientsIndex"/> page.
/// </summary>
public partial class ClientsIndex
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
    /// This field contains the model for the page.
    /// </summary>
    internal protected List<EditClientVM> _model = new();

    /// <summary>
    /// This field indicates whether or not we are busy.
    /// </summary>
    internal protected bool _isBusy;

    /// <summary>
    /// This field indicates whether or not we are loading data.
    /// </summary>
    internal protected bool _isLoading;

    /// <summary>
    /// This field contains the current grid search string.
    /// </summary>
    internal protected string _gridSearchString = "";

    #endregion

    // *******************************************************************
    // Properties.
    // *******************************************************************

    #region Properties

    /// <summary>
    /// This property contains the CG.Green api for this page.
    /// </summary>
    [Inject]
    protected IGreenApi Api { get; set; } = null!;

    /// <summary>
    /// This property contains the dialog service for this page.
    /// </summary>
    [Inject]
    protected IDialogService DialogService { get; set; } = null!;

    /// <summary>
    /// This property contains the snackbar service for this page.
    /// </summary>
    [Inject]
    protected ISnackbar Snackbar { get; set; } = null!;

    /// <summary>
    /// This property contains the HTTP context accessor.
    /// </summary>
    [Inject]
    protected IHttpContextAccessor HttpContextAccessor { get; set; } = null!;

    /// <summary>
    /// This property contains the logger for this page.
    /// </summary>
    [Inject]
    protected ILogger<ClientsIndex> Logger { get; set; } = null!;

    /// <summary>
    /// This property contains the name of the current user, or the word
    /// 'anonymous' if nobody is currently authenticated.
    /// </summary>
    protected string UserName => HttpContextAccessor.HttpContext?.User?.Identity?.Name ?? "anonymous";

    #endregion

    // *******************************************************************
    // Protected methods.
    // *******************************************************************

    #region Protected methods

    /// <summary>
    /// This method is called by the framework to initialize the page.
    /// </summary>
    /// <returns>A task to perform the operation.</returns>
    protected override async Task OnInitializedAsync()
    {
        try
        {
            // Log what we are about to do.
            Logger.LogDebug(
                "Setting the page to busy and loading."
                );

            // We're busy.
            _isBusy = true;

            // We're loading.
            _isLoading = true;

            // Log what we are about to do.
            Logger.LogDebug(
                "Fetching data for the page."
                );

            // Fetch the clients as view-models.
            _model = (await Api.Clients.FindAllAsync())
                .Select(x => x.FromDuende())
                .ToList();

            // Give the base class a chance.
            await base.OnInitializedAsync();
        }
        catch (Exception ex)
        {
            // Log what happened.
            Logger.LogError(
                ex.GetBaseException(),
                "Failed to fetch initialize the page!"
                );

            // Log what we are about to do.
            Logger.LogDebug(
                "Showing the message box"
                );

            // Tell the world what happened.
            await DialogService.ShowErrorBox(ex);
        }
        finally
        {
            // Log what we are about to do.
            Logger.LogDebug(
                "Setting the page to not busy and not loading."
                );

            // We're no longer busy.
            _isBusy = false;

            // We're no longer loading.
            _isLoading = false;
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
    protected async Task OnCreateClientAsync()
    {
        try
        {
            // Log what we are about to do.
            Logger.LogDebug(
                "Setting the page to busy."
                );

            // We're busy.
            _isBusy = true;

            // Log what we are about to do.
            Logger.LogDebug(
                "Creating dialog options."
                );

            // Create the dialog options.
            var options = new DialogOptions
            {
                MaxWidth = MaxWidth.ExtraSmall,
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
            var dialog = DialogService.Show<NewClientDialog>(
                "Create a Client",
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

            // TODO : write the code for this.
        }
        catch (Exception ex)
        {
            // Log what happened.
            Logger.LogError(
                ex.GetBaseException(),
                "Failed to fetch initialize the page!"
                );

            // Log what we are about to do.
            Logger.LogDebug(
                "Showing the message box"
                );

            // Tell the world what happened.
            await DialogService.ShowErrorBox(ex);
        }
        finally
        {
            // Log what we are about to do.
            Logger.LogDebug(
                "Setting the page to not busy."
                );

            // We're no longer busy.
            _isBusy = false;
        }
    }

    // *******************************************************************

    /// <summary>
    /// This method deletes the given client.
    /// </summary>
    /// <param name="client">The client to use for the operation.</param>
    /// <returns>A task to perform the operation.</returns>
    protected async Task OnDeleteClientAsync(
        EditClientVM client
        )
    {

    }

    // *******************************************************************

    /// <summary>
    /// This method edits the given client.
    /// </summary>
    /// <param name="client">The client to use for the operation.</param>
    /// <returns>A task to perform the operation.</returns>
    protected async Task OnEditClientAsync(
        EditClientVM client
        )
    {

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

    }

    #endregion
}

