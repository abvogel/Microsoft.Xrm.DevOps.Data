using System;
using System.Xml;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Tooling.Connector;
using Microsoft.Xrm.Sdk.Query;
using System.Linq;
using Microsoft.Crm.Sdk.Messages;

namespace Microsoft.Xrm.DevOps.Data
{
    public class DataBuilder
    {
        private Dictionary<String, BuilderEntityMetadata> _Entities = new Dictionary<String, BuilderEntityMetadata>();
        private Boolean? _PluginsDisabled = null;
        private IOrganizationService _service;
        public IOrganizationService Service {
            get { 
                return _service; 
            }
            private set {
                this._service = value;
            }
        }

        private void VerifyEntityExists(String logicalName)
        {
            if (!_Entities.ContainsKey(logicalName))
            {
                _Entities[logicalName] = new BuilderEntityMetadata();
            }

            VerifyMetadataExists(logicalName);
        }

        private void VerifyMetadataExists(String logicalName) {
            if (_Entities[logicalName].Metadata == null
                    && this.Service != null) {
                var retrieveEntityRequest = new RetrieveEntityRequest();
                retrieveEntityRequest.LogicalName = logicalName;
                retrieveEntityRequest.EntityFilters = EntityFilters.Attributes;
                RetrieveEntityResponse RetrieveEntityResponse = (RetrieveEntityResponse)Service.Execute(retrieveEntityRequest);
                _Entities[logicalName].Metadata = RetrieveEntityResponse.EntityMetadata;

                var PartyTypeAttribute = Array.Find(_Entities[logicalName].Metadata.Attributes, s => s.AttributeType.Equals(AttributeTypeCode.PartyList));
                if (PartyTypeAttribute != null)
                {
                    retrieveEntityRequest.LogicalName = "activityparty";
                    retrieveEntityRequest.EntityFilters = EntityFilters.Attributes;
                    RetrieveEntityResponse = (RetrieveEntityResponse)Service.Execute(retrieveEntityRequest);

                    if (RetrieveEntityResponse != null)
                    {
                        _Entities[logicalName].PartyMetadata = RetrieveEntityResponse.EntityMetadata;
                    }
                }
            }
        }

        public DataBuilder(IOrganizationService service)
        {
            this.Service = service;
        }

        public void AppendData(Entity entity)
        {
            VerifyEntityExists(entity.LogicalName);

            // Always include the guid as a field if it is set for the entity.
            if (entity.Id != Guid.Empty)
            {
                entity[_Entities[entity.LogicalName].Metadata.PrimaryIdAttribute] = entity.Id;
            }

            _Entities[entity.LogicalName].AppendEntity(entity);
        }

        public void AppendData(List<Entity> entities)
        {
            foreach (var entity in entities)
            {
                AppendData(entity);
            }
        }

        public void AppendData(EntityCollection entityCollection)
        {
            foreach (var entity in entityCollection.Entities)
            {
                AppendData(entity);
            }
        }

        public void AppendData(String logicalName, Dictionary<String, Object> entity)
        {
            Entity newEntity = new Entity(logicalName);
            foreach (var keyValuePair in entity)
            {
                // trimend to compensate for MS bug that adds extra whitespace
                if (newEntity.Id == Guid.Empty &&
                      ((keyValuePair.Key.ToLower().TrimEnd() == "ReturnProperty_Id")
                    || (keyValuePair.Key.ToLower() == logicalName.ToLower() + "id")))
                {
                    newEntity.Id = Guid.Parse(keyValuePair.Value.ToString());
                }

                if (keyValuePair.Key.Contains("ReturnProperty_"))
                    continue;

                if (keyValuePair.Value != null && keyValuePair.Value.GetType().Name == "KeyValuePair`2")
                    continue;

                newEntity[keyValuePair.Key] = keyValuePair.Value;
            }

            AppendData(newEntity);
        }

        public void AppendData(String logicalName, Dictionary<String, Object>[] entities)
        {
            foreach (var entity in entities)
            {
                AppendData(logicalName, entity);
            }
        }

        public void AppendData(String fetchXml)
        {
            RetrieveMultipleRequest req = new RetrieveMultipleRequest
            {
                Query = new FetchExpression(fetchXml)
            };

            try
            {
                RetrieveMultipleResponse retrieveMultipleResponse = (RetrieveMultipleResponse)this._service.Execute(req);

                if (HasManyToManyAttribute(fetchXml))
                {
                    AppendM2MData(retrieveMultipleResponse.EntityCollection);
                }
                else
                {
                    AppendData(retrieveMultipleResponse.EntityCollection);
                }
            }
            catch (Exception)
            {
                throw new Exception("Failed to retrieve fetch results.");
            }
        }

