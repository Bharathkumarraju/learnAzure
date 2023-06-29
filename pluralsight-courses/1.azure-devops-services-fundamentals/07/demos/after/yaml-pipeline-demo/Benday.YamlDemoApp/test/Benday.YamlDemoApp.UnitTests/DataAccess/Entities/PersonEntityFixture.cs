using Benday.YamlDemoApp.Api.DataAccess.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Benday.YamlDemoApp.UnitTests.DataAccess.Entities
{
    [TestClass]
    public class PersonEntityFixture
    {
        [TestInitialize]
        public void OnTestInitialize()
        {
            _systemUnderTest = null;
        }

        private PersonEntity _systemUnderTest;
        public PersonEntity SystemUnderTest
        {
            get
            {
                if (_systemUnderTest == null)
                {
                    _systemUnderTest = new PersonEntity();
                }

                return _systemUnderTest;
            }
        }


    }
}
