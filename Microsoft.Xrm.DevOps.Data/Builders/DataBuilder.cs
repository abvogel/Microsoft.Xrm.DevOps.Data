using System;
using System.Xml;
using System.Collections.Generic;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;
using System.Linq;
using System.Xml.Serialization;

namespace Microsoft.Xrm.DevOps.Data
{
    public partial class DataBuilder
    {
        #region Declarations
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
        #endregion

        public DataBuilder()
        {

        }

        public DataBuilder(IOrganizationService service)
        {
            this.Service = service;
        }

        public void AppendData(Entity entity)
        {
            this.VerifyEntityExists(entity.LogicalName);
            this.RefreshMetadataFromConnection(entity.LogicalName);
            this._Entities[entity.LogicalName].AppendEntity(entity);
        }

        public void AppendData(List<Entity> entities)
        {
            foreach (var entity in entities)
            {
                this.AppendData(entity);
            }
        }

        public void AppendData(EntityCollection entityCollection)
        {
            foreach (var entity in entityCollection.Entities)
            {
                this.AppendData(entity);
            }
        }

        public void AppendData(String logicalName, Dictionary<String, Object> entity)
        {
            this.VerifyEntityExists(logicalName);
            this.RefreshMetadataFromConnection(logicalName);
            var primaryIdField = _Entities[logicalName].Metadata.PrimaryIdAttribute;

            Entity newEntity = new Entity(logicalName);
            foreach (var keyValuePair in entity)
            {
                if (newEntity.Id == Guid.Empty &&
                      (keyValuePair.Key.ToLower() == primaryIdField))
                {
                    newEntity.Id = Guid.Parse(keyValuePair.Value.ToString());
                }

                if (keyValuePair.Key.Contains("ReturnProperty_"))
                    continue;

                if (keyValuePair.Value != null && keyValuePair.Value.GetType().Name == "KeyValuePair`2")
                    continue;

                newEntity[keyValuePair.Key] = keyValuePair.Value;
            }

            this.AppendData(newEntity);
        }

        public void AppendData(String logicalName, Dictionary<String, Object>[] entities)
        {
            foreach (var entity in entities)
            {
                this.AppendData(logicalName, entity);
            }
        }

        public void AppendData(String fetchXml)
        {
            try
            {
                List<Entity> retrievedEntities = SupportClasses.SupportMethods.RetrieveAllRecords(this._service, fetchXml);

                if (retrievedEntities.Count == 0)
                    return;

                if (HasManyToManyAttribute(fetchXml))
                {
                    this.AppendM2MData(retrievedEntities, GetFirstLinkEntityName(fetchXml));
                }
                else
                {
                    this.AppendData(retrievedEntities);
                }

                this.RefreshMetadataFromConnection(retrievedEntities[0].LogicalName);
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Failed to retrieve fetch results: {0}.", ex.Message));
            }
        }

        public void AppendData(String DataXML, String SchemaXML)
        {
            var _dataXml = new XmlDocument();
            _dataXml.LoadXml(DataXML);

            var _schemaXml = new XmlDocument();
            _schemaXml.LoadXml(SchemaXML);

            this.AppendData(_dataXml, _schemaXml);
        }

        public void AppendData(XmlDocument DataXML, XmlDocument SchemaXML)
        {
            var dataSerializer = new XmlSerializer(typeof(Data.DataXml.Entities));
            XmlReader dataReader = new XmlNodeReader(DataXML);
            var data = (Data.DataXml.Entities)dataSerializer.Deserialize(dataReader);

            var schemaSerializer = new XmlSerializer(typeof(Data.SchemaXml.Entities));
            XmlReader schemaReader = new XmlNodeReader(SchemaXML);
            var schema = (Data.SchemaXml.Entities)schemaSerializer.Deserialize(schemaReader);

            this.AppendData(data, schema);
        }

