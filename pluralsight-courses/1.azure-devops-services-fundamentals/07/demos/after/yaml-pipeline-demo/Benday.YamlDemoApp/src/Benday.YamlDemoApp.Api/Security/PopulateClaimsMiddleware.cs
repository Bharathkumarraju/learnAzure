using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Benday.JsonUtilities;
using Benday.YamlDemoApp.Api.DomainModels;
using Benday.YamlDemoApp.Api.ServiceLayers;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;

namespace Benday.YamlDemoApp.Api.Security
{
    public class PopulateClaimsMiddleware : IMiddleware
    {
        private readonly ISecurityConfiguration _configuration;
        private readonly IUserService _userService;

        public PopulateClaimsMiddleware(ISecurityConfiguration configuration,
            IUserService userService)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration), $"{nameof(configuration)} is null.");
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var claims = new List<Claim>();

            if (_configuration.DevelopmentMode == true)
            {
                ProcessDevelopmentModeClaims(context, claims);
            }
            else
            {
                ProcessNonDevelopmentModeClaims(context, claims);
            }

            await next(context);
        }

        private void ProcessNonDevelopmentModeClaims(
            HttpContext context, List<Claim> claims)
        {
            if (AddClaimsFromHeader(context, claims) == true)
            {
                AddClaimsFromAuthMeService(context, claims);
                AddClaimsFromDatabaseAndCreateUserIfNotPresent(context, claims);

                var identity = new ClaimsIdentity(claims, "EasyAuth");

                context.User = new System.Security.Claims.ClaimsPrincipal(identity);
            }
        }

        private void ProcessDevelopmentModeClaims(
            HttpContext context, List<Claim> claims)
        {
            if (context.User != null &&
            context.User.Identity != null &&
            context.User.Identity.IsAuthenticated == true)
            {
                // copy the existing claims
                claims.AddRange(context.User.Claims);

                var info = new UserInformation(
                new SimpleClaimsAccessor(claims));

                var username = info.Username;

                var isAdmin = false;

                if (username == "admin@test.com")
                {
                    isAdmin = true;
                }

                username = username.Replace(".com", string.Empty)
                .Replace(".org", string.Empty);

                var tokens = username.Split("@");

                AddClaim(claims, ClaimTypes.GivenName, tokens.FirstOrDefault());
                AddClaim(claims, ClaimTypes.Surname, tokens.LastOrDefault());
                AddClaim(claims, ClaimTypes.Email, info.Username);
                AddClaim(claims, ClaimTypes.Name, info.Username);

                if (isAdmin == true)
                {
                    AddClaim(claims, ClaimTypes.Role, SecurityConstants.RoleName_Admin);
                }

                AddClaimsFromDatabaseAndCreateUserIfNotPresent(info, claims);

                var identity = new ClaimsIdentity(claims, "DevelopmentMode");

                context.User = new System.Security.Claims.ClaimsPrincipal(identity);
            }
        }

        private void AddClaimsFromDatabaseAndCreateUserIfNotPresent(
            UserInformation info, List<Claim> claims)
        {
            AddClaimsFromDatabaseAndCreateUserIfNotPresent(
            claims,
            info.Username);
        }

        private static void AddClaimsFromAuthMeService(
            HttpContext context, List<Claim> claims)
        {
            if (context.Request.Cookies.ContainsKey(SecurityConstants.Cookie_AppServiceAuthSession) == true)
            {
                var authMeJson = GetAuthMeInfo(context.Request);

                var jsonArray = JArray.Parse(authMeJson);

                var editor = new JsonEditor(jsonArray[0].ToString(), true);

                AddClaimIfExists(claims, editor, ClaimTypes.GivenName);
                AddClaimIfExists(claims, editor, ClaimTypes.Surname);
                if (AddClaimIfExists(claims, editor, ClaimTypes.Email) == false)
                {
                    var temp = editor.GetValue("user_id");

                    if (temp.IsNullOrWhitespace() == false)
                    {
                        claims.Add(new Claim(ClaimTypes.Email, temp));
                    }
                    else
                    {
                        temp = editor.GetValue("preferred_username");

                        if (temp.IsNullOrWhitespace() == false)
                        {
                            claims.Add(new Claim(ClaimTypes.Email, temp));
                        }
                    }
                }
            }
        }

        private static bool AddClaimIfExists(List<Claim> claims, JsonEditor editor, string claimTypeName)
        {
            var temp = GetClaimValue(editor, claimTypeName);

            if (temp.IsNullOrWhitespace() == false)
            {
                AddClaim(claims, claimTypeName, temp);

                return true;
            }
            else
            {
                return false;
            }
        }

        private static void AddClaim(List<Claim> claims, string claimTypeName, string value)
        {
            claims.Add(new Claim(claimTypeName, value));
        }

        private static string GetClaimValue(JsonEditor editor, string claimName)
        {
            var args = new SiblingValueArguments
            {
                SiblingSearchKey = "typ",
                SiblingSearchValue = claimName,

                DesiredNodeKey = "val",
                PathArguments = new[] { "user_claims" }
            };

            var temp = editor.GetSiblingValue(args);

            return temp;
        }

        private static string GetAuthMeInfo(HttpRequest request)
        {
            var client = new AzureEasyAuthClient(request);

            if (client.IsReadyForAuthenticatedCall == false)
            {
                return null;
            }
            else
            {
                var resultAsString = client.GetUserInformationJson();

                return resultAsString;
            }
        }


        private void AddClaimsFromDatabaseAndCreateUserIfNotPresent(
            HttpContext context, List<Claim> claims)
        {
            var identityProviderHeader =
            GetHeaderValue(context, SecurityConstants.Claim_X_MsClientPrincipalIdp);

            var username =
            GetHeaderValue(
            context,
            SecurityConstants.Claim_X_MsClientPrincipalName);

            if (identityProviderHeader != null &&
            username != null)
            {
                AddClaimsFromDatabaseAndCreateUserIfNotPresent(claims, username);
            }
        }

        private void AddClaimsFromDatabaseAndCreateUserIfNotPresent(List<Claim> claims, string username)
        {
            var user = _userService.GetByUsername(username);

            if (user == null)
            {
                user = CreateNewUser(claims);
            }

            if (user == null || user.Claims == null)
            {
                throw new InvalidOperationException("User or user claims collection was null.");
            }

            var values = user.Claims.ToList();

            var now = DateTime.UtcNow;

            foreach (var item in values)
            {
                if (item.IsValidOnDate(now) == false)
                {
                    continue;
                }
                else if (item.ClaimName == ApiConstants.ClaimName_Role)
                {
                    AddClaim(claims,
                    ClaimTypes.Role, item.ClaimValue);
                }
                else
                {
                    AddClaim(claims,
                    item.ClaimName, item.ClaimValue);
                }
            }

            AddClaim(claims, ApiConstants.ClaimName_UserId, user.Id.ToString());
        }

        private User CreateNewUser(List<Claim> claims)
        {
            var info = new UserInformation(new SimpleClaimsAccessor(claims));

            var user = new User
            {
                EmailAddress = info.EmailAddress,
                FirstName = info.FirstName,
                Username = info.Username,
                LastName = info.LastName,
                PhoneNumber = string.Empty,
                Status = ApiConstants.StatusActive,
                Source = info.Source
            };

            _userService.Save(user);

            return user;
        }

        private static bool AddClaimsFromHeader(HttpContext context, List<Claim> claims)
        {
            var identityProviderHeader =
            GetHeaderValue(context, SecurityConstants.Claim_X_MsClientPrincipalIdp);

            if (identityProviderHeader != null)
            {
                var identityHeader =
                GetHeaderValue(
                context,
                SecurityConstants.Claim_X_MsClientPrincipalId);

                var nameHeader =
                GetHeaderValue(
                context,
                SecurityConstants.Claim_X_MsClientPrincipalName);

                claims.Add(new Claim(
                SecurityConstants.Claim_X_MsClientPrincipalIdp,
                identityProviderHeader));

                claims.Add(new Claim(
                SecurityConstants.Claim_X_MsClientPrincipalId,
                identityHeader));

                claims.Add(new Claim(
                SecurityConstants.Claim_X_MsClientPrincipalName,
                nameHeader));

                claims.Add(new Claim(
                ClaimTypes.Name,
                nameHeader));

                return true;
            }
            else
            {
                return false;
            }
        }

        private static string GetHeaderValue(HttpContext context, string headerName)
        {
            var match = (from temp in context.Request.Headers
                         where temp.Key == headerName
                         select temp.Value).FirstOrDefault();

            return match;
        }
    }
}
