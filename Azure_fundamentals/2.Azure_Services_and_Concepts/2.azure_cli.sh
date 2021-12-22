MacBook-Pro:external bharathdasaraju$ az login
The default web browser has been opened at https://login.microsoftonline.com/organizations/oauth2/v2.0/authorize. Please continue the login in the web browser. If no web browser is available or if the web browser fails to open, use device code flow with `az login --use-device-code`.
[
  {
    "cloudName": "AzureCloud",
    "homeTenantId": "63e7f621-4866-4926-b111-c386f7c83c11",
    "id": "fda65227-e02e-4d3b-9ae4-f32fb9f280fd",
    "isDefault": true,
    "managedByTenants": [],
    "name": "Azure subscription 1",
    "state": "Enabled",
    "tenantId": "63e7f621-4866-4926-b111-c386f7c83c11",
    "user": {
      "name": "dbkraju@hotmail.com",
      "type": "user"
    }
  }
]
MacBook-Pro:external bharathdasaraju$ az account list | jq
[
  {
    "cloudName": "AzureCloud",
    "homeTenantId": "63e7f621-4866-4926-b111-c386f7c83c11",
    "id": "fda65227-e02e-4d3b-9ae4-f32fb9f280fd",
    "isDefault": true,
    "managedByTenants": [],
    "name": "Azure subscription 1",
    "state": "Enabled",
    "tenantId": "63e7f621-4866-4926-b111-c386f7c83c11",
    "user": {
      "name": "dbkraju@hotmail.com",
      "type": "user"
    }
  }
]
MacBook-Pro:external bharathdasaraju$ az account list | jq .[]
{
  "cloudName": "AzureCloud",
  "homeTenantId": "63e7f621-4866-4926-b111-c386f7c83c11",
  "id": "fda65227-e02e-4d3b-9ae4-f32fb9f280fd",
  "isDefault": true,
  "managedByTenants": [],
  "name": "Azure subscription 1",
  "state": "Enabled",
  "tenantId": "63e7f621-4866-4926-b111-c386f7c83c11",
  "user": {
    "name": "dbkraju@hotmail.com",
    "type": "user"
  }
}
MacBook-Pro:external bharathdasaraju$ az account list | jq .[].name
"Azure subscription 1"
MacBook-Pro:external bharathdasaraju$
