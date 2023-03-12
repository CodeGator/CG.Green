
namespace CG.Green.Areas.Admin.Pages.Users;

/// <summary>
/// This class is the code-behind for the <see cref="UsersDetail"/> page.
/// </summary>
public partial class UsersDetail
{
	// *******************************************************************
	// Fields.
	// *******************************************************************

	#region Fields

	/// <summary>
	/// This field contains a reference to breadcrumbs.
	/// </summary>
	internal protected List<BreadcrumbItem> _crumbs = new();

	/// <summary>
	/// This field contains the model for the page.
	/// </summary>
	internal protected EditGreenUserVM? _model;

	/// <summary>
	/// This field indicates when the page is loading data.
	/// </summary>
	internal protected bool _isLoading;

	#endregion

	// *******************************************************************
	// Properties.
	// *******************************************************************

	#region Properties

	/// <summary>
	/// This property contains the identifier for the user.
	/// </summary>
	[Parameter]
	public string? UserId { get; set; }

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
	protected IStringLocalizer<UsersDetail> Localizer { get; set; } = null!;

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
	protected ILogger<UsersDetail> Logger { get; set; } = null!;

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
				new BreadcrumbItem(Localizer["Home"], href: "/"),
				new BreadcrumbItem(Localizer["Admin"], href: "/admin", disabled: true),
				new BreadcrumbItem(Localizer["Users"], href: "/admin/users"),
				new BreadcrumbItem(Localizer["Details"], href: $"/admin/users/{UserId}/detail")
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
			await Dialog.ShowErrorBox(
				exception: ex,
				title: Localizer["Broke"]
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

			// Is lock disabled for this user?
			if (!_model.LockoutEnabled)
			{
				// No lockout date specified.
				_model.LockoutEnd = null;
			}

			// Map back to the ASP.NET model.
			var dirtyModel = AutoMapper.Map<GreenUser>(_model);

			// Update the user in the api.
			await GreenApi.Users.UpdateAsync(
				dirtyModel,
				UserName
				);

			// Log what we are about to do.
			Logger.LogDebug(
				"Loading data for the page."
				);

			// Load the user.
			await LoadDataAsync();

			// Log what we are about to do.
			Logger.LogDebug(
				"Showing the snackbar"
				);

			// Tell the world what we did.
			Snackbar.Add(
				Localizer["ChangesSaved"]
				);
		}
		catch (Exception ex)
		{
			// Log what happened.
			Logger.LogError(
				ex.GetBaseException(),
				"Failed to save changes to a user!"
				);

			// Tell the world what happened.
			await Dialog.ShowErrorBox(
				exception: ex,
				title: Localizer["Broke"]
				);
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
			if (string.IsNullOrEmpty(UserId))
			{
				_model = null; // no user!
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
				"Fetching user from the API."
				);

			// Get the user.
			var user = await GreenApi.Users.FindByIdAsync(
				UserId ?? ""
				);

			// Did we succeed?
			if (user is not null)
			{
				// Log what we are about to do.
				Logger.LogDebug(
					"Wrapping the user in a VM."
					);

				// Wrap the model.
				_model = AutoMapper.Map<EditGreenUserVM>(user);
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
