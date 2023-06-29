using System;
using Benday.YamlDemoApp.Api.DataAccess.Entities;
using Benday.YamlDemoApp.Api.DomainModels;

namespace Benday.YamlDemoApp.Api.Adapters
{
    public partial class LogEntryAdapter :
        AdapterBase<LogEntry, LogEntryEntity>
    {

        protected override void PerformAdapt(
            LogEntry fromValue,
            LogEntryEntity toValue)
        {
            if (fromValue == null)
            {
                throw new ArgumentNullException(nameof(fromValue));
            }

            if (toValue == null)
            {
                throw new ArgumentNullException(nameof(toValue));
            }
            toValue.Id = fromValue.Id;
            toValue.Category = fromValue.Category;
            toValue.LogLevel = fromValue.LogLevel;
            toValue.LogText = fromValue.LogText;
            toValue.ExceptionText = fromValue.ExceptionText;
            toValue.EventId = fromValue.EventId;
            toValue.State = fromValue.State;
            toValue.LogDate = fromValue.LogDate;


        }

        protected override void PerformAdapt(
            LogEntryEntity fromValue,
            LogEntry toValue
            )
        {
            if (fromValue == null)
            {
                throw new ArgumentNullException(nameof(fromValue));
            }

            if (toValue == null)
            {
                throw new ArgumentNullException(nameof(toValue));
            }

            toValue.Id = fromValue.Id;
            toValue.Category = fromValue.Category;
            toValue.LogLevel = fromValue.LogLevel;
            toValue.LogText = fromValue.LogText;
            toValue.ExceptionText = fromValue.ExceptionText;
            toValue.EventId = fromValue.EventId;
            toValue.State = fromValue.State;
            toValue.LogDate = fromValue.LogDate;

            toValue.AcceptChanges();

        }
    }
}
