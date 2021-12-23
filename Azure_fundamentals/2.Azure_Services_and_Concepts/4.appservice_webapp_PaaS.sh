# host a web application 

# First create a appservice plan -- it defines the underlying infrastructure that the webservice runs on
# Second appservice webapp

1. az appservice plan 
2. az webapp create

sku --> defines the size, price and infrastructure that we are provisioning

MacBook-Pro:external bharathdasaraju$ az appservice plan create --resource-group BharathTestRG --name TestAppSvcPlan --sku S1
Resource provider 'Microsoft.Web' used by this operation is not registered. We are registering for you.
Registration succeeded.
{
  "freeOfferExpirationTime": null,
  "geoRegion": "East US",
  "hostingEnvironmentProfile": null,
  "hyperV": false,
  "id": "/subscriptions/fda65227-e02e-4d3b-9ae4-f32fb9f280fd/resourceGroups/BharathTestRG/providers/Microsoft.Web/serverfarms/TestAppSvcPlan",
  "isSpot": false,
  "isXenon": false,
  "kind": "app",
  "location": "eastus",
  "maximumElasticWorkerCount": 1,
  "maximumNumberOfWorkers": 0,
  "name": "TestAppSvcPlan",
  "numberOfSites": 0,
  "perSiteScaling": false,
  "provisioningState": "Succeeded",
  "reserved": false,
  "resourceGroup": "BharathTestRG",
  "sku": {
    "capabilities": null,
    "capacity": 1,
    "family": "S",
    "locations": null,
    "name": "S1",
    "size": "S1",
    "skuCapacity": null,
    "tier": "Standard"
  },
  "spotExpirationTime": null,
  "status": "Ready",
  "subscription": "fda65227-e02e-4d3b-9ae4-f32fb9f280fd",
  "systemData": null,
  "tags": null,
  "targetWorkerCount": 0,
  "targetWorkerSizeId": 0,
  "type": "Microsoft.Web/serverfarms",
  "workerTierName": null
}
MacBook-Pro:external bharathdasaraju$



