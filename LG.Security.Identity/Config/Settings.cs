using System;
using System.Configuration;
using System.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Protocols;
using Microsoft.Owin;
using Microsoft.Owin.Logging;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Infrastructure;
using Microsoft.Owin.Security.OpenIdConnect;
using Owin;

namespace LG.Owin.Security.Config
{
    public class MyOpenIDConnectAuthenticationMiddleware : OpenIdConnectAuthenticationMiddleware
    {
        private readonly ILogger _logger;

        public MyOpenIDConnectAuthenticationMiddleware(OwinMiddleware next, IAppBuilder app,
            OpenIdConnectAuthenticationOptions options) : base(next, app, options)
        {
            _logger = app.CreateLogger<MyOpenIDConnectAuthenticationMiddleware>();
        }

        protected override AuthenticationHandler<OpenIdConnectAuthenticationOptions> CreateHandler()
        {
            return new MyOpenIDConnectAuthenticationHandler(_logger);
        }
    }
    public class MyOpenIDConnectAuthenticationHandler : OpenIdConnectAuthenticationHandler
    {
        private const string NonceProperty 
            = "nonceProperty";
        private const string NoncePrefix = OpenIdConnectAuthenticationDefaults.CookiePrefix + "nonce.";

        private readonly ILogger _logger;

        public MyOpenIDConnectAuthenticationHandler(ILogger logger) : base(logger)
        {
            _logger = logger;
        }

        protected override void AddNonceToMessage(OpenIdConnectMessage message)
        {
            if (message == null)
            {
                throw new ArgumentNullException("message");
            }

            var properties = new AuthenticationProperties();
            var nonce = Options.ProtocolValidator.GenerateNonce();
            properties.Dictionary.Add(
                NonceProperty, nonce);
            message.Nonce = nonce;

            //computing the hash of nonce and appending it to the cookie name
            string nonceKey = GetNonceKey(nonce);
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = Request.IsSecure,
            };
            var nonceId = Convert.ToBase64String(Encoding.UTF8.GetBytes((Options.StateDataFormat.Protect(properties))));
            Response.Cookies.Append(
                nonceKey,
                nonceId,
                cookieOptions);
        }

        protected string GetNonceKey(string nonce)
        {
            using (HashAlgorithm hash = SHA256.Create())
            {
                return NoncePrefix + Convert.ToBase64String(hash.ComputeHash(Encoding.UTF8.GetBytes(nonce)));
            }
        }

        protected override string RetrieveNonce(OpenIdConnectMessage message)
        {
            if (message.IdToken == null)
            {
                return null;
            }

            JwtSecurityToken token = new JwtSecurityToken(message.IdToken);
            if (token == null)
            {
                return null;
            }

            //computing the hash of nonce and appending it to the cookie name
            string nonceKey = GetNonceKey(token.Payload.Nonce);
            string nonceCookie = Request.Cookies[nonceKey];
            if (string.IsNullOrWhiteSpace(nonceCookie))
            {
                _logger.WriteWarning("The nonce cookie was not found.");
                return null;
            }

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = Request.IsSecure
            };

            Response.Cookies.Delete(nonceKey, cookieOptions);

            string nonce = null;
            AuthenticationProperties nonceProperties =
                Options.StateDataFormat.Unprotect(Encoding.UTF8.GetString(Convert.FromBase64String(nonceCookie)));
            if (nonceProperties != null)
            {
                nonceProperties.Dictionary.TryGetValue(NonceProperty, out nonce);
            }
            else
            {
                _logger.WriteWarning("Failed to un-protect the nonce cookie.");
            }

