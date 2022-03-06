test the order function locally


serverless

1. Create Storage Account

bharathkumar@Azure:~$ az storage account create --name bkrreaditfuncstorage --location eastasia --resource-group readit-app-rg --sku Standard_LRS
{
  "accessTier": "Hot",
  "allowBlobPublicAccess": true,
  "allowCrossTenantReplication": null,
  "allowSharedKeyAccess": null,
  "allowedCopyScope": null,
  "azureFilesIdentityBasedAuthentication": null,
  "blobRestoreStatus": null,
  "creationTime": "2022-03-06T17:33:57.341326+00:00",
  "customDomain": null,
  "defaultToOAuthAuthentication": null,
  "enableHttpsTrafficOnly": true,
  "enableNfsV3": null,
  "encryption": {
    "encryptionIdentity": null,
    "keySource": "Microsoft.Storage",
    "keyVaultProperties": null,
    "requireInfrastructureEncryption": null,
    "services": {
      "blob": {
        "enabled": true,
        "keyType": "Account",
        "lastEnabledTime": "2022-03-06T17:33:57.403844+00:00"
      },
      "file": {
        "enabled": true,
        "keyType": "Account",
        "lastEnabledTime": "2022-03-06T17:33:57.403844+00:00"
      },
      "queue": null,
      "table": null
    }
  },
  "extendedLocation": null,
  "failoverInProgress": null,
  "geoReplicationStats": null,
  "id": "/subscriptions/1cb67706-f0c0-4a7c-9940-4d9779fbce91/resourceGroups/readit-app-rg/providers/Microsoft.Storage/storageAccounts/bkrreaditfuncstorage",
  "identity": null,
  "immutableStorageWithVersioning": null,
  "isHnsEnabled": null,
  "isLocalUserEnabled": null,
  "isSftpEnabled": null,
  "keyCreationTime": {
    "key1": "2022-03-06T17:33:57.403844+00:00",
    "key2": "2022-03-06T17:33:57.403844+00:00"
  },
  "keyPolicy": null,
  "kind": "StorageV2",
  "largeFileSharesState": null,
  "lastGeoFailoverTime": null,
  "location": "eastasia",
  "minimumTlsVersion": "TLS1_0",
  "name": "bkrreaditfuncstorage",
  "networkRuleSet": {
    "bypass": "AzureServices",
    "defaultAction": "Allow",
    "ipRules": [],
    "resourceAccessRules": null,
    "virtualNetworkRules": []
  },
  "primaryEndpoints": {
    "blob": "https://bkrreaditfuncstorage.blob.core.windows.net/",
    "dfs": "https://bkrreaditfuncstorage.dfs.core.windows.net/",
    "file": "https://bkrreaditfuncstorage.file.core.windows.net/",
    "internetEndpoints": null,
    "microsoftEndpoints": null,
    "queue": "https://bkrreaditfuncstorage.queue.core.windows.net/",
    "table": "https://bkrreaditfuncstorage.table.core.windows.net/",
    "web": "https://bkrreaditfuncstorage.z7.web.core.windows.net/"
  },
  "primaryLocation": "eastasia",
  "privateEndpointConnections": [],
  "provisioningState": "Succeeded",
  "publicNetworkAccess": null,
  "resourceGroup": "readit-app-rg",
  "routingPreference": null,
  "sasPolicy": null,
  "secondaryEndpoints": null,
  "secondaryLocation": null,
  "sku": {
    "name": "Standard_LRS",
    "tier": "Standard"
  },
  "statusOfPrimary": "available",
  "statusOfSecondary": null,
  "tags": {},
  "type": "Microsoft.Storage/storageAccounts"
}
bharathkumar@Azure:~$


2.Create function:

