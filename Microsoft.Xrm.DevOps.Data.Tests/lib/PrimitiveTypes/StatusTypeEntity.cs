// rf -fetch "<fetch top='5'><entity name='knowledgearticle'><attribute name='statuscode'/><filter type='and'><condition attribute='statuscode' operator='not-null'/></filter></entity></fetch>"
// Entity:
// LogicalName     : knowledgearticle
// Id              : 4bfd843b-e99e-e711-9401-0003ff66fbff
// Attributes      : {[statuscode, Microsoft.Xrm.Sdk.OptionSetValue], [knowledgearticleid, 4bfd843b-e99e-e711-9401-0003ff66fbff]}
// EntityState     : 
// FormattedValues : {[statuscode, Published]}
// RelatedEntities : {}
// RowVersion      : 4051075
// KeyAttributes   : {}
// ExtensionData   : System.Runtime.Serialization.ExtensionDataObject

// Key                Value                               
// ---                -----                               
// statuscode         Microsoft.Xrm.Sdk.OptionSetValue    
// knowledgearticleid 4bfd843b-e99e-e711-9401-0003ff66fbff
 
// Dictionary:
// Key                         Value                                                     
// ---                         -----                                                     
// statuscode_Property         [statuscode, Microsoft.Xrm.Sdk.OptionSetValue]            
// statuscode                  Published                                                 
// knowledgearticleid_Property [knowledgearticleid, 4bfd843b-e99e-e711-9401-0003ff66fbff]
// knowledgearticleid          4bfd843b-e99e-e711-9401-0003ff66fbff                      
// ReturnProperty_EntityName   knowledgearticle                                          
// ReturnProperty_Id           4bfd843b-e99e-e711-9401-0003ff66fbff                      

// $v2["statuscode_Property"].Value
// Value ExtensionData                                   
// ----- -------------                                   
//     7 System.Runtime.Serialization.ExtensionDataObject

using Microsoft.Xrm.Sdk;
using System;

namespace Microsoft.Xrm.DevOps.Data.Tests
{
    public partial class SupportMethods
    {
        public static Entity GetStatusTypeEntity()
        {
            Entity result = new Entity("knowledgearticle");
            result.Id = Guid.Parse("4bfd843b-e99e-e711-9401-0003ff66fbff");
            result["knowledgearticleid"] = result.Id;
            result["statuscode"] = new OptionSetValue(7);
            result.FormattedValues.Add("statecode", "Published");

            return result;
        }
    }
}