using System;
using Benday.YamlDemoApp.Api.DataAccess.Entities;
using Benday.YamlDemoApp.Api.DomainModels;

namespace Benday.YamlDemoApp.Api.Adapters
{
    public partial class UserAdapter :
        AdapterBase<User, UserEntity>
    {

        protected override void PerformAdapt(
            User fromValue,
            UserEntity toValue)
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
            toValue.Source = fromValue.Source;
            toValue.EmailAddress = fromValue.EmailAddress;
            toValue.FirstName = fromValue.FirstName;
            toValue.LastName = fromValue.LastName;
            toValue.PhoneNumber = fromValue.PhoneNumber;
            new UserClaimAdapter().Adapt(
            fromValue.Claims,
            toValue.Claims);
            toValue.Status = fromValue.Status;
            toValue.CreatedBy = fromValue.CreatedBy;
            toValue.CreatedDate = fromValue.CreatedDate;
            toValue.LastModifiedBy = fromValue.LastModifiedBy;
            toValue.LastModifiedDate = fromValue.LastModifiedDate;
            toValue.Timestamp = fromValue.Timestamp;


        }

        protected override void PerformAdapt(
            UserEntity fromValue,
            User toValue
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
            toValue.Source = fromValue.Source;
            toValue.EmailAddress = fromValue.EmailAddress;
            toValue.FirstName = fromValue.FirstName;
            toValue.LastName = fromValue.LastName;
            toValue.PhoneNumber = fromValue.PhoneNumber;
            new UserClaimAdapter().Adapt(
            fromValue.Claims,
            toValue.Claims);
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
