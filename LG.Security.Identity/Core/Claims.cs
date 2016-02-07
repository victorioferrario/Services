using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LG.Owin.Security.Core
{
    public static class ClaimTypes
    {
        // core oidc claims
        public const string Subject = "sub";
        public const string Name = "name";
        public const string GivenName = "given_name";
        public const string FamilyName = "family_name";
        public const string MiddleName = "middle_name";
        public const string NickName = "nickname";
        public const string PreferredUserName = "preferred_username";
        public const string Profile = "profile";
        public const string Picture = "picture";
        public const string WebSite = "website";
        public const string Email = "email";
        public const string EmailVerified = "email_verified";
        public const string Gender = "gender";
        public const string BirthDate = "birthdate";
        public const string ZoneInfo = "zoneinfo";
        public const string Locale = "locale";
        public const string PhoneNumber = "phone_number";
        public const string PhoneNumberVerified = "phone_number_verified";
        public const string Address = "address";
        public const string Audience = "aud";
        public const string Issuer = "iss";
        public const string NotBefore = "nbf";
        public const string Expiration = "exp";

        // more standard claims
        public const string UpdatedAt = "updated_at";
        public const string IssuedAt = "iat";
        public const string AuthenticationMethod = "amr";
        public const string AuthenticationContextClassReference = "acr";
        public const string AuthenticationTime = "auth_time";
        public const string AuthorizedParty = "azp";
        public const string AccessTokenHash = "at_hash";
        public const string AuthorizationCodeHash = "c_hash";
        public const string Nonce = "nonce";
        public const string JwtId = "jti";

        // more claims
        public const string ClientId = "client_id";
        public const string Scope = "scope";
        public const string Id = "id";
        public const string Secret = "secret";
        public const string IdentityProvider = "idp";
        public const string Role = "role";
        public const string ReferenceTokenId = "reference_token_id";

        // claims for authentication controller partial logins
        public const string AuthorizationReturnUrl = "authorization_return_url";
        public const string PartialLoginRestartUrl = "partial_login_restart_url";
        public const string PartialLoginReturnUrl = "partial_login_return_url";

        // internal claim types
        // claim type to identify external user from external provider
        public const string ExternalProviderUserId = "external_provider_user_id";
        public const string PartialLoginResumeId = PartialLoginResumeClaimPrefix + "{0}";
        public const string PartialLoginResumeClaimPrefix = "partial_login_resume_id:";
    }
}