        protected void AppendData(Data.DataXml.Entities DataXML, Data.SchemaXml.Entities SchemaXML)
        {
            foreach (var schemaData in SchemaXML.Entity)
            {
                this.VerifyEntityExists(schemaData);
                this.AddMetadataFromSchema(schemaData);
            }

            foreach (var entity in DataXML.Entity)
            {
                String logicalName = entity.Name;
                var schemaData = SchemaXML.Entity.Where(x => x.Name.Equals(logicalName)).First();
                this.VerifyEntityExists(schemaData);
                this.AddMetadataFromSchema(schemaData);

                foreach (var record in entity.Records.Record)
                {
                    Entity holdingEntity = new Entity(logicalName, Guid.Parse(record.Id));
                    foreach (var field in record.Field)
                    {
                        holdingEntity[field.Name] = Builders.XmlImporter.GetObjectFromFieldNodeType(field, schemaData);
                    }
                    this.AppendData(holdingEntity);
                }

                if (this._Entities[logicalName].Identifiers.Count == 0)
                {
                    List<String> setIdentifiers = schemaData.Fields.Field.Where(field => field.UpdateCompare == "true").Select(field => field.Name).ToList();
                    if (setIdentifiers.Count == 0)
                    {
                        setIdentifiers.Add(schemaData.Primaryidfield);
                    }
                    this._Entities[logicalName].Identifiers = setIdentifiers;
                }

                if (this._Entities[logicalName].PluginsDisabled == null
                        && !String.IsNullOrEmpty(schemaData.Disableplugins))
                {
                    this._Entities[logicalName].PluginsDisabled = Boolean.Parse(schemaData.Disableplugins);
                }

                foreach (var relationship in entity.M2mrelationships.M2mrelationship)
                {
                    Dictionary<Guid, List<Guid>> relationshipPairs = CompileRelationshipData(relationship);
                    this._Entities[entity.Name].AppendM2MDataToEntity(relationship.M2mrelationshipname, relationshipPairs);
                }
            }
        }

        public void RemoveData(Entity entity)
        {
            this.VerifyEntityExists(entity.LogicalName);
            this._Entities[entity.LogicalName].RemoveAttributesWhereIdentical(entity);
        }

        public void RemoveData(Entity[] entities)
        {
            foreach (var entity in entities)
            {
                this.VerifyEntityExists(entity.LogicalName);
                this._Entities[entity.LogicalName].RemoveAttributesWhereIdentical(entity);
            }
        }

        public void RemoveData(String DataXML, String SchemaXML)
        {
            var _dataXml = new XmlDocument();
            _dataXml.LoadXml(DataXML);

            var _schemaXml = new XmlDocument();
            _schemaXml.LoadXml(SchemaXML);

            RemoveData(_dataXml, _schemaXml);
        }

        public void RemoveData(XmlDocument DataXML, XmlDocument SchemaXML)
        {
            var dataSerializer = new XmlSerializer(typeof(Data.DataXml.Entities));
            XmlReader dataReader = new XmlNodeReader(DataXML);
            var data = (Data.DataXml.Entities)dataSerializer.Deserialize(dataReader);

            var schemaSerializer = new XmlSerializer(typeof(Data.SchemaXml.Entities));
            XmlReader schemaReader = new XmlNodeReader(SchemaXML);
            var schema = (Data.SchemaXml.Entities)schemaSerializer.Deserialize(schemaReader);

            RemoveData(data, schema);
        }

        protected void RemoveData(Data.DataXml.Entities DataXML, Data.SchemaXml.Entities SchemaXML)
        {
            foreach (var entity in DataXML.Entity)
            {
                String logicalName = entity.Name;
                var schemaData = SchemaXML.Entity.Where(x => x.Name.Equals(logicalName)).First();
                this.VerifyEntityExists(schemaData);
                this.AddMetadataFromSchema(schemaData);

                foreach (var record in entity.Records.Record)
                {
                    Entity holdingEntity = new Entity(logicalName, Guid.Parse(record.Id));
                    foreach (var field in record.Field)
                    {
                        holdingEntity[field.Name] = Builders.XmlImporter.GetObjectFromFieldNodeType(field, schemaData);
                    }
                    this.RemoveData(holdingEntity);
                }

                foreach (var relationship in entity.M2mrelationships.M2mrelationship)
                {
                    Dictionary<Guid, List<Guid>> relationshipPairs = CompileRelationshipData(relationship);
                    this._Entities[logicalName].RemoveRelationshipsWhereIdentical(relationship.M2mrelationshipname, relationshipPairs);
                }
            }
        }

        public void SetIdentifier(String LogicalName, String[] Identifier)
        {
            this.VerifyEntityExists(LogicalName);
            this._Entities[LogicalName].Identifiers = new List<String>();
            foreach (String partialIdentifier in Identifier)
            {
                this._Entities[LogicalName].Identifiers.Add(partialIdentifier);
            };
        }

        public void SetIdentifier(String logicalName, String identifier)
        {
            this.VerifyEntityExists(logicalName);
            this._Entities[logicalName].Identifiers = new List<string>() { identifier };
        }

        public void SetPluginsDisabled(Boolean disabled)
        {
            this._PluginsDisabled = disabled;
        }

