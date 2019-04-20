using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.DevOps.Data;
using Microsoft.Xrm.Tooling.Connector;

namespace Microsoft.Xrm.DevOps.Data.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.Write("Connecting to CRM...");
            CrmServiceClient crmSvc = new CrmServiceClient(ConfigurationManager.ConnectionStrings["CRMConnection"].ConnectionString);
            System.Console.Write("Initializing DataBuilder class...");

            //DataBuilder db = new DataBuilder
            //{
            //    service = crmSvc
            //};

            //db.AppendData("<fetch><entity name='contact'><attribute name='contactid' /></entity></fetch>");

            //System.Xml.XmlDocument test = db.BuildDataXML();

            //System.Console.Write(test.InnerXml);
        }
    }
}
