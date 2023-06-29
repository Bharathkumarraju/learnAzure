using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Benday.YamlDemoApp.UnitTests.Fakes.Security
{
    public class MockAuthorizationHandler : AuthorizationHandler<MockAuthorizationRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            MockAuthorizationRequirement requirement)
        {
            if (requirement.IsAuthorizedReturnValue == true)
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }

            return Task.CompletedTask;
        }
    }
}
