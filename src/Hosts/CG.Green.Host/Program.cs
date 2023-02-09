using Serilog;

// Uncomment this line to help troubleshoot startup problems.
//BootstrapLogger.LogLevelToDebug();

try
{
    // Log what we are about to do.
    BootstrapLogger.Instance().LogInformation(
        "Starting up {name}",
        AppDomain.CurrentDomain.FriendlyName
        );

    // Create an application builder.
    var builder = WebApplication.CreateBuilder(args);

    // Log what we are about to do.
    BootstrapLogger.Instance().LogDebug(
        "Adding serilog, for logging"
        );

    // Add Serilog stuff.
    builder.Host.UseSerilog((ctx, lc) =>
    {
        lc.ReadFrom.Configuration(ctx.Configuration);
    });

    // Log what we are about to do.
    BootstrapLogger.Instance().LogDebug(
        "Adding Blazor startup"
        );

    // Add Blazor stuff.
    builder.Services.AddRazorPages();
    builder.Services.AddServerSideBlazor();
    builder.Services.AddHttpContextAccessor();

    // Log what we are about to do.
    BootstrapLogger.Instance().LogDebug(
        "Adding MudBlazor startup"
        );

    // Add MudBlazor stuff
    builder.Services.AddMudServices(options =>
    {
        options.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.TopCenter;
        options.SnackbarConfiguration.PreventDuplicates = true;
        options.SnackbarConfiguration.ClearAfterNavigation = true;
    });

    // Log what we are about to do.
    BootstrapLogger.Instance().LogDebug(
        "Adding Green startup"
        );

    // Add Green stuff.
    builder.AddGreenRepositories(bootstrapLogger: BootstrapLogger.Instance())
        .AddGreenManagers(bootstrapLogger: BootstrapLogger.Instance())
        .AddGreenIdentity(bootstrapLogger: BootstrapLogger.Instance());

    // Log what we are about to do.
    BootstrapLogger.Instance().LogDebug(
        "Adding CodeGator startup"
        );

    // Add CodeGator stuff
    builder.AddDataAccess(bootstrapLogger: BootstrapLogger.Instance())
        .AddSeeding<SeedDirector>(bootstrapLogger: BootstrapLogger.Instance())
        .AddBlazorPlugins(bootstrapLogger: BootstrapLogger.Instance());

    // Log what we are about to do.
    BootstrapLogger.Instance().LogDebug(
        "Building the application instance"
        );

    // Build the application.
    var app = builder.Build();

    // Setup the proper environment.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Error");
        app.UseHsts();
    }
    else
    {
        app.UseSerilogRequestLogging();
    }

    // Use Blazor stuff.
    app.UseHttpsRedirection();
    app.UseStaticFiles();
    app.UseRouting();
    app.MapBlazorHub();
    app.MapFallbackToPage("/_Host");
        
    // Use CodeGator stuff.
    app.UseDataAccess()
        .UseSeeding()
        .UseBlazorPlugins();

    // Use Green stuff.
    app.UseGreenIdentity();

    // Run the application.
    app.Run();
}
catch (Exception ex)
{
    // Log the error.
    BootstrapLogger.Instance().LogCritical(
        ex,
        "Unhandled exception: {msg}!",
        ex.GetBaseException().Message
        );
}
finally
{
    // Log what we are doing.
    BootstrapLogger.Instance().LogInformation(
        "Shutting down"
        );
}
