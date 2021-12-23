# Azure Resource Manager Templates
Written in JavaScript Object Notation(JSON).

Defines infrastructure and configuration for azure resources.

Declarative syntax

Deployment:
------------------------>
1. Using azure pipelines(CI/CD)
2. From Github
3. Using powershell and the azure cli 
4. Resource Manager REST API 
5. Using the Azure Portal 


Get the json from existing appservice plan and webapp deployed 


MacBook-Pro:external bharathdasaraju$ az resource list --resource-group BharathTestRG --out table --query "[].{name:name, Type:type}"
Name              Type
----------------  ---------------------------------
bharathstorage    Microsoft.Storage/storageAccounts
TestAppSvcPlan    Microsoft.Web/serverFarms
bharathwebappcli  Microsoft.Web/sites
MacBook-Pro:external bharathdasaraju$

export the resource manager templates from above resources.

