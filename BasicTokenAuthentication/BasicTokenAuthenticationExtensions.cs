using System;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace BasicTokenAuthentication
{
    public static class BasicTokenAuthenticationExtensions
    {
        public static AuthenticationBuilder AddBasic<TAuthService>(this AuthenticationBuilder builder)
            where TAuthService : class, IBasicTokenAuthenticationService
        {
            return AddBasic<TAuthService>(builder, BasicTokenAuthenticationDefaults.AuthenticationScheme, _ => { });
        }

        public static AuthenticationBuilder AddBasic<TAuthService>(this AuthenticationBuilder builder, string authenticationScheme)
            where TAuthService : class, IBasicTokenAuthenticationService
        {
            return AddBasic<TAuthService>(builder, authenticationScheme, _ => { });
        }

        public static AuthenticationBuilder AddBasic<TAuthService>(this AuthenticationBuilder builder, Action<BasicTokenAuthenticationOptions> configureOptions)
            where TAuthService : class, IBasicTokenAuthenticationService
        {
            return AddBasic<TAuthService>(builder, BasicTokenAuthenticationDefaults.AuthenticationScheme, configureOptions);
        }

        public static AuthenticationBuilder AddBasic<TAuthService>(this AuthenticationBuilder builder, string authenticationScheme, Action<BasicTokenAuthenticationOptions> configureOptions)
            where TAuthService : class, IBasicTokenAuthenticationService
        {
            builder.Services.AddSingleton<IPostConfigureOptions<BasicTokenAuthenticationOptions>, BasicTokenAuthenticationPostConfigureOptions>();
            builder.Services.AddTransient<IBasicTokenAuthenticationService, TAuthService>();

            return builder.AddScheme<BasicTokenAuthenticationOptions, BasicTokenAuthenticationHandler>(
                authenticationScheme, configureOptions);
        }
    }
}