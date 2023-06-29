using System;
using Benday.YamlDemoApp.Api.DataAccess.Entities;
using Benday.YamlDemoApp.Api.DomainModels;

namespace Benday.YamlDemoApp.Api.Adapters
{
    public partial class LookupAdapter :
        AdapterBase<Lookup, LookupEntity>
    {

        protected override void PerformAdapt(
            Lookup fromValue,
            LookupEntity toValue)
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

        protected override void PerformAdapt(
            LookupEntity fromValue,
            Lookup toValue
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

            toValue.AcceptChanges();

        }
    }
}
