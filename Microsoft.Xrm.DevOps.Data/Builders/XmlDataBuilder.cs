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
    public class XmlDataBuilder
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
                EntitiesNode.Entity.Add(GenerateEntityNode(logicalname, entities[logicalname]));
            }

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
                RecordNode.Field.Add(GenerateFieldNode(attribute, builderEntityMetadata.Metadata.Attributes.Where(a => a.LogicalName.Equals(attribute.Key)).First(), builderEntityMetadata));
            }

            RecordNode.Field.Sort((x, y) => string.Compare(x.Name, y.Name));

            return RecordNode;
        }

        private static DataXml.Field GenerateFieldNode(KeyValuePair<string, object> attribute, Sdk.Metadata.AttributeMetadata attributeMetadata, BuilderEntityMetadata builderEntityMetadata)
        {
            var FieldNode = new DataXml.Field
            {
                Name = attribute.Key
            };

            switch (attributeMetadata.AttributeType)
            {
                case Sdk.Metadata.AttributeTypeCode.Boolean:
                case Sdk.Metadata.AttributeTypeCode.DateTime:       // If need to convert to UTC may need to be isolated
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
                case Sdk.Metadata.AttributeTypeCode.Customer:
                case Sdk.Metadata.AttributeTypeCode.Lookup:
                case Sdk.Metadata.AttributeTypeCode.Owner:
                    if (attribute.Value is Guid)                    // Compensates for bug in FakeXrmEasy
                    {
                        FieldNode.Value = attribute.Value.ToString();
                    } else
                    {
                        var EntityReference = (EntityReference)attribute.Value;
                        FieldNode.Value = EntityReference.Id.ToString();
                        FieldNode.Lookupentity = EntityReference.LogicalName;
                        FieldNode.Lookupentityname = EntityReference.Name;
                    }
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
                    throw new NotImplementedException();
                default:
                    break;
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
                ActivitypointerNode.Field.Add(GenerateFieldNode(attribute, builderEntityMetadata.PartyMetadata.Attributes.Where(a => a.LogicalName.Equals(attribute.Key)).First(), builderEntityMetadata));
            }

            return ActivitypointerNode;
        }
    }
}
