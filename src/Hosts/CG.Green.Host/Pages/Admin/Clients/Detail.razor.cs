
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

    /// <summary>
    /// This field contains the list of client secrets.
    /// </summary>
    internal protected readonly List<SecretVM> _secrets = new();

    /// <summary>
    /// This field contains the list of redirect uris.
    /// </summary>
    internal protected readonly List<_Wrapper> _redirectUris = new();

    /// <summary>
    /// This field contains the list of post logout redirect uris.
    /// </summary>
    internal protected readonly List<_Wrapper> _postLogoutRedirectUris = new();

    /// <summary>
    /// This field contains the list of allowed CORS origins.
    /// </summary>
    internal protected readonly List<_Wrapper> _allowedCorsOrigins = new();

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
                return; // Nothing to do!
            }

            // Log what we are about to do.
            Logger.LogDebug(
                "Saving the allowed grant types"
                );

            // Remove any existing grant type(s).
            _model.AllowedGrantTypes.Clear();

            // Add the selected grant types to the model.
            foreach (var grantType in _selectedGrantType.FromAllowedGrantTypes())
            {
                _model.AllowedGrantTypes.Add(grantType);
            }

            // Log what we are about to do.
            Logger.LogDebug(
                "Saving the allowed scopes"
                );

            // Remove any existing scope(s).
            _model.AllowedScopes.Clear();

            // Add the selected scopes to the model.
            foreach (var selectedScope in _selectedScopes)
            {
                _model.AllowedScopes.Add(selectedScope);
            }

            // Log what we are about to do.
            Logger.LogDebug(
                "Saving the client secrets"
                );

            // Remove any previous secrets.
            _model.ClientSecrets.Clear();

            // Add the secrets to the model.
            foreach (var secret in _secrets)
            {
                // Should we hash the secret?
                if (!secret.IsHashed)
                {
                    secret.Secret.Value = secret.Secret.Value.ToSha256();
                }
                _model.ClientSecrets.Add(secret.Secret);
            }

            // Log what we are about to do.
            Logger.LogDebug(
                "Saving the redirect uris"
                );

            // Remove any previous redirect uris.
            _model.RedirectUris.Clear();
            
            // Add the redirect uris to the model.
            foreach (var uri in _redirectUris)
            {
                _model.RedirectUris.Add(uri.Value);
            }

            // Log what we are about to do.
            Logger.LogDebug(
                "Saving the post logout redirect uris"
                );

            // Remove any previous redirect uris.
            _model.PostLogoutRedirectUris.Clear();

            // Add the redirect uris to the model.
            foreach (var uri in _postLogoutRedirectUris)
            {
                _model.PostLogoutRedirectUris.Add(uri.Value);
            }

            // Log what we are about to do.
            Logger.LogDebug(
                "Saving allowed the CORS origins"
                );

            // Remove any previous redirect uris.
            _model.AllowedCorsOrigins.Clear();

            // Add the redirect uris to the model.
            foreach (var uri in _allowedCorsOrigins)
            {
                _model.AllowedCorsOrigins.Add(uri.Value);
            }

            // Log what we are about to do.
            Logger.LogDebug(
                "Saving the client changes"
                );

            // Update the client in the api.
            await GreenApi.Clients.UpdateAsync(
                _model,
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
            SnackbarService.Add(
                $"Saved changes to client"
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
    /// This method creates a new secret for the client.
    /// </summary>
    protected async Task OnCreateSecretAsync()
    {
        try
        {
            // Log what we are about to do.
            Logger.LogDebug(
                "Adding a new secret."
                );

            // Add a new secret.
            _secrets.Add(new SecretVM()
            {
                Secret = new Secret()
                {
                    Description = "new secret",
                    Type = "SharedSecret"
                }
            });
        }
        catch (Exception ex)
        {
            // Log what happened.
            Logger.LogError(
                ex.GetBaseException(),
                "Failed to add a secret!"
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
    /// This method creates a redirect claim for the client.
    /// </summary>
    protected async Task OnCreateRedirectUriAsync()
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
                "Adding a new redirect uri."
                );

            // Create the new redirect claim.
            var newItem = new _Wrapper() 
            { 
                Value = $"https://localhost/{_redirectUris.Count()+1}/signin-oidc" 
            };

            // Add the item to the list.
            _redirectUris.Add(newItem);

            // Tell Blazor to update.
            StateHasChanged();
        }
        catch (Exception ex)
        {
            // Log what happened.
            Logger.LogError(
                ex.GetBaseException(),
                "Failed to add a redirect uri!"
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
    /// This method creates a post logout redirect claim for the client.
    /// </summary>
    protected async Task OnCreatePostLogoutRedirectUriAsync()
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
                "Adding a new post logout redirect uri."
                );

            // Create the new post logout redirect claim.
            var newItem = new _Wrapper() 
            {
                Value = $"https://localhost{_postLogoutRedirectUris.Count() + 1}/signout-callback-oidc" 
            };

            // Add the item to the list.
            _postLogoutRedirectUris.Add(newItem);

            // Tell Blazor to update.
            StateHasChanged();
        }
        catch (Exception ex)
        {
            // Log what happened.
            Logger.LogError(
                ex.GetBaseException(),
                "Failed to add a post logout redirect uri!"
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
    /// This method creates an allowed CORS origin for the client.
    /// </summary>
    protected async Task OnCreateAllowedCorsOriginAsync()
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
                "Adding a new CORS origin."
                );

            // Create the new CORS origin.
            var newItem = new _Wrapper()
            {
                Value = $"https://localhost{_allowedCorsOrigins.Count() + 1}"
            };

            // Add the item to the list.
            _allowedCorsOrigins.Add(newItem);

            // Tell Blazor to update.
            StateHasChanged();
        }
        catch (Exception ex)
        {
            // Log what happened.
            Logger.LogError(
                ex.GetBaseException(),
                "Failed to add a CORS origin!"
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
    /// This method creates a claim for the client.
    /// </summary>
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
                "Adding a new claim."
                );

            // Create the new claim.
            var newItem = new ClientClaim()
            {
                Value = $"claim value {_model.Claims.Count() + 1}",
                Type = $"claim type {_model.Claims.Count() + 1}",
                ValueType = ""
            };

            // Add the item to the list.
            _model.Claims.Add(newItem);

            // Tell Blazor to update.
            StateHasChanged();
        }
        catch (Exception ex)
        {
            // Log what happened.
            Logger.LogError(
                ex.GetBaseException(),
                "Failed to add a claim!"
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
    /// This method deletes a redirect claim from the client.
    /// </summary>
    /// <param name="uri">The claim to use for the operation.</param>
    protected async Task OnDeleteRedirectUriAsync(
        string uri
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
                markupMessage: new MarkupString("This will delete the redirect " +
                $"uri <b>'{uri}'</b> <br /> <br /> Are you <i>sure</i> you " +
                "want to do that?"),
                noText: "Cancel"
                );

            // Did the user cancel?
            if (result.HasValue && !result.Value)
            {
                return; // Nothing more to do.
            }

            // Log what we are about to do.
            Logger.LogDebug(
                "Deleting a redirect uri."
                );

            // Look for the claim.
            var item = _redirectUris.FirstOrDefault(x => 
                x.Value == uri
                );

            // Did we find it?
            if (item is not null)
            {
                // Remove the item.
                _redirectUris.Remove(item);

                // Tell Blazor to update.
                StateHasChanged();
            }
        }
        catch (Exception ex)
        {
            // Log what happened.
            Logger.LogError(
                ex.GetBaseException(),
                "Failed to delete a redirect uri!"
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
    /// This method deletes a post logout redirect claim from the client.
    /// </summary>
    /// <param name="uri">The claim to use for the operation.</param>
    protected async Task OnDeletePostLogoutRedirectUriAsync(
        string uri
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
                markupMessage: new MarkupString("This will delete the post " +
                "logout redirect uri <b>'{uri}'</b> <br /> <br /> Are you " +
                "<i>sure</i> you want to do that?"),
                noText: "Cancel"
                );

            // Did the user cancel?
            if (result.HasValue && !result.Value)
            {
                return; // Nothing more to do.
            }

            // Log what we are about to do.
            Logger.LogDebug(
                "Deleting a post logout redirect uri."
                );

            // Look for the claim.
            var item = _postLogoutRedirectUris.FirstOrDefault(x =>
                x.Value == uri
                );

            // Did we find it?
            if (item is not null)
            {
                // Remove the item.
                _postLogoutRedirectUris.Remove(item);

                // Tell Blazor to update.
                StateHasChanged();
            }
        }
        catch (Exception ex)
        {
            // Log what happened.
            Logger.LogError(
                ex.GetBaseException(),
                "Failed to delete a post logout redirect uri!"
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
    /// This method deletes an allowed CORS origin.
    /// </summary>
    /// <param name="uri">The claim to use for the operation.</param>
    protected async Task OnDeleteAllowedCorsOriginAsync(
        string uri
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
                markupMessage: new MarkupString("This will delete the allowed " +
                $"CORS origin <b>'{uri}'</b> <br /> <br /> Are you <i>sure</i> " +
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
                "Deleting an allowed CORS origin."
                );

            // Look for the claim.
            var item = _allowedCorsOrigins.FirstOrDefault(x =>
                x.Value == uri
                );

            // Did we find it?
            if (item is not null)
            {
                // Remove the item.
                _allowedCorsOrigins.Remove(item);

                // Tell Blazor to update.
                StateHasChanged();
            }
        }
        catch (Exception ex)
        {
            // Log what happened.
            Logger.LogError(
                ex.GetBaseException(),
                "Failed to delete an allowed CORS origin!"
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
    /// This method deletes a claim.
    /// </summary>
    /// <param name="claim">The claim to use for the operation.</param>
    protected async Task OnDeleteClaimAsync(
        ClientClaim claim
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
                $"<b>'{claim.Type}'</b> <br /> <br /> Are you <i>sure</i> " +
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

            // Remove the item.
            _model.Claims.Remove(claim);

            // Tell Blazor to update.
            StateHasChanged();
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
    /// This method deletes a secret from the client.
    /// </summary>
    /// <param name="secret">The secret to use for the operation.</param>
    protected async Task OnDeleteSecretAsync(
        SecretVM secret
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
                markupMessage: new MarkupString("This will delete the secret " +
                $"<b>'{secret.Secret.Description}'</b> <br /> <br /> Are you " +
                "<i>sure</i> you want to do that?"),
                noText: "Cancel"
                );

            // Did the user cancel?
            if (result.HasValue && !result.Value)
            {
                return; // Nothing more to do.
            }

            // Log what we are about to do.
            Logger.LogDebug(
                "Deleting a secret."
                );

            // Remove the secret from the client.
            _secrets.Remove(secret);
        }
        catch (Exception ex)
        {
            // Log what happened.
            Logger.LogError(
                ex.GetBaseException(),
                "Failed to delete a secret!"
                );

            // Log what we are about to do.
            Logger.LogDebug(
                "Showing the message box"
                );

            /// Tell the world what happened.
            await DialogService.ShowErrorBox(ex);
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
                "Fetching api scopes."
                );

            // Get the list of API scopes.
            var apiScopes = (await GreenApi.ApiScopes.FindAllAsync())
                .Select(x => x.Name);

            // Log what we are about to do.
            Logger.LogDebug(
                "Fetching identity resources."
                );

            // Get the list of identity resources.
            var identityResources = (await GreenApi.IdentityResources.FindAllAsync())
                .Select(x => x.Name);

            // Log what we are about to do.
            Logger.LogDebug(
                "Building the combines list of scopes."
                );

            // Get the list of all scopes.
            _allScopes = apiScopes.Union(
                    identityResources
                    ).Order();

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
                // Log what we are about to do.
                Logger.LogDebug(
                    "Saving the selected grant types."
                    );

                // Convert the grant type(s) to something we can bind to.
                _selectedGrantType = _model.AllowedGrantTypes.ToAllowedGrantTypes();

                // Log what we are about to do.
                Logger.LogDebug(
                    "Saving the allowed scopes."
                    );

                // Copy the client scopes.
                _selectedScopes = _model.AllowedScopes.ToList();

                // Log what we are about to do.
                Logger.LogDebug(
                    "Building the list of secrets."
                    );

                // Remove any existing secrets.
                _secrets.Clear();

                // Build the list of secrets.
                _secrets.AddRange(
                    _model.ClientSecrets.Select(x => new SecretVM()
                    {
                        Secret = x,
                        IsHashed = true
                    }));

                // Remove any existing redirect uris.
                _redirectUris.Clear();

                // Build the list of redirect uris.
                _redirectUris.AddRange(
                    _model.RedirectUris.Select(x => new _Wrapper()
                    {
                        Value = x
                    }));

                // Remove any existing post logout redirect uris.
                _postLogoutRedirectUris.Clear();

                // Build the list of post logout redirect uris.
                _postLogoutRedirectUris.AddRange(
                    _model.PostLogoutRedirectUris.Select(x => new _Wrapper()
                    {
                        Value = x
                    }));

                // Remove any existing CORS origins.
                _allowedCorsOrigins.Clear();

                // Build the list of CORS origins.
                _allowedCorsOrigins.AddRange(
                    _model.AllowedCorsOrigins.Select(x => new _Wrapper()
                    {
                        Value = x
                    }));
            }
            else
            {
                // Log what we are about to do.
                Logger.LogDebug(
                    "Setting defaults since we don't have a model."
                    );

                // Select defaults since we don't have a model.
                _selectedGrantType = AllowedGrantTypes.Ciba;
                _selectedScopes = new HashSet<string>();
                _secrets.Clear();
                _redirectUris.Clear();
                _postLogoutRedirectUris.Clear();
                _allowedCorsOrigins.Clear();
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

