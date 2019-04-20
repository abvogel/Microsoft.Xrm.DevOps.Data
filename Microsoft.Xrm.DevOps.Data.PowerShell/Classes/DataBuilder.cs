using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Xrm.DevOps.Data.PowerShell
{
    public class DataBuilder
    {
        public String[] FetchCollection { get; set; } = new String[0];
        public Dictionary<String, String[]> Identifiers { get; set; } = new Dictionary<String, String[]>();
        public Dictionary<String, Boolean> DisablePlugins { get; set; } = new Dictionary<String, Boolean>();
        public Boolean DisablePluginsGlobally { get; set; }
    }
}
