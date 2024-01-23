using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Xml;

namespace Microsoft.Xrm.DevOps.Data.Tests
{
    public partial class SupportMethods
    {
        public static List<Entity> GetAllEntities_SomePluginsDisabled_MixedIdentifiersEntity()
        {
            List<Entity> entities = new List<Entity>();

            Entity entity1 = new Entity("theme");
            entity1.Id = Guid.Parse("581ba8b5-f3cf-422e-8030-e2a645b39971");
            entity1["themeid"] = entity1.Id;
            entity1["isdefaulttheme"] = Boolean.Parse("false");
            entities.Add(entity1);

            Entity entity2 = new Entity("theme");
            entity2.Id = Guid.Parse("f499443d-2082-4938-8842-e7ee62de9a23");
            entity2["themeid"] = entity2.Id;
            entity2["isdefaulttheme"] = Boolean.Parse("true");
            entities.Add(entity2);

            Entity entity3 = new Entity("incident");
            entity3.Id = Guid.Parse("616f5b50-774d-e911-a96a-000d3a1d23d3");
            entity3["incidentid"] = entity3.Id;
            entity3["customerid"] = new EntityReference("contact", Guid.Parse("9a913e9f-cbb8-e711-a968-000d3a192387")) { Name = "Andrew Vogel" };
            entities.Add(entity3);

            Entity entity4 = new Entity("incident");
            entity4.Id = Guid.Parse("cb9f9c66-774d-e911-a96a-000d3a1d23d3");
            entity4["incidentid"] = entity4.Id;
            entity4["customerid"] = new EntityReference("account", Guid.Parse("669537c3-a4bd-e711-a950-000d3a1087a0")) { Name = "test" };
            entities.Add(entity4);

            Entity entity5 = new Entity("knowledgearticle");
            entity5.Id = Guid.Parse("1c73d420-d29f-e711-9401-0003ff66fbff");
            entity5["keywords"] = "A, B, C, D";
            entity5["knowledgearticleid"] = Guid.Parse("1c73d420-d29f-e711-9401-0003ff66fbff");
            entity5["expirationstateid"] = Int32.Parse("3");
            entity5["ownerid"] = new EntityReference("systemuser", Guid.Parse("cdffaae5-55b0-e711-a95c-000d3a192e9a")) { Name = "Andrew Vogel" };
            entity5["statecode"] = new OptionSetValue(3);
            entity5["statuscode"] = new OptionSetValue(7);
            entity5["description"] = "Short Description";
            entity5["articlepublicnumber"] = "KA-23223";
            entities.Add(entity5);

            Entity entity6 = new Entity("knowledgearticle");
            entity6.Id = Guid.Parse("cea06e98-364e-e711-810e-e0071b6a82d1");
            entity6["keywords"] = "Loyalty, Winning, Hurricanes";
            entity6["knowledgearticleid"] = Guid.Parse("cea06e98-364e-e711-810e-e0071b6a82d1");
            entity6["publishon"] = DateTime.Parse("2017-06-12T09:45:00.0000000Z");
            entity6["ownerid"] = new EntityReference("systemuser", Guid.Parse("cdffaae5-55b0-e711-a95c-000d3a192e9a")) { Name = "Andrew Vogel" };
            entity6["statecode"] = new OptionSetValue(1);
            entity6["statuscode"] = new OptionSetValue(5);
            entity6["description"] = "Loyalty Disc Golf";
            entity6["articlepublicnumber"] = "KA-16398";
            entities.Add(entity6);

            Entity entity7 = new Entity("knowledgearticle");
            entity7.Id = Guid.Parse("75a90062-4c5c-e711-810e-e0071b6a82d1");
            entity7["keywords"] = "Grassroot, Pixel";
            entity7["knowledgearticleid"] = Guid.Parse("75a90062-4c5c-e711-810e-e0071b6a82d1");
            entity7["previousarticlecontentid"] = new EntityReference("knowledgearticle", Guid.Parse("8f9d8286-430c-e711-8121-e0071b6ac0e1")) { Name = "Mbps vs. MBps" };
            entity7["publishon"] = DateTime.Parse("2017-06-12T09:45:00.0000000Z");
            entity7["ownerid"] = new EntityReference("systemuser", Guid.Parse("cdffaae5-55b0-e711-a95c-000d3a192e9a")) { Name = "Andrew Vogel" };
            entity7["statecode"] = new OptionSetValue(6);
            entity7["statuscode"] = new OptionSetValue(13);
            entity7["description"] = "For use by professionals only.";
            entity7["articlepublicnumber"] = "KA-12971";
            entities.Add(entity7);

            Entity entity8 = new Entity("knowledgearticle");
            entity8.Id = Guid.Parse("7272848c-430c-e711-8121-e0071b6ac0e1");
            entity8["knowledgearticleid"] = Guid.Parse("7272848c-430c-e711-8121-e0071b6ac0e1");
            entity8["ownerid"] = new EntityReference("systemuser", Guid.Parse("cdffaae5-55b0-e711-a95c-000d3a192e9a")) { Name = "Andrew Vogel" };
            entity8["expiredreviewoptions"] = new OptionSetValue(-1);
            entity5["statecode"] = new OptionSetValue(3);
            entity5["statuscode"] = new OptionSetValue(7);
            entity8["stageid"] = Guid.Parse("0c6ed1c6-e9f1-4b43-8d9b-f680fded9b67");
            entity7["description"] = "It's a bird, it's a plane";
            entity7["articlepublicnumber"] = "KA-12982";
            entities.Add(entity8);

            Entity entity9 = new Entity("msdyn_resourcerequirementdetail");
            entity9.Id = Guid.Parse("73d453d8-774d-e911-a96a-000d3a1d23d3");
            entity9["msdyn_resourcerequirementdetailid"] = entity9.Id;
            entity9["msdyn_hours"] = Decimal.Parse("5.0000000000");
            entities.Add(entity9);

            Entity entity10 = new Entity("msdyn_purchaseorderproduct");
            entity10.Id = Guid.Parse("fbb6f525-794d-e911-a96a-000d3a1d23d3");
            entity10["msdyn_purchaseorderproductid"] = entity10.Id;
            entity10["msdyn_quantity"] = Double.Parse("73.25");
            entities.Add(entity10);

            Entity entity11 = new Entity("invoice");
            entity11.Id = Guid.Parse("e45e1402-7c4d-e911-a96a-000d3a1d23d3");
            entity11["invoiceid"] = entity11.Id;
                Money moneyObject = new Money(Decimal.Parse("0.0000"));
            entity11["totaltax"] = moneyObject;
                EntityReference transactioncurrencyER = new EntityReference("transactioncurrencyid", Guid.Parse("ff4bc237-a3a4-e711-a967-000d3a192828")) { Name = "US Dollar" };
            entity11["transactioncurrencyid"] = transactioncurrencyER;
            entities.Add(entity11);
            
            entities.Add(SupportMethods.GetPartyListTypeEntity());

            return entities;
        }