MacBook-Pro:external bharathdasaraju$ az webapp create --resource-group BharathTestRG --plan TestAppSvcPlan --name bharathwebappcli
{
  "availabilityState": "Normal",
  "clientAffinityEnabled": true,
  "clientCertEnabled": false,
  "clientCertExclusionPaths": null,
  "clientCertMode": "Required",
  "cloningInfo": null,
  "containerSize": 0,
  "customDomainVerificationId": "EC8B77BFF15DF1F22DAAF3AA76DDE25E383AA2DD5DFDA00E1D6E6E495BFE416D",
  "dailyMemoryTimeQuota": 0,
  "defaultHostName": "bharathwebappcli.azurewebsites.net",
  "enabled": true,
  "enabledHostNames": [
    "bharathwebappcli.azurewebsites.net",
    "bharathwebappcli.scm.azurewebsites.net"
  ],
  "ftpPublishingUrl": "ftp://waws-prod-blu-275.ftp.azurewebsites.windows.net/site/wwwroot",
  "hostNameSslStates": [
    {
      "hostType": "Standard",
      "ipBasedSslResult": null,
      "ipBasedSslState": "NotConfigured",
      "name": "bharathwebappcli.azurewebsites.net",
      "sslState": "Disabled",
      "thumbprint": null,
      "toUpdate": null,
      "toUpdateIpBasedSsl": null,
      "virtualIp": null
    },
    {
      "hostType": "Repository",
      "ipBasedSslResult": null,
      "ipBasedSslState": "NotConfigured",
      "name": "bharathwebappcli.scm.azurewebsites.net",
      "sslState": "Disabled",
      "thumbprint": null,
      "toUpdate": null,
      "toUpdateIpBasedSsl": null,
      "virtualIp": null
    }
  ],
  "hostNames": [
    "bharathwebappcli.azurewebsites.net"
  ],
  "hostNamesDisabled": false,
  "hostingEnvironmentProfile": null,
  "httpsOnly": false,
  "hyperV": false,
  "id": "/subscriptions/fda65227-e02e-4d3b-9ae4-f32fb9f280fd/resourceGroups/BharathTestRG/providers/Microsoft.Web/sites/bharathwebappcli",
  "identity": null,
  "inProgressOperationId": null,
  "isDefaultContainer": null,
  "isXenon": false,
  "kind": "app",
  "lastModifiedTimeUtc": "2021-12-23T00:40:58.270000",
  "location": "East US",
  "maxNumberOfWorkers": null,
  "name": "bharathwebappcli",
  "outboundIpAddresses": "20.72.176.105,20.72.176.186,20.72.176.197,20.72.176.230,20.72.177.64,20.72.177.107,20.49.104.47",
  "possibleOutboundIpAddresses": "20.72.176.105,20.72.176.186,20.72.176.197,20.72.176.230,20.72.177.64,20.72.177.107,20.72.177.147,20.72.177.187,20.72.177.194,20.72.177.206,20.72.177.215,20.72.178.0,20.72.178.26,20.72.178.41,20.72.178.63,20.72.178.72,20.72.178.78,20.72.178.135,20.72.178.190,20.72.178.216,20.72.178.238,20.72.178.249,20.72.179.6,20.72.179.16,20.72.179.25,20.72.179.57,20.72.179.60,20.72.179.73,20.72.179.92,52.146.84.211,20.49.104.47",
  "redundancyMode": "None",
  "repositorySiteName": "bharathwebappcli",
  "reserved": false,
  "resourceGroup": "BharathTestRG",
  "scmSiteAlsoStopped": false,
  "serverFarmId": "/subscriptions/fda65227-e02e-4d3b-9ae4-f32fb9f280fd/resourceGroups/BharathTestRG/providers/Microsoft.Web/serverfarms/TestAppSvcPlan",
  "siteConfig": {
    "acrUseManagedIdentityCreds": false,
    "acrUserManagedIdentityId": null,
    "alwaysOn": false,
    "antivirusScanEnabled": null,
    "apiDefinition": null,
    "apiManagementConfig": null,
    "appCommandLine": null,
    "appSettings": null,
    "autoHealEnabled": null,
    "autoHealRules": null,
    "autoSwapSlotName": null,
    "azureMonitorLogCategories": null,
    "azureStorageAccounts": null,
    "connectionStrings": null,
    "cors": null,
    "customAppPoolIdentityAdminState": null,
    "customAppPoolIdentityTenantState": null,
    "defaultDocuments": null,
    "detailedErrorLoggingEnabled": null,
    "documentRoot": null,
    "experiments": null,
    "fileChangeAuditEnabled": null,
    "ftpsState": null,
    "functionAppScaleLimit": 0,
    "functionsRuntimeScaleMonitoringEnabled": null,
    "handlerMappings": null,
    "healthCheckPath": null,
    "http20Enabled": false,
    "http20ProxyFlag": null,
    "httpLoggingEnabled": null,
    "ipSecurityRestrictions": [
      {
        "action": "Allow",
        "description": "Allow all access",
        "headers": null,
        "ipAddress": "Any",
        "name": "Allow all",
        "priority": 1,
        "subnetMask": null,
        "subnetTrafficTag": null,
        "tag": null,
        "vnetSubnetResourceId": null,
        "vnetTrafficTag": null
      }
    ],
    "javaContainer": null,
    "javaContainerVersion": null,
    "javaVersion": null,
    "keyVaultReferenceIdentity": null,
    "limits": null,
    "linuxFxVersion": "",
    "loadBalancing": null,
    "localMySqlEnabled": null,
    "logsDirectorySizeLimit": null,
    "machineKey": null,
    "managedPipelineMode": null,
    "managedServiceIdentityId": null,
    "metadata": null,
    "minTlsVersion": null,
    "minimumElasticInstanceCount": 0,
    "netFrameworkVersion": null,
    "nodeVersion": null,
    "numberOfWorkers": 1,
    "phpVersion": null,
    "powerShellVersion": null,
    "preWarmedInstanceCount": null,
    "publicNetworkAccess": null,
    "publishingPassword": null,
    "publishingUsername": null,
    "push": null,
    "pythonVersion": null,
    "remoteDebuggingEnabled": null,
    "remoteDebuggingVersion": null,
    "requestTracingEnabled": null,
    "requestTracingExpirationTime": null,
    "routingRules": null,
    "runtimeADUser": null,
    "runtimeADUserPassword": null,
    "scmIpSecurityRestrictions": [
      {
        "action": "Allow",
        "description": "Allow all access",
        "headers": null,
        "ipAddress": "Any",
        "name": "Allow all",
        "priority": 1,
        "subnetMask": null,
        "subnetTrafficTag": null,
        "tag": null,
        "vnetSubnetResourceId": null,
        "vnetTrafficTag": null
      }
    ],
    "scmIpSecurityRestrictionsUseMain": null,
    "scmMinTlsVersion": null,
    "scmType": null,
    "sitePort": null,
    "tracingOptions": null,
    "use32BitWorkerProcess": null,
    "virtualApplications": null,
    "vnetName": null,
    "vnetPrivatePortsCount": null,
    "vnetRouteAllEnabled": null,
    "webSocketsEnabled": null,
    "websiteTimeZone": null,
    "winAuthAdminState": null,
    "winAuthTenantState": null,
    "windowsFxVersion": null,
    "xManagedServiceIdentityId": null
  },
  "slotSwapStatus": null,
  "state": "Running",
  "suspendedTill": null,
  "systemData": null,
  "tags": null,
  "targetSwapSlot": null,
  "trafficManagerHostNames": null,
  "type": "Microsoft.Web/sites",
  "usageState": "Normal"
}
MacBook-Pro:external bharathdasaraju$



MacBook-Pro:external bharathdasaraju$ az resource list --resource-group BharathTestRG --out table --query "[].{name:name, Type:type}"
Name            Type
--------------  ---------------------------------
bharathstorage  Microsoft.Storage/storageAccounts
TestAppSvcPlan  Microsoft.Web/serverFarms
MacBook-Pro:external bharathdasaraju$

