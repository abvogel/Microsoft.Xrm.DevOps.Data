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
            set {
                this._service = value;
                foreach (var kvp in _Entities.Keys) {
                    VerifyMetadataExists(kvp);
                }
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
                
                if (RetrieveEntityResponse != null) {
                    _Entities[logicalName].Metadata = RetrieveEntityResponse.EntityMetadata;
                } else { 
                    throw new Exception(String.Format("Metadata does not exist for entity {0}", logicalName));
                }

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

        public DataBuilder() {
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
                      ((keyValuePair.Key.ToLower().TrimEnd() == "returnproperty_id")
                    || (keyValuePair.Key.ToLower() == logicalName.ToLower() + "id")))
                {
                    newEntity.Id = Guid.Parse(keyValuePair.Value.ToString());
                }

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

            RetrieveMultipleResponse retrieveMultipleResponse = (RetrieveMultipleResponse)this._service.Execute(req);

            if (retrieveMultipleResponse != null) {
                AppendData(retrieveMultipleResponse.EntityCollection);
            } else {
                throw new Exception("Failed to retrieve fetch results.");
            }
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

                // Add record stub to support internal lookups where the record doesn't exist
                List<AttributeMetadata> lookups = GetFieldsThatAreEntityReference(logicalName);
                AppendStubRecordsForInternalLookups(logicalName, lookups);

                // Commit identifier - merge duplicates based on chosen identifier
                _Entities[logicalName].CommitIdentifier();
            }

            return XmlSchemaBuilder.ToXmlDocument(_Entities);
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

        public XmlDocument BuildDataXML() {
            foreach (var logicalName in _Entities.Keys)
            {
                // Add record stub to support internal lookups where the record doesn't exist
                List<AttributeMetadata> lookups = GetFieldsThatAreEntityReference(logicalName);
                AppendStubRecordsForInternalLookups(logicalName, lookups);

                // Commit identifier - merge duplicates based on chosen identifier
                _Entities[logicalName].CommitIdentifier();
            }

            return XmlDataBuilder.ToXmlDocument(_Entities);
        }
    }
}
