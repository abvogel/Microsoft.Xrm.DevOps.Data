using System.Collections.Generic;
using System.Xml.Serialization;

namespace Microsoft.Xrm.DevOps.Data.DataXml
{
	[XmlRoot(ElementName="activitypointerrecords")]
	public class Activitypointerrecords {
		[XmlElement(ElementName="field")]
		public List<Field> Field { get; set; }
		[XmlAttribute(AttributeName="id")]
		public string Id { get; set; }
	}

	[XmlRoot(ElementName="entities")]
	public class Entities {
		[XmlElement(ElementName="entity")]
		public List<Entity> Entity { get; set; }
		[XmlAttribute(AttributeName="timestamp")]
		public string Timestamp { get; set; }
		[XmlAttribute(AttributeName="xsd", Namespace="http://www.w3.org/2000/xmlns/")]
		public string Xsd { get; set; }
		[XmlAttribute(AttributeName="xsi", Namespace="http://www.w3.org/2000/xmlns/")]
		public string Xsi { get; set; }
	}

    [XmlRoot(ElementName = "entity")]
    public class Entity
    {
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
        [XmlAttribute(AttributeName = "displayname")]
        public string Displayname { get; set; }
		[XmlElement(ElementName="records")]
		public Records Records { get; set; }
        [XmlElement(ElementName = "m2mrelationships", IsNullable = true)]
        public M2mrelationships M2mrelationships
        {
            get
            {
                return new M2mrelationships();
            }
            set
            {
            }
        }

        public bool ShouldSerializem2mrelationships()
        {
            return true;
        }
	}

	[XmlRoot(ElementName="field")]
	public class Field
    {
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
        [XmlAttribute(AttributeName = "value")]
        public string Value { get; set; }
        [XmlAttribute(AttributeName = "lookupentity")]
        public string Lookupentity { get; set; }
        [XmlAttribute(AttributeName = "lookupentityname")]
        public string Lookupentityname { get; set; }
        [XmlElement(ElementName="activitypointerrecords")]
		public List<Activitypointerrecords> Activitypointerrecords { get; set; }
	}

	[XmlRoot(ElementName="m2mrelationship")]
	public class M2mrelationship {
		[XmlAttribute(AttributeName="m2mrelationshipname")]
		public string M2mrelationshipname { get; set; }
		[XmlAttribute(AttributeName="sourceid")]
		public string Sourceid { get; set; }
		[XmlAttribute(AttributeName="targetentityname")]
		public string Targetentityname { get; set; }
		[XmlAttribute(AttributeName="targetentitynameidfield")]
		public string Targetentitynameidfield { get; set; }
		[XmlElement(ElementName="targetids")]
		public Targetids Targetids { get; set; }
	}

	[XmlRoot(ElementName="m2mrelationships")]
	public class M2mrelationships {
		[XmlElement(ElementName="m2mrelationship")]
		public List<M2mrelationship> M2mrelationship { get; set; }
	}

	[XmlRoot(ElementName="record")]
	public class Record {
		[XmlElement(ElementName="field")]
		public List<Field> Field { get; set; }
		[XmlAttribute(AttributeName="id")]
		public string Id { get; set; }
	}

	[XmlRoot(ElementName="records")]
	public class Records {
		[XmlElement(ElementName="record")]
		public List<Record> Record { get; set; }
	}

	[XmlRoot(ElementName="targetids")]
	public class Targetids {
		[XmlElement(ElementName="targetid")]
		public List<string> Targetid { get; set; }
	}

}