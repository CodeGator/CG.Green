
namespace CG.Green.Areas.Admin.Pages.Clients;

/// <summary>
/// This class is the code-behind for the <see cref="ClientDetail"/> page.
/// </summary>
public partial class ClientDetail
{
    // *******************************************************************
    // Fields.
    // *******************************************************************

    #region Fields

    /// <summary>
    /// This field contains a reference to breadcrumbs for the view.
    /// </summary>
    internal protected List<BreadcrumbItem> _crumbs = new();

    /// <summary>
    /// This field contains the model for the page.
    /// </summary>
    internal protected EditClientVM? _model;

    /// <summary>
    /// This field indicates the page is loading data.
    /// </summary>
    internal protected bool _isLoading;

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
    protected IDialogService Dialog { get; set; } = null!;

    /// <summary>
    /// This property contains the snackbar service for the page.
    /// </summary>
    [Inject]
    protected ISnackbar Snackbar { get; set; } = null!;

    /// <summary>
    /// This property contains the HTTP context accessor.
    /// </summary>
    [Inject]
    protected IHttpContextAccessor HttpContext { get; set; } = null!;

	/// <summary>
	/// This property contains the localizer for the page.
	/// </summary>
	[Inject]
	protected IStringLocalizer<ClientDetail> Localizer { get; set; } = null!;

	/// <summary>
	/// This property contains the navigation manager for the page.
	/// </summary>
	[Inject]
    protected NavigationManager Navigation { get; set; } = null!;

    /// <summary>
    /// This property contains the green API for the page.
    /// </summary>
    [Inject]
    protected IGreenApi GreenApi { get; set; } = null!;
	
    /// <summary>
    /// This property contains the auto mapper for this page.
    /// </summary>
    [Inject]
    protected IMapper AutoMapper { get; set; } = null!;

    /// <summary>
    /// This property contains the name of the current user, or the word
    /// 'anonymous' if nobody is currently authenticated.
    /// </summary>
    protected string UserName => HttpContext.HttpContext?.User?.Identity?.Name ?? "anonymous";

    /// <summary>
    /// This property contains the logger for this page.
    /// </summary>
    [Inject]
    protected ILogger<ClientDetail> Logger { get; set; } = null!;

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
                new BreadcrumbItem("Details", href: $"/admin/clients/{ClientId}/detail")
            };

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

            // Tell the world what happened.
            await Dialog.ShowErrorBox(ex);
        }
    }

    // *******************************************************************

    /// <summary>
    /// This method is called when the user submits the form.
    /// </summary>
    /// <returns>A task to perform the operation.</returns>
    protected async Task OnValidSubmitAsync()
    {
        try
        {
            // Sanity check the model.
            if (_model is null)
            {
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
                "Saving the client changes"
                );

            // Update the client in the api.
            await GreenApi.Clients.UpdateAsync(
                AutoMapper.Map<Client>(_model),
                UserName
                );

            // Log what we are about to do.
            Logger.LogDebug(
                "Loading data for the page."
                );

            // Load the client.
            await LoadDataAsync();

            // Log what we are about to do.
            Logger.LogDebug(
                "Showing the snackbar"
                );

            // Tell the world what we did.
            Snackbar.Add(
                $"Changes were saved"
                );
        }
        catch (Exception ex)
        {
            // Log what happened.
            Logger.LogError(
                ex.GetBaseException(),
                "Failed to save changes to a secret!"
                );

            // Tell the world what happened.
            await Dialog.ShowErrorBox(ex);
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

            // Log what we are about to do.
            Logger.LogDebug(
                "Fetching client from the API."
                );

            // Get the client.
            var client = await GreenApi.Clients.FindByIdAsync(
                ClientId ?? ""
                );

            // Did we succeed?
            if (client is not null)
            {
                // Log what we are about to do.
                Logger.LogDebug(
                    "Wrapping the client in a VM."
                    );

                // Wrap the model.
                _model = AutoMapper.Map<EditClientVM>(client);

                // Mark any secrets as hashed since anything coming from
                //   Duende is going to be hashed already.
                _model.ClientSecrets.ForEach(x => x.IsHashed = true); 
            }
            else
            {
                // Log what we are about to do.
                Logger.LogDebug(
                    "Setting defaults since we don't have a model."
                    );
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
