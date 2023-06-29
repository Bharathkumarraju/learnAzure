using System;
using System.Collections.Generic;
using System.Linq;
using Benday.Common;

namespace Benday.YamlDemoApp.Api.Adapters
{
    public abstract class AdapterBase<TType1, TType2>
        where TType1 : class, IInt32Identity, new()
        where TType2 : class, IInt32Identity, IDeleteable, new()
    {
        public virtual void Adapt(
            TType1 fromValue,
            TType2 toValue)
        {
            var action = BeforeAdapt(fromValue, toValue);

            if (action == AdapterActions.Adapt)
            {
                PerformAdapt(fromValue, toValue);
                AfterAdapt(fromValue, toValue);
            }
        }
        public virtual void Adapt(
            TType2 fromValue,
            TType1 toValue)
        {
            var action = BeforeAdapt(fromValue, toValue);

            if (action == AdapterActions.Adapt)
            {
                PerformAdapt(fromValue, toValue);
                AfterAdapt(fromValue, toValue);
            }
        }

        public virtual void Adapt(
            IList<TType1> fromValues,
            IList<TType2> toValues)
        {
            BeforeAdapt(fromValues, toValues);
            PerformAdapt(fromValues, toValues);

            ProcessDeletes(fromValues, toValues);

            AfterAdapt(fromValues, toValues);
        }

        public virtual void Adapt(
            IList<TType2> fromValues,
            IList<TType1> toValues)
        {
            BeforeAdapt(fromValues, toValues);
            PerformAdapt(fromValues, toValues);
            AfterAdapt(fromValues, toValues);
        }

        protected virtual void ProcessDeletes(
            IList<TType1> fromValues, IList<TType2> toValues)
        {
            if (fromValues.Count == toValues.Count)
            {
                return;
            }

            var fromValuesById = new Dictionary<int, TType1>();

            foreach (var item in fromValues)
            {
                if (item.Id > 0 && fromValuesById.ContainsKey(item.Id) == false)
                {
                    fromValuesById.Add(item.Id, item);
                }
            }

            var toValuesById = new Dictionary<int, IDeleteable>();

            foreach (var item in toValues)
            {
                if (item.Id > 0 && toValuesById.ContainsKey(item.Id) == false)
                {
                    toValuesById.Add(item.Id, item);
                }
            }

            if (fromValuesById.Count == toValuesById.Count)
            {
                return;
            }
            else
            {
                foreach (var key in toValuesById.Keys)
                {
                    if (fromValuesById.ContainsKey(key) == false)
                    {
                        toValuesById[key].IsMarkedForDelete = true;
                    }
                }
            }
        }

        protected virtual AdapterActions BeforeAdapt(
            TType1 fromValue,
            TType2 toValue)
        {
            return AdapterActions.Adapt;
        }

        protected virtual AdapterActions BeforeAdapt(
            TType2 fromValue,
            TType1 toValue)
        {
            return AdapterActions.Adapt;
        }

        protected virtual void BeforeAdapt(
            IList<TType1> fromValues,
            IList<TType2> toValues)
        {

        }

        protected virtual void BeforeAdapt(
            IList<TType2> fromValues,
            IList<TType1> toValues)
        {

        }

        protected virtual void AfterAdapt(
            TType1 fromValue,
            TType2 toValue)
        {

        }
        protected virtual void AfterAdapt(
            TType2 fromValue,
            TType1 toValue)
        {

        }
        protected virtual void AfterAdapt(
            IList<TType1> fromValues,
            IList<TType2> toValues)
        {

        }
        protected virtual void AfterAdapt(
            IList<TType2> fromValues,
            IList<TType1> toValues)
        {

        }

        protected abstract void PerformAdapt(
            TType1 fromValue,
            TType2 toValue);
        protected abstract void PerformAdapt(
        TType2 fromValue,
        TType1 toValue);

        protected void PerformAdapt(
        IList<TType1> fromValues,
        IList<TType2> toValues)
        {
            if (fromValues == null)
            {
                throw new ArgumentNullException(nameof(fromValues));
            }

            if (toValues == null)
            {
                throw new ArgumentNullException(nameof(toValues));
            }

            TType2 toValue;
            bool add;

            foreach (var fromValue in fromValues)
            {
                add = false;

                if (fromValue.Id == ApiConstants.UnsavedId)
                {
                    toValue = new TType2();

                    add = true;
                }
                else
                {
                    toValue = FindById(toValues, fromValue.Id);

                    if (toValue == null)
                    {
                        toValue = new TType2();

                        add = true;
                    }
                }

                var action = BeforeAdapt(fromValue, toValue);
                if (action == AdapterActions.Adapt)
                {
                    PerformAdapt(fromValue, toValue);
                    AfterAdapt(fromValue, toValue);

                    if (add == true)
                    {
                        toValues.Add(toValue);
                    }
                }
                else if (action == AdapterActions.Delete)
                {
                    toValues.Remove(toValue);
                    AfterAdapt(fromValue, toValue);
                }
            }
        }

        protected void PerformAdapt(
            IList<TType2> fromValues,
            IList<TType1> toValues)
        {
            if (fromValues == null)
            {
                throw new ArgumentNullException(nameof(fromValues));
            }

            if (toValues == null)
            {
                throw new ArgumentNullException(nameof(toValues));
            }

            TType1 toValue;
            bool add;

            foreach (var fromValue in fromValues)
            {
                add = false;

                if (fromValue.Id == ApiConstants.UnsavedId)
                {
                    toValue = new TType1();

                    add = true;
                }
                else
                {
                    toValue = FindById(toValues, fromValue.Id);

                    if (toValue == null)
                    {
                        toValue = new TType1();

                        add = true;
                    }
                }

                var action = BeforeAdapt(fromValue, toValue);
                if (action == AdapterActions.Adapt)
                {
                    PerformAdapt(fromValue, toValue);
                    AfterAdapt(fromValue, toValue);

                    if (add == true)
                    {
                        toValues.Add(toValue);
                    }
                }
                else if (action == AdapterActions.Delete)
                {
                    toValues.Remove(toValue);
                    AfterAdapt(fromValue, toValue);
                }
            }
        }

        protected virtual TType1 FindById
        (IList<TType1> values, int id)
        {
            var returnValue = (from temp in values
                               where temp.Id == id
                               select temp).FirstOrDefault();

            return returnValue;
        }

        protected virtual TType2 FindById
        (IList<TType2> values, int id)
        {
            var returnValue = (from temp in values
                               where temp.Id == id
                               select temp).FirstOrDefault();

            return returnValue;
        }
    }
}
