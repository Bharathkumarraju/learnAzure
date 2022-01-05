https://docs.microsoft.com/en-us/powershell/module/az.resources/?view=azps-4.7.0#policy
https://docs.microsoft.com/en-us/azure/governance/policy/assign-policy-powershell

# Get Policy Assignments at Resource Group Level
$rg = Get-AzResourceGroup -Name 'demo-rg'
Get-AzPolicyAssignment -Scope $rg.ResourceId | select Name

# Create Policy

# Get Resource Group
$rg = Get-AzResourceGroup -Name 'demo-rg'

# Get a reference to the built-in policy definition to assign
$definition = Get-AzPolicyDefinition | Where-Object { $_.Properties.DisplayName -eq 'Audit VMs that do not use managed disks' }

# Create the policy assignment with the built-in definition against your resource group
New-AzPolicyAssignment -Name 'audit-vm-manageddisks' -DisplayName 'Audit VMs without managed disks Assignment' -Scope $rg.ResourceId -PolicyDefinition $definition

# Get the resources in your resource group that are non-compliant to the policy assignment
Get-AzPolicyState -ResourceGroupName $rg.ResourceGroupName -PolicyAssignmentName 'audit-vm-manageddisks' -Filter 'IsCompliant eq false'

# Manually initiate a policy scan
Start-AzPolicyComplianceScan




# Resource Locks

# Retrieve all locks in connected subscription
Get-AzResourceLock

# Create hashtable for splatting
$arguments = @{
    LockName          = 'dontdeleteme'
    LockLevel         = 'CanNotDelete'
    ResourceGroupName = 'demo-rg'
}

# Create new resource lock
New-AzResourceLock @arguments -Verbose -Force

# View new resource lock
Get-AzResourceLock

# Delete resource locks (warning, this will delete all locks in subscription)
Get-AzResourceLock | Remove-AzResourceLock


# List resource groups
Get-AzResourceGroup | select ResourceGroupName, Location, Tags

# Create a resource group
New-AzResourceGroup -Name demoResourceGroup -Location centralus

# Delete resource groups
Get-AzResourceGroup -Name 'demoResourceGroup' | Remove-AzResourceGroup
