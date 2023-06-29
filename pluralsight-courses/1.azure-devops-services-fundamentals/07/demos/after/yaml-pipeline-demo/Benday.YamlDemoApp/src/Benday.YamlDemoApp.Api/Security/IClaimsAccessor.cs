using System.Collections.Generic;
using System.Security.Claims;

namespace Benday.YamlDemoApp.Api.Security
{
    public interface IClaimsAccessor
    {
        IEnumerable<Claim> Claims { get; }
    }
}
