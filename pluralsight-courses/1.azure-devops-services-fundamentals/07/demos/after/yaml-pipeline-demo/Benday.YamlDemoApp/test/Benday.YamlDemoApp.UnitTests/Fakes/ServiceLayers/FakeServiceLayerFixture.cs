using System.Collections.Generic;
using Benday.YamlDemoApp.Api.DomainModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Benday.YamlDemoApp.UnitTests.Fakes.ServiceLayers
{
    [TestClass]
    public class FakeServiceLayerFixture
    {
        public class TestDomainClass : DomainModelBase
        {

        }

        [TestInitialize]
        public void OnTestInitialize()
        {
            _systemUnderTest = null;
        }

        private FakeServiceLayer<TestDomainClass> _systemUnderTest;
        public FakeServiceLayer<TestDomainClass> SystemUnderTest
        {
            get
            {
                if (_systemUnderTest == null)
                {
                    _systemUnderTest = new FakeServiceLayer<TestDomainClass>();
                }

                return _systemUnderTest;
            }
        }

        [TestMethod]
        public void FakeServiceLayer_GetById()
        {
            // arrange
            var expected = new TestDomainClass();
            SystemUnderTest.GetByIdReturnValue = expected;

            // act
            var actual = SystemUnderTest.GetById(1234);

            // assert
            Assert.AreEqual<TestDomainClass>(expected, actual, "Wrong value");
            Assert.IsTrue(SystemUnderTest.WasGetByIdCalled, "Method wasn't called");
        }

        [TestMethod]
        public void FakeServiceLayer_GetAll()
        {
            // arrange
            var expected = new List<TestDomainClass>();
            SystemUnderTest.GetAllReturnValue = expected;

            // act
            var actual = SystemUnderTest.GetAll();

            // assert
            Assert.AreSame(expected, actual, "Wrong return value");
            Assert.IsTrue(SystemUnderTest.WasGetAllCalled, "Method wasn't called");
        }

        [TestMethod]
        public void FakeServiceLayer_Save()
        {
            // arrange
            var saveThis = new TestDomainClass();

            // act
            SystemUnderTest.Save(saveThis);

            // assert
            Assert.AreEqual<TestDomainClass>(
            saveThis,
            SystemUnderTest.SaveArgumentValue,
            "Save argument value was wrong.");

            Assert.IsTrue(SystemUnderTest.WasSaveCalled, "Method wasn't called");
        }

        [TestMethod]
        public void FakeServiceLayer_DeleteById()
        {
            // arrange
            var deleteThisId = 1234;

            // act
            SystemUnderTest.DeleteById(deleteThisId);

            // assert
            Assert.AreEqual<int>(deleteThisId, SystemUnderTest.DeleteByIdArgumentValue, "Delete argument value was wrong.");
            Assert.IsTrue(SystemUnderTest.WasDeleteByIdCalled, "Method wasn't called");
        }
    }
}
