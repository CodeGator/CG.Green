
namespace CG.Green.Host.ViewModels;

/// <summary>
/// This class is a view-model for editing an API scope.
/// </summary>
public class EditApiScopeVM
{
    // *******************************************************************
    // Properties.
    // *******************************************************************

    #region Properties

    /// <summary>
    /// This property contains the name of the API scope.
    /// </summary>
    [MaxLength(Globals.Models.ApiScopes.NameLength)]
    [Display(Name = "Name")]
    public string Name { get; set; } = null!;

    /// <summary>
    /// This property contains the display name of the API scope.
    /// </summary>
    [MaxLength(Globals.Models.ApiScopes.DisplayNameLength)]
    [Display(Name = "Display Name")]
    public string DisplayName { get; set; } = null!;

    /// <summary>
    /// This property contains the description of the API scope.
    /// </summary>
    [MaxLength(Globals.Models.ApiScopes.DescriptionLength)]
    [Display(Name = "Description")]
    public string Description { get; set; } = null!;

    /// <summary>
    /// This property indicates whether the API scope is required, or not.
    /// </summary>
    [Display(Name = "Required")]
    public bool Required { get; set; }

    /// <summary>
    /// This property indicates whether the API scope is enabled, or not.
    /// </summary>
    [Display(Name = "Enabled")]
    public bool Enabled { get; set; }

    /// <summary>
    /// This property indicates whether the API scope is visible in the discovery
    /// document, or not.
    /// </summary>
    [Display(Name = "Show In Discovery Document")]
    public bool ShowInDiscoveryDocument { get; set; }

    /// <summary>
    /// This property indicates whether the API scope should be emphasized 
    /// in consent screens, or not.
    /// </summary>
    [Display(Name = "Emphasize")]
    public bool Emphasize { get; set; }

    /// <summary>
    /// This property contains user claims associated with the API scope.
    /// </summary>
    [Display(Name = "User Claims")]
    public List<_Wrapper> UserClaims { get; set; } = new();

    /// <summary>
    /// This property contains properties associated with the API scope.
    /// </summary>
    [Display(Name = "Properties")]
    public IDictionary<string, string> Properties { get; set; } = new Dictionary<string, string>();

    #endregion

    // *******************************************************************
    // Public methods.
    // *******************************************************************

    #region Public methods

    /// <summary>
    /// This method converts the object to a Duende <see cref="ApiScope"/>
    /// object.
    /// </summary>
    /// <returns>A <see cref="ApiScope"/> object.</returns>
    public ApiScope ToDuende()
    {
        var obj = new ApiScope()
        {
            Name = Name,
            DisplayName = DisplayName,
            Description = Description,
            Required = Required,
            Enabled = Enabled,
            ShowInDiscoveryDocument = ShowInDiscoveryDocument,
            Emphasize = Emphasize,
            UserClaims = UserClaims.Select(x => x.Value).ToList(),
            Properties = Properties
        };
        return obj;
    }

    #endregion
}
