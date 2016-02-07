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
using LG.Owin.Security.Managers;
using Microsoft.IdentityModel.Protocols;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using Owin;
using Thinktecture.IdentityModel.Client;
using Thinktecture.IdentityModel.Owin.ResourceAuthorization;

namespace LG.Owin.Security
{
    public static class IdentityManager
    {
        #region [@  -   Internal Methods    -          @]

        static readonly string AntiXsrfTokenKey = "__AntiXsrfToken";
        static string AntiXsrfUserNameKey = "__AntiXsrfUserName";

        internal static string InvokeAntiForgery(Page currentPage)
        {
            string antiXsrfTokenValue;
            var requestCookie = HttpContext.Current.Request.Cookies[AntiXsrfTokenKey];
            Guid requestCookieGuidValue;
            if (requestCookie != null && Guid.TryParse(requestCookie.Value, out requestCookieGuidValue))
            {
                // Use the Anti-XSRF token from the cookie
                antiXsrfTokenValue = requestCookie.Value; currentPage.ViewStateUserKey = antiXsrfTokenValue;
            }
            else
            {
                // Generate a new Anti-XSRF token and save to the cookie
                antiXsrfTokenValue = Guid.NewGuid().ToString("N");
                currentPage.ViewStateUserKey = antiXsrfTokenValue;
                var responseCookie = new HttpCookie(AntiXsrfTokenKey)
                {
                    HttpOnly = true,
                    Value = antiXsrfTokenValue
                };
                if (FormsAuthentication.RequireSSL
                    && HttpContext.Current.Request.IsSecureConnection)
                {
                    responseCookie.Secure = true;
                }
                HttpContext.Current.Response.Cookies.Set(responseCookie);
            }
            return antiXsrfTokenValue;
        }

        internal static bool DestroySession()
        {
            HttpContext.Current.Session.RemoveAll(); //HttpContext.Current.Session.Abandon();
            return true;
        }

        internal static bool SignOutIdentity()
        {
            HttpContext.Current.GetOwinContext().Authentication.SignOut(
                new AuthenticationProperties
                {
                    RedirectUri = LG.Owin.Security.Config.ClientSettings.ClientLogoutURL,
                    ExpiresUtc = DateTimeOffset.UtcNow.Add(new TimeSpan(-1, -1, -2, -2))
                });
            return true;
        }

        internal static bool ChallengeIdentity()
        {
            if (!HttpContext.Current.Request.IsAuthenticated)
            {
                HttpContext.Current.GetOwinContext().Authentication.Challenge(
                    new AuthenticationProperties
                    {
                        RedirectUri = LG.Owin.Security.Config.ClientSettings.ClientLogoutURL
                    }, OpenIdConnectAuthenticationDefaults.AuthenticationType);
            }
            return true;
        }

        internal static bool IdentitySignOutAndChallenge()
        {
            return IdentityManager.SignOutIdentity()
                && IdentityManager.ChallengeIdentity();
        }

