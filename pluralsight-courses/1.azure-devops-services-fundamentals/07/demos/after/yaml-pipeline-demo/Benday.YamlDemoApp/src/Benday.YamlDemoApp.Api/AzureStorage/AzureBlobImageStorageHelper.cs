using System;
using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using Microsoft.Extensions.Options;

namespace Benday.YamlDemoApp.Api.AzureStorage
{

    public class AzureBlobImageStorageHelper : IAzureBlobImageSasTokenGenerator
    {
        private readonly AzureBlobImageStorageOptions _options;

        public AzureBlobImageStorageHelper(IOptionsMonitor<AzureBlobImageStorageOptions> options)
        {
            if (options is null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            _options = options.CurrentValue;
        }

        public Uri GetBlobUriWithSasToken(string containerName, string blobName)
        {
            var builder = new BlobSasBuilder
            {
                BlobContainerName = containerName
            };

            if (blobName.StartsWith("/") == true)
            {
                blobName = blobName[1..];
            }

            var containerPrefix = containerName + "/";

            if (blobName.StartsWith(containerPrefix) == true)
            {
                blobName = blobName[containerPrefix.Length..];
            }

            builder.BlobName = blobName;

            builder.SetPermissions(BlobSasPermissions.Read);

            builder.ExpiresOn = DateTime.UtcNow.AddSeconds(
            _options.ReadTokenExpirationInSeconds);

            var credentials = GetAzureCredentials();

            var parameters = builder.ToSasQueryParameters(credentials);

            var token = parameters.ToString();

            string path;

            if (_options.UseDevelopmentStorage == true)
            {
                path = string.Format("{0}/{1}/{2}",
                credentials.AccountName, containerName, blobName);
            }
            else
            {
                path = string.Format("{0}/{1}", containerName, blobName);
            }

            var fullUri = new UriBuilder(BlobServiceClientInstance.Uri)
            {
                Path = path,
                Query = token
            };

            return fullUri.Uri;
        }

        public Uri GetBlobUri(string containerName, string blobName)
        {
            if (blobName.StartsWith("/") == true)
            {
                blobName = blobName[1..];
            }

            var containerPrefix = containerName + "/";

            if (blobName.StartsWith(containerPrefix) == true)
            {
                blobName = blobName[containerPrefix.Length..];
            }

            var credentials = GetAzureCredentials();

            string path;

            if (_options.UseDevelopmentStorage == true)
            {
                path = string.Format("{0}/{1}/{2}",
                credentials.AccountName, containerName, blobName);
            }
            else
            {
                path = string.Format("{0}/{1}", containerName, blobName);
            }

            var fullUri = new UriBuilder(BlobServiceClientInstance.Uri)
            {
                Path = path
            };

            return fullUri.Uri;
        }

        private BlobServiceClient _blobServiceClientInstance;
        private BlobServiceClient BlobServiceClientInstance
        {
            get
            {
                if (_blobServiceClientInstance == null)
                {
                    if (_options.UseDevelopmentStorage == true)
                    {
                        _blobServiceClientInstance =
                        new BlobServiceClient("UseDevelopmentStorage=true");
                    }
                    else
                    {
                        var endpoint = new Uri(
                        string.Format(
                        "https://{0}.blob.core.windows.net",
                        _options.AccountName));

                        _blobServiceClientInstance =
                        new BlobServiceClient(endpoint,
                        GetAzureCredentials());
                    }
                }

                return _blobServiceClientInstance;
            }
        }

        private StorageSharedKeyCredential GetAzureCredentials()
        {
            if (_options.UseDevelopmentStorage == true)
            {
                var accountName = "devstoreaccount1";
                var accountKey = "Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==";

                var temp = new StorageSharedKeyCredential(
                accountName, accountKey);

                return temp;
            }
            else
            {
                if (string.IsNullOrWhiteSpace(_options.AccountName) == true)
                {
                    throw new InvalidOperationException(
                    "Azure account name is not configured.");
                }

                if (string.IsNullOrWhiteSpace(_options.AccountKey) == true)
                {
                    throw new InvalidOperationException(
                    "Azure account key is not configured.");
                }

                var temp = new StorageSharedKeyCredential(
                _options.AccountName, _options.AccountKey);

                return temp;
            }
        }
    }
}
