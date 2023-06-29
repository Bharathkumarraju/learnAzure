using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Benday.YamlDemoApp.Api.DataAccess.Entities;
using Benday.EfCore.SqlServer;
using Benday.Common;
using System.Linq.Expressions;

namespace Benday.YamlDemoApp.Api.DataAccess.SqlServer
{
    public partial class SqlEntityFrameworkUserRepository :
        SqlEntityFrameworkSearchableRepositoryBase<UserEntity, YamlDemoAppDbContext>,
        IUserRepository
    {
        public SqlEntityFrameworkUserRepository(
            YamlDemoAppDbContext context) : base(context)
        {
            
        }
        
        protected override IQueryable<UserEntity> AddIncludes(IQueryable<UserEntity> queryable)
        {
            return queryable
            .Include(x => x.Claims)
            ;
        }
        protected override DbSet<UserEntity> EntityDbSet => Context.UserEntities;
        
        protected override Expression<Func<UserEntity, bool>> GetPredicateForContains(
            SearchArgument arg)
        {
            if (arg is null)
            {
                throw new ArgumentNullException(nameof(arg));
            }
            else
            {
                
                if (arg.PropertyName == nameof(UserEntity.Username))
                {
                    return q => q.Username.Contains(arg.SearchValue);
                }
                else if (arg.PropertyName == nameof(UserEntity.Source))
                {
                    return q => q.Source.Contains(arg.SearchValue);
                }
                else if (arg.PropertyName == nameof(UserEntity.EmailAddress))
                {
                    return q => q.EmailAddress.Contains(arg.SearchValue);
                }
                else if (arg.PropertyName == nameof(UserEntity.FirstName))
                {
                    return q => q.FirstName.Contains(arg.SearchValue);
                }
                else if (arg.PropertyName == nameof(UserEntity.LastName))
                {
                    return q => q.LastName.Contains(arg.SearchValue);
                }
                else if (arg.PropertyName == nameof(UserEntity.PhoneNumber))
                {
                    return q => q.PhoneNumber.Contains(arg.SearchValue);
                }
                else if (arg.PropertyName == nameof(UserEntity.Status))
                {
                    return q => q.Status.Contains(arg.SearchValue);
                }
                else if (arg.PropertyName == nameof(UserEntity.CreatedBy))
                {
                    return q => q.CreatedBy.Contains(arg.SearchValue);
                }
                else if (arg.PropertyName == nameof(UserEntity.LastModifiedBy))
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
        
        protected override Expression<Func<UserEntity, bool>> GetPredicateForDoesNotContain(
            SearchArgument arg)
        {
            if (arg is null)
            {
                throw new ArgumentNullException(nameof(arg));
            }
            else
            {
                if (arg.PropertyName == nameof(UserEntity.Username))
                {
                    return q => q.Username.Contains(arg.SearchValue) == false;
                }
                else if (arg.PropertyName == nameof(UserEntity.Source))
                {
                    return q => q.Source.Contains(arg.SearchValue) == false;
                }
                else if (arg.PropertyName == nameof(UserEntity.EmailAddress))
                {
                    return q => q.EmailAddress.Contains(arg.SearchValue) == false;
                }
                else if (arg.PropertyName == nameof(UserEntity.FirstName))
                {
                    return q => q.FirstName.Contains(arg.SearchValue) == false;
                }
                else if (arg.PropertyName == nameof(UserEntity.LastName))
                {
                    return q => q.LastName.Contains(arg.SearchValue) == false;
                }
                else if (arg.PropertyName == nameof(UserEntity.PhoneNumber))
                {
                    return q => q.PhoneNumber.Contains(arg.SearchValue) == false;
                }
                else if (arg.PropertyName == nameof(UserEntity.Status))
                {
                    return q => q.Status.Contains(arg.SearchValue) == false;
                }
                else if (arg.PropertyName == nameof(UserEntity.CreatedBy))
                {
                    return q => q.CreatedBy.Contains(arg.SearchValue) == false;
                }
                else if (arg.PropertyName == nameof(UserEntity.LastModifiedBy))
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
        
        protected override Expression<Func<UserEntity, bool>> GetPredicateForStartsWith(
            SearchArgument arg)
        {
            if (arg is null)
            {
                throw new ArgumentNullException(nameof(arg));
            }
            else
            {
                if (arg.PropertyName == nameof(UserEntity.Username))
                {
                    return q => q.Username.StartsWith(arg.SearchValue);
                }
                else if (arg.PropertyName == nameof(UserEntity.Source))
                {
                    return q => q.Source.StartsWith(arg.SearchValue);
                }
                else if (arg.PropertyName == nameof(UserEntity.EmailAddress))
                {
                    return q => q.EmailAddress.StartsWith(arg.SearchValue);
                }
                else if (arg.PropertyName == nameof(UserEntity.FirstName))
                {
                    return q => q.FirstName.StartsWith(arg.SearchValue);
                }
                else if (arg.PropertyName == nameof(UserEntity.LastName))
                {
                    return q => q.LastName.StartsWith(arg.SearchValue);
                }
                else if (arg.PropertyName == nameof(UserEntity.PhoneNumber))
                {
                    return q => q.PhoneNumber.StartsWith(arg.SearchValue);
                }
                else if (arg.PropertyName == nameof(UserEntity.Status))
                {
                    return q => q.Status.StartsWith(arg.SearchValue);
                }
                else if (arg.PropertyName == nameof(UserEntity.CreatedBy))
                {
                    return q => q.CreatedBy.StartsWith(arg.SearchValue);
                }
                else if (arg.PropertyName == nameof(UserEntity.LastModifiedBy))
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
        
        protected override Expression<Func<UserEntity, bool>> GetPredicateForEndsWith(
            SearchArgument arg)
        {
            if (arg is null)
            {
                throw new ArgumentNullException(nameof(arg));
            }
            else
            {
                if (arg.PropertyName == nameof(UserEntity.Username))
                {
                    return q =>
                    q.Username.EndsWith(arg.SearchValue);
                }
                else if (arg.PropertyName == nameof(UserEntity.Source))
                {
                    return q =>
                    q.Source.EndsWith(arg.SearchValue);
                }
                else if (arg.PropertyName == nameof(UserEntity.EmailAddress))
                {
                    return q =>
                    q.EmailAddress.EndsWith(arg.SearchValue);
                }
                else if (arg.PropertyName == nameof(UserEntity.FirstName))
                {
                    return q =>
                    q.FirstName.EndsWith(arg.SearchValue);
                }
                else if (arg.PropertyName == nameof(UserEntity.LastName))
                {
                    return q =>
                    q.LastName.EndsWith(arg.SearchValue);
                }
                else if (arg.PropertyName == nameof(UserEntity.PhoneNumber))
                {
                    return q =>
                    q.PhoneNumber.EndsWith(arg.SearchValue);
                }
                else if (arg.PropertyName == nameof(UserEntity.Status))
                {
                    return q =>
                    q.Status.EndsWith(arg.SearchValue);
                }
                else if (arg.PropertyName == nameof(UserEntity.CreatedBy))
                {
                    return q =>
                    q.CreatedBy.EndsWith(arg.SearchValue);
                }
                else if (arg.PropertyName == nameof(UserEntity.LastModifiedBy))
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
        
        protected override Expression<Func<UserEntity, bool>> GetPredicateForEquals(
            SearchArgument arg)
        {
            if (arg is null)
            {
                throw new ArgumentNullException(nameof(arg));
            }
            else
            {
                
                if (arg.PropertyName == nameof(UserEntity.Username))
                {
                    return q => q.Username == arg.SearchValue;
                }
                else if (arg.PropertyName == nameof(UserEntity.Source))
                {
                    return q => q.Source == arg.SearchValue;
                }
                else if (arg.PropertyName == nameof(UserEntity.EmailAddress))
                {
                    return q => q.EmailAddress == arg.SearchValue;
                }
                else if (arg.PropertyName == nameof(UserEntity.FirstName))
                {
                    return q => q.FirstName == arg.SearchValue;
                }
                else if (arg.PropertyName == nameof(UserEntity.LastName))
                {
                    return q => q.LastName == arg.SearchValue;
                }
                else if (arg.PropertyName == nameof(UserEntity.PhoneNumber))
                {
                    return q => q.PhoneNumber == arg.SearchValue;
                }
                else if (arg.PropertyName == nameof(UserEntity.Status))
                {
                    return q => q.Status == arg.SearchValue;
                }
                else if (arg.PropertyName == nameof(UserEntity.CreatedBy))
                {
                    return q => q.CreatedBy == arg.SearchValue;
                }
                else if (arg.PropertyName == nameof(UserEntity.LastModifiedBy))
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
        
        protected override Expression<Func<UserEntity, bool>> GetPredicateForIsNotEqualTo(
            SearchArgument arg)
        {
            if (arg is null)
            {
                throw new ArgumentNullException(nameof(arg));
            }
            else
            {
                if (arg.PropertyName == nameof(UserEntity.Username))
                {
                    return q => q.Username == arg.SearchValue;
                }
                else if (arg.PropertyName == nameof(UserEntity.Source))
                {
                    return q => q.Source == arg.SearchValue;
                }
                else if (arg.PropertyName == nameof(UserEntity.EmailAddress))
                {
                    return q => q.EmailAddress == arg.SearchValue;
                }
                else if (arg.PropertyName == nameof(UserEntity.FirstName))
                {
                    return q => q.FirstName == arg.SearchValue;
                }
                else if (arg.PropertyName == nameof(UserEntity.LastName))
                {
                    return q => q.LastName == arg.SearchValue;
                }
                else if (arg.PropertyName == nameof(UserEntity.PhoneNumber))
                {
                    return q => q.PhoneNumber == arg.SearchValue;
                }
                else if (arg.PropertyName == nameof(UserEntity.Status))
                {
                    return q => q.Status == arg.SearchValue;
                }
                else if (arg.PropertyName == nameof(UserEntity.CreatedBy))
                {
                    return q => q.CreatedBy == arg.SearchValue;
                }
                else if (arg.PropertyName == nameof(UserEntity.LastModifiedBy))
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
        
        protected override IOrderedQueryable<UserEntity> AddSortAscending(IOrderedQueryable<UserEntity> query, string propertyName, bool isFirstSort)
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
            else if (string.Compare(propertyName, "Username", true) == 0)
            {
                if (isFirstSort == true)
                {
                    return query.OrderBy(x => x.Username);
                }
                else
                {
                    return query.ThenBy(x => x.Username);
                }
            }
            else if (string.Compare(propertyName, "Source", true) == 0)
            {
                if (isFirstSort == true)
                {
                    return query.OrderBy(x => x.Source);
                }
                else
                {
                    return query.ThenBy(x => x.Source);
                }
            }
            else if (string.Compare(propertyName, "EmailAddress", true) == 0)
            {
                if (isFirstSort == true)
                {
                    return query.OrderBy(x => x.EmailAddress);
                }
                else
                {
                    return query.ThenBy(x => x.EmailAddress);
                }
            }
            else if (string.Compare(propertyName, "FirstName", true) == 0)
            {
                if (isFirstSort == true)
                {
                    return query.OrderBy(x => x.FirstName);
                }
                else
                {
                    return query.ThenBy(x => x.FirstName);
                }
            }
            else if (string.Compare(propertyName, "LastName", true) == 0)
            {
                if (isFirstSort == true)
                {
                    return query.OrderBy(x => x.LastName);
                }
                else
                {
                    return query.ThenBy(x => x.LastName);
                }
            }
            else if (string.Compare(propertyName, "PhoneNumber", true) == 0)
            {
                if (isFirstSort == true)
                {
                    return query.OrderBy(x => x.PhoneNumber);
                }
                else
                {
                    return query.ThenBy(x => x.PhoneNumber);
                }
            }
            else if (string.Compare(propertyName, "Claims", true) == 0)
            {
                if (isFirstSort == true)
                {
                    return query.OrderBy(x => x.Claims);
                }
                else
                {
                    return query.ThenBy(x => x.Claims);
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
        
        protected override IOrderedQueryable<UserEntity> AddSortDescending(IOrderedQueryable<UserEntity> query, string propertyName, bool isFirstSort)
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
            else if (string.Compare(propertyName, "Username", true) == 0)
            {
                if (isFirstSort == true)
                {
                    return query.OrderByDescending(x => x.Username);
                }
                else
                {
                    return query.ThenByDescending(x => x.Username);
                }
            }
            else if (string.Compare(propertyName, "Source", true) == 0)
            {
                if (isFirstSort == true)
                {
                    return query.OrderByDescending(x => x.Source);
                }
                else
                {
                    return query.ThenByDescending(x => x.Source);
                }
            }
            else if (string.Compare(propertyName, "EmailAddress", true) == 0)
            {
                if (isFirstSort == true)
                {
                    return query.OrderByDescending(x => x.EmailAddress);
                }
                else
                {
                    return query.ThenByDescending(x => x.EmailAddress);
                }
            }
            else if (string.Compare(propertyName, "FirstName", true) == 0)
            {
                if (isFirstSort == true)
                {
                    return query.OrderByDescending(x => x.FirstName);
                }
                else
                {
                    return query.ThenByDescending(x => x.FirstName);
                }
            }
            else if (string.Compare(propertyName, "LastName", true) == 0)
            {
                if (isFirstSort == true)
                {
                    return query.OrderByDescending(x => x.LastName);
                }
                else
                {
                    return query.ThenByDescending(x => x.LastName);
                }
            }
            else if (string.Compare(propertyName, "PhoneNumber", true) == 0)
            {
                if (isFirstSort == true)
                {
                    return query.OrderByDescending(x => x.PhoneNumber);
                }
                else
                {
                    return query.ThenByDescending(x => x.PhoneNumber);
                }
            }
            else if (string.Compare(propertyName, "Claims", true) == 0)
            {
                if (isFirstSort == true)
                {
                    return query.OrderByDescending(x => x.Claims);
                }
                else
                {
                    return query.ThenByDescending(x => x.Claims);
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