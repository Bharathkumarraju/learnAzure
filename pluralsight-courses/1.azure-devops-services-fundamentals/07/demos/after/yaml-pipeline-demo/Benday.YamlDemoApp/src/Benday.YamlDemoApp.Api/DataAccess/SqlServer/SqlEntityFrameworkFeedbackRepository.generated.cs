using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Benday.YamlDemoApp.Api.DataAccess.Entities;
using Benday.EfCore.SqlServer;
using Benday.Common;
using System.Linq.Expressions;

namespace Benday.YamlDemoApp.Api.DataAccess.SqlServer
{
    public partial class SqlEntityFrameworkFeedbackRepository :
        SqlEntityFrameworkSearchableRepositoryBase<FeedbackEntity, YamlDemoAppDbContext>,
        IFeedbackRepository
    {
        public SqlEntityFrameworkFeedbackRepository(
            YamlDemoAppDbContext context) : base(context)
        {
            
        }
        
        protected override DbSet<FeedbackEntity> EntityDbSet => Context.FeedbackEntities;
        
        protected override Expression<Func<FeedbackEntity, bool>> GetPredicateForContains(
            SearchArgument arg)
        {
            if (arg is null)
            {
                throw new ArgumentNullException(nameof(arg));
            }
            else
            {
                
                if (arg.PropertyName == nameof(FeedbackEntity.FeedbackType))
                {
                    return q => q.FeedbackType.Contains(arg.SearchValue);
                }
                else if (arg.PropertyName == nameof(FeedbackEntity.Sentiment))
                {
                    return q => q.Sentiment.Contains(arg.SearchValue);
                }
                else if (arg.PropertyName == nameof(FeedbackEntity.Subject))
                {
                    return q => q.Subject.Contains(arg.SearchValue);
                }
                else if (arg.PropertyName == nameof(FeedbackEntity.FeedbackText))
                {
                    return q => q.FeedbackText.Contains(arg.SearchValue);
                }
                else if (arg.PropertyName == nameof(FeedbackEntity.Username))
                {
                    return q => q.Username.Contains(arg.SearchValue);
                }
                else if (arg.PropertyName == nameof(FeedbackEntity.FirstName))
                {
                    return q => q.FirstName.Contains(arg.SearchValue);
                }
                else if (arg.PropertyName == nameof(FeedbackEntity.LastName))
                {
                    return q => q.LastName.Contains(arg.SearchValue);
                }
                else if (arg.PropertyName == nameof(FeedbackEntity.Referer))
                {
                    return q => q.Referer.Contains(arg.SearchValue);
                }
                else if (arg.PropertyName == nameof(FeedbackEntity.UserAgent))
                {
                    return q => q.UserAgent.Contains(arg.SearchValue);
                }
                else if (arg.PropertyName == nameof(FeedbackEntity.IpAddress))
                {
                    return q => q.IpAddress.Contains(arg.SearchValue);
                }
                else if (arg.PropertyName == nameof(FeedbackEntity.Status))
                {
                    return q => q.Status.Contains(arg.SearchValue);
                }
                else if (arg.PropertyName == nameof(FeedbackEntity.CreatedBy))
                {
                    return q => q.CreatedBy.Contains(arg.SearchValue);
                }
                else if (arg.PropertyName == nameof(FeedbackEntity.LastModifiedBy))
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
        
        protected override Expression<Func<FeedbackEntity, bool>> GetPredicateForDoesNotContain(
            SearchArgument arg)
        {
            if (arg is null)
            {
                throw new ArgumentNullException(nameof(arg));
            }
            else
            {
                if (arg.PropertyName == nameof(FeedbackEntity.FeedbackType))
                {
                    return q => q.FeedbackType.Contains(arg.SearchValue) == false;
                }
                else if (arg.PropertyName == nameof(FeedbackEntity.Sentiment))
                {
                    return q => q.Sentiment.Contains(arg.SearchValue) == false;
                }
                else if (arg.PropertyName == nameof(FeedbackEntity.Subject))
                {
                    return q => q.Subject.Contains(arg.SearchValue) == false;
                }
                else if (arg.PropertyName == nameof(FeedbackEntity.FeedbackText))
                {
                    return q => q.FeedbackText.Contains(arg.SearchValue) == false;
                }
                else if (arg.PropertyName == nameof(FeedbackEntity.Username))
                {
                    return q => q.Username.Contains(arg.SearchValue) == false;
                }
                else if (arg.PropertyName == nameof(FeedbackEntity.FirstName))
                {
                    return q => q.FirstName.Contains(arg.SearchValue) == false;
                }
                else if (arg.PropertyName == nameof(FeedbackEntity.LastName))
                {
                    return q => q.LastName.Contains(arg.SearchValue) == false;
                }
                else if (arg.PropertyName == nameof(FeedbackEntity.Referer))
                {
                    return q => q.Referer.Contains(arg.SearchValue) == false;
                }
                else if (arg.PropertyName == nameof(FeedbackEntity.UserAgent))
                {
                    return q => q.UserAgent.Contains(arg.SearchValue) == false;
                }
                else if (arg.PropertyName == nameof(FeedbackEntity.IpAddress))
                {
                    return q => q.IpAddress.Contains(arg.SearchValue) == false;
                }
                else if (arg.PropertyName == nameof(FeedbackEntity.Status))
                {
                    return q => q.Status.Contains(arg.SearchValue) == false;
                }
                else if (arg.PropertyName == nameof(FeedbackEntity.CreatedBy))
                {
                    return q => q.CreatedBy.Contains(arg.SearchValue) == false;
                }
                else if (arg.PropertyName == nameof(FeedbackEntity.LastModifiedBy))
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
        
        protected override Expression<Func<FeedbackEntity, bool>> GetPredicateForStartsWith(
            SearchArgument arg)
        {
            if (arg is null)
            {
                throw new ArgumentNullException(nameof(arg));
            }
            else
            {
                if (arg.PropertyName == nameof(FeedbackEntity.FeedbackType))
                {
                    return q => q.FeedbackType.StartsWith(arg.SearchValue);
                }
                else if (arg.PropertyName == nameof(FeedbackEntity.Sentiment))
                {
                    return q => q.Sentiment.StartsWith(arg.SearchValue);
                }
                else if (arg.PropertyName == nameof(FeedbackEntity.Subject))
                {
                    return q => q.Subject.StartsWith(arg.SearchValue);
                }
                else if (arg.PropertyName == nameof(FeedbackEntity.FeedbackText))
                {
                    return q => q.FeedbackText.StartsWith(arg.SearchValue);
                }
                else if (arg.PropertyName == nameof(FeedbackEntity.Username))
                {
                    return q => q.Username.StartsWith(arg.SearchValue);
                }
                else if (arg.PropertyName == nameof(FeedbackEntity.FirstName))
                {
                    return q => q.FirstName.StartsWith(arg.SearchValue);
                }
                else if (arg.PropertyName == nameof(FeedbackEntity.LastName))
                {
                    return q => q.LastName.StartsWith(arg.SearchValue);
                }
                else if (arg.PropertyName == nameof(FeedbackEntity.Referer))
                {
                    return q => q.Referer.StartsWith(arg.SearchValue);
                }
                else if (arg.PropertyName == nameof(FeedbackEntity.UserAgent))
                {
                    return q => q.UserAgent.StartsWith(arg.SearchValue);
                }
                else if (arg.PropertyName == nameof(FeedbackEntity.IpAddress))
                {
                    return q => q.IpAddress.StartsWith(arg.SearchValue);
                }
                else if (arg.PropertyName == nameof(FeedbackEntity.Status))
                {
                    return q => q.Status.StartsWith(arg.SearchValue);
                }
                else if (arg.PropertyName == nameof(FeedbackEntity.CreatedBy))
                {
                    return q => q.CreatedBy.StartsWith(arg.SearchValue);
                }
                else if (arg.PropertyName == nameof(FeedbackEntity.LastModifiedBy))
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
        
        protected override Expression<Func<FeedbackEntity, bool>> GetPredicateForEndsWith(
            SearchArgument arg)
        {
            if (arg is null)
            {
                throw new ArgumentNullException(nameof(arg));
            }
            else
            {
                if (arg.PropertyName == nameof(FeedbackEntity.FeedbackType))
                {
                    return q =>
                    q.FeedbackType.EndsWith(arg.SearchValue);
                }
                else if (arg.PropertyName == nameof(FeedbackEntity.Sentiment))
                {
                    return q =>
                    q.Sentiment.EndsWith(arg.SearchValue);
                }
                else if (arg.PropertyName == nameof(FeedbackEntity.Subject))
                {
                    return q =>
                    q.Subject.EndsWith(arg.SearchValue);
                }
                else if (arg.PropertyName == nameof(FeedbackEntity.FeedbackText))
                {
                    return q =>
                    q.FeedbackText.EndsWith(arg.SearchValue);
                }
                else if (arg.PropertyName == nameof(FeedbackEntity.Username))
                {
                    return q =>
                    q.Username.EndsWith(arg.SearchValue);
                }
                else if (arg.PropertyName == nameof(FeedbackEntity.FirstName))
                {
                    return q =>
                    q.FirstName.EndsWith(arg.SearchValue);
                }
                else if (arg.PropertyName == nameof(FeedbackEntity.LastName))
                {
                    return q =>
                    q.LastName.EndsWith(arg.SearchValue);
                }
                else if (arg.PropertyName == nameof(FeedbackEntity.Referer))
                {
                    return q =>
                    q.Referer.EndsWith(arg.SearchValue);
                }
                else if (arg.PropertyName == nameof(FeedbackEntity.UserAgent))
                {
                    return q =>
                    q.UserAgent.EndsWith(arg.SearchValue);
                }
                else if (arg.PropertyName == nameof(FeedbackEntity.IpAddress))
                {
                    return q =>
                    q.IpAddress.EndsWith(arg.SearchValue);
                }
                else if (arg.PropertyName == nameof(FeedbackEntity.Status))
                {
                    return q =>
                    q.Status.EndsWith(arg.SearchValue);
                }
                else if (arg.PropertyName == nameof(FeedbackEntity.CreatedBy))
                {
                    return q =>
                    q.CreatedBy.EndsWith(arg.SearchValue);
                }
                else if (arg.PropertyName == nameof(FeedbackEntity.LastModifiedBy))
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
        
        protected override Expression<Func<FeedbackEntity, bool>> GetPredicateForEquals(
            SearchArgument arg)
        {
            if (arg is null)
            {
                throw new ArgumentNullException(nameof(arg));
            }
            else
            {
                
                if (arg.PropertyName == nameof(FeedbackEntity.FeedbackType))
                {
                    return q => q.FeedbackType == arg.SearchValue;
                }
                else if (arg.PropertyName == nameof(FeedbackEntity.Sentiment))
                {
                    return q => q.Sentiment == arg.SearchValue;
                }
                else if (arg.PropertyName == nameof(FeedbackEntity.Subject))
                {
                    return q => q.Subject == arg.SearchValue;
                }
                else if (arg.PropertyName == nameof(FeedbackEntity.FeedbackText))
                {
                    return q => q.FeedbackText == arg.SearchValue;
                }
                else if (arg.PropertyName == nameof(FeedbackEntity.Username))
                {
                    return q => q.Username == arg.SearchValue;
                }
                else if (arg.PropertyName == nameof(FeedbackEntity.FirstName))
                {
                    return q => q.FirstName == arg.SearchValue;
                }
                else if (arg.PropertyName == nameof(FeedbackEntity.LastName))
                {
                    return q => q.LastName == arg.SearchValue;
                }
                else if (arg.PropertyName == nameof(FeedbackEntity.Referer))
                {
                    return q => q.Referer == arg.SearchValue;
                }
                else if (arg.PropertyName == nameof(FeedbackEntity.UserAgent))
                {
                    return q => q.UserAgent == arg.SearchValue;
                }
                else if (arg.PropertyName == nameof(FeedbackEntity.IpAddress))
                {
                    return q => q.IpAddress == arg.SearchValue;
                }
                else if (arg.PropertyName == nameof(FeedbackEntity.Status))
                {
                    return q => q.Status == arg.SearchValue;
                }
                else if (arg.PropertyName == nameof(FeedbackEntity.CreatedBy))
                {
                    return q => q.CreatedBy == arg.SearchValue;
                }
                else if (arg.PropertyName == nameof(FeedbackEntity.LastModifiedBy))
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
        
        protected override Expression<Func<FeedbackEntity, bool>> GetPredicateForIsNotEqualTo(
            SearchArgument arg)
        {
            if (arg is null)
            {
                throw new ArgumentNullException(nameof(arg));
            }
            else
            {
                if (arg.PropertyName == nameof(FeedbackEntity.FeedbackType))
                {
                    return q => q.FeedbackType == arg.SearchValue;
                }
                else if (arg.PropertyName == nameof(FeedbackEntity.Sentiment))
                {
                    return q => q.Sentiment == arg.SearchValue;
                }
                else if (arg.PropertyName == nameof(FeedbackEntity.Subject))
                {
                    return q => q.Subject == arg.SearchValue;
                }
                else if (arg.PropertyName == nameof(FeedbackEntity.FeedbackText))
                {
                    return q => q.FeedbackText == arg.SearchValue;
                }
                else if (arg.PropertyName == nameof(FeedbackEntity.Username))
                {
                    return q => q.Username == arg.SearchValue;
                }
                else if (arg.PropertyName == nameof(FeedbackEntity.FirstName))
                {
                    return q => q.FirstName == arg.SearchValue;
                }
                else if (arg.PropertyName == nameof(FeedbackEntity.LastName))
                {
                    return q => q.LastName == arg.SearchValue;
                }
                else if (arg.PropertyName == nameof(FeedbackEntity.Referer))
                {
                    return q => q.Referer == arg.SearchValue;
                }
                else if (arg.PropertyName == nameof(FeedbackEntity.UserAgent))
                {
                    return q => q.UserAgent == arg.SearchValue;
                }
                else if (arg.PropertyName == nameof(FeedbackEntity.IpAddress))
                {
                    return q => q.IpAddress == arg.SearchValue;
                }
                else if (arg.PropertyName == nameof(FeedbackEntity.Status))
                {
                    return q => q.Status == arg.SearchValue;
                }
                else if (arg.PropertyName == nameof(FeedbackEntity.CreatedBy))
                {
                    return q => q.CreatedBy == arg.SearchValue;
                }
                else if (arg.PropertyName == nameof(FeedbackEntity.LastModifiedBy))
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
        
        protected override IOrderedQueryable<FeedbackEntity> AddSortAscending(IOrderedQueryable<FeedbackEntity> query, string propertyName, bool isFirstSort)
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
            else if (string.Compare(propertyName, "FeedbackType", true) == 0)
            {
                if (isFirstSort == true)
                {
                    return query.OrderBy(x => x.FeedbackType);
                }
                else
                {
                    return query.ThenBy(x => x.FeedbackType);
                }
            }
            else if (string.Compare(propertyName, "Sentiment", true) == 0)
            {
                if (isFirstSort == true)
                {
                    return query.OrderBy(x => x.Sentiment);
                }
                else
                {
                    return query.ThenBy(x => x.Sentiment);
                }
            }
            else if (string.Compare(propertyName, "Subject", true) == 0)
            {
                if (isFirstSort == true)
                {
                    return query.OrderBy(x => x.Subject);
                }
                else
                {
                    return query.ThenBy(x => x.Subject);
                }
            }
            else if (string.Compare(propertyName, "FeedbackText", true) == 0)
            {
                if (isFirstSort == true)
                {
                    return query.OrderBy(x => x.FeedbackText);
                }
                else
                {
                    return query.ThenBy(x => x.FeedbackText);
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
            else if (string.Compare(propertyName, "Referer", true) == 0)
            {
                if (isFirstSort == true)
                {
                    return query.OrderBy(x => x.Referer);
                }
                else
                {
                    return query.ThenBy(x => x.Referer);
                }
            }
            else if (string.Compare(propertyName, "UserAgent", true) == 0)
            {
                if (isFirstSort == true)
                {
                    return query.OrderBy(x => x.UserAgent);
                }
                else
                {
                    return query.ThenBy(x => x.UserAgent);
                }
            }
            else if (string.Compare(propertyName, "IpAddress", true) == 0)
            {
                if (isFirstSort == true)
                {
                    return query.OrderBy(x => x.IpAddress);
                }
                else
                {
                    return query.ThenBy(x => x.IpAddress);
                }
            }
            else if (string.Compare(propertyName, "IsContactRequest", true) == 0)
            {
                if (isFirstSort == true)
                {
                    return query.OrderBy(x => x.IsContactRequest);
                }
                else
                {
                    return query.ThenBy(x => x.IsContactRequest);
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
        
        protected override IOrderedQueryable<FeedbackEntity> AddSortDescending(IOrderedQueryable<FeedbackEntity> query, string propertyName, bool isFirstSort)
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
            else if (string.Compare(propertyName, "FeedbackType", true) == 0)
            {
                if (isFirstSort == true)
                {
                    return query.OrderByDescending(x => x.FeedbackType);
                }
                else
                {
                    return query.ThenByDescending(x => x.FeedbackType);
                }
            }
            else if (string.Compare(propertyName, "Sentiment", true) == 0)
            {
                if (isFirstSort == true)
                {
                    return query.OrderByDescending(x => x.Sentiment);
                }
                else
                {
                    return query.ThenByDescending(x => x.Sentiment);
                }
            }
            else if (string.Compare(propertyName, "Subject", true) == 0)
            {
                if (isFirstSort == true)
                {
                    return query.OrderByDescending(x => x.Subject);
                }
                else
                {
                    return query.ThenByDescending(x => x.Subject);
                }
            }
            else if (string.Compare(propertyName, "FeedbackText", true) == 0)
            {
                if (isFirstSort == true)
                {
                    return query.OrderByDescending(x => x.FeedbackText);
                }
                else
                {
                    return query.ThenByDescending(x => x.FeedbackText);
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
            else if (string.Compare(propertyName, "Referer", true) == 0)
            {
                if (isFirstSort == true)
                {
                    return query.OrderByDescending(x => x.Referer);
                }
                else
                {
                    return query.ThenByDescending(x => x.Referer);
                }
            }
            else if (string.Compare(propertyName, "UserAgent", true) == 0)
            {
                if (isFirstSort == true)
                {
                    return query.OrderByDescending(x => x.UserAgent);
                }
                else
                {
                    return query.ThenByDescending(x => x.UserAgent);
                }
            }
            else if (string.Compare(propertyName, "IpAddress", true) == 0)
            {
                if (isFirstSort == true)
                {
                    return query.OrderByDescending(x => x.IpAddress);
                }
                else
                {
                    return query.ThenByDescending(x => x.IpAddress);
                }
            }
            else if (string.Compare(propertyName, "IsContactRequest", true) == 0)
            {
                if (isFirstSort == true)
                {
                    return query.OrderByDescending(x => x.IsContactRequest);
                }
                else
                {
                    return query.ThenByDescending(x => x.IsContactRequest);
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