        public static List<Entity> GetSingleEntity_MoreThan5000RowsEntity()
        {
            List<Entity> entities = new List<Entity>();

            for (int i = 0; i < 6000; i++)
            {
                Entity entity1 = new Entity("theme");
                entity1["isdefaulttheme"] = true;
                entity1.Id = Guid.NewGuid();
                entity1["themeid"] = entity1.Id;
                entities.Add(entity1);
            }

            return entities;
        }

        public static List<Entity> GetSingleEntity_TenRowsEntity()
        {
            List<Entity> entities = new List<Entity>();

            for (int i = 0; i < 10; i++)
            {
                Entity entity1 = new Entity("theme");
                entity1["isdefaulttheme"] = true;
                entity1.Id = Guid.NewGuid();
                entity1["themeid"] = entity1.Id;
                entities.Add(entity1);
            }

            return entities;
        }

        public static String GetAllEntities_SomePluginsDisabled_MixedIdentifiersThemeFetch()
        {
            return "<fetch><entity name='theme'><attribute name='themeid'/><attribute name='isdefaulttheme'/><filter type='and'><condition attribute='isdefaulttheme' operator='not-null'/></filter></entity></fetch>";
        }
        public static String GetAllEntities_SomePluginsDisabled_MixedIdentifiersIncidentFetch()
        {
            return "<fetch><entity name='incident'><attribute name='incidentid'/><attribute name='customerid'/></entity></fetch>";
        }
        public static String GetAllEntities_SomePluginsDisabled_MixedIdentifiersKnowledgeArticleFetch()
        {
            return "<fetch><entity name='knowledgearticle'><attribute name='knowledgearticleid'/><attribute name='keywords'/><attribute name='publishon'/><attribute name='expirationstateid'/><attribute name='previousarticlecontentid'/><attribute name='ownerid'/><attribute name='expiredreviewoptions'/><attribute name='statecode'/><attribute name='statuscode'/><attribute name='stageid'/><attribute name='description'/><attribute name='articlepublicnumber'/></entity></fetch>";
        }
        public static String GetAllEntities_SomePluginsDisabled_MixedIdentifiersResourceRequirementDetailFetch()
        {
            return "<fetch><entity name='msdyn_resourcerequirementdetail'><attribute name='msdyn_resourcerequirementdetailid'/><attribute name='msdyn_hours'/></entity></fetch>";
        }
        public static String GetAllEntities_SomePluginsDisabled_MixedIdentifiersPurchaseOrderProductFetch()
        {
            return "<fetch><entity name='msdyn_purchaseorderproduct'><attribute name='msdyn_purchaseorderproductid'/><attribute name='msdyn_quantity'/></entity></fetch>";
        }
        public static String GetAllEntities_SomePluginsDisabled_MixedIdentifiersInvoiceFetch()
        {
            return "<fetch><entity name='invoice'><attribute name='invoiceid'/><attribute name='totaltax'/></entity></fetch>";
        }
        public static String GetAllEntities_SomePluginsDisabled_MixedIdentifiersApprovalFetch()
        {
            return "<fetch><entity name='msdyn_approval'><attribute name='activityid'/><attribute name='customers'/></entity></fetch>";
        }

        public static String GetAllEntities_SomePluginsDisabled_MixedIdentifiersExpectedData()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(@"../../lib/Configurations/AllEntities_SomePluginsDisabled_MixedIdentifiers_data.xml");
            doc.FirstChild.Attributes.RemoveNamedItem("timestamp");
            return doc.InnerXml;
        }

        public static String GetAllEntities_SomePluginsDisabled_MixedIdentifiersExpectedSchema()
        {
            return LoadXmlFile(@"../../lib/Configurations/AllEntities_SomePluginsDisabled_MixedIdentifiers_data_schema.xml");
        }
    }
}