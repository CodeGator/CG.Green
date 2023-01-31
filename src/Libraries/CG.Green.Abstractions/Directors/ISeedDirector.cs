
namespace CG.Green.Directors;

/// <summary>
/// This interface represents an object that performs data seeding operations.
/// </summary>
public interface ISeedDirector
{
    /// <summary>
    /// This method performs a seeding operation for <see cref="ApiScope"/>
    /// objects.
    /// </summary>
    /// <param name="apiScopes">The data to use for the operation.</param>
    /// <param name="userName">The name of the user performing the operation.</param>
    /// <param name="force"><c>true</c> to force the seeding operation when 
    /// there are existing entities in the underlying data-store; <c>false</c> 
    /// otherwise.</param>
    /// <param name="cancellationToken">A cancellation token that is monitored
    /// for the lifetime of the method.</param>
    /// <returns>A task to perform the operation.</returns>
    /// <exception cref="ArgumentException">This exception is thrown whenever one
    /// or more arguments are missing, or invalid.</exception>
    /// <exception cref="DirectorException">This exception is thrown whenever the
    /// director fails to complete the operation.</exception>
    Task SeedApiScopesAsync(
        List<ApiScope> apiScopes,
        string userName,
        bool force = false,
        CancellationToken cancellationToken = default
        );

    /// <summary>
    /// This method performs a seeding operation for <see cref="Client"/>
    /// objects.
    /// </summary>
    /// <param name="clients">The data to use for the operation.</param>
    /// <param name="userName">The name of the user performing the operation.</param>
    /// <param name="force"><c>true</c> to force the seeding operation when 
    /// there are existing entities in the underlying data-store; <c>false</c> 
    /// otherwise.</param>
    /// <param name="cancellationToken">A cancellation token that is monitored
    /// for the lifetime of the method.</param>
    /// <returns>A task to perform the operation.</returns>
    /// <exception cref="ArgumentException">This exception is thrown whenever one
    /// or more arguments are missing, or invalid.</exception>
    /// <exception cref="DirectorException">This exception is thrown whenever the
    /// director fails to complete the operation.</exception>
    Task SeedClientsAsync(
        List<Client> clients,
        string userName,
        bool force = false,
        CancellationToken cancellationToken = default
        );

    /// <summary>
    /// This method performs a seeding operation for <see cref="IdentityResource"/>
    /// objects.
    /// </summary>
    /// <param name="users">The data to use for the operation.</param>
    /// <param name="userName">The name of the user performing the operation.</param>
    /// <param name="force"><c>true</c> to force the seeding operation when 
    /// there are existing entities in the underlying data-store; <c>false</c> 
    /// otherwise.</param>
    /// <param name="cancellationToken">A cancellation token that is monitored
    /// for the lifetime of the method.</param>
    /// <returns>A task to perform the operation.</returns>
    /// <exception cref="ArgumentException">This exception is thrown whenever one
    /// or more arguments are missing, or invalid.</exception>
    /// <exception cref="DirectorException">This exception is thrown whenever the
    /// director fails to complete the operation.</exception>
    Task SeedIdentityResourcesAsync(
        List<IdentityResource> users,
        string userName,
        bool force = false,
        CancellationToken cancellationToken = default
        );

    /// <summary>
    /// This method performs a seeding operation for <see cref="GreenRole"/>
    /// objects.
    /// </summary>
    /// <param name="roles">The data to use for the operation.</param>
    /// <param name="userName">The name of the user performing the operation.</param>
    /// <param name="force"><c>true</c> to force the seeding operation when 
    /// there are existing entities in the underlying data-store; <c>false</c> 
    /// otherwise.</param>
    /// <param name="cancellationToken">A cancellation token that is monitored
    /// for the lifetime of the method.</param>
    /// <returns>A task to perform the operation.</returns>
    /// <exception cref="ArgumentException">This exception is thrown whenever one
    /// or more arguments are missing, or invalid.</exception>
    /// <exception cref="DirectorException">This exception is thrown whenever the
    /// director fails to complete the operation.</exception>
    Task SeedRolesAsync(
        List<GreenRole> roles,
        string userName,
        bool force = false,
        CancellationToken cancellationToken = default
        );

