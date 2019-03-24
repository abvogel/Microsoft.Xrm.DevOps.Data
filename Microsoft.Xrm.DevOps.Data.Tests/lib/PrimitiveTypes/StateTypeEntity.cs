// rf -fetch "<fetch top='5'><entity name='knowledgearticle'><attribute name='statecode'/><filter type='and'><condition attribute='statecode' operator='not-null'/></filter></entity></fetch>"
// Entity:
// LogicalName     : knowledgearticle
// Id              : 4bfd843b-e99e-e711-9401-0003ff66fbff
// Attributes      : {[statecode, Microsoft.Xrm.Sdk.OptionSetValue], [knowledgearticleid, 4bfd843b-e99e-e711-9401-0003ff66fbff]}
// EntityState     : 
// FormattedValues : {[statecode, Published]}
// RelatedEntities : {}
// RowVersion      : 4051075
// KeyAttributes   : {}
// ExtensionData   : System.Runtime.Serialization.ExtensionDataObject

// Key                Value                               
// ---                -----                               
// statecode          Microsoft.Xrm.Sdk.OptionSetValue    
// knowledgearticleid 4bfd843b-e99e-e711-9401-0003ff66fbff

// Dictionary:
// Key                         Value                                                     
// ---                         -----                                                     
// statecode_Property          [statecode, Microsoft.Xrm.Sdk.OptionSetValue]             
// statecode                   Published                                                 
// knowledgearticleid_Property [knowledgearticleid, 4bfd843b-e99e-e711-9401-0003ff66fbff]
// knowledgearticleid          4bfd843b-e99e-e711-9401-0003ff66fbff                      
// ReturnProperty_EntityName   knowledgearticle                                          
// ReturnProperty_Id           4bfd843b-e99e-e711-9401-0003ff66fbff                  

// $v2["statecode_Property"].Value
// Value ExtensionData                                   
// ----- -------------                                   
//     3 System.Runtime.Serialization.ExtensionDataObject    

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk;
using System;

namespace Microsoft.Xrm.DevOps.Data.Tests
{
    public partial class SupportMethods
    {
        public static Entity GetStateTypeEntity()
        {
            Entity result = new Entity("knowledgearticle");
            result.Id = Guid.Parse("4bfd843b-e99e-e711-9401-0003ff66fbff");
            result["knowledgearticleid"] = result.Id;
            result["statecode"] = new OptionSetValue(3);
            result.FormattedValues.Add("statecode", "Published");

            return result;
        }
    }
}