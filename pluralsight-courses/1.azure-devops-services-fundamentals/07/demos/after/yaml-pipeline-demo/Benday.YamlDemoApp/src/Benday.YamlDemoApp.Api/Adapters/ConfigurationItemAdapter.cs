using System;
using Benday.YamlDemoApp.Api.DataAccess.Entities;
using Benday.YamlDemoApp.Api.DomainModels;

namespace Benday.YamlDemoApp.Api.Adapters
{
    public partial class ConfigurationItemAdapter :
        AdapterBase<ConfigurationItem, ConfigurationItemEntity>
    {

        protected override void PerformAdapt(
            ConfigurationItem fromValue,
            ConfigurationItemEntity toValue)
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

        protected override void PerformAdapt(
            ConfigurationItemEntity fromValue,
            ConfigurationItem toValue
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

            toValue.AcceptChanges();

        }
    }
}
