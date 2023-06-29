using Microsoft.AspNetCore.Authorization;

namespace Benday.YamlDemoApp.Api.Security
{
    public class LoggedInUsingEasyAuthRequirement : IAuthorizationRequirement
    {
    }
}
