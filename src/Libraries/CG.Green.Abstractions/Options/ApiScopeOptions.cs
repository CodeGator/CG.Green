
namespace CG.Green.Options;

/// <summary>
/// This class contains configuration settings for seeding api scopes.
/// </summary>
public class ApiScopeOptions
{
    // *******************************************************************
    // Properties.
    // *******************************************************************

    #region Properties

    /// <summary>
    /// This property contains a list of <see cref="ApiScope"/> objects.
    /// </summary>
    [Required]
    public List<ApiScope> ApiScopes { get; set; } = new();

    #endregion
}
