
namespace CG.Green.Host.Pages.Home;

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
    /// This field contains the optional model for create a default super
    /// administrator account.
    /// </summary>
    internal protected NewSuperAdminVM? _model;

    /// <summary>
    /// This field indicates the type of the password control.
    /// </summary>
    private InputType _passwordInput = InputType.Password;

    /// <summary>
    /// This field contains the icon for the password control.
    /// </summary>
    private string _passwordInputIcon = Icons.Material.Filled.VisibilityOff;

    #endregion

    // *******************************************************************
    // Properties.
    // *******************************************************************

    #region Properties

    /// <summary>
    /// This property contains the green API for this page.
    /// </summary>
    [Inject]
    internal protected IGreenApi GreenApi { get; set; } = null!;

    /// <summary>
    /// This property contains the dialog service for this page.
    /// </summary>
    [Inject]
    internal protected IDialogService DialogService { get; set; } = null!;

    /// <summary>
    /// This property contains the navigation manager for this page.
    /// </summary>
    [Inject]
    protected NavigationManager NavigationManager { get; set; } = null!;

    /// <summary>
    /// This property contains the logger for this page.
    /// </summary>
    [Inject]
    internal protected ILogger<Index> Logger { get; set; } = null!;

    /// <summary>
    /// This property contains the HTTP context accessor for this page.
    /// </summary>
    [Inject]
    internal protected IHttpContextAccessor HttpContextAccessor { get; set; } = null!;

    /// <summary>
    /// This property contains the name of the current user, or the word
    /// 'anonymous' if nobody is currently authenticated.
    /// </summary>
    internal protected string UserName => HttpContextAccessor.HttpContext?.User?.Identity?.Name ?? "anonymous";

    #endregion

    // *******************************************************************
    // Protected methods.
    // *******************************************************************

    #region Protected methods

    /// <summary>
    /// This method is called to initialize the page.
    /// </summary>
    /// <returns>A task to perform the operation.</returns>
    protected async override Task OnInitializedAsync()
    {
        try
        {
            // By default, we don't create the super administrator account.
            _model = null;

            // We only do this from local host.
            if (HttpContextAccessor.HttpContext?.Request.Host.Host == "localhost")
            {
                // Log what we are about to do.
                Logger.LogDebug(
                    "Checking for users"
                    );

                // Do we need to create a user?
                if (!await GreenApi.Users.AnyAsync())
                {
                    // Log what we are about to do.
                    Logger.LogDebug(
                        "Creating the {model} model.",
                        nameof(NewSuperAdminVM)
                        );

                    // With this model, we'll prompt to create a user.
                    _model = new NewSuperAdminVM();
                }
            }

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
        }
    }

    // *******************************************************************

    /// <summary>
    /// This method is called to submit the edit form.
    /// </summary>
    /// <returns>A task to perform the operation.</returns>
    protected async Task OnValidSubmitAsync()
    {
        try
        {
            // Sanity check the model.
            if (_model is null || _model.User is not null)
            {
                return; // Nothing to do!
            }

            // Log what we are about to do.
            Logger.LogDebug(
                "Looking for the super admin role."
                );

            // Look for the role we'll need.
            var superAdminRole = await GreenApi.Roles.FindByNameAsync(
                Globals.RoleNames.SuperAdmin
                );

            // Did we fail?
            if (superAdminRole is null)
            {
                // Log what we are about to do.
                Logger.LogDebug(
                    "Creating the super admin role."
                    );

                // Create the missing role.
                superAdminRole = await GreenApi.Roles.CreateAsync(
                    new GreenRole()
                    {
                        Name = Globals.RoleNames.SuperAdmin
                    },
                    UserName
                    );
            }

            // Log what we are about to do.
            Logger.LogDebug(
                "Creating the new user."
                );

            // Create the new user.
            _model.User = await GreenApi.Users.CreateAsync(
                new Identity.Models.GreenUser()
                {
                    Email = _model.Email,
                    UserName = _model.UserName, 
                },
                _model.Password,
                UserName
                );

            // Log what we are about to do.
            Logger.LogDebug(
                "Assigning the super admin role."
                );

            // Create the role / user association.
            await GreenApi.UserRoles.CreateAsync(
                new GreenUserRole()
                {
                    RoleId = superAdminRole.Id,
                    UserId = _model.User.Id
                },
                UserName
                );

            // Log what we are about to do.
            Logger.LogDebug(
                "Showing the message box."
                );

            // Show the user what we did.
            await DialogService.ShowMessageBox(
                title: Globals.Caption,
                markupMessage: new MarkupString("We created a super administrator account " +
                $"called: <b>{_model.User.UserName}</b> and we sent a confirmation email to: " +
                $"<b>{_model.User.Email}</b>.<br /><br />Go look for the confirmation email " +
                "now in your email inbox. When you find it, click the link to confirm your " +
                "email address.<br /><br /><b>NOTE:</b> You won't be able to log in until you " +
                "confirm your email address.")
                );

            // Log what we are about to do.
            Logger.LogDebug(
                "Navigating to the login page."
                );

            // Go to the login page.
            NavigationManager.NavigateTo("Identity/Account/Login?redirectUri=/", true);
        }
        catch (Exception ex)
        {
            // Log what happened.
            Logger.LogError(
                ex,
                "Failed to create a new admin user."
                );

            // Display the error message.
            await DialogService.ShowErrorBox(
				exception: ex,
				title: "Something Broke!"
				);
        }
    }

    // *******************************************************************

    /// <summary>
    /// This method toggles the password control between secure and not secure.
    /// </summary>
    void TogglePasswordVisibility()
    {
        if (_passwordInput != InputType.Password)
        {
            _passwordInputIcon = Icons.Material.Filled.VisibilityOff;
            _passwordInput = InputType.Password;
        }
        else
        {
            _passwordInputIcon = Icons.Material.Filled.Visibility;
            _passwordInput = InputType.Text;
        }
    }

    #endregion
}
