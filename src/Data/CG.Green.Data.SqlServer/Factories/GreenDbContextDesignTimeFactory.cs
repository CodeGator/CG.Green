﻿
namespace CG.Green.Data.SqlServer;

/// <summary>
/// This class is a SQLServer design time factory for the <see cref="AspNetDbContext"/>
/// data-context type.
/// </summary>
internal class GreenDbContextDesignTimeFactory : DesignTimeDbContextFactory<AspNetDbContext>
{
    // *******************************************************************
    // Protected methods.
    // *******************************************************************

    #region Protected methods

    /// <summary>
    /// This method is overridden in order to configure the options for 
    /// a SqlServer data-context instance.
    /// </summary>
    /// <param name="optionsBuilder">The data-context options builder
    /// to use for the operation.</param>
    /// <param name="configuration">The configuration to use for the operation.</param>
    protected override void OnConfigureDataContextOptions(
        DbContextOptionsBuilder<AspNetDbContext> optionsBuilder,
        IConfiguration configuration
        )
    {
        // Get the connection string from the DAL section.
        var connectionString = configuration["DAL:SQLServer:ConnectionString"];
        if (string.IsNullOrEmpty(connectionString))
        {
            // Panic!!
            throw new ArgumentException(
                message: "The connection string at DAL:SQLServer:ConnectionString, " +
                "in the appSettings, json file is required for migrations, " +
                "but is currently missing, or empty!"
                );
        }

        // Configure the data-context options using the connection string
        //   and our migrations assembly.
        optionsBuilder.UseSqlServer(
            connectionString,
            sqlServerOptions =>
            {
                sqlServerOptions.MigrationsAssembly(
                    Assembly.GetExecutingAssembly().GetName().Name
                    );
            });
    }

    #endregion
}
