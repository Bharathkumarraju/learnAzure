using Benday.EfCore.SqlServer;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Benday.Common;

namespace Benday.YamlDemoApp.WebUi.Models
{
    public partial class LogEntrySearchViewModel :
        SearchViewModelBase<Benday.YamlDemoApp.Api.DomainModels.LogEntry>
    {
        public LogEntrySearchViewModel()
        {
            Results = new PageableResults<Benday.YamlDemoApp.Api.DomainModels.LogEntry>();
            IsSimpleSearch = true;
            SimpleSearchValue = string.Empty;
        }
        
        [Display(Name = "category")]
        public string Category { get; set; }
        [Display(Name = "log level")]
        public string LogLevel { get; set; }
        [Display(Name = "log text")]
        public string LogText { get; set; }
        [Display(Name = "exception text")]
        public string ExceptionText { get; set; }
        [Display(Name = "event id")]
        public string EventId { get; set; }
        [Display(Name = "state")]
        public string State { get; set; }
        
        
    }
}