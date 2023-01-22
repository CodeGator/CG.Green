
namespace CG.Green.Host.Pages.Admin.Clients;

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
    internal protected Client? _model;

    /// <summary>
    /// This field indicates the page is loading data.
    /// </summary>
    internal protected bool _isLoading;

    /// <summary>
    /// This field contains the selected grant type, for the model.
    /// </summary>
    internal protected AllowedGrantTypes _selectedGrantType;

    /// <summary>
    /// This field contains the list of all scopes.
    /// </summary>
    internal protected IEnumerable<string> _allScopes = new HashSet<string>();

    /// <summary>
    /// This field contains the list of selected scopes.
    /// </summary>
    internal protected IEnumerable<string> _selectedScopes = new HashSet<string>();

    #endregion

    // *******************************************************************
    // Properties.
    // *******************************************************************

    #region Properties

    /// <summary>
    /// This property contains the identifier for the client.
    /// </summary>
    [Parameter]
    public string? ClientId { get; set; }

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
                new BreadcrumbItem("Clients", href: "/admin/clients"),
                new BreadcrumbItem("Details", href: $"/admin/clients/detail/{ClientId}")
            };

            // Log what we are about to do.
            Logger.LogDebug(
                "Loading data for the page."
                );

            // Load the client.
            await LoadDataAsync();

            // Log what we are about to do.
            Logger.LogDebug(
                "Initializing the page."
                );

            // Give the base class a chance.
            await base.OnInitializedAsync();
        }
        catch (Exception ex)
        {
            // Log what happened.
            Logger.LogError(
                ex,
                "Failed to initialize the page."
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
    /// This method is called when the user submits the form.
    /// </summary>
    /// <returns>A task to perform the operation.</returns>
    protected async Task OnValidSubmitAsync()
    {
        // Sanity check the model.
        if (_model is null)
        {
            return; // Nothing to do!
        }

        // Remove any existing grant type(s).
        _model.AllowedGrantTypes.Clear();

        // Add the selected grant types to the model.
        foreach (var grantType in _selectedGrantType.FromAllowedGrantTypes())
        {
            _model.AllowedGrantTypes.Add(grantType);
        }

        // Remove any existing scope(s).
        _model.AllowedScopes.Clear();

        // Add the selected scopes to the model.
        foreach (var selectedScope in _selectedScopes)
        {
            _model.AllowedScopes.Add(selectedScope);
        }
    }

    // *******************************************************************

    /// <summary>
    /// This method creates a new secret for the client.
    /// </summary>
    protected async Task OnCreateSecretAsync()
    {

    }

    // *******************************************************************

    /// <summary>
    /// This method deletes a secret from the client.
    /// </summary>
    /// <param name="secret">The secret to use for the operation.</param>
    protected async Task OnDeleteSecretAsync(
        Secret secret
        )
    {

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
            if (string.IsNullOrEmpty(ClientId))
            {
                _model = null; // no client!
                return;
            }

            // Log what we are about to do.
            Logger.LogDebug(
                "Marking the page as busy."
                );

            // We're now officially busy.
            _isLoading = true;

            // Get the list of available api scopes.
            _allScopes = (await GreenApi.ApiScopes.FindAllAsync())
                .Select(x => x.Name);

            // Log what we are about to do.
            Logger.LogDebug(
                "Fetching client from the API."
                );

            // Get the client.
            _model = await GreenApi.Clients.FindByIdAsync(
                ClientId ?? ""
                );

            // Did we succeed?
            if (_model is not null)
            {
                // Convert the grant type(s) to something we can bind to.
                _selectedGrantType = _model.AllowedGrantTypes.ToAllowedGrantTypes();

                // Select the client scopes.
                _selectedScopes = _model.AllowedScopes;
            }
            else
            {
                // Select defaults since we don't have a model.
                _selectedGrantType = AllowedGrantTypes.Ciba;
                _selectedScopes = new HashSet<string>();
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
