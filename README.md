[![Build Status](https://dev.azure.com/MicrosoftXrmDevOps/Microsoft.Xrm.DevOps.Data/_apis/build/status/abvogel.Microsoft.Xrm.DevOps.Data?branchName=master)](https://dev.azure.com/MicrosoftXrmDevOps/Microsoft.Xrm.DevOps.Data/_build/latest?definitionId=1&branchName=master)

# Microsoft.Xrm.DevOps.Data
This library provides an easy way to generate **filtered** data compatible with the Configuration Data Migration Tool. These zip files can be used to push specific records between Dynamics 365 environments using the Dynamics 365 Package Deployer.

* 118 unit tests providing 100% code coverage. Since launch, test driven development has resulted in 0 known bugs and allowed new features to be added with high confidence.
Driven by FetchXML which supports all CRM importable field types, including many to many relationships and rarely used CRM types (e.g. double). Because it uses FetchXML, filtered queries can be generated using CRM Advanced Find, or XrmToolbox's Fetch Builder for easy setup and verification.
* Native commands to generate, extend, merge, and subtract data packages from a saved file or live system. This provides the capability of comparing environments as well as generating delta packages which substantially reduces deployment risk and time.
* Is 100% compatible with the Microsoft Configuration Migration Data Deployer generated XML files and import/export commands. Unit tests often compare library results against real Microsoft results verifying not just schema compatibility but format equivalence.
* Extended capabilities by having default values set where users often experience problems. This means records will by default be compared against records of the same guid, not the name field, and that plugins will by default be disabled for releases. These can be changed as necessary, but substantially reduce testing and deployment issues.
* Behind the scenes algorithms that keep your entities, fields, and values ordered the same between runs. Checking data changes into source control and comparing results now makes sense as the delta is an actual delta between environments, not output formats.

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
        | Add-FetchesToCrmDataPackage -Conn $Conn -Fetches @("<fetch><entity name='knowledgearticle'><all-attributes/></entity></fetch>") `
        | Merge-CrmDataPackage -AdditionalPackage $anotherPackage `
        | Remove-CrmDataPackage -RemovePackage $anotherPackage `
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

### Add-FetchesCrmDataPackage
    Returns object CrmDataPackage
    Takes a CrmDataPackage input from pipeline or variable as well as the inputs Get-CrmDataPackage takes.

### Merge-CrmDataPackage
    Returns object CrmDataPackage
    Takes two CrmDataPackages as inputs. Values in the second package override the first.

### Export-CrmDataPackage
    Returns nothing.
    -Package takes a CrmDataPackage
    -ZipPath takes a path to the zip file where data will be saved.
    
### Import-CrmDataPackage
    Returns object CrmDataPackage
    -ZipPath takes a path to the zip file that will be imported.
    
### Remove-CrmDataPackage
    Returns object CrmDataPackage
    -SourcePackage is the package data will be removed from.
    -RemovePackage is the package that will be removed. Identical attributes are first removed, and if an entity is empty it will then also be removed.

### CrmDataPackage
    Contains three XmlDocuments representing the three XML files that go in a Configuration Migration Data Package.
    -ContentTypes
    -Data
    -Schema
