
namespace CG.Green.Plugins.Admin;

/// <summary>
/// This class is the main entry point for the plugin.
/// </summary>
internal class Module : ModuleBase
{
    // *******************************************************************
    // Public methods.
    // *******************************************************************

    #region Public methods

    /// <summary>
    /// This method configures any required services for the plugin.
    /// </summary>
    /// <param name="webApplicationBuilder">The web application builder to 
    /// use for the operation.</param>
    /// <param name="configuration">The plugin configuration to use for the 
    /// operation.</param>
    /// <param name="bootstrapLogger">The bootstrap logger to use for the 
    /// operation.</param>
    public override void ConfigureServices(
        WebApplicationBuilder webApplicationBuilder,
        IConfiguration configuration,
        ILogger? bootstrapLogger
        )
    {

    }

    // *******************************************************************

    /// <summary>
    /// This method wires up any startup or pipeline logic for the plugin.
    /// </summary>
    /// <param name="webApplication">The web application to use for the operation.</param>
    public override void Configure(
        WebApplication webApplication
        )
    {
        
    }

    #endregion
}
