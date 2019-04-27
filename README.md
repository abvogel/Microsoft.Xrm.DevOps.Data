
[![Build Status](https://dev.azure.com/MicrosoftXrmDevOpsData/Microsoft.Xrm.DevOps.Data/_apis/build/status/abvogel.Microsoft.Xrm.DevOps.Data?branchName=master)](https://dev.azure.com/MicrosoftXrmDevOpsData/Microsoft.Xrm.DevOps.Data/_build/latest?definitionId=1&branchName=master)

# Microsoft.Xrm.DevOps.Data
This library provides an easy way to generate **filtered** data compatible with the Configuration Data Migration Tool. These zip files can be used to push specific records between Dynamics 365 environments using the Dynamics 365 Package Deployer.

# Microsoft.Xrm.DevOps.Data.PowerShell
This wrapper uses the Microsoft.Xrm.DevOps.Data library providing a simple PowerShell interface.

## Installing
The PowerShell module has been posted to PowerShell Gallery. Install using their standard commands - 
https://www.powershellgallery.com/packages/Microsoft.Xrm.DevOps.Data.PowerShell/

    Install-Module -Name Microsoft.Xrm.DevOps.Data.PowerShell

## Example Code
    $Conn = Get-CrmConnection -Interactive
    $anotherPackage = Get-CrmDataPackage -Conn $Conn -Fetches @("<fetch><entity name='account'><all-attributes/></entity></fetch>");
    Get-CrmDataPackage -Conn $Conn -Fetches `
        @("<fetch><entity name='contact'><all-attributes/></entity></fetch>", "<fetch><entity name='category'><all-attributes/></entity></fetch>") `
        | Add-CrmDataPackage -Conn $Conn -Fetches @("<fetch><entity name='knowledgearticle'><all-attributes/></entity></fetch>") `
        | Merge-CrmDataPackage -AdditionalPackage $anotherPackage `
        | Export-CrmDataPackage -ZipPath $env:USERPROFILE\Downloads\testrun.zip
    
    Get-CrmDataPackage -Conn $Conn -Fetches @("<fetch><entity name='contact'><all-attributes/></entity></fetch>") -Identifiers @{ "contact" = @("firstname", "lastname", "birthdate") } -DisablePluginsGlobally $true | Export-CrmDataPackage -ZipPath "$env:USERPROFILE\Desktop\contacts.zip";
## Commands
### Get-CrmDataPackage
    Returns object CrmDataPackage used by this module
    -Fetches takes an array of fetches 
        e.g. @($fetch1, $fetch2);
    -Identifiers Dictionary<String, String[]>
        Default is the primarykey
        e.g. @{ "contact" = @("firstname", "lastname", "birthdate") };
    -DisablePlugins Dictionary<String, Boolean>
        Default is false
        e.g. @{ "contact" = $false };
    -DisablePluginsGlobally Boolean
        e.g. $false;

### Add-CrmDataPackage
    Returns object CrmDataPackage
    Same as Get-CrmDataPackage except it can take a CrmDataPackage via pipe, or input Package

### Merge-CrmDataPackage
    Returns object CrmDataPackage
    Takes two CrmDataPackages as inputs. Values in the second package override the first.

### Export-CrmDataPackage
    Returns nothing.
    -Package takes a CrmDataPackage
    -ZipPath takes a "$env:USERPROFILE\Downloads\thedata.zip"

### CrmDataPackage
    Contains three XmlDocuments representing the three XML files that go in a Configuration Migration Data Package.
    -ContentTypes
    -Data
    -Schema
