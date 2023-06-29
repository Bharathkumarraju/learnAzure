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
    public partial class LookupEditorViewModelAdapter :
        AdapterBase<
        Benday.YamlDemoApp.Api.DomainModels.Lookup,
        Benday.YamlDemoApp.WebUi.Models.LookupEditorViewModel>
    {
    
        protected override void PerformAdapt(
            Benday.YamlDemoApp.Api.DomainModels.Lookup fromValue,
            Benday.YamlDemoApp.WebUi.Models.LookupEditorViewModel toValue)
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
            toValue.DisplayOrder = fromValue.DisplayOrder;
            toValue.LookupType = fromValue.LookupType;
            toValue.LookupKey = fromValue.LookupKey;
            toValue.LookupValue = fromValue.LookupValue;
            toValue.Status = fromValue.Status;
            toValue.CreatedBy = fromValue.CreatedBy;
            toValue.CreatedDate = fromValue.CreatedDate;
            toValue.LastModifiedBy = fromValue.LastModifiedBy;
            toValue.LastModifiedDate = fromValue.LastModifiedDate;
            toValue.Timestamp = fromValue.Timestamp;
            
            
        }
        
        protected override AdapterActions BeforeAdapt(
            Benday.YamlDemoApp.WebUi.Models.LookupEditorViewModel fromValue,
            Benday.YamlDemoApp.Api.DomainModels.Lookup toValue)
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
            Benday.YamlDemoApp.WebUi.Models.LookupEditorViewModel fromValue,
            Benday.YamlDemoApp.Api.DomainModels.Lookup toValue)
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
            toValue.DisplayOrder = fromValue.DisplayOrder;
            toValue.LookupType = fromValue.LookupType;
            toValue.LookupKey = fromValue.LookupKey;
            toValue.LookupValue = fromValue.LookupValue;
            toValue.Status = fromValue.Status;
            toValue.CreatedBy = fromValue.CreatedBy;
            toValue.CreatedDate = fromValue.CreatedDate;
            toValue.LastModifiedBy = fromValue.LastModifiedBy;
            toValue.LastModifiedDate = fromValue.LastModifiedDate;
            toValue.Timestamp = fromValue.Timestamp;
            
            
        }
    }
}