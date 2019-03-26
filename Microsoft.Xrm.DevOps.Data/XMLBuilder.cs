using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Microsoft.Xrm.DevOps.Data
{
    public class XMLBuilder
    {
        internal static Dictionary<String, String> ToStrings(Dictionary<string, BuilderEntityMetadata> entities)
        {
            var results = new Dictionary<String, String>();

            XmlSerializer xmlSerializer = new XmlSerializer(String.Empty.GetType());
            using (StringWriter textWriter = new StringWriter())
            {
                xmlSerializer.Serialize(textWriter, GetDataSchema(entities));
                results["data_schema.xml"] = textWriter.ToString();
            }

            using (StringWriter textWriter = new StringWriter())
            {
                xmlSerializer.Serialize(textWriter, GetData(entities));
                results["data.xml"] = textWriter.ToString();
            }

            return results;
        }

        private static String GetDataSchema(Dictionary<string, BuilderEntityMetadata> entities)
        {
            // TBD
            return String.Empty;
        }

        private static String GetData(Dictionary<string, BuilderEntityMetadata> entities)
        {
            // TBD
            return String.Empty;
        }
    }
}
