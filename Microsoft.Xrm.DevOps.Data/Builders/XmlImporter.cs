using Microsoft.Xrm.DevOps.Data.DataXml;
using Microsoft.Xrm.DevOps.Data.SchemaXml;
using Microsoft.Xrm.DevOps.Data.SupportClasses;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Xrm.DevOps.Data.Builders
{
    internal class XmlImporter
    {
        internal static object GetObjectFromFieldNodeType(Data.DataXml.Field field, SchemaXml.Entity schemaData)
        {
            var fieldSchemaData = schemaData.Fields.Field.Where(x => x.Name.Equals(field.Name)).FirstOrDefault();

            switch (fieldSchemaData.Type)
            {
                case "bool":
                    return Boolean.Parse(field.Value);
                case "entityreference":
                case "owner":
                    var value = new EntityReference(field.Lookupentity, Guid.Parse(field.Value));
                    value.Name = field.Lookupentityname;
                    return value;
                case "datetime":
                    return DateTime.Parse(field.Value, CultureInfo.CurrentCulture, DateTimeStyles.AdjustToUniversal);
                case "decimal":
                    return Decimal.Parse(field.Value.NormalizeSeparator());
                case "float":
                    return Double.Parse(field.Value.NormalizeSeparator());
                case "number":
                    return Int32.Parse(field.Value);
                case "string":
                    return field.Value;
                case "money":
                    return new Money(Decimal.Parse(field.Value.NormalizeSeparator()));
                case "partylist":
                    return new EntityCollection(BuildActivityPartyList(field, schemaData));
                case "optionsetvalue":
                case "state":
                case "status":
                    return new OptionSetValue(Int32.Parse(field.Value));
                case "guid":
                    return Guid.Parse(field.Value);
                default:
                    throw new Exception(String.Format("Unknown Field Node Type: {0}", fieldSchemaData.Type));
            }
        }

        private static IList<Sdk.Entity> BuildActivityPartyList(DataXml.Field field, SchemaXml.Entity schemaData)
        {
            var activityParties = new List<Sdk.Entity>();

            foreach (var record in field.Activitypointerrecords)
            {
                var party = new Sdk.Entity("activityparty", Guid.Parse(record.Id));
                foreach (var attribute in record.Field)
                {
                    party[attribute.Name] = GetActivityPartyObjectFromFieldNodeType(attribute, schemaData);
                }
                activityParties.Add(party);
            }

            return activityParties;
        }

        private static object GetActivityPartyObjectFromFieldNodeType(DataXml.Field field, SchemaXml.Entity schemaData)
        {
            var attribute = Builders.ActivityPartyMetadata.Build().Attributes.Where(attr => attr.LogicalName.Equals(field.Name)).FirstOrDefault();

            switch (attribute.AttributeType)
            {
                case AttributeTypeCode.Boolean:
                    return Boolean.Parse(field.Value);
                case AttributeTypeCode.Lookup:
                case AttributeTypeCode.Owner:
                    return new EntityReference()
                    {
                        LogicalName = field.Lookupentity,
                        Id = Guid.Parse(field.Value),
                        Name = field.Lookupentityname
                    };
                case AttributeTypeCode.Picklist:
                case AttributeTypeCode.State:
                case AttributeTypeCode.Status:
                    return new OptionSetValue(Int32.Parse(field.Value));
                case AttributeTypeCode.Uniqueidentifier:
                    return Guid.Parse(field.Value);
                case AttributeTypeCode.CalendarRules:
                case AttributeTypeCode.Virtual:
                case AttributeTypeCode.BigInt:
                case AttributeTypeCode.ManagedProperty:
                case AttributeTypeCode.EntityName:
                case AttributeTypeCode.Customer:
                case AttributeTypeCode.PartyList:
                case AttributeTypeCode.DateTime:
                case AttributeTypeCode.Decimal:
                case AttributeTypeCode.Double:
                case AttributeTypeCode.Integer:
                case AttributeTypeCode.Memo:
                case AttributeTypeCode.String:
                case AttributeTypeCode.Money:
                default:
                    throw new Exception(String.Format("Unsupported attribute type {0}.", attribute.AttributeType));
            }
        }

        private static AttributeMetadata GetAttributeMetadataFromFieldNodeType(SchemaXml.Field property)
        {
            var attr = new AttributeMetadata();

            switch (property.Type)
            {
                case "bool":
                    return new BooleanAttributeMetadata();
                case "partylist":
                    attr = new LookupAttributeMetadata();
                    attr.SetSealedPropertyValue("AttributeType", AttributeTypeCode.PartyList);
                    attr.SetSealedPropertyValue("Targets", new String[] { "account", "contact" });
                    return attr;
                case "entityreference":
                    attr = new LookupAttributeMetadata();
                    return attr;
                case "owner":
                    attr = new LookupAttributeMetadata();
                    attr.SetSealedPropertyValue("AttributeType", AttributeTypeCode.Owner);
                    return attr;
                case "datetime":
                    return new DateTimeAttributeMetadata();
                case "decimal":
                    return new DecimalAttributeMetadata();
                case "float":
                    return new DoubleAttributeMetadata();
                case "number":
                    return new IntegerAttributeMetadata();
                case "string":
                    return new StringAttributeMetadata();
                case "money":
                    return new MoneyAttributeMetadata();
                case "optionsetvalue":
                    return new PicklistAttributeMetadata();
                case "state":
                    return new StateAttributeMetadata();
                case "status":
                    return new StatusAttributeMetadata();
                case "guid":
                    return new UniqueIdentifierAttributeMetadata();
                default:
                    throw new Exception(String.Format("Unknown Field Node Type: {0}", property.Type));
                    //  ?  return new MultiSelectPicklistAttributeMetadata();
            }
        }
            
        internal static EntityMetadata GenerateAdditionalMetadata(EntityMetadata currentMetadata, SchemaXml.Entity schemaXML)
        {
            var attributes = currentMetadata.Attributes.ToList();
            var manyToManyRelationshipMetadatas = currentMetadata.ManyToManyRelationships.ToList();
            currentMetadata.LogicalName = schemaXML.Name;
            currentMetadata.SetFieldValue("ObjectTypeCode", schemaXML.Etc);
            currentMetadata.SetFieldValue("PrimaryIdAttribute", schemaXML.Primaryidfield);
            currentMetadata.SetFieldValue("PrimaryNameAttribute", schemaXML.Primarynamefield);
            currentMetadata.SetFieldValue("DisplayName", new Label(schemaXML.Displayname, 1033));
            foreach (var property in schemaXML.Fields.Field)
            {
                var index = attributes.FindIndex(attr => attr.LogicalName.Equals(property.Name));

                if (index == -1)
                {
                    attributes.Add(GetAttributeMetadataFromFieldNodeType(property));
                    index = attributes.Count - 1;
                }
                else
                    attributes[index] = GetAttributeMetadataFromFieldNodeType(property);

                attributes[index].SetFieldValue("EntityLogicalName", schemaXML.Name);
                attributes[index].SetFieldValue("LogicalName", property.Name);
                attributes[index].SetFieldValue("DisplayName", new Label(property.Displayname, 1033));

                if (!String.IsNullOrEmpty(property.Customfield) && property.Customfield.ToLower().Equals("true"))
                    attributes[index].SetFieldValue("IsCustomAttribute", true);

                if (!String.IsNullOrEmpty(property.LookupType))
                    ((LookupAttributeMetadata)attributes[index]).SetFieldValue("Targets", property.LookupType.Split('|'));
            }

            if (schemaXML.Relationships != null)
            {
                foreach (var relationship in schemaXML.Relationships.Relationship)
                {
                    var index = manyToManyRelationshipMetadatas.FindIndex(m2m => m2m.IntersectEntityName.Equals(relationship.RelatedEntityName));

                    if (index == -1)
                    {
                        manyToManyRelationshipMetadatas.Add(new ManyToManyRelationshipMetadata());
                        index = manyToManyRelationshipMetadatas.Count - 1;
                    }

                    manyToManyRelationshipMetadatas[index] = new ManyToManyRelationshipMetadata()
                    {
                        Entity1LogicalName = currentMetadata.LogicalName,
                        Entity2LogicalName = relationship.M2mTargetEntity,
                        IntersectEntityName = relationship.RelatedEntityName,
                        Entity2IntersectAttribute = relationship.M2mTargetEntityPrimaryKey
                    };
                }
            }

            currentMetadata.SetSealedPropertyValue("Attributes", attributes.ToArray());
            currentMetadata.SetSealedPropertyValue("ManyToManyRelationships", manyToManyRelationshipMetadatas.ToArray());
            
            return currentMetadata;
        }
    }
}
