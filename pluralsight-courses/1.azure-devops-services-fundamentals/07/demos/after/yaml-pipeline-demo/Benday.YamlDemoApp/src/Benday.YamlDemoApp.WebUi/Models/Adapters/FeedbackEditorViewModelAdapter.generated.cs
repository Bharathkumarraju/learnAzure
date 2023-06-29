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
    public partial class FeedbackEditorViewModelAdapter :
        AdapterBase<
        Benday.YamlDemoApp.Api.DomainModels.Feedback,
        Benday.YamlDemoApp.WebUi.Models.FeedbackEditorViewModel>
    {
    
        protected override void PerformAdapt(
            Benday.YamlDemoApp.Api.DomainModels.Feedback fromValue,
            Benday.YamlDemoApp.WebUi.Models.FeedbackEditorViewModel toValue)
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
        
        protected override AdapterActions BeforeAdapt(
            Benday.YamlDemoApp.WebUi.Models.FeedbackEditorViewModel fromValue,
            Benday.YamlDemoApp.Api.DomainModels.Feedback toValue)
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
            Benday.YamlDemoApp.WebUi.Models.FeedbackEditorViewModel fromValue,
            Benday.YamlDemoApp.Api.DomainModels.Feedback toValue)
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
    }
}