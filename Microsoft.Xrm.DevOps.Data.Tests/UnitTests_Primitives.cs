using FakeXrmEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using System;
using System.Reflection;

namespace Microsoft.Xrm.DevOps.Data.Tests
{
    [TestClass]
    public class Primitives
    {
        [TestMethod]
        public void BooleanType()
        {
            XrmFakedContext fakedContext = SupportMethods.SetupPrimitiveFakedService(
                SupportMethods.ThemeLogicalName, 
                SupportMethods.ThemeDisplayName, 
                SupportMethods.GetBooleanTypeEntity());
            IOrganizationService fakedService = fakedContext.GetOrganizationService();

            DataBuilder DataBuilder = new DataBuilder(fakedService);
            DataBuilder.AppendData(SupportMethods.GetBooleanTypeFetch());
            Assert.AreEqual(
                DataBuilder.BuildDataXML().InnerXml, 
                SupportMethods.GetBooleanTypeExpectedData());
        }

        [TestMethod]
        public void CustomerType()
        {
            // CustomerType         customerid                         incident                                  
            XrmFakedContext fakedContext = SupportMethods.SetupPrimitiveFakedService(
                SupportMethods.IncidentLogicalName, 
                SupportMethods.IncidentDisplayName, 
                SupportMethods.GetCustomerTypeEntity());
            IOrganizationService fakedService = fakedContext.GetOrganizationService();

            DataBuilder DataBuilder = new DataBuilder(fakedService);
            DataBuilder.AppendData(SupportMethods.GetCustomerTypeFetch());
            Assert.AreEqual(
                DataBuilder.BuildDataXML().InnerXml, 
                SupportMethods.GetCustomerTypeExpectedData());
        }
        
        [TestMethod]
        public void DateTimeType()
        {
            // DateTimeType         publishon                          knowledgearticle
            XrmFakedContext fakedContext = SupportMethods.SetupPrimitiveFakedService(
                SupportMethods.KnowledgeArticleLogicalName, 
                SupportMethods.KnowledgeArticleDisplayName, 
                SupportMethods.GetDateTimeTypeEntity());
            IOrganizationService fakedService = fakedContext.GetOrganizationService();

            DataBuilder DataBuilder = new DataBuilder(fakedService);
            DataBuilder.AppendData(SupportMethods.GetDateTimeTypeFetch());
            Assert.AreEqual(
                DataBuilder.BuildDataXML().InnerXml, 
                SupportMethods.GetDateTimeTypeExpectedData());
        }
        
        [TestMethod]
        public void DecimalType()
        {
            // DecimalType          msdyn_hours                        msdyn_resourcerequirementdetail           
            XrmFakedContext fakedContext = SupportMethods.SetupPrimitiveFakedService(
                SupportMethods.ResourceRequirementDetailLogicalName, 
                SupportMethods.ResourceRequirementDetailDisplayName, 
                SupportMethods.GetDecimalTypeEntity());
            IOrganizationService fakedService = fakedContext.GetOrganizationService();

            DataBuilder DataBuilder = new DataBuilder(fakedService);
            DataBuilder.AppendData(SupportMethods.GetDecimalTypeFetch());
            Assert.AreEqual(
                DataBuilder.BuildDataXML().InnerXml, 
                SupportMethods.GetDecimalTypeExpectedData());
        }
        
        [TestMethod]
        public void DoubleType()
        {
            // DoubleType           msdyn_quantity                     msdyn_purchaseorderproduct                
            XrmFakedContext fakedContext = SupportMethods.SetupPrimitiveFakedService(
                SupportMethods.PurchaseOrderProductLogicalName, 
                SupportMethods.PurchaseOrderProductDisplayName, 
                SupportMethods.GetDoubleTypeEntity());
            IOrganizationService fakedService = fakedContext.GetOrganizationService();

            DataBuilder DataBuilder = new DataBuilder(fakedService);
            DataBuilder.AppendData(SupportMethods.GetDoubleTypeFetch());
            Assert.AreEqual(
                DataBuilder.BuildDataXML().InnerXml, 
                SupportMethods.GetDoubleTypeExpectedData());
        }
        
        [TestMethod]
        public void IntegerType()
        {
            // IntegerType          expirationstateid                  knowledgearticle
            XrmFakedContext fakedContext = SupportMethods.SetupPrimitiveFakedService(
                SupportMethods.KnowledgeArticleLogicalName, 
                SupportMethods.KnowledgeArticleDisplayName, 
                SupportMethods.GetIntegerTypeEntity());
            IOrganizationService fakedService = fakedContext.GetOrganizationService();

            DataBuilder DataBuilder = new DataBuilder(fakedService);
            DataBuilder.AppendData(SupportMethods.GetIntegerTypeFetch());
            Assert.AreEqual(
                DataBuilder.BuildDataXML().InnerXml, 
                SupportMethods.GetIntegerTypeExpectedData());
        }
        
