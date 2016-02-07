﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;
using LG.Owin.Security.Core;
using LG.Owin.Security.Managers;
using Microsoft.IdentityModel.Protocols;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using Owin;
using Thinktecture.IdentityModel.Client;
using System.IdentityModel.Tokens;
using LG.Owin.Security.Config;

namespace LG.Owin.Security.Enviornment
{
    public static class Startup
    {
        public static  void Configuration(IAppBuilder app)
        {
            AntiForgeryConfig.UniqueClaimTypeIdentifier = Core.ClaimTypes.Subject;
            JwtSecurityTokenHandler.InboundClaimTypeMap = new Dictionary<string, string>();
            app.UseResourceAuthorization(
                new AuthorizationManager());

            


            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "Cookies",
                AuthenticationMode = AuthenticationMode.Active
            });
            //app.O

            app.SetDefaultSignInAsAuthenticationType("External");

            var openIDConnectOptions = new OpenIdConnectAuthenticationOptions
            {
                // ConfigurationManager = 
                Authority = LG.Owin.Security.Config.ServerSettings.Url,
                ClientId = LG.Owin.Security.Config.ClientSettings.ClientID,
                ClientSecret = "51FC860D-07D3-4296-9147-2E40AC7FF6C8".Sha256(),
                Scope = "openid profile roles all_claims",
                ResponseType = "id_token token",
                RedirectUri = LG.Owin.Security.Config.ClientSettings.ClientUrl,
                SignInAsAuthenticationType = "Cookies",
                UseTokenLifetime = false,
                Notifications = new OpenIdConnectAuthenticationNotifications
                {
                    SecurityTokenValidated = async n =>
                    {
                        var nid = new ClaimsIdentity(
                            n.AuthenticationTicket.Identity.AuthenticationType,
                            Core.ClaimTypes.GivenName, Core.ClaimTypes.Role);

                        var userInfoClient = new UserInfoClient(
                            new Uri(n.Options.Authority + "/connect/userinfo"),
                            n.ProtocolMessage.AccessToken);

                        var userInfo = await userInfoClient.GetAsync();
                        userInfo.Claims.ToList().ForEach(
                            ui => nid.AddClaim(new Claim(ui.Item1, ui.Item2)));

                        // keep the id_token for logout
                        nid.AddClaim(new Claim("id_token",
                            n.ProtocolMessage.IdToken));

                        // add access token for sample API
                        nid.AddClaim(new Claim("access_token",
                            n.ProtocolMessage.AccessToken));

                        // keep track of access token expiration
                        nid.AddClaim(new Claim("expires_at",
                            DateTimeOffset.Now.AddSeconds(int.Parse(n.ProtocolMessage.ExpiresIn)).ToString()));

                        // add some other app specific claim
                        //nid.AddClaim(new Claim("app_specific", "some data"));
                        n.AuthenticationTicket = new AuthenticationTicket(nid, n.AuthenticationTicket.Properties);

                    },
                    RedirectToIdentityProvider = async n =>
                    {
                        if (n.ProtocolMessage.RequestType == OpenIdConnectRequestType.AuthenticationRequest)
                        {
                            await GetWaiter();
                            var sb = new StringBuilder();
                            var custom = n.OwinContext.Get<Dictionary<string, string>>("OpenIdConnect.Parameters");
                            if (custom != null)
                            {
                                foreach (var c in custom)
                                {
                                    // ReSharper disable once UseStringInterpolation
                                    sb.Append(string.Format(" {0}={1}", c.Key, c.Value));
                                }
                            }

                            if (sb.Length > 0)
                            {
                                sb.Remove(0, 1);
                            }
                            n.ProtocolMessage.AcrValues = sb.ToString();
                        }
                        if (n.ProtocolMessage.RequestType == OpenIdConnectRequestType.LogoutRequest)
                        {
                            var idTokenHint = n.OwinContext.Authentication.User.FindFirst("id_token");
                            if (idTokenHint != null)
                            {
                                n.ProtocolMessage.IdTokenHint = idTokenHint.Value;
                            }
                        }
                    }
                }
            };
            app.Use(typeof(MyOpenIDConnectAuthenticationMiddleware),
                app, 
                openIDConnectOptions);
            // app.UseOpenIdConnectAuthentication(openIDConnectOptions);
        }
        //protected getNew(){
        internal static async Task<bool> GetWaiter()
        {
            //await System.Threading.Timer;
            await Task.Delay(1);
            return true;
        }
    }
}
