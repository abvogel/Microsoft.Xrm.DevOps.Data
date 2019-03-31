// RF -fetch "<fetch><entity name='systemuser'><attribute name='systemuserid'/><link-entity name='systemuserroles' from='systemuserid' to='systemuserid' intersect='true'><link-entity name='role' from='roleid' to='roleid'/><attribute name='roleid'/></link-entity><filter><condition attribute='systemuserid' operator='eq' value='00e7b0b9-1ace-e711-a970-000d3a192311'/></filter></entity></fetch>";
// Entity:
// LogicalName     : systemuser
// Id              : 00e7b0b9-1ace-e711-a970-000d3a192311
// Attributes      : {[systemuserid, 00e7b0b9-1ace-e711-a970-000d3a192311], [systemuserroles1.roleid, Microsoft.Xrm.Sdk.AliasedValue]}
// EntityState     : 
// FormattedValues : {}
// RelatedEntities : {}
// RowVersion      : 6412206
// KeyAttributes   : {}
// ExtensionData   : System.Runtime.Serialization.ExtensionDataObject

// Key                     Value                               
// ---                     -----                               
// systemuserid            00e7b0b9-1ace-e711-a970-000d3a192311
// systemuserroles1.roleid Microsoft.Xrm.Sdk.AliasedValue      
// $v1.Attributes["systemuserroles1.roleid"]
// AttributeLogicalName EntityLogicalName Value                                ExtensionData                                   
// -------------------- ----------------- -----                                -------------                                   
// roleid               systemuserroles   cad52a75-568c-e611-80d4-00155d42a122 System.Runtime.Serialization.ExtensionDataObject
 
// $resultsec.Entities.Attributes | Where { $_.key -eq "systemuserroles1.roleid" } | Select -ExpandProperty Value
// AttributeLogicalName EntityLogicalName Value                                ExtensionData                                   
// -------------------- ----------------- -----                                -------------                                   
// roleid               systemuserroles   cad52a75-568c-e611-80d4-00155d42a122 System.Runtime.Serialization.ExtensionDataObject
// roleid               systemuserroles   3dac4d9e-568c-e611-80d4-00155d42a122 System.Runtime.Serialization.ExtensionDataObject
// roleid               systemuserroles   85d865b5-fea1-e611-8116-00155da84013 System.Runtime.Serialization.ExtensionDataObject
// roleid               systemuserroles   e186d6f0-28d6-4bfc-bea5-71e44c3e13a4 System.Runtime.Serialization.ExtensionDataObject
// roleid               systemuserroles   52d9415f-86f4-e611-8128-e0071b6a4261 System.Runtime.Serialization.ExtensionDataObject

// Dictionary:
// Key                              Value                                                    
// ---                              -----                                                    
// systemuserid_Property            [systemuserid, 00e7b0b9-1ace-e711-a970-000d3a192311]     
// systemuserid                     00e7b0b9-1ace-e711-a970-000d3a192311                     
// systemuserroles1.roleid_Property [systemuserroles1.roleid, Microsoft.Xrm.Sdk.AliasedValue]
// systemuserroles1.roleid          Microsoft.Xrm.Sdk.AliasedValue                           
// ReturnProperty_EntityName        systemuser                                               
// ReturnProperty_Id                00e7b0b9-1ace-e711-a970-000d3a192311                     
// systemuserid_Property            [systemuserid, 00e7b0b9-1ace-e711-a970-000d3a192311]     
// systemuserid                     00e7b0b9-1ace-e711-a970-000d3a192311                     
// systemuserroles1.roleid_Property [systemuserroles1.roleid, Microsoft.Xrm.Sdk.AliasedValue]
// systemuserroles1.roleid          Microsoft.Xrm.Sdk.AliasedValue                           
// ReturnProperty_EntityName        systemuser                                               
// ReturnProperty_Id                00e7b0b9-1ace-e711-a970-000d3a192311                     
// systemuserid_Property            [systemuserid, 00e7b0b9-1ace-e711-a970-000d3a192311]     
// systemuserid                     00e7b0b9-1ace-e711-a970-000d3a192311                     
// systemuserroles1.roleid_Property [systemuserroles1.roleid, Microsoft.Xrm.Sdk.AliasedValue]
// systemuserroles1.roleid          Microsoft.Xrm.Sdk.AliasedValue                           
// ReturnProperty_EntityName        systemuser                                               
// ReturnProperty_Id                00e7b0b9-1ace-e711-a970-000d3a192311                     
// systemuserid_Property            [systemuserid, 00e7b0b9-1ace-e711-a970-000d3a192311]     
// systemuserid                     00e7b0b9-1ace-e711-a970-000d3a192311                     
// systemuserroles1.roleid_Property [systemuserroles1.roleid, Microsoft.Xrm.Sdk.AliasedValue]
// systemuserroles1.roleid          Microsoft.Xrm.Sdk.AliasedValue                           
// ReturnProperty_EntityName        systemuser                                               
// ReturnProperty_Id                00e7b0b9-1ace-e711-a970-000d3a192311                     
// systemuserid_Property            [systemuserid, 00e7b0b9-1ace-e711-a970-000d3a192311]     
// systemuserid                     00e7b0b9-1ace-e711-a970-000d3a192311                     
// systemuserroles1.roleid_Property [systemuserroles1.roleid, Microsoft.Xrm.Sdk.AliasedValue]
// systemuserroles1.roleid          Microsoft.Xrm.Sdk.AliasedValue                           
// ReturnProperty_EntityName        systemuser                                               
// ReturnProperty_Id                00e7b0b9-1ace-e711-a970-000d3a192311                     

