using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Xml;

namespace Microsoft.Xrm.DevOps.Data.Tests
{
    public partial class SupportMethods
    {
        public static List<Entity> GetMultiValueIdentifierEntity()
        {
            List<Entity> results = new List<Entity>();

            Entity entity1 = new Entity("contact");
            entity1.Id = Guid.Parse("00b9c27b-ebdf-e711-a954-000d3a109495");
            entity1["firstname"] = "SomeFirst";
            entity1["fullname"] = "SomeFirst AndLast";
            entity1["lastname"] = "AndLast";
            entity1["birthdate"] = DateTime.Parse("11/9/1988 12:00:00 AM");
            results.Add(entity1);

            Entity entity2 = new Entity("contact");
            entity2.Id = Guid.Parse("9a913e9f-cbb8-e711-a968-000d3a192387");
            entity2["firstname"] = "Andrew";
            entity2["fullname"] = "Andrew Vogel";
            entity2["lastname"] = "Vogel";
            entity2["emailaddress1"] = "andrew.vogel@fakeemail.com";
            entity1["birthdate"] = DateTime.Parse("3/2/1977 12:00:00 AM");
            results.Add(entity2);

            return results;
        }

        public static String GetMultiValueIdentifierFetch()
        {
            return "<fetch top='2'><entity name='contact'><attribute name='fullname'/><attribute name='birthdate'/><attribute name='emailaddress1'/><attribute name='lastname'/><attribute name='firstname'/><order attribute='lastname'/></entity></fetch>";
        }

        public static String GetMultiValueIdentifierExpectedData()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(@"../../lib/Configurations/MultiValueIdentifierSchema_data.xml");
            doc.FirstChild.Attributes.RemoveNamedItem("timestamp");
            return doc.InnerXml;
        }

        public static String GetMultiValueIdentifierEntityExpectedSchema()
        {
            return LoadXmlFile(@"../../lib/Configurations/MultiValueIdentifierSchema_data_schema.xml");
        }
    }
}