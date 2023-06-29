using System;
using System.IO;
using System.Linq;

namespace Benday.YamlDemoApp.Api.Security
{
    public static partial class SecurityConstants
    {
        public const string Claim_X_MsClientPrincipalId = "X-MS-CLIENT-PRINCIPAL-ID";
        public const string Claim_X_MsClientPrincipalIdp = "X-MS-CLIENT-PRINCIPAL-IDP";
        public const string Claim_X_MsClientPrincipalName = "X-MS-CLIENT-PRINCIPAL-NAME";
        public const string Cookie_AppServiceAuthSession = "AppServiceAuthSession";
        
        public const string Idp_DevelopmentMode = "DevelopmentMode";
        
        public const string AuthType_AzureAppService = "AzureAppService";
        public const string AuthType_None = "None";
        
        public const string RoleName_Admin = "YamlDemoApp.Admin";
        
        public const string Policy_LoggedInUsingEasyAuth = "LoggedInUsingEasyAuthHandler";
        public const string Policy_IsAdministrator = "IsAdministrator";
    }
}