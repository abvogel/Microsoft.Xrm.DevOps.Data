using System;
using System.Collections.Generic;

namespace Microsoft.Xrm.DevOps.Data.PowerShell
{
    public class CrmDataPackage
    {
        public System.Xml.XmlDocument ContentTypes { get; set; }
        public System.Xml.XmlDocument Data { get; set; }
        public System.Xml.XmlDocument Schema { get; set; }
    }
}
