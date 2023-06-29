using System;
using System.Collections.Generic;
using Benday.YamlDemoApp.Api.DataAccess.Entities;
using Benday.YamlDemoApp.Api.DomainModels;

namespace Benday.YamlDemoApp.Api.ServiceLayers
{
    public abstract class ServiceLayerBase<T> where T : DomainModelBase
    {
        protected IUsernameProvider _usernameProvider;

        public ServiceLayerBase(
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

        protected virtual void PopulateAuditFieldsBeforeSave(T toValue)
        {
            OnPopulateAuditFieldsBeforeSave(toValue);
        }

        protected virtual void OnPopulateAuditFieldsBeforeSave(T toValue)
        {
        }

        protected virtual void PopulateAuditFieldsBeforeSave(DomainModelBase toValue)
        {
        }

        protected virtual void PopulateFieldsFromEntityAfterSave(
            List<EntityBase> fromValues, List<DomainModelBase> toValues)
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
            EntityBase fromValue, DomainModelBase toValue)
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
        }
    }
}
