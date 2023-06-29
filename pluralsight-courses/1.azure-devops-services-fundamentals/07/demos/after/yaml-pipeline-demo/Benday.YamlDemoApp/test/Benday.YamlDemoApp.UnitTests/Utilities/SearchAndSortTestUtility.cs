using System;
using Benday.YamlDemoApp.WebUi.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Benday.YamlDemoApp.UnitTests.Utilities
{
    public static class SearchAndSortTestUtility
    {
        public static void AssertSearchResultsAndSortDirection<T>(SearchViewModelBase<T> actual, string expectedSortBy, string expectedSortDirection)
        {
            if (actual is null)
            {
                throw new ArgumentNullException(nameof(actual));
            }

            Assert.IsNotNull(actual, "Model was null");
            Assert.IsNotNull(actual.Results, "Results was null");
            Assert.IsNotNull(actual.Results.PageValues, "Results.PageValues was null");
            Assert.AreNotEqual<int>(0, actual.Results.PageValues.Count, "No items on the current page");

            if (expectedSortBy == null)
            {
                Assert.IsTrue(string.IsNullOrWhiteSpace(actual.CurrentSortProperty),
                "CurrentSortProperty should be null or empty.");
            }
            else
            {
                Assert.AreEqual<string>(expectedSortBy, actual.CurrentSortProperty, "CurrentSortProperty should be null or empty.");
            }

            Assert.AreEqual<string>(expectedSortDirection, actual.CurrentSortDirection, "CurrentSortDirection was wrong");
        }
    }
}
