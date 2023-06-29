using System;
using Microsoft.Extensions.Options;

namespace Benday.YamlDemoApp.UnitTests.AzureStorage
{
    public class OptionsMonitorMock<T> : IOptionsMonitor<T>
    {

        public T CurrentValue { get; set; }

        public T Get(string name)
        {
            throw new NotImplementedException();
        }

        public IDisposable OnChange(Action<T, string> listener)
        {
            throw new NotImplementedException();
        }
    }
}
