using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Benday.YamlDemoApp.WebUi.Security
{
    public class HttpContextUserClaimsPrincipalProvider : IUserClaimsPrincipalProvider
    {

        private readonly IHttpContextAccessor _contextAccessor;

        public HttpContextUserClaimsPrincipalProvider(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor ?? throw new ArgumentNullException(nameof(contextAccessor),
            $"{nameof(contextAccessor)} is null.");
        }

        public ClaimsPrincipal GetUser()
        {
            var context = _contextAccessor.HttpContext;

            if (context != null && context.User != null)
            {
                return context.User;
            }
            else
            {
                return null;
            }
        }
    }
}
