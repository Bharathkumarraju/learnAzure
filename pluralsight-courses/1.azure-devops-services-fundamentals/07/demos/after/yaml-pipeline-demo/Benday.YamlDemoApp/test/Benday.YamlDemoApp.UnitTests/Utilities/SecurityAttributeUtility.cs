using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Benday.YamlDemoApp.Api.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Benday.YamlDemoApp.UnitTests.Utilities
{
    public static class SecurityAttributeUtility
    {
        public static void AssertAuthorizeAttributeRolesOnMethod(
            string expectedRoles,
            Type containingDataType, string methodName,
            params Type[] methodParameters
            )
        {
            var attribute =
            GetAttributeFromMethod<AuthorizeAttribute>(
            containingDataType, methodName, methodParameters);

            Assert.IsNotNull(attribute,
            "Method '{0}' on class '{1}' does not have an authorize attribute.",
            methodName, containingDataType.FullName);

            Assert.AreEqual<string>(expectedRoles,
            attribute.Roles,
            "Roles contains the wrong value.");
        }

        public static void AssertAuthorizeAttributePolicyOnMethod(
            string expectedPolicy,
            Type containingDataType, string methodName,
            params Type[] methodParameters
            )
        {
            var attribute =
            GetAttributeFromMethod<AuthorizeAttribute>(
            containingDataType, methodName, methodParameters);

            Assert.IsNotNull(attribute,
            "Method '{0}' on class '{1}' does not have an authorize attribute.",
            methodName, containingDataType.FullName);

            Assert.AreEqual<string>(expectedPolicy,
            attribute.Policy,
            "Policy contains the wrong value.");
        }

        public static void AssertRequiresAdminOnPublicMethods(
            Type containingDataType, List<string> exceptThese)
        {
            var methods = containingDataType.GetMethods(
            BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);

            foreach (var method in methods)
            {
                if (exceptThese.Contains(method.Name) == true)
                {
                    continue;
                }

                AssertPolicyOnMethod(method, SecurityConstants.Policy_IsAdministrator);
            }
        }

        private static void AssertPolicyOnMethod(MethodInfo method, string expectedPolicy)
        {
            var attribute = GetAttributeFromMethod<AuthorizeAttribute>(method);

            Assert.IsNotNull(attribute,
            "Method '{0}' does not have an authorize attribute.",
            method.Name);

            Assert.AreEqual<string>(expectedPolicy,
            attribute.Policy,
            "Policy on method '{0}' contains the wrong value.", method.Name);
        }

        public static void AssertAuthorizeAttributePolicyOnClass(
            string expectedPolicy,
            Type containingDataType)
        {
            var attribute =
            GetAttributeFromClass<AuthorizeAttribute>(
            containingDataType);

            Assert.IsNotNull(attribute,
            "Class '{0}' does not have an authorize attribute.",
            containingDataType.FullName);

            Assert.AreEqual<string>(expectedPolicy,
            attribute.Policy,
            "Policy contains the wrong value.");
        }

        public static void AssertAuthorizeAttributeOnClassWithNoArgs(
            Type containingDataType)
        {
            var attribute =
            GetAttributeFromClass<AuthorizeAttribute>(
            containingDataType);

            Assert.IsNotNull(attribute,
            "Class '{0}' does not have an authorize attribute.",
            containingDataType.FullName);

            Assert.IsTrue(string.IsNullOrEmpty(attribute.Policy),
            "Attribute should not define a policy.");

            Assert.IsTrue(string.IsNullOrEmpty(attribute.Roles),
            "Attribute should not define roles.");
        }

        public static void AssertHasSomeKindOfSecurityDefinedOnClass(Type containingDataType)
        {
            var authorizeAttr =
            GetAttributeFromClass<AuthorizeAttribute>(
            containingDataType);

            var allowAnonymousAttr =
            GetAttributeFromClass<AllowAnonymousAttribute>(
            containingDataType);

            if (authorizeAttr == null && allowAnonymousAttr == null)
            {
                Assert.Fail("Controller '{0}' does not have any security attribute checks set.",
                containingDataType.Name);
            }
        }

        public static void AssertAuthorizeAttributeOnMethod(Type containingDataType,
            string methodName,
            params Type[] methodParameters)
        {
            var attribute =
            GetAttributeFromMethod<AuthorizeAttribute>(
            containingDataType, methodName, methodParameters);

            Assert.IsNotNull(attribute,
            "Method '{0}' on class '{1}' does not have an allow anonymous attribute.",
            methodName, containingDataType.FullName);
        }

        public static void AssertAllowAnonymousAttributeOnMethod(
            Type containingDataType,
            string methodName,
            params Type[] methodParameters
            )
        {
            var attribute =
            GetAttributeFromMethod<AllowAnonymousAttribute>(
            containingDataType, methodName, methodParameters);

            Assert.IsNotNull(attribute,
            "Method '{0}' on class '{1}' does not have an allow anonymous attribute.",
            methodName, containingDataType.FullName);
        }



        public static void AssertDoNotAllowAnonymousAttributeOnMethod(
            Type containingDataType,
            string methodName,
            params Type[] methodParameters
            )
        {
            var attribute =
            GetAttributeFromMethod<AllowAnonymousAttribute>(
            containingDataType, methodName, methodParameters);

            Assert.IsNull(attribute,
            "Method '{0}' on class '{1}' should not have an allow anonymous attribute.",
            methodName, containingDataType.FullName);
        }

        public static void AssertAuthorizeAttributeRolesOnClass(
            string expectedRoles,
            Type containingDataType
            )
        {
            var attribute =
            GetAttributeFromClass<AuthorizeAttribute>(
            containingDataType);

            Assert.IsNotNull(attribute,
            "Class '{0}' does not have an authorize attribute.",
            containingDataType.FullName);

            Assert.AreEqual<string>(expectedRoles,
            attribute.Roles,
            "Roles contains the wrong value.");
        }

        private static T GetAttributeFromMethod<T>(
            Type containingDataType,
            string methodName,
            params Type[] methodArgs) where T : Attribute
        {
            var method = containingDataType.GetMethod(
            methodName, methodArgs);

            Assert.IsNotNull(method,
            "Could not locate a method named '{0}' with matching parameters.",
            methodName);

            var attribute = method.GetCustomAttributes(
            typeof(T), true).FirstOrDefault();

            return attribute as T;
        }

        private static T GetAttributeFromMethod<T>(
            MethodInfo method) where T : Attribute
        {
            var attribute = method.GetCustomAttributes(
            typeof(T), true).FirstOrDefault();

            return attribute as T;
        }

        private static T GetAttributeFromClass<T>(
            Type containingDataType)
            where T : Attribute
        {

            var attribute = containingDataType.GetCustomAttributes(
            typeof(T), true).FirstOrDefault();

            return attribute as T;
        }
    }
}
