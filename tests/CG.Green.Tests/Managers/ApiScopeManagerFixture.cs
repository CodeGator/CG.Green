
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

    #endregion
}
