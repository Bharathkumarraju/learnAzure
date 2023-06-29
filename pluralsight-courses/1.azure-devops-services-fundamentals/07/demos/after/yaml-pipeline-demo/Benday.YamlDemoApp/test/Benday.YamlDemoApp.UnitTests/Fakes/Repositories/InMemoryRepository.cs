using System;
using System.Collections.Generic;
using System.Linq;
using Benday.Common;
using Benday.EfCore.SqlServer;
using Benday.YamlDemoApp.Api;
using Benday.YamlDemoApp.Api.DataAccess.Entities;

namespace Benday.YamlDemoApp.UnitTests.Fakes.Repositories
{
    public class InMemoryRepository<T> : ISearchableRepository<T> where T : IInt32Identity
    {
        private int _currentIdentityValue = 0;

        public InMemoryRepository()
        {
            Items = new List<T>();
        }

        public List<T> Items
        {
            get;
            set;
        }

        public bool WasGetAllCalled { get; private set; }

        public IList<T> GetAll()
        {
            WasGetAllCalled = true;

            return Items;
        }

        public IList<T> GetAll(int maxNumberOfResults, bool noIncludes)
        {
            WasGetAllCalled = true;

            return Items;
        }

        public bool WasGetByIdCalled { get; private set; }


        public T GetById(int id)
        {
            WasGetByIdCalled = true;

            return (from temp in Items
                    where temp.Id == id
                    select temp).FirstOrDefault();
        }

        public bool WasSaveCalled { get; private set; }

        public void Save(T saveThis)
        {
            WasSaveCalled = true;

            if (saveThis == null)
            {
                throw new ArgumentNullException(nameof(saveThis), "Argument cannot be null.");
            }

            if (saveThis.Id == 0)
            {
                // assign new identity value
                saveThis.Id = GetNextIdValue();
            }

            if (Items.Contains(saveThis) == false)
            {
                Items.Add(saveThis);
            }

            OnSave(saveThis);

            SaveAttributes(saveThis);
        }

        protected virtual void OnSave(T saveThis)
        {

        }

        private void SaveAttributes(T saveThis)
        {
            if (saveThis is IAttributedEntity saveThisAsAttributed)
            {
                foreach (var item in saveThisAsAttributed.GetAttributes())
                {
                    AttributeRepository.Save(item);
                }
            }
        }

        private InMemoryRepository<EntityBase> _attributeRepository;

        public InMemoryRepository<EntityBase> AttributeRepository
        {
            get
            {
                if (_attributeRepository == null)
                {
                    _attributeRepository = new InMemoryRepository<EntityBase>();
                }

                return _attributeRepository;
            }
        }

        public bool WasDeleteByIdCalled { get; private set; }

        public void Delete(T deleteThis)
        {
            WasDeleteByIdCalled = true;

            if (deleteThis == null)
            {
                throw new ArgumentNullException(nameof(deleteThis), "Argument cannot be null.");
            }

            if (Items.Contains(deleteThis) == true)
            {
                Items.Remove(deleteThis);
            }
        }

        protected int GetNextIdValue()
        {
            return ++_currentIdentityValue;
        }

        public void ResetMethodCallTrackers()
        {
            WasDeleteByIdCalled = false;
            WasGetAllCalled = false;
            WasGetByIdCalled = false;
            WasSaveCalled = false;
        }

        public SearchResult<T> Search(Search search)
        {
            throw new NotImplementedException();
        }
    }
}
