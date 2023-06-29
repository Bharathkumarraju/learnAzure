using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Benday.YamlDemoApp.Api.DataAccess.Entities;
using Benday.EfCore.SqlServer;
using Benday.Common;
using System.Linq.Expressions;

namespace Benday.YamlDemoApp.Api.DataAccess.SqlServer
{
    public partial class SqlEntityFrameworkConfigurationItemRepository :
        SqlEntityFrameworkSearchableRepositoryBase<ConfigurationItemEntity, YamlDemoAppDbContext>,
        IConfigurationItemRepository
    {
        public SqlEntityFrameworkConfigurationItemRepository(
            YamlDemoAppDbContext context) : base(context)
        {
            
        }
        
        protected override DbSet<ConfigurationItemEntity> EntityDbSet => Context.ConfigurationItemEntities;
        
        protected override Expression<Func<ConfigurationItemEntity, bool>> GetPredicateForContains(
            SearchArgument arg)
        {
            if (arg is null)
            {
                throw new ArgumentNullException(nameof(arg));
            }
            else
            {
                
                if (arg.PropertyName == nameof(ConfigurationItemEntity.Category))
                {
                    return q => q.Category.Contains(arg.SearchValue);
                }
                else if (arg.PropertyName == nameof(ConfigurationItemEntity.ConfigurationKey))
                {
                    return q => q.ConfigurationKey.Contains(arg.SearchValue);
                }
                else if (arg.PropertyName == nameof(ConfigurationItemEntity.Description))
                {
                    return q => q.Description.Contains(arg.SearchValue);
                }
                else if (arg.PropertyName == nameof(ConfigurationItemEntity.ConfigurationValue))
                {
                    return q => q.ConfigurationValue.Contains(arg.SearchValue);
                }
                else if (arg.PropertyName == nameof(ConfigurationItemEntity.Status))
                {
                    return q => q.Status.Contains(arg.SearchValue);
                }
                else if (arg.PropertyName == nameof(ConfigurationItemEntity.CreatedBy))
                {
                    return q => q.CreatedBy.Contains(arg.SearchValue);
                }
                else if (arg.PropertyName == nameof(ConfigurationItemEntity.LastModifiedBy))
                {
                    return q => q.LastModifiedBy.Contains(arg.SearchValue);
                }
                
                else
                {
                    throw new InvalidOperationException(
                    string.Format("Unknown argument '{0}'.", arg.PropertyName));
                }
            }
        }
        
        protected override Expression<Func<ConfigurationItemEntity, bool>> GetPredicateForDoesNotContain(
            SearchArgument arg)
        {
            if (arg is null)
            {
                throw new ArgumentNullException(nameof(arg));
            }
            else
            {
                if (arg.PropertyName == nameof(ConfigurationItemEntity.Category))
                {
                    return q => q.Category.Contains(arg.SearchValue) == false;
                }
                else if (arg.PropertyName == nameof(ConfigurationItemEntity.ConfigurationKey))
                {
                    return q => q.ConfigurationKey.Contains(arg.SearchValue) == false;
                }
                else if (arg.PropertyName == nameof(ConfigurationItemEntity.Description))
                {
                    return q => q.Description.Contains(arg.SearchValue) == false;
                }
                else if (arg.PropertyName == nameof(ConfigurationItemEntity.ConfigurationValue))
                {
                    return q => q.ConfigurationValue.Contains(arg.SearchValue) == false;
                }
                else if (arg.PropertyName == nameof(ConfigurationItemEntity.Status))
                {
                    return q => q.Status.Contains(arg.SearchValue) == false;
                }
                else if (arg.PropertyName == nameof(ConfigurationItemEntity.CreatedBy))
                {
                    return q => q.CreatedBy.Contains(arg.SearchValue) == false;
                }
                else if (arg.PropertyName == nameof(ConfigurationItemEntity.LastModifiedBy))
                {
                    return q => q.LastModifiedBy.Contains(arg.SearchValue) == false;
                }
                
                else
                {
                    throw new InvalidOperationException(
                    string.Format("Unknown argument '{0}'.", arg.PropertyName));
                }
            }
        }
        
        protected override Expression<Func<ConfigurationItemEntity, bool>> GetPredicateForStartsWith(
            SearchArgument arg)
        {
            if (arg is null)
            {
                throw new ArgumentNullException(nameof(arg));
            }
            else
            {
                if (arg.PropertyName == nameof(ConfigurationItemEntity.Category))
                {
                    return q => q.Category.StartsWith(arg.SearchValue);
                }
                else if (arg.PropertyName == nameof(ConfigurationItemEntity.ConfigurationKey))
                {
                    return q => q.ConfigurationKey.StartsWith(arg.SearchValue);
                }
                else if (arg.PropertyName == nameof(ConfigurationItemEntity.Description))
                {
                    return q => q.Description.StartsWith(arg.SearchValue);
                }
                else if (arg.PropertyName == nameof(ConfigurationItemEntity.ConfigurationValue))
                {
                    return q => q.ConfigurationValue.StartsWith(arg.SearchValue);
                }
                else if (arg.PropertyName == nameof(ConfigurationItemEntity.Status))
                {
                    return q => q.Status.StartsWith(arg.SearchValue);
                }
                else if (arg.PropertyName == nameof(ConfigurationItemEntity.CreatedBy))
                {
                    return q => q.CreatedBy.StartsWith(arg.SearchValue);
                }
                else if (arg.PropertyName == nameof(ConfigurationItemEntity.LastModifiedBy))
                {
                    return q => q.LastModifiedBy.StartsWith(arg.SearchValue);
                }
                
                else
                {
                    throw new InvalidOperationException(
                    string.Format("Unknown argument '{0}'.", arg.PropertyName));
                }
            }
        }
        
        protected override Expression<Func<ConfigurationItemEntity, bool>> GetPredicateForEndsWith(
            SearchArgument arg)
        {
            if (arg is null)
            {
                throw new ArgumentNullException(nameof(arg));
            }
            else
            {
                if (arg.PropertyName == nameof(ConfigurationItemEntity.Category))
                {
                    return q =>
                    q.Category.EndsWith(arg.SearchValue);
                }
                else if (arg.PropertyName == nameof(ConfigurationItemEntity.ConfigurationKey))
                {
                    return q =>
                    q.ConfigurationKey.EndsWith(arg.SearchValue);
                }
                else if (arg.PropertyName == nameof(ConfigurationItemEntity.Description))
                {
                    return q =>
                    q.Description.EndsWith(arg.SearchValue);
                }
                else if (arg.PropertyName == nameof(ConfigurationItemEntity.ConfigurationValue))
                {
                    return q =>
                    q.ConfigurationValue.EndsWith(arg.SearchValue);
                }
                else if (arg.PropertyName == nameof(ConfigurationItemEntity.Status))
                {
                    return q =>
                    q.Status.EndsWith(arg.SearchValue);
                }
                else if (arg.PropertyName == nameof(ConfigurationItemEntity.CreatedBy))
                {
                    return q =>
                    q.CreatedBy.EndsWith(arg.SearchValue);
                }
                else if (arg.PropertyName == nameof(ConfigurationItemEntity.LastModifiedBy))
                {
                    return q =>
                    q.LastModifiedBy.EndsWith(arg.SearchValue);
                }
                
                else
                {
                    throw new InvalidOperationException(
                    string.Format("Unknown argument '{0}'.", arg.PropertyName));
                }
            }
        }
        
        protected override Expression<Func<ConfigurationItemEntity, bool>> GetPredicateForEquals(
            SearchArgument arg)
        {
            if (arg is null)
            {
                throw new ArgumentNullException(nameof(arg));
            }
            else
            {
                
                if (arg.PropertyName == nameof(ConfigurationItemEntity.Category))
                {
                    return q => q.Category == arg.SearchValue;
                }
                else if (arg.PropertyName == nameof(ConfigurationItemEntity.ConfigurationKey))
                {
                    return q => q.ConfigurationKey == arg.SearchValue;
                }
                else if (arg.PropertyName == nameof(ConfigurationItemEntity.Description))
                {
                    return q => q.Description == arg.SearchValue;
                }
                else if (arg.PropertyName == nameof(ConfigurationItemEntity.ConfigurationValue))
                {
                    return q => q.ConfigurationValue == arg.SearchValue;
                }
                else if (arg.PropertyName == nameof(ConfigurationItemEntity.Status))
                {
                    return q => q.Status == arg.SearchValue;
                }
                else if (arg.PropertyName == nameof(ConfigurationItemEntity.CreatedBy))
                {
                    return q => q.CreatedBy == arg.SearchValue;
                }
                else if (arg.PropertyName == nameof(ConfigurationItemEntity.LastModifiedBy))
                {
                    return q => q.LastModifiedBy == arg.SearchValue;
                }
                
                else
                {
                    throw new InvalidOperationException(
                    string.Format("Unknown argument '{0}'.", arg.PropertyName));
                }
            }
        }
        
        protected override Expression<Func<ConfigurationItemEntity, bool>> GetPredicateForIsNotEqualTo(
            SearchArgument arg)
        {
            if (arg is null)
            {
                throw new ArgumentNullException(nameof(arg));
            }
            else
            {
                if (arg.PropertyName == nameof(ConfigurationItemEntity.Category))
                {
                    return q => q.Category == arg.SearchValue;
                }
                else if (arg.PropertyName == nameof(ConfigurationItemEntity.ConfigurationKey))
                {
                    return q => q.ConfigurationKey == arg.SearchValue;
                }
                else if (arg.PropertyName == nameof(ConfigurationItemEntity.Description))
                {
                    return q => q.Description == arg.SearchValue;
                }
                else if (arg.PropertyName == nameof(ConfigurationItemEntity.ConfigurationValue))
                {
                    return q => q.ConfigurationValue == arg.SearchValue;
                }
                else if (arg.PropertyName == nameof(ConfigurationItemEntity.Status))
                {
                    return q => q.Status == arg.SearchValue;
                }
                else if (arg.PropertyName == nameof(ConfigurationItemEntity.CreatedBy))
                {
                    return q => q.CreatedBy == arg.SearchValue;
                }
                else if (arg.PropertyName == nameof(ConfigurationItemEntity.LastModifiedBy))
                {
                    return q => q.LastModifiedBy == arg.SearchValue;
                }
                
                else
                {
                    throw new InvalidOperationException(
                    string.Format("Unknown argument '{0}'.", arg.PropertyName));
                }
            }
        }
        
        protected override IOrderedQueryable<ConfigurationItemEntity> AddSortAscending(IOrderedQueryable<ConfigurationItemEntity> query, string propertyName, bool isFirstSort)
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
            else if (string.Compare(propertyName, "ConfigurationKey", true) == 0)
            {
                if (isFirstSort == true)
                {
                    return query.OrderBy(x => x.ConfigurationKey);
                }
                else
                {
                    return query.ThenBy(x => x.ConfigurationKey);
                }
            }
            else if (string.Compare(propertyName, "Description", true) == 0)
            {
                if (isFirstSort == true)
                {
                    return query.OrderBy(x => x.Description);
                }
                else
                {
                    return query.ThenBy(x => x.Description);
                }
            }
            else if (string.Compare(propertyName, "ConfigurationValue", true) == 0)
            {
                if (isFirstSort == true)
                {
                    return query.OrderBy(x => x.ConfigurationValue);
                }
                else
                {
                    return query.ThenBy(x => x.ConfigurationValue);
                }
            }
            else if (string.Compare(propertyName, "Status", true) == 0)
            {
                if (isFirstSort == true)
                {
                    return query.OrderBy(x => x.Status);
                }
                else
                {
                    return query.ThenBy(x => x.Status);
                }
            }
            else if (string.Compare(propertyName, "CreatedBy", true) == 0)
            {
                if (isFirstSort == true)
                {
                    return query.OrderBy(x => x.CreatedBy);
                }
                else
                {
                    return query.ThenBy(x => x.CreatedBy);
                }
            }
            else if (string.Compare(propertyName, "CreatedDate", true) == 0)
            {
                if (isFirstSort == true)
                {
                    return query.OrderBy(x => x.CreatedDate);
                }
                else
                {
                    return query.ThenBy(x => x.CreatedDate);
                }
            }
            else if (string.Compare(propertyName, "LastModifiedBy", true) == 0)
            {
                if (isFirstSort == true)
                {
                    return query.OrderBy(x => x.LastModifiedBy);
                }
                else
                {
                    return query.ThenBy(x => x.LastModifiedBy);
                }
            }
            else if (string.Compare(propertyName, "LastModifiedDate", true) == 0)
            {
                if (isFirstSort == true)
                {
                    return query.OrderBy(x => x.LastModifiedDate);
                }
                else
                {
                    return query.ThenBy(x => x.LastModifiedDate);
                }
            }
            else if (string.Compare(propertyName, "Timestamp", true) == 0)
            {
                if (isFirstSort == true)
                {
                    return query.OrderBy(x => x.Timestamp);
                }
                else
                {
                    return query.ThenBy(x => x.Timestamp);
                }
            }
            else
            {
                throw new InvalidOperationException(
                string.Format("Unknown sort argument '{0}'.", propertyName));
            }
        }
        
