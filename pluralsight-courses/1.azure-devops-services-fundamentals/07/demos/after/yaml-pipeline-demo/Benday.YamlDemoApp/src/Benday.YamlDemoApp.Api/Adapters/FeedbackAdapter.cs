using System;
using Benday.YamlDemoApp.Api.DataAccess.Entities;
using Benday.YamlDemoApp.Api.DomainModels;

namespace Benday.YamlDemoApp.Api.Adapters
{
    public partial class FeedbackAdapter :
        AdapterBase<Feedback, FeedbackEntity>
    {

        protected override void PerformAdapt(
            Feedback fromValue,
            FeedbackEntity toValue)
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
            toValue.FeedbackType = fromValue.FeedbackType;
            toValue.Sentiment = fromValue.Sentiment;
            toValue.Subject = fromValue.Subject;
            toValue.FeedbackText = fromValue.FeedbackText;
            toValue.Username = fromValue.Username;
            toValue.FirstName = fromValue.FirstName;
            toValue.LastName = fromValue.LastName;
            toValue.Referer = fromValue.Referer;
            toValue.UserAgent = fromValue.UserAgent;
            toValue.IpAddress = fromValue.IpAddress;
            toValue.IsContactRequest = fromValue.IsContactRequest;
            toValue.Status = fromValue.Status;
            toValue.CreatedBy = fromValue.CreatedBy;
            toValue.CreatedDate = fromValue.CreatedDate;
            toValue.LastModifiedBy = fromValue.LastModifiedBy;
            toValue.LastModifiedDate = fromValue.LastModifiedDate;
            toValue.Timestamp = fromValue.Timestamp;


        }

        protected override void PerformAdapt(
            FeedbackEntity fromValue,
            Feedback toValue
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
            toValue.FeedbackType = fromValue.FeedbackType;
            toValue.Sentiment = fromValue.Sentiment;
            toValue.Subject = fromValue.Subject;
            toValue.FeedbackText = fromValue.FeedbackText;
            toValue.Username = fromValue.Username;
            toValue.FirstName = fromValue.FirstName;
            toValue.LastName = fromValue.LastName;
            toValue.Referer = fromValue.Referer;
            toValue.UserAgent = fromValue.UserAgent;
            toValue.IpAddress = fromValue.IpAddress;
            toValue.IsContactRequest = fromValue.IsContactRequest;
            toValue.Status = fromValue.Status;
            toValue.CreatedBy = fromValue.CreatedBy;
            toValue.CreatedDate = fromValue.CreatedDate;
            toValue.LastModifiedBy = fromValue.LastModifiedBy;
            toValue.LastModifiedDate = fromValue.LastModifiedDate;
            toValue.Timestamp = fromValue.Timestamp;

            toValue.AcceptChanges();

        }
    }
}
