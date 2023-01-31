
namespace CG.Green;

/// <summary>
/// This class contains global compile-time constants for the <see cref="CG.Green"/>
/// microservice.
/// </summary>
public static class Globals
{
    /// <summary>
    /// This constant represents the caption for the application.
    /// </summary>
    public const string Caption = "Green";

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

            /// <summary>
            /// This constant represents the length of the <see cref="Client.FrontChannelLogoutUri"/> 
            /// property.
            /// </summary>
            public const int FrontChannelLogoutUriLength = 2000;
        }

        /// <summary>
        /// This class contains sizes for <see cref="GreenUser"/> properties.
        /// </summary>
        public static class Users
        {
            /// <summary>
            /// This constant represents the length of the <see cref="GreenUser.UserName"/> 
            /// property.
            /// </summary>
            public const int UserNameLength = 256;

            /// <summary>
            /// This constant represents the length of the <see cref="GreenUser.PasswordHash"/> 
            /// property.
            /// </summary>
            public const int PasswordHashLength = 100;

            /// <summary>
            /// This constant represents the length of the <see cref="GreenUser.Email"/> 
            /// property.
            /// </summary>
            public const int EmailLength = 256;
        }

        /// <summary>
        /// This class contains sizes for <see cref="GreenUserRole"/> properties.
        /// </summary>
        public static class Roles
        {
            /// <summary>
            /// This constant represents the length of a role id.
            /// property.
            /// </summary>
            public const int IdLength = 256;

            /// <summary>
            /// This constant represents the length of a role name.
            /// property.
            /// </summary>
            public const int NameLength = 256;
        }
    }
}

