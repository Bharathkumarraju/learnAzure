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
    public partial class UserClaimEditorViewModelAdapter :
        AdapterBase<
        Benday.YamlDemoApp.Api.DomainModels.UserClaim,
        Benday.YamlDemoApp.WebUi.Models.UserClaimEditorViewModel>
    {
    
        protected override void PerformAdapt(
            Benday.YamlDemoApp.Api.DomainModels.UserClaim fromValue,
            Benday.YamlDemoApp.WebUi.Models.UserClaimEditorViewModel toValue)
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
            toValue.Username = fromValue.Username;
            toValue.ClaimName = fromValue.ClaimName;
            toValue.ClaimValue = fromValue.ClaimValue;
            toValue.UserId = fromValue.UserId;
            toValue.ClaimLogicType = fromValue.ClaimLogicType;
            toValue.StartDate = fromValue.StartDate;
            toValue.EndDate = fromValue.EndDate;
            toValue.Status = fromValue.Status;
            toValue.CreatedBy = fromValue.CreatedBy;
            toValue.CreatedDate = fromValue.CreatedDate;
            toValue.LastModifiedBy = fromValue.LastModifiedBy;
            toValue.LastModifiedDate = fromValue.LastModifiedDate;
            toValue.Timestamp = fromValue.Timestamp;
            
            
        }
        
        protected override AdapterActions BeforeAdapt(
            Benday.YamlDemoApp.WebUi.Models.UserClaimEditorViewModel fromValue,
            Benday.YamlDemoApp.Api.DomainModels.UserClaim toValue)
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
            Benday.YamlDemoApp.WebUi.Models.UserClaimEditorViewModel fromValue,
            Benday.YamlDemoApp.Api.DomainModels.UserClaim toValue)
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
            toValue.Username = fromValue.Username;
            toValue.ClaimName = fromValue.ClaimName;
            toValue.ClaimValue = fromValue.ClaimValue;
            toValue.UserId = fromValue.UserId;
            toValue.ClaimLogicType = fromValue.ClaimLogicType;
            toValue.StartDate = fromValue.StartDate;
            toValue.EndDate = fromValue.EndDate;
            toValue.Status = fromValue.Status;
            toValue.CreatedBy = fromValue.CreatedBy;
            toValue.CreatedDate = fromValue.CreatedDate;
            toValue.LastModifiedBy = fromValue.LastModifiedBy;
            toValue.LastModifiedDate = fromValue.LastModifiedDate;
            toValue.Timestamp = fromValue.Timestamp;
            
            
        }
    }
}