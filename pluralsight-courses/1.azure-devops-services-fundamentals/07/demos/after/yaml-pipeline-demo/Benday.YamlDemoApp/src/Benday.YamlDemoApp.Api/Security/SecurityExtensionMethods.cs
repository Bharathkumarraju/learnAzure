using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace Benday.YamlDemoApp.Api.Security
{
    public static class SecurityExtensionMethods
    {
        public static IApplicationBuilder UsePopulateClaimsMiddleware(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<PopulateClaimsMiddleware>();
        }

        public static Claim GetClaim(
            this IEnumerable<Claim> claims, string claimName)
        {
            if (claims == null)
            {
                return null;
            }
            else
            {
                var match = (from temp in claims
                             where temp.Type == claimName
                             select temp).FirstOrDefault();

                return match;
            }
        }

        public static string GetClaimValue(
            this IEnumerable<Claim> claims, string claimName)
        {
            var match = claims.GetClaim(claimName);

            if (match == null)
            {
                return null;
            }
            else
            {
                return match.Value;
            }
        }

        public static bool ContainsClaim(
            this IEnumerable<Claim> claims, string claimName)
        {
            var match = claims.GetClaim(claimName);

            if (match == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool ContainsRoleClaim(
            this IEnumerable<Claim> claims, string roleName)
        {
            var match = claims.Where(
            x => x.Type == ClaimTypes.Role && x.Value == roleName).FirstOrDefault();

            if (match == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static KeyValuePair<string, StringValues> GetHeader(
            this IHeaderDictionary headers, string name)
        {
            if (headers == null)
            {
                return default;
            }
            else
            {
                var match =
                (from temp in headers
                 where temp.Key == name
                 select temp).FirstOrDefault();

                return match;
            }
        }

        public static string GetHeaderValue(
            this IHeaderDictionary headers, string name)
        {
            if (headers.ContainsKey(name) == false)
            {
                return null;
            }
            else
            {
                return headers[name];
            }
        }
    }
}
