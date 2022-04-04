using Domain.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebAPI.Providers
{
    public class AuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public AuthorizationServerProvider()
        {
        }

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
            await Task.FromResult<object>(null);
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            UserManager<AppUser> userManager = context.OwinContext.GetUserManager<UserManager<AppUser>>();
            var user = await userManager.FindAsync(context.UserName, context.Password);
            if (user == null)
            {
                context.SetError("invalid_grant", "invalid_grant.");
                context.Rejected();
            }
            else
            {
                //var permissions = ServiceFactory.Get<IPermissionService>().GetByUserId(user.Id);
                //var permissionViewModels = AutoMapper.Mapper.Map<ICollection<Permission>, ICollection<PermissionViewModel>>(permissions);
                var roles = userManager.GetRoles(user.Id);
                var claimsIdentity = await userManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ExternalBearer);
                string avatar = string.IsNullOrEmpty(user.Avatar) ? "" : user.Avatar;
                string email = string.IsNullOrEmpty(user.Email) ? "" : user.Email;
                claimsIdentity.AddClaim(new Claim("uid", user.Id));
                claimsIdentity.AddClaim(new Claim("fullName", user.FullName));
                claimsIdentity.AddClaim(new Claim("avatar", avatar));
                claimsIdentity.AddClaim(new Claim("email", email));
                claimsIdentity.AddClaim(new Claim("username", user.UserName));
                claimsIdentity.AddClaim(new Claim("roles", JsonConvert.SerializeObject(roles)));
                //identity.AddClaim(new Claim("permissions", JsonConvert.SerializeObject(permissionViewModels)));
                var props = new AuthenticationProperties(new Dictionary<string, string>
                    {
                        {"uid", user.Id},
                        {"fullName", user.FullName},
                        {"avatar", avatar },
                        {"email", email},
                        {"username", user.UserName},
                        //{"permissions",JsonConvert.SerializeObject(permissionViewModels) },
                        {"roles", JsonConvert.SerializeObject(roles) }
                    });
                context.Validated(new AuthenticationTicket(claimsIdentity, props));
            }
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