        protected override IOrderedQueryable<ConfigurationItemEntity> AddSortDescending(IOrderedQueryable<ConfigurationItemEntity> query, string propertyName, bool isFirstSort)
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
            else if (string.Compare(propertyName, "ConfigurationKey", true) == 0)
            {
                if (isFirstSort == true)
                {
                    return query.OrderByDescending(x => x.ConfigurationKey);
                }
                else
                {
                    return query.ThenByDescending(x => x.ConfigurationKey);
                }
            }
            else if (string.Compare(propertyName, "Description", true) == 0)
            {
                if (isFirstSort == true)
                {
                    return query.OrderByDescending(x => x.Description);
                }
                else
                {
                    return query.ThenByDescending(x => x.Description);
                }
            }
            else if (string.Compare(propertyName, "ConfigurationValue", true) == 0)
            {
                if (isFirstSort == true)
                {
                    return query.OrderByDescending(x => x.ConfigurationValue);
                }
                else
                {
                    return query.ThenByDescending(x => x.ConfigurationValue);
                }
            }
            else if (string.Compare(propertyName, "Status", true) == 0)
            {
                if (isFirstSort == true)
                {
                    return query.OrderByDescending(x => x.Status);
                }
                else
                {
                    return query.ThenByDescending(x => x.Status);
                }
            }
            else if (string.Compare(propertyName, "CreatedBy", true) == 0)
            {
                if (isFirstSort == true)
                {
                    return query.OrderByDescending(x => x.CreatedBy);
                }
                else
                {
                    return query.ThenByDescending(x => x.CreatedBy);
                }
            }
            else if (string.Compare(propertyName, "CreatedDate", true) == 0)
            {
                if (isFirstSort == true)
                {
                    return query.OrderByDescending(x => x.CreatedDate);
                }
                else
                {
                    return query.ThenByDescending(x => x.CreatedDate);
                }
            }
            else if (string.Compare(propertyName, "LastModifiedBy", true) == 0)
            {
                if (isFirstSort == true)
                {
                    return query.OrderByDescending(x => x.LastModifiedBy);
                }
                else
                {
                    return query.ThenByDescending(x => x.LastModifiedBy);
                }
            }
            else if (string.Compare(propertyName, "LastModifiedDate", true) == 0)
            {
                if (isFirstSort == true)
                {
                    return query.OrderByDescending(x => x.LastModifiedDate);
                }
                else
                {
                    return query.ThenByDescending(x => x.LastModifiedDate);
                }
            }
            else if (string.Compare(propertyName, "Timestamp", true) == 0)
            {
                if (isFirstSort == true)
                {
                    return query.OrderByDescending(x => x.Timestamp);
                }
                else
                {
                    return query.ThenByDescending(x => x.Timestamp);
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