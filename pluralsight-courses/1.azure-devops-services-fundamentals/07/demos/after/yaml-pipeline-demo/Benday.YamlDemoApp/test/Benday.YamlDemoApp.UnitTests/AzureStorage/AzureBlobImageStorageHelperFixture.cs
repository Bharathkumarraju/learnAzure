using System;
using Benday.YamlDemoApp.Api.AzureStorage;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Benday.YamlDemoApp.UnitTests.AzureStorage
{
    [TestClass()]
    public class AzureBlobImageStorageHelperFixture
    {
        [TestInitialize]
        public void OnTestInitialize()
        {
            _systemUnderTest = null;
        }

        private AzureBlobImageStorageHelper _systemUnderTest;
        public AzureBlobImageStorageHelper SystemUnderTest
        {
            get
            {
                if (_systemUnderTest == null)
                {
                    _systemUnderTest =
                    new AzureBlobImageStorageHelper(GetOptionsConfiguration());
                }

                return _systemUnderTest;
            }
        }

        private static IOptionsMonitor<AzureBlobImageStorageOptions> GetOptionsConfiguration()
        {
            var config = new AzureBlobImageStorageOptions
            {
                UseDevelopmentStorage = true,
                ContainerName = "course-assets"
            };

            var returnValue = new OptionsMonitorMock<AzureBlobImageStorageOptions>
            {
                CurrentValue = config
            };

            return returnValue;
        }

        [TestMethod()]
        public void GetTokenAndAppendToUri_RelativeUrl_ForDevelopmentStorage()
        {
            // arrange
            var container = "course-assets";

            var expectedStartOfUrl = "http://127.0.0.1:10000/devstoreaccount1/course-assets/azure-devops-getting-started/m02/azure-devops-getting-started-m2-01.mp4";

            var relativeUrl = "azure-devops-getting-started/m02/azure-devops-getting-started-m2-01.mp4";

            // act
            var actual = SystemUnderTest.GetBlobUriWithSasToken(container, relativeUrl);

            // assert
            Console.WriteLine(actual);
            StringAssert.StartsWith(actual.ToString(), expectedStartOfUrl);
        }
    }
}
