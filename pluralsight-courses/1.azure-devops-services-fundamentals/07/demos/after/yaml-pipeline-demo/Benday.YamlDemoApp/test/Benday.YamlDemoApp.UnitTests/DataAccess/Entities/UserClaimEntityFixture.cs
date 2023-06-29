using Benday.YamlDemoApp.Api.DataAccess.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Benday.YamlDemoApp.UnitTests.DataAccess.Entities
{
    [TestClass]
    public class UserClaimEntityFixture
    {
        [TestInitialize]
        public void OnTestInitialize()
        {
            _systemUnderTest = null;
        }

        private UserClaimEntity _systemUnderTest;
        public UserClaimEntity SystemUnderTest
        {
            get
            {
                if (_systemUnderTest == null)
                {
                    _systemUnderTest = new UserClaimEntity();
                }

                return _systemUnderTest;
            }
        }


    }
}
