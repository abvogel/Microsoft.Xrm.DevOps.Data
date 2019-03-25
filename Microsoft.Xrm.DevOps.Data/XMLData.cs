using System.Collections.Generic;
using System.Xml.Serialization;

namespace Microsoft.Xrm.DevOps.Data.XMLData
{
    [XmlRoot(ElementName = "attribute", Namespace = "http://www.w3.org/2001/XMLSchema")]
    public class Attribute
    {
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
        [XmlAttribute(AttributeName = "type")]
        public string Type { get; set; }
    }

    [XmlRoot(ElementName = "complexType", Namespace = "http://www.w3.org/2001/XMLSchema")]
    public class ComplexType
    {
        [XmlElement(ElementName = "attribute", Namespace = "http://www.w3.org/2001/XMLSchema")]
        public List<Attribute> Attribute { get; set; }
    }

    [XmlRoot(ElementName = "element", Namespace = "http://www.w3.org/2001/XMLSchema")]
    public class Element
    {
        [XmlElement(ElementName = "complexType", Namespace = "http://www.w3.org/2001/XMLSchema")]
        public ComplexType ComplexType { get; set; }
        [XmlAttribute(AttributeName = "maxOccurs")]
        public string MaxOccurs { get; set; }
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
    }

    [XmlRoot(ElementName = "schema", Namespace = "http://www.w3.org/2001/XMLSchema")]
    public class Schema
    {
        [XmlAttribute(AttributeName = "attributeFormDefault")]
        public string AttributeFormDefault { get; set; }
        [XmlElement(ElementName = "element", Namespace = "http://www.w3.org/2001/XMLSchema")]
        public Element Element { get; set; }
        [XmlAttribute(AttributeName = "elementFormDefault")]
        public string ElementFormDefault { get; set; }
        [XmlAttribute(AttributeName = "xs", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Xs { get; set; }
    }

    [XmlRoot(ElementName = "sequence", Namespace = "http://www.w3.org/2001/XMLSchema")]
    public class Sequence
    {
        [XmlElement(ElementName = "element", Namespace = "http://www.w3.org/2001/XMLSchema")]
        public Element Element { get; set; }
    }

}