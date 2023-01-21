
namespace CG.Green.Managers;

// <summary>
/// This interface represents an object that manages operations related to
/// <see cref="GreenUserClaim"/> objects.
/// </summary>
public interface IGreenUserClaimManager
{
    /// <summary>
    /// This method indicates whether there are any <see cref="GreenUserClaim"/> objects
    /// in the underlying storage.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token that is monitored
    /// for the lifetime of the method.</param>
    /// <returns>A task to perform the operation that returns <c>true</c> if there
    /// are any <see cref="GreenUserClaim"/> objects; <c>false</c> otherwise.</returns>
    /// <exception cref="ManagerException">This exception is thrown whenever the
    /// manager fails to complete the operation.</exception>
    Task<bool> AnyAsync(
        CancellationToken cancellationToken = default
        );

    /// <summary>
    /// This method counts the number of <see cref="GreenUserClaim"/> objects in the 
    /// underlying storage.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token that is monitored
    /// for the lifetime of the method.</param>
    /// <returns>A task to perform the operation that returns a count of the 
    /// number of <see cref="GreenUserClaim"/> objects in the underlying storage.</returns>
    /// <exception cref="ManagerException">This exception is thrown whenever the
    /// manager fails to complete the operation.</exception>
    Task<long> CountAsync(
        CancellationToken cancellationToken = default
        );

    /// <summary>
    /// This method creates a new <see cref="GreenUserClaim"/> object in the 
    /// underlying storage.
    /// </summary>
    /// <param name="userEmail">The user email to use for the operation.</param>
    /// <param name="claimType">The claim type to use for the operation.</param>
    /// <param name="claimValue">The claim value to use for the operation.</param>
    /// <param name="userName">The user name of the person performing the 
    /// operation.</param>
    /// <param name="cancellationToken">A cancellation token that is monitored
    /// for the lifetime of the method.</param>
    /// <returns>A task to perform the operation that returns the newly created
    /// <see cref="Client"/> object.</returns>
    /// <exception cref="ArgumentException">This exception is thrown whenever one
    /// or more arguments are missing, or invalid.</exception>
    /// <exception cref="ManagerException">This exception is thrown whenever the
    /// manager fails to complete the operation.</exception>
    Task<GreenUserClaim> CreateAsync(
        string userEmail,
        string claimType,
        string claimValue,
        string userName,
        CancellationToken cancellationToken = default
        );

    /// <summary>
    /// This method creates a new <see cref="GreenUserClaim"/> object in the 
    /// underlying storage.
    /// </summary>
    /// <param name="userClaim">The model to use for the operation.</param>
    /// <param name="userName">The user name of the person performing the 
    /// operation.</param>
    /// <param name="cancellationToken">A cancellation token that is monitored
    /// for the lifetime of the method.</param>
    /// <returns>A task to perform the operation that returns the newly created
    /// <see cref="Client"/> object.</returns>
    /// <exception cref="ArgumentException">This exception is thrown whenever one
    /// or more arguments are missing, or invalid.</exception>
    /// <exception cref="ManagerException">This exception is thrown whenever the
    /// manager fails to complete the operation.</exception>
    Task<GreenUserClaim> CreateAsync(
        GreenUserClaim userClaim,
        string userName,
        CancellationToken cancellationToken = default
        );
}
