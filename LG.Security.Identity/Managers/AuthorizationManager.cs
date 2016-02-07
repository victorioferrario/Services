using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

using System.Web.Security;
using System.Web.UI;
using LG.Owin.Security.Core;

using Microsoft.IdentityModel.Protocols;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using Owin;
using Thinktecture.IdentityModel.Client;
using Thinktecture.IdentityModel.Owin.ResourceAuthorization;
namespace LG.Owin.Security.Managers
{
    public class AuthorizationManager
       : ResourceAuthorizationManager
    {
        Task<bool> AuthorizeMember(ResourceAuthorizationContext context)
        {
            switch (context.Action.First().Value)
            {
                case "Read":
                    return Eval(context.Principal.HasClaim("role", "Member"));
                case "Write":
                    return Eval(context.Principal.HasClaim("role", "PrimaryMember"));
                default:
                    return Nok();
            }
        }
        public override Task<bool> CheckAccessAsync(ResourceAuthorizationContext context)
        {
            //switch (context.Resource.First().Value)
            //{
            //    case "PrimaryMember":
            //        return AuthorizeMember(context);
            //    case "FamilyMember":
            //    default:
            //        return Nok();
            //}
            return Nok();
        }
    }
}
