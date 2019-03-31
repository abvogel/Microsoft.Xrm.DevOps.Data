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
            IOrganizationService fakedService = SupportMethods.SetupPrimitiveFakedService(
                SupportMethods.ThemeLogicalName, 
                SupportMethods.ThemeDisplayName, 
                SupportMethods.GetBooleanTypeEntity());

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
            IOrganizationService fakedService = SupportMethods.SetupPrimitiveFakedService(
                SupportMethods.IncidentLogicalName, 
                SupportMethods.IncidentDisplayName, 
                SupportMethods.GetCustomerTypeEntity());

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
            IOrganizationService fakedService = SupportMethods.SetupPrimitiveFakedService(
                SupportMethods.KnowledgeArticleLogicalName, 
                SupportMethods.KnowledgeArticleDisplayName, 
                SupportMethods.GetDateTimeTypeEntity());

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
            IOrganizationService fakedService = SupportMethods.SetupPrimitiveFakedService(
                SupportMethods.ResourceRequirementDetailLogicalName, 
                SupportMethods.ResourceRequirementDetailDisplayName, 
                SupportMethods.GetDecimalTypeEntity());

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
            IOrganizationService fakedService = SupportMethods.SetupPrimitiveFakedService(
                SupportMethods.PurchaseOrderProductLogicalName, 
                SupportMethods.PurchaseOrderProductDisplayName, 
                SupportMethods.GetDoubleTypeEntity());

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
            IOrganizationService fakedService = SupportMethods.SetupPrimitiveFakedService(
                SupportMethods.KnowledgeArticleLogicalName, 
                SupportMethods.KnowledgeArticleDisplayName, 
                SupportMethods.GetIntegerTypeEntity());

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
            IOrganizationService fakedService = SupportMethods.SetupPrimitiveFakedService(
                SupportMethods.KnowledgeArticleLogicalName, 
                SupportMethods.KnowledgeArticleDisplayName, 
                SupportMethods.GetLookupTypeEntity());

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
            IOrganizationService fakedService = SupportMethods.SetupPrimitiveFakedService(
                SupportMethods.KnowledgeArticleLogicalName, 
                SupportMethods.KnowledgeArticleDisplayName, 
                SupportMethods.GetMemoTypeEntity());

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
            IOrganizationService fakedService = SupportMethods.SetupPrimitiveFakedService(
                SupportMethods.InvoiceLogicalName,
                SupportMethods.InvoiceDisplayName, 
                SupportMethods.GetMoneyTypeEntity());

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
            IOrganizationService fakedService = SupportMethods.SetupPrimitiveFakedService(
                SupportMethods.KnowledgeArticleLogicalName, 
                SupportMethods.KnowledgeArticleDisplayName, 
                SupportMethods.GetOwnerTypeEntity());

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
            IOrganizationService fakedService = SupportMethods.SetupPrimitiveFakedService(
                SupportMethods.ApprovalLogicalName, 
                SupportMethods.ApprovalDisplayName, 
                SupportMethods.GetPartyListTypeEntity());

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
            IOrganizationService fakedService = SupportMethods.SetupPrimitiveFakedService(
                SupportMethods.KnowledgeArticleLogicalName, 
                SupportMethods.KnowledgeArticleDisplayName, 
                SupportMethods.GetPicklistTypeEntity());

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
            IOrganizationService fakedService = SupportMethods.SetupPrimitiveFakedService(
                SupportMethods.KnowledgeArticleLogicalName, 
                SupportMethods.KnowledgeArticleDisplayName, 
                SupportMethods.GetStateTypeEntity());

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
            IOrganizationService fakedService = SupportMethods.SetupPrimitiveFakedService(
                SupportMethods.KnowledgeArticleLogicalName, 
                SupportMethods.KnowledgeArticleDisplayName, 
                SupportMethods.GetStatusTypeEntity());

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
            IOrganizationService fakedService = SupportMethods.SetupPrimitiveFakedService(
                SupportMethods.KnowledgeArticleLogicalName, 
                SupportMethods.KnowledgeArticleDisplayName, 
                SupportMethods.GetStringTypeEntity());

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
            IOrganizationService fakedService = SupportMethods.SetupPrimitiveFakedService(
                SupportMethods.KnowledgeArticleLogicalName, 
                SupportMethods.KnowledgeArticleDisplayName, 
                SupportMethods.GetUniqueIdentifierTypeEntity());

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
