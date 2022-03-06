bharathkumar@Azure:~/clouddrive/templates$ az deployment group create --resource-group optimized-vm-rg --template-file template.json --parameters parameters.json 
{
  "id": "/subscriptions/1cb67706-f0c0-4a7c-9940-4d9779fbce91/resourceGroups/optimized-vm-rg/providers/Microsoft.Resources/deployments/template",
  "location": null,
  "name": "template",
  "properties": {
    "correlationId": "dbd7264a-763b-4950-b34a-044c3f4dd1a9",
    "debugSetting": null,
    "dependencies": [
      {
        "dependsOn": [
          {
            "id": "/subscriptions/1cb67706-f0c0-4a7c-9940-4d9779fbce91/resourceGroups/optimized-vm-rg/providers/Microsoft.Network/networkSecurityGroups/optimized-vm-nsg",
            "resourceGroup": "optimized-vm-rg",
            "resourceName": "optimized-vm-nsg",
            "resourceType": "Microsoft.Network/networkSecurityGroups"
          },
          {
            "id": "/subscriptions/1cb67706-f0c0-4a7c-9940-4d9779fbce91/resourceGroups/optimized-vm-rg/providers/Microsoft.Network/virtualNetworks/optimized-vm-rg-vnet",
            "resourceGroup": "optimized-vm-rg",
            "resourceName": "optimized-vm-rg-vnet",
            "resourceType": "Microsoft.Network/virtualNetworks"
          },
          {
            "id": "/subscriptions/1cb67706-f0c0-4a7c-9940-4d9779fbce91/resourceGroups/optimized-vm-rg/providers/Microsoft.Network/publicIpAddresses/optimized-vm-ip",
            "resourceGroup": "optimized-vm-rg",
            "resourceName": "optimized-vm-ip",
            "resourceType": "Microsoft.Network/publicIpAddresses"
          }
        ],
        "id": "/subscriptions/1cb67706-f0c0-4a7c-9940-4d9779fbce91/resourceGroups/optimized-vm-rg/providers/Microsoft.Network/networkInterfaces/optimized-vm154",
        "resourceGroup": "optimized-vm-rg",
        "resourceName": "optimized-vm154",
        "resourceType": "Microsoft.Network/networkInterfaces"
      },
      {
        "dependsOn": [
          {
            "id": "/subscriptions/1cb67706-f0c0-4a7c-9940-4d9779fbce91/resourceGroups/optimized-vm-rg/providers/Microsoft.Network/networkInterfaces/optimized-vm154",
            "resourceGroup": "optimized-vm-rg",
            "resourceName": "optimized-vm154",
            "resourceType": "Microsoft.Network/networkInterfaces"
          }
        ],
        "id": "/subscriptions/1cb67706-f0c0-4a7c-9940-4d9779fbce91/resourceGroups/optimized-vm-rg/providers/Microsoft.Compute/virtualMachines/optimized-vm",
        "resourceGroup": "optimized-vm-rg",
        "resourceName": "optimized-vm",
        "resourceType": "Microsoft.Compute/virtualMachines"
      },
      {
        "dependsOn": [
          {
            "id": "/subscriptions/1cb67706-f0c0-4a7c-9940-4d9779fbce91/resourceGroups/optimized-vm-rg/providers/Microsoft.Compute/virtualMachines/optimized-vm",
            "resourceGroup": "optimized-vm-rg",
            "resourceName": "optimized-vm",
            "resourceType": "Microsoft.Compute/virtualMachines"
          }
        ],
        "id": "/subscriptions/1cb67706-f0c0-4a7c-9940-4d9779fbce91/resourceGroups/optimized-vm-rg/providers/Microsoft.DevTestLab/schedules/shutdown-computevm-optimized-vm",
        "resourceGroup": "optimized-vm-rg",
        "resourceName": "shutdown-computevm-optimized-vm",
        "resourceType": "Microsoft.DevTestLab/schedules"
      }
    ],
    "duration": "PT1M23.0068318S",
    "error": null,
    "mode": "Incremental",
    "onErrorDeployment": null,
    "outputResources": [
      {
        "id": "/subscriptions/1cb67706-f0c0-4a7c-9940-4d9779fbce91/resourceGroups/optimized-vm-rg/providers/Microsoft.Compute/virtualMachines/optimized-vm",
        "resourceGroup": "optimized-vm-rg"
      },
      {
        "id": "/subscriptions/1cb67706-f0c0-4a7c-9940-4d9779fbce91/resourceGroups/optimized-vm-rg/providers/Microsoft.DevTestLab/schedules/shutdown-computevm-optimized-vm",
        "resourceGroup": "optimized-vm-rg"
      },
      {
        "id": "/subscriptions/1cb67706-f0c0-4a7c-9940-4d9779fbce91/resourceGroups/optimized-vm-rg/providers/Microsoft.Network/networkInterfaces/optimized-vm154",
        "resourceGroup": "optimized-vm-rg"
      },
      {
        "id": "/subscriptions/1cb67706-f0c0-4a7c-9940-4d9779fbce91/resourceGroups/optimized-vm-rg/providers/Microsoft.Network/networkSecurityGroups/optimized-vm-nsg",
        "resourceGroup": "optimized-vm-rg"
      },
      {
        "id": "/subscriptions/1cb67706-f0c0-4a7c-9940-4d9779fbce91/resourceGroups/optimized-vm-rg/providers/Microsoft.Network/publicIpAddresses/optimized-vm-ip",
        "resourceGroup": "optimized-vm-rg"
      },
      {
        "id": "/subscriptions/1cb67706-f0c0-4a7c-9940-4d9779fbce91/resourceGroups/optimized-vm-rg/providers/Microsoft.Network/virtualNetworks/optimized-vm-rg-vnet",
        "resourceGroup": "optimized-vm-rg"
      }
    ],
    "outputs": {
      "adminUsername": {
        "type": "String",
        "value": "bharath"
      }
    },
    "parameters": {
      "addressPrefixes": {
        "type": "Array",
        "value": [
          "10.1.0.0/16"
        ]
      },
      "adminPassword": {
        "type": "SecureString"
      },
      "adminUsername": {
        "type": "String",
        "value": "bharath"
      },
      "autoShutdownNotificationLocale": {
        "type": "String",
        "value": "en"
      },
      "autoShutdownNotificationStatus": {
        "type": "String",
        "value": "Disabled"
      },
      "autoShutdownStatus": {
        "type": "String",
        "value": "Enabled"
      },
      "autoShutdownTime": {
        "type": "String",
        "value": "19:00"
      },
      "autoShutdownTimeZone": {
        "type": "String",
        "value": "UTC"
      },
      "enableAcceleratedNetworking": {
        "type": "Bool",
        "value": true
      },
      "enableHotpatching": {
        "type": "Bool",
        "value": false
      },
      "location": {
        "type": "String",
        "value": "southeastasia"
      },
      "networkInterfaceName": {
        "type": "String",
        "value": "optimized-vm154"
      },
      "networkSecurityGroupName": {
        "type": "String",
        "value": "optimized-vm-nsg"
      },
      "networkSecurityGroupRules": {
        "type": "Array",
        "value": [
          {
            "name": "RDP",
            "properties": {
              "access": "Allow",
              "destinationAddressPrefix": "*",
              "destinationPortRange": "3389",
              "direction": "Inbound",
              "priority": 300,
              "protocol": "TCP",
              "sourceAddressPrefix": "*",
              "sourcePortRange": "*"
            }
          }
        ]
      },
      "nicDeleteOption": {
        "type": "String",
        "value": "Detach"
      },
      "osDiskDeleteOption": {
        "type": "String",
        "value": "Delete"
      },
      "osDiskType": {
        "type": "String",
        "value": "standardSSD_LRS"
      },
      "patchMode": {
        "type": "String",
        "value": "AutomaticByOS"
      },
      "pipDeleteOption": {
        "type": "String",
        "value": "Detach"
      },
      "publicIpAddressName": {
        "type": "String",
        "value": "optimized-vm-ip"
      },
      "publicIpAddressSku": {
        "type": "String",
        "value": "Basic"
      },
      "publicIpAddressType": {
        "type": "String",
        "value": "Dynamic"
      },
      "subnetName": {
        "type": "String",
        "value": "default"
      },
      "subnets": {
        "type": "Array",
        "value": [
          {
            "name": "default",
            "properties": {
              "addressPrefix": "10.1.0.0/24"
            }
          }
        ]
      },
      "virtualMachineComputerName": {
        "type": "String",
        "value": "optimized-vm"
      },
      "virtualMachineName": {
        "type": "String",
        "value": "optimized-vm"
      },
      "virtualMachineRG": {
        "type": "String",
        "value": "optimized-vm-rg"
      },
      "virtualMachineSize": {
        "type": "String",
        "value": "Standard_DC2s_v2"
      },
      "virtualNetworkName": {
        "type": "String",
        "value": "optimized-vm-rg-vnet"
      }
    },
    "parametersLink": null,
    "providers": [
      {
        "id": null,
        "namespace": "Microsoft.Network",
        "providerAuthorizationConsentState": null,
        "registrationPolicy": null,
        "registrationState": null,
        "resourceTypes": [
          {
            "aliases": null,
            "apiProfiles": null,
            "apiVersions": null,
            "capabilities": null,
            "defaultApiVersion": null,
            "locationMappings": null,
            "locations": [
              "southeastasia"
            ],
            "properties": null,
            "resourceType": "networkInterfaces"
          },
          {
            "aliases": null,
            "apiProfiles": null,
            "apiVersions": null,
            "capabilities": null,
            "defaultApiVersion": null,
            "locationMappings": null,
            "locations": [
              "southeastasia"
            ],
            "properties": null,
            "resourceType": "networkSecurityGroups"
          },
          {
            "aliases": null,
            "apiProfiles": null,
            "apiVersions": null,
            "capabilities": null,
            "defaultApiVersion": null,
            "locationMappings": null,
            "locations": [
              "southeastasia"
            ],
            "properties": null,
            "resourceType": "virtualNetworks"
          },
          {
            "aliases": null,
            "apiProfiles": null,
            "apiVersions": null,
            "capabilities": null,
            "defaultApiVersion": null,
            "locationMappings": null,
            "locations": [
              "southeastasia"
            ],
            "properties": null,
            "resourceType": "publicIpAddresses"
          }
        ]
      },
      {
        "id": null,
        "namespace": "Microsoft.Compute",
        "providerAuthorizationConsentState": null,
        "registrationPolicy": null,
        "registrationState": null,
        "resourceTypes": [
          {
            "aliases": null,
            "apiProfiles": null,
            "apiVersions": null,
            "capabilities": null,
            "defaultApiVersion": null,
            "locationMappings": null,
            "locations": [
              "southeastasia"
            ],
            "properties": null,
            "resourceType": "virtualMachines"
          }
        ]
      },
      {
        "id": null,
        "namespace": "Microsoft.DevTestLab",
        "providerAuthorizationConsentState": null,
        "registrationPolicy": null,
        "registrationState": null,
        "resourceTypes": [
          {
            "aliases": null,
            "apiProfiles": null,
            "apiVersions": null,
            "capabilities": null,
            "defaultApiVersion": null,
            "locationMappings": null,
            "locations": [
              "southeastasia"
            ],
            "properties": null,
            "resourceType": "schedules"
          }
        ]
      }
    ],
    "provisioningState": "Succeeded",
    "templateHash": "5965842553202399222",
    "templateLink": null,
    "timestamp": "2022-03-06T01:17:32.167859+00:00",
    "validatedResources": null
  },
  "resourceGroup": "optimized-vm-rg",
  "tags": null,
  "type": "Microsoft.Resources/deployments"
}
bharathkumar@Azure:~/clouddrive/templates$ 