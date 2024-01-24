using FakeXrmEasy;
using FakeXrmEasy.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace Microsoft.Xrm.DevOps.Data.Tests
{
    [TestClass]
    public class DataPrimitives : TestBase
    {
        /// <summary>
        /// <see cref="DynamicDataAttribute"/> directly doesn't want to invoke base class GetCultures method, so just "duplicate:  it here
        /// </summary>
        public static IEnumerable<object[]> GetCulturesTestData() => GetCultures();

        [TestMethod]
        public void BooleanType()
        {
            XrmFakedContext fakedContext = SupportMethods.SetupPrimitiveFakedService(
                SupportMethods.ThemeLogicalName, 
                SupportMethods.ThemeDisplayName, 
                SupportMethods.GetBooleanTypeEntity());
            IOrganizationService fakedService = fakedContext.GetOrganizationService();

            DataBuilder DataBuilder = new DataBuilder(fakedService);
            DataBuilder.AppendData(SupportMethods.GetBooleanTypeFetch_TopIsOne());
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

        [DataTestMethod]
        [DynamicData(nameof(GetCulturesTestData), DynamicDataSourceType.Method)]
        public void DecimalType(CultureInfo culture)
        {
            SetCulture(culture);
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

        [DataTestMethod]
        [DynamicData(nameof(GetCulturesTestData), DynamicDataSourceType.Method)]
        public void DoubleType(CultureInfo culture)
        {
            SetCulture(culture);
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

        [DataTestMethod]
        [DynamicData(nameof(GetCulturesTestData), DynamicDataSourceType.Method)]
        public void MoneyType(CultureInfo culture)
        {
            SetCulture(culture);
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
            var fakedContext = new XrmFakedContext();
            fakedContext.InitializeMetadata(typeof(CrmEarlyBound.CrmServiceContext).Assembly);
            fakedContext.Initialize(SupportMethods.GetUniqueIdentifierTypeEntity());
            fakedContext.AddExecutionMock<RetrieveEntityRequest>(req =>
            {
                var entityMetadata = fakedContext.GetEntityMetadataByName(SupportMethods.KnowledgeArticleLogicalName);
                entityMetadata.DisplayName = new Label(SupportMethods.KnowledgeArticleDisplayName, 1033);
                entityMetadata.Attributes.First(a => a.LogicalName == "stageid").SetSealedPropertyValue("AttributeType", Sdk.Metadata.AttributeTypeCode.Uniqueidentifier);

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
                DataBuilder.BuildDataXML().InnerXml, 
                SupportMethods.GetUniqueIdentifierTypeExpectedData());
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

            var dataXml = DataBuilder.BuildDataXML();

            Assert.AreEqual(
                dataXml.InnerXml,
                SupportMethods.Getm2mRelationshipTypeExpectedData());
        }
    }
}
