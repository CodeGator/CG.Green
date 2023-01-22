
namespace CG.Green;

/// <summary>
/// This class contains global compile-time constants for the <see cref="CG.Green"/>
/// microservice.
/// </summary>
public static class Globals
{
    /// <summary>
    /// This class contains model property sizes.
    /// </summary>
    public static class Models
    {
        /// <summary>
        /// This class contains sizes for <see cref="Client"/> properties.
        /// </summary>
        public static class Clients
        {
            /// <summary>
            /// This constant represents the length of the <see cref="Client.ClientName"/> 
            /// property.
            /// </summary>
            public const int ClientNameLength = 200;

            /// <summary>
            /// This constant represents the length of the <see cref="Client.ClientId"/> 
            /// property.
            /// </summary>
            public const int ClientIdLength = 200;
        }
    }
}
