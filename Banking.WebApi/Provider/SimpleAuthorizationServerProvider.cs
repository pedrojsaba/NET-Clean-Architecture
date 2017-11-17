using Banking.WebApi.Repository;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace Banking.WebApi.Provider
{
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
            IdentityUser user;
            IList<string> userInRol;
            IDictionary<string, string> profile = new Dictionary<string, string>();

            using (AuthRepository _repo = new AuthRepository())
            {
                user = await _repo.FindUser(context.UserName, context.Password);
                if (user == null)
                {
                    context.SetError("invalid_grant", "The user name or password is incorrect.");
                    return;
                }

                userInRol = await _repo.FindUserInRole(user.Id);
                profile = _repo.FindUserProfile(user.Id);
            }

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
            identity.AddClaim(new Claim(ClaimTypes.Role, userInRol[0].ToString()));


            var props = new AuthenticationProperties(profile);

            //var props = new AuthenticationProperties(new Dictionary<string, string>
            //    {
            //        { "name", "made6" },
            //        { "surname", "s_made6" },
            //        { "role", "administrator" }
            //    });

            var ticket = new AuthenticationTicket(identity, props);
            context.Validated(ticket);

            //var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            //identity.AddClaim(new Claim("sub", context.UserName));
            //identity.AddClaim(new Claim("role", "user"));
            //context.Validated(identity);
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }
            return Task.FromResult<object>(null);
        }
    }
}