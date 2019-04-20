using System;
using System.Collections.Generic;

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
