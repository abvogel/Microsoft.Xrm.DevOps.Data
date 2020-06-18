using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xrm.DevOps.Data.SupportClasses;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;

namespace Microsoft.Xrm.DevOps.Data
{
    internal class BuilderEntityMetadata
    {
        public EntityMetadata Metadata { get; set; }
        public Boolean FetchedAllMetadata = false;
        public List<String> Attributes { get; private set; }
        public List<String> Identifiers = new List<String>();
        public Boolean? PluginsDisabled = null;
        public Boolean? SkipUpdate = null;
        public Queue<Entity> Entities { get; set; }
        public Dictionary<String, Dictionary<Guid, List<Guid>>> RelatedEntities = new Dictionary<String, Dictionary<Guid, List<Guid>>>();

        public BuilderEntityMetadata()
        {
            Entities = new Queue<Entity>();
            Attributes = new List<string>();
            Metadata = new EntityMetadata();
            List<AttributeMetadata> attributeMetadatas = new List<AttributeMetadata>();
            List<ManyToManyRelationshipMetadata> manyToManyRelationshipMetadatas = new List<ManyToManyRelationshipMetadata>();
            Metadata.SetSealedPropertyValue("Attributes", attributeMetadatas.ToArray());
            Metadata.SetSealedPropertyValue("ManyToManyRelationships", manyToManyRelationshipMetadatas.ToArray());
        }

        public void AppendEntity(Entity entity)
        {
            Attributes = entity.Attributes.Select(b => b.Key)
                        .Union(Attributes)
                        .Distinct()
                        .ToList<String>();

            Entities.Enqueue(entity);
        }

        public void RemoveAttributesWhereIdentical(Entity entity)
        {
            this.CommitIdentifier();

            var ReducedEntities = new Queue<Entity>();

            while (this.Entities.Count > 0)
            {
                var e = this.Entities.Dequeue();
                String EntityIdentifier = GetIdentifierFromEntity(e);
                String EntityToMatch = GetIdentifierFromEntity(entity);

                if (EntityIdentifier.Equals(EntityToMatch))
                {
                    var newAttributes = e.Attributes.Except(entity.Attributes, new AttributeComparer()).ToList();

                    if (newAttributes.Count == 0)
                        continue;

                    e.SetSealedPropertyValue("Attributes", new AttributeCollection());
                    foreach (var attribute in newAttributes)
                    {
                        e[attribute.Key] = attribute.Value;
                    }
                }

                ReducedEntities.Enqueue(e);
            }

            while (ReducedEntities.Count > 0)
                this.Entities.Enqueue(ReducedEntities.Dequeue());
        }

        public void RemoveRelationshipsWhereIdentical(String relationshipName, Dictionary<Guid, List<Guid>> relatedEntities)
        {
            if (!RelatedEntities.ContainsKey(relationshipName))
                return;

            foreach (var id in relatedEntities.Keys)
            {
                if (!RelatedEntities[relationshipName].ContainsKey(id))
                    return;

                var newRelatedIds = this.RelatedEntities[relationshipName][id].Except(relatedEntities[id]);
                if (newRelatedIds.Count() == 0)
                {
                    this.RelatedEntities[relationshipName].Remove(id);
                    if (this.RelatedEntities[relationshipName].Count == 0)
                        this.RelatedEntities.Remove(relationshipName);
                } else
                {
                    this.RelatedEntities[relationshipName][id] = newRelatedIds.ToList();
                }
            }
        }

        public void AppendM2MDataToEntity(String relationshipName, Dictionary<Guid, List<Guid>> relatedEntities)
        {
            if (!RelatedEntities.ContainsKey(relationshipName))
                this.RelatedEntities[relationshipName] = new Dictionary<Guid, List<Guid>>();

            foreach (var id in relatedEntities.Keys)
            {
                if (!RelatedEntities[relationshipName].ContainsKey(id))
                    this.RelatedEntities[relationshipName][id] = new List<Guid>();

                this.RelatedEntities[relationshipName][id].AddRange(relatedEntities[id]);
                this.RelatedEntities[relationshipName][id] = RelatedEntities[relationshipName][id].Distinct().ToList();
            }
        }

        public void CommitIdentifier()
        {
            // Default to the Guid as the identifier
            if (Identifiers.Count == 0)
            {
                Identifiers.Add(Metadata.PrimaryIdAttribute);
            }

            // Add attribute matching the primary ID if it wasn't provided
            if (Identifiers.Contains(Metadata.PrimaryIdAttribute))
            {
                foreach (var record in this.Entities)
                {
                    if (!String.IsNullOrEmpty(record.Id.ToString()))
                        record[Metadata.PrimaryIdAttribute] = record.Id;
                }
            }

            // Calculate what records exist when the identifier is enforced
            ReduceEntitiesBasedOnIdentifier();
        }

