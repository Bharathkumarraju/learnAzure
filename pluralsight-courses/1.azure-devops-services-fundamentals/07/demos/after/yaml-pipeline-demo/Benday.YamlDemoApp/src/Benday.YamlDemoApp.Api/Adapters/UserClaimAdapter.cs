using System;
using Benday.YamlDemoApp.Api.DataAccess.Entities;
using Benday.YamlDemoApp.Api.DomainModels;

namespace Benday.YamlDemoApp.Api.Adapters
{
    public partial class UserClaimAdapter :
        AdapterBase<UserClaim, UserClaimEntity>
    {

        protected override void PerformAdapt(
            UserClaim fromValue,
            UserClaimEntity toValue)
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

        protected override void PerformAdapt(
            UserClaimEntity fromValue,
            UserClaim toValue
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

            toValue.AcceptChanges();

        }
    }
}
