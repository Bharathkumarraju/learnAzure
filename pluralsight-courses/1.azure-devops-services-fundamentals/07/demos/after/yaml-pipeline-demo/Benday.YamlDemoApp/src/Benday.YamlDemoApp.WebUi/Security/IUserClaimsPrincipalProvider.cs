using System.Security.Claims;

namespace Benday.YamlDemoApp.WebUi.Security
{
    public interface IUserClaimsPrincipalProvider
    {
        ClaimsPrincipal GetUser();
    }
}
