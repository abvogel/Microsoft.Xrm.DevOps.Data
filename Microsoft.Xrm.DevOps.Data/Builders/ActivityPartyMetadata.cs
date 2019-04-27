using Microsoft.Xrm.DevOps.Data.SupportClasses;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Xrm.DevOps.Data.Builders
{
    class ActivityPartyMetadata
    {
        public static EntityMetadata Build()
        {
            EntityMetadata metadata = new EntityMetadata
            {
                LogicalName = "activityparty"
            };

            List<AttributeMetadata> attributeMetadatas = new List<AttributeMetadata>();
            metadata.SetFieldValue("ObjectTypeCode", 135);
            metadata.SetFieldValue("PrimaryIdAttribute", "activitypartyid");
            metadata.SetFieldValue("PrimaryNameAttribute", "partyidname");
            metadata.SetFieldValue("DisplayName", new Label("Activity Party", 1033));

            attributeMetadatas.Add(BuildAttribute(new LookupAttributeMetadata(), "ActivityId", "Activity"));
            attributeMetadatas.Add(BuildAttribute(new UniqueIdentifierAttributeMetadata(), "ActivityPartyId", "Activity Party"));
            attributeMetadatas.Add(BuildAttribute(new StringAttributeMetadata(), "AddressUsed", "Address"));
            attributeMetadatas.Add(BuildAttribute(new IntegerAttributeMetadata(), "AddressUsedEmailColumnNumber", "Email column number of party"));
            attributeMetadatas.Add(BuildAttribute(new BooleanAttributeMetadata(), "DoNotEmail", "Do not allow Emails"));
            attributeMetadatas.Add(BuildAttribute(new BooleanAttributeMetadata(), "DoNotFax", "Do not allow Faxes"));
            attributeMetadatas.Add(BuildAttribute(new BooleanAttributeMetadata(), "DoNotPhone", "Do not allow Phone Calls"));
            attributeMetadatas.Add(BuildAttribute(new BooleanAttributeMetadata(), "DoNotPostalMail", "Do not allow Postal Mails"));
            attributeMetadatas.Add(BuildAttribute(new DoubleAttributeMetadata(), "Effort", "Effort"));
            attributeMetadatas.Add(BuildAttribute(new StringAttributeMetadata(), "ExchangeEntryId", "Exchange Entry"));
            attributeMetadatas.Add(BuildAttribute(new PicklistAttributeMetadata(), "InstanceTypeCode", "Appointment Type"));
            attributeMetadatas.Add(BuildAttribute(new BooleanAttributeMetadata(), "IsPartyDeleted", "Is Party Deleted"));
            attributeMetadatas.Add(BuildAttribute(new LookupAttributeMetadata(), "OwnerId", "Owner"));
            attributeMetadatas.Add(BuildAttribute(new PicklistAttributeMetadata(), "ParticipationTypeMask", "Participation Type"));
            attributeMetadatas.Add(BuildAttribute(new LookupAttributeMetadata(), "PartyId", "Party"));
            attributeMetadatas.Add(BuildAttribute(new EntityNameAttributeMetadata(), "PartyObjectTypeCode", String.Empty));
            attributeMetadatas.Add(BuildAttribute(new LookupAttributeMetadata(), "ResourceSpecId", "Resource Specification"));
            attributeMetadatas.Add(BuildAttribute(new StringAttributeMetadata(), "ResourceSpecIdName", String.Empty));
            attributeMetadatas.Add(BuildAttribute(new DateTimeAttributeMetadata(), "ScheduledEnd", "Scheduled End"));
            attributeMetadatas.Add(BuildAttribute(new DateTimeAttributeMetadata(), "ScheduledStart", "Scheduled Start"));

            metadata.SetSealedPropertyValue("Attributes", attributeMetadatas.ToArray());

            return metadata;
        }

        private static AttributeMetadata BuildAttribute(AttributeMetadata Attribute, string LogicalName, string DisplayName)
        {
            Attribute.SetFieldValue("EntityLogicalName", "activityparty");
            Attribute.SetFieldValue("LogicalName", LogicalName.ToLower());
            Attribute.SetFieldValue("DisplayName", new Label(DisplayName, 1033));
            return Attribute;
        }
    }
}
