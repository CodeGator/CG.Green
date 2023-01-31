
namespace CG.Green.Managers;

// <summary>
/// This interface represents an object that manages operations related to
/// <see cref="GreenRoleClaim"/> objects.
/// </summary>
public interface IGreenRoleClaimManager
{
    /// <summary>
    /// This method indicates whether there are any <see cref="GreenRoleClaim"/> objects
    /// in the underlying storage.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token that is monitored
    /// for the lifetime of the method.</param>
    /// <returns>A task to perform the operation that returns <c>true</c> if there
    /// are any <see cref="GreenRoleClaim"/> objects; <c>false</c> otherwise.</returns>
    /// <exception cref="ManagerException">This exception is thrown whenever the
    /// manager fails to complete the operation.</exception>
    Task<bool> AnyAsync(
        CancellationToken cancellationToken = default
        );

    /// <summary>
    /// This method counts the number of <see cref="GreenRoleClaim"/> objects in the 
    /// underlying storage.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token that is monitored
    /// for the lifetime of the method.</param>
    /// <returns>A task to perform the operation that returns a count of the 
    /// number of <see cref="GreenRoleClaim"/> objects in the underlying storage.</returns>
    /// <exception cref="ManagerException">This exception is thrown whenever the
    /// manager fails to complete the operation.</exception>
    Task<long> CountAsync(
        CancellationToken cancellationToken = default
        );

    /// <summary>
    /// This method creates a new <see cref="GreenRoleClaim"/> object in the 
    /// underlying storage.
    /// </summary>
    /// <param name="roleClaim">The model to use for the operation.</param>
    /// <param name="userName">The user name of the person performing the 
    /// operation.</param>
    /// <param name="cancellationToken">A cancellation token that is monitored
    /// for the lifetime of the method.</param>
    /// <returns>A task to perform the operation that returns the newly created
    /// <see cref="GreenRoleClaim"/> object.</returns>
    /// <exception cref="ArgumentException">This exception is thrown whenever one
    /// or more arguments are missing, or invalid.</exception>
    /// <exception cref="ManagerException">This exception is thrown whenever the
    /// manager fails to complete the operation.</exception>
    Task<GreenRoleClaim> CreateAsync(
        GreenRoleClaim roleClaim,
        string userName,
        CancellationToken cancellationToken = default
        );

    /// <summary>
    /// This method searches for claims that are associated with the given role.
    /// </summary>
    /// <param name="roleId">The identifier to use for the operation.</param>
    /// <param name="cancellationToken">A cancellation token that is monitored
    /// for the lifetime of the method.</param>
    /// <returns>A task to perform the operation that returns a list of matching 
    /// <see cref="GreenRoleClaim"/> objects.</returns>
    /// <exception cref="ArgumentException">This exception is thrown whenever one
    /// or more arguments are missing, or invalid.</exception>
    /// <exception cref="ManagerException">This exception is thrown whenever the
    /// manager fails to complete the operation.</exception>
    Task<IEnumerable<GreenRoleClaim>> FindByRoleIdAsync(
        string roleId,
        CancellationToken cancellationToken = default
        );

    /// <summary>
    /// This method updates the claims for the given <see cref="GreenRole"/> object,
    /// in the underlying storage.
    /// </summary>
    /// <param name="greenRole">The role to use for the operation.</param>
    /// <param name="userClaims">The models to updated in the underlying storage.</param>
    /// <param name="userName">The user name of the person performing the 
    /// operation.</param>
    /// <param name="cancellationToken">A cancellation token that is monitored
    /// for the lifetime of the method.</param>
    /// <returns>A task to perform the operation that returns the newly updated
    /// <see cref="GreenRole"/> object.</returns>
    /// <exception cref="ArgumentException">This exception is thrown whenever one
    /// or more arguments are missing, or invalid.</exception>
    /// <exception cref="ManagerException">This exception is thrown whenever the
    /// manager fails to complete the operation.</exception>
    Task<GreenRole> UpdateAsync(
        GreenRole greenRole,
        IEnumerable<GreenRoleClaim> userClaims,
        string userName,
        CancellationToken cancellationToken = default
        );
}
