using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;

namespace Microsoft.Xrm.DevOps.Data
{
    public class DataBuilder
    {
        private Dictionary<String, BuilderEntityMetadata> _Entities { get; set; }
        private void VerifyEntityExists(String logicalName)
        {
            if (!_Entities.ContainsKey(logicalName))
            {
                _Entities[logicalName] = new BuilderEntityMetadata();
            }
        }

    public DataBuilder()
        {

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
        
        public void AddMetadata(EntityMetadata entityMetadata)
        {
            if (!_Entities.ContainsKey(entityMetadata.LogicalName))
            {
                _Entities[entityMetadata.LogicalName] = new BuilderEntityMetadata();
            }

            _Entities[entityMetadata.LogicalName].Metadata = entityMetadata;
        }

        public void AddMetadata(EntityMetadata[] entityMetadata)
        {
            foreach (var entity in entityMetadata)
            {
                AddMetadata(entity);
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

        public void SetPluginsDisabled(String logicalName, Boolean disabled)
        {
            VerifyEntityExists(logicalName);
            _Entities[logicalName].PluginsDisabled = disabled;
        }

        public Dictionary<String,String> Build()
        {
            return XMLBuilder.ToStrings(_Entities);
        }
    }
}
