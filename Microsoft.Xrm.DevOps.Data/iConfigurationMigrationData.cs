using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Xrm.DevOps.Data
{
    class ConfigurationMigrationData
    {
        Microsoft.Xrm.DevOps.Data.XMLSchema.Entities Data_Schema
        {
            get; set;
        }

        Microsoft.Xrm.DevOps.Data.XMLData.Entities Data
        {
            get; set;
        }
    }
}
