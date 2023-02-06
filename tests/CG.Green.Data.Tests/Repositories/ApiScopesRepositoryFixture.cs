
namespace CG.Green.Data.Repositories;

/// <summary>
/// This class is a test fixture for the <see cref="ApiScopeRepository"/>
/// class.
/// </summary>
[TestClass]
public class ApiScopesRepositoryFixture
{
    // *******************************************************************
    // Fields.
    // *******************************************************************

    #region Fields

    /// <summary>
    /// This field contains the service provider for this fixture.
    /// </summary>
    internal protected IServiceProvider _serviceProvider = null!;

    #endregion

    // *******************************************************************
    // Public methods.
    // *******************************************************************

    #region Public methods

    /// <summary>
    /// This method initializes the test fixture before a test run.
    /// </summary>
    [TestInitialize]
    public void Initialize()
    {
        // Create a DI container to support the Duende data contexts.
        var services = new ServiceCollection();
        services.AddSingleton<ConfigurationStoreOptions>();

        // Create the service provider.
        _serviceProvider = services.BuildServiceProvider();
    }

    // *******************************************************************

    /// <summary>
    /// This method ensures the <see cref="ApiScopeRepository.ApiScopeRepository(ConfigurationDbContext, ILogger{IApiScopeRepository})"/>
    /// constructor properly initializes object instances.
    /// </summary>
    [TestMethod]
    public void ApiScopeRepository_ctor()
    {
        // Arrange ...
        var builder = new DbContextOptionsBuilder<ConfigurationDbContext>();
        builder.UseInMemoryDatabase($"{Guid.NewGuid()}")
            .UseApplicationServiceProvider(_serviceProvider);
        using var dbContext = new ConfigurationDbContext(builder.Options);

        var logger = new Mock<ILogger<IApiScopeRepository>>();

        // Act ...
        var result = new ApiScopeRepository(
            dbContext,
            logger.Object
            );

        // Assert ...
        Assert.IsTrue(
            result._configurationDbContext == dbContext,
            "The _configurationDbContext field was invalid"
            );
        Assert.IsTrue(
            result._logger == logger.Object,
            "The _logger field was invalid"
            );
    }

    // *******************************************************************

    /// <summary>
    /// This method ensures the <see cref="ApiScopeRepository.AnyAsync(CancellationToken)"/>
    /// method returns the proper value.
    /// </summary>
    [TestMethod]
    public async Task ApiScopeRepository_AnyAsync()
    {
        // Arrange ...
        var builder = new DbContextOptionsBuilder<ConfigurationDbContext>();
        builder.UseInMemoryDatabase($"{Guid.NewGuid()}")
            .UseApplicationServiceProvider(_serviceProvider);
        using var dbContext = new ConfigurationDbContext(builder.Options);

        dbContext.ApiScopes.Add(new Duende.IdentityServer.EntityFramework.Entities.ApiScope()
        {
            Name = "test"
        });
        dbContext.SaveChanges();

        var logger = new Mock<ILogger<IApiScopeRepository>>();

        var repository = new ApiScopeRepository(
            dbContext,
            logger.Object
            );

        // Act ...
        var result = await repository.AnyAsync();

        // Assert ...
        Assert.IsTrue(
            result,
            "The return value was invalid"
            );

        Mock.Verify(
            logger
            );
    }

    // *******************************************************************

    /// <summary>
    /// This method ensures the <see cref="ApiScopeRepository.CountAsync(CancellationToken)"/>
    /// method returns the proper value.
    /// </summary>
    [TestMethod]
    public async Task ApiScopeRepository_CountAsync()
    {
        // Arrange ...
        var builder = new DbContextOptionsBuilder<ConfigurationDbContext>();
        builder.UseInMemoryDatabase($"{Guid.NewGuid()}")
            .UseApplicationServiceProvider(_serviceProvider);
        using var dbContext = new ConfigurationDbContext(builder.Options);

        dbContext.ApiScopes.Add(new Duende.IdentityServer.EntityFramework.Entities.ApiScope()
        {
            Name = "test"
        });
        dbContext.SaveChanges();

        var logger = new Mock<ILogger<IApiScopeRepository>>();

        var repository = new ApiScopeRepository(
            dbContext,
            logger.Object
            );

        // Act ...
        var result = await repository.CountAsync();

        // Assert ...
        Assert.IsTrue(
            result == 1,
            "The return value was invalid"
            );

        Mock.Verify(
            logger
            );
    }

    // *******************************************************************

    /// <summary>
    /// This method ensures the <see cref="ApiScopeRepository.CreateAsync(Duende.IdentityServer.Models.ApiScope, CancellationToken)"/>
    /// method stores the model and returns the proper value.
    /// </summary>
    [TestMethod]
    public async Task ApiScopeRepository_CreateAsync()
    {
        // Arrange ...
        var builder = new DbContextOptionsBuilder<ConfigurationDbContext>();
        builder.UseInMemoryDatabase($"{Guid.NewGuid()}")
            .UseApplicationServiceProvider(_serviceProvider);
        using var dbContext = new ConfigurationDbContext(builder.Options);

        var logger = new Mock<ILogger<IApiScopeRepository>>();

        var repository = new ApiScopeRepository(
            dbContext,
            logger.Object
            );

        var data = new Duende.IdentityServer.Models.ApiScope()
        {
            Name = "test"
        };

        // Act ...
        var result = await repository.CreateAsync(data);

        // Assert ...
        Assert.IsTrue(
            result.Name == data.Name,
            "The return value was invalid"
            );
        Assert.IsTrue(
            dbContext.ApiScopes.Count() == 1,
            "The model wasn't stored"
            );

        Mock.Verify(
            logger
            );
    }

