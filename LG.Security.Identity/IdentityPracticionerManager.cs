using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OpenIdConnect;

namespace LG.Owin.Security
{
    public class IdentityPracticionerManager
    {
        #region [@  -   Internal Methods    -          @]

        private const string AntiXsrfTokenKey = "__AntiXsrfToken";
        private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";

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
            HttpContext.Current.Session.RemoveAll(); 
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
            return IdentityPracticionerManager.SignOutIdentity()
                && IdentityPracticionerManager.ChallengeIdentity();
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
                if (memberRole?.Value == "Primary Member")
                {
                    if (SignOutIdentity())
                    {
                        ChallengeIdentity();
                    }
                    return false;
                }
            }

            var claimRID = claimsPrincipal?.Claims.FirstOrDefault(
              x => x.Type == "RID");

            if (claimRID?.Value != null)
            {
                await Instance.LoadAuthenticateUser(
                    Convert.ToInt64(claimRID?.Value));

                if (!Instance.IsAuthenticated || !string.IsNullOrEmpty(
                    Instance.AuthTokenJsonString)) return false;

                var authPayload = new LG.Owin.Security.Models.IdentityUserPractioner()
                {
                    AuthGuid = Guid.NewGuid(),
                    RolodexItemID = Instance.RolodexItemID,
                    DateCreated = DateTime.Now,
                    IsAuthenticated = true,
                    DateExpiry = DateTime.Now.AddMinutes(15),
                    Name = new LG.Owin.Security.Models.NameEntity()
                    {
                        FirstName = Instance.Info.MedicalPractitioner.PersonInfo.FName,
                        LastName = Instance.Info.MedicalPractitioner.PersonInfo.LName,
                        PrintedName = Instance.Info.MedicalPractitioner.PrintedName
                    }
                };

                Instance.AuthTokenJsonString
                    = Newtonsoft.Json.JsonConvert.SerializeObject(authPayload);

                var item = new HttpCookie("PortalID")
                {
                    Value = "Doctors",
                    Expires = DateTime.Now.AddDays(1),
                    Shareable = false,
                    HttpOnly = false,
                    Secure = true
                };

                var item2 = new HttpCookie("IsAuthenticatedCookie")
                {
                    Values = {
                        {
                            "A", "true"
                        },
                        {
                            "R", Instance.RolodexItemID.ToString()
                        },
                        {
                            "P", "DOCTOR"
                        }
                    },
                    Expires = DateTime.Now.AddDays(1),
                    Shareable = false,
                    HttpOnly = false,
                    Secure = true
                };

                var cookie = new Cookie();
                HttpContext.Current.Response.Cookies.Add(item);
                HttpContext.Current.Response.Cookies.Add(item2);

                HttpContext.Current.Response.Redirect(
                    "Default");
                return true;
            }
            else
            {
                return false;
            }
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
        #region [@  -   Public Methods    -          @]


        /// <summary>
        ///  Session Populate A sync: 
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
       
        
        #endregion

        #region [@  -   Public Properties    -          @]

        public static LG.Session.DoctorInstance Instance => LG.Session.DoctorInstance.GetCurrentSingleton();

        #endregion
    
    }
}