            return nonce;
        }

    }

    public static class ClientSettings
    {
        /// <summary>
        /// Instance of LG.Owin.Security.AppContextUser
        /// </summary>
        public static string ContextPortal
            => ConfigurationManager.AppSettings["APP:PORTAL"];

        /// <summary>
        /// ClientID from Web.config.
        /// </summary>
        public static string ClientID
            => ConfigurationManager.AppSettings[
                "CLIENT:ID:" + ContextPortal];

        /// <summary>
        /// Client Url from Web.config.
        /// </summary>
        public static string ClientUrl
            => ConfigurationManager.AppSettings[
                "CLIENT:URL:" + ContextPortal];

        /// <summary>
        /// ClientName from Web.config.
        /// </summary>
        public static string ClientName
            => ConfigurationManager.AppSettings[
                "CLIENT:NAME:" + ContextPortal];

        /// <summary>
        /// Client Secret found in web.config
        /// </summary>
        public static string ClientSecret => ConfigurationManager.AppSettings[
            "CLIENT:START:UP:SECRET:" + ContextPortal];

        /// <summary>
        /// Client Logout Url from Web.config.
        /// </summary>
        public static string ClientLogoutURL
            => ConfigurationManager.AppSettings["CLIENT:LOGOUT:URL:"
                                                + ContextPortal];

        /// <summary>
        /// The following items are used during start up.
        /// </summary>

        public static string SCOPE
            => ConfigurationManager.AppSettings["CLIENT:START:UP:SCOPE:"
                                                + ContextPortal];

        public static string SECRET
            => ConfigurationManager.AppSettings["CLIENT:START:UP:SECRET:"
                                                + ContextPortal];

        public static string RESPONSE_TYPE
            => ConfigurationManager.AppSettings["CLIENT:START:UP:RESPONSE:TYPE:"
                                                + ContextPortal];

        public static string AUTHENTICATION_TYPE
            => ConfigurationManager.AppSettings["CLIENT:START:UP:SIGN:AUTHENTICATION:TYPE:"
                                                + ContextPortal];

    }

    public static class DoctorsClientSettings
    {
        /// <summary>
        /// Instance of LG.Owin.Security.AppContextUser
        /// </summary>
        public static string ContextPortal
            => ConfigurationManager.AppSettings["APP:PORTAL"];

        /// <summary>
        /// ClientID from Web.config.
        /// </summary>
        public static string ClientID
            => ConfigurationManager.AppSettings[
                "CLIENT:ID:" + ContextPortal];

        /// <summary>
        /// Client Url from Web.config.
        /// </summary>
        public static string ClientUrl
            => ConfigurationManager.AppSettings[
                "CLIENT:URL:" + ContextPortal];

        /// <summary>
        /// ClientName from Web.config.
        /// </summary>
        public static string ClientName
            => ConfigurationManager.AppSettings[
                "CLIENT:NAME:" + ContextPortal];

        /// <summary>
        /// Client Secret found in web.config
        /// </summary>
        public static string ClientSecret => ConfigurationManager.AppSettings[
            "CLIENT:START:UP:SECRET:" + ContextPortal];

        /// <summary>
        /// Client Logout Url from Web.config.
        /// </summary>
        public static string ClientLogoutURL
            => ConfigurationManager.AppSettings["CLIENT:LOGOUT:URL:"
                                                + ContextPortal];

        /// <summary>
        /// The following items are used during start up.
        /// </summary>

        public static string SCOPE
            => ConfigurationManager.AppSettings["CLIENT:START:UP:SCOPE:"
                                                + ContextPortal];

        public static string SECRET
            => ConfigurationManager.AppSettings["CLIENT:START:UP:SECRET:"
                                                + ContextPortal];

        public static string RESPONSE_TYPE
            => ConfigurationManager.AppSettings["CLIENT:START:UP:RESPONSE:TYPE:"
                                                + ContextPortal];

        public static string AUTHENTICATION_TYPE
            => ConfigurationManager.AppSettings["CLIENT:START:UP:SIGN:AUTHENTICATION:TYPE:"
                                                + ContextPortal];

    }

    public static class ServerSettings
    {
        public static string Token => ServerSettings.Url + "/connect/token";
        public static string UserInfo => ServerSettings.Url + "/connect/userinfo";
        public static string Authorize => ServerSettings.Url + "/connect/authorize";
        public static string Url => ConfigurationManager.AppSettings["IDS:SRV:URL"];
        public static string IssuerUri => ConfigurationManager.AppSettings["IDS:SRV:ISSUER:URI"];
    }
}