    // *******************************************************************

    /// <summary>
    /// This method ensures the <see cref="ApiScopeRepository.DeleteAsync(Duende.IdentityServer.Models.ApiScope, CancellationToken)"/>
    /// method stores the model and returns the proper value.
    /// </summary>
    [TestMethod]
    public async Task ApiScopeRepository_DeleteAsync()
    {
        // Arrange ...
        var builder = new DbContextOptionsBuilder<ConfigurationDbContext>();
        builder.UseInMemoryDatabase($"{Guid.NewGuid()}")
            .UseApplicationServiceProvider(_serviceProvider);
        using var dbContext = new ConfigurationDbContext(builder.Options);

        var logger = new Mock<ILogger<IApiScopeRepository>>();

        var repository = new ApiScopeRepository(
            dbContext,
            logger.Object
            );

        dbContext.ApiScopes.Add(new Duende.IdentityServer.EntityFramework.Entities.ApiScope()
        {
            Name = "test"
        });
        dbContext.SaveChanges();

        // Act ...
        await repository.DeleteAsync(new Duende.IdentityServer.Models.ApiScope()
        {
            Name = "test"
        });

        // Assert ...
        Assert.IsTrue(
            dbContext.ApiScopes.Count() == 0,
            "The model wasn't deleted"
            );

        Mock.Verify(
            logger
            );
    }

    // *******************************************************************

    /// <summary>
    /// This method ensures the <see cref="ApiScopeRepository.FindAllAsync(CancellationToken)"/>
    /// method returns the proper value.
    /// </summary>
    [TestMethod]
    public async Task ApiScopeRepository_FindAllAsync()
    {
        // Arrange ...
        var builder = new DbContextOptionsBuilder<ConfigurationDbContext>();
        builder.UseInMemoryDatabase($"{Guid.NewGuid()}")
            .UseApplicationServiceProvider(_serviceProvider);
        using var dbContext = new ConfigurationDbContext(builder.Options);

        var logger = new Mock<ILogger<IApiScopeRepository>>();

        var repository = new ApiScopeRepository(
            dbContext,
            logger.Object
            );

        dbContext.ApiScopes.Add(new Duende.IdentityServer.EntityFramework.Entities.ApiScope()
        {
            Name = "test"
        });
        dbContext.SaveChanges();

        // Act ...
        var result = await repository.FindAllAsync();

        // Assert ...
        Assert.IsTrue(
            result.Count() == 1,
            "The return value was invalid"
            );

        Mock.Verify(
            logger
            );
    }

    // *******************************************************************

    /// <summary>
    /// This method ensures the <see cref="ApiScopeRepository.FindByNameAsync(string, CancellationToken)"/>
    /// method returns the proper value.
    /// </summary>
    [TestMethod]
    public async Task ApiScopeRepository_FindByNameAsync()
    {
        // Arrange ...
        var builder = new DbContextOptionsBuilder<ConfigurationDbContext>();
        builder.UseInMemoryDatabase($"{Guid.NewGuid()}")
            .UseApplicationServiceProvider(_serviceProvider);
        using var dbContext = new ConfigurationDbContext(builder.Options);

        var logger = new Mock<ILogger<IApiScopeRepository>>();

        var repository = new ApiScopeRepository(
            dbContext,
            logger.Object
            );

        dbContext.ApiScopes.Add(new Duende.IdentityServer.EntityFramework.Entities.ApiScope()
        {
            Name = "test"
        });
        dbContext.SaveChanges();

        // Act ...
        var result = await repository.FindByNameAsync("test");

        // Assert ...
        Assert.IsTrue(
            result != null,
            "The return value was invalid"
            );

        Mock.Verify(
            logger
            );
    }

    // *******************************************************************

    /// <summary>
    /// This method ensures the <see cref="ApiScopeRepository.UpdateAsync(Duende.IdentityServer.Models.ApiScope, CancellationToken)"/>
    /// updates the model and returns the proper value.
    /// </summary>
    [TestMethod]
    public async Task ApiScopeRepository_UpdateAsync()
    {
        // Arrange ...
        var builder = new DbContextOptionsBuilder<ConfigurationDbContext>();
        builder.UseInMemoryDatabase($"{Guid.NewGuid()}")
            .UseApplicationServiceProvider(_serviceProvider);
        using var dbContext = new ConfigurationDbContext(builder.Options);

        var logger = new Mock<ILogger<IApiScopeRepository>>();

        var repository = new ApiScopeRepository(
            dbContext,
            logger.Object
            );

        dbContext.ApiScopes.Add(new Duende.IdentityServer.EntityFramework.Entities.ApiScope()
        {
            Name = "test"
        });
        dbContext.SaveChanges();

        // Act ...
        var result = await repository.UpdateAsync(new Duende.IdentityServer.Models.ApiScope()
        {
            Name = "test",
            Description = "test"
        });

        // Assert ...
        Assert.IsTrue(
            result != null,
            "The return value was invalid"
            );
        Assert.IsTrue(
            result.Description == "test",
            "The model wasn't updated"
            );

        Mock.Verify(
            logger
            );
    }

    #endregion
}
