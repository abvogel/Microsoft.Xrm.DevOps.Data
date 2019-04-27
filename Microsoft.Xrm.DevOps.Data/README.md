[![Build status](https://ci.appveyor.com/api/projects/status/xr9wvx7f38wgblnn?svg=true)](https://ci.appveyor.com/project/abvogel/microsoft-xrm-devops-data)

# Microsoft.Xrm.DevOps.Data
This library provides an easy way to generate **filtered** data compatible with the Configuration Data Migration Tool. These zip files can be used to push specific records between Dynamics 365 environments using the Dynamics 365 Package Deployer.

Currently **all** exportable data types are supported and unit tested. m2m relationships can be included by providing a fetch that has an attribute from the linked entity.
