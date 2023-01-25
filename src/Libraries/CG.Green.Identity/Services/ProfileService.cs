
namespace CG.Green.Services
{
    /// <summary>
    /// This class is a custom implementation of <see cref="DefaultProfileService"/>
    /// </summary>
    public class ProfileService : DefaultProfileService
    {
        // *******************************************************************
        // Constructors.
        // *******************************************************************

        #region Constructors

        /// <summary>
        /// This constructor creates a new instance of the <see cref="ProfileService"/>
        /// class.
        /// </summary>
        /// <param name="logger">The logger to use with this service.</param>
        public ProfileService(
            ILogger<ProfileService> logger
            ) : base(logger)
        {

        }

        #endregion

        // *******************************************************************
        // Public methods.
        // *******************************************************************

        #region Public methods

        /// <summary>
        /// This method adds custom claims to the profile.
        /// </summary>
        /// <param name="context">The context to use for the operation.</param>
        /// <returns>A task to perform the operation.</returns>
        public override async Task GetProfileDataAsync(
            ProfileDataRequestContext context
            )
        {
            // Give the base class a chance.
            await base.GetProfileDataAsync(context);

            // Add the role(s) from the subject
            var roleClaims = context.Subject.FindAll(JwtClaimTypes.Role);
            context.IssuedClaims.AddRange(roleClaims);

            // Add the name from the subject
            var nameClaims = context.Subject.FindAll(JwtClaimTypes.Name);
            context.IssuedClaims.AddRange(nameClaims);

            // Loop through the client claims.
            foreach (var clientClaim in context.Client.Claims)
            {
                // Add the claim.
                context.IssuedClaims.Add(new Claim(
                    clientClaim.Type, 
                    clientClaim.Value)
                    );
            }
        }

        #endregion
    }
}
