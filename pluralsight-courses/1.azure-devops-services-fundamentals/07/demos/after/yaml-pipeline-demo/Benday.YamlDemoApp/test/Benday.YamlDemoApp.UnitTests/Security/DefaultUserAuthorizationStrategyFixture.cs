using Benday.YamlDemoApp.WebUi.Security;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Benday.YamlDemoApp.UnitTests.Security
{
    [TestClass]
    public class DefaultUserAuthorizationStrategyFixture
    {
        [TestInitialize]
        public void OnTestInitialize()
        {
            _systemUnderTest = null;
            _principalProvider = null;
        }

        private DefaultUserAuthorizationStrategy _systemUnderTest;
        public DefaultUserAuthorizationStrategy SystemUnderTest
        {
            get
            {
                if (_systemUnderTest == null)
                {
                    _systemUnderTest =
                    new DefaultUserAuthorizationStrategy(
                    PrincipalProvider);
                }

                return _systemUnderTest;
            }
        }

        private MockUserClaimsPrincipalProvider _principalProvider;
        public MockUserClaimsPrincipalProvider PrincipalProvider
        {
            get
            {
                if (_principalProvider == null)
                {
                    _principalProvider = new MockUserClaimsPrincipalProvider();
                }

                return _principalProvider;
            }
        }

        /*
        // SAMPLE
        [TestMethod]
        public void IsAuthorizedForImages_ReturnsFalseForNoClaims()
        {
            Assert.IsFalse(SystemUnderTest.IsAuthorizedForImages,
            "Should not be authorized for search.");
        }
        
        [TestMethod]
        public void IsAuthorizedForImages_ReturnsTrueForAdministrator()
        {
            PrincipalProvider.AddClaim(
            ClaimTypes.Role,
            SecurityConstants.RoleName_Admin);
            
            Assert.IsTrue(SystemUnderTest.IsAuthorizedForImages,
            "Should be authorized for search.");
        }
        
        [TestMethod]
        public void IsAuthorizedForImages_ReturnsTrueForImageViewerRole()
        {
            
            PrincipalProvider.AddClaim(
            ClaimTypes.Role,
            TEST_CLAIM_NAME);
            
            Assert.IsTrue(SystemUnderTest.IsAuthorizedForImages,
            "Should be authorized for search.");
        }
        */
    }
}
