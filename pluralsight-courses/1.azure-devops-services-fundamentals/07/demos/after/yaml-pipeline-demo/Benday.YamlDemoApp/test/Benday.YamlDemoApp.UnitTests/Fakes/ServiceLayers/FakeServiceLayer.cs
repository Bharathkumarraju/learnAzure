using System.Collections.Generic;
using Benday.Common;
using Benday.YamlDemoApp.Api.ServiceLayers;

namespace Benday.YamlDemoApp.UnitTests.Fakes.ServiceLayers
{
    public class FakeServiceLayer<T> : IServiceLayer<T> where T : IInt32Identity
    {
        public FakeServiceLayer()
        {
            OnSaveUpdateId = false;
            OnSaveUpdateIdToThisValue = 9999;
        }

        public int DeleteByIdArgumentValue { get; set; }
        public bool WasDeleteByIdCalled { get; set; }

        public void DeleteById(int id)
        {
            WasDeleteByIdCalled = true;

            DeleteByIdArgumentValue = id;
        }

        public IList<T> GetAllReturnValue { get; set; }
        public bool WasGetAllCalled { get; set; }

        public IList<T> GetAll(int maxNumberOfResults = 100)
        {
            WasGetAllCalled = true;

            return GetAllReturnValue;
        }

        public T GetByIdReturnValue { get; set; }
        public bool WasGetByIdCalled { get; set; }

        public T GetById(int id)
        {
            WasGetByIdCalled = true;

            return GetByIdReturnValue;
        }

        public T SaveArgumentValue { get; set; }
        public bool WasSaveCalled { get; set; }
        public bool OnSaveUpdateId { get; set; }
        public int OnSaveUpdateIdToThisValue { get; set; }

        public void Save(T saveThis)
        {
            WasSaveCalled = true;

            SaveArgumentValue = saveThis;

            if (OnSaveUpdateId == true)
            {
                saveThis.Id = OnSaveUpdateIdToThisValue;
            }
        }

        public IList<T> SearchReturnValue { get; set; }
        public bool WasSearchCalled { get; set; }

        public IList<T> Search(Search search)
        {
            WasSearchCalled = true;

            return SearchReturnValue;
        }

        public IList<T> SearchUsingSimpleSearchReturnValue { get; set; }
        public bool WasSearchUsingSimpleSearchCalled { get; set; }

        public IList<T> Search(
            string searchValue, int maxNumberOfResults = 100)
        {
            WasSearchUsingSimpleSearchCalled = true;

            return SearchUsingSimpleSearchReturnValue;
        }
    }
}
