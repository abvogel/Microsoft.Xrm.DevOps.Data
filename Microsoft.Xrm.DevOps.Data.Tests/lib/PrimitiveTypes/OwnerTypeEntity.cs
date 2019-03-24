// rf -fetch "<fetch top='5'><entity name='knowledgearticle'><attribute name='ownerid'/><filter type='and'><condition attribute='ownerid' operator='not-null'/></filter></entity></fetch>"
// Entity:
// LogicalName     : knowledgearticle
// Id              : 4bfd843b-e99e-e711-9401-0003ff66fbff
// Attributes      : {[ownerid, Microsoft.Xrm.Sdk.EntityReference], [knowledgearticleid, 4bfd843b-e99e-e711-9401-0003ff66fbff]}
// EntityState     : 
// FormattedValues : {[ownerid, Andrew Vogel]}
// RelatedEntities : {}
// RowVersion      : 4051075
// KeyAttributes   : {}
// ExtensionData   : System.Runtime.Serialization.ExtensionDataObject

// Key                Value                               
// ---                -----                               
// ownerid            Microsoft.Xrm.Sdk.EntityReference   
// knowledgearticleid 4bfd843b-e99e-e711-9401-0003ff66fbff
 
// Dictionary:
// Key                         Value                                                     
// ---                         -----                                                     
// ownerid_Property            [ownerid, Microsoft.Xrm.Sdk.EntityReference]              
// ownerid                     Andrew Vogel                                             
// knowledgearticleid_Property [knowledgearticleid, 4bfd843b-e99e-e711-9401-0003ff66fbff]
// knowledgearticleid          4bfd843b-e99e-e711-9401-0003ff66fbff                      
// ReturnProperty_EntityName   knowledgearticle                                          
// ReturnProperty_Id           4bfd843b-e99e-e711-9401-0003ff66fbff                      

// Id            : cdffaae5-55b0-e711-a95c-000d3a192e9a
// LogicalName   : systemuser
// Name          : Kishore Bethi

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk;
using System;

namespace Microsoft.Xrm.DevOps.Data.Tests
{
    public partial class SupportMethods
    {
        public static Entity GetOwnerTypeEntity()
        {
            EntityReference o = new EntityReference("systemuser", Guid.Parse("cdffaae5-55b0-e711-a95c-000d3a192e9a"));
                o.Name = "Andrew Vogel";

            Entity result = new Entity("knowledgearticle");
            result.Id = Guid.Parse("4bfd843b-e99e-e711-9401-0003ff66fbff");
            result["knowledgearticleid"] = result.Id;
            result["ownerid"] = o;
            result.FormattedValues.Add("ownerid", "Andrew Vogel");

            return result;
        }
    }
}