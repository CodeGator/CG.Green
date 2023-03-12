


namespace CG.Green.Areas.Admin.Pages.Roles;

/// <summary>
/// This class is the code-behind for the <see cref="RolesIndex"/> page.
/// </summary>
public partial class RolesIndex
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
	/// This field contains the list of roles.
	/// </summary>
	internal protected List<ListGreenRoleVM> _roles = new();

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
	/// This property contains the localizer for this page.
	/// </summary>
	[Inject]
	protected IStringLocalizer<RolesIndex> Localizer { get; set; } = null!;

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
	protected ILogger<RolesIndex> Logger { get; set; } = null!;

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
				new BreadcrumbItem(Localizer["Home"], href: "/"),
				new BreadcrumbItem(Localizer["Admin"], href: "/admin", disabled: true),
				new BreadcrumbItem(Localizer["Users"], href: "/admin/users")
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
				title: Localizer["Broke"]
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
	protected bool FilterFunc1(ListGreenRoleVM element) =>
		FilterFunc(element, _gridSearchString);

	// *******************************************************************

	/// <summary>
	/// This method performs a search of the roles.
	/// </summary>
	/// <param name="element">The element to use for the operation.</param>
	/// <param name="searchString">The search string to use for the operation.</param>
	/// <returns><c>true</c> if a match was found; <c>false</c> otherwise.</returns>
	protected bool FilterFunc(
		ListGreenRoleVM element,
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
				{ "Model", new EditGreenRoleVM() }
			};

			// Log what we are about to do.
			Logger.LogDebug(
				"Creating new dialog."
				);

			// Create the dialog.
			var dialog = Dialog.Show<EditRoleDialog>(
				Localizer["CreateRole"],
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
			var model = (EditGreenRoleVM)result.Data;

			// Log what we are about to do.
			Logger.LogDebug(
				"Creating the new role."
				);

			// Map the VM to an ASP.NET role.
			var role = AutoMapper.Map<GreenRole>(model);

			// Create the new role.
			var newRole = await GreenApi.Roles.CreateAsync(
				role,
				UserName
				);

			// Log what we are about to do.
			Logger.LogDebug(
				"Navigating to the user detail page."
				);

			// Go to the detail page.
			Navigation.NavigateTo(
				$"/admin/roles/{Uri.EscapeDataString(newRole.Id ?? "")}/detail"
				);
		}
		catch (Exception ex)
		{
			// Log what happened.
			Logger.LogError(
				ex.GetBaseException(),
				"Failed to create a role!"
				);

			// Log what we are about to do.
			Logger.LogDebug(
				"Showing the message box"
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
	/// This method edits the given ASP.NET role.
	/// </summary>
	/// <param name="role">The role to use for the operation.</param>
	/// <returns>A task to perform the operation.</returns>
	protected Task OnEditAsync(
		ListGreenRoleVM role
		)
	{
		// Log what we are about to do.
		Logger.LogDebug(
			"Navigating to the role detail page."
			);

		// Go to the detail page.
		Navigation.NavigateTo(
			$"/admin/roles/{Uri.EscapeDataString(role.Id)}/detail"
			);

		// Return the task.
		return Task.CompletedTask;
	}

	// *******************************************************************

	/// <summary>
	/// This method deletes the given ASP.NET role.
	/// </summary>
	/// <param name="role">The role to use for the operation.</param>
	/// <returns>A task to perform the operation.</returns>
	protected async Task OnDeleteAsync(
		ListGreenRoleVM role
		)
	{
		try
		{
			// Log what we are about to do.
			Logger.LogDebug(
				"Prompting the caller."
				);

			// Prompt the role.
			var result = await Dialog.ShowDeleteBox(
				role.Name
				);

			// Did the user cancel?
			if (!result)
			{
				return; // Nothing more to do.
			}

			// Log what we are about to do.
			Logger.LogDebug(
				"Deleting a role."
				);

			// Convert to the ASP.NET model.
			var greenRole = AutoMapper.Map<GreenRole>(role);

			// Delete the role.
			await GreenApi.Roles.DeleteAsync(
				greenRole,
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
				"Failed to delete a role!"
				);

			// Log what we are about to do.
			Logger.LogDebug(
				"Showing the message box"
				);

			// Tell the world what happened.
			await Dialog.ShowErrorBox(
				exception: ex,
				title: Localizer["Broke"]
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
				.Select(x => AutoMapper.Map<ListGreenRoleVM>(x))
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
