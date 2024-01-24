// RF -fetch "<fetch top='5'><entity name='msdyn_resourcerequirementdetail'><attribute name='msdyn_hours'/><filter type='and'><condition attribute='msdyn_hours' operator='not-null'/></filter></entity></fetch>";
// Entity:
// LogicalName     : msdyn_resourcerequirementdetail
// Id              : 73d453d8-774d-e911-a96a-000d3a1d23d3
// Attributes      : {[msdyn_hours, 5.0000000000], [msdyn_resourcerequirementdetailid, 
//                   73d453d8-774d-e911-a96a-000d3a1d23d3]}
// EntityState     : 
// FormattedValues : {[msdyn_hours, 5.00]}
// RelatedEntities : {}
// RowVersion      : 8003533
// KeyAttributes   : {}
// ExtensionData   : System.Runtime.Serialization.ExtensionDataObject

// Key                                                              Value
// ---                                                              -----
// msdyn_hours                                               5.0000000000
// msdyn_resourcerequirementdetailid 73d453d8-774d-e911-a96a-000d3a1d23d3

// Dictionary:
// Key                                        Value                                                           
// ---                                        -----                                                           
// msdyn_hours_Property                       [msdyn_hours, 5.0000000000]                                     
// msdyn_hours                                5.00                                                            
// msdyn_resourcerequirementdetailid_Property [msdyn_resourcerequirementdetailid, 73d453d8-774d-e911-a96a-0...
// msdyn_resourcerequirementdetailid          73d453d8-774d-e911-a96a-000d3a1d23d3                            
// ReturnProperty_EntityName                  msdyn_resourcerequirementdetail                                 
// ReturnProperty_Id                          73d453d8-774d-e911-a96a-000d3a1d23d3      

using Microsoft.Xrm.Sdk;
using System;
using System.Globalization;

namespace Microsoft.Xrm.DevOps.Data.Tests
{
    public partial class SupportMethods
    {
        public static Entity GetDecimalTypeEntity()
        {
            Entity result = new Entity("msdyn_resourcerequirementdetail");
            result.Id = Guid.Parse("73d453d8-774d-e911-a96a-000d3a1d23d3");
            result["msdyn_resourcerequirementdetailid"] = result.Id;
            result["msdyn_hours"] = Decimal.Parse("5.0000000000".NormalizeSeparator());
            result.FormattedValues.Add("msdyn_hours", "5.00".NormalizeSeparator());

            return result;
        }

        public static String GetDecimalTypeFetch()
        {
            return "<fetch top='1'><entity name='msdyn_resourcerequirementdetail'><attribute name='msdyn_hours'/><filter type='and'><condition attribute='msdyn_hours' operator='not-null'/></filter></entity></fetch>";
        }

        public static String GetDecimalTypeExpectedData(Separator? separator = null)
        {
            return UseCommaSeparatedData(separator)
                ? LoadXmlFile(@"../../lib/PrimitiveTypes/DecimalTypedata_comma.xml")
                : LoadXmlFile(@"../../lib/PrimitiveTypes/DecimalTypedata.xml");
        }

        public static String GetDecimalTypeExpectedSchema()
        {
            return LoadXmlFile(@"../../lib/PrimitiveTypes/DecimalTypedata_schema.xml");
        }
    }
}