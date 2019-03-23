using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;

namespace Microsoft.Xrm.DevOps.Data
{
    public class DataBuilder
    {
        private Dictionary<String, List<Entity>> _EntityData { get; set; }
        private Dictionary<String, EntityMetadata> _EntityMetadata { get; set; }

        public DataBuilder()
        {
            _EntityData = new Dictionary<String, List<Entity>>();
            _EntityMetadata = new Dictionary<String, EntityMetadata>();
        }

        public void AddData(Entity entity)
        {
            if (!_EntityData.ContainsKey(entity.LogicalName))
            {
                _EntityData[entity.LogicalName] = new List<Entity>();
            }

            _EntityData[entity.LogicalName].Add(entity);
        }

        public void AddData(EntityCollection entityCollection)
        {
            foreach (var entity in entityCollection.Entities)
            {
                _EntityData[entity.LogicalName].Add(entity);
            }
        }

        public void AddData(String logicalName, Dictionary<String, Object> entity)
        {
            Entity newEntity = new Entity(logicalName);
            foreach (var keyValuePair in entity)
            {
                if (newEntity.Id == Guid.Empty 
                    || (keyValuePair.Key.ToLower().TrimEnd() == "returnproperty_id") // trimend to compensate for MS bug that adds extra whitespace
                    || (keyValuePair.Key.ToLower() == logicalName.ToLower() + "id"))
                {
                    newEntity.Id = Guid.Parse(keyValuePair.Value.ToString());
                }

                newEntity[keyValuePair.Key] = keyValuePair.Value;
            }

            AddData(newEntity);
        }

        public void AddData(String logicalName, Dictionary<String, Object>[] entities)
        {
            foreach (var entity in entities)
            {
                AddData(logicalName, entity);
            }
        }
        
        public void AddMetadata(EntityMetadata entityMetadata)
        {
            _EntityMetadata[entityMetadata.LogicalName] = entityMetadata;
        }

        public void AddMetadata(EntityMetadata[] entityMetadata)
        {
            foreach (var entity in entityMetadata)
            {
                AddMetadata(entity);
            }
        }

        public String[] ToList()
        {
            List<String> responses = new List<string>();

            //foreach (var _OrganizationResponse in _OrganizationResponses)
            //{
            //    if (!_EntityMetadata.ContainsKey(_OrganizationResponse.Key))
            //    {
            //        throw new Exception("Metadata missing from DataBuilder");
            //    }
            //}

            return responses.ToArray();
        }

        public override String ToString()
        {
            StringBuilder response = new StringBuilder();
            response.Append(ToList());
            return response.ToString();
        }
    }
}