    /// <summary>
    /// This method performs a seeding operation for <see cref="RoleClaimOptions"/>
    /// objects.
    /// </summary>
    /// <param name="roleClaims">The data to use for the operation.</param>
    /// <param name="userName">The name of the user performing the operation.</param>
    /// <param name="force"><c>true</c> to force the seeding operation when 
    /// there are existing entities in the underlying data-store; <c>false</c> 
    /// otherwise.</param>
    /// <param name="cancellationToken">A cancellation token that is monitored
    /// for the lifetime of the method.</param>
    /// <returns>A task to perform the operation.</returns>
    /// <exception cref="ArgumentException">This exception is thrown whenever one
    /// or more arguments are missing, or invalid.</exception>
    /// <exception cref="DirectorException">This exception is thrown whenever the
    /// director fails to complete the operation.</exception>
    Task SeedRoleClaimsAsync(
        List<RoleClaimAssignmentOptions> roleClaims,
        string userName,
        bool force = false,
        CancellationToken cancellationToken = default
        );

    /// <summary>
    /// This method performs a seeding operation for <see cref="GreenUser"/>
    /// objects.
    /// </summary>
    /// <param name="roles">The data to use for the operation.</param>
    /// <param name="userName">The name of the user performing the operation.</param>
    /// <param name="force"><c>true</c> to force the seeding operation when 
    /// there are existing entities in the underlying data-store; <c>false</c> 
    /// otherwise.</param>
    /// <param name="cancellationToken">A cancellation token that is monitored
    /// for the lifetime of the method.</param>
    /// <returns>A task to perform the operation.</returns>
    /// <exception cref="ArgumentException">This exception is thrown whenever one
    /// or more arguments are missing, or invalid.</exception>
    /// <exception cref="DirectorException">This exception is thrown whenever the
    /// director fails to complete the operation.</exception>
    Task SeedUsersAsync(
        List<GreenUser> roles,
        string userName,
        bool force = false,
        CancellationToken cancellationToken = default
        );

    /// <summary>
    /// This method performs a seeding operation for <see cref="UserClaimOptions"/>
    /// objects.
    /// </summary>
    /// <param name="userClaims">The data to use for the operation.</param>
    /// <param name="userName">The name of the user performing the operation.</param>
    /// <param name="force"><c>true</c> to force the seeding operation when 
    /// there are existing entities in the underlying data-store; <c>false</c> 
    /// otherwise.</param>
    /// <param name="cancellationToken">A cancellation token that is monitored
    /// for the lifetime of the method.</param>
    /// <returns>A task to perform the operation.</returns>
    /// <exception cref="ArgumentException">This exception is thrown whenever one
    /// or more arguments are missing, or invalid.</exception>
    /// <exception cref="DirectorException">This exception is thrown whenever the
    /// director fails to complete the operation.</exception>
    Task SeedUserClaimsAsync(
        List<UserClaimAssignmentOptions> userClaims,
        string userName,
        bool force = false,
        CancellationToken cancellationToken = default
        );

    /// <summary>
    /// This method performs a seeding operation for <see cref="UserRoleOptions"/>
    /// objects.
    /// </summary>
    /// <param name="userRoles">The data to use for the operation.</param>
    /// <param name="userName">The name of the user performing the operation.</param>
    /// <param name="force"><c>true</c> to force the seeding operation when 
    /// there are existing entities in the underlying data-store; <c>false</c> 
    /// otherwise.</param>
    /// <param name="cancellationToken">A cancellation token that is monitored
    /// for the lifetime of the method.</param>
    /// <returns>A task to perform the operation.</returns>
    /// <exception cref="ArgumentException">This exception is thrown whenever one
    /// or more arguments are missing, or invalid.</exception>
    /// <exception cref="DirectorException">This exception is thrown whenever the
    /// director fails to complete the operation.</exception>
    Task SeedUserRolesAsync(
        List<UserRoleAssignmentOptions> userRoles,
        string userName,
        bool force = false,
        CancellationToken cancellationToken = default
        );
}
