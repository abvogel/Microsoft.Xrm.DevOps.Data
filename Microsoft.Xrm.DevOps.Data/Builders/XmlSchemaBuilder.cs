using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Xrm.DevOps.Data.SchemaXml;
using Microsoft.Xrm.Sdk.Metadata;

namespace Microsoft.Xrm.DevOps.Data
{
    public class XmlSchemaBuilder
    {
        internal static XmlDocument ToXmlDocument(Dictionary<string, BuilderEntityMetadata> entities, Boolean pluginsdisabled)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(String.Empty.GetType());
            using (StringWriter textWriter = new StringWriter())
            {
                xmlSerializer.Serialize(textWriter, GenerateSchemaXml(entities));
                //results["data_schema.xml"] = textWriter.ToString();
            }

            return new XmlDocument();
        }

        private static SchemaXml.Entities GenerateSchemaXml(Dictionary<string, BuilderEntityMetadata> entities)
        {
            SchemaXml.Entities entitiesNode = new SchemaXml.Entities
            {
                Entity = new List<SchemaXml.Entity>()
            };

            foreach (var logicalname in entities.Keys)
            {
                entitiesNode.Entity.Add(GenerateEntityNode(logicalname, entities[logicalname]));
            }

            return entitiesNode;
        }

        private static Entity GenerateEntityNode(string logicalName, BuilderEntityMetadata builderEntityMetadata)
        {
            SchemaXml.Fields fieldsNode = new SchemaXml.Fields()
            {
                Field = new List<SchemaXml.Field>()
            };

            SchemaXml.Entity entityNode = new SchemaXml.Entity()
            {
                Name = logicalName,
                Displayname = builderEntityMetadata.Metadata.DisplayName.LocalizedLabels[0].Label,
                Etc = builderEntityMetadata.Metadata.ObjectTypeCode.ToString(),
                Primaryidfield = builderEntityMetadata.Metadata.PrimaryIdAttribute,
                Primarynamefield = builderEntityMetadata.Metadata.PrimaryNameAttribute,
                Disableplugins = "false",
                Fields = fieldsNode
            };

            foreach (var attribute in builderEntityMetadata.Metadata.Attributes)
            {
                fieldsNode.Field.Add(GenerateFieldNode(attribute));
            }

            return entityNode;
        }

        private static Field GenerateFieldNode(AttributeMetadata attribute)
        {
            SchemaXml.Field fieldNode = new SchemaXml.Field()
            {
                Displayname = attribute.DisplayName.LocalizedLabels[0].Label,
                Name = attribute.LogicalName,
                Type = attribute.AttributeType.ToString()
            };

            return fieldNode;
        }
    }
}
