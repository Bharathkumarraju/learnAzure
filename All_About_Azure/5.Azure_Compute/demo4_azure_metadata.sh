It is a REST API that is accessible from the VM.

Providing a lot of info about the machine 
info includes:
 1. SKU
 2. storage
 3. networking
 4. scheduled events

Accessible ONLY from the VM.

http://169.254.169.254/metadata/instance?api-version=2020-06-01


{
    "compute": {
        "azEnvironment": "AzurePublicCloud",
        "customData": "",
        "isHostCompatibilityLayerVm": "false",
        "location": "southeastasia",
        "name": "mds-demo-vm",
        "offer": "WindowsServer",
        "osType": "Windows",
        "placementGroupId": "",
        "plan": {
            "name": "",
            "product": "",
            "publisher": ""
        },
        "platformFaultDomain": "0",
        "platformUpdateDomain": "0",
        "provider": "Microsoft.Compute",
        "publicKeys": [],
        "publisher": "MicrosoftWindowsServer",
        "resourceGroupName": "mds-rg",
        "resourceId": "/subscriptions/1cb67706-f0c0-4a7c-9940-4d9779fbce91/resourceGroups/mds-rg/providers/Microsoft.Compute/virtualMachines/mds-demo-vm",
        "securityProfile": {
            "secureBootEnabled": "false",
            "virtualTpmEnabled": "false"
        },
        "sku": "2022-datacenter-azure-edition",
        "storageProfile": {
            "dataDisks": [],
            "imageReference": {
                "id": "",
                "offer": "WindowsServer",
                "publisher": "MicrosoftWindowsServer",
                "sku": "2022-datacenter-azure-edition",
                "version": "latest"
            },
            "osDisk": {
                "caching": "ReadWrite",
                "createOption": "FromImage",
                "diffDiskSettings": {
                    "option": ""
                },
                "diskSizeGB": "127",
                "encryptionSettings": {
                    "enabled": "false"
                },
                "image": {
                    "uri": ""
                },
                "managedDisk": {
                    "id": "/subscriptions/1cb67706-f0c0-4a7c-9940-4d9779fbce91/resourceGroups/mds-rg/providers/Microsoft.Compute/disks/mds-demo-vm_OsDisk_1_72af4dd78091444c924b87b1f7aeb51e",
                    "storageAccountType": "Premium_LRS"
                },
                "name": "mds-demo-vm_OsDisk_1_72af4dd78091444c924b87b1f7aeb51e",
                "osType": "Windows",
                "vhd": {
                    "uri": ""
                },
                "writeAcceleratorEnabled": "false"
            }
        },
        "subscriptionId": "1cb67706-f0c0-4a7c-9940-4d9779fbce91",
        "tags": "",
        "tagsList": [],
        "version": "20348.524.220201",
        "vmId": "330e9f36-64b3-4f79-b017-50ed1047efa1",
        "vmScaleSetName": "",
        "vmSize": "Standard_DC2s_v2",
        "zone": ""
    },
    "network": {
        "interface": [
            {
                "ipv4": {
                    "ipAddress": [
                        {
                            "privateIpAddress": "10.1.0.4",
                            "publicIpAddress": "20.212.110.219"
                        }
                    ],
                    "subnet": [
                        {
                            "address": "10.1.0.0",
                            "prefix": "24"
                        }
                    ]
                },
                "ipv6": {
                    "ipAddress": []
                },
                "macAddress": "000D3AA2184E"
            }
        ]
    }
}



http://169.254.169.254/metadata/scheduledevents?api-version=2019-08-01

{
    "DocumentIncarnation": 0,
    "Events": []
}



