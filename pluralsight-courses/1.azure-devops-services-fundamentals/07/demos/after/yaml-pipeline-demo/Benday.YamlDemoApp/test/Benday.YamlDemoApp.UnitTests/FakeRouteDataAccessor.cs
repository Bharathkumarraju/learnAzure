using Benday.YamlDemoApp.Api.Security;

namespace Benday.YamlDemoApp.UnitTests
{
    public class FakeRouteDataAccessor : IRouteDataAccessor
    {
        public bool WasGetIdCalled { get; set; }
        public string GetIdReturnValue { get; set; }

        public string GetId()
        {
            WasGetIdCalled = true;
            return GetIdReturnValue;
        }
    }
}