        //ToDo: Need to update Populating the AppContextUser.
        internal static async Task<bool> InitSessionContext()
        {

            var claimsPrincipal = Thread.CurrentPrincipal
                as ClaimsPrincipal;

            var identity = claimsPrincipal?.Identity
                as ClaimsIdentity;

            var memberRole = claimsPrincipal?.Claims.FirstOrDefault(
                x => x.Type == "role");

            if (memberRole?.Value != null)
            {
                if (memberRole?.Value != "Primary Member")
                {
                    IdentitySignOutAndChallenge();
                    return false;
                }
            }

            if (identity != null && identity.IsAuthenticated)
            {
                var roles = identity.Claims.Select(
                    x => x.Type == "role");

                var rid = identity.Claims.FirstOrDefault(
                    x => x.Type == "RID");

                var name = identity.Claims.FirstOrDefault(
                    x => x.Type == "name");

                var id_token = identity.Claims.FirstOrDefault(
                    x => x.Type == "id_token");

                var access_token = identity.Claims.FirstOrDefault(
                    x => x.Type == "access_token");

                var expires_at = identity.Claims.FirstOrDefault(
                    x => x.Type == "expires_at");

                if (rid != null
                    && access_token != null && id_token != null && expires_at != null)
                {
                    var model = new LG.Owin.Identity.Models.IdentityUser()
                    {
                        RID = Convert.ToInt64(rid.Value),
                        Name = name != null ? name.Value : "",
                        AccessToken = access_token.Value,
                        TokenID = id_token.Value,
                        ExpirationDate = Convert.ToDateTime(expires_at.Value)
                    };
                    var e = roles as List<Claim>;
                    if (e != null)
                        foreach (var role in e)
                        {
                            if (role.Value == "Primary Member")
                            {
                                model.IsPrimaryMember = true;
                            }
                            if (role.Value == "Account Manager")
                            {
                                model.IsAccountManager = true;
                            }
                        }
                    UserContext.IsAuthenticated = true;
                    UserContext.Identity = model;
                }
            }
            else
            {
                if (SignOutIdentity())
                {
                     ChallengeIdentity();
                }
            }
            //await Instance.LoadAuthenticateUser(
            //    Convert.ToInt64(claimRID.Value));

            //if (!Instance.IsAuthenticated || !string.IsNullOrEmpty(
            //    Instance.AuthTokenJsonString))
            //    return false;

            //var authPayload = new IdentityAuthPackage()
            //{
            //    AuthGuid = Guid.NewGuid(),
            //    RolodexItemID = Instance.RolodexItemID,
            //    DateCreated = DateTime.Now,
            //    IsAuthenticated = true,
            //    DateExpiry = DateTime.Now.AddMinutes(15),
            //    Name = new NameEntity()
            //    {
            //        FirstName = Instance.Info.MedicalPractitioner.PersonInfo.FName,
            //        LastName = Instance.Info.MedicalPractitioner.PersonInfo.LName,
            //        PrintedName = Instance.Info.MedicalPractitioner.PrintedName
            //    }
            //};
            //Instance.AuthTokenJsonString
            //    = Newtonsoft.Json.JsonConvert.SerializeObject(authPayload);
            await GetWaiter();
            HttpContext.Current.Response.Redirect("Default.aspx");
            return true;
        }

        internal static async
               Task<bool> GetWaiter()
        {
            //await System.Threading.Timer;
            await Task.Delay(1);
            return true;
        }

        #endregion

        /// <summary>
        ///  Session Populate Async: 
        ///  short cut to Populate the Session
        /// </summary>
        /// <returns></returns>
        public static async Task<bool>
            StartAsync()
        {
            return await InitSessionContext();
        }

        public static bool SignOut()
        {
            DestroySession();
            return IdentitySignOutAndChallenge();
        }

        public static bool Challenge()
        {
            return ChallengeIdentity();
        }

        public static string AntiForgery(Page ctx)
        {
            return InvokeAntiForgery(ctx);
        }

        public static void PreLoad(MasterPage ctx, StateBag viewState, string antixsrf)
        {
            if (!ctx.Page.IsPostBack)
            {
                // Set Anti-XSRF token
                viewState[AntiXsrfTokenKey] = ctx.Page.ViewStateUserKey;
                viewState[AntiXsrfUserNameKey] = HttpContext.Current.User.Identity.Name ?? string.Empty;
            }
            else
            {
                // Validate the Anti-XSRF token
                if ((string)viewState[AntiXsrfTokenKey] != antixsrf
                    || (string)viewState[AntiXsrfUserNameKey] != (HttpContext.Current.User.Identity.Name ?? string.Empty))
                {
                    throw new InvalidOperationException("Validation of Anti-XSRF token failed.");
                }
            }
        }

        #region [@  -   Public Properties    -          @]

        public static LG.Owin.Identity.AppContextUser UserContext => LG.Owin.Identity.AppContextUser.GetCurrentSingleton();


        #endregion
    }
}
