// rf -fetch "<fetch top='5'><entity name='theme'><attribute name='isdefaulttheme'/><filter type='and'><condition attribute='isdefaulttheme' operator='not-null'/></filter></entity></fetch>"
// Entity:
// LogicalName     : theme
// Id              : 581ba8b5-f3cf-422e-8030-e2a645b39971
// Attributes      : {[isdefaulttheme, False], [themeid, 581ba8b5-f3cf-422e-8030-e2a645b39971]}
// EntityState     : 
// FormattedValues : {[isdefaulttheme, No]}
// RelatedEntities : {}
// RowVersion      : 
// KeyAttributes   : {}
// ExtensionData   : System.Runtime.Serialization.ExtensionDataObject

// Key                                           Value
// ---                                           -----
// isdefaulttheme                                False
// themeid        581ba8b5-f3cf-422e-8030-e2a645b39971

// Dictionary:
// Key                       Value                                          
// ---                       -----                                          
// isdefaulttheme_Property   [isdefaulttheme, False]                        
// isdefaulttheme            No                                             
// themeid_Property          [themeid, 581ba8b5-f3cf-422e-8030-e2a645b39971]
// themeid                   581ba8b5-f3cf-422e-8030-e2a645b39971           
// ReturnProperty_EntityName theme                                          
// ReturnProperty_Id         581ba8b5-f3cf-422e-8030-e2a645b39971           

using Microsoft.Xrm.Sdk;
using System;

namespace Microsoft.Xrm.DevOps.Data.Tests
{
    public partial class SupportMethods
    {
        public static Entity GetBooleanTypeEntity()
        {
            Entity result = new Entity("theme");
            result.Id = Guid.Parse("581ba8b5-f3cf-422e-8030-e2a645b39971");
            result["themeid"] = result.Id;
            result["isdefaulttheme"] = Boolean.Parse("false");
            result.FormattedValues.Add("isdefaulttheme", "No");

            return result;
        }
    }
}