using System;
using System.Xml;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Tooling.Connector;
using Microsoft.Xrm.Sdk.Query;

namespace Microsoft.Xrm.DevOps.Data
{
    public class DataBuilder
    {
        private Dictionary<String, BuilderEntityMetadata> _Entities = new Dictionary<String, BuilderEntityMetadata>();
        private Boolean _PluginsDisabled = true;
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
            _Entities[entity.LogicalName].Entities.Enqueue(entity);
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
        
        public void SetIdentifier(String logicalName, List<String> identifiers)
        {
            VerifyEntityExists(logicalName);
            _Entities[logicalName].Identifiers = identifiers;
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
            return XmlSchemaBuilder.ToXmlDocument(_Entities, _PluginsDisabled);
        }

        public XmlDocument BuildDataXML() {
            return XmlDataBuilder.ToXmlDocument(_Entities, _PluginsDisabled);
        }
    }
}
