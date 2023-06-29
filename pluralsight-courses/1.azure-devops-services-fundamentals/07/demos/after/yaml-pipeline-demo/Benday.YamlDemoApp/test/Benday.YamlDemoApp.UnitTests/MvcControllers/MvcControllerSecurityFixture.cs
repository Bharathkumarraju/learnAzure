using System;
using System.Collections.Generic;
using Benday.YamlDemoApp.Api.Security;
using Benday.YamlDemoApp.UnitTests.Utilities;
using Benday.YamlDemoApp.WebUi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Benday.YamlDemoApp.UnitTests.MvcControllers
{
    [TestClass]
    public class MvcControllerSecurityFixture
    {
        private void AssertRequiresAdmin(Type systemUnderTest)
        {
            SecurityAttributeUtility.AssertAuthorizeAttributePolicyOnClass(
            SecurityConstants.Policy_IsAdministrator, systemUnderTest);
        }

        [TestMethod]
        public void ConfigurationItemController_RequiresAdministratorRole()
        {
            AssertRequiresAdmin(typeof(ConfigurationItemController));
        }

        [TestMethod]
        public void LogEntryController_RequiresAdministratorRole()
        {
            AssertRequiresAdmin(typeof(LogEntryController));
        }

        [TestMethod]
        public void ControllersHaveSomeKindOfSecuritySetOrAreWhitelisted()
        {
            var allTypes = typeof(HomeController).Assembly.GetTypes();

            var foundAtLeastOneType = false;

            var whitelist = new List<Type>();

            whitelist.Add(typeof(HomeController));

            foreach (var typeToCheck in allTypes)
            {
                if (typeToCheck.IsSubclassOf(typeof(Controller)) == true &&
                typeToCheck.IsAbstract == false)
                {
                    Console.WriteLine("Checking controller type...{0}", typeToCheck.Name);
                    foundAtLeastOneType = true;

                    if (whitelist.Contains(typeToCheck) == true)
                    {
                        continue;
                    }

                    Console.WriteLine($"Checking security for '{typeToCheck.FullName}'");

                    SecurityAttributeUtility.AssertHasSomeKindOfSecurityDefinedOnClass(
                    typeToCheck);
                }
            }

            Assert.IsTrue(foundAtLeastOneType, "Found at least one controller type.");
        }

        [TestMethod]
        public void LookupController_RequiresAdministratorRole()
        {
            AssertRequiresAdmin(typeof(LookupController));
        }

        [TestMethod]
        public void UserClaimController_RequiresAdministratorRole()
        {
            AssertRequiresAdmin(typeof(UserClaimController));
        }

        [TestMethod]
        public void UserController_RequiresAdministratorRole()
        {
            AssertRequiresAdmin(typeof(UserController));
        }
    }
}
