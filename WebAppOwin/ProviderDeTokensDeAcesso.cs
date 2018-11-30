using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace WebAppOwin
{
    public class ProviderDeTokensDeAcesso : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var usuario = BaseUsuarios.usuarios().FirstOrDefault(x => x.nome == context.UserName && x.senha == context.Password);

            if (usuario == null)
            {
                context.SetError("invalid_grant", "Usuário não encontrado um senha incorreta.");
                return;
            }

            var props = new AuthenticationProperties(new Dictionary<string, string>
            {
                {"client", context.UserName},
                {"uid", usuario.id.ToString()},
            });

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);

            var ticket = new AuthenticationTicket(identity, props);

            context.Validated(ticket);
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