
namespace CG.Green.Host.ViewModels;

/// <summary>
/// This class is a view-model for creating a new API scope.
/// </summary>
public class NewApiScopeVM
{
    // *******************************************************************
    // Properties.
    // *******************************************************************

    #region Properties

    /// <summary>
    /// This property contains the name for the scope.
    /// </summary>
    [Required]
    [MaxLength(Globals.Models.ApiScopes.NameLength)]
    public string Name { get; set; } = null!;

    #endregion
}
