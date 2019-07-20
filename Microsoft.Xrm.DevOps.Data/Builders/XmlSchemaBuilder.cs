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
    internal class XmlSchemaBuilder
    {
        internal static XmlDocument ToXmlDocument(Dictionary<string, BuilderEntityMetadata> entities)
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

            entitiesNode.Entity.Sort((x, y) => string.Compare(x.Name, y.Name));

            return entitiesNode;
        }

        private static Entity GenerateEntityNode(string logicalName, BuilderEntityMetadata builderEntityMetadata)
        {
            SchemaXml.Entity entityNode = new SchemaXml.Entity()
            {
                Name = logicalName,
                Displayname = builderEntityMetadata.Metadata.DisplayName.LocalizedLabels[0].Label,
                Etc = builderEntityMetadata.Metadata.ObjectTypeCode.ToString(),
                Primaryidfield = builderEntityMetadata.Metadata.PrimaryIdAttribute,
                Primarynamefield = builderEntityMetadata.Metadata.PrimaryNameAttribute,
                Disableplugins = (builderEntityMetadata.PluginsDisabled == null
                                   || builderEntityMetadata.PluginsDisabled == false ? "false" : "true"),
                Fields = new SchemaXml.Fields()
                {
                    Field = new List<SchemaXml.Field>()
                }
            };

            foreach (var attribute in builderEntityMetadata.Attributes)
            {
                var AttributeMetadata = builderEntityMetadata.Metadata.Attributes.Where(a => a.LogicalName == attribute).First();

                if (!IsSupportedAttributeType(AttributeMetadata.AttributeType))
                    continue;

                SchemaXml.Field field = new SchemaXml.Field()
                {
                    Displayname = AttributeMetadata.DisplayName.LocalizedLabels[0].Label,
                    Name = AttributeMetadata.LogicalName,
                    Type = GetFieldNodeType(AttributeMetadata),
                    LookupType = GetFieldNodeLookupType(AttributeMetadata),
                    UpdateCompare = (builderEntityMetadata.Identifiers.Contains(AttributeMetadata.LogicalName) ? "true" : null),
                    PrimaryKey = (entityNode.Primaryidfield == AttributeMetadata.LogicalName ? "true" : null),
                    Customfield = (AttributeMetadata.IsCustomAttribute == true ? "true" : null)
                };

                entityNode.Fields.Field.Add(field);
            }

            entityNode.Fields.Field.Sort((x, y) => string.Compare(x.Name, y.Name));

            if (builderEntityMetadata.RelatedEntities.Count > 0)
            {
                entityNode.Relationships = new SchemaXml.Relationships()
                {
                    Relationship = new List<SchemaXml.Relationship>()
                };

                foreach (var relationshipname in builderEntityMetadata.RelatedEntities.Keys)
                {
                    var relationshipMetadata = builderEntityMetadata.Metadata.ManyToManyRelationships.Where(x => x.IntersectEntityName == relationshipname).First();
                    
                    string targetPrimaryKey = relationshipMetadata.Entity2IntersectAttribute;
                    if (relationshipMetadata.Entity1LogicalName == relationshipMetadata.Entity2LogicalName)
                    {
                        targetPrimaryKey = relationshipMetadata.Entity2IntersectAttribute.Substring(0, relationshipMetadata.Entity2IntersectAttribute.Length - 3);
                    }

                    entityNode.Relationships.Relationship.Add(new SchemaXml.Relationship()
                    {
                        Name = relationshipname,
                        ManyToMany = "true",
                        Isreflexive = (relationshipMetadata.Entity1LogicalName == relationshipMetadata.Entity2LogicalName).ToString().ToLower(),
                        RelatedEntityName = relationshipMetadata.IntersectEntityName,
                        M2mTargetEntity = relationshipMetadata.Entity2LogicalName,
                        M2mTargetEntityPrimaryKey = targetPrimaryKey
                    });

                }
            }

            return entityNode;
        }

        private static bool IsSupportedAttributeType(AttributeTypeCode? attributeType)
        {
            switch (attributeType)
            {
                case AttributeTypeCode.CalendarRules:
                case AttributeTypeCode.Virtual:
                case AttributeTypeCode.BigInt:
                case AttributeTypeCode.ManagedProperty:
                case AttributeTypeCode.EntityName:
                    return false;
                default:
                    return true;
            }
        }

        private static String GetFieldNodeType(AttributeMetadata attribute)
        {
            switch (attribute.AttributeType)
            {
                case AttributeTypeCode.Boolean:
                    return "bool";
                case AttributeTypeCode.Customer:
                case AttributeTypeCode.Lookup:
                    return "entityreference";
                case AttributeTypeCode.DateTime:
                    return "datetime";
                case AttributeTypeCode.Decimal:
                    return "decimal";
                case AttributeTypeCode.Double:
                    return "float";
                case AttributeTypeCode.Integer:
                    return "number";
                case AttributeTypeCode.Memo:
                    return "string";
                case AttributeTypeCode.Money:
                    return "money";
                case AttributeTypeCode.Owner:
                    return "owner";
                case AttributeTypeCode.PartyList:
                    return "partylist";
                case AttributeTypeCode.Picklist:
                    return "optionsetvalue";
                case AttributeTypeCode.State:
                    return "state";
                case AttributeTypeCode.Status:
                    return "status";
                case AttributeTypeCode.String:
                    return "string";
                case AttributeTypeCode.Uniqueidentifier:
                    return "guid";
                case AttributeTypeCode.CalendarRules:
                    break;
                case AttributeTypeCode.Virtual:
                    break;
                case AttributeTypeCode.BigInt:
                    break;
                case AttributeTypeCode.ManagedProperty:
                    break;
                case AttributeTypeCode.EntityName:
                    break;
                default:
                    break;
            }

            throw new Exception(String.Format("GetFieldNodeType: Unknown Field Node Type - {0}:{1} - {2}:{3}", attribute.EntityLogicalName, attribute.LogicalName, attribute.AttributeTypeName.Value, attribute.AttributeType));
        }

        private static String GetFieldNodeLookupType(AttributeMetadata attribute)
        {
            switch (attribute.AttributeType)
            {
                case AttributeTypeCode.Customer:
                case AttributeTypeCode.Lookup:
                    return String.Join("|", ((LookupAttributeMetadata)attribute).Targets.ToList<String>());
                default:
                    break;
            }

            return null;
        }
    }
}