        private void ReduceEntitiesBasedOnIdentifier()
        {
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
                }
                else
                {
                    DistinctEntities.Add(EntityIdentifier, entity);
                }
            }

            // Rebuild list of entities based on an enforced identifier
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

    internal class AttributeComparer : IEqualityComparer<KeyValuePair<string, object>>
    {
        public Dictionary<Type, Func<Object, Object, Boolean>> Comparisons = new Dictionary<Type, Func<Object, Object, Boolean>>()
        {
            { typeof(EntityCollection), (a, b) =>
            {
                var c = (EntityCollection)a;
                var d = (EntityCollection)b;
                if (c.EntityName != d.EntityName)
                    return false;

                if (c.Entities.Count != d.Entities.Count)
                    return false;

                if (c.Entities.Select(x => x.Id).Except(d.Entities.Select(x => x.Id)).Count() != 0)
                    return false;

                foreach (var entity1 in c.Entities)
                {
                    foreach (var entity2 in d.Entities)
                    {
                        if (entity1.Id.Equals(entity2.Id))
                        {
                            if (entity1.Attributes.Except(entity2.Attributes, new AttributeComparer()).Count() != 0)
                            {
                                return false;
                            }
                        }
	                }
	            }

                return true;
            } },
            { typeof(Double), (a, b) => { return Double.Equals(a, b); } },
            { typeof(Money), (a, b) => { return Money.Equals(a, b); } },
            { typeof(OptionSetValue), (a, b) => {
                var c = (OptionSetValue)a;
                var d = (OptionSetValue)b;
                return Int32.Equals(c.Value, d.Value);
            } },
            { typeof(EntityReference), (a, b) => {
                var c = (EntityReference)a;
                var d = (EntityReference)b;
                return (Guid.Equals(c.Id, d.Id) && String.Equals(c.LogicalName, d.LogicalName));
            } },
            { typeof(Boolean), (a, b) => { return Boolean.Equals(a, b); } },
            { typeof(String), (a, b) => { return String.Equals(a, b); } },
            { typeof(Decimal), (a, b) => { return Decimal.Equals(a, b); } },
            { typeof(Guid), (a, b) => { return Guid.Equals(a, b); } },
            { typeof(Int32), (a, b) => { return Int32.Equals(a, b); } },
            { typeof(DateTime), (a, b) => { return DateTime.Equals(a, b); } }
        };

        public Dictionary<Type, Func<Object, Int32>> HashCodes = new Dictionary<Type, Func<Object, Int32>>()
        {
            { typeof(EntityCollection), (b) =>
            {
                var d = (EntityCollection)b;
                var hash = String.IsNullOrEmpty(d.EntityName) ? 1 : d.EntityName.GetHashCode();
                foreach (var entity in (d.Entities))
                    hash += entity.Id.ToString().GetHashCode();

                return hash;
            } },
            { typeof(Double), (b) => { return b.GetHashCode(); } },
            { typeof(Money), (b) => { return b.GetHashCode(); } },
            { typeof(OptionSetValue), (b) => {
                var d = (OptionSetValue)b;
                return (d.Value.GetHashCode());
            } },
            { typeof(EntityReference), (b) => {
                var d = (EntityReference)b;
                return (d.Id.ToString().GetHashCode() + d.LogicalName.GetHashCode());
            } },
            { typeof(Boolean), (b) => { return b.GetHashCode(); } },
            { typeof(String), (b) => { return b.GetHashCode(); } },
            { typeof(Decimal), (b) => { return b.GetHashCode(); } },
            { typeof(Guid), (b) => { return b.GetHashCode(); } },
            { typeof(Int32), (b) => { return b.GetHashCode(); } },
            { typeof(DateTime), (b) => { return b.GetHashCode(); } }
        };

        public bool Equals(KeyValuePair<string, object> x, KeyValuePair<string, object> y)
        {
            var type = x.Value.GetType();

            if (!type.Equals(y.Value.GetType()))
                return false;

            try
            {
                return Comparisons[type](x.Value, y.Value);
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Invalid type {0}: {1}", type, ex.Message));
            }   
        }

        public int GetHashCode(KeyValuePair<string, object> obj)
        {
            var type = obj.Value.GetType();

            try
            {
                return HashCodes[obj.Value.GetType()](obj.Value);
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Invalid type {0}: {1}", type, ex));
            }
                
        }
    }
}
