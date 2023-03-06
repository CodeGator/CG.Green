
namespace Microsoft.AspNetCore.Builder;

/// <summary>
/// This class contains extension methods related to the <see cref="WebApplicationBuilder"/>
/// type.
/// </summary>
public static class WebApplicationBuilderExtensions012
{
    // *******************************************************************
    // Public methods.
    // *******************************************************************

    #region Public methods

    /// <summary>
    /// This method adds a UI for administering the <see cref="CG.Green"/> 
    /// microservice.
    /// </summary>
    /// <param name="webApplicationBuilder">The web application builder to
    /// use for the operation.</param>
    /// <param name="sectionName">The configuration section to use for the 
    /// operation. Defaults to <c>Administration</c>.</param>
    /// <param name="bootstrapLogger">The bootstrap logger to use for the 
    /// operation.</param>
    /// <returns>The value of the <paramref name="webApplicationBuilder"/>
    /// parameter, for chaining calls together, Fluent style.</returns>
    /// <exception cref="ArgumentException">This exception is thrown whenever
    /// one or more arguments are missing, or invalid.</exception>
    public static WebApplicationBuilder AddGreenAdministration(
        this WebApplicationBuilder webApplicationBuilder,
        string sectionName = "Administration",
        ILogger? bootstrapLogger = null
        )
    {
        // Validate the parameters before attempting to use them.
        Guard.Instance().ThrowIfNull(webApplicationBuilder, nameof(webApplicationBuilder));

        // Tell the world what we are about to do.
        bootstrapLogger?.LogDebug(
            "Wiring up the auto-mapper for the admin library"
            );

        // Wire up the auto-mapper.
        webApplicationBuilder.Services.AddAutoMapper(cfg =>
        {
            // Wire up the conversion maps between Duende models and our VMs.
            cfg.CreateMap<Client, ListClientVM>().ReverseMap();
            cfg.CreateMap<Client, NewClientVM>().ReverseMap();
			cfg.CreateMap<Secret, ClientSecretVM>().ReverseMap();
			cfg.CreateMap<ClientClaim, ClientClaimVM>().ReverseMap();
			cfg.CreateMap<Client, EditClientVM>()
				.ForMember(dest => dest.IdentityProviderRestrictions, opt => opt.MapFrom(src => src.IdentityProviderRestrictions.Select(x => new EditProviderVM() { Name = x })))
				.ForMember(dest => dest.RedirectUris, opt => opt.MapFrom(src => src.RedirectUris.Select(x => new EditUriVM() { Value = x })))
				.ForMember(dest => dest.PostLogoutRedirectUris, opt => opt.MapFrom(src => src.PostLogoutRedirectUris.Select(x => new EditUriVM() { Value = x })))
				.ForMember(dest => dest.FrontChannelLogoutUri, opt => opt.MapFrom(src => new EditUriVM() { Value = src.FrontChannelLogoutUri }))
				.ForMember(dest => dest.BackChannelLogoutUri, opt => opt.MapFrom(src => new EditUriVM() { Value = src.BackChannelLogoutUri }))
				.ReverseMap()
				.ForMember(dest => dest.IdentityProviderRestrictions, opt => opt.MapFrom(src => src.IdentityProviderRestrictions.Select(x => x.Name)))
				.ForMember(dest => dest.RedirectUris, opt => opt.MapFrom(src => src.RedirectUris.Select(x => x.Value)))
				.ForMember(dest => dest.PostLogoutRedirectUris, opt => opt.MapFrom(src => src.PostLogoutRedirectUris.Select(x => x.Value)))
				.ForMember(dest => dest.FrontChannelLogoutUri, opt => opt.MapFrom(src => src.FrontChannelLogoutUri.Value))
				.ForMember(dest => dest.BackChannelLogoutUri, opt => opt.MapFrom(src => src.BackChannelLogoutUri.Value));
			cfg.CreateMap<GreenUser, ListUserVM>().ReverseMap();
			cfg.CreateMap<GreenUser, NewUserVM>().ReverseMap();
		});

        // Return the application builder.
        return webApplicationBuilder;
    }

    #endregion
}
