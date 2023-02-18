
namespace CG.Green.Data;

/// <summary>
/// This class is a data-context for the <see cref="CG.Green"/> microservice.
/// </summary>
public class GreenDbContext : 
    IdentityDbContext<
        GreenUser, 
        GreenRole, 
        string, 
        GreenUserClaim, 
        GreenUserRole, 
        GreenUserLogin, 
        GreenRoleClaim, 
        GreenUserToken
        >
{
    // *******************************************************************
    // Properties.
    // *******************************************************************

    #region Properties

    #endregion

    // *******************************************************************
    // Constructors.
    // *******************************************************************

    #region Constructors

    /// <summary>
    /// This constructor creates a new instance of the <see cref="GreenDbContext"/>
    /// class.
    /// </summary>
    /// <param name="options">The options to use with this data-context.</param>
    public GreenDbContext(
        DbContextOptions<GreenDbContext> options
        ) : base(options)
    {

    }

    #endregion

    // *******************************************************************
    // Protected methods.
    // *******************************************************************

    #region Protected methods

    /// <summary>
    /// This method is called to create the data model.
    /// </summary>
    /// <param name="modelBuilder">The builder to use for the operation.</param>
    protected override void OnModelCreating(
        ModelBuilder modelBuilder
        )
    {
        // We want everything in this context to use the "Green" schema.
        modelBuilder.HasDefaultSchema("Green");

        // Give the base class a chance.
        base.OnModelCreating(modelBuilder);
    }

    #endregion
}
