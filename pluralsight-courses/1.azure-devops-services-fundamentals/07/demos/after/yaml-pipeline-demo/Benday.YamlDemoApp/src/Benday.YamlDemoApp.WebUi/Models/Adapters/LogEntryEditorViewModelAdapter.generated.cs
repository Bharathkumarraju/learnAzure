using Benday.YamlDemoApp.Api.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Benday.YamlDemoApp.Api;
using Benday.YamlDemoApp.Api.Adapters;
using Benday.YamlDemoApp.WebUi.Models;

namespace Benday.YamlDemoApp.WebUi.Models.Adapters
{
    public partial class LogEntryEditorViewModelAdapter :
        AdapterBase<
        Benday.YamlDemoApp.Api.DomainModels.LogEntry,
        Benday.YamlDemoApp.WebUi.Models.LogEntryEditorViewModel>
    {
    
        protected override void PerformAdapt(
            Benday.YamlDemoApp.Api.DomainModels.LogEntry fromValue,
            Benday.YamlDemoApp.WebUi.Models.LogEntryEditorViewModel toValue)
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
        
        protected override AdapterActions BeforeAdapt(
            Benday.YamlDemoApp.WebUi.Models.LogEntryEditorViewModel fromValue,
            Benday.YamlDemoApp.Api.DomainModels.LogEntry toValue)
        {
            if (fromValue == null)
            {
                return AdapterActions.Skip;
            }
            else if (fromValue.Id != ApiConstants.UnsavedId && fromValue.IsMarkedForDelete == true)
            {
                return AdapterActions.Delete;
            }
            else
            {
                return AdapterActions.Adapt;
            }
        }
        
        protected override void PerformAdapt(
            Benday.YamlDemoApp.WebUi.Models.LogEntryEditorViewModel fromValue,
            Benday.YamlDemoApp.Api.DomainModels.LogEntry toValue)
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
    }
}