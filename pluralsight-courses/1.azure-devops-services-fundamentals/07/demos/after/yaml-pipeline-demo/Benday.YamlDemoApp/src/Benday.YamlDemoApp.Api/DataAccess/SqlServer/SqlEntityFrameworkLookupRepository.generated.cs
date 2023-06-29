using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Benday.YamlDemoApp.Api.DataAccess.Entities;
using Benday.EfCore.SqlServer;
using Benday.Common;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace Benday.YamlDemoApp.Api.DataAccess.SqlServer
{
    public partial class SqlEntityFrameworkLookupRepository :
        SqlEntityFrameworkSearchableRepositoryBase<LookupEntity, YamlDemoAppDbContext>,
        ILookupRepository
    {
        public SqlEntityFrameworkLookupRepository(
            YamlDemoAppDbContext context) : base(context)
        {
            
        }
        
        public virtual IList<LookupEntity> GetAllByType(string lookupType)
        {
            var results = (
            from temp in EntityDbSet
            where temp.LookupType == lookupType
            select temp
            ).ToList();
            
            return results;
        }
        protected override DbSet<LookupEntity> EntityDbSet => Context.LookupEntities;
        
        protected override Expression<Func<LookupEntity, bool>> GetPredicateForContains(
            SearchArgument arg)
        {
            if (arg is null)
            {
                throw new ArgumentNullException(nameof(arg));
            }
            else
            {
                
                if (arg.PropertyName == nameof(LookupEntity.LookupType))
                {
                    return q => q.LookupType.Contains(arg.SearchValue);
                }
                else if (arg.PropertyName == nameof(LookupEntity.LookupKey))
                {
                    return q => q.LookupKey.Contains(arg.SearchValue);
                }
                else if (arg.PropertyName == nameof(LookupEntity.LookupValue))
                {
                    return q => q.LookupValue.Contains(arg.SearchValue);
                }
                else if (arg.PropertyName == nameof(LookupEntity.Status))
                {
                    return q => q.Status.Contains(arg.SearchValue);
                }
                else if (arg.PropertyName == nameof(LookupEntity.CreatedBy))
                {
                    return q => q.CreatedBy.Contains(arg.SearchValue);
                }
                else if (arg.PropertyName == nameof(LookupEntity.LastModifiedBy))
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
        
        protected override Expression<Func<LookupEntity, bool>> GetPredicateForDoesNotContain(
            SearchArgument arg)
        {
            if (arg is null)
            {
                throw new ArgumentNullException(nameof(arg));
            }
            else
            {
                if (arg.PropertyName == nameof(LookupEntity.LookupType))
                {
                    return q => q.LookupType.Contains(arg.SearchValue) == false;
                }
                else if (arg.PropertyName == nameof(LookupEntity.LookupKey))
                {
                    return q => q.LookupKey.Contains(arg.SearchValue) == false;
                }
                else if (arg.PropertyName == nameof(LookupEntity.LookupValue))
                {
                    return q => q.LookupValue.Contains(arg.SearchValue) == false;
                }
                else if (arg.PropertyName == nameof(LookupEntity.Status))
                {
                    return q => q.Status.Contains(arg.SearchValue) == false;
                }
                else if (arg.PropertyName == nameof(LookupEntity.CreatedBy))
                {
                    return q => q.CreatedBy.Contains(arg.SearchValue) == false;
                }
                else if (arg.PropertyName == nameof(LookupEntity.LastModifiedBy))
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
        
        protected override Expression<Func<LookupEntity, bool>> GetPredicateForStartsWith(
            SearchArgument arg)
        {
            if (arg is null)
            {
                throw new ArgumentNullException(nameof(arg));
            }
            else
            {
                if (arg.PropertyName == nameof(LookupEntity.LookupType))
                {
                    return q => q.LookupType.StartsWith(arg.SearchValue);
                }
                else if (arg.PropertyName == nameof(LookupEntity.LookupKey))
                {
                    return q => q.LookupKey.StartsWith(arg.SearchValue);
                }
                else if (arg.PropertyName == nameof(LookupEntity.LookupValue))
                {
                    return q => q.LookupValue.StartsWith(arg.SearchValue);
                }
                else if (arg.PropertyName == nameof(LookupEntity.Status))
                {
                    return q => q.Status.StartsWith(arg.SearchValue);
                }
                else if (arg.PropertyName == nameof(LookupEntity.CreatedBy))
                {
                    return q => q.CreatedBy.StartsWith(arg.SearchValue);
                }
                else if (arg.PropertyName == nameof(LookupEntity.LastModifiedBy))
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
        
        protected override Expression<Func<LookupEntity, bool>> GetPredicateForEndsWith(
            SearchArgument arg)
        {
            if (arg is null)
            {
                throw new ArgumentNullException(nameof(arg));
            }
            else
            {
                if (arg.PropertyName == nameof(LookupEntity.LookupType))
                {
                    return q =>
                    q.LookupType.EndsWith(arg.SearchValue);
                }
                else if (arg.PropertyName == nameof(LookupEntity.LookupKey))
                {
                    return q =>
                    q.LookupKey.EndsWith(arg.SearchValue);
                }
                else if (arg.PropertyName == nameof(LookupEntity.LookupValue))
                {
                    return q =>
                    q.LookupValue.EndsWith(arg.SearchValue);
                }
                else if (arg.PropertyName == nameof(LookupEntity.Status))
                {
                    return q =>
                    q.Status.EndsWith(arg.SearchValue);
                }
                else if (arg.PropertyName == nameof(LookupEntity.CreatedBy))
                {
                    return q =>
                    q.CreatedBy.EndsWith(arg.SearchValue);
                }
                else if (arg.PropertyName == nameof(LookupEntity.LastModifiedBy))
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
        
        protected override Expression<Func<LookupEntity, bool>> GetPredicateForEquals(
            SearchArgument arg)
        {
            if (arg is null)
            {
                throw new ArgumentNullException(nameof(arg));
            }
            else
            {
                
                if (arg.PropertyName == nameof(LookupEntity.LookupType))
                {
                    return q => q.LookupType == arg.SearchValue;
                }
                else if (arg.PropertyName == nameof(LookupEntity.LookupKey))
                {
                    return q => q.LookupKey == arg.SearchValue;
                }
                else if (arg.PropertyName == nameof(LookupEntity.LookupValue))
                {
                    return q => q.LookupValue == arg.SearchValue;
                }
                else if (arg.PropertyName == nameof(LookupEntity.Status))
                {
                    return q => q.Status == arg.SearchValue;
                }
                else if (arg.PropertyName == nameof(LookupEntity.CreatedBy))
                {
                    return q => q.CreatedBy == arg.SearchValue;
                }
                else if (arg.PropertyName == nameof(LookupEntity.LastModifiedBy))
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
        
        protected override Expression<Func<LookupEntity, bool>> GetPredicateForIsNotEqualTo(
            SearchArgument arg)
        {
            if (arg is null)
            {
                throw new ArgumentNullException(nameof(arg));
            }
            else
            {
                if (arg.PropertyName == nameof(LookupEntity.LookupType))
                {
                    return q => q.LookupType == arg.SearchValue;
                }
                else if (arg.PropertyName == nameof(LookupEntity.LookupKey))
                {
                    return q => q.LookupKey == arg.SearchValue;
                }
                else if (arg.PropertyName == nameof(LookupEntity.LookupValue))
                {
                    return q => q.LookupValue == arg.SearchValue;
                }
                else if (arg.PropertyName == nameof(LookupEntity.Status))
                {
                    return q => q.Status == arg.SearchValue;
                }
                else if (arg.PropertyName == nameof(LookupEntity.CreatedBy))
                {
                    return q => q.CreatedBy == arg.SearchValue;
                }
                else if (arg.PropertyName == nameof(LookupEntity.LastModifiedBy))
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
        
        protected override IOrderedQueryable<LookupEntity> AddSortAscending(IOrderedQueryable<LookupEntity> query, string propertyName, bool isFirstSort)
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
            else if (string.Compare(propertyName, "DisplayOrder", true) == 0)
            {
                if (isFirstSort == true)
                {
                    return query.OrderBy(x => x.DisplayOrder);
                }
                else
                {
                    return query.ThenBy(x => x.DisplayOrder);
                }
            }
            else if (string.Compare(propertyName, "LookupType", true) == 0)
            {
                if (isFirstSort == true)
                {
                    return query.OrderBy(x => x.LookupType);
                }
                else
                {
                    return query.ThenBy(x => x.LookupType);
                }
            }
            else if (string.Compare(propertyName, "LookupKey", true) == 0)
            {
                if (isFirstSort == true)
                {
                    return query.OrderBy(x => x.LookupKey);
                }
                else
                {
                    return query.ThenBy(x => x.LookupKey);
                }
            }
            else if (string.Compare(propertyName, "LookupValue", true) == 0)
            {
                if (isFirstSort == true)
                {
                    return query.OrderBy(x => x.LookupValue);
                }
                else
                {
                    return query.ThenBy(x => x.LookupValue);
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
        
        protected override IOrderedQueryable<LookupEntity> AddSortDescending(IOrderedQueryable<LookupEntity> query, string propertyName, bool isFirstSort)
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
            else if (string.Compare(propertyName, "DisplayOrder", true) == 0)
            {
                if (isFirstSort == true)
                {
                    return query.OrderByDescending(x => x.DisplayOrder);
                }
                else
                {
                    return query.ThenByDescending(x => x.DisplayOrder);
                }
            }
            else if (string.Compare(propertyName, "LookupType", true) == 0)
            {
                if (isFirstSort == true)
                {
                    return query.OrderByDescending(x => x.LookupType);
                }
                else
                {
                    return query.ThenByDescending(x => x.LookupType);
                }
            }
            else if (string.Compare(propertyName, "LookupKey", true) == 0)
            {
                if (isFirstSort == true)
                {
                    return query.OrderByDescending(x => x.LookupKey);
                }
                else
                {
                    return query.ThenByDescending(x => x.LookupKey);
                }
            }
            else if (string.Compare(propertyName, "LookupValue", true) == 0)
            {
                if (isFirstSort == true)
                {
                    return query.OrderByDescending(x => x.LookupValue);
                }
                else
                {
                    return query.ThenByDescending(x => x.LookupValue);
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