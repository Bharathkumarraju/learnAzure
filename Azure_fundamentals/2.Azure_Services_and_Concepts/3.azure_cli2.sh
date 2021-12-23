
MacBook-Pro:external bharathdasaraju$ az group list | jq
[
  {
    "id": "/subscriptions/fda65227-e02e-4d3b-9ae4-f32fb9f280fd/resourceGroups/BharathTestRG",
    "location": "eastus",
    "managedBy": null,
    "name": "BharathTestRG",
    "properties": {
      "provisioningState": "Succeeded"
    },
    "tags": {},
    "type": "Microsoft.Resources/resourceGroups"
  },
  {
    "id": "/subscriptions/fda65227-e02e-4d3b-9ae4-f32fb9f280fd/resourceGroups/cloud-shell-storage-southeastasia",
    "location": "southeastasia",
    "managedBy": null,
    "name": "cloud-shell-storage-southeastasia",
    "properties": {
      "provisioningState": "Succeeded"
    },
    "tags": null,
    "type": "Microsoft.Resources/resourceGroups"
  }
]
MacBook-Pro:external bharathdasaraju$ az resource list --resource-group BharathTestRG | jq
[]
MacBook-Pro:external bharathdasaraju$ az resource list --resource-group cloud-shell-storage-southeastasia | jq
[
  {
    "changedTime": "2021-12-22T00:24:44.502719+00:00",
    "createdTime": "2021-12-22T00:14:20.251884+00:00",
    "extendedLocation": null,
    "id": "/subscriptions/fda65227-e02e-4d3b-9ae4-f32fb9f280fd/resourceGroups/cloud-shell-storage-southeastasia/providers/Microsoft.Storage/storageAccounts/cs110032001c3e96f06",
    "identity": null,
    "kind": "StorageV2",
    "location": "southeastasia",
    "managedBy": null,
    "name": "cs110032001c3e96f06",
    "plan": null,
    "properties": null,
    "provisioningState": "Succeeded",
    "resourceGroup": "cloud-shell-storage-southeastasia",
    "sku": {
      "capacity": null,
      "family": null,
      "model": null,
      "name": "Standard_LRS",
      "size": null,
      "tier": "Standard"
    },
    "tags": {
      "ms-resource-usage": "azure-cloud-shell"
    },
    "type": "Microsoft.Storage/storageAccounts"
  }
]
MacBook-Pro:external bharathdasaraju$


MacBook-Pro:external bharathdasaraju$
MacBook-Pro:external bharathdasaraju$ az resource list --resource-group cloud-shell-storage-southeastasia --out table
Name                 ResourceGroup                      Location       Type                               Status
-------------------  ---------------------------------  -------------  ---------------------------------  --------
cs110032001c3e96f06  cloud-shell-storage-southeastasia  southeastasia  Microsoft.Storage/storageAccounts
MacBook-Pro:external bharathdasaraju$


MacBook-Pro:external bharathdasaraju$ az resource list --resource-group cloud-shell-storage-southeastasia --out table --query "[].{name:name, Type:type}"
Name                 Type
-------------------  ---------------------------------
cs110032001c3e96f06  Microsoft.Storage/storageAccounts
MacBook-Pro:external bharathdasaraju$







MacBook-Pro:external bharathdasaraju$ az resource list --resource-group BharathTestRG
[
  {
    "changedTime": "2021-12-22T01:12:41.077080+00:00",
    "createdTime": "2021-12-22T01:02:10.533817+00:00",
    "extendedLocation": null,
    "id": "/subscriptions/fda65227-e02e-4d3b-9ae4-f32fb9f280fd/resourceGroups/BharathTestRG/providers/Microsoft.Storage/storageAccounts/bharathstorage",
    "identity": null,
    "kind": "StorageV2",
    "location": "eastus",
    "managedBy": null,
    "name": "bharathstorage",
    "plan": null,
    "properties": null,
    "provisioningState": "Succeeded",
    "resourceGroup": "BharathTestRG",
    "sku": {
      "capacity": null,
      "family": null,
      "model": null,
      "name": "Standard_LRS",
      "size": null,
      "tier": "Standard"
    },
    "tags": {},
    "type": "Microsoft.Storage/storageAccounts"
  }
]
MacBook-Pro:external bharathdasaraju$ az resource list --resource-group BharathTestRG --out table
Name            ResourceGroup    Location    Type                               Status
--------------  ---------------  ----------  ---------------------------------  --------
bharathstorage  BharathTestRG    eastus      Microsoft.Storage/storageAccounts
MacBook-Pro:external bharathdasaraju$ az resource list --resource-group BharathTestRG --out table --query "[]"
Name            Location    Kind       CreatedTime                       ChangedTime                       ProvisioningState    ResourceGroup
--------------  ----------  ---------  --------------------------------  --------------------------------  -------------------  ---------------
bharathstorage  eastus      StorageV2  2021-12-22T01:02:10.533817+00:00  2021-12-22T01:12:41.077080+00:00  Succeeded            BharathTestRG
MacBook-Pro:external bharathdasaraju$

MacBook-Pro:external bharathdasaraju$ az resource list --resource-group BharathTestRG --out table --query "[].{name:name}"
Name
--------------
bharathstorage
MacBook-Pro:external bharathdasaraju$ az resource list --resource-group BharathTestRG --out table --query "[].{name:name, Kind:kind}"
Name            Kind
--------------  ---------
bharathstorage  StorageV2

MacBook-Pro:external bharathdasaraju$ az resource list --resource-group BharathTestRG --out table --query "[].{Name:name, Kind:kind}"
Name            Kind
--------------  ---------
bharathstorage  StorageV2
MacBook-Pro:external bharathdasaraju$


