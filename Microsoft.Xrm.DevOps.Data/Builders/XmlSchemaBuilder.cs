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
            XmlDocument xd = null;
            var xns = new XmlSerializerNamespaces();
            xns.Add(string.Empty, string.Empty);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(SchemaXml.Entities));
            using (MemoryStream memStm = new MemoryStream())
            {
                using (XmlWriter writer = XmlWriter.Create(memStm, new XmlWriterSettings { OmitXmlDeclaration = true }))
                {
                    xmlSerializer.Serialize(writer, GenerateSchemaXml(entities), xns);

                    memStm.Position = 0;
                    var settings = new XmlReaderSettings
                    {
                        IgnoreWhitespace = true
                    };

                    using (var xtr = XmlReader.Create(memStm, settings))
                    {
                        xd = new XmlDocument();
                        xd.Load(xtr);
                    }
                }
            }

            return xd;
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

            foreach (var attribute in builderEntityMetadata.Attributes)
            {
                var AttributeMetadata = builderEntityMetadata.Metadata.Attributes.Where(a => a.LogicalName == attribute).First();
                fieldsNode.Field.Add(GenerateFieldNode(AttributeMetadata));
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
