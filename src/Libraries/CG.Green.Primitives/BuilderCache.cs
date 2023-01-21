
namespace CG.Green.Identity;

/// <summary>
/// This class utility contains a cache of identity builders.
/// </summary>
/// <remarks>
/// <para>
/// This type exists as a way to separate the database specific, and non
/// database specific parts of the ASP.NET and Duende identity libraries. 
/// Both those libraries fully expect everything to live together in a single, 
/// monolithic project. I chose not to take that approach. Because of that 
/// fact, I need to share the ASP.NET and Duende identity builders between 
/// the identity and DAL projects - without introducing crazy project 
/// dependencies in the process. That way I do that is with this class 
/// utility. This type serves no other purpose and shouldn't be used for
/// any other reason.
/// </para>
/// </remarks>
public static class BuilderCache
{
    // *******************************************************************
    // Properties.
    // *******************************************************************

    #region Properties

    /// <summary>
    /// This property contains the cache of identity builders.
    /// </summary>
    public static Dictionary<string, object> Builders { get; set; } = new();

    #endregion
}
