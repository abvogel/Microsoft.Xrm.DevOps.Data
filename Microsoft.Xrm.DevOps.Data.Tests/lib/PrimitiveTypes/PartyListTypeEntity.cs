// rf -fetch "<fetch top='5'><entity name='msdyn_approval'><attribute name='customers'/><filter type='and'></filter></entity></fetch>"
// Entity:
// LogicalName     : msdyn_approval
// Id              : 3ff9afe1-7c4d-e911-a96a-000d3a1d23d3
// Attributes      : {[activityid, 3ff9afe1-7c4d-e911-a96a-000d3a1d23d3], [from, Microsoft.Xrm.Sdk.EntityCollection], [resources, 
//                   Microsoft.Xrm.Sdk.EntityCollection], [customers, Microsoft.Xrm.Sdk.EntityCollection]...}
// EntityState     : 
// FormattedValues : {}
// RelatedEntities : {}
// RowVersion      : 8003850
// KeyAttributes   : {}
// ExtensionData   : System.Runtime.Serialization.ExtensionDataObject

// Key               Value                               
// ---               -----                               
// activityid        3ff9afe1-7c4d-e911-a96a-000d3a1d23d3
// from              Microsoft.Xrm.Sdk.EntityCollection  
// resources         Microsoft.Xrm.Sdk.EntityCollection  
// customers         Microsoft.Xrm.Sdk.EntityCollection  
// partners          Microsoft.Xrm.Sdk.EntityCollection  
// cc                Microsoft.Xrm.Sdk.EntityCollection  
// bcc               Microsoft.Xrm.Sdk.EntityCollection  
// requiredattendees Microsoft.Xrm.Sdk.EntityCollection  
// optionalattendees Microsoft.Xrm.Sdk.EntityCollection  
// organizer         Microsoft.Xrm.Sdk.EntityCollection  
// to                Microsoft.Xrm.Sdk.EntityCollection  
 
// Dictionary:
// Key                        Value                                                  
// ---                        -----                                                  
// activityid_Property        [activityid, 3ff9afe1-7c4d-e911-a96a-000d3a1d23d3]     
// activityid                 3ff9afe1-7c4d-e911-a96a-000d3a1d23d3                   
// from_Property              [from, Microsoft.Xrm.Sdk.EntityCollection]             
// from                       Microsoft.Xrm.Sdk.EntityCollection                     
// resources_Property         [resources, Microsoft.Xrm.Sdk.EntityCollection]        
// resources                  Microsoft.Xrm.Sdk.EntityCollection                     
// customers_Property         [customers, Microsoft.Xrm.Sdk.EntityCollection]        
// customers                  Microsoft.Xrm.Sdk.EntityCollection                     
// partners_Property          [partners, Microsoft.Xrm.Sdk.EntityCollection]         
// partners                   Microsoft.Xrm.Sdk.EntityCollection                     
// cc_Property                [cc, Microsoft.Xrm.Sdk.EntityCollection]               
// cc                         Microsoft.Xrm.Sdk.EntityCollection                     
// bcc_Property               [bcc, Microsoft.Xrm.Sdk.EntityCollection]              
// bcc                        Microsoft.Xrm.Sdk.EntityCollection                     
// requiredattendees_Property [requiredattendees, Microsoft.Xrm.Sdk.EntityCollection]
// requiredattendees          Microsoft.Xrm.Sdk.EntityCollection                     
// optionalattendees_Property [optionalattendees, Microsoft.Xrm.Sdk.EntityCollection]
// optionalattendees          Microsoft.Xrm.Sdk.EntityCollection                     
// organizer_Property         [organizer, Microsoft.Xrm.Sdk.EntityCollection]        
// organizer                  Microsoft.Xrm.Sdk.EntityCollection                     
// to_Property                [to, Microsoft.Xrm.Sdk.EntityCollection]               
// to                         Microsoft.Xrm.Sdk.EntityCollection                     
// ReturnProperty_EntityName  msdyn_approval                                         
// ReturnProperty_Id          3ff9afe1-7c4d-e911-a96a-000d3a1d23d3                   

// $v2.Values | Select Key -ExpandProperty Value
// Guid                                
// ----                                
// 3ff9afe1-7c4d-e911-a96a-000d3a1d23d3

// Key                           : from
// Entities                      : {}
// MoreRecords                   : False
// PagingCookie                  : 
// MinActiveRowVersion           : -1
// TotalRecordCount              : -1
// TotalRecordCountLimitExceeded : False
// EntityName                    : activityparty
// ExtensionData                 : System.Runtime.Serialization.ExtensionDataObject

// Key                           : resources
// Entities                      : {}
// Additional properties removed

// Key                           : customers
// Entities                      : {3d12ac27-7d4d-e911-a96a-000d3a1d23d3, 3e12ac27-7d4d-e911-a96a-000d3a1d23d3}

// LogicalName     : activityparty
// Id              : 3d12ac27-7d4d-e911-a96a-000d3a1d23d3
// Attributes      : {[ispartydeleted, False], [activityid, Microsoft.Xrm.Sdk.EntityReference], [participationtypemask, 
//                   Microsoft.Xrm.Sdk.OptionSetValue], [donotemail, False]...}
// FormattedValues : {[ispartydeleted, No], [participationtypemask, Customer], [donotemail, Allow], [donotfax, Allow]...}

