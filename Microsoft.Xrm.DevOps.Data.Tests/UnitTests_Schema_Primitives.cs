using FakeXrmEasy;
using FakeXrmEasy.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Microsoft.Xrm.DevOps.Data.Tests
{
    [TestClass]
    public class SchemaPrimitives
    {
        [TestMethod]
        public void BooleanType()
        {
            XrmFakedContext fakedContext = SupportMethods.SetupPrimitiveFakedService(
                SupportMethods.ThemeLogicalName, 
                SupportMethods.ThemeDisplayName, 
                SupportMethods.GetBooleanTypeEntity());
            
            fakedContext.AddExecutionMock<RetrieveEntityRequest>(req =>
            {
                var entityMetadata = fakedContext.GetEntityMetadataByName(SupportMethods.ThemeLogicalName);
                entityMetadata.DisplayName = new Label(SupportMethods.ThemeDisplayName, 1033);
                entityMetadata.SetSealedPropertyValue("PrimaryNameAttribute", "name");

                entityMetadata.Attributes.First(a => a.LogicalName == "isdefaulttheme").SetSealedPropertyValue("DisplayName", new Label("Default Theme", 1033));

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
            DataBuilder.AppendData(SupportMethods.GetBooleanTypeFetch());

            String DataBuilderXML = DataBuilder.BuildSchemaXML().InnerXml;
            String RealXml = SupportMethods.GetBooleanTypeExpectedSchema();
            Assert.AreEqual(
               DataBuilder.BuildSchemaXML().InnerXml, 
               SupportMethods.GetBooleanTypeExpectedSchema());
        }

        [TestMethod]
        public void CustomerType()
        {
            // CustomerType         customerid                         incident                                  
            XrmFakedContext fakedContext = SupportMethods.SetupPrimitiveFakedService(
                SupportMethods.IncidentLogicalName, 
                SupportMethods.IncidentDisplayName, 
                SupportMethods.GetCustomerTypeEntity());

            fakedContext.AddExecutionMock<RetrieveEntityRequest>(req =>
            {
                var entityMetadata = fakedContext.GetEntityMetadataByName(SupportMethods.IncidentLogicalName);
                entityMetadata.DisplayName = new Label(SupportMethods.IncidentDisplayName, 1033);
                entityMetadata.SetSealedPropertyValue("PrimaryNameAttribute", "title");

                entityMetadata.Attributes.First(a => a.LogicalName == "customerid").SetSealedPropertyValue("DisplayName", new Label("Customer", 1033));
                entityMetadata.Attributes.First(a => a.LogicalName == "customerid").SetSealedPropertyValue("AttributeType", Sdk.Metadata.AttributeTypeCode.Customer);
                entityMetadata.Attributes.First(a => a.LogicalName == "customerid").SetSealedPropertyValue("Targets", new String[] { "account", "contact" });

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
            DataBuilder.AppendData(SupportMethods.GetCustomerTypeFetch());
            Assert.AreEqual(
                DataBuilder.BuildSchemaXML().InnerXml, 
                SupportMethods.GetCustomerTypeExpectedSchema());
        }
        
        [TestMethod]
        public void DateTimeType()
        {
            // DateTimeType         publishon                          knowledgearticle
            XrmFakedContext fakedContext = SupportMethods.SetupPrimitiveFakedService(
                SupportMethods.KnowledgeArticleLogicalName, 
                SupportMethods.KnowledgeArticleDisplayName, 
                SupportMethods.GetDateTimeTypeEntity());

            fakedContext.AddExecutionMock<RetrieveEntityRequest>(req =>
            {
                var entityMetadata = fakedContext.GetEntityMetadataByName(SupportMethods.KnowledgeArticleLogicalName);
                entityMetadata.DisplayName = new Label(SupportMethods.KnowledgeArticleDisplayName, 1033);
                entityMetadata.SetSealedPropertyValue("PrimaryNameAttribute", "title");

                entityMetadata.Attributes.First(a => a.LogicalName == "publishon").SetSealedPropertyValue("DisplayName", new Label("Publish On", 1033));

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
            DataBuilder.AppendData(SupportMethods.GetDateTimeTypeFetch());
            Assert.AreEqual(
                DataBuilder.BuildSchemaXML().InnerXml, 
                SupportMethods.GetDateTimeTypeExpectedSchema());
        }
        
        [TestMethod]
        public void DecimalType()
        {
            // DecimalType          msdyn_hours                        msdyn_resourcerequirementdetail           
            XrmFakedContext fakedContext = SupportMethods.SetupPrimitiveFakedService(
                SupportMethods.ResourceRequirementDetailLogicalName, 
                SupportMethods.ResourceRequirementDetailDisplayName, 
                SupportMethods.GetDecimalTypeEntity());

            fakedContext.AddExecutionMock<RetrieveEntityRequest>(req =>
            {
                var entityMetadata = fakedContext.GetEntityMetadataByName(SupportMethods.ResourceRequirementDetailLogicalName);
                entityMetadata.DisplayName = new Label(SupportMethods.ResourceRequirementDetailDisplayName, 1033);
                entityMetadata.SetSealedPropertyValue("PrimaryNameAttribute", "msdyn_name");

                entityMetadata.Attributes.First(a => a.LogicalName == "msdyn_hours").SetSealedPropertyValue("DisplayName", new Label("Hours", 1033));

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
            DataBuilder.AppendData(SupportMethods.GetDecimalTypeFetch());
            Assert.AreEqual(
                DataBuilder.BuildSchemaXML().InnerXml, 
                SupportMethods.GetDecimalTypeExpectedSchema());
        }
        
        [TestMethod]
        public void DoubleType()
        {
            // DoubleType           msdyn_quantity                     msdyn_purchaseorderproduct                
            XrmFakedContext fakedContext = SupportMethods.SetupPrimitiveFakedService(
                SupportMethods.PurchaseOrderProductLogicalName, 
                SupportMethods.PurchaseOrderProductDisplayName, 
                SupportMethods.GetDoubleTypeEntity());

            fakedContext.AddExecutionMock<RetrieveEntityRequest>(req =>
            {
                var entityMetadata = fakedContext.GetEntityMetadataByName(SupportMethods.PurchaseOrderProductLogicalName);
                entityMetadata.DisplayName = new Label(SupportMethods.PurchaseOrderProductDisplayName, 1033);
                entityMetadata.SetSealedPropertyValue("PrimaryNameAttribute", "msdyn_name");

                entityMetadata.Attributes.First(a => a.LogicalName == "msdyn_quantity").SetSealedPropertyValue("DisplayName", new Label("Quantity", 1033));

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
            DataBuilder.AppendData(SupportMethods.GetDoubleTypeFetch());
            Assert.AreEqual(
                DataBuilder.BuildSchemaXML().InnerXml, 
                SupportMethods.GetDoubleTypeExpectedSchema());
        }
        
        [TestMethod]
        public void IntegerType()
        {
            // IntegerType          expirationstateid                  knowledgearticle
            XrmFakedContext fakedContext = SupportMethods.SetupPrimitiveFakedService(
                SupportMethods.KnowledgeArticleLogicalName, 
                SupportMethods.KnowledgeArticleDisplayName, 
                SupportMethods.GetIntegerTypeEntity());
            
            fakedContext.AddExecutionMock<RetrieveEntityRequest>(req =>
            {
                var entityMetadata = fakedContext.GetEntityMetadataByName(SupportMethods.KnowledgeArticleLogicalName);
                entityMetadata.DisplayName = new Label(SupportMethods.KnowledgeArticleDisplayName, 1033);
                entityMetadata.SetSealedPropertyValue("PrimaryNameAttribute", "title");

                entityMetadata.Attributes.First(a => a.LogicalName == "expirationstateid").SetSealedPropertyValue("DisplayName", new Label("Expiration State Id", 1033));

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
            DataBuilder.AppendData(SupportMethods.GetIntegerTypeFetch());
            Assert.AreEqual(
                DataBuilder.BuildSchemaXML().InnerXml, 
                SupportMethods.GetIntegerTypeExpectedSchema());
        }
        
        [TestMethod]
        public void LookupType()
        {
            // LookupType           previousarticlecontentid           knowledgearticle
            XrmFakedContext fakedContext = SupportMethods.SetupPrimitiveFakedService(
                SupportMethods.KnowledgeArticleLogicalName, 
                SupportMethods.KnowledgeArticleDisplayName, 
                SupportMethods.GetLookupTypeEntity());

            fakedContext.AddExecutionMock<RetrieveEntityRequest>(req =>
            {
                var entityMetadata = fakedContext.GetEntityMetadataByName(SupportMethods.KnowledgeArticleLogicalName);
                entityMetadata.DisplayName = new Label(SupportMethods.KnowledgeArticleDisplayName, 1033);
                entityMetadata.SetSealedPropertyValue("PrimaryNameAttribute", "title");

                entityMetadata.Attributes.First(a => a.LogicalName == "previousarticlecontentid").SetSealedPropertyValue("DisplayName", new Label("Previous Article Content ID", 1033));
                entityMetadata.Attributes.First(a => a.LogicalName == "previousarticlecontentid").SetSealedPropertyValue("Targets", new String[] { "knowledgearticle" });

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
            DataBuilder.AppendData(SupportMethods.GetLookupTypeFetch());
            Assert.AreEqual(
                DataBuilder.BuildSchemaXML().InnerXml, 
                SupportMethods.GetLookupTypeExpectedSchema());
        }
        
        [TestMethod]
        public void MemoType()
        {
            // MemoType             keywords                           knowledgearticle
            XrmFakedContext fakedContext = SupportMethods.SetupPrimitiveFakedService(
                SupportMethods.KnowledgeArticleLogicalName, 
                SupportMethods.KnowledgeArticleDisplayName, 
                SupportMethods.GetMemoTypeEntity());

            fakedContext.AddExecutionMock<RetrieveEntityRequest>(req =>
            {
                var entityMetadata = fakedContext.GetEntityMetadataByName(SupportMethods.KnowledgeArticleLogicalName);
                entityMetadata.DisplayName = new Label(SupportMethods.KnowledgeArticleDisplayName, 1033);
                entityMetadata.SetSealedPropertyValue("PrimaryNameAttribute", "title");

                entityMetadata.Attributes.First(a => a.LogicalName == "keywords").SetSealedPropertyValue("DisplayName", new Label("Keywords", 1033));
                entityMetadata.Attributes.First(a => a.LogicalName == "keywords").SetSealedPropertyValue("AttributeType", Sdk.Metadata.AttributeTypeCode.Memo);

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
            DataBuilder.AppendData(SupportMethods.GetMemoTypeFetch());
            Assert.AreEqual(
                DataBuilder.BuildSchemaXML().InnerXml, 
                SupportMethods.GetMemoTypeExpectedSchema());
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
                DataBuilder.BuildSchemaXML().InnerXml, 
                SupportMethods.GetMoneyTypeExpectedSchema());
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
                DataBuilder.BuildSchemaXML().InnerXml, 
                SupportMethods.GetOwnerTypeExpectedSchema());
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
                DataBuilder.BuildSchemaXML().InnerXml, 
                SupportMethods.GetPartyListTypeExpectedSchema());
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
                DataBuilder.BuildSchemaXML().InnerXml, 
                SupportMethods.GetPicklistTypeExpectedSchema());
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
                DataBuilder.BuildSchemaXML().InnerXml, 
                SupportMethods.GetStateTypeExpectedSchema());
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
                DataBuilder.BuildSchemaXML().InnerXml, 
                SupportMethods.GetStatusTypeExpectedSchema());
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
                DataBuilder.BuildSchemaXML().InnerXml, 
                SupportMethods.GetStringTypeExpectedSchema());
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
                DataBuilder.BuildSchemaXML().InnerXml, 
                SupportMethods.GetUniqueIdentifierTypeExpectedSchema());
        }
    }
}
