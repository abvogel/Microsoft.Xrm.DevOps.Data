using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;

namespace Microsoft.Xrm.DevOps.Data
{
    public class BuilderEntityMetadata
    {
        public EntityMetadata Metadata { get; set; }
        public EntityMetadata PartyMetadata { get; set; }
        public List<String> Attributes { get; private set; }
        public List<String> Identifiers = new List<String>();
        public Boolean PluginsDisabled = false;
        public Queue<Entity> Entities { get; }

        public BuilderEntityMetadata()
        {
            Entities = new Queue<Entity>();
        }

        public void AppendEntity(Entity entity)
        {
            Attributes = entity.Attributes.Select(b => b.Key)
                        .Union(Attributes)
                        .Distinct()
                        .ToList<String>();

            Entities.Enqueue(entity);
        }

        public void AppendEntities(List<Entity> entities)
        {
            entities.ForEach(entity => AppendEntity(entity));
        }

        public void CommitIdentifiers()
        {
            // Error checks
            if (Metadata == null)
            {
                throw new Exception("Metadata has not been set.");
            }

            if (Identifiers.Count == 0
                && String.IsNullOrEmpty(Metadata.PrimaryIdAttribute))
            {
                throw new Exception("Incomplete metadata available.");
            }

            if (Identifiers.Count == 0)
            {
                Identifiers.Add(Metadata.PrimaryIdAttribute);
            }

            Dictionary<String, Entity> DistinctEntities = new Dictionary<String, Entity>();
            
            while (Entities.Count > 0)
            {
                var entity = Entities.Dequeue();
                String EntityIdentifier = GetIdentifierFromEntity(entity);
                if (DistinctEntities.ContainsKey(EntityIdentifier))
                {
                    Entity priorEntity = DistinctEntities[EntityIdentifier];
                    foreach (var attribute in entity.Attributes)
                    {
                        priorEntity[attribute.Key] = attribute.Value;
                    }
                    DistinctEntities[EntityIdentifier] = priorEntity;
                } else {
                    DistinctEntities.Add(EntityIdentifier, entity);
                }
            }

            DistinctEntities.Keys.ToList<String>().ForEach(key => Entities.Enqueue(DistinctEntities[key]));
        }

        private String GetIdentifierFromEntity(Entity entity)
        {
            List<Object> EntityIdentifier = new List<Object>();

            Identifiers.ForEach(identifier =>
            {
                if (entity.Contains(identifier))
                {
                    EntityIdentifier.Add(entity[identifier]);
                }
            });

            return String.Join("|", EntityIdentifier);
        }
    }
}
