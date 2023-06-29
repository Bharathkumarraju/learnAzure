using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace Benday.YamlDemoApp.WebUi.Models
{
    public class SecuritySummaryViewModel
    {
        public IEnumerable<System.Security.Claims.Claim> Claims { get; set; }
        public IHeaderDictionary Headers { get; set; }
        public IRequestCookieCollection Cookies { get; set; }

        public string IsLoggedIn { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
    }
}
