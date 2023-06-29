using Benday.YamlDemoApp.Api.Security;

namespace Benday.YamlDemoApp.WebUi.Security
{
    public partial class DefaultUserAuthorizationStrategy : IUserAuthorizationStrategy
    {
        private readonly SecurityUtility _securityUtility;
        
        public DefaultUserAuthorizationStrategy(
            IUserClaimsPrincipalProvider provider)
        {
            var principal = provider.GetUser();
            
            _securityUtility =
            new SecurityUtility(principal.Identity, principal);
        }
        
        private bool IsAdministrator()
        {
            return _securityUtility.IsInRole(
            SecurityConstants.RoleName_Admin);
        }
    }
}