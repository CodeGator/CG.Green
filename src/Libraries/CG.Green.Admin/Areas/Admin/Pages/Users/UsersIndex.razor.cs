
namespace CG.Green.Areas.Admin.Pages.Users;

/// <summary>
/// This class is the code-behind for the <see cref="UsersIndex"/> page.
/// </summary>
public partial class UsersIndex
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
	/// This field contains the list of users.
	/// </summary>
	internal protected List<ListGreenUserVM> _users = new();

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
	/// This property contains the feature manager for this page.
	/// </summary>
	[Inject]
	protected IFeatureManager Features { get; set; } = null!;

	/// <summary>
	/// This property contains the green api for this page.
	/// </summary>
	[Inject]
	protected IGreenApi GreenApi { get; set; } = null!;

	/// <summary>
	/// This property contains the MudBlazor theme for this page.
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
	protected ILogger<UsersIndex> Logger { get; set; } = null!;

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
			// Build the localized breadcrumbs.
			_crumbs = new()
			{
				new BreadcrumbItem("Home", href: "/"),
				new BreadcrumbItem("Admin", href: "/admin", disabled: true),
				new BreadcrumbItem("Users", href: "/admin/users")
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

			// Tell the world what happened.
			await Dialog.ShowErrorBox(
				exception: ex,
				title: "Something Broke!"
				);
		}
	}

	// *******************************************************************

	/// <summary>
	/// This method adapts the <see cref="FilterFunc"/> method for use with 
	/// a <see cref="MudTable{T}"/> control.
	/// </summary>
	/// <param name="element">The element to use for the operation.</param>
	/// <returns><c>true</c> if a match was found; <c>false</c> otherwise.</returns>
	protected bool FilterFunc1(ListGreenUserVM element) =>
		FilterFunc(element, _gridSearchString);

	// *******************************************************************

	/// <summary>
	/// This method performs a search of the users.
	/// </summary>
	/// <param name="element">The element to use for the operation.</param>
	/// <param name="searchString">The search string to use for the operation.</param>
	/// <returns><c>true</c> if a match was found; <c>false</c> otherwise.</returns>
	protected bool FilterFunc(
		ListGreenUserVM element,
		string searchString
		)
	{
		if (string.IsNullOrWhiteSpace(searchString))
		{
			return true;
		}
		if ((element.UserName ?? "").Contains(
			searchString,
			StringComparison.OrdinalIgnoreCase)
			)
		{
			return true;
		}
		if ((element.Email ?? "").Contains(
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
	/// This method creates a new ASP.NET user.
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
				{ "Model", new NewGreenUserVM() }
			};

			// Log what we are about to do.
			Logger.LogDebug(
				"Creating new dialog."
				);

			// Create the dialog.
			var dialog = Dialog.Show<NewUserDialog>(
				"Create User",
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
			var model = (NewGreenUserVM)result.Data;

			// Log what we are about to do.
			Logger.LogDebug(
				"Creating the new user."
				);

			// Map the VM to an ASP.NET user.
			var user = AutoMapper.Map<GreenUser>(model);

			// Create the new user.
			var newUser = await GreenApi.Users.CreateAsync(
				user,
				model.Password,
				UserName
				);

			// Log what we are about to do.
			Logger.LogDebug(
				"Navigating to the user detail page."
				);

			// Go to the detail page.
			Navigation.NavigateTo(
				$"/admin/users/{Uri.EscapeDataString(newUser.Id ?? "")}/detail"
				);
		}
		catch (Exception ex)
		{
			// Log what happened.
			Logger.LogError(
				ex.GetBaseException(),
				"Failed to create a user!"
				);

			// Log what we are about to do.
			Logger.LogDebug(
				"Showing the message box"
				);

			// Tell the world what happened.
			await Dialog.ShowErrorBox(
				exception: ex,
				title: "Something Broke!"
				);
		}

	}

	// *******************************************************************

	/// <summary>
	/// This method edits the given ASP.NET user.
	/// </summary>
	/// <param name="user">The user to use for the operation.</param>
	/// <returns>A task to perform the operation.</returns>
	protected Task OnEditAsync(
		ListGreenUserVM user
		)
	{
		// Log what we are about to do.
		Logger.LogDebug(
			"Navigating to the user detail page."
			);

		// Go to the detail page.
		Navigation.NavigateTo(
			$"/admin/users/{Uri.EscapeDataString(user.Id)}/detail"
			);

		// Return the task.
		return Task.CompletedTask;
	}

	// *******************************************************************

	/// <summary>
	/// This method deletes the given ASP.NET user.
	/// </summary>
	/// <param name="user">The user to use for the operation.</param>
	/// <returns>A task to perform the operation.</returns>
	protected async Task OnDeleteAsync(
		ListGreenUserVM user
		)
	{
		try
		{
			// Log what we are about to do.
			Logger.LogDebug(
				"Prompting the caller."
				);

			// Prompt the user.
			var result = await Dialog.ShowDeleteBox(
				user.UserName
				);

			// Did the user cancel?
			if (!result)
			{
				return; // Nothing more to do.
			}

			// Log what we are about to do.
			Logger.LogDebug(
				"Deleting a user."
				);

			// Convert to the ASP.NET model.
			var greenUser = AutoMapper.Map<GreenUser>(user);

			// Delete the user.
			await GreenApi.Users.DeleteAsync(
				greenUser,
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
				"Failed to delete a user!"
				);

			// Log what we are about to do.
			Logger.LogDebug(
				"Showing the message box"
				);

			// Tell the world what happened.
			await Dialog.ShowErrorBox(
				exception: ex,
				title: "Something Broke!"
				);
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
				"Clearing any old users."
				);

			// Get rid of any old users.
			_users.Clear();

			// Log what we are about to do.
			Logger.LogDebug(
				"Fetching users from the API."
				);

			// Get the list of users.
			_users = (await GreenApi.Users.FindAllAsync())
				.Select(x => AutoMapper.Map<ListGreenUserVM>(x))
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