// v2.values | Where { $_.key -eq "systemuserroles1.roleid" } | Select -ExpandProperty Value
// AttributeLogicalName EntityLogicalName Value                                ExtensionData                                   
// -------------------- ----------------- -----                                -------------                                   
// roleid               systemuserroles   cad52a75-568c-e611-80d4-00155d42a122 System.Runtime.Serialization.ExtensionDataObject
// roleid               systemuserroles   3dac4d9e-568c-e611-80d4-00155d42a122 System.Runtime.Serialization.ExtensionDataObject
// roleid               systemuserroles   85d865b5-fea1-e611-8116-00155da84013 System.Runtime.Serialization.ExtensionDataObject
// roleid               systemuserroles   e186d6f0-28d6-4bfc-bea5-71e44c3e13a4 System.Runtime.Serialization.ExtensionDataObject
// roleid               systemuserroles   52d9415f-86f4-e611-8128-e0071b6a4261 System.Runtime.Serialization.ExtensionDataObject

using Microsoft.Xrm.Sdk;
using System;

namespace Microsoft.Xrm.DevOps.Data.Tests
{
    public partial class SupportMethods
    {
        public static DataCollection<Entity> Getm2mRelationshipTypeEntity()
        {
            AliasedValue av1 = new AliasedValue("systemuserroles", "roleid", Guid.Parse("cad52a75-568c-e611-80d4-00155d42a122"));
            Entity result1 = new Entity("systemuser");
            result1.Id = Guid.Parse("00e7b0b9-1ace-e711-a970-000d3a192311");
            result1["systemuserid"] = result1.Id;
            result1["systemuserroles1.roleid"] = av1;

            AliasedValue av2 = new AliasedValue("systemuserroles", "roleid", Guid.Parse("3dac4d9e-568c-e611-80d4-00155d42a122"));
            Entity result2 = new Entity("systemuser");
            result2.Id = Guid.Parse("00e7b0b9-1ace-e711-a970-000d3a192311");
            result2["systemuserid"] = result2.Id;
            result2["systemuserroles1.roleid"] = av2;

            AliasedValue av3 = new AliasedValue("systemuserroles", "roleid", Guid.Parse("85d865b5-fea1-e611-8116-00155da84013"));
            Entity result3 = new Entity("systemuser");
            result3.Id = Guid.Parse("00e7b0b9-1ace-e711-a970-000d3a192311");
            result3["systemuserid"] = result3.Id;
            result3["systemuserroles1.roleid"] = av3;
            
            AliasedValue av4 = new AliasedValue("systemuserroles", "roleid", Guid.Parse("e186d6f0-28d6-4bfc-bea5-71e44c3e13a4"));
            Entity result4 = new Entity("systemuser");
            result4.Id = Guid.Parse("00e7b0b9-1ace-e711-a970-000d3a192311");
            result4["systemuserid"] = result4.Id;
            result4["systemuserroles1.roleid"] = av4;
            
            AliasedValue av5 = new AliasedValue("systemuserroles", "roleid", Guid.Parse("52d9415f-86f4-e611-8128-e0071b6a4261"));
            Entity result5 = new Entity("systemuser");
            result5.Id = Guid.Parse("00e7b0b9-1ace-e711-a970-000d3a192311");
            result5["systemuserid"] = result5.Id;
            result5["systemuserroles1.roleid"] = av5;
            
            System.Collections.ObjectModel.Collection<Entity> DC = new System.Collections.ObjectModel.Collection<Entity>();
            DC.Add(result1);
            DC.Add(result2);
            DC.Add(result3);
            DC.Add(result4);
            DC.Add(result5);
            return (DataCollection<Entity>)DC;
        }

        public static String Getm2mRelationshipTypeFetch()
        {
            return "<fetch><entity name='systemuser'><attribute name='systemuserid'/><link-entity name='systemuserroles' from='systemuserid' to='systemuserid' intersect='true'><link-entity name='role' from='roleid' to='roleid'/><attribute name='roleid'/></link-entity><filter><condition attribute='systemuserid' operator='eq' value='00e7b0b9-1ace-e711-a970-000d3a192311'/></filter></entity></fetch>";
        }

        public static String Getm2mRelationshipTypeExpectedData()
        {
            return LoadXmlFile(@"../../lib/PrimitiveTypes/m2mRelationshipTypedata.xml");
        }

        public static String Getm2mRelationshipTypeExpectedSchema()
        {
            return LoadXmlFile(@"../../lib/PrimitiveTypes/m2mRelationshipTypedata_schema.xml");
        }
    }
}