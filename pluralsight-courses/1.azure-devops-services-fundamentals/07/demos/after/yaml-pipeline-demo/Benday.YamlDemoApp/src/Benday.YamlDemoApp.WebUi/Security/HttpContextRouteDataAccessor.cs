using Benday.YamlDemoApp.Api.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Benday.YamlDemoApp.WebUi.Security
{

    public class HttpContextRouteDataAccessor : IRouteDataAccessor
    {
        private readonly IHttpContextAccessor _accessor;

        public HttpContextRouteDataAccessor(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public string GetId()
        {
            var values = _accessor.HttpContext.Request.RouteValues!;

            if (values == null)
            {
                return null;
            }
            else if (values.ContainsKey("id") == true)
            {
                return GetValue(values, "id");
            }
            else if (values.ContainsKey("courseId") == true)
            {
                return GetValue(values, "courseId");
            }
            else
            {
                return null;
            }
        }

        private static string GetValue(RouteValueDictionary values, string key)
        {
            var val = values[key].ToString();

            if (string.IsNullOrEmpty(val) == true)
            {
                return null;
            }
            else
            {
                return val;
            }
        }
    }
}
