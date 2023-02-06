
namespace CG.Green.Managers;

/// <summary>
/// This class is a test fixture for the <see cref="ApiScopeManager"/>
/// class.
/// </summary>
[TestClass]
public class ApiScopeManagerFixture
{
    // *******************************************************************
    // Public methods.
    // *******************************************************************

    #region Public methods

    /// <summary>
    /// This method ensures the <see cref="ApiScopeManager.ApiScopeManager(Repositories.IApiScopeRepository, Microsoft.Extensions.Logging.ILogger{IApiScopeManager})"/>
    /// constructor properly initializes object instances.
    /// </summary>
    [TestMethod]
    public void ApiScopeManager_ctor()
    {
        // Arrange ...
        var apiScopeRepository = new Mock<IApiScopeRepository>();
        var logger = new Mock<ILogger<IApiScopeManager>>();

        // Act ...
        var result = new ApiScopeManager(
            apiScopeRepository.Object,
            logger.Object
            );

        // Assert ...
        Assert.IsTrue(
            result._apiScopeRepository == apiScopeRepository.Object,
            "The _apiScopeRepository property wasn't initialized"
            );
        Assert.IsTrue(
            result._logger == logger.Object,
            "The _logger property wasn't initialized"
            );

        Mock.Verify(
            apiScopeRepository,
            logger
            );
    }

    // *******************************************************************

    /// <summary>
    /// This method ensures the <see cref="ApiScopeManager.AnyAsync(CancellationToken)"/>
    /// method calls the proper repository method(s) and returns the correct
    /// return value.
    /// </summary>
    [TestMethod]
    public async Task ApiScopeManager_AnyAsync()
    {
        // Arrange ...
        var apiScopeRepository = new Mock<IApiScopeRepository>();
        var logger = new Mock<ILogger<IApiScopeManager>>();

        apiScopeRepository.Setup(x => x.AnyAsync(
            It.IsAny<CancellationToken>()
            )).ReturnsAsync(true)
            .Verifiable();

        var manager = new ApiScopeManager(
            apiScopeRepository.Object,
            logger.Object
            );

        // Act ...
        var result = await manager.AnyAsync()
            .ConfigureAwait(false);

        // Assert ...
        Assert.IsTrue(
            result,
            "The return value was invalid"
            );

        Mock.Verify(
            apiScopeRepository,
            logger
            );
    }

    // *******************************************************************

    /// <summary>
    /// This method ensures the <see cref="ApiScopeManager.CountAsync(CancellationToken)"/>
    /// method calls the proper repository method(s) and returns the correct
    /// return value.
    /// </summary>
    [TestMethod]
    public async Task ApiScopeManager_CountAsync()
    {
        // Arrange ...
        var apiScopeRepository = new Mock<IApiScopeRepository>();
        var logger = new Mock<ILogger<IApiScopeManager>>();

        apiScopeRepository.Setup(x => x.CountAsync(
            It.IsAny<CancellationToken>()
            )).ReturnsAsync(1)
            .Verifiable();

        var manager = new ApiScopeManager(
            apiScopeRepository.Object,
            logger.Object
            );

        // Act ...
        var result = await manager.CountAsync()
            .ConfigureAwait(false);

        // Assert ...
        Assert.IsTrue(
            result == 1,
            "The return value was invalid"
            );

        Mock.Verify(
            apiScopeRepository,
            logger
            );
    }

    // *******************************************************************

    /// <summary>
    /// This method ensures the <see cref="ApiScopeManager.CreateAsync(Duende.IdentityServer.Models.ApiScope, string, CancellationToken)"/>
    /// method calls the proper repository method(s) and returns the correct
    /// return value.
    /// </summary>
    [TestMethod]
    public async Task ApiScopeManager_CreateAsync()
    {
        // Arrange ...
        var apiScopeRepository = new Mock<IApiScopeRepository>();
        var logger = new Mock<ILogger<IApiScopeManager>>();
        var data = new Duende.IdentityServer.Models.ApiScope();

        apiScopeRepository.Setup(x => x.CreateAsync(
            It.IsAny<Duende.IdentityServer.Models.ApiScope>(),
            It.IsAny<CancellationToken>()
            )).ReturnsAsync(data)
            .Verifiable();

        var manager = new ApiScopeManager(
            apiScopeRepository.Object,
            logger.Object
            );

        // Act ...
        var result = await manager.CreateAsync(
            new Duende.IdentityServer.Models.ApiScope(),
            "test"
            ).ConfigureAwait(false);

        // Assert ...
        Assert.IsTrue(
            result == data,
            "The return value was invalid"
            );

        Mock.Verify(
            apiScopeRepository,
            logger
            );
    }

