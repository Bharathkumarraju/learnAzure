Virtual Machines uses virtual hard disks(VHDs) for operating system and data.

Must consider design considerations such as availability and scalability


Resources used by VMs
1. Resources Group    ---> Yes
2. Storage Account    ---> Yes
3. Virtual Network    ---> Yes 
4. Public IP          ---> No , Can have a PIP assigned to access remotely
5. Network Interface  ---> Yes, The VM needs a NIC to communicate
6. Data Disks         ---> No, Can include data disks to expand storage.

az vm create --resource-Group BkVMRG --name windows1 --image win2016datacenter --admin-username bharath


MacBook-Pro:external bharathdasaraju$ az vm create --resource-group BkVMRG --name windows1 --image win2016datacenter --admin-username bharath
Admin Password:
Confirm Admin Password:
It is recommended to use parameter "--public-ip-sku Standard" to create new VM with Standard public IP. Please note that the default public IP used for VM creation will be changed from Basic to Standard in the future.
{
  "fqdns": "",
  "id": "/subscriptions/fda65227-e02e-4d3b-9ae4-f32fb9f280fd/resourceGroups/BkVMRG/providers/Microsoft.Compute/virtualMachines/windows1",
  "location": "eastus",
  "macAddress": "00-0D-3A-1B-FD-8D",
  "powerState": "VM running",
  "privateIpAddress": "10.0.0.4",
  "publicIpAddress": "20.127.69.12",
  "resourceGroup": "BkVMRG",
  "zones": ""
}
MacBook-Pro:external bharathdasaraju$


MacBook-Pro:external bharathdasaraju$ az vm delete -g BkVMRG -n windows1 --yes
MacBook-Pro:external bharathdasaraju$
