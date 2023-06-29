using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Benday.Common;
using Benday.YamlDemoApp.Api.DomainModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Benday.YamlDemoApp.UnitTests.Utilities
{
    public static class UnitTestUtility
    {
        public static DateTime SafeToDateTime(string fromValue)
        {
            if (DateTime.TryParse(fromValue, out var temp) == true)
            {
                return temp;
            }
            else
            {
                return default;
            }
        }

        public static int SafeToInt32(string fromValue)
        {
            if (int.TryParse(fromValue, out var temp) == true)
            {
                return temp;
            }
            else
            {
                return default;
            }
        }

        internal static void Pause(int milliseconds)
        {
            var pause = new ManualResetEvent(false);

            pause.WaitOne(milliseconds);
        }

        internal static void WriteVeryVisibleMessageToConsole(string message)
        {
            var visibilityChar = '*';

            var visibilityString = new string(visibilityChar, message.Length);

            Console.WriteLine(visibilityString);

            Console.WriteLine(message);

            Console.WriteLine(visibilityString);
        }

        internal static int GetFakeValueForInt(string forFieldName)
        {
            if (forFieldName == null)
            {
                throw new ArgumentNullException(nameof(forFieldName));
            }

            var rnd = new Random();

            var lengthOfFieldName = forFieldName.Length;

            return rnd.Next() + lengthOfFieldName;
        }

        internal static double GetFakeValueForDouble(string forFieldName)
        {
            if (forFieldName == null)
            {
                throw new ArgumentNullException(nameof(forFieldName));
            }

            var rnd = new Random();

            var lengthOfFieldName = forFieldName.Length;

            return rnd.NextDouble() + (double)lengthOfFieldName;
        }

        internal static float GetFakeValueForFloat(string forFieldName)
        {
            if (forFieldName == null)
            {
                throw new ArgumentNullException(nameof(forFieldName));
            }

            var rnd = new Random();

            var lengthOfFieldName = forFieldName.Length;

            return Convert.ToSingle(rnd.NextDouble()) + (float)lengthOfFieldName;
        }

        internal static byte[] GetFakeValueForByteArray(string forFieldName)
        {
            var bytes = Encoding.Default.GetBytes(forFieldName);

            return bytes;
        }

        internal static string GetFakeValueForString(string forFieldName)
        {
            return string.Format("{0}_{1}", forFieldName, DateTime.Now.Ticks);
        }

        internal static string GetFakeValueForUrl(string forFieldName)
        {
            return string.Format(
            "http://www.fakestuff.com/images/{0}.jpg",
            forFieldName);
        }

        internal static DateTime GetFakeValueForDateTime(string forFieldName)
        {
            if (forFieldName == null)
            {
                throw new ArgumentNullException(nameof(forFieldName));
            }

            var lengthOfFieldName = forFieldName.Length;

            var now = DateTime.Now;

            return now.AddMinutes(lengthOfFieldName);
        }

        public static T GetModel<T>(IActionResult result) where T : class
        {
            Assert.IsNotNull(result, "Result is null.");

            if (result is OkObjectResult okObjResult)
            {
                Assert.IsNotNull(okObjResult.Value, "OkObjectResult.Value is null.");

                var temp = okObjResult.Value as T;

                Assert.IsNotNull(temp, "OkObjectResult.Value as {0} is null.", typeof(T).Name);

                return temp;
            }
            else if (result is ViewResult)
            {
                var asViewResult = result as ViewResult;

                var temp = asViewResult.Model as T;

                Assert.IsNotNull(asViewResult.Model, "ViewResult.Model is null.");
                Assert.IsNotNull(temp, "ViewResult.Model as {0} is null.", typeof(T).Name);

                return temp;
            }
            else
            {
                Assert.Fail("Unexpected result type '{0}'",
                result.GetType().FullName);

                throw new InvalidOperationException("Unexpected result type.");
            }
        }


        public static void AssertIsHttpNotFound(IActionResult actionResult)
        {
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        public static void AssertIsBadRequest(IActionResult actionResult)
        {
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult));
        }

        public static void AssertIsNoContentResult(IActionResult actionResult)
        {
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult));
        }

        public static void AssertIsViewResult(IActionResult actionResult)
        {
            Assert.IsInstanceOfType(actionResult, typeof(ViewResult));
        }

        public static void AssertIsRedirectToActionResult(
            IActionResult actionResult)
        {
            Assert.IsInstanceOfType(actionResult, typeof(RedirectToActionResult));
        }

        public static RedirectToActionResult AssertIsRedirectToActionResult(
            IActionResult actionResult, string expectedController, string expectedAction)
        {
            Assert.IsInstanceOfType(actionResult, typeof(RedirectToActionResult));

            var result = actionResult as RedirectToActionResult;

            Assert.AreEqual<string>(expectedController, result.ControllerName, "Controller name was wrong.");
            Assert.AreEqual<string>(expectedAction, result.ActionName, "Action name was wrong.");

            return result;
        }

        public static RedirectToActionResult AssertIsRedirectToActionResult(IActionResult actionResult,
            string expectedController, string expectedAction, string expectedId)
        {
            var result =
            AssertIsRedirectToActionResult(actionResult, expectedController, expectedAction);

            var actualId = result.RouteValues["id"].ToString();

            Assert.AreEqual<string>(expectedId, actualId, "Id did not match.");

            return result;
        }

        public static T AssertCreatedAtActionResultAndReturnModel<T>(
            IActionResult result,
            int onSaveUpdateIdToThisValue,
            T saveThis) where T : class, IInt32Identity
        {
            Assert.IsNotNull(result, "Result is null.");

            Assert.IsInstanceOfType(
            result, typeof(CreatedAtActionResult),
            "Wrong result type.");

            if (result is not CreatedAtActionResult item)
            {
                return null;
            }
            else
            {
                Assert.IsNotNull(item.Value, "Model is null.");

                Assert.IsInstanceOfType(
                item.Value, typeof(T),
                "Wrong result value type.");

                Assert.IsTrue(item.RouteValues.ContainsKey("id"),
                "Route values does not contain an item for id.");

                Assert.AreEqual<string>(saveThis.Id.ToString(),
                item.RouteValues["id"].ToString(), "Id route value was wrong.");

                return item.Value as T;
            }
        }


        public static void AssertDateTimeIsRecent(DateTime actual, string fieldName)
        {
            var now = DateTime.UtcNow;

            var diff = now - actual;

            var actualIsRecent = IsDateTimeRecent(now, actual);

            if (actualIsRecent == false)
            {
                Console.WriteLine("*** '{0}' is not a recent date ***",
                fieldName);
                Console.WriteLine("Recent Now Date: {0}", now);
                Console.WriteLine("Actual Date: {0}", actual);
                Console.WriteLine("**********************************");
            }

            Assert.IsTrue(
            actualIsRecent,
            string.Format("Expected '{0}' value to be less than '{1}'ms old but was '{2}'ms old",
            fieldName, UnitTestConstants.NumberOfMillisecondsForRecentDateTimeAssert,
            Math.Abs(diff.TotalMilliseconds)));
        }

        public static bool IsDateTimeRecent(DateTime value1, DateTime value2)
        {
            var diff = value1 - value2;

            if (Math.Abs(diff.TotalMilliseconds) <= UnitTestConstants.NumberOfMillisecondsForRecentDateTimeAssert)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void AssertDomainModelBaseIsSavedAndAuditFieldsArePopulated(
            List<CoreFieldsDomainModelBase> values, string valueName)
        {
            if (values == null)
            {
                return;
            }

            var index = 0;

            foreach (var item in values)
            {
                AssertDomainModelBaseIsSavedAndAuditFieldsArePopulated(
                item, string.Format("{0} #{1}", valueName, index));

                index++;
            }
        }

        public static void AssertDomainModelBaseIsSavedAndAuditFieldsArePopulated(
            CoreFieldsDomainModelBase actual, string valueName)
        {
            Assert.IsNotNull(valueName, "valueName parameter should not be null.");

            Assert.AreNotEqual<int>(0, actual.Id,
            string.Format(
            "Expected object to be saved but {0} Id was zero.",
            valueName));

            AssertIsNotNullOrWhitespace(
            actual.CreatedBy,
            string.Format("{0} CreatedBy", valueName));

            AssertIsNotNullOrWhitespace(
            actual.LastModifiedBy,
            string.Format("{0} LastModifiedBy", valueName));

            UnitTestUtility.AssertDateTimeIsRecent(
            actual.CreatedDate,
            string.Format("{0} CreatedDate", valueName));

            UnitTestUtility.AssertDateTimeIsRecent(
            actual.LastModifiedDate,
            string.Format("{0} LastModified", valueName));
        }

        public static void AssertDomainModelBaseAuditFieldsArePopulated(
            CoreFieldsDomainModelBase actual, string valueName)
        {
            Assert.IsNotNull(valueName, "valueName parameter should not be null.");

            AssertIsNotNullOrWhitespace(
            actual.CreatedBy,
            string.Format("{0} CreatedBy", valueName));

            AssertIsNotNullOrWhitespace(
            actual.LastModifiedBy,
            string.Format("{0} LastModifiedBy", valueName));

            UnitTestUtility.AssertDateTimeIsRecent(
            actual.CreatedDate,
            string.Format("{0} CreatedDate", valueName));

            UnitTestUtility.AssertDateTimeIsRecent(
            actual.LastModifiedDate,
            string.Format("{0} LastModified", valueName));
        }

        public static void AssertDomainModelBaseAuditFieldsArePopulated(
            CoreFieldsDomainModelBase actual, string expectedUsername, string valueName)
        {
            Assert.IsNotNull(valueName, "valueName parameter should not be null.");

            AssertDomainModelBaseAuditFieldsArePopulated(
            actual, valueName);


            Assert.AreEqual<string>(
            expectedUsername, actual.CreatedBy,
            string.Format("{0} CreatedBy", valueName));

            Assert.AreEqual<string>(
            expectedUsername, actual.LastModifiedBy,
            string.Format("{0} LastModifiedBy", valueName));
        }

        private static void AssertIsNotNullOrWhitespace(
            string actual, string valueName)
        {
            Assert.IsNotNull(actual, "Value for '{0}' should not be null.", valueName);

            Assert.IsFalse(string.IsNullOrWhiteSpace(actual),
            "Value for '{0}' should not be null or whitespace.", valueName);
        }
    }
}