        private void AppendM2MData(EntityCollection queryResponse)
        {
            var SourceEntity = queryResponse.EntityName;

            if (queryResponse.Entities.Count == 0)
            {
                return;
            }

            Dictionary<Guid, List<Guid>> relationshipPairs = new Dictionary<Guid, List<Guid>>();
            String relationshipName = queryResponse.Entities[0].Attributes.Where(x => x.Value is AliasedValue).Select(x => ((AliasedValue)x.Value).EntityLogicalName).First();

            foreach (var record in queryResponse.Entities)
            {
                Guid relatedId = ((Guid)record.Attributes.Where(x => x.Value is AliasedValue).Select(x => ((AliasedValue)x.Value).Value).First());

                if (!relationshipPairs.ContainsKey(record.Id))
                {
                    relationshipPairs[record.Id] = new List<Guid>();
                }
                
                relationshipPairs[record.Id].Add(relatedId);
            }

            VerifyEntityExists(SourceEntity);
            _Entities[SourceEntity].AppendM2MDataToEntity(relationshipName, relationshipPairs);
        }

        private bool HasManyToManyAttribute(string fetchXml)
        {
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(fetchXml);

            if (xml.SelectSingleNode("fetch/entity/link-entity[@intersect='true']//attribute") != null)
            {
                return true;
            }

            return false;
        }

        public void SetIdentifier(String logicalName, String[] identifiers)
        {
            VerifyEntityExists(logicalName);
            _Entities[logicalName].Identifiers = new List<String>(identifiers);
        }

        public void SetIdentifier(String logicalName, String identifier)
        {
            VerifyEntityExists(logicalName);
            _Entities[logicalName].Identifiers = new List<string>() { identifier };
        }

        public void SetPluginsDisabled(Boolean disabled) {
            _PluginsDisabled = disabled;
        }

        public void SetPluginsDisabled(String logicalName, Boolean disabled)
        {
            VerifyEntityExists(logicalName);
            _Entities[logicalName].PluginsDisabled = disabled;
        }

        public XmlDocument BuildSchemaXML() {
            foreach (var logicalName in _Entities.Keys)
            {
                // Commit global plugin disable state if it was set
                if (_PluginsDisabled != null)
                    _Entities[logicalName].PluginsDisabled = (bool)_PluginsDisabled;

                FinalizeEntity(logicalName);
            }

            return XmlSchemaBuilder.ToXmlDocument(_Entities);
        }

        public XmlDocument BuildDataXML() {
            foreach (var logicalName in _Entities.Keys)
            {
                FinalizeEntity(logicalName);
            }

            return XmlDataBuilder.ToXmlDocument(_Entities);
        }

        public XmlDocument BuildContentTypesXML()
        {
            String contentType = "<?xml version=\"1.0\" encoding=\"utf-8\"?><Types xmlns=\"http://schemas.openxmlformats.org/package/2006/content-types\"><Default Extension=\"xml\" ContentType=\"application/octet-stream\" /></Types>";
            XmlDocument content = new XmlDocument();
            content.LoadXml(contentType);
            return content;
        }

        private void FinalizeEntity(string logicalName)
        {
            // Add record stub to support internal lookups where the record doesn't exist
            List<AttributeMetadata> lookups = GetFieldsThatAreEntityReference(logicalName);
            AppendStubRecordsForInternalLookups(logicalName, lookups);

            // Add record stub to support m2m relationships
            if (_Entities[logicalName].RelatedEntities.Count > 0)
                AppendStubRecordsForM2MRelationships(logicalName);

            // Commit identifier - merge duplicates based on chosen identifier
            _Entities[logicalName].CommitIdentifier();
        }

        private void AppendStubRecordsForInternalLookups(string logicalName, List<AttributeMetadata> lookups)
        {
            var stubRecords = new List<Entity>();
            foreach (Entity record in _Entities[logicalName].Entities)
            {
                foreach (AttributeMetadata fieldMetadata in lookups)
                {
                    if (!record.Contains(fieldMetadata.LogicalName))
                        continue;

                    var recordField = (EntityReference)record[fieldMetadata.LogicalName];

                    if (_Entities.ContainsKey(recordField.LogicalName))
                        stubRecords.Add(new Entity(recordField.LogicalName, recordField.Id));
                }
            }
            this.AppendData(stubRecords);
        }

        private List<AttributeMetadata> GetFieldsThatAreEntityReference(string logicalName)
        {
            return _Entities[logicalName].Metadata.Attributes.Where(x =>
            {
                if (_Entities[logicalName].Attributes.Contains(x.LogicalName))
                {
                    switch (x.AttributeType)
                    {
                        case AttributeTypeCode.Lookup:
                        case AttributeTypeCode.Customer:
                        case AttributeTypeCode.Owner:
                            return true;
                        default:
                            return false;
                    }
                }
                return false;
            }).ToList();
        }

        private void AppendStubRecordsForM2MRelationships(string logicalName)
        {
            var stubRecords = new List<Entity>();
            foreach (var Relationship in _Entities[logicalName].RelatedEntities)
            {
                foreach (var RelatedEntityPair in Relationship.Value)
                {
                    stubRecords.Add(new Entity(logicalName, RelatedEntityPair.Key));
                }
            }
            this.AppendData(stubRecords);
        }
    }
}