        public void SetPluginsDisabled(String logicalName, Boolean disabled)
        {
            this.VerifyEntityExists(logicalName);
            this._Entities[logicalName].PluginsDisabled = disabled;
        }

        public XmlDocument BuildSchemaXML()
        {
            this._Entities.Keys.ToList().ForEach(logicalName =>
            {
                this.AppendData(GetStubRecords(logicalName));
            });

            this._Entities.Keys.ToList().ForEach(logicalName =>
            {
                // Commit global plugin disable state if it was set
                if (_PluginsDisabled != null)
                    this._Entities[logicalName].PluginsDisabled = (bool)_PluginsDisabled;
                this.FinalizeEntity(logicalName);
            });

            return XmlSchemaBuilder.ToXmlDocument(_Entities);
        }

        public XmlDocument BuildDataXML()
        {
            this._Entities.Keys.ToList().ForEach(logicalName =>
            {
                this.AppendData(GetStubRecords(logicalName));
            });

            this._Entities.Keys.ToList().ForEach(logicalName =>
            {
                this.FinalizeEntity(logicalName);
            });

            return XmlDataBuilder.ToXmlDocument(_Entities);
        }

        public XmlDocument BuildContentTypesXML()
        {
            String contentType = "<?xml version=\"1.0\" encoding=\"utf-8\"?><Types xmlns=\"http://schemas.openxmlformats.org/package/2006/content-types\"><Default Extension=\"xml\" ContentType=\"application/octet-stream\" /></Types>";
            XmlDocument content = new XmlDocument();
            content.LoadXml(contentType);
            return content;
        }

        #region Private Methods
        private void VerifyEntityExists(String logicalName)
        {
            if (!this._Entities.ContainsKey(logicalName))
            {
                this._Entities[logicalName] = new BuilderEntityMetadata();
            }
        }

        private void VerifyEntityExists(SchemaXml.Entity schemaXML)
        {
            if (!this._Entities.ContainsKey(schemaXML.Name))
            {
                this._Entities[schemaXML.Name] = new BuilderEntityMetadata();
            }
        }

        private static Dictionary<Guid, List<Guid>> CompileRelationshipData(DataXml.M2mrelationship relationship)
        {
            Dictionary<Guid, List<Guid>> relationshipPairs = new Dictionary<Guid, List<Guid>>();
            List<Guid> targetids = new List<Guid>();

            foreach (var targetid in relationship.Targetids.Targetid)
            {
                targetids.Add(Guid.Parse(targetid));
            }

            relationshipPairs.Add(Guid.Parse(relationship.Sourceid), targetids);
            return relationshipPairs;
        }

        private void RefreshMetadataFromConnection(String logicalName)
        {
            if (this.Service != null
                    && this._Entities[logicalName].FetchedAllMetadata == false)
            {
                var retrieveEntityRequest = new RetrieveEntityRequest();
                retrieveEntityRequest.LogicalName = logicalName;
                retrieveEntityRequest.EntityFilters = EntityFilters.All;
                RetrieveEntityResponse RetrieveEntityResponse = (RetrieveEntityResponse)Service.Execute(retrieveEntityRequest);
                this._Entities[logicalName].Metadata = RetrieveEntityResponse.EntityMetadata;
                this._Entities[logicalName].FetchedAllMetadata = true;
            }
        }

        private void AddMetadataFromSchema(SchemaXml.Entity schemaXML)
        {
            this._Entities[schemaXML.Name].PluginsDisabled = schemaXML.Disableplugins == "true";
            this._Entities[schemaXML.Name].SkipUpdate = schemaXML.Skipupdate == "true";
            this._Entities[schemaXML.Name].Metadata = Builders.XmlImporter.GenerateAdditionalMetadata(this._Entities[schemaXML.Name].Metadata, schemaXML);
        }

        private void AppendM2MData(EntityCollection queryResponse, String FirstLinkEntityName)
        {
            AppendM2MData(queryResponse.Entities.ToList(), FirstLinkEntityName);
        }

