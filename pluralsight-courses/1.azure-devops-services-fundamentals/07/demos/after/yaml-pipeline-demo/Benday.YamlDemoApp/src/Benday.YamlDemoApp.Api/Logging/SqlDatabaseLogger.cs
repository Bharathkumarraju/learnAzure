using System;
using Benday.YamlDemoApp.Api.DataAccess.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace Benday.YamlDemoApp.Api.Logging
{
    public class SqlDatabaseLogger : ILogger
    {
        private readonly string _connectionString;

        public SqlDatabaseLogger(SqlDatabaseLoggerProvider provider, string category)
        {
            if (provider == null)
            {
                throw new ArgumentNullException(nameof(provider), "Argument cannot be null.");
            }

            if (provider.Options == null ||
            string.IsNullOrEmpty(provider.Options.ConnectionString) == true)
            {
                throw new ArgumentNullException(
                nameof(provider),
                "Settings are null or connection string is not set.");
            }
            else
            {
                _connectionString = provider.Options.ConnectionString;
            }

            Provider = provider;
            Category = category;
        }

        IDisposable ILogger.BeginScope<TState>(TState state)
        {
            return Provider.ScopeProvider.Push(state);
        }

        bool ILogger.IsEnabled(LogLevel logLevel)
        {
            return Provider.IsEnabled(logLevel);
        }

        void ILogger.Log<TState>(LogLevel logLevel, EventId eventId,
        TState state, Exception exception,
        Func<TState, Exception, string> formatter)
        {
            if ((this as ILogger).IsEnabled(logLevel))
            {
                var logItem = new LogEntryEntity
                {
                    Category = Category,
                    LogLevel = logLevel.ToString(),

                    LogText = exception?.Message ?? state.ToString(),
                    ExceptionText = exception == null ? "" : exception.ToString(),
                    EventId = eventId.ToString(),
                    State = state == null ? state.ToString() : "unknown-state"
                };

                SaveToDatabase(logItem);
            }
        }

        private void SaveToDatabase(LogEntryEntity item)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = connection.CreateCommand();

            command.CommandText =
            "INSERT INTO LogEntry (" +
            "Category, LogLevel, LogText, ExceptionText, EventId, State, LogDate) " +
            "VALUES (" +
            "@Category, @LogLevel, @LogText, @ExceptionText, @EventId, @State, GETUTCDATE())";

            command.Parameters.AddWithValue("@Category", item.Category);
            command.Parameters.AddWithValue("@LogLevel", item.LogLevel);
            command.Parameters.AddWithValue("@LogText", item.LogText);
            command.Parameters.AddWithValue("@ExceptionText", item.ExceptionText);
            command.Parameters.AddWithValue("@EventId", item.EventId);
            command.Parameters.AddWithValue("@State", item.State);

            connection.Open();
            command.ExecuteNonQuery();
        }

        public SqlDatabaseLoggerProvider Provider { get; private set; }
        public string Category { get; private set; }
    }
}
