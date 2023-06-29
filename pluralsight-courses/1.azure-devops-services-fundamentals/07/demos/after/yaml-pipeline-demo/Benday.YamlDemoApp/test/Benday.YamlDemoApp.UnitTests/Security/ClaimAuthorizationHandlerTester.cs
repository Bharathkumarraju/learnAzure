using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Benday.YamlDemoApp.Api.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Benday.YamlDemoApp.UnitTests.Security
{
    public class ClaimAuthorizationHandlerTester
    {
        private readonly FakeRouteDataAccessor _RouteData = new FakeRouteDataAccessor();
        public ClaimAuthorizationHandlerTester()
        {
            Requirement = new ClaimAuthorizationRequirement();
            Handler = new ClaimAuthorizationHandler(_RouteData);
        }

        public async Task AssertSuccess()
        {
            var context = await RunHandler();

            Assert.IsTrue(context.HasSucceeded,
            "Handler should have returned success.");
        }

        public async Task AssertFailure()
        {
            var context = await RunHandler();

            Assert.IsTrue(context.HasFailed,
            "Handler should have failed.");
        }

        internal void SetRouteDataValue(string id)
        {
            _RouteData.GetIdReturnValue = id;
        }

        public ClaimAuthorizationRequirement Requirement { get; }

        public ClaimAuthorizationHandler Handler { get; }

        private List<Claim> _Claims;
        private List<Claim> Claims
        {
            get
            {
                if (_Claims == null)
                {
                    _Claims = new List<Claim>();
                }

                return _Claims;
            }
        }

        public void AddClaim(string claimType, string claimValue)
        {
            var temp = new Claim(claimType, claimValue);

            Claims.Add(temp);
        }

        public void AddRequirementRole(string roleName)
        {
            Requirement.Roles.Add(roleName);
        }

        private RouteData _RouteDataInfo;
        private RouteData RouteDataInfo
        {
            get
            {
                if (_RouteDataInfo == null)
                {
                    _RouteDataInfo = new RouteData();
                }

                return _RouteDataInfo;
            }
        }

        public void AddClaimRole(string roleName)
        {
            AddClaim(ClaimTypes.Role, roleName);
        }

        private async Task<AuthorizationHandlerContext> RunHandler()
        {
            var actionContext = new ActionContext();

            actionContext.RouteData = RouteDataInfo;
            actionContext.HttpContext = new DefaultHttpContext();
            actionContext.ActionDescriptor = new ActionDescriptor();

            var filterContext = new AuthorizationFilterContext(
            actionContext, new List<IFilterMetadata>());

            var identity = new ClaimsIdentity(Claims, "Test");

            var principal = new ClaimsPrincipal(identity);

            var context = new AuthorizationHandlerContext(
            new List<IAuthorizationRequirement>()
            {
                Requirement
            },
            principal, filterContext);


            await Handler.HandleAsync(context);

            return context;
        }

        public void AddRequirementPermission(string permissionName)
        {
            Requirement.PermissionNames.Add(permissionName);
        }
    }
}
