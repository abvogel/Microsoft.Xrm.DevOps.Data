// LogicalName     : incident
// Id              : 616f5b50-774d-e911-a96a-000d3a1d23d3
// Attributes      : {[customerid, Microsoft.Xrm.Sdk.EntityReference], [incidentid, 616f5b50-774d-e911-a96a-000d3a1d23d3]}
// EntityState     : 
// FormattedValues : {[customerid, Andrew Vogel]}
// RelatedEntities : {}
// RowVersion      : 8003414
// KeyAttributes   : {}
// ExtensionData   : System.Runtime.Serialization.ExtensionDataObject

// Key        Value                               
// ---        -----                               
// customerid Microsoft.Xrm.Sdk.EntityReference   
// incidentid 616f5b50-774d-e911-a96a-000d3a1d23d3

// Dictionary:
// Key                       Value                                             
// ---                       -----                                             
// customerid_Property       [customerid, Microsoft.Xrm.Sdk.EntityReference]   
// customerid                Andrew Vogel                                      
// incidentid_Property       [incidentid, 616f5b50-774d-e911-a96a-000d3a1d23d3]
// incidentid                616f5b50-774d-e911-a96a-000d3a1d23d3              
// ReturnProperty_EntityName incident                                          
// ReturnProperty_Id         616f5b50-774d-e911-a96a-000d3a1d23d3         

// Id            : 9a913e9f-cbb8-e711-a968-000d3a192387
// LogicalName   : contact
// Name          : Andrew Vogel

using Microsoft.Xrm.Sdk;
using System;

namespace Microsoft.Xrm.DevOps.Data.Tests
{
    public partial class SupportMethods
    {
        public static Entity GetCustomerTypeEntity()
        {
            EntityReference er = new EntityReference("contact", Guid.Parse("9a913e9f-cbb8-e711-a968-000d3a192387"));
            er.Name = "Andrew Vogel";

            Entity result = new Entity("incident");
            result.Id = Guid.Parse("616f5b50-774d-e911-a96a-000d3a1d23d3");
            result["incidentid"] = Guid.Parse("616f5b50-774d-e911-a96a-000d3a1d23d3");
            result["customerid"] = er;
            result.FormattedValues.Add("customerid", "Andrew Vogel");

            return result;
        }
    }
}