// LogicalName     : activityparty
// Id              : 3e12ac27-7d4d-e911-a96a-000d3a1d23d3
// Attributes      : {[ispartydeleted, False], [activityid, Microsoft.Xrm.Sdk.EntityReference], [participationtypemask, 
//                   Microsoft.Xrm.Sdk.OptionSetValue], [donotemail, False]...}
// FormattedValues : {[ispartydeleted, No], [participationtypemask, Customer], [donotemail, Allow], [donotfax, Allow]...}

// $v3.Entities.Attributes
// Key                                                  Value
// ---                                                  -----
// ispartydeleted                                       False
// activityid               Microsoft.Xrm.Sdk.EntityReference
// participationtypemask     Microsoft.Xrm.Sdk.OptionSetValue
// donotemail                                           False
// donotfax                                             False
// donotpostalmail                                      False
// ownerid                  Microsoft.Xrm.Sdk.EntityReference
// partyid                  Microsoft.Xrm.Sdk.EntityReference
// activitypartyid       3d12ac27-7d4d-e911-a96a-000d3a1d23d3
// instancetypecode          Microsoft.Xrm.Sdk.OptionSetValue
// donotphone                                           False

// ispartydeleted
// False
// True     True     Boolean

// activityid
// Id            : 3ff9afe1-7c4d-e911-a96a-000d3a1d23d3
// LogicalName   : activitypointer
// Name          : 
// True     True     EntityReference

// participationtypemask
// Value         : 11
// True     False    OptionSetValue

// donotemail
// False
// True     True     Boolean

// donotfax
// False
// True     True     Boolean

// donotpostalmail
// False
// True     True     Boolean

// ownerid
// Id            : be9135c6-f5a9-e711-a95c-000d3a192e9a
// LogicalName   : systemuser
// Name          : 
// True     True     EntityReference

// partyid
// Id            : 9a913e9f-cbb8-e711-a968-000d3a192387
// LogicalName   : contact
// Name          : Andrew Vogel
// True     True     EntityReference

// activitypartyid
// Guid : 3d12ac27-7d4d-e911-a96a-000d3a1d23d3
// True     True     Guid

// instancetypecode
// Value         : 0
// True     False    OptionSetValue

// donotphone
// False
// True     True     Boolean

// $v3.Entities.formattedvalues
// Key                   Value        
// ---                   -----        
// ispartydeleted        No           
// participationtypemask Customer     
// donotemail            Allow        
// donotfax              Allow        
// donotpostalmail       Allow        
// partyid               Andrew Vogel 
// instancetypecode      Not Recurring
// donotphone            Allow       

// ispartydeleted                                       False
// activityid               Microsoft.Xrm.Sdk.EntityReference
// participationtypemask     Microsoft.Xrm.Sdk.OptionSetValue
// donotemail                                           False
// donotfax                                             False
// donotpostalmail                                      False
// ownerid                  Microsoft.Xrm.Sdk.EntityReference
// partyid                  Microsoft.Xrm.Sdk.EntityReference
// activitypartyid       3e12ac27-7d4d-e911-a96a-000d3a1d23d3
// instancetypecode          Microsoft.Xrm.Sdk.OptionSetValue
// donotphone                                           False

// ispartydeleted
// False
// True     True     Boolean

// activityid
// Id            : 3ff9afe1-7c4d-e911-a96a-000d3a1d23d3
// LogicalName   : activitypointer
// Name          : 
// True     True     EntityReference

// participationtypemask
// Value         : 11
// True     False    OptionSetValue

// donotemail
// False
// True     True     Boolean

// donotfax
// False
// True     True     Boolean

// donotpostalmail
// False
// True     True     Boolean

// ownerid
// Id            : be9135c6-f5a9-e711-a95c-000d3a192e9a
// LogicalName   : systemuser
// Name          : 
// True     True     EntityReference

// partyid
// Id            : 669537c3-a4bd-e711-a950-000d3a1087a0
// LogicalName   : account
// Name          : test
// True     True     EntityReference

// activitypartyid
// Guid : 3e12ac27-7d4d-e911-a96a-000d3a1d23d3
// True     True     Guid

// instancetypecode
// Value         : 0
// True     False    OptionSetValue

// donotphone
// False
// True     True     Boolean
 
// $v3.Entities.formattedvalues
// ispartydeleted        No           
// participationtypemask Customer     
// donotemail            Allow        
// donotfax              Allow        
// donotpostalmail       Allow        
// partyid               test         
// instancetypecode      Not Recurring
// donotphone            Allow        

// Key                           : partners
// Entities                      : {}

// Key                           : cc
// Entities                      : {}

// Key                           : bcc
// Entities                      : {}

// Key                           : requiredattendees
// Entities                      : {}

// Key                           : optionalattendees
// Entities                      : {}

// Key                           : organizer
// Entities                      : {}

// Key                           : to
// Entities                      : {}

