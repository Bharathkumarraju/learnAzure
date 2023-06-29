using System;
using Benday.YamlDemoApp.Api.AzureStorage;

namespace Benday.YamlDemoApp.UnitTests.Fakes
{
    public class FakeAzureBlobImageSasTokenGenerator : IAzureBlobImageSasTokenGenerator
    {
        public bool WasGetBlobUriTokenCalled
        {
            get;
            private set;
        }

        public Uri GetBlobUri(string containerName, string blobName)
        {
            WasGetBlobUriTokenCalled = true;

            return new Uri($"http://www.benday.com/{containerName}/{blobName}");
        }

        public bool WasGetBlobUriWithSasTokenCalled
        {
            get;
            private set;
        }

        public Uri GetBlobUriWithSasToken(string containerName, string blobName)
        {
            WasGetBlobUriWithSasTokenCalled = true;

            return new Uri($"http://www.benday.com/{containerName}/{blobName}?token=token-value");
        }
    }
}
