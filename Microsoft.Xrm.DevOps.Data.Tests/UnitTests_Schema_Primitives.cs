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
                entityMetadata.Attributes.First(a => a.LogicalName == "themeid").SetSealedPropertyValue("DisplayName", new Label("Theme", 1033));

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
                entityMetadata.Attributes.First(a => a.LogicalName == "incidentid").SetSealedPropertyValue("DisplayName", new Label("Case", 1033));

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
                entityMetadata.Attributes.First(a => a.LogicalName == "knowledgearticleid").SetSealedPropertyValue("DisplayName", new Label("Knowledge Article", 1033));

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
                entityMetadata.Attributes.First(a => a.LogicalName == "msdyn_hours").SetSealedPropertyValue("IsCustomAttribute", true);
                entityMetadata.Attributes.First(a => a.LogicalName == "msdyn_resourcerequirementdetailid").SetSealedPropertyValue("DisplayName", new Label("Resource Requirement Detail", 1033));

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
                entityMetadata.Attributes.First(a => a.LogicalName == "msdyn_quantity").SetSealedPropertyValue("IsCustomAttribute", true);
                entityMetadata.Attributes.First(a => a.LogicalName == "msdyn_purchaseorderproductid").SetSealedPropertyValue("DisplayName", new Label("Purchase Order Product", 1033));

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
                entityMetadata.Attributes.First(a => a.LogicalName == "knowledgearticleid").SetSealedPropertyValue("DisplayName", new Label("Knowledge Article", 1033));

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
                entityMetadata.Attributes.First(a => a.LogicalName == "knowledgearticleid").SetSealedPropertyValue("DisplayName", new Label("Knowledge Article", 1033));

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
                entityMetadata.Attributes.First(a => a.LogicalName == "knowledgearticleid").SetSealedPropertyValue("DisplayName", new Label("Knowledge Article", 1033));

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

            fakedContext.AddExecutionMock<RetrieveEntityRequest>(req =>
            {
                var entityMetadata = fakedContext.GetEntityMetadataByName(SupportMethods.InvoiceLogicalName);
                entityMetadata.DisplayName = new Label(SupportMethods.InvoiceDisplayName, 1033);
                entityMetadata.SetSealedPropertyValue("PrimaryNameAttribute", "name");

                entityMetadata.Attributes.First(a => a.LogicalName == "totaltax").SetSealedPropertyValue("DisplayName", new Label("Total Tax", 1033));
                entityMetadata.Attributes.First(a => a.LogicalName == "invoiceid").SetSealedPropertyValue("DisplayName", new Label("Invoice", 1033));

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

            fakedContext.AddExecutionMock<RetrieveEntityRequest>(req =>
            {
                var entityMetadata = fakedContext.GetEntityMetadataByName(SupportMethods.KnowledgeArticleLogicalName);
                entityMetadata.DisplayName = new Label(SupportMethods.KnowledgeArticleDisplayName, 1033);
                entityMetadata.SetSealedPropertyValue("PrimaryNameAttribute", "title");

                entityMetadata.Attributes.First(a => a.LogicalName == "ownerid").SetSealedPropertyValue("DisplayName", new Label("Owner", 1033));
                entityMetadata.Attributes.First(a => a.LogicalName == "ownerid").SetSealedPropertyValue("AttributeType", Sdk.Metadata.AttributeTypeCode.Owner);
                entityMetadata.Attributes.First(a => a.LogicalName == "knowledgearticleid").SetSealedPropertyValue("DisplayName", new Label("Knowledge Article", 1033));

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
                var entityMetadata = fakedContext.GetEntityMetadataByName(logicalName);

                entityMetadata.DisplayName = new Label("Approval", 1033);
                entityMetadata.SetSealedPropertyValue("PrimaryNameAttribute", "subject");

                if (entityMetadata.LogicalName == SupportMethods.ApprovalLogicalName)
                {
                    entityMetadata.Attributes.First(a => a.LogicalName == "customers").SetSealedPropertyValue("DisplayName", new Label("Customers", 1033));
                    entityMetadata.Attributes.First(a => a.LogicalName == "customers").SetSealedPropertyValue("AttributeType", Sdk.Metadata.AttributeTypeCode.PartyList);
                    entityMetadata.Attributes.First(a => a.LogicalName == "activityid").SetSealedPropertyValue("DisplayName", new Label("Approval", 1033));
                }

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

            fakedContext.AddExecutionMock<RetrieveEntityRequest>(req =>
            {
                var entityMetadata = fakedContext.GetEntityMetadataByName(SupportMethods.KnowledgeArticleLogicalName);
                entityMetadata.DisplayName = new Label(SupportMethods.KnowledgeArticleDisplayName, 1033);
                entityMetadata.SetSealedPropertyValue("PrimaryNameAttribute", "title");

                entityMetadata.Attributes.First(a => a.LogicalName == "expiredreviewoptions").SetSealedPropertyValue("DisplayName", new Label("Expired Review Options", 1033));
                entityMetadata.Attributes.First(a => a.LogicalName == "knowledgearticleid").SetSealedPropertyValue("DisplayName", new Label("Knowledge Article", 1033));

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

            fakedContext.AddExecutionMock<RetrieveEntityRequest>(req =>
            {
                var entityMetadata = fakedContext.GetEntityMetadataByName(SupportMethods.KnowledgeArticleLogicalName);
                entityMetadata.DisplayName = new Label(SupportMethods.KnowledgeArticleDisplayName, 1033);
                entityMetadata.SetSealedPropertyValue("PrimaryNameAttribute", "title");

                entityMetadata.Attributes.First(a => a.LogicalName == "statecode").SetSealedPropertyValue("DisplayName", new Label("Status", 1033));
                entityMetadata.Attributes.First(a => a.LogicalName == "knowledgearticleid").SetSealedPropertyValue("DisplayName", new Label("Knowledge Article", 1033));

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

            fakedContext.AddExecutionMock<RetrieveEntityRequest>(req =>
            {
                var entityMetadata = fakedContext.GetEntityMetadataByName(SupportMethods.KnowledgeArticleLogicalName);
                entityMetadata.DisplayName = new Label(SupportMethods.KnowledgeArticleDisplayName, 1033);
                entityMetadata.SetSealedPropertyValue("PrimaryNameAttribute", "title");

                entityMetadata.Attributes.First(a => a.LogicalName == "statuscode").SetSealedPropertyValue("DisplayName", new Label("Status Reason", 1033));
                entityMetadata.Attributes.First(a => a.LogicalName == "knowledgearticleid").SetSealedPropertyValue("DisplayName", new Label("Knowledge Article", 1033));

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

            fakedContext.AddExecutionMock<RetrieveEntityRequest>(req =>
            {
                var entityMetadata = fakedContext.GetEntityMetadataByName(SupportMethods.KnowledgeArticleLogicalName);
                entityMetadata.DisplayName = new Label(SupportMethods.KnowledgeArticleDisplayName, 1033);
                entityMetadata.SetSealedPropertyValue("PrimaryNameAttribute", "title");

                entityMetadata.Attributes.First(a => a.LogicalName == "description").SetSealedPropertyValue("DisplayName", new Label("Short Description", 1033));
                entityMetadata.Attributes.First(a => a.LogicalName == "knowledgearticleid").SetSealedPropertyValue("DisplayName", new Label("Knowledge Article", 1033));

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

            fakedContext.AddExecutionMock<RetrieveEntityRequest>(req =>
            {
                var entityMetadata = fakedContext.GetEntityMetadataByName(SupportMethods.KnowledgeArticleLogicalName);
                entityMetadata.DisplayName = new Label(SupportMethods.KnowledgeArticleDisplayName, 1033);
                entityMetadata.SetSealedPropertyValue("PrimaryNameAttribute", "title");

                entityMetadata.Attributes.First(a => a.LogicalName == "stageid").SetSealedPropertyValue("DisplayName", new Label("Stage Id", 1033));
                entityMetadata.Attributes.First(a => a.LogicalName == "stageid").SetSealedPropertyValue("AttributeType", Sdk.Metadata.AttributeTypeCode.Uniqueidentifier);
                entityMetadata.Attributes.First(a => a.LogicalName == "knowledgearticleid").SetSealedPropertyValue("DisplayName", new Label("Knowledge Article", 1033));

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
            DataBuilder.AppendData(SupportMethods.GetUniqueIdentifierTypeFetch());
            Assert.AreEqual(
                DataBuilder.BuildSchemaXML().InnerXml, 
                SupportMethods.GetUniqueIdentifierTypeExpectedSchema());
        }

        [TestMethod]
        public void m2mRelationshipType()
        {
            var fakedContext = new XrmFakedContext();
            fakedContext.InitializeMetadata(typeof(CrmEarlyBound.CrmServiceContext).Assembly);

            fakedContext.Initialize(SupportMethods.Getm2mRelationshipTypeEntities());

            fakedContext.AddRelationship("systemuserroles_association", new XrmFakedRelationship
            {
                IntersectEntity = "systemuserroles",
                Entity1LogicalName = "systemuser",
                Entity1Attribute = "systemuserid",
                Entity2LogicalName = "role",
                Entity2Attribute = "roleid"
            });

            var AssociateRequest = SupportMethods.Getm2mRelationshipTypeAssociateRequest();
            IOrganizationService fakedService = fakedContext.GetOrganizationService();
            fakedService.Execute(AssociateRequest);
            
            fakedContext.AddExecutionMock<RetrieveEntityRequest>(req =>
            {
                var logicalName = ((RetrieveEntityRequest)req).LogicalName;
                var entityMetadata = fakedContext.GetEntityMetadataByName(logicalName);

                switch (entityMetadata.LogicalName)
                {
                    case SupportMethods.UserLogicalName:
                        entityMetadata.DisplayName = new Label(SupportMethods.UserDisplayName, 1033);
                        entityMetadata.SetSealedPropertyValue("PrimaryNameAttribute", "fullname");
                        entityMetadata.Attributes.First(a => a.LogicalName == "systemuserid").SetSealedPropertyValue("DisplayName", new Label("User", 1033));
                        entityMetadata.ManyToManyRelationships.First(m => m.SchemaName == "systemuserroles_association").SetSealedPropertyValue("IntersectEntityName", "systemuserroles");
                        entityMetadata.ManyToManyRelationships.First(m => m.SchemaName == "systemuserroles_association").SetSealedPropertyValue("Entity2LogicalName", "role");
                        entityMetadata.ManyToManyRelationships.First(m => m.SchemaName == "systemuserroles_association").SetSealedPropertyValue("Entity2IntersectAttribute", "roleid");
                        break;
                    case SupportMethods.SecurityRoleLogicalName:
                        entityMetadata.DisplayName = new Label(SupportMethods.SecurityRoleDisplayName, 1033);
                        entityMetadata.SetSealedPropertyValue("PrimaryNameAttribute", "name");
                        entityMetadata.Attributes.First(a => a.LogicalName == "roleid").SetSealedPropertyValue("DisplayName", new Label("Role", 1033));
                        break;
                    default:
                        break;
                }

                var response = new RetrieveEntityResponse()
                {
                    Results = new ParameterCollection
                        {
                            { "EntityMetadata", entityMetadata }
                        }
                };
                return response;
            });

            DataBuilder DataBuilder = new DataBuilder(fakedService);
            DataBuilder.AppendData("<fetch><entity name='systemuser'><attribute name='systemuserid'/><link-entity name='systemuserroles' from='systemuserid' to='systemuserid' intersect='true'><link-entity name='role' from='roleid' to='roleid'/><attribute name='roleid'/></link-entity><filter><condition attribute='systemuserid' operator='eq' value='00e7b0b9-1ace-e711-a970-000d3a192311'/></filter></entity></fetch>");

            var schemaXml = DataBuilder.BuildSchemaXML();

            Assert.AreEqual(
                schemaXml.InnerXml,
                SupportMethods.Getm2mRelationshipTypeExpectedSchema());
        }
    }
}
