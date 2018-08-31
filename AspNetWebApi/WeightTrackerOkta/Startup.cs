using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Jwt;

using Owin;

using System.Threading.Tasks;

[assembly: OwinStartup(typeof(WeightTrackerOkta.Startup))]

namespace WeightTrackerOkta
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Configure JWT Bearer middleware
            // with an OpenID Connect Authority

            var authority = "https://{oktaDomain}/oauth2/default";

            var configurationManager = new ConfigurationManager<OpenIdConnectConfiguration>(
                authority + "/.well-known/openid-configuration",
                new OpenIdConnectConfigurationRetriever(),
                new HttpDocumentRetriever());

            app.UseJwtBearerAuthentication(new JwtBearerAuthenticationOptions
            {
                AuthenticationMode = AuthenticationMode.Active,
                TokenValidationParameters = new TokenValidationParameters
                {
                    ValidAudience = "api://default",
                    ValidIssuer = authority,
                    IssuerSigningKeyResolver = (token, securityToken, identifier, parameters) =>
                    {
                        var discoveryDocument = Task.Run(() => configurationManager.GetConfigurationAsync()).GetAwaiter().GetResult();
                        return discoveryDocument.SigningKeys;
                    }
                }
            });
        }
    }
}