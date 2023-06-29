using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Benday.YamlDemoApp.Api.DataAccess.Entities;
using Benday.EfCore.SqlServer;
using Benday.Common;
using System.Linq.Expressions;

namespace Benday.YamlDemoApp.Api.DataAccess.SqlServer
{
    public partial class SqlEntityFrameworkLogEntryRepository :
        SqlEntityFrameworkSearchableRepositoryBase<LogEntryEntity, YamlDemoAppDbContext>,
        ILogEntryRepository
    {
        public SqlEntityFrameworkLogEntryRepository(
            YamlDemoAppDbContext context) : base(context)
        {
            
        }
        
        protected override DbSet<LogEntryEntity> EntityDbSet => Context.LogEntryEntities;
        protected override IQueryable<LogEntryEntity> AddDefaultSort(IQueryable<LogEntryEntity> query)
        {
            return query.OrderByDescending(x => x.Id);
        }
        
        protected override Expression<Func<LogEntryEntity, bool>> GetPredicateForContains(
            SearchArgument arg)
        {
            if (arg is null)
            {
                throw new ArgumentNullException(nameof(arg));
            }
            else
            {
                
                if (arg.PropertyName == nameof(LogEntryEntity.Category))
                {
                    return q => q.Category.Contains(arg.SearchValue);
                }
                else if (arg.PropertyName == nameof(LogEntryEntity.LogLevel))
                {
                    return q => q.LogLevel.Contains(arg.SearchValue);
                }
                else if (arg.PropertyName == nameof(LogEntryEntity.LogText))
                {
                    return q => q.LogText.Contains(arg.SearchValue);
                }
                else if (arg.PropertyName == nameof(LogEntryEntity.ExceptionText))
                {
                    return q => q.ExceptionText.Contains(arg.SearchValue);
                }
                else if (arg.PropertyName == nameof(LogEntryEntity.EventId))
                {
                    return q => q.EventId.Contains(arg.SearchValue);
                }
                else if (arg.PropertyName == nameof(LogEntryEntity.State))
                {
                    return q => q.State.Contains(arg.SearchValue);
                }
                
                else
                {
                    throw new InvalidOperationException(
                    string.Format("Unknown argument '{0}'.", arg.PropertyName));
                }
            }
        }
        
        protected override Expression<Func<LogEntryEntity, bool>> GetPredicateForDoesNotContain(
            SearchArgument arg)
        {
            if (arg is null)
            {
                throw new ArgumentNullException(nameof(arg));
            }
            else
            {
                if (arg.PropertyName == nameof(LogEntryEntity.Category))
                {
                    return q => q.Category.Contains(arg.SearchValue) == false;
                }
                else if (arg.PropertyName == nameof(LogEntryEntity.LogLevel))
                {
                    return q => q.LogLevel.Contains(arg.SearchValue) == false;
                }
                else if (arg.PropertyName == nameof(LogEntryEntity.LogText))
                {
                    return q => q.LogText.Contains(arg.SearchValue) == false;
                }
                else if (arg.PropertyName == nameof(LogEntryEntity.ExceptionText))
                {
                    return q => q.ExceptionText.Contains(arg.SearchValue) == false;
                }
                else if (arg.PropertyName == nameof(LogEntryEntity.EventId))
                {
                    return q => q.EventId.Contains(arg.SearchValue) == false;
                }
                else if (arg.PropertyName == nameof(LogEntryEntity.State))
                {
                    return q => q.State.Contains(arg.SearchValue) == false;
                }
                
                else
                {
                    throw new InvalidOperationException(
                    string.Format("Unknown argument '{0}'.", arg.PropertyName));
                }
            }
        }
        
        protected override Expression<Func<LogEntryEntity, bool>> GetPredicateForStartsWith(
            SearchArgument arg)
        {
            if (arg is null)
            {
                throw new ArgumentNullException(nameof(arg));
            }
            else
            {
                if (arg.PropertyName == nameof(LogEntryEntity.Category))
                {
                    return q => q.Category.StartsWith(arg.SearchValue);
                }
                else if (arg.PropertyName == nameof(LogEntryEntity.LogLevel))
                {
                    return q => q.LogLevel.StartsWith(arg.SearchValue);
                }
                else if (arg.PropertyName == nameof(LogEntryEntity.LogText))
                {
                    return q => q.LogText.StartsWith(arg.SearchValue);
                }
                else if (arg.PropertyName == nameof(LogEntryEntity.ExceptionText))
                {
                    return q => q.ExceptionText.StartsWith(arg.SearchValue);
                }
                else if (arg.PropertyName == nameof(LogEntryEntity.EventId))
                {
                    return q => q.EventId.StartsWith(arg.SearchValue);
                }
                else if (arg.PropertyName == nameof(LogEntryEntity.State))
                {
                    return q => q.State.StartsWith(arg.SearchValue);
                }
                
                else
                {
                    throw new InvalidOperationException(
                    string.Format("Unknown argument '{0}'.", arg.PropertyName));
                }
            }
        }
        
        protected override Expression<Func<LogEntryEntity, bool>> GetPredicateForEndsWith(
            SearchArgument arg)
        {
            if (arg is null)
            {
                throw new ArgumentNullException(nameof(arg));
            }
            else
            {
                if (arg.PropertyName == nameof(LogEntryEntity.Category))
                {
                    return q =>
                    q.Category.EndsWith(arg.SearchValue);
                }
                else if (arg.PropertyName == nameof(LogEntryEntity.LogLevel))
                {
                    return q =>
                    q.LogLevel.EndsWith(arg.SearchValue);
                }
                else if (arg.PropertyName == nameof(LogEntryEntity.LogText))
                {
                    return q =>
                    q.LogText.EndsWith(arg.SearchValue);
                }
                else if (arg.PropertyName == nameof(LogEntryEntity.ExceptionText))
                {
                    return q =>
                    q.ExceptionText.EndsWith(arg.SearchValue);
                }
                else if (arg.PropertyName == nameof(LogEntryEntity.EventId))
                {
                    return q =>
                    q.EventId.EndsWith(arg.SearchValue);
                }
                else if (arg.PropertyName == nameof(LogEntryEntity.State))
                {
                    return q =>
                    q.State.EndsWith(arg.SearchValue);
                }
                
                else
                {
                    throw new InvalidOperationException(
                    string.Format("Unknown argument '{0}'.", arg.PropertyName));
                }
            }
        }
        
        protected override Expression<Func<LogEntryEntity, bool>> GetPredicateForEquals(
            SearchArgument arg)
        {
            if (arg is null)
            {
                throw new ArgumentNullException(nameof(arg));
            }
            else
            {
                
                if (arg.PropertyName == nameof(LogEntryEntity.Category))
                {
                    return q => q.Category == arg.SearchValue;
                }
                else if (arg.PropertyName == nameof(LogEntryEntity.LogLevel))
                {
                    return q => q.LogLevel == arg.SearchValue;
                }
                else if (arg.PropertyName == nameof(LogEntryEntity.LogText))
                {
                    return q => q.LogText == arg.SearchValue;
                }
                else if (arg.PropertyName == nameof(LogEntryEntity.ExceptionText))
                {
                    return q => q.ExceptionText == arg.SearchValue;
                }
                else if (arg.PropertyName == nameof(LogEntryEntity.EventId))
                {
                    return q => q.EventId == arg.SearchValue;
                }
                else if (arg.PropertyName == nameof(LogEntryEntity.State))
                {
                    return q => q.State == arg.SearchValue;
                }
                
                else
                {
                    throw new InvalidOperationException(
                    string.Format("Unknown argument '{0}'.", arg.PropertyName));
                }
            }
        }
        
        protected override Expression<Func<LogEntryEntity, bool>> GetPredicateForIsNotEqualTo(
            SearchArgument arg)
        {
            if (arg is null)
            {
                throw new ArgumentNullException(nameof(arg));
            }
            else
            {
                if (arg.PropertyName == nameof(LogEntryEntity.Category))
                {
                    return q => q.Category == arg.SearchValue;
                }
                else if (arg.PropertyName == nameof(LogEntryEntity.LogLevel))
                {
                    return q => q.LogLevel == arg.SearchValue;
                }
                else if (arg.PropertyName == nameof(LogEntryEntity.LogText))
                {
                    return q => q.LogText == arg.SearchValue;
                }
                else if (arg.PropertyName == nameof(LogEntryEntity.ExceptionText))
                {
                    return q => q.ExceptionText == arg.SearchValue;
                }
                else if (arg.PropertyName == nameof(LogEntryEntity.EventId))
                {
                    return q => q.EventId == arg.SearchValue;
                }
                else if (arg.PropertyName == nameof(LogEntryEntity.State))
                {
                    return q => q.State == arg.SearchValue;
                }
                
                else
                {
                    throw new InvalidOperationException(
                    string.Format("Unknown argument '{0}'.", arg.PropertyName));
                }
            }
        }
        
        protected override IOrderedQueryable<LogEntryEntity> AddSortAscending(IOrderedQueryable<LogEntryEntity> query, string propertyName, bool isFirstSort)
        {
            if (string.Compare(propertyName, "Id", true) == 0)
            {
                if (isFirstSort == true)
                {
                    return query.OrderBy(x => x.Id);
                }
                else
                {
                    return query.ThenBy(x => x.Id);
                }
            }
            else if (string.Compare(propertyName, "Category", true) == 0)
            {
                if (isFirstSort == true)
                {
                    return query.OrderBy(x => x.Category);
                }
                else
                {
                    return query.ThenBy(x => x.Category);
                }
            }
            else if (string.Compare(propertyName, "LogLevel", true) == 0)
            {
                if (isFirstSort == true)
                {
                    return query.OrderBy(x => x.LogLevel);
                }
                else
                {
                    return query.ThenBy(x => x.LogLevel);
                }
            }
            else if (string.Compare(propertyName, "LogText", true) == 0)
            {
                if (isFirstSort == true)
                {
                    return query.OrderBy(x => x.LogText);
                }
                else
                {
                    return query.ThenBy(x => x.LogText);
                }
            }
            else if (string.Compare(propertyName, "ExceptionText", true) == 0)
            {
                if (isFirstSort == true)
                {
                    return query.OrderBy(x => x.ExceptionText);
                }
                else
                {
                    return query.ThenBy(x => x.ExceptionText);
                }
            }
            else if (string.Compare(propertyName, "EventId", true) == 0)
            {
                if (isFirstSort == true)
                {
                    return query.OrderBy(x => x.EventId);
                }
                else
                {
                    return query.ThenBy(x => x.EventId);
                }
            }
            else if (string.Compare(propertyName, "State", true) == 0)
            {
                if (isFirstSort == true)
                {
                    return query.OrderBy(x => x.State);
                }
                else
                {
                    return query.ThenBy(x => x.State);
                }
            }
            else if (string.Compare(propertyName, "LogDate", true) == 0)
            {
                if (isFirstSort == true)
                {
                    return query.OrderBy(x => x.LogDate);
                }
                else
                {
                    return query.ThenBy(x => x.LogDate);
                }
            }
            else
            {
                throw new InvalidOperationException(
                string.Format("Unknown sort argument '{0}'.", propertyName));
            }
        }
        
        protected override IOrderedQueryable<LogEntryEntity> AddSortDescending(IOrderedQueryable<LogEntryEntity> query, string propertyName, bool isFirstSort)
        {
            if (string.Compare(propertyName, "Id", true) == 0)
            {
                if (isFirstSort == true)
                {
                    return query.OrderByDescending(x => x.Id);
                }
                else
                {
                    return query.ThenByDescending(x => x.Id);
                }
            }
            else if (string.Compare(propertyName, "Category", true) == 0)
            {
                if (isFirstSort == true)
                {
                    return query.OrderByDescending(x => x.Category);
                }
                else
                {
                    return query.ThenByDescending(x => x.Category);
                }
            }
            else if (string.Compare(propertyName, "LogLevel", true) == 0)
            {
                if (isFirstSort == true)
                {
                    return query.OrderByDescending(x => x.LogLevel);
                }
                else
                {
                    return query.ThenByDescending(x => x.LogLevel);
                }
            }
            else if (string.Compare(propertyName, "LogText", true) == 0)
            {
                if (isFirstSort == true)
                {
                    return query.OrderByDescending(x => x.LogText);
                }
                else
                {
                    return query.ThenByDescending(x => x.LogText);
                }
            }
            else if (string.Compare(propertyName, "ExceptionText", true) == 0)
            {
                if (isFirstSort == true)
                {
                    return query.OrderByDescending(x => x.ExceptionText);
                }
                else
                {
                    return query.ThenByDescending(x => x.ExceptionText);
                }
            }
            else if (string.Compare(propertyName, "EventId", true) == 0)
            {
                if (isFirstSort == true)
                {
                    return query.OrderByDescending(x => x.EventId);
                }
                else
                {
                    return query.ThenByDescending(x => x.EventId);
                }
            }
            else if (string.Compare(propertyName, "State", true) == 0)
            {
                if (isFirstSort == true)
                {
                    return query.OrderByDescending(x => x.State);
                }
                else
                {
                    return query.ThenByDescending(x => x.State);
                }
            }
            else if (string.Compare(propertyName, "LogDate", true) == 0)
            {
                if (isFirstSort == true)
                {
                    return query.OrderByDescending(x => x.LogDate);
                }
                else
                {
                    return query.ThenByDescending(x => x.LogDate);
                }
            }
            else
            {
                throw new InvalidOperationException(
                string.Format("Unknown sort argument '{0}'.", propertyName));
            }
        }
        
    }
}