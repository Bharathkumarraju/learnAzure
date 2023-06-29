using System.Threading.Tasks;
using Benday.YamlDemoApp.Api.Security;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Benday.YamlDemoApp.UnitTests.Security
{
    [TestClass]
    public class ClaimAuthorizationHandlerFixture
    {
        private const string TEST_CLAIM_NAME = "ClaimName123";

        [TestMethod]
        public async Task HandlerSucceedsWhenUserIsAuthorized_DirectPermissionViaClaim()
        {
            // arrange
            var tester = new ClaimAuthorizationHandlerTester();

            tester.AddRequirementPermission(TEST_CLAIM_NAME);
            tester.AddClaim(TEST_CLAIM_NAME, "123");
            tester.SetRouteDataValue("123");

            // act
            // assert
            await tester.AssertSuccess();
        }

        [TestMethod]
        public async Task HandlerFailsWhenUserIsNotAuthorized_NoClaims()
        {
            // arrange
            var tester = new ClaimAuthorizationHandlerTester();

            tester.AddRequirementPermission(TEST_CLAIM_NAME);
            // tester.AddClaim(TEST_CLAIM_NAME, "123");
            tester.SetRouteDataValue("123");

            // act
            // assert
            await tester.AssertFailure();
        }

        [TestMethod]
        public async Task HandlerFailsWhenUserIsNotAuthorized_DoesNotHaveAppropriateClaims()
        {
            // arrange
            var tester = new ClaimAuthorizationHandlerTester();

            tester.AddRequirementPermission(TEST_CLAIM_NAME);
            tester.AddClaim(TEST_CLAIM_NAME, "456");
            tester.SetRouteDataValue("123");

            // act
            // assert
            await tester.AssertFailure();
        }

        [TestMethod]
        public async Task HandlerFailsWhenUserIsNotAuthorized_NotDirectlyAndNotInRequiredRole()
        {
            // arrange
            var tester = new ClaimAuthorizationHandlerTester();

            tester.AddRequirementPermission(TEST_CLAIM_NAME);
            tester.AddRequirementRole(SecurityConstants.RoleName_Admin);
            tester.AddClaim(TEST_CLAIM_NAME, "456");
            tester.AddClaimRole("useless-role");

            tester.SetRouteDataValue("123");

            // act
            // assert
            await tester.AssertFailure();
        }

        [TestMethod]
        public async Task HandlerSucceedsWhenUserIsAuthorized_ViaRequiredRole()
        {
            // arrange
            var tester = new ClaimAuthorizationHandlerTester();

            tester.AddRequirementPermission(TEST_CLAIM_NAME);
            tester.AddRequirementRole(SecurityConstants.RoleName_Admin);
            tester.AddClaim(TEST_CLAIM_NAME, "456");
            tester.AddClaimRole(SecurityConstants.RoleName_Admin);

            tester.SetRouteDataValue("123");

            // act
            // assert
            await tester.AssertSuccess();
        }

        [TestMethod]
        public async Task HandlerFailsIfRouteDataValueIsNotPresent()
        {
            // arrange
            var tester = new ClaimAuthorizationHandlerTester();

            tester.AddRequirementPermission(TEST_CLAIM_NAME);
            tester.AddRequirementRole(SecurityConstants.RoleName_Admin);
            tester.AddClaim(TEST_CLAIM_NAME, "456");
            tester.AddClaimRole(SecurityConstants.RoleName_Admin);

            // tester.SetRouteDataValue("123");

            // act
            // assert
            await tester.AssertFailure();
        }
    }
}