    // *******************************************************************

    /// <summary>
    /// This method ensures the <see cref="ApiScopeManager.DeleteAsync(Duende.IdentityServer.Models.ApiScope, string, CancellationToken)"/>
    /// method calls the proper repository method(s) and returns the correct
    /// return value.
    /// </summary>
    [TestMethod]
    public async Task ApiScopeManager_DeleteAsync()
    {
        // Arrange ...
        var apiScopeRepository = new Mock<IApiScopeRepository>();
        var logger = new Mock<ILogger<IApiScopeManager>>();

        apiScopeRepository.Setup(x => x.DeleteAsync(
            It.IsAny<Duende.IdentityServer.Models.ApiScope>(),
            It.IsAny<CancellationToken>()
            )).Verifiable();

        var manager = new ApiScopeManager(
            apiScopeRepository.Object,
            logger.Object
            );

        // Act ...
        await manager.DeleteAsync(
            new Duende.IdentityServer.Models.ApiScope(),
            "test"
            ).ConfigureAwait(false);

        // Assert ...

        Mock.Verify(
            apiScopeRepository,
            logger
            );
    }

    // *******************************************************************

    /// <summary>
    /// This method ensures the <see cref="ApiScopeManager.FindAllAsync(CancellationToken)"/>
    /// method calls the proper repository method(s) and returns the correct
    /// return value.
    /// </summary>
    [TestMethod]
    public async Task ApiScopeManager_FindAllAsync()
    {
        // Arrange ...
        var apiScopeRepository = new Mock<IApiScopeRepository>();
        var logger = new Mock<ILogger<IApiScopeManager>>();
        var data = Array.Empty<ApiScope>();

        apiScopeRepository.Setup(x => x.FindAllAsync(
            It.IsAny<CancellationToken>()
            )).ReturnsAsync(data)
            .Verifiable();

        var manager = new ApiScopeManager(
            apiScopeRepository.Object,
            logger.Object
            );

        // Act ...
        var result = await manager.FindAllAsync()
            .ConfigureAwait(false);

        // Assert ...
        Assert.IsTrue(
            result == data,
            "The return value was invalid"
            );

        Mock.Verify(
            apiScopeRepository,
            logger
            );
    }

    // *******************************************************************

    /// <summary>
    /// This method ensures the <see cref="ApiScopeManager.FindByNameAsync(string, CancellationToken)"/>
    /// method calls the proper repository method(s) and returns the correct
    /// return value.
    /// </summary>
    [TestMethod]
    public async Task ApiScopeManager_FindByNameAsync()
    {
        // Arrange ...
        var apiScopeRepository = new Mock<IApiScopeRepository>();
        var logger = new Mock<ILogger<IApiScopeManager>>();
        var data = new ApiScope();

        apiScopeRepository.Setup(x => x.FindByNameAsync(
            It.IsAny<string>(),
            It.IsAny<CancellationToken>()
            )).ReturnsAsync(data)
            .Verifiable();

        var manager = new ApiScopeManager(
            apiScopeRepository.Object,
            logger.Object
            );

        // Act ...
        var result = await manager.FindByNameAsync(
            "test"
            ).ConfigureAwait(false);

        // Assert ...
        Assert.IsTrue(
            result == data,
            "The return value was invalid"
            );

        Mock.Verify(
            apiScopeRepository,
            logger
            );
    }

    // *******************************************************************

    /// <summary>
    /// This method ensures the <see cref="ApiScopeManager.UpdateAsync(ApiScope, string, CancellationToken)"/>
    /// method calls the proper repository method(s) and returns the correct
    /// return value.
    /// </summary>
    [TestMethod]
    public async Task ApiScopeManager_UpdateAsync()
    {
        // Arrange ...
        var apiScopeRepository = new Mock<IApiScopeRepository>();
        var logger = new Mock<ILogger<IApiScopeManager>>();
        var data = new ApiScope();

        apiScopeRepository.Setup(x => x.UpdateAsync(
            It.IsAny<ApiScope>(),
            It.IsAny<CancellationToken>()
            )).ReturnsAsync(data)
            .Verifiable();

        var manager = new ApiScopeManager(
            apiScopeRepository.Object,
            logger.Object
            );

        // Act ...
        var result = await manager.UpdateAsync(
            new ApiScope(),
            "test"
            ).ConfigureAwait(false);

        // Assert ...
        Assert.IsTrue(
            result == data,
            "The return value was invalid"
            );

        Mock.Verify(
            apiScopeRepository,
            logger
            );
    }

    #endregion
}
