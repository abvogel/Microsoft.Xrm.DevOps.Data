using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Xrm.DevOps.Data
{
    public class XmlDataBuilder
    {
        internal static XmlDocument ToXmlDocument(Dictionary<string, BuilderEntityMetadata> entities, Boolean pluginsdisabled)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(String.Empty.GetType());
            using (StringWriter textWriter = new StringWriter())
            {
                xmlSerializer.Serialize(textWriter, GenerateDataXml(entities));
                //results["data_schema.xml"] = textWriter.ToString();
            }

            return new XmlDocument();
        }

        private static DataXml.Entities GenerateDataXml(Dictionary<string, BuilderEntityMetadata> entities)
        {
            throw new NotImplementedException();
        }
    }
}
