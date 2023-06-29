using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Benday.YamlDemoApp.Api;
using Benday.YamlDemoApp.Api.DomainModels;
using Benday.YamlDemoApp.Api.Security;
using Benday.YamlDemoApp.UnitTests.Fakes.ServiceLayers;
using Benday.YamlDemoApp.UnitTests.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Benday.YamlDemoApp.UnitTests.Security
{
    [TestClass]
    public class PopulateClaimsMiddlewareFixture
    {
        [TestInitialize]
        public void OnTestInitialize()
        {
            _SystemUnderTest = null;
            _UserServiceInstance = null;
            _SecurityConfig = null;
        }

        private PopulateClaimsMiddleware _SystemUnderTest;
        private FakeUserService _UserServiceInstance;
        private ISecurityConfiguration _SecurityConfig;

        private PopulateClaimsMiddleware SystemUnderTest
        {
            get
            {
                if (_SystemUnderTest == null)
                {
                    _SystemUnderTest = new PopulateClaimsMiddleware(
                    SecurityConfigurationInstance,
                    UserServiceInstance);
                }

                return _SystemUnderTest;
            }
        }

        public FakeUserService UserServiceInstance
        {
            get
            {
                if (_UserServiceInstance == null)
                {
                    _UserServiceInstance = new FakeUserService();
                }

                return _UserServiceInstance;
            }
        }

        public ISecurityConfiguration SecurityConfigurationInstance
        {
            get
            {
                if (_SecurityConfig == null)
                {
                    _SecurityConfig = new FakeSecurityConfiguration();
                }

                return _SecurityConfig;
            }
        }

        private DefaultHttpContext GetHttpContextForAuthenticatedUser()
        {
            var httpContext = new DefaultHttpContext();

            httpContext.Request.Headers.Add(
            SecurityConstants.Claim_X_MsClientPrincipalIdp,
            SecurityConstants.Idp_DevelopmentMode);

            httpContext.Request.Headers.Add(
            SecurityConstants.Claim_X_MsClientPrincipalId,
            "user@user.com");

            httpContext.Request.Headers.Add(
            SecurityConstants.Claim_X_MsClientPrincipalName,
            "user@user.com");

            var claims = new List<Claim>();

            claims.Add(new Claim(
            ClaimTypes.Name, "test user"));

            var identity = new ClaimsIdentity(claims, "Testing");

            httpContext.User = new ClaimsPrincipal(identity);

            return httpContext;
        }

        private DefaultHttpContext GetHttpContextForAnonymousUser()
        {
            // arrange
            var httpContext = new DefaultHttpContext();

            httpContext.User = new ClaimsPrincipal();
            return httpContext;
        }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        private async Task DoNothing(HttpContext context)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {

        }

        private RequestDelegate GetDoNothingNextDelegate()
        {
            return new RequestDelegate(DoNothing);
        }

        [TestMethod]
        public async Task NotAuthenticated_ClaimsCountIsZero()
        {
            // arrange
            UserServiceInstance.GetByUsernameReturnValue = null;

            var httpContext = GetHttpContextForAnonymousUser();

            // act
            await SystemUnderTest.InvokeAsync(httpContext, GetDoNothingNextDelegate());

            // assert
            Assert.AreEqual<int>(0, httpContext.User.Claims.Count(), "Claim count should be zero.");
            Assert.IsFalse(UserServiceInstance.WasGetByUsernameCalled, "GetByUsername should not be called");
        }

        [TestMethod]
        public async Task Authenticated_KnownUser_WithClaims_AddUserClaims()
        {
            // arrange
            var httpContext = GetHttpContextForAuthenticatedUser();
            var user = GetUser(true);
            UserServiceInstance.GetByUsernameReturnValue = user;

            // act
            await SystemUnderTest.InvokeAsync(httpContext, GetDoNothingNextDelegate());

            // assert
            Assert.AreNotEqual<int>(0, httpContext.User.Claims.Count(), "Claim count should not be zero.");

            Assert.IsTrue(UserServiceInstance.WasGetByUsernameCalled, "GetByUsername should be called");

            AssertClaim(
            httpContext.User.Claims.ToList(),
            ApiConstants.ClaimName_UserId, user.Id.ToString());

            foreach (var item in user.Claims)
            {
                AssertClaim(
                httpContext.User.Claims.ToList(),
                item
                );
            }
        }

        [TestMethod]
        public async Task Authenticated_KnownUser_WithTemporalClaims_OnlyAddActiveTimedClaims()
        {
            // arrange
            var httpContext = GetHttpContextForAuthenticatedUser();
            var user = GetUserWithTemporalClaims();
            UserServiceInstance.GetByUsernameReturnValue = user;

            // act
            await SystemUnderTest.InvokeAsync(httpContext, GetDoNothingNextDelegate());

            // assert
            Assert.AreNotEqual<int>(0, httpContext.User.Claims.Count(), "Claim count should not be zero.");

            Assert.IsTrue(UserServiceInstance.WasGetByUsernameCalled, "GetByUsername should be called");
            AssertClaim(
            httpContext.User.Claims.ToList(),
            ApiConstants.ClaimName_UserId, user.Id.ToString());

            AssertClaim(httpContext.User.Claims.ToList(),
            "REGULAR", "REGULAR VALUE");
            AssertClaim(httpContext.User.Claims.ToList(),
            "VALID_TODAY", "VALID_TODAY VALUE");

            AssertClaimDoesNotExist(httpContext.User.Claims.ToList(),
            "INVALID_BEFORE");
            AssertClaimDoesNotExist(httpContext.User.Claims.ToList(),
            "INVALID_AFTER");
        }

        [TestMethod]
        public async Task Authenticated_KnownUser_WithNoClaims()
        {
            // arrange
            var httpContext = GetHttpContextForAuthenticatedUser();
            var user = GetUser(false);
            UserServiceInstance.GetByUsernameReturnValue = user;

            // act
            await SystemUnderTest.InvokeAsync(httpContext, GetDoNothingNextDelegate());

            // assert
            DebugClaims(httpContext.User.Claims);
            Assert.AreEqual<int>(6, httpContext.User.Claims.Count(), "Claim count was wrong.");

            Assert.IsTrue(UserServiceInstance.WasGetByUsernameCalled, "GetByUsername should be called");

            AssertClaim(
            httpContext.User.Claims.ToList(),
            ApiConstants.ClaimName_UserId, user.Id.ToString());
        }

        private static void DebugClaims(IEnumerable<Claim> claims)
        {
            if (claims == null || claims.Count() == 0)
            {
                Console.WriteLine($"DebugClaims(): null or 0 claims");
            }
            else
            {
                foreach (var claim in claims)
                {
                    Console.WriteLine($"DebugClaims(): {claim.Type} - {claim.Value}");
                }
            }
        }

        private User GetUser(bool withClaims)
        {
            var user = UserTestUtility.CreateModel(false);

            if (withClaims == false)
            {
                user.Claims = new List<UserClaim>();
            }
            else
            {
                var claims = UserClaimTestUtility.CreateModels(false);

                user.Claims = claims;
            }

            return user;
        }

        private User GetUserWithTemporalClaims()
        {
            var user = UserTestUtility.CreateModel(false);

            var claims = new List<UserClaim>();

            var dateTimeNowUtc = DateTime.UtcNow;
            var yesterday = dateTimeNowUtc.AddDays(-1);
            var tomorrow = dateTimeNowUtc.AddDays(1);

            claims.Add(CreateClaim("REGULAR"));

            claims.Add(CreateClaim(
            "INVALID_BEFORE",
            default(DateTime), yesterday));

            claims.Add(CreateClaim(
            "INVALID_AFTER",
            tomorrow, default(DateTime)));

            claims.Add(CreateClaim(
            "VALID_TODAY",
            yesterday, tomorrow));

            user.Claims = claims;

            return user;
        }

        [TestMethod]
        public void UserClaim_CreateTemporalClaim()
        {
            // arrange
            var expectedName = "BINGBONG";
            var expectedValue = "BINGBONG VALUE";
            var expectedStartTime = new DateTime(2000, 1, 1);
            var expectedEndTime = new DateTime(2100, 1, 1);
            var expectedClaimLogicType = ApiConstants.ClaimLogicType_DateTimeBased;

            // act
            var actual = CreateClaim(
            expectedName, expectedStartTime, expectedEndTime);

            // assert
            Assert.AreEqual<string>(
            expectedClaimLogicType, actual.ClaimLogicType,
            "ClaimLogicType was wrong.");
            Assert.AreEqual<string>(
            expectedName, actual.ClaimName,
            "ClaimName was wrong.");
            Assert.AreEqual<string>(
            expectedValue, actual.ClaimValue,
            "ClaimValue was wrong.");
            Assert.AreEqual<DateTime>(
            expectedStartTime, actual.StartDate,
            "StartDate was wrong.");
            Assert.AreEqual<DateTime>(
            expectedEndTime, actual.EndDate,
            "EndDate was wrong.");
        }

        [TestMethod]
        public void UserClaim_CreateRegularClaim()
        {
            // arrange
            var expectedName = "BINGBONG";
            var expectedValue = "BINGBONG VALUE";
            var expectedStartTime = default(DateTime);
            var expectedEndTime = default(DateTime);
            var expectedClaimLogicType = ApiConstants.ClaimLogicType_Default;

            // act
            var actual = CreateClaim(
            expectedName);

            // assert
            Assert.AreEqual<string>(
            expectedClaimLogicType, actual.ClaimLogicType,
            "ClaimLogicType was wrong.");
            Assert.AreEqual<string>(
            expectedName, actual.ClaimName,
            "ClaimName was wrong.");
            Assert.AreEqual<string>(
            expectedValue, actual.ClaimValue,
            "ClaimValue was wrong.");
            Assert.AreEqual<DateTime>(
            expectedStartTime, actual.StartDate,
            "StartDate was wrong.");
            Assert.AreEqual<DateTime>(
            expectedEndTime, actual.EndDate,
            "EndDate was wrong.");
        }

        private static UserClaim CreateClaim(string name, DateTime start, DateTime end)
        {
            var claim = CreateClaim(name);

            claim.ClaimLogicType = ApiConstants.ClaimLogicType_DateTimeBased;
            claim.StartDate = start;
            claim.EndDate = end;

            return claim;
        }

        private static UserClaim CreateClaim(string name)
        {
            var claim = new UserClaim
            {
                ClaimLogicType = ApiConstants.ClaimLogicType_Default,
                ClaimName = name,
                ClaimValue = $"{name} VALUE",
                Status = ApiConstants.StatusActive
            };

            return claim;
        }

        [TestMethod]
        public async Task Authenticated_UnknownUser_CreateUserInDatabase_AddBasicClaims()
        {
            // arrange
            var httpContext = GetHttpContextForAuthenticatedUser();
            UserServiceInstance.GetByUsernameReturnValue = null;

            // act
            await SystemUnderTest.InvokeAsync(httpContext, GetDoNothingNextDelegate());

            // assert
            Assert.IsTrue(UserServiceInstance.WasGetByUsernameCalled, "GetByUsername should be called");
            Assert.IsTrue(UserServiceInstance.WasSaveCalled, "Save should be called");

            var savedUser = UserServiceInstance.SaveArgumentValue;

            DebugClaims(httpContext.User.Claims);
            Assert.AreEqual<int>(6, httpContext.User.Claims.Count(), "Claim count should be zero.");

            AssertClaim(
            httpContext.User.Claims.ToList(),
            ApiConstants.ClaimName_UserId, savedUser.Id.ToString());
        }

        private static void AssertClaimDoesNotExist(List<Claim> actualClaims,
            string expectedClaimName)
        {
            var actual = actualClaims.Where(
            x => x.Type == expectedClaimName).FirstOrDefault();

            Assert.IsNull(actual, $"Should not find claim '{expectedClaimName}'");
        }

        private static void AssertClaim(List<Claim> actualClaims,
            string expectedClaimName, string expectedClaimValue)
        {
            var actual = actualClaims.Where(
            x => x.Type == expectedClaimName).FirstOrDefault();

            Assert.IsNotNull(actual, $"Could not find claim '{expectedClaimName}'");

            Assert.AreEqual<string>(expectedClaimValue, actual.Value,
            $"Claim '{expectedClaimName}' value was wrong.");
        }

        private void AssertClaim(List<Claim> actualClaims,
            UserClaim expected)
        {
            AssertClaim(actualClaims, expected.ClaimName, expected.ClaimValue);
        }
    }
}
