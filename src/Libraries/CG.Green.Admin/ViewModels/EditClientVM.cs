
using System.Diagnostics.Contracts;

namespace CG.Green.ViewModels;

/// <summary>
/// This class is a view-model for editing a Duende client.
/// </summary>
public class EditClientVM
{
    // *******************************************************************
    // Properties.
    // *******************************************************************

    #region Properties

    /// <summary>
    /// This property contains the identifier for the client.
    /// </summary>
    [Required]
    [MaxLength(Globals.Models.Clients.ClientIdLength)]
    [Display(Name = "Client Id")]
    public string ClientId { get; set; } = null!;

    /// <summary>
    /// This property contains the name for the client.
    /// </summary>
    [Required]
    [MaxLength(Globals.Models.Clients.ClientNameLength)]
    [Display(Name = "Client Name")]
    public string ClientName { get; set; } = null!;

    /// <summary>
    /// This property indicates whether or not the client is enabled.
    /// </summary>
    [Display(Name = "Enabled")]
    public bool Enabled { get; set; }

    /// <summary>
    /// This property contains the description for the client.
    /// </summary>
    [Required]
    [MaxLength(Globals.Models.Clients.DescriptionLength)]
    [Display(Name = "Description")]
    public string Description { get; set; } = null!;

    /// <summary>
    /// This property indicates whether or not the client is allows
    /// offline accesss.
    /// </summary>
    [Display(Name = "Offline Access")]
    public bool AllowOfflineAccess { get; set; }

    /// <summary>
    /// This property indicates whether or not the client is required
    /// to send signed requests only.
    /// </summary>
    [Display(Name = "Required Request Object")]
    public bool RequireRequestObject { get; set; }

    /// <summary>
    /// This property indicates whether or not the client is required
    /// to know one or more secrets.
    /// </summary>
    [Display(Name = "Require Client Secret")]
    public bool RequireClientSecret { get; set; }

    /// <summary>
    /// This property contains a list of secrets for the client.
    /// </summary>
    [Display(Name = "Secrets")]
    public List<EditSecretVM> Secrets { get; set; } = new();

    #endregion
}
