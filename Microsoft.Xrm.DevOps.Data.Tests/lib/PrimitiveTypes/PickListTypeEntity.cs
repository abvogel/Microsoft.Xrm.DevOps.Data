// rf -fetch "<fetch top='5'><entity name='knowledgearticle'><attribute name='expiredreviewoptions'/><filter type='and'><condition attribute='expiredreviewoptions' operator='not-null'/></filter></entity></fetch>"
// Entity:
// LogicalName     : knowledgearticle
// Id              : 6ecdf2da-dae1-e711-9403-0003ff863885
// Attributes      : {[expiredreviewoptions, Microsoft.Xrm.Sdk.OptionSetValue], [knowledgearticleid, 6ecdf2da-dae1-e711-9403-0003ff863885]}
// EntityState     : 
// FormattedValues : {[expiredreviewoptions, Republish]}
// RelatedEntities : {}
// RowVersion      : 4051281
// KeyAttributes   : {}
// ExtensionData   : System.Runtime.Serialization.ExtensionDataObject

// Key                  Value                               
// ---                  -----                               
// expiredreviewoptions Microsoft.Xrm.Sdk.OptionSetValue    
// knowledgearticleid   6ecdf2da-dae1-e711-9403-0003ff863885

// Dictionary:
// Key                           Value                                                     
// ---                           -----                                                     
// expiredreviewoptions_Property [expiredreviewoptions, Microsoft.Xrm.Sdk.OptionSetValue]  
// expiredreviewoptions          Republish                                                 
// knowledgearticleid_Property   [knowledgearticleid, 6ecdf2da-dae1-e711-9403-0003ff863885]
// knowledgearticleid            6ecdf2da-dae1-e711-9403-0003ff863885                      
// ReturnProperty_EntityName     knowledgearticle                                          
// ReturnProperty_Id             6ecdf2da-dae1-e711-9403-0003ff863885                      

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk;
using System;

namespace Microsoft.Xrm.DevOps.Data.Tests
{
    public partial class SupportMethods
    {
        public static Entity GetPicklistTypeEntity()
        {
            Entity result = new Entity("knowledgearticle");
            result.Id = Guid.Parse("6ecdf2da-dae1-e711-9403-0003ff863885");
            result["knowledgearticleid"] = result.Id;
            result["expiredreviewoptions"] = new OptionSetValue(1);
            result.FormattedValues.Add("expiredreviewoptions", "Republish");

            return result;
        }
    }
}