using Microsoft.Xrm.Sdk;
using System;

namespace Microsoft.Xrm.DevOps.Data.Tests
{
    public partial class SupportMethods
    {
        public static Entity GetPartyListTypeEntity()
        {
            EntityCollection emptyEC = new EntityCollection();
            emptyEC.EntityName = "activityparty";

            System.Collections.ObjectModel.Collection<Entity> Parties = new System.Collections.ObjectModel.Collection<Entity>();
                Entity Party1 = new Entity("activityparty", Guid.Parse("3d12ac27-7d4d-e911-a96a-000d3a1d23d3"));
                    Party1["ispartydeleted"] = Boolean.Parse("False");
                    Party1.FormattedValues.Add("ispartydeleted","No");
                    Party1["activityid"] = new EntityReference("activitypointer", Guid.Parse("3ff9afe1-7c4d-e911-a96a-000d3a1d23d3"));
                    Party1["participationtypemask"] = new OptionSetValue(11);
                    Party1.FormattedValues.Add("participationtypemask", "Customer");
                    Party1["donotemail"] = Boolean.Parse("False");
                    Party1.FormattedValues.Add("donotemail", "Allow");
                    Party1["donotfax"] = Boolean.Parse("False");
                    Party1.FormattedValues.Add("donotfax", "Allow");
                    Party1["donotpostalmail"] = Boolean.Parse("False");
                    Party1.FormattedValues.Add("donotpostalmail", "Allow");
                    Party1["ownerid"] = new EntityReference("systemuser", Guid.Parse("be9135c6-f5a9-e711-a95c-000d3a192e9a"));
                    EntityReference Party1PartyId = new EntityReference("contact", Guid.Parse("9a913e9f-cbb8-e711-a968-000d3a192387"));
                    Party1PartyId.Name = "Andrew Vogel";
                    Party1["partyid"] = Party1PartyId;
                    Party1.FormattedValues.Add("partyid", "Andrew Vogel");
                    Party1["activitypartyid"] = Guid.Parse("3d12ac27-7d4d-e911-a96a-000d3a1d23d3");
                    Party1["instancetypecode"] = new OptionSetValue(0);
                    Party1.FormattedValues.Add("instancetypecode", "Not Recurring");
                    Party1["donotphone"] = Boolean.Parse("False");
                    Party1.FormattedValues.Add("donotphone", "Allow");

                Entity Party2 = new Entity("activityparty", Guid.Parse("3e12ac27-7d4d-e911-a96a-000d3a1d23d3"));
                    Party2["ispartydeleted"] = Boolean.Parse("False");
                    Party1.FormattedValues.Add("ispartydeleted","No");
                    Party2["activityid"] = new EntityReference("activitypointer", Guid.Parse("3ff9afe1-7c4d-e911-a96a-000d3a1d23d3"));
                    Party2["participationtypemask"] = new OptionSetValue(11);
                    Party1.FormattedValues.Add("participationtypemask", "Customer");
                    Party2["donotemail"] = Boolean.Parse("False");
                    Party1.FormattedValues.Add("donotemail", "Allow");
                    Party2["donotfax"] = Boolean.Parse("False");
                    Party1.FormattedValues.Add("donotfax", "Allow");
                    Party2["donotpostalmail"] = Boolean.Parse("False");
                    Party1.FormattedValues.Add("donotpostalmail", "Allow");
                    Party2["ownerid"] = new EntityReference("systemuser", Guid.Parse("be9135c6-f5a9-e711-a95c-000d3a192e9a"));
                    EntityReference Party2PartyId = new EntityReference("account", Guid.Parse("669537c3-a4bd-e711-a950-000d3a1087a0"));
                    Party2PartyId.Name = "test";
                    Party2["partyid"] = Party2PartyId;
                    Party1.FormattedValues.Add("partyid", "test");
                    Party2["activitypartyid"] = Guid.Parse("3e12ac27-7d4d-e911-a96a-000d3a1d23d3");
                    Party2["instancetypecode"] = new OptionSetValue(0);
                    Party1.FormattedValues.Add("instancetypecode", "Not Recurring");
                    Party2["donotphone"] = Boolean.Parse("False");
                    Party1.FormattedValues.Add("donotphone", "Allow");

                Parties.Add(Party1);
                Parties.Add(Party2);

            Entity result = new Entity("msdyn_approval");
                result.Id = Guid.Parse("3ff9afe1-7c4d-e911-a96a-000d3a1d23d3");
                result["activityid"] = result.Id;
                result["from"] = emptyEC;
                result["resources"] = emptyEC;
                result["customers"] = (DataCollection<Entity>)Parties;
                result["partners"] = emptyEC;
                result["cc"] = emptyEC;
                result["bcc"] = emptyEC;
                result["requiredattendees"] = emptyEC;
                result["optionalattendees"] = emptyEC;
                result["organizer"] = emptyEC;
                result["to"] = emptyEC;

            return result;
        }

        public static String GetPartyListTypeFetch()
        {
            return "<fetch top='1'><entity name='msdyn_approval'><attribute name='customers'/><filter type='and'></filter></entity></fetch>";
        }
    }
}