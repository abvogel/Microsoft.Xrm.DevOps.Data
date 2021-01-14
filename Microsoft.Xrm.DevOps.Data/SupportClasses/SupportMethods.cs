using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Microsoft.Xrm.DevOps.Data.SupportClasses
{
    static class SupportMethods
    {

        public static void SetSealedPropertyValue(this Entity entity, string sPropertyName, object value)
        {
            entity.GetType().GetProperty(sPropertyName).SetValue(entity, value, null);
        }

        public static void SetSealedPropertyValue(this EntityMetadata entityMetadata, string sPropertyName, object value)
        {
            entityMetadata.GetType().GetProperty(sPropertyName).SetValue(entityMetadata, value, null);
        }

        public static void SetSealedPropertyValue(this AttributeMetadata attributeMetadata, string sPropertyName, object value)
        {
            attributeMetadata.GetType().GetProperty(sPropertyName).SetValue(attributeMetadata, value, null);
        }

        public static void SetFieldValue(this object inputObject, string propertyName, object propertyVal)
        {
            Type type = inputObject.GetType();
            System.Reflection.PropertyInfo propertyInfo = type.GetProperty(propertyName);
            Type propertyType = propertyInfo.PropertyType;
            var targetType = IsNullableType(propertyType) ? Nullable.GetUnderlyingType(propertyType) : propertyType;
            propertyVal = Convert.ChangeType(propertyVal, targetType);
            propertyInfo.SetValue(inputObject, propertyVal, null);
        }

        private static bool IsNullableType(Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition().Equals(typeof(Nullable<>));
        }

        public static List<Entity> RetrieveAllRecords(IOrganizationService service, string fetch)
        {
            int pageNumber = 1;
            bool moreRecords = false;
            List<Entity> Entities = new List<Entity>();
            var fetchXmlDoc = CreateXml(fetch);

            do
            {
                var retrievedRecords = service.RetrieveMultiple(new Sdk.Query.FetchExpression(fetchXmlDoc.InnerXml));
                moreRecords = retrievedRecords.MoreRecords;
                if (retrievedRecords.Entities.Count >= 0)
                    Entities.AddRange(retrievedRecords.Entities);

                if (moreRecords)
                {
                    fetchXmlDoc = UpdatePagingCookie(fetchXmlDoc, System.Security.SecurityElement.Escape(retrievedRecords.PagingCookie), ++pageNumber);
                }

            } while (moreRecords);
            return Entities;
        }

        public static string ExtractNodeValue(XmlNode parentNode, string name)
        {
            XmlNode childNode = parentNode.SelectSingleNode(name);

            if (null == childNode)
            {
                return null;
            }
            return childNode.InnerText;
        }

        public static string ExtractAttribute(XmlDocument doc, string name)
        {
            XmlAttributeCollection attrs = doc.DocumentElement.Attributes;
            var attr = (XmlAttribute)attrs.GetNamedItem(name);
            if (null == attr)
            {
                return null;
            }
            return attr.Value;
        }

        public static XmlDocument CreateXml(string xml)
        {
            StringReader stringReader = new StringReader(xml);
            var reader = new XmlTextReader(stringReader);

            XmlDocument doc = new XmlDocument();
            doc.Load(reader);

            return doc;
        }

        public static XmlDocument CreateXml(string xml, string cookie, int page)
        {
            StringReader stringReader = new StringReader(xml);
            var reader = new XmlTextReader(stringReader);

            XmlDocument doc = new XmlDocument();
            doc.Load(reader);

            return CreateXml(doc, cookie, page);
        }

        public static XmlDocument CreateXml(XmlDocument doc, string cookie, int page)
        {
            XmlAttributeCollection attrs = doc.DocumentElement.Attributes;

            if (cookie != null)
            {
                XmlAttribute pagingAttr = doc.CreateAttribute("paging-cookie");
                pagingAttr.InnerXml = cookie;
                attrs.Append(pagingAttr);
            }

            XmlAttribute pageAttr = doc.CreateAttribute("page");
            pageAttr.Value = System.Convert.ToString(page);
            attrs.Append(pageAttr);

            return doc;
        }

        public static XmlDocument UpdatePagingCookie(XmlDocument doc, string cookie, int pageNumber)
        {
            XmlAttributeCollection attrs = doc.DocumentElement.Attributes;

            XmlAttribute pagingAttr = doc.CreateAttribute("paging-cookie");
            pagingAttr.InnerXml = cookie;
            attrs.Append(pagingAttr);

            XmlAttribute pageAttr = doc.CreateAttribute("page");
            pageAttr.Value = System.Convert.ToString(pageNumber);
            attrs.Append(pageAttr);

            return doc;
        }
    }
}
