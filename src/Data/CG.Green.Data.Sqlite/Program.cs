using CG.Green.Identity.Models;

// The code below is required, for migrations, because the Duende library expects its
//   data-contexts to be registered as part of the application startup, which it also
//   expects to include standing up all the associated identity services. So, if we
//   don't do this, when we try to run a migration command, in either Visual Studio
//   or the command line, Duende looks for at least some of this information and,
//   when it fails to find it, then decides it can't find its own data-contexts and
//   the entire migration command fails.

// We didn't stand up a factory for the Duende data-contexts because they rely on
//   at least some of the services we're registering in the code below, and I don't
//   care to go spelunking around to find out what's needed, in a Duende design-time
//   factory, to get it to work for migrations.

// We did stand up a design-time factory for the ASP.NET data-context, because that
//   library doesn't do, whatever the Duende library is doing, to require the code 
//   below. For migrations, ASP.NET identity works just fine with a plain old EFCORE 
//   design-time factory.
//
// We stand up the ASP.NET identity stuff, in the code below, because the Duende library
//   is looking for the claims factory, which is part of the ASP.NET registration - hence
//   its addition in the code below.

new HostBuilder()
    .ConfigureHostConfiguration(builder =>
    {
        builder.AddJsonFile("appsettings.json");
    })
    .ConfigureServices((context, services) =>
    {
        // Get the connection string from the DAL section. We don't need all the options
        //   from the configuration, just this connection string.
        var connectionString = context.Configuration["DAL:SQLite:ConnectionString"];
        if (string.IsNullOrEmpty(connectionString))
        {
            // Panic!!
            throw new ArgumentException(
                message: "The connection string at DAL:SQLite:ConnectionString, " +
                "in the appSettings, json file is required for migrations, " +
                "but is currently missing, or empty!"
                );
        }

        // Because the Duende library uses the IUserClaimsPrincipalFactory, for
        //   migrations, and that abstraction is registered using this code.
        services.AddIdentity<GreenUser, GreenRole>()
            .AddEntityFrameworkStores<AspNetDbContext>()
            .AddDefaultTokenProviders();

        // Because the Duende library is using some of these services, for migrations,
        //   and the only other way to do this is by writing a design-time factory 
        //   that's smart enough to simulate whatever Duende is up to. This is easier.
        services.AddIdentityServer()
            .AddAspNetIdentity<GreenUser>()
            .AddConfigurationStore(options =>
            {
                options.DefaultSchema = "Green";
                options.ConfigureDbContext = b => b.UseSqlite(
                    connectionString,
                    dbOpts => dbOpts.MigrationsAssembly(typeof(Program).Assembly.FullName)
                    );
            })
            .AddOperationalStore(options =>
            {
                options.DefaultSchema = "Green";
                options.ConfigureDbContext = b => b.UseSqlite(
                    connectionString,
                    dbOpts => dbOpts.MigrationsAssembly(typeof(Program).Assembly.FullName)
                    );
            });
    }).Build();
