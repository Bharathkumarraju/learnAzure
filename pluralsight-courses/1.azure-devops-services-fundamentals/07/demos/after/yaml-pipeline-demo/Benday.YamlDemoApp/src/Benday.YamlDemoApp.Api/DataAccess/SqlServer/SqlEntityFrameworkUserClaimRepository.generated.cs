using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Benday.YamlDemoApp.Api.DataAccess.Entities;
using Benday.EfCore.SqlServer;
using Benday.Common;
using System.Linq.Expressions;

namespace Benday.YamlDemoApp.Api.DataAccess.SqlServer
{
    public partial class SqlEntityFrameworkUserClaimRepository :
        SqlEntityFrameworkSearchableRepositoryBase<UserClaimEntity, YamlDemoAppDbContext>,
        IUserClaimRepository
    {
        public SqlEntityFrameworkUserClaimRepository(
            YamlDemoAppDbContext context) : base(context)
        {
            
        }
        
        protected override DbSet<UserClaimEntity> EntityDbSet => Context.UserClaimEntities;
        
        protected override Expression<Func<UserClaimEntity, bool>> GetPredicateForContains(
            SearchArgument arg)
        {
            if (arg is null)
            {
                throw new ArgumentNullException(nameof(arg));
            }
            else
            {
                
                if (arg.PropertyName == nameof(UserClaimEntity.Username))
                {
                    return q => q.Username.Contains(arg.SearchValue);
                }
                else if (arg.PropertyName == nameof(UserClaimEntity.ClaimName))
                {
                    return q => q.ClaimName.Contains(arg.SearchValue);
                }
                else if (arg.PropertyName == nameof(UserClaimEntity.ClaimLogicType))
                {
                    return q => q.ClaimLogicType.Contains(arg.SearchValue);
                }
                else if (arg.PropertyName == nameof(UserClaimEntity.Status))
                {
                    return q => q.Status.Contains(arg.SearchValue);
                }
                else if (arg.PropertyName == nameof(UserClaimEntity.CreatedBy))
                {
                    return q => q.CreatedBy.Contains(arg.SearchValue);
                }
                else if (arg.PropertyName == nameof(UserClaimEntity.LastModifiedBy))
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
        
        protected override Expression<Func<UserClaimEntity, bool>> GetPredicateForDoesNotContain(
            SearchArgument arg)
        {
            if (arg is null)
            {
                throw new ArgumentNullException(nameof(arg));
            }
            else
            {
                if (arg.PropertyName == nameof(UserClaimEntity.Username))
                {
                    return q => q.Username.Contains(arg.SearchValue) == false;
                }
                else if (arg.PropertyName == nameof(UserClaimEntity.ClaimName))
                {
                    return q => q.ClaimName.Contains(arg.SearchValue) == false;
                }
                else if (arg.PropertyName == nameof(UserClaimEntity.ClaimLogicType))
                {
                    return q => q.ClaimLogicType.Contains(arg.SearchValue) == false;
                }
                else if (arg.PropertyName == nameof(UserClaimEntity.Status))
                {
                    return q => q.Status.Contains(arg.SearchValue) == false;
                }
                else if (arg.PropertyName == nameof(UserClaimEntity.CreatedBy))
                {
                    return q => q.CreatedBy.Contains(arg.SearchValue) == false;
                }
                else if (arg.PropertyName == nameof(UserClaimEntity.LastModifiedBy))
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
        
        protected override Expression<Func<UserClaimEntity, bool>> GetPredicateForStartsWith(
            SearchArgument arg)
        {
            if (arg is null)
            {
                throw new ArgumentNullException(nameof(arg));
            }
            else
            {
                if (arg.PropertyName == nameof(UserClaimEntity.Username))
                {
                    return q => q.Username.StartsWith(arg.SearchValue);
                }
                else if (arg.PropertyName == nameof(UserClaimEntity.ClaimName))
                {
                    return q => q.ClaimName.StartsWith(arg.SearchValue);
                }
                else if (arg.PropertyName == nameof(UserClaimEntity.ClaimLogicType))
                {
                    return q => q.ClaimLogicType.StartsWith(arg.SearchValue);
                }
                else if (arg.PropertyName == nameof(UserClaimEntity.Status))
                {
                    return q => q.Status.StartsWith(arg.SearchValue);
                }
                else if (arg.PropertyName == nameof(UserClaimEntity.CreatedBy))
                {
                    return q => q.CreatedBy.StartsWith(arg.SearchValue);
                }
                else if (arg.PropertyName == nameof(UserClaimEntity.LastModifiedBy))
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
        
        protected override Expression<Func<UserClaimEntity, bool>> GetPredicateForEndsWith(
            SearchArgument arg)
        {
            if (arg is null)
            {
                throw new ArgumentNullException(nameof(arg));
            }
            else
            {
                if (arg.PropertyName == nameof(UserClaimEntity.Username))
                {
                    return q =>
                    q.Username.EndsWith(arg.SearchValue);
                }
                else if (arg.PropertyName == nameof(UserClaimEntity.ClaimName))
                {
                    return q =>
                    q.ClaimName.EndsWith(arg.SearchValue);
                }
                else if (arg.PropertyName == nameof(UserClaimEntity.ClaimLogicType))
                {
                    return q =>
                    q.ClaimLogicType.EndsWith(arg.SearchValue);
                }
                else if (arg.PropertyName == nameof(UserClaimEntity.Status))
                {
                    return q =>
                    q.Status.EndsWith(arg.SearchValue);
                }
                else if (arg.PropertyName == nameof(UserClaimEntity.CreatedBy))
                {
                    return q =>
                    q.CreatedBy.EndsWith(arg.SearchValue);
                }
                else if (arg.PropertyName == nameof(UserClaimEntity.LastModifiedBy))
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
        
        protected override Expression<Func<UserClaimEntity, bool>> GetPredicateForEquals(
            SearchArgument arg)
        {
            if (arg is null)
            {
                throw new ArgumentNullException(nameof(arg));
            }
            else
            {
                
                if (arg.PropertyName == nameof(UserClaimEntity.Username))
                {
                    return q => q.Username == arg.SearchValue;
                }
                else if (arg.PropertyName == nameof(UserClaimEntity.ClaimName))
                {
                    return q => q.ClaimName == arg.SearchValue;
                }
                else if (arg.PropertyName == nameof(UserClaimEntity.ClaimLogicType))
                {
                    return q => q.ClaimLogicType == arg.SearchValue;
                }
                else if (arg.PropertyName == nameof(UserClaimEntity.Status))
                {
                    return q => q.Status == arg.SearchValue;
                }
                else if (arg.PropertyName == nameof(UserClaimEntity.CreatedBy))
                {
                    return q => q.CreatedBy == arg.SearchValue;
                }
                else if (arg.PropertyName == nameof(UserClaimEntity.LastModifiedBy))
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
        
        protected override Expression<Func<UserClaimEntity, bool>> GetPredicateForIsNotEqualTo(
            SearchArgument arg)
        {
            if (arg is null)
            {
                throw new ArgumentNullException(nameof(arg));
            }
            else
            {
                if (arg.PropertyName == nameof(UserClaimEntity.Username))
                {
                    return q => q.Username == arg.SearchValue;
                }
                else if (arg.PropertyName == nameof(UserClaimEntity.ClaimName))
                {
                    return q => q.ClaimName == arg.SearchValue;
                }
                else if (arg.PropertyName == nameof(UserClaimEntity.ClaimLogicType))
                {
                    return q => q.ClaimLogicType == arg.SearchValue;
                }
                else if (arg.PropertyName == nameof(UserClaimEntity.Status))
                {
                    return q => q.Status == arg.SearchValue;
                }
                else if (arg.PropertyName == nameof(UserClaimEntity.CreatedBy))
                {
                    return q => q.CreatedBy == arg.SearchValue;
                }
                else if (arg.PropertyName == nameof(UserClaimEntity.LastModifiedBy))
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
        
        protected override IOrderedQueryable<UserClaimEntity> AddSortAscending(IOrderedQueryable<UserClaimEntity> query, string propertyName, bool isFirstSort)
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
            else if (string.Compare(propertyName, "ClaimName", true) == 0)
            {
                if (isFirstSort == true)
                {
                    return query.OrderBy(x => x.ClaimName);
                }
                else
                {
                    return query.ThenBy(x => x.ClaimName);
                }
            }
            else if (string.Compare(propertyName, "ClaimValue", true) == 0)
            {
                if (isFirstSort == true)
                {
                    return query.OrderBy(x => x.ClaimValue);
                }
                else
                {
                    return query.ThenBy(x => x.ClaimValue);
                }
            }
            else if (string.Compare(propertyName, "UserId", true) == 0)
            {
                if (isFirstSort == true)
                {
                    return query.OrderBy(x => x.UserId);
                }
                else
                {
                    return query.ThenBy(x => x.UserId);
                }
            }
            else if (string.Compare(propertyName, "User", true) == 0)
            {
                if (isFirstSort == true)
                {
                    return query.OrderBy(x => x.User);
                }
                else
                {
                    return query.ThenBy(x => x.User);
                }
            }
            else if (string.Compare(propertyName, "ClaimLogicType", true) == 0)
            {
                if (isFirstSort == true)
                {
                    return query.OrderBy(x => x.ClaimLogicType);
                }
                else
                {
                    return query.ThenBy(x => x.ClaimLogicType);
                }
            }
            else if (string.Compare(propertyName, "StartDate", true) == 0)
            {
                if (isFirstSort == true)
                {
                    return query.OrderBy(x => x.StartDate);
                }
                else
                {
                    return query.ThenBy(x => x.StartDate);
                }
            }
            else if (string.Compare(propertyName, "EndDate", true) == 0)
            {
                if (isFirstSort == true)
                {
                    return query.OrderBy(x => x.EndDate);
                }
                else
                {
                    return query.ThenBy(x => x.EndDate);
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
        
        protected override IOrderedQueryable<UserClaimEntity> AddSortDescending(IOrderedQueryable<UserClaimEntity> query, string propertyName, bool isFirstSort)
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
            else if (string.Compare(propertyName, "ClaimName", true) == 0)
            {
                if (isFirstSort == true)
                {
                    return query.OrderByDescending(x => x.ClaimName);
                }
                else
                {
                    return query.ThenByDescending(x => x.ClaimName);
                }
            }
            else if (string.Compare(propertyName, "ClaimValue", true) == 0)
            {
                if (isFirstSort == true)
                {
                    return query.OrderByDescending(x => x.ClaimValue);
                }
                else
                {
                    return query.ThenByDescending(x => x.ClaimValue);
                }
            }
            else if (string.Compare(propertyName, "UserId", true) == 0)
            {
                if (isFirstSort == true)
                {
                    return query.OrderByDescending(x => x.UserId);
                }
                else
                {
                    return query.ThenByDescending(x => x.UserId);
                }
            }
            else if (string.Compare(propertyName, "User", true) == 0)
            {
                if (isFirstSort == true)
                {
                    return query.OrderByDescending(x => x.User);
                }
                else
                {
                    return query.ThenByDescending(x => x.User);
                }
            }
            else if (string.Compare(propertyName, "ClaimLogicType", true) == 0)
            {
                if (isFirstSort == true)
                {
                    return query.OrderByDescending(x => x.ClaimLogicType);
                }
                else
                {
                    return query.ThenByDescending(x => x.ClaimLogicType);
                }
            }
            else if (string.Compare(propertyName, "StartDate", true) == 0)
            {
                if (isFirstSort == true)
                {
                    return query.OrderByDescending(x => x.StartDate);
                }
                else
                {
                    return query.ThenByDescending(x => x.StartDate);
                }
            }
            else if (string.Compare(propertyName, "EndDate", true) == 0)
            {
                if (isFirstSort == true)
                {
                    return query.OrderByDescending(x => x.EndDate);
                }
                else
                {
                    return query.ThenByDescending(x => x.EndDate);
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