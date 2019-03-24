// rf -fetch "<fetch top='5'><entity name='knowledgearticle'><attribute name='stageid'/><filter type='and'><condition attribute='stageid' operator='not-null'/></filter></entity></fetch>"
// Entity:
// LogicalName     : knowledgearticle
// Id              : a737336f-4472-e711-9401-0003ff86444f
// Attributes      : {[stageid, 0c6ed1c6-e9f1-4b43-8d9b-f680fded9b67], [knowledgearticleid, a737336f-4472-e711-9401-0003ff86444f]}
// EntityState     : 
// FormattedValues : {}
// RelatedEntities : {}
// RowVersion      : 4051294
// KeyAttributes   : {}
// ExtensionData   : System.Runtime.Serialization.ExtensionDataObject

// Key                Value                               
// ---                -----                               
// stageid            0c6ed1c6-e9f1-4b43-8d9b-f680fded9b67
// knowledgearticleid a737336f-4472-e711-9401-0003ff86444f

// Dictionary:
// Key                         Value                                                     
// ---                         -----                                                     
// stageid_Property            [stageid, 0c6ed1c6-e9f1-4b43-8d9b-f680fded9b67]           
// stageid                     0c6ed1c6-e9f1-4b43-8d9b-f680fded9b67                      
// knowledgearticleid_Property [knowledgearticleid, a737336f-4472-e711-9401-0003ff86444f]
// knowledgearticleid          a737336f-4472-e711-9401-0003ff86444f                      
// ReturnProperty_EntityName   knowledgearticle                                          
// ReturnProperty_Id           a737336f-4472-e711-9401-0003ff86444f                      

// $v2["stageid_Property"].Value
// Guid                                
// ----                                
// 0c6ed1c6-e9f1-4b43-8d9b-f680fded9b67

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk;
using System;

namespace Microsoft.Xrm.DevOps.Data.Tests
{
    public partial class SupportMethods
    {
        public static Entity GetUniqueIdentifierTypeEntity()
        {
            Entity result = new Entity("knowledgearticle");
            result.Id = Guid.Parse("a737336f-4472-e711-9401-0003ff86444f");
            result["knowledgearticleid"] = result.Id;
            result["stageid"] = Guid.Parse("0c6ed1c6-e9f1-4b43-8d9b-f680fded9b67");

            return result;
        }
    }
}