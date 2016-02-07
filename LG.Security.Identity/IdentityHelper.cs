using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using LG.Owin.Security.Core;

namespace LG.Owin.Security
{

    public static class IdentityHelper
    {
        private static readonly string AntiXsrfTokenKey = "__AntiXsrfToken";
        private static string AntiXsrfUserNameKey = "__AntiXsrfUserName";

        public static string InvokeAntiForgery(Page currentPage)
        {
            string _antiXsrfTokenValue;
            var requestCookie = HttpContext.Current.Request.Cookies[AntiXsrfTokenKey];
            Guid requestCookieGuidValue;
            if (requestCookie != null && Guid.TryParse(requestCookie.Value, out requestCookieGuidValue))
            {
                // Use the Anti-XSRF token from the cookie
                _antiXsrfTokenValue = requestCookie.Value;
                currentPage.ViewStateUserKey = _antiXsrfTokenValue;
            }
            else
            {
                // Generate a new Anti-XSRF token and save to the cookie
                _antiXsrfTokenValue = Guid.NewGuid().ToString("N");
                currentPage.ViewStateUserKey = _antiXsrfTokenValue;
                var responseCookie = new HttpCookie(AntiXsrfTokenKey)
                {
                    HttpOnly = true,
                    Value = _antiXsrfTokenValue
                };
                if (FormsAuthentication.RequireSSL
                    && HttpContext.Current.Request.IsSecureConnection)
                {
                    responseCookie.Secure = true;
                }
                HttpContext.Current.Response.Cookies.Set(responseCookie);
            }
            return _antiXsrfTokenValue;
        }
    }
}

//using Microsoft.Owin.Security;
//using Microsoft.Owin.Security.OpenIdConnect;
//using Thinktecture.IdentityModel.Owin.ResourceAuthorization;
//using Thinktecture.IdentityServer.TokenService;
