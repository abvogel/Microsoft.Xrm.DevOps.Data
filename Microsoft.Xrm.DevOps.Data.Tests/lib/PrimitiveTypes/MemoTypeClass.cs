// RF -fetch "<fetch top='5'><entity name='knowledgearticle'><attribute name='keywords'/><filter type='and'><condition attribute='keywords' operator='not-null'/></filter></entity></fetch>"
// Entity:
// LogicalName     : knowledgearticle
// Id              : 4bfd843b-e99e-e711-9401-0003ff66fbff
// Attributes      : {[keywords, keywords here], [knowledgearticleid, 4bfd843b-e99e-e711-9401-0003ff66fbff]}
// EntityState     : 
// FormattedValues : {}
// RelatedEntities : {}
// RowVersion      : 4051075
// KeyAttributes   : {}
// ExtensionData   : System.Runtime.Serialization.ExtensionDataObject

// Key                Value                               
// ---                -----                               
// keywords           keywords here                       
// knowledgearticleid 4bfd843b-e99e-e711-9401-0003ff66fbff

// Dictionary:
// Key                         Value                                                     
// ---                         -----                                                     
// keywords_Property           [keywords, keywords here]                                 
// keywords                    keywords here                                             
// knowledgearticleid_Property [knowledgearticleid, 4bfd843b-e99e-e711-9401-0003ff66fbff]
// knowledgearticleid          4bfd843b-e99e-e711-9401-0003ff66fbff                      
// ReturnProperty_EntityName   knowledgearticle                                          
// ReturnProperty_Id           4bfd843b-e99e-e711-9401-0003ff66fbff                      

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk;
using System;

namespace Microsoft.Xrm.DevOps.Data.Tests
{
    public partial class SupportMethods
    {
        public static Entity GetMemoTypeClass()
        {
            Entity result = new Entity("knowledgearticle");
            result.Id = Guid.Parse("4bfd843b-e99e-e711-9401-0003ff66fbff");
            result["knowledgearticleid"] = result.Id;
            result["keywords"] = "keywords here";

            return result;
        }
    }
}