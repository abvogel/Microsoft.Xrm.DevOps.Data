using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Xml;

namespace Microsoft.Xrm.DevOps.Data.Tests
{
    public partial class SupportMethods
    {
        public static List<Entity> GetSingleEntity_AllPluginsDisabledEntity()
        {
            List<Entity> results = new List<Entity>();

            Entity entity1 = new Entity("account");
            entity1.Id = Guid.Parse("669537c3-a4bd-e711-a950-000d3a1087a0");
            entity1["name"] = "test";
            results.Add(entity1);

            return results;
        }

        public static String GetSingleEntity_AllPluginsDisabledFetch()
        {
            return "<fetch top='1'><entity name='account'><attribute name='name'/><attribute name='accountid'/></entity></fetch>";
        }

        public static String GetSingleEntity_AllPluginsDisabledExpectedData()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(@"../../lib/Configurations/SingleEntity_AllPluginsDisabled_data.xml");
            doc.FirstChild.Attributes.RemoveNamedItem("timestamp");
            return doc.InnerXml;
        }

        public static String GetSingleEntity_AllPluginsDisabledEntityExpectedSchema()
        {
            return LoadXmlFile(@"../../lib/Configurations/SingleEntity_AllPluginsDisabled_data_schema.xml");
        }
    }
}