using System.Collections.Generic;
using System.Xml.Serialization;

namespace Microsoft.Xrm.DevOps.Data.SchemaXml
{
    [XmlRoot(ElementName = "entities")]
    public class Entities
    {
        [XmlElement(ElementName = "entity")]
        public List<Entity> Entity { get; set; }
    }

    [XmlRoot(ElementName = "entity")]
    public class Entity
    {
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
        [XmlAttribute(AttributeName = "displayname")]
        public string Displayname { get; set; }
        [XmlAttribute(AttributeName = "etc")]
        public string Etc { get; set; }
        [XmlAttribute(AttributeName = "primaryidfield")]
        public string Primaryidfield { get; set; }
        [XmlAttribute(AttributeName = "primarynamefield")]
        public string Primarynamefield { get; set; }
        [XmlAttribute(AttributeName = "disableplugins")]
        public string Disableplugins { get; set; }
        [XmlElement(ElementName = "fields")]
        public Fields Fields { get; set; }
        [XmlElement(ElementName = "relationships")]
        public Relationships Relationships { get; set; }
    }

    [XmlRoot(ElementName = "field")]
    public class Field
    {
        [XmlAttribute(AttributeName = "updateCompare")]
        public string UpdateCompare { get; set; } 
        [XmlAttribute(AttributeName = "displayname")]
        public string Displayname { get; set; }
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
        [XmlAttribute(AttributeName = "type")]
        public string Type { get; set; }
        [XmlAttribute(AttributeName = "customfield")]
        public string Customfield { get; set; }
        [XmlAttribute(AttributeName = "dateMode")]
        public string DateMode { get; set; }
        [XmlAttribute(AttributeName = "lookupType")]
        public string LookupType { get; set; }
        [XmlAttribute(AttributeName = "primaryKey")]
        public string PrimaryKey { get; set; }
    }

    [XmlRoot(ElementName = "fields")]
    public class Fields
    {
        [XmlElement(ElementName = "field")]
        public List<Field> Field { get; set; }
    }

    [XmlRoot(ElementName = "relationship")]
    public class Relationship
    {
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
        [XmlAttribute(AttributeName = "manyToMany")]
        public string ManyToMany { get; set; }
        [XmlAttribute(AttributeName = "isreflexive")]
        public string Isreflexive { get; set; }
        [XmlAttribute(AttributeName = "relatedEntityName")]
        public string RelatedEntityName { get; set; }
        [XmlAttribute(AttributeName = "m2mTargetEntity")]
        public string M2mTargetEntity { get; set; }
        [XmlAttribute(AttributeName = "m2mTargetEntityPrimaryKey")]
        public string M2mTargetEntityPrimaryKey { get; set; }
    }

    [XmlRoot(ElementName = "relationships")]
    public class Relationships
    {
        [XmlElement(ElementName = "relationship")]
        public List<Relationship> Relationship { get; set; }
    }
}