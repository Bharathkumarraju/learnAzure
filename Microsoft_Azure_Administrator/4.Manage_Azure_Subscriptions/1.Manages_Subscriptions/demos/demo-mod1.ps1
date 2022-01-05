# https://docs.microsoft.com/en-us/azure/azure-resource-manager/management/tag-resources

# Get tagged resources
Get-AzTag -Detailed

# Get resources by tag name
(Get-AzResource -TagName department).name

# Get resources by tag value
(Get-AzResource -TagValue ux).name

# Add an existing tag and non-existing tag to a resource with a tag already

$tags = @{'project' = 'ux'; 'location' = 'Dublin' }
$rg = Get-AzResourceGroup -Name 'demo-rg'
$rg.ResourceId
New-AzTag -ResourceId $rg.resourceid -Tag $tags


# Add tags to resources within a resource group
Get-AzResource -ResourceGroupName 'demo-rg' | Set-AzResource -Tag @{'environment' = 'staging' }

# Update existing tags on resources, operations (merge, replace, delete)
$tags = @{"environment" = "staging" }
$rg = Get-AzResourceGroup -Name 'demo-rg'
Update-AzTag -ResourceId $rg.ResourceId -Tag $tags -Operation Merge