        private void AppendM2MData(List<Entity> queryResponse, String FirstLinkEntityName)
        {
            var SourceEntity = queryResponse.FirstOrDefault().LogicalName;

            Dictionary<Guid, List<Guid>> relationshipPairs = new Dictionary<Guid, List<Guid>>();
            String relationshipName = queryResponse.FirstOrDefault().Attributes.Where(x => x.Value is AliasedValue).Select(x => ((AliasedValue)x.Value).EntityLogicalName).First();

            if (RelationshipIsReflexive(queryResponse.FirstOrDefault().LogicalName, relationshipName))
            {
                relationshipName = FirstLinkEntityName;
            }

            foreach (var record in queryResponse)
            {
                Guid relatedId = ((Guid)record.Attributes.Where(x => x.Value is AliasedValue).Select(x => ((AliasedValue)x.Value).Value).First());

                if (!relationshipPairs.ContainsKey(record.Id))
                {
                    relationshipPairs[record.Id] = new List<Guid>();
                }
                
                relationshipPairs[record.Id].Add(relatedId);
            }

            this.VerifyEntityExists(SourceEntity);
            foreach (var relationshipPair in relationshipPairs)
            {
                this._Entities[SourceEntity].AppendEntity(new Entity(SourceEntity, relationshipPair.Key));
            }
            this._Entities[SourceEntity].AppendM2MDataToEntity(relationshipName, relationshipPairs);
        }

        private bool RelationshipIsReflexive(string entityName, string relationshipName)
        {
            return entityName.Equals(relationshipName);
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

        private string GetFirstLinkEntityName(string fetchXml)
        {
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(fetchXml);

            return xml.SelectSingleNode("fetch/entity/link-entity[@intersect='true']/@name").Value;
        }

        private List<Entity> GetStubRecords(string logicalName)
        {
            List<Entity> stubRecords = new List<Entity>();

            // Add record stub to support internal lookups where the record doesn't exist
            List<AttributeMetadata> lookups = this.GetFieldsThatAreEntityReference(logicalName);
                stubRecords.AddRange(this.GetStubRecordsForInternalLookups(logicalName, lookups));

            // Add record stub to support m2m relationships
            if (this._Entities[logicalName].RelatedEntities.Count > 0)
                stubRecords.AddRange(this.GetStubRecordsForM2MRelationships(logicalName));

            return stubRecords;
        }

        private void FinalizeEntity(string logicalName)
        {
            // Always include the guid as a field if it is set for the entity.
            this.AppendIdFieldsForMissingIds(logicalName);

            // Commit identifier - merge duplicates based on chosen identifier
            this._Entities[logicalName].CommitIdentifier();
        }

        private void AppendIdFieldsForMissingIds(string logicalName)
        {
            var newEntities = new List<Entity>();

            foreach (var entity in _Entities[logicalName].Entities)
            {
                var newEntity = new Entity(entity.LogicalName, entity.Id);
                newEntity[_Entities[entity.LogicalName].Metadata.PrimaryIdAttribute] = entity.Id;
                newEntities.Add(newEntity);
            }

            this.AppendData(newEntities);
        }

        private List<Entity> GetStubRecordsForInternalLookups(string logicalName, List<AttributeMetadata> lookups)
        {
            var stubRecords = new List<Entity>();
            foreach (Entity record in this._Entities[logicalName].Entities)
            {
                foreach (AttributeMetadata fieldMetadata in lookups)
                {
                    if (!record.Contains(fieldMetadata.LogicalName))
                        continue;

                    else if (record[fieldMetadata.LogicalName] is EntityReference)
                    {
                        var recordField = (EntityReference)record[fieldMetadata.LogicalName];
                        if (_Entities.ContainsKey(recordField.LogicalName))
                            stubRecords.Add(new Entity(recordField.LogicalName, recordField.Id));
                    }
                }
            }
            return stubRecords;
        }

        private List<AttributeMetadata> GetFieldsThatAreEntityReference(string logicalName)
        {
            return this._Entities[logicalName].Metadata.Attributes.Where(x =>
            {
                if (this._Entities[logicalName].Attributes.Contains(x.LogicalName))
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

        private List<Entity> GetStubRecordsForM2MRelationships(string logicalName)
        {
            var stubRecords = new List<Entity>();
            foreach (var Relationship in this._Entities[logicalName].RelatedEntities)
            {
                var targetLogicalName = this._Entities[logicalName].Metadata.ManyToManyRelationships.Where(x => x.SchemaName == Relationship.Key || x.IntersectEntityName == Relationship.Key).Select(x => x.Entity2LogicalName).FirstOrDefault();

                foreach (var RelatedEntityPair in Relationship.Value)
                {
                    stubRecords.Add(new Entity(logicalName, RelatedEntityPair.Key));
                    RelatedEntityPair.Value.ToList<Guid>().ForEach(stubRecordId => stubRecords.Add(new Entity(targetLogicalName, stubRecordId)));
                }
            }
            return stubRecords;
        }
        #endregion
    }
}
