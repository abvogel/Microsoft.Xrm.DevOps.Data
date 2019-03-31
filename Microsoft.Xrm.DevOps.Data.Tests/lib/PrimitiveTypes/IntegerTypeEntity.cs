// RF -fetch "<fetch top='5'><entity name='knowledgearticle'><attribute name='expirationstateid'/><filter type='and'><condition attribute='expirationstateid' operator='not-null'/></filter></entity></fetch>";
// Entity:
// LogicalName     : knowledgearticle
// Id              : f835a513-ed9e-e711-9401-0003ff66fbff
// Attributes      : {[expirationstateid, 3], [knowledgearticleid, f835a513-ed9e-e711-9401-0003ff66fbff]}
// EntityState     : 
// FormattedValues : {[expirationstateid, 3]}
// RelatedEntities : {}
// RowVersion      : 3895327
// KeyAttributes   : {}
// ExtensionData   : System.Runtime.Serialization.ExtensionDataObject

// Key                                               Value
// ---                                               -----
// expirationstateid                                     3
// knowledgearticleid f835a513-ed9e-e711-9401-0003ff66fbff
 
// Dictionary:
// Key                         Value                                                     
// ---                         -----                                                     
// expirationstateid_Property  [expirationstateid, 3]                                    
// expirationstateid           3                                                         
// knowledgearticleid_Property [knowledgearticleid, f835a513-ed9e-e711-9401-0003ff66fbff]
// knowledgearticleid          f835a513-ed9e-e711-9401-0003ff66fbff                      
// ReturnProperty_EntityName   knowledgearticle                                          
// ReturnProperty_Id           f835a513-ed9e-e711-9401-0003ff66fbff     

using Microsoft.Xrm.Sdk;
using System;

namespace Microsoft.Xrm.DevOps.Data.Tests
{
    public partial class SupportMethods
    {
        public static Entity GetIntegerTypeEntity()
        {
            Entity result = new Entity("knowledgearticle");
            result.Id = Guid.Parse("f835a513-ed9e-e711-9401-0003ff66fbff");
            result["knowledgearticleid"] = result.Id;
            result["expirationstateid"] = Int32.Parse("3");
            result.FormattedValues.Add("expirationstateid", "3");

            return result;
        }

        public static String GetIntegerTypeFetch()
        {
            return "<fetch top='1'><entity name='knowledgearticle'><attribute name='expirationstateid'/><filter type='and'><condition attribute='expirationstateid' operator='not-null'/></filter></entity></fetch>";
        }
    }
}