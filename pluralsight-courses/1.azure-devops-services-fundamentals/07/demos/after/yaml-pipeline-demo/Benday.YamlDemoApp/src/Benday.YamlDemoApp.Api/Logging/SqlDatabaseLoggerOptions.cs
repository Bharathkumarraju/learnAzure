using Microsoft.Extensions.Logging;

namespace Benday.YamlDemoApp.Api.Logging
{
    public class SqlDatabaseLoggerOptions
    {
        public LogLevel LogLevel { get; set; }
        public string ConnectionString { get; set; }
    }
}
