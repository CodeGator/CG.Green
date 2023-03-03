
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
        /// This constant represents the length of a URI.
        /// </summary>
        public const int UriLength = 400;

        /// <summary>
        /// This class contains sizes for API scope properties.
        /// </summary>
        public static class ApiScopes
        {
            /// <summary>
            /// This constant represents the length of an API scope name.
            /// property.
            /// </summary>
            public const int NameLength = 200;

            /// <summary>
            /// This constant represents the length of an API scope display name.
            /// property.
            /// </summary>
            public const int DisplayNameLength = 200;

            /// <summary>
            /// This constant represents the length of an API scope description.
            /// property.
            /// </summary>
            public const int DescriptionLength = 1000;
        }

        /// <summary>
        /// This class contains sizes for Claim properties.
        /// </summary>
        public static class Claims
        {
            /// <summary>
            /// This constant represents the length of the Type property.
            /// </summary>
            public const int TypeLength = 200;

            /// <summary>
            /// This constant represents the length of the Value property.
            /// </summary>
            public const int ValueLength = 200;
        }

        /// <summary>
        /// This class contains sizes for Client properties.
        /// </summary>
        public static class Clients
        {
			/// <summary>
			/// This constant represents the length of the ClientClaimsPrefix property.
			/// </summary>
			public const int ClientClaimsPrefixLength = 200;

			/// <summary>
			/// This constant represents the length of the SigningAlgorithm property.
			/// </summary>
			public const int SigningAlgorithmLength = 10;

			/// <summary>
			/// This constant represents the length of the ClientName property.
			/// </summary>
			public const int ClientNameLength = 200;

            /// <summary>
            /// This constant represents the length of the ClientId property.
            /// </summary>
            public const int ClientIdLength = 200;

            /// <summary>
            /// This constant represents the length of the ClientUri property.
            /// </summary>
            public const int ClientUriLength = 2000;

            /// <summary>
            /// This constant represents the length of the Description property.
            /// </summary>
            public const int DescriptionLength = 1000;

            /// <summary>
            /// This constant represents the length of the FrontChannelLogoutUri property.
            /// </summary>
            public const int FrontChannelLogoutUriLength = 2000;

            /// <summary>
            /// This constant represents the length of the LogoUri property.
            /// </summary>
            public const int LogoUriLength = 2000;

			/// <summary>
			/// This constant represents the length of an identity provider name.
			/// </summary>
			public const int IdentityProviderNameLength = 200;
		}

        /// <summary>
        /// This class contains sizes for property properties.
        /// </summary>
        public static class Properties
        {
            /// <summary>
            /// This constant represents the length of a property key.
            /// property.
            /// </summary>
            public const int KeyLength = 250;

            /// <summary>
            /// This constant represents the length of a property value.
            /// property.
            /// </summary>
            public const int ValueLength = 200;
        }

        /// <summary>
        /// This class contains sizes for identity resources.
        /// </summary>
        public static class Resources
        {
            /// <summary>
            /// This constant represents the length of a resource name.
            /// property.
            /// </summary>
            public const int NameLength = 200;

            /// <summary>
            /// This constant represents the length of a resource display name.
            /// property.
            /// </summary>
            public const int DisplayNameLength = 200;

            /// <summary>
            /// This constant represents the length of a resource description.
            /// property.
            /// </summary>
            public const int DescriptionLength = 1000;
        }

        /// <summary>
        /// This class contains sizes for GreenUserRole properties.
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

        /// <summary>
        /// This class contains sizes for Duende Secret properties.
        /// </summary>
        public static class Secrets
        {
            /// <summary>
            /// This constant represents the length of a description.
            /// property.
            /// </summary>
            public const int DescriptionLength = 2000;

            /// <summary>
            /// This constant represents the length of a value.
            /// property.
            /// </summary>
            public const int ValueLength = 4000;

            /// <summary>
            /// This constant represents the length of a type.
            /// property.
            /// </summary>
            public const int TypeLength = 250;
        }

        /// <summary>
        /// This class contains sizes for GreenUser properties.
        /// </summary>
        public static class Users
        {
            /// <summary>
            /// This constant represents the length of the UserName property.
            /// </summary>
            public const int UserNameLength = 256;

            /// <summary>
            /// This constant represents the length of the PasswordHash property.
            /// </summary>
            public const int PasswordHashLength = 100;

            /// <summary>
            /// This constant represents the length of the Email property.
            /// </summary>
            public const int EmailLength = 256;
        }        
    }

    /// <summary>
    /// This class contains well-known role names.
    /// </summary>
    public static class RoleNames
    {
        /// <summary>
        /// This constant represents the administrator role.
        /// </summary>
        public const string Admin = "Admin";

        /// <summary>
        /// This constant represents the super administrator role.
        /// </summary>
        public const string SuperAdmin = "SuperAdmin";
    }
}

