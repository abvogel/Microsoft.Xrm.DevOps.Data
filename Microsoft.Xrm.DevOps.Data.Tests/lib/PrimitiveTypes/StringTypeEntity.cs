// rf -fetch "<fetch top='5'><entity name='knowledgearticle'><attribute name='description'/><filter type='and'><condition attribute='description' operator='not-null'/></filter></entity></fetch>"
// Entity:
// LogicalName     : knowledgearticle
// Id              : 4bfd843b-e99e-e711-9401-0003ff66fbff
// Attributes      : {[description, Testing the new search fields], [knowledgearticleid, 4bfd843b-e99e-e711-9401-0003ff66fbff]}
// EntityState     : 
// FormattedValues : {}
// RelatedEntities : {}
// RowVersion      : 4051075
// KeyAttributes   : {}
// ExtensionData   : System.Runtime.Serialization.ExtensionDataObject

// Key                Value                               
// ---                -----                               
// description        Testing the new search fields       
// knowledgearticleid 4bfd843b-e99e-e711-9401-0003ff66fbff

// Dictionary:
// Key                         Value                                                     
// ---                         -----                                                     
// description_Property        [description, Testing the new search fields]              
// description                 Testing the new search fields                             
// knowledgearticleid_Property [knowledgearticleid, 4bfd843b-e99e-e711-9401-0003ff66fbff]
// knowledgearticleid          4bfd843b-e99e-e711-9401-0003ff66fbff                      
// ReturnProperty_EntityName   knowledgearticle                                          
// ReturnProperty_Id           4bfd843b-e99e-e711-9401-0003ff66fbff                      

using Microsoft.Xrm.Sdk;
using System;

namespace Microsoft.Xrm.DevOps.Data.Tests
{
    public partial class SupportMethods
    {
        public static Entity GetStringTypeEntity()
        {
            Entity result = new Entity("knowledgearticle");
            result.Id = Guid.Parse("4bfd843b-e99e-e711-9401-0003ff66fbff");
            result["knowledgearticleid"] = result.Id;
            result["description"] = "Testing the new search fields";

            return result;
        }

        public static String GetStringTypeFetch()
        {
            return "<fetch top='1'><entity name='knowledgearticle'><attribute name='description'/><filter type='and'><condition attribute='description' operator='not-null'/></filter></entity></fetch>";
        }
    }
}