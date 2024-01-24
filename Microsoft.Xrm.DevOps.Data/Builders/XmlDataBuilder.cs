using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Xrm.DevOps.Data.DataXml;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;

namespace Microsoft.Xrm.DevOps.Data
{
    internal class XmlDataBuilder
    {
        internal static XmlDocument ToXmlDocument(Dictionary<string, BuilderEntityMetadata> entities)
        {
            XmlDocument xd = null;
            XmlSerializer xmlSerializer = new XmlSerializer(typeof (DataXml.Entities));
            using (MemoryStream memStm = new MemoryStream())
            {
                using (XmlWriter writer = XmlWriter.Create(memStm, new XmlWriterSettings { OmitXmlDeclaration = true }))
                {
                    xmlSerializer.Serialize(writer, GenerateDataXml(entities));

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

        private static DataXml.Entities GenerateDataXml(Dictionary<string, BuilderEntityMetadata> entities)
        {
            DataXml.Entities EntitiesNode = new DataXml.Entities
            {
                Entity = new List<DataXml.Entity>()
            };

            foreach (var logicalname in entities.Keys) {
                if (entities[logicalname].Entities.Count == 0 && entities[logicalname].RelatedEntities.Count == 0)
                    continue;

                EntitiesNode.Entity.Add(GenerateEntityNode(logicalname, entities[logicalname]));
            }

            EntitiesNode.Entity.Sort((x, y) => string.Compare(x.Name, y.Name));

            return EntitiesNode;
        }

        private static DataXml.Entity GenerateEntityNode(String logicalName, BuilderEntityMetadata builderEntityMetadata)
        {
            var EntityNode = new DataXml.Entity
            {
                Name = logicalName,
                Displayname = builderEntityMetadata.Metadata.DisplayName.LocalizedLabels[0].Label
            };

            var RecordsNode = new DataXml.Records
            {
                Record = new List<Record>()
            };

            foreach (var entity in builderEntityMetadata.Entities) {
                RecordsNode.Record.Add(GenerateRecordNode(entity, builderEntityMetadata));
            }

            EntityNode.Records = RecordsNode;

            EntityNode.M2mrelationships = new DataXml.M2mrelationships()
            {
                M2mrelationship = new List<M2mrelationship>()
            };

            foreach (var relationshipName in builderEntityMetadata.RelatedEntities.Keys)
            {
                var relationshipMetadata = builderEntityMetadata.Metadata.ManyToManyRelationships.Where(x => x.IntersectEntityName == relationshipName).First();

                foreach (KeyValuePair<Guid, List<Guid>> relationshipPair in builderEntityMetadata.RelatedEntities[relationshipName])
                {
                    string targetPrimaryKey = relationshipMetadata.Entity2IntersectAttribute;
                    if (relationshipMetadata.Entity1LogicalName == relationshipMetadata.Entity2LogicalName)
                    {
                        targetPrimaryKey = relationshipMetadata.Entity2IntersectAttribute.Substring(0, relationshipMetadata.Entity2IntersectAttribute.Length - 3);
                    }

                    var relationship = new M2mrelationship()
                    {
                        Sourceid = relationshipPair.Key.ToString(),
                        Targetentityname = relationshipMetadata.Entity2LogicalName,
                        Targetentitynameidfield = targetPrimaryKey,
                        M2mrelationshipname = relationshipMetadata.IntersectEntityName,
                        Targetids = new Targetids()
                        {
                            Targetid = new List<string>()
                        }
                    };

                    foreach (var relatedId in relationshipPair.Value)
                    {
                        relationship.Targetids.Targetid.Add(relatedId.ToString());
                    }

                    EntityNode.M2mrelationships.M2mrelationship.Add(relationship);
                }
            }

            EntityNode.Records.Record.Sort((x, y) => string.Compare(x.Id, y.Id));

            return EntityNode;
        }

        private static DataXml.Record GenerateRecordNode(Sdk.Entity entity, BuilderEntityMetadata builderEntityMetadata)
        {
            var RecordNode = new DataXml.Record
            {
                Id = entity.Id.ToString(),
                Field = new List<DataXml.Field>()
            };

            foreach (var attribute in entity.Attributes) {
                var attributeMetadata = builderEntityMetadata.Metadata.Attributes.Where(a => a.LogicalName.Equals(attribute.Key)).First();

                if (!IsSupportedAttributeType(attributeMetadata.AttributeType))
                    continue;

                RecordNode.Field.Add(GenerateFieldNode(attribute, attributeMetadata, builderEntityMetadata));
            }

            RecordNode.Field.Sort((x, y) => string.Compare(x.Name, y.Name));

            return RecordNode;
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

        private static DataXml.Field GenerateFieldNode(KeyValuePair<string, object> attribute, Sdk.Metadata.AttributeMetadata attributeMetadata, BuilderEntityMetadata builderEntityMetadata)
        {
            var FieldNode = new DataXml.Field
            {
                Name = attribute.Key
            };

            switch (attributeMetadata.AttributeType)
            {
                case Sdk.Metadata.AttributeTypeCode.DateTime:       // If need to convert to UTC may need to be isolated
                    FieldNode.Value = ((DateTime)attribute.Value).ToString("o");
                    break;
                case Sdk.Metadata.AttributeTypeCode.Boolean:
                case Sdk.Metadata.AttributeTypeCode.Decimal:        // Precision should carry from initial value
                case Sdk.Metadata.AttributeTypeCode.Double:
                case Sdk.Metadata.AttributeTypeCode.Integer:
                case Sdk.Metadata.AttributeTypeCode.Memo:
                case Sdk.Metadata.AttributeTypeCode.String:
                case Sdk.Metadata.AttributeTypeCode.Uniqueidentifier:
                    FieldNode.Value = attribute.Value.ToString();
                    break;
                case Sdk.Metadata.AttributeTypeCode.Money:
                    FieldNode.Value = ((Microsoft.Xrm.Sdk.Money)attribute.Value).Value.ToString();
                    break;
                case Sdk.Metadata.AttributeTypeCode.Lookup:
                case Sdk.Metadata.AttributeTypeCode.Owner:
                case Sdk.Metadata.AttributeTypeCode.Customer:
                    var EntityReference = (EntityReference)attribute.Value;
                        FieldNode.Value = EntityReference.Id.ToString();
                        FieldNode.Lookupentity = EntityReference.LogicalName;
                        FieldNode.Lookupentityname = EntityReference.Name;
                    break;
                case Sdk.Metadata.AttributeTypeCode.Picklist:
                case Sdk.Metadata.AttributeTypeCode.State:
                case Sdk.Metadata.AttributeTypeCode.Status:
                    FieldNode.Value = ((OptionSetValue)attribute.Value).Value.ToString();
                    break;
                case Sdk.Metadata.AttributeTypeCode.PartyList:
                    FieldNode.Value = String.Empty;
                    FieldNode.Lookupentity = String.Empty;
                    FieldNode.Lookupentityname = String.Empty;
                    var values = (EntityCollection)attribute.Value;
                    FieldNode.Activitypointerrecords = new List<DataXml.Activitypointerrecords>();
                    
                    foreach (var entity in values.Entities)
                    {
                        FieldNode.Activitypointerrecords.Add(GenerateActivitypointerNode(entity, builderEntityMetadata));
                    }
                    break;
                case Sdk.Metadata.AttributeTypeCode.CalendarRules:
                case Sdk.Metadata.AttributeTypeCode.Virtual:
                case Sdk.Metadata.AttributeTypeCode.BigInt:
                case Sdk.Metadata.AttributeTypeCode.ManagedProperty:
                case Sdk.Metadata.AttributeTypeCode.EntityName:
                default:
                    throw new Exception(String.Format("GenerateFieldNode: Unknown Field Node Type - {0}", attributeMetadata.AttributeType));
            }

            return FieldNode;
        }

        private static Activitypointerrecords GenerateActivitypointerNode(Sdk.Entity entity, BuilderEntityMetadata builderEntityMetadata)
        {
            var ActivitypointerNode = new DataXml.Activitypointerrecords
            {
                Id = entity.Id.ToString(),
                Field = new List<DataXml.Field>()
            };

            foreach (var attribute in entity.Attributes)
            {
                ActivitypointerNode.Field.Add(GenerateFieldNode(attribute, Builders.ActivityPartyMetadata.Build().Attributes.Where(a => a.LogicalName.Equals(attribute.Key)).First(), builderEntityMetadata));
            }

            return ActivitypointerNode;
        }
    }
}
