using System;
using System.Collections.Generic;
using Benday.YamlDemoApp.Api.DataAccess.Entities;
using Benday.YamlDemoApp.Api.DomainModels;

namespace Benday.YamlDemoApp.Api.ServiceLayers
{
    public abstract class CoreFieldsServiceLayerBase<T> where T : CoreFieldsDomainModelBase
    {
        protected IUsernameProvider _usernameProvider;

        public CoreFieldsServiceLayerBase(
            IUsernameProvider usernameProvider)
        {
            _usernameProvider = usernameProvider;
        }

        protected virtual void BeforeReturnFromGet(T returnValue)
        {

        }

        protected virtual void BeforeReturnFromGet(IList<T> returnValues)
        {

        }

        protected void PopulateAuditFieldsBeforeSave(T toValue)
        {
            PopulateAuditFieldsBeforeSave(toValue as CoreFieldsDomainModelBase);
            OnPopulateAuditFieldsBeforeSave(toValue);
        }

        protected virtual void OnPopulateAuditFieldsBeforeSave(T toValue)
        {
        }

        protected virtual void PopulateAuditFieldsBeforeSave(
            List<CoreFieldsDomainModelBase> values)
        {
            if (values != null)
            {
                foreach (var item in values)
                {
                    PopulateAuditFieldsBeforeSave(item);
                }
            }
        }

        protected virtual void PopulateAuditFieldsBeforeSave(CoreFieldsDomainModelBase toValue)
        {
            if (toValue == null)
            {
                throw new ArgumentNullException(nameof(toValue), $"{nameof(toValue)} is null.");
            }

            if (toValue.Id == 0)
            {
                toValue.CreatedBy = _usernameProvider.Username;
                toValue.CreatedDate = DateTime.UtcNow;
            }

            if (toValue.HasChanges() == true)
            {
                toValue.LastModifiedBy = _usernameProvider.Username;
                toValue.LastModifiedDate = DateTime.UtcNow;
            }
        }

        protected virtual void PopulateFieldsFromEntityAfterSave(
            List<CoreFieldsEntityBase> fromValues, List<CoreFieldsDomainModelBase> toValues)
        {
            if (fromValues == null)
            {
                throw new ArgumentNullException(nameof(fromValues));
            }

            if (toValues == null)
            {
                throw new ArgumentNullException(nameof(toValues));
            }

            if (fromValues.Count != toValues.Count)
            {
                throw new InvalidOperationException("Item count in collection doesn't match.");
            }

            for (var index = 0; index < fromValues.Count; index++)
            {
                PopulateFieldsFromEntityAfterSave(
                fromValues[index],
                toValues[index]);
            }
        }

        protected virtual void PopulateFieldsFromEntityAfterSave(
            CoreFieldsEntityBase fromValue, CoreFieldsDomainModelBase toValue)
        {
            if (fromValue == null)
            {
                throw new ArgumentNullException(nameof(fromValue), $"{nameof(fromValue)} is null.");
            }

            if (toValue == null)
            {
                throw new ArgumentNullException(nameof(toValue), $"{nameof(toValue)} is null.");
            }

            toValue.Id = fromValue.Id;
            toValue.LastModifiedDate = fromValue.LastModifiedDate;
            toValue.LastModifiedBy = fromValue.LastModifiedBy;

            toValue.CreatedDate = fromValue.CreatedDate;
            toValue.CreatedBy = fromValue.CreatedBy;
        }
    }
}
