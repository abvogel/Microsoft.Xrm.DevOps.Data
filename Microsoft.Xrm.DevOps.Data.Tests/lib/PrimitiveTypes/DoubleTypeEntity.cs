// RF -fetch "<fetch top='5'><entity name='msdyn_purchaseorderproduct'><attribute name='msdyn_quantity'/><filter type='and'><condition attribute='msdyn_quantity' operator='not-null'/></filter></entity></fetch>";
// Entity:
// LogicalName     : msdyn_purchaseorderproduct
// Id              : fbb6f525-794d-e911-a96a-000d3a1d23d3
// Attributes      : {[msdyn_quantity, 73.25], [msdyn_purchaseorderproductid, 
//                   fbb6f525-794d-e911-a96a-000d3a1d23d3]}
// EntityState     : 
// FormattedValues : {[msdyn_quantity, 73.25]}
// RelatedEntities : {}
// RowVersion      : 8003640
// KeyAttributes   : {}
// ExtensionData   : System.Runtime.Serialization.ExtensionDataObject

// Key                                                         Value
// ---                                                         -----
// msdyn_quantity                                              73.25
// msdyn_purchaseorderproductid fbb6f525-794d-e911-a96a-000d3a1d23d3
 
// Dictionary:
// Key                                   Value                                                               
// ---                                   -----                                                               
// msdyn_quantity_Property               [msdyn_quantity, 73.25]                                             
// msdyn_quantity                        73.25                                                               
// msdyn_purchaseorderproductid_Property [msdyn_purchaseorderproductid, fbb6f525-794d-e911-a96a-000d3a1d23d3]
// msdyn_purchaseorderproductid          fbb6f525-794d-e911-a96a-000d3a1d23d3                                
// ReturnProperty_EntityName             msdyn_purchaseorderproduct                                          
// ReturnProperty_Id                     fbb6f525-794d-e911-a96a-000d3a1d23d3     

using Microsoft.Xrm.Sdk;
using System;

namespace Microsoft.Xrm.DevOps.Data.Tests
{
    public partial class SupportMethods
    {
        public static Entity GetDoubleTypeEntity()
        {
            Entity result = new Entity("msdyn_purchaseorderproduct");
            result.Id = Guid.Parse("fbb6f525-794d-e911-a96a-000d3a1d23d3");
            result["msdyn_purchaseorderproductid"] = result.Id;
            result["msdyn_quantity"] = Double.Parse("73.25");
            result.FormattedValues.Add("msdyn_quantity", "73.25");

            return result;
        }

        public static String GetDoubleTypeFetch()
        {
            return "<fetch top='1'><entity name='msdyn_purchaseorderproduct'><attribute name='msdyn_quantity'/><filter type='and'><condition attribute='msdyn_quantity' operator='not-null'/></filter></entity></fetch>";
        }
    }
}