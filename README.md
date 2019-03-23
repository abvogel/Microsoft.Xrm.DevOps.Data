# Microsoft.Xrm.DevOps.Data
This module builds on Microsoft.Xrm.Data.PowerShell and Microsoft.CrmSdk.XrmTooling.PackageDeployment to provide common functions to replace the Configuration Data Migration Tool.

# Microsoft.Xrm.DevOps.PowerShell
This module builds on the DevOps.Data DLL providing a full PowerShell wrapper for DevOps related Dynamics 365 tasks.
https://github.com/abvogel/Microsoft.Xrm.DevOps.PowerShell/

# Example Code
PowerShell: DataBuilder.AddData($fetchCRMResults.CrmRecords[0].logicalname, $fetchCRMResults.CrmRecords.original);
C#: DataBuilder.AddData(EntityCollectionRecords);
