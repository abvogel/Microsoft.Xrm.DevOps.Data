// RF -fetch "<fetch top='5'><entity name='knowledgearticle'><attribute name='previousarticlecontentid'/><filter type='and'><condition attribute='previousarticlecontentid' operator='not-null'/></filter></entity></fetch>";
// Entity:
// LogicalName     : knowledgearticle
// Id              : 165ef3a6-ef9e-e711-80fd-3863bb2eb058
// Attributes      : {[previousarticlecontentid, Microsoft.Xrm.Sdk.EntityReference], [knowledgearticleid, 
//                   165ef3a6-ef9e-e711-80fd-3863bb2eb058]}
// EntityState     : 
// FormattedValues : {[previousarticlecontentid, demo Article for QA ]}
// RelatedEntities : {}
// RowVersion      : 4024132
// KeyAttributes   : {}
// ExtensionData   : System.Runtime.Serialization.ExtensionDataObject

// Key                      Value                               
// ---                      -----                               
// previousarticlecontentid Microsoft.Xrm.Sdk.EntityReference   
// knowledgearticleid       165ef3a6-ef9e-e711-80fd-3863bb2eb058

// Dictionary:
// Key                               Value                                                        
// ---                               -----                                                        
// previousarticlecontentid_Property [previousarticlecontentid, Microsoft.Xrm.Sdk.EntityReference]
// previousarticlecontentid          demo Article for QA                                          
// knowledgearticleid_Property       [knowledgearticleid, 165ef3a6-ef9e-e711-80fd-3863bb2eb058]   
// knowledgearticleid                165ef3a6-ef9e-e711-80fd-3863bb2eb058                         
// ReturnProperty_EntityName         knowledgearticle                                             
// ReturnProperty_Id                 165ef3a6-ef9e-e711-80fd-3863bb2eb058        

// Id            : f835a513-ed9e-e711-9401-0003ff66fbff
// LogicalName   : knowledgearticle
// Name          : demo Article for QA 

using Microsoft.Xrm.Sdk;
using System;

namespace Microsoft.Xrm.DevOps.Data.Tests
{
    public partial class SupportMethods
    {
        public static Entity GetLookupTypeEntity()
        {
            EntityReference er = new EntityReference("knowledgearticle", Guid.Parse("f835a513-ed9e-e711-9401-0003ff66fbff"));
            er.Name = "demo Article for QA ";

            Entity result = new Entity("knowledgearticle");
            result.Id = Guid.Parse("165ef3a6-ef9e-e711-80fd-3863bb2eb058");
            result["knowledgearticleid"] = result.Id;
            result["previousarticlecontentid"] = er;
            result.FormattedValues.Add("previousarticlecontentid", "demo Article for QA ");

            return result;
        }

        public static String GetLookupTypeFetch()
        {
            return "<fetch top='1'><entity name='knowledgearticle'><attribute name='previousarticlecontentid'/><filter type='and'><condition attribute='previousarticlecontentid' operator='not-null'/></filter></entity></fetch>";
        }
    }
}