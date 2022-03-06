MacBook-Pro:cart bharathdasaraju$ az aks create --resource-group readit-app-rg --name bkcart-aks --node-count 2 --generate-ssh-keys --attach-acr bharathreaditacr --node-vm-size Standard_DS2_v2 --location eastasia
AAD role propagation done[############################################]  100.0000%{
  "aadProfile": null,
  "addonProfiles": null,
  "agentPoolProfiles": [
    {
      "availabilityZones": null,
      "count": 2,
      "enableAutoScaling": false,
      "enableEncryptionAtHost": false,
      "enableFips": false,
      "enableNodePublicIp": false,
      "enableUltraSsd": false,
      "gpuInstanceProfile": null,
      "kubeletConfig": null,
      "kubeletDiskType": "OS",
      "linuxOsConfig": null,
      "maxCount": null,
      "maxPods": 110,
      "minCount": null,
      "mode": "System",
      "name": "nodepool1",
      "nodeImageVersion": "AKSUbuntu-1804gen2containerd-2022.02.15",
      "nodeLabels": null,
      "nodePublicIpPrefixId": null,
      "nodeTaints": null,
      "orchestratorVersion": "1.21.9",
      "osDiskSizeGb": 128,
      "osDiskType": "Managed",
      "osSku": "Ubuntu",
      "osType": "Linux",
      "podSubnetId": null,
      "powerState": {
        "code": "Running"
      },
      "provisioningState": "Succeeded",
      "proximityPlacementGroupId": null,
      "scaleDownMode": null,
      "scaleSetEvictionPolicy": null,
      "scaleSetPriority": null,
      "spotMaxPrice": null,
      "tags": null,
      "type": "VirtualMachineScaleSets",
      "upgradeSettings": null,
      "vmSize": "Standard_DS2_v2",
      "vnetSubnetId": null
    }
  ],
  "apiServerAccessProfile": null,
  "autoScalerProfile": null,
  "autoUpgradeProfile": null,
  "azurePortalFqdn": "bkcart-aks-readit-app-rg-1cb677-5d0972b4.portal.hcp.eastasia.azmk8s.io",
  "disableLocalAccounts": false,
  "diskEncryptionSetId": null,
  "dnsPrefix": "bkcart-aks-readit-app-rg-1cb677",
  "enablePodSecurityPolicy": null,
  "enableRbac": true,
  "extendedLocation": null,
  "fqdn": "bkcart-aks-readit-app-rg-1cb677-5d0972b4.hcp.eastasia.azmk8s.io",
  "fqdnSubdomain": null,
  "httpProxyConfig": null,
  "id": "/subscriptions/1cb67706-f0c0-4a7c-9940-4d9779fbce91/resourcegroups/readit-app-rg/providers/Microsoft.ContainerService/managedClusters/bkcart-aks",
  "identity": {
    "principalId": "0d9fdf84-c240-4071-94b9-0d06430d4951",
    "tenantId": "eaf90161-b7b2-463f-bc2a-8ec64d391dca",
    "type": "SystemAssigned",
    "userAssignedIdentities": null
  },
  "identityProfile": {
    "kubeletidentity": {
      "clientId": "750ac9ab-d8d1-4a3a-ad0b-606c244751d4",
      "objectId": "526147e8-af0a-4526-9aaa-f0a3acf61665",
      "resourceId": "/subscriptions/1cb67706-f0c0-4a7c-9940-4d9779fbce91/resourcegroups/MC_readit-app-rg_bkcart-aks_eastasia/providers/Microsoft.ManagedIdentity/userAssignedIdentities/bkcart-aks-agentpool"
    }
  },
  "kubernetesVersion": "1.21.9",
  "linuxProfile": {
    "adminUsername": "azureuser",
    "ssh": {
      "publicKeys": [
        {
          "keyData": "ssh-rsa AAAAB3NzaC1yc2EAAAADAQABAAABAQDWxbYYi/mGidrVN0zVSzwJ16RU0WtxA360KTVpUoGqkGxZaAsjEA76MBcUyKpFl3vaoVZI91n5jVO9i8aZCAf0GHwX5DbvZZ65FUZA1MZJXzv2HHU27Ym/api108rKzZr2A87SzSlQrdO8ILUcVbAn5JrzQEEFMMjPdGY8G8e9CsvIPPfwpkyAlCW908BCK1aU9YVqpFbh+Q0Kgcaf2ArKqwl8uO28NB9O6kmvdZcjmlJ962+Vku5CMyNIk697V1wMpEYcyGSHZQ2Q0tF3ElY7X10pEwUbU60jJqFuDn6BvTCcBHx9l8S2YN/eWaLHIhn48eGR2iy0LeHxcBhxW+yR bharathdasaraju@Bharaths-MacBook-Pro.local\n"
        }
      ]
    }
  },
  "location": "eastasia",
  "maxAgentPools": 100,
  "name": "bkcart-aks",
  "networkProfile": {
    "dnsServiceIp": "10.0.0.10",
    "dockerBridgeCidr": "172.17.0.1/16",
    "loadBalancerProfile": {
      "allocatedOutboundPorts": null,
      "effectiveOutboundIPs": [
        {
          "id": "/subscriptions/1cb67706-f0c0-4a7c-9940-4d9779fbce91/resourceGroups/MC_readit-app-rg_bkcart-aks_eastasia/providers/Microsoft.Network/publicIPAddresses/a118938f-d06e-4c7e-9464-f615c80432a5",
          "resourceGroup": "MC_readit-app-rg_bkcart-aks_eastasia"
        }
      ],
      "idleTimeoutInMinutes": null,
      "managedOutboundIPs": {
        "count": 1
      },
      "outboundIPs": null,
      "outboundIpPrefixes": null
    },
    "loadBalancerSku": "Standard",
    "natGatewayProfile": null,
    "networkMode": null,
    "networkPlugin": "kubenet",
    "networkPolicy": null,
    "outboundType": "loadBalancer",
    "podCidr": "10.244.0.0/16",
    "serviceCidr": "10.0.0.0/16"
  },
  "nodeResourceGroup": "MC_readit-app-rg_bkcart-aks_eastasia",
  "podIdentityProfile": null,
  "powerState": {
    "code": "Running"
  },
  "privateFqdn": null,
  "privateLinkResources": null,
  "provisioningState": "Succeeded",
  "resourceGroup": "readit-app-rg",
  "securityProfile": null,
  "servicePrincipalProfile": {
    "clientId": "msi",
    "secret": null
  },
  "sku": {
    "name": "Basic",
    "tier": "Free"
  },
  "tags": null,
  "type": "Microsoft.ContainerService/ManagedClusters",
  "windowsProfile": null
}
MacBook-Pro:cart bharathdasaraju$ 