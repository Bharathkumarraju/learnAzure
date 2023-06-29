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
    public partial class ConfigurationItemEditorViewModelAdapter :
        AdapterBase<
        Benday.YamlDemoApp.Api.DomainModels.ConfigurationItem,
        Benday.YamlDemoApp.WebUi.Models.ConfigurationItemEditorViewModel>
    {
    
        protected override void PerformAdapt(
            Benday.YamlDemoApp.Api.DomainModels.ConfigurationItem fromValue,
            Benday.YamlDemoApp.WebUi.Models.ConfigurationItemEditorViewModel toValue)
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
            toValue.ConfigurationKey = fromValue.ConfigurationKey;
            toValue.Description = fromValue.Description;
            toValue.ConfigurationValue = fromValue.ConfigurationValue;
            toValue.Status = fromValue.Status;
            toValue.CreatedBy = fromValue.CreatedBy;
            toValue.CreatedDate = fromValue.CreatedDate;
            toValue.LastModifiedBy = fromValue.LastModifiedBy;
            toValue.LastModifiedDate = fromValue.LastModifiedDate;
            toValue.Timestamp = fromValue.Timestamp;
            
            
        }
        
        protected override AdapterActions BeforeAdapt(
            Benday.YamlDemoApp.WebUi.Models.ConfigurationItemEditorViewModel fromValue,
            Benday.YamlDemoApp.Api.DomainModels.ConfigurationItem toValue)
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
            Benday.YamlDemoApp.WebUi.Models.ConfigurationItemEditorViewModel fromValue,
            Benday.YamlDemoApp.Api.DomainModels.ConfigurationItem toValue)
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
            toValue.ConfigurationKey = fromValue.ConfigurationKey;
            toValue.Description = fromValue.Description;
            toValue.ConfigurationValue = fromValue.ConfigurationValue;
            toValue.Status = fromValue.Status;
            toValue.CreatedBy = fromValue.CreatedBy;
            toValue.CreatedDate = fromValue.CreatedDate;
            toValue.LastModifiedBy = fromValue.LastModifiedBy;
            toValue.LastModifiedDate = fromValue.LastModifiedDate;
            toValue.Timestamp = fromValue.Timestamp;
            
            
        }
    }
}