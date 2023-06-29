using System;
using System.Collections.Concurrent;
using Microsoft.Extensions.Logging;

namespace Benday.YamlDemoApp.Api.Logging
{
    public class SqlDatabaseLoggerProvider : IDisposable, ILoggerProvider, ISupportExternalScope
    {
        public SqlDatabaseLoggerProvider(
            SqlDatabaseLoggerOptions options)
        {
            Options = options ?? throw new ArgumentNullException(nameof(options), "Argument cannot be null.");
        }

        ~SqlDatabaseLoggerProvider()
        {
            if (!IsDisposed)
            {
                Dispose(false);
            }
        }

        private readonly ConcurrentDictionary<string, SqlDatabaseLogger> _loggers = new();
        private IExternalScopeProvider _scopeProvider;
        protected IDisposable _settingsOnChangeIDisposable;

        void ISupportExternalScope.SetScopeProvider(IExternalScopeProvider scopeProvider)
        {
            _scopeProvider = scopeProvider;
        }

        ILogger ILoggerProvider.CreateLogger(string category)
        {
            return _loggers.GetOrAdd(category,
            (category) =>
            {
                return new SqlDatabaseLogger(this, category);
            });
        }

        void IDisposable.Dispose()
        {
            if (!IsDisposed)
            {
                try
                {
                    Dispose(true);
                }
                catch
                {
                }

                IsDisposed = true;
                GC.SuppressFinalize(this);  // instructs GC not bother to call the destructor
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_settingsOnChangeIDisposable != null)
            {
                _settingsOnChangeIDisposable.Dispose();
                _settingsOnChangeIDisposable = null;
            }
        }

        internal SqlDatabaseLoggerOptions Options { get; private set; }

        public bool IsEnabled(LogLevel logLevel)
        {
            var result = logLevel != LogLevel.None &&
            Options.LogLevel != LogLevel.None &&
            Convert.ToInt32(logLevel) >= Convert.ToInt32(Options.LogLevel);

            return result;
        }

        internal IExternalScopeProvider ScopeProvider
        {
            get
            {
                if (_scopeProvider == null)
                    _scopeProvider = new LoggerExternalScopeProvider();
                return _scopeProvider;
            }
        }

        public bool IsDisposed { get; protected set; }
    }
}
