# Ref: https://timw.info/akh

# Get the module and update help
Get-InstalledModule -Name AzureAD

Install-Module -Name AzureAD -Force -Verbose ; Update-Help -Force -ErrorAction SilentlyContinue

# Connect to AzureAD
Connect-AzureAD

# Get CSV content
$CSVrecords = Import-Csv C:\update2.csv -Delimiter ";"

# Create arrays for skipped and failed users
$SkippedUsers = @()
$FailedUsers = @()

# Loop trough CSV records
foreach ($CSVrecord in $CSVrecords) {
    $upn = $CSVrecord.UserPrincipalName
    $user = Get-AzureADUser -Filter "userPrincipalName eq '$upn'"
    if ($user) {
        try {
            $user | Set-AzureADUser -Department $CSVrecord.Department -TelephoneNumber $CSVrecord.TelephoneNumber
        }
        catch {
            $FailedUsers += $upn
            Write-Warning "$upn user found, but failed to update."
        }
    }
    else {
        Write-Warning "$upn not found - skipped."
        $SkippedUsers += $upn
    }
}

# Array skipped users
# $SkippedUsers

# Array failed users
# $FailedUsers