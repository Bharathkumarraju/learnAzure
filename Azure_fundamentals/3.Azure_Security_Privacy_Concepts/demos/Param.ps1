{
  "$schema": "https://schema.management.azure.com/schemas/2015-01-01/deploymentParameters.json#",
  "contentVersion": "1.0.0.0",
  "parameters": {
      "adminLogin": {
        "value": "exampleadmin"
      },
      "adminPassword": {
        "reference": {
          "keyVault": {
          "id": "/subscriptions/<subscription-id>/resourceGroups/Globo-secure/providers/Microsoft.KeyVault/vaults/GloboVault1"
          },
          "secretName": "GLOBOPassword"
        }
      },
      "sqlServerName": {
        "value": "GloboSQL1"
      }
  }
}