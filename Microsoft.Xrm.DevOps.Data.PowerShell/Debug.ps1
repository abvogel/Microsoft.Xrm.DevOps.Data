cd C:\git\Microsoft.Xrm.DevOps.Data\Microsoft.Xrm.DevOps.Data.PowerShell\bin\Debug;
Import-Module .\Microsoft.Xrm.DevOps.Data.PowerShell.dll -Force;
$Conn = Get-CrmConnection -Interactive;

$firstPackage = Get-CrmDataPackage -Conn $Conn -Fetches @("<fetch><entity name='account'><all-attributes/></entity></fetch>") -Verbose;
Get-CrmDataPackage -Conn $Conn -Fetches @("<fetch><entity name='contact'><all-attributes/></entity></fetch>", "<fetch><entity name='category'><all-attributes/></entity></fetch>") -Identifiers @{ "contact" = @("firstname", "lastname"); "category" = @("categoryid") } -DisablePlugins @{ "contact" = $true } -DisablePluginsGlobally $true -Verbose `
    | Add-CrmDataPackage -Conn $Conn -Fetches @("<fetch><entity name='knowledgearticle'><all-attributes/></entity></fetch>") `
    | Merge-CrmDataPackage -AdditionalPackage $firstPackage -Verbose `
    | Export-CrmDataPackage -ZipPath $env:USERPROFILE\Downloads\testrun.zip -Verbose