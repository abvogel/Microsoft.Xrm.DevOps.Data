// rf -fetch "<fetch top='5'><entity name='invoice'><attribute name='totaltax'/><filter type='and'><condition attribute='totaltax' operator='not-null'/></filter></entity></fetch>"
// Entity:
// LogicalName     : invoice
// Id              : e45e1402-7c4d-e911-a96a-000d3a1d23d3
// Attributes      : {[totaltax, Microsoft.Xrm.Sdk.Money], [transactioncurrencyid, Microsoft.Xrm.Sdk.EntityReference], [invoiceid, 
//                   e45e1402-7c4d-e911-a96a-000d3a1d23d3]}
// EntityState     : 
// FormattedValues : {[totaltax, $0.00], [transactioncurrencyid, US Dollar]}
// RelatedEntities : {}
// RowVersion      : 8003738
// KeyAttributes   : {}
// ExtensionData   : System.Runtime.Serialization.ExtensionDataObject

// Key                   Value                               
// ---                   -----                               
// totaltax              Microsoft.Xrm.Sdk.Money             
// transactioncurrencyid Microsoft.Xrm.Sdk.EntityReference   
// invoiceid             e45e1402-7c4d-e911-a96a-000d3a1d23d3

// Dictionary:
// Key                            Value                                                     
// ---                            -----                                                     
// totaltax_Property              [totaltax, Microsoft.Xrm.Sdk.Money]                       
// totaltax                       $0.00                                                     
// transactioncurrencyid_Property [transactioncurrencyid, Microsoft.Xrm.Sdk.EntityReference]
// transactioncurrencyid          US Dollar                                                 
// invoiceid_Property             [invoiceid, e45e1402-7c4d-e911-a96a-000d3a1d23d3]         
// invoiceid                      e45e1402-7c4d-e911-a96a-000d3a1d23d3                      
// ReturnProperty_EntityName      invoice                                                   
// ReturnProperty_Id              e45e1402-7c4d-e911-a96a-000d3a1d23d3                      

// $v2.totaltax_Property.Value
//  Value ExtensionData                                   
//  ----- -------------                                   
// 0.0000 System.Runtime.Serialization.ExtensionDataObject

// $v2.totaltax_Property.Value.Value.gettype()
// IsPublic IsSerial Name                                     BaseType                                                                     
// -------- -------- ----                                     --------                                                                     
// True     True     Decimal                                  System.ValueType     

// Id            : ff4bc237-a3a4-e711-a967-000d3a192828
// LogicalName   : transactioncurrency
// Name          : US Dollar

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk;
using System;

namespace Microsoft.Xrm.DevOps.Data.Tests
{
    public partial class SupportMethods
    {
        public static Entity GetMoneyTypeEntity()
        {
            Money m = new Money(Decimal.Parse("0.00"));
            EntityReference t = new EntityReference("transactioncurrencyid", Guid.Parse("ff4bc237-a3a4-e711-a967-000d3a192828"));
                t.Name = "US Dollar";

            Entity result = new Entity("invoice");
            result.Id = Guid.Parse("e45e1402-7c4d-e911-a96a-000d3a1d23d3");
            result["invoiceid"] = result.Id;
            result["totaltax"] = m;
            result.FormattedValues.Add("totaltax", "$0.00");
            result["transactioncurrencyid"] = t;
            result.FormattedValues.Add("transactioncurrencyid", "US Dollar");

            return result;
        }
    }
}