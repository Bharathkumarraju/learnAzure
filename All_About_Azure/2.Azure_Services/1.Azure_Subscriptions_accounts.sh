Azure subscription is a Logical Container -- Where as 
Account is an identity.



MacBook-Pro:learnAzure bharathdasaraju$ az account list | jq
[
  {
    "cloudName": "AzureCloud",
    "homeTenantId": "63e7f621-4866-4926-b111-c386f7c83c11",
    "id": "fda65227-e02e-4d3b-9ae4-f32fb9f280fd",
    "isDefault": false,
    "managedByTenants": [],
    "name": "Azure subscription 1",
    "state": "Enabled",
    "tenantId": "63e7f621-4866-4926-b111-c386f7c83c11",
    "user": {
      "name": "dbkraju@hotmail.com",
      "type": "user"
    }
  },
  {
    "cloudName": "AzureCloud",
    "homeTenantId": "63e7f621-4866-4926-b111-c386f7c83c11",
    "id": "6ced41d4-5c31-4325-b2c0-ac5cf2dac444",
    "isDefault": false,
    "managedByTenants": [],
    "name": "bharath-ai-sg-test-01",
    "state": "Enabled",
    "tenantId": "63e7f621-4866-4926-b111-c386f7c83c11",
    "user": {
      "name": "dbkraju@hotmail.com",
      "type": "user"
    }
  },
  {
    "cloudName": "AzureCloud",
    "homeTenantId": "eaf90161-b7b2-463f-bc2a-8ec64d391dca",
    "id": "18284a02-9053-4be7-b6e0-37610786ed7a",
    "isDefault": true,
    "managedByTenants": [],
    "name": "Azure subscription 1",
    "state": "Enabled",
    "tenantId": "eaf90161-b7b2-463f-bc2a-8ec64d391dca",
    "user": {
      "name": "bhrthdsra1@outlook.com",
      "type": "user"
    }
  },
  {
    "cloudName": "AzureCloud",
    "homeTenantId": "eaf90161-b7b2-463f-bc2a-8ec64d391dca",
    "id": "1cb67706-f0c0-4a7c-9940-4d9779fbce91",
    "isDefault": false,
    "managedByTenants": [],
    "name": "Bharath_Devlopment",
    "state": "Enabled",
    "tenantId": "eaf90161-b7b2-463f-bc2a-8ec64d391dca",
    "user": {
      "name": "bhrthdsra1@outlook.com",
      "type": "user"
    }
  }
]
MacBook-Pro:learnAzure bharathdasaraju$