        [TestMethod]
        public void LookupType()
        {
            // LookupType           previousarticlecontentid           knowledgearticle
            XrmFakedContext fakedContext = SupportMethods.SetupPrimitiveFakedService(
                SupportMethods.KnowledgeArticleLogicalName, 
                SupportMethods.KnowledgeArticleDisplayName, 
                SupportMethods.GetLookupTypeEntity());
            IOrganizationService fakedService = fakedContext.GetOrganizationService();

            DataBuilder DataBuilder = new DataBuilder(fakedService);
            DataBuilder.AppendData(SupportMethods.GetLookupTypeFetch());
            Assert.AreEqual(
                DataBuilder.BuildDataXML().InnerXml, 
                SupportMethods.GetLookupTypeExpectedData());
        }
        
        [TestMethod]
        public void MemoType()
        {
            // MemoType             keywords                           knowledgearticle
            XrmFakedContext fakedContext = SupportMethods.SetupPrimitiveFakedService(
                SupportMethods.KnowledgeArticleLogicalName, 
                SupportMethods.KnowledgeArticleDisplayName, 
                SupportMethods.GetMemoTypeEntity());
            IOrganizationService fakedService = fakedContext.GetOrganizationService();

            DataBuilder DataBuilder = new DataBuilder(fakedService);
            DataBuilder.AppendData(SupportMethods.GetMemoTypeFetch());
            Assert.AreEqual(
                DataBuilder.BuildDataXML().InnerXml, 
                SupportMethods.GetMemoTypeExpectedData());
        }
        
        [TestMethod]
        public void MoneyType()
        {
            // MoneyType            totaltax                           invoice                                   
            XrmFakedContext fakedContext = SupportMethods.SetupPrimitiveFakedService(
                SupportMethods.InvoiceLogicalName,
                SupportMethods.InvoiceDisplayName, 
                SupportMethods.GetMoneyTypeEntity());
            IOrganizationService fakedService = fakedContext.GetOrganizationService();

            DataBuilder DataBuilder = new DataBuilder(fakedService);
            DataBuilder.AppendData(SupportMethods.GetMoneyTypeFetch());
            Assert.AreEqual(
                DataBuilder.BuildDataXML().InnerXml, 
                SupportMethods.GetMoneyTypeExpectedData());
        }
        
        [TestMethod]
        public void OwnerType()
        {
            // OwnerType            ownerid                            knowledgearticle                           
            XrmFakedContext fakedContext = SupportMethods.SetupPrimitiveFakedService(
                SupportMethods.KnowledgeArticleLogicalName, 
                SupportMethods.KnowledgeArticleDisplayName, 
                SupportMethods.GetOwnerTypeEntity());
            IOrganizationService fakedService = fakedContext.GetOrganizationService();

            DataBuilder DataBuilder = new DataBuilder(fakedService);
            DataBuilder.AppendData(SupportMethods.GetOwnerTypeFetch());
            Assert.AreEqual(
                DataBuilder.BuildDataXML().InnerXml, 
                SupportMethods.GetOwnerTypeExpectedData());
        }
        
        [TestMethod]
        public void PartyListType()
        {
            // PartyListType        customers                          msdyn_approval
            XrmFakedContext fakedContext = SupportMethods.SetupPrimitiveFakedService(
                SupportMethods.ApprovalLogicalName, 
                SupportMethods.ApprovalDisplayName, 
                SupportMethods.GetPartyListTypeEntity());

            fakedContext.AddExecutionMock<RetrieveEntityRequest>(req =>
            {
                var logicalName = ((RetrieveEntityRequest)req).LogicalName;
                var displayName = String.Empty;
                switch (logicalName)
                {
                    case SupportMethods.ApprovalLogicalName:
                        displayName = SupportMethods.ApprovalDisplayName;
                        break;
                    case SupportMethods.ActivityPartyLogicalName:
                        displayName = SupportMethods.ActivityPartyDisplayName;
                        break;
                    default:
                        throw new NotImplementedException();
                }
                var entityMetadata = fakedContext.GetEntityMetadataByName(logicalName);

                entityMetadata.DisplayName = new Label(displayName, 1033);
                var response = new RetrieveEntityResponse()
                {
                    Results = new ParameterCollection
                        {
                            { "EntityMetadata", entityMetadata }
                        }
                };
                return response;
            });

            IOrganizationService fakedService = fakedContext.GetOrganizationService();

            DataBuilder DataBuilder = new DataBuilder(fakedService);
            DataBuilder.AppendData(SupportMethods.GetPartyListTypeFetch());
            Assert.AreEqual(
                DataBuilder.BuildDataXML().InnerXml, 
                SupportMethods.GetPartyListTypeExpectedData());
        }
        
        [TestMethod]
        public void PicklistType()
        {
            // PicklistType         expiredreviewoptions               knowledgearticle
            XrmFakedContext fakedContext = SupportMethods.SetupPrimitiveFakedService(
                SupportMethods.KnowledgeArticleLogicalName, 
                SupportMethods.KnowledgeArticleDisplayName, 
                SupportMethods.GetPicklistTypeEntity());
            IOrganizationService fakedService = fakedContext.GetOrganizationService();

            DataBuilder DataBuilder = new DataBuilder(fakedService);
            DataBuilder.AppendData(SupportMethods.GetPicklistTypeFetch());
            Assert.AreEqual(
                DataBuilder.BuildDataXML().InnerXml, 
                SupportMethods.GetPicklistTypeExpectedData());
        }
        
        [TestMethod]
        public void StateType()
        {
            // StateType            statecode                          knowledgearticle  
            XrmFakedContext fakedContext = SupportMethods.SetupPrimitiveFakedService(
                SupportMethods.KnowledgeArticleLogicalName, 
                SupportMethods.KnowledgeArticleDisplayName, 
                SupportMethods.GetStateTypeEntity());
            IOrganizationService fakedService = fakedContext.GetOrganizationService();

            DataBuilder DataBuilder = new DataBuilder(fakedService);
            DataBuilder.AppendData(SupportMethods.GetStateTypeFetch());
            Assert.AreEqual(
                DataBuilder.BuildDataXML().InnerXml, 
                SupportMethods.GetStateTypeExpectedData());
        }
        
        [TestMethod]
        public void StatusType()
        {
            // StatusType           statuscode                         knowledgearticle
            XrmFakedContext fakedContext = SupportMethods.SetupPrimitiveFakedService(
                SupportMethods.KnowledgeArticleLogicalName, 
                SupportMethods.KnowledgeArticleDisplayName, 
                SupportMethods.GetStatusTypeEntity());
            IOrganizationService fakedService = fakedContext.GetOrganizationService();

            DataBuilder DataBuilder = new DataBuilder(fakedService);
            DataBuilder.AppendData(SupportMethods.GetStatusTypeFetch());
            Assert.AreEqual(
                DataBuilder.BuildDataXML().InnerXml, 
                SupportMethods.GetStatusTypeExpectedData());
        }
        
        [TestMethod]
        public void StringType()
        {
            // StringType           description                        knowledgearticle
            XrmFakedContext fakedContext = SupportMethods.SetupPrimitiveFakedService(
                SupportMethods.KnowledgeArticleLogicalName, 
                SupportMethods.KnowledgeArticleDisplayName, 
                SupportMethods.GetStringTypeEntity());
            IOrganizationService fakedService = fakedContext.GetOrganizationService();

            DataBuilder DataBuilder = new DataBuilder(fakedService);
            DataBuilder.AppendData(SupportMethods.GetStringTypeFetch());
            Assert.AreEqual(
                DataBuilder.BuildDataXML().InnerXml, 
                SupportMethods.GetStringTypeExpectedData());
        }
        
        [TestMethod]
        public void UniqueidentifierType()
        {
            // UniqueidentifierType stageid                            knowledgearticle
            XrmFakedContext fakedContext = SupportMethods.SetupPrimitiveFakedService(
                SupportMethods.KnowledgeArticleLogicalName, 
                SupportMethods.KnowledgeArticleDisplayName, 
                SupportMethods.GetUniqueIdentifierTypeEntity());
            IOrganizationService fakedService = fakedContext.GetOrganizationService();

            DataBuilder DataBuilder = new DataBuilder(fakedService);
            DataBuilder.AppendData(SupportMethods.GetUniqueIdentifierTypeFetch());
            Assert.AreEqual(
                DataBuilder.BuildDataXML().InnerXml, 
                SupportMethods.GetUniqueIdentifierTypeExpectedData());
        }

        [TestMethod]
        public void m2mRelationshipType()
        {
            // UniqueidentifierType stageid                            knowledgearticle
            throw new NotImplementedException();
        }
    }
}
