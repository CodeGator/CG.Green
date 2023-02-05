
namespace CG.Green.Host.ViewModels;

/// <summary>
/// This class is a view-model for editing an identity resource.
/// </summary>
public class EditResourceVM
{
    // *******************************************************************
    // Properties.
    // *******************************************************************

    #region Properties

    /// <summary>
    /// This property contains the name of the resource.
    /// </summary>
    [MaxLength(Globals.Models.Resources.NameLength)]
    [Display(Name = "Name")]
    public string Name { get; set; } = null!;

    /// <summary>
    /// This property contains the display name of the resource.
    /// </summary>
    [MaxLength(Globals.Models.Resources.DisplayNameLength)]
    [Display(Name = "Display Name")]
    public string DisplayName { get; set; } = null!;

    /// <summary>
    /// This property contains the description of the resource.
    /// </summary>
    [MaxLength(Globals.Models.Resources.DescriptionLength)]
    [Display(Name = "Description")]
    public string Description { get; set; } = null!;

    /// <summary>
    /// This property indicates whether the resource is required, or not.
    /// </summary>
    [Display(Name = "Required")]
    public bool Required { get; set; }

    /// <summary>
    /// This property indicates whether the resource is enabled, or not.
    /// </summary>
    [Display(Name = "Enabled")]
    public bool Enabled { get; set; }

    /// <summary>
    /// This property indicates whether the resource is visible in the discovery
    /// document, or not.
    /// </summary>
    [Display(Name = "Show In Discovery Document")]
    public bool ShowInDiscoveryDocument { get; set; }

    /// <summary>
    /// This property indicates whether the resource should be emphasized 
    /// in consent screens, or not.
    /// </summary>
    [Display(Name = "Emphasize")]
    public bool Emphasize { get; set; }

    /// <summary>
    /// This property contains user claims associated with the resource.
    /// </summary>
    [Display(Name = "User Claims")]
    public List<_Wrapper> UserClaims { get; set; } = new();

    /// <summary>
    /// This property contains properties associated with the resource.
    /// </summary>
    [Display(Name = "Properties")]
    public List<EditPropertyVM> Properties { get; set; } = new();

    #endregion

    // *******************************************************************
    // Public methods.
    // *******************************************************************

    #region Public methods

    /// <summary>
    /// This method converts the object to a Duende <see cref="IdentityResource"/>
    /// object.
    /// </summary>
    /// <returns>A <see cref="IdentityResource"/> object.</returns>
    public IdentityResource ToDuende()
    {
        var obj = new IdentityResource()
        {
            Name = Name,
            DisplayName = DisplayName,
            Description = Description,
            Required = Required,
            Enabled = Enabled,
            ShowInDiscoveryDocument = ShowInDiscoveryDocument,
            Emphasize = Emphasize,
            UserClaims = UserClaims.Select(x => x.Value).ToList(),
            Properties = Properties.ToDictionary(x => x.Key, y => y.Value)
        };
        return obj;
    }

    #endregion
}
