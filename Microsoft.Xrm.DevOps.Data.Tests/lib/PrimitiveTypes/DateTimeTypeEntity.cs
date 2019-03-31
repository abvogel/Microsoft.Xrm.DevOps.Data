// RF -fetch "<fetch top='5'><entity name='knowledgearticle'><attribute name='publishon'/><filter type='and'><condition attribute='publishon' operator='not-null'/></filter></entity></fetch>";
// Entity:
// LogicalName     : knowledgearticle
// Id              : 6ecdf2da-dae1-e711-9403-0003ff863885
// Attributes      : {[publishon, 12/19/2017 4:57:00 PM], [knowledgearticleid, 
//                   6ecdf2da-dae1-e711-9403-0003ff863885]}
// EntityState     : 
// FormattedValues : {[publishon, 12/19/2017 10:57 AM]}
// RelatedEntities : {}
// RowVersion      : 4051281
// KeyAttributes   : {}
// ExtensionData   : System.Runtime.Serialization.ExtensionDataObject

// Key                Value                               
// ---                -----                               
// publishon          12/19/2017 4:57:00 PM               
// knowledgearticleid 6ecdf2da-dae1-e711-9403-0003ff863885

// Dictionary:
// Key                         Value                                                     
// ---                         -----                                                     
// publishon_Property          [publishon, 12/19/2017 4:57:00 PM]                        
// publishon                   12/19/2017 10:57 AM                                       
// knowledgearticleid_Property [knowledgearticleid, 6ecdf2da-dae1-e711-9403-0003ff863885]
// knowledgearticleid          6ecdf2da-dae1-e711-9403-0003ff863885                      
// ReturnProperty_EntityName   knowledgearticle                                          
// ReturnProperty_Id           6ecdf2da-dae1-e711-9403-0003ff863885          

using Microsoft.Xrm.Sdk;
using System;

namespace Microsoft.Xrm.DevOps.Data.Tests
{
    public partial class SupportMethods
    {
        public static Entity GetDateTimeTypeEntity()
        {
            Entity result = new Entity("knowledgearticle");
            result.Id = Guid.Parse("6ecdf2da-dae1-e711-9403-0003ff863885");
            result["knowledgearticleid"] = Guid.Parse("6ecdf2da-dae1-e711-9403-0003ff863885");
            result["publishon"] = DateTime.Parse("12/19/2017 10:57 AM");
            result.FormattedValues.Add("publishon", "12/19/2017 10:57 AM");

            return result;
        }

        public static String GetDateTimeTypeFetch()
        {
            return "<fetch top='1'><entity name='knowledgearticle'><attribute name='publishon'/><filter type='and'><condition attribute='publishon' operator='not-null'/></filter></entity></fetch>";
        }

        public static String GetDateTimeTypeExpectedData()
        {
            return LoadXmlFile(@"../../lib/PrimitiveTypes/DateTimeTypedata.xml");
        }

        public static String GetDateTimeTypeExpectedSchema()
        {
            return LoadXmlFile(@"../../lib/PrimitiveTypes/DateTimeTypedata_schema.xml");
        }
    }
}