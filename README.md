
[![Build status](https://ci.appveyor.com/api/projects/status/xr9wvx7f38wgblnn?svg=true)](https://ci.appveyor.com/project/abvogel/microsoft-xrm-devops-data)

# Microsoft.Xrm.DevOps.Data
This library provides an easy way to generate **filtered** data compatible with the Configuration Data Migration Tool. These zip files can be used to push specific records between Dynamics 365 environments using the Dynamics 365 Package Deployer.

# Microsoft.Xrm.DevOps.Data.PowerShell
This wrapper uses the Microsoft.Xrm.DevOps.Data library providing a simple PowerShell interface.

## Installing
    Install-Module Microsoft.Xrm.DevOps.Data.PowerShell
    https://www.powershellgallery.com/packages/Microsoft.Xrm.DevOps.Data.PowerShell/1.0.1

## Example Code
    $Conn = Get-CrmConnection -Interactive
    $build = Get-CrmDataBuilder;
    $build.FetchCollection = @("<fetch><entity name='category'><all-attributes /></entity></fetch>");
    Export-CrmDataBuilder -Conn $Conn -Builder $build -ZipPath c:\data.zip
## Other Features
### Per entity custom identifiers
    $build.Identifiers["category"] = @("categoryid"); # default
    $build.Identifiers["contact"] = @("firstname", "lastname", "birthdate");
### Per entity capability to disable plugins
    $build.DisablePlugins["contact"] = $false; # default
    $build.DisablePlugins["category"] = $true;
### Global ability to disable plugins for all imported entities
    $build.DisablePluginsGlobally = $false; # default
    $build.DisablePluginsGlobally = $true;
