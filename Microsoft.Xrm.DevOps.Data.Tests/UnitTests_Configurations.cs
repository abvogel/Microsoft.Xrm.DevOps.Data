using FakeXrmEasy;
using FakeXrmEasy.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Xrm.DevOps.Data.Tests
{
    [TestClass]
    public class Configurations : TestBase
    {
        [TestMethod]
        public void ObjectConstructor_Constructs()
        {
            XrmFakedContext context = new XrmFakedContext();
            DataBuilder DataBuilder = new DataBuilder(context.GetOrganizationService());

            Assert.IsInstanceOfType(DataBuilder, typeof(Microsoft.Xrm.DevOps.Data.DataBuilder));
        }

        [TestMethod]
        public void ContentType_Builds()
        {
            XrmFakedContext context = new XrmFakedContext();
            DataBuilder DataBuilder = new DataBuilder(context.GetOrganizationService());

            Assert.AreEqual(
                DataBuilder.BuildContentTypesXML().InnerXml,
                SupportMethods.LoadXmlFile(@"../../lib/Configurations/[Content_Types].xml"));
        }

        [TestMethod]
        public void AppendDataUsingDictionaryLikePowerShell_Works()
        {
            // StringType           description                        knowledgearticle
            var fakedContext = new XrmFakedContext();
            fakedContext.InitializeMetadata(typeof(CrmEarlyBound.CrmServiceContext).Assembly);
            fakedContext.Initialize(SupportMethods.GetStringTypeEntity());
            fakedContext.AddExecutionMock<RetrieveEntityRequest>(req =>
            {
                var entityMetadata = fakedContext.GetEntityMetadataByName(SupportMethods.KnowledgeArticleLogicalName);
                entityMetadata.DisplayName = new Label(SupportMethods.KnowledgeArticleDisplayName, 1033);
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
            DataBuilder.AppendData(SupportMethods.KnowledgeArticleLogicalName, SupportMethods.GetStringTypePowerShellObject());
            Assert.AreEqual(
                DataBuilder.BuildDataXML().InnerXml,
                SupportMethods.GetStringTypeExpectedData());
        }

        [TestMethod]
        public void AppendDataUsingDictionariesLikePowerShell_Works()
        {
            // StringType           description                        knowledgearticle
            var fakedContext = new XrmFakedContext();
            fakedContext.InitializeMetadata(typeof(CrmEarlyBound.CrmServiceContext).Assembly);
            fakedContext.Initialize(SupportMethods.GetStringTypeEntity());
            fakedContext.AddExecutionMock<RetrieveEntityRequest>(req =>
            {
                var entityMetadata = fakedContext.GetEntityMetadataByName(SupportMethods.KnowledgeArticleLogicalName);
                entityMetadata.DisplayName = new Label(SupportMethods.KnowledgeArticleDisplayName, 1033);
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
            DataBuilder.AppendData(SupportMethods.KnowledgeArticleLogicalName, SupportMethods.GetStringTypePowerShellObjects());
            Assert.AreEqual(
                DataBuilder.BuildDataXML().InnerXml,
                SupportMethods.GetStringTypeExpectedData());
        }

        [TestMethod]
        public void SingleIdentifierSchema()
        {
            var fakedContext = new XrmFakedContext();
            fakedContext.InitializeMetadata(typeof(CrmEarlyBound.CrmServiceContext).Assembly);
            fakedContext.Initialize(SupportMethods.GetSingleIdentifierEntity());
            fakedContext.AddExecutionMock<RetrieveEntityRequest>(req =>
            {
                var entityMetadata = fakedContext.GetEntityMetadataByName(SupportMethods.ContactLogicalName);
                entityMetadata.DisplayName = new Label(SupportMethods.ContactDisplayName, 1033);
                entityMetadata.SetSealedPropertyValue("PrimaryNameAttribute", "fullname");

                entityMetadata.Attributes.First(a => a.LogicalName == "firstname").SetSealedPropertyValue("DisplayName", new Label("First Name", 1033));
                entityMetadata.Attributes.First(a => a.LogicalName == "fullname").SetSealedPropertyValue("DisplayName", new Label("Full Name", 1033));
                entityMetadata.Attributes.First(a => a.LogicalName == "lastname").SetSealedPropertyValue("DisplayName", new Label("Last Name", 1033));
                entityMetadata.Attributes.First(a => a.LogicalName == "birthdate").SetSealedPropertyValue("DisplayName", new Label("Birthday", 1033));
                entityMetadata.Attributes.First(a => a.LogicalName == "emailaddress1").SetSealedPropertyValue("DisplayName", new Label("Email", 1033));
                entityMetadata.Attributes.First(a => a.LogicalName == "contactid").SetSealedPropertyValue("DisplayName", new Label("Contact", 1033));

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
            DataBuilder.AppendData(SupportMethods.GetSingleIdentifierFetch());
            DataBuilder.SetIdentifier(SupportMethods.ContactLogicalName, "emailaddress1");
            Assert.AreEqual(
                DataBuilder.BuildSchemaXML().InnerXml,
                SupportMethods.GetSingleIdentifierEntityExpectedSchema());
        }

        [TestMethod]
        public void MultiValueIdentifierSchema()
        {
            var fakedContext = new XrmFakedContext();
            fakedContext.InitializeMetadata(typeof(CrmEarlyBound.CrmServiceContext).Assembly);
            fakedContext.Initialize(SupportMethods.GetMultiValueIdentifierEntity());
            fakedContext.AddExecutionMock<RetrieveEntityRequest>(req =>
            {
                var entityMetadata = fakedContext.GetEntityMetadataByName(SupportMethods.ContactLogicalName);
                entityMetadata.DisplayName = new Label(SupportMethods.ContactDisplayName, 1033);
                entityMetadata.SetSealedPropertyValue("PrimaryNameAttribute", "fullname");

                entityMetadata.Attributes.First(a => a.LogicalName == "firstname").SetSealedPropertyValue("DisplayName", new Label("First Name", 1033));
                entityMetadata.Attributes.First(a => a.LogicalName == "fullname").SetSealedPropertyValue("DisplayName", new Label("Full Name", 1033));
                entityMetadata.Attributes.First(a => a.LogicalName == "lastname").SetSealedPropertyValue("DisplayName", new Label("Last Name", 1033));
                entityMetadata.Attributes.First(a => a.LogicalName == "birthdate").SetSealedPropertyValue("DisplayName", new Label("Birthday", 1033));
                entityMetadata.Attributes.First(a => a.LogicalName == "emailaddress1").SetSealedPropertyValue("DisplayName", new Label("Email", 1033));
                entityMetadata.Attributes.First(a => a.LogicalName == "contactid").SetSealedPropertyValue("DisplayName", new Label("Contact", 1033));

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
            DataBuilder.AppendData(SupportMethods.GetMultiValueIdentifierFetch());
            DataBuilder.SetIdentifier(SupportMethods.ContactLogicalName, new String[] { "firstname", "lastname", "birthdate" });
            Assert.AreEqual(
                DataBuilder.BuildSchemaXML().InnerXml,
                SupportMethods.GetMultiValueIdentifierEntityExpectedSchema());
        }

        [TestMethod]
        public void SingleEntity_AllPluginsDisabled()
        {
            var fakedContext = new XrmFakedContext();
            fakedContext.InitializeMetadata(typeof(CrmEarlyBound.CrmServiceContext).Assembly);
            fakedContext.Initialize(SupportMethods.GetSingleEntity_AllPluginsDisabledEntity());
            fakedContext.AddExecutionMock<RetrieveEntityRequest>(req =>
            {
                var entityMetadata = fakedContext.GetEntityMetadataByName(SupportMethods.AccountLogicalName);
                entityMetadata.DisplayName = new Label(SupportMethods.AccountDisplayName, 1033);
                entityMetadata.SetSealedPropertyValue("PrimaryNameAttribute", "name");

                entityMetadata.Attributes.First(a => a.LogicalName == "name").SetSealedPropertyValue("DisplayName", new Label("Account Name", 1033));
                entityMetadata.Attributes.First(a => a.LogicalName == "accountid").SetSealedPropertyValue("DisplayName", new Label("Account", 1033));
                entityMetadata.Attributes.First(a => a.LogicalName == "accountid").SetSealedPropertyValue("AttributeType", AttributeTypeCode.Uniqueidentifier);

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
            DataBuilder.SetPluginsDisabled(true);
            DataBuilder.AppendData(SupportMethods.GetSingleEntity_AllPluginsDisabledFetch());
            Assert.AreEqual(
                DataBuilder.BuildSchemaXML().InnerXml,
                SupportMethods.GetSingleEntity_AllPluginsDisabledEntityExpectedSchema());
        }

        [TestMethod]
        public void AllEntities_SomePluginsDisabled_MixedIdentifiers()
        {
            var fakedContext = new XrmFakedContext();
            fakedContext.InitializeMetadata(typeof(CrmEarlyBound.CrmServiceContext).Assembly);
            fakedContext.Initialize(SupportMethods.GetAllEntities_SomePluginsDisabled_MixedIdentifiersEntity());
            fakedContext.AddExecutionMock<RetrieveEntityRequest>(req =>
            {
                var logicalName = ((RetrieveEntityRequest)req).LogicalName;
                var entityMetadata = fakedContext.GetEntityMetadataByName(logicalName);

                switch (entityMetadata.LogicalName)
                {
                    case SupportMethods.ApprovalLogicalName:
                        entityMetadata.DisplayName = new Label(SupportMethods.ApprovalDisplayName, 1033);
                        entityMetadata.SetSealedPropertyValue("PrimaryNameAttribute", "subject");
                        entityMetadata.Attributes.First(a => a.LogicalName == "activityid").SetSealedPropertyValue("DisplayName", new Label("Activity", 1033));
                        entityMetadata.Attributes.First(a => a.LogicalName == "customers").SetSealedPropertyValue("DisplayName", new Label("Customers", 1033));
                        entityMetadata.Attributes.First(a => a.LogicalName == "customers").SetSealedPropertyValue("AttributeType", Sdk.Metadata.AttributeTypeCode.PartyList);
                        break;
                    case SupportMethods.IncidentLogicalName:
                        entityMetadata.DisplayName = new Label(SupportMethods.IncidentDisplayName, 1033);
                        entityMetadata.SetSealedPropertyValue("PrimaryNameAttribute", "title");
                        entityMetadata.Attributes.First(a => a.LogicalName == "incidentid").SetSealedPropertyValue("DisplayName", new Label("Case", 1033));
                        entityMetadata.Attributes.First(a => a.LogicalName == "customerid").SetSealedPropertyValue("DisplayName", new Label("Customer", 1033));
                        entityMetadata.Attributes.First(a => a.LogicalName == "customerid").SetSealedPropertyValue("AttributeType", Sdk.Metadata.AttributeTypeCode.Customer);
                        entityMetadata.Attributes.First(a => a.LogicalName == "customerid").SetSealedPropertyValue("Targets", new String[] { "account", "contact" });
                        break;
                    case SupportMethods.InvoiceLogicalName:
                        entityMetadata.DisplayName = new Label(SupportMethods.InvoiceDisplayName, 1033);
                        entityMetadata.SetSealedPropertyValue("PrimaryNameAttribute", "name");
                        entityMetadata.Attributes.First(a => a.LogicalName == "invoiceid").SetSealedPropertyValue("DisplayName", new Label("Invoice", 1033));
                        entityMetadata.Attributes.First(a => a.LogicalName == "totaltax").SetSealedPropertyValue("DisplayName", new Label("Total Tax", 1033));
                        break;
                    case SupportMethods.KnowledgeArticleLogicalName:
                        entityMetadata.DisplayName = new Label(SupportMethods.KnowledgeArticleDisplayName, 1033);
                        entityMetadata.SetSealedPropertyValue("PrimaryNameAttribute", "title");
                        entityMetadata.Attributes.First(a => a.LogicalName == "keywords").SetSealedPropertyValue("DisplayName", new Label("Keywords", 1033));
                        entityMetadata.Attributes.First(a => a.LogicalName == "keywords").SetSealedPropertyValue("AttributeType", Sdk.Metadata.AttributeTypeCode.Memo);
                        entityMetadata.Attributes.First(a => a.LogicalName == "knowledgearticleid").SetSealedPropertyValue("DisplayName", new Label("Knowledge Article", 1033));
                        entityMetadata.Attributes.First(a => a.LogicalName == "publishon").SetSealedPropertyValue("DisplayName", new Label("Publish On", 1033));
                        entityMetadata.Attributes.First(a => a.LogicalName == "expirationstateid").SetSealedPropertyValue("DisplayName", new Label("Expiration State Id", 1033));
                        entityMetadata.Attributes.First(a => a.LogicalName == "previousarticlecontentid").SetSealedPropertyValue("DisplayName", new Label("Previous Article Content ID", 1033));
                        entityMetadata.Attributes.First(a => a.LogicalName == "previousarticlecontentid").SetSealedPropertyValue("Targets", new String[] { "knowledgearticle" });
                        entityMetadata.Attributes.First(a => a.LogicalName == "ownerid").SetSealedPropertyValue("DisplayName", new Label("Owner", 1033));
                        entityMetadata.Attributes.First(a => a.LogicalName == "ownerid").SetSealedPropertyValue("AttributeType", Sdk.Metadata.AttributeTypeCode.Owner);
                        entityMetadata.Attributes.First(a => a.LogicalName == "expiredreviewoptions").SetSealedPropertyValue("DisplayName", new Label("Expired Review Options", 1033));
                        entityMetadata.Attributes.First(a => a.LogicalName == "statecode").SetSealedPropertyValue("DisplayName", new Label("Status", 1033));
                        entityMetadata.Attributes.First(a => a.LogicalName == "statuscode").SetSealedPropertyValue("DisplayName", new Label("Status Reason", 1033));
                        entityMetadata.Attributes.First(a => a.LogicalName == "stageid").SetSealedPropertyValue("DisplayName", new Label("Stage Id", 1033));
                        entityMetadata.Attributes.First(a => a.LogicalName == "stageid").SetSealedPropertyValue("AttributeType", Sdk.Metadata.AttributeTypeCode.Uniqueidentifier);
                        entityMetadata.Attributes.First(a => a.LogicalName == "description").SetSealedPropertyValue("DisplayName", new Label("Short Description", 1033));
                        entityMetadata.Attributes.First(a => a.LogicalName == "articlepublicnumber").SetSealedPropertyValue("DisplayName", new Label("Article Public Number", 1033));
                        break;
                    case SupportMethods.PurchaseOrderProductLogicalName:
                        entityMetadata.DisplayName = new Label(SupportMethods.PurchaseOrderProductDisplayName, 1033);
                        entityMetadata.SetSealedPropertyValue("PrimaryNameAttribute", "msdyn_name");
                        entityMetadata.Attributes.First(a => a.LogicalName == "msdyn_quantity").SetSealedPropertyValue("DisplayName", new Label("Quantity", 1033));
                        entityMetadata.Attributes.First(a => a.LogicalName == "msdyn_quantity").SetSealedPropertyValue("IsCustomAttribute", true);
                        entityMetadata.Attributes.First(a => a.LogicalName == "msdyn_purchaseorderproductid").SetSealedPropertyValue("DisplayName", new Label("Purchase Order Product", 1033));
                        break;
                    case SupportMethods.ResourceRequirementDetailLogicalName:
                        entityMetadata.DisplayName = new Label(SupportMethods.ResourceRequirementDetailDisplayName, 1033);
                        entityMetadata.SetSealedPropertyValue("PrimaryNameAttribute", "msdyn_name");
                        entityMetadata.Attributes.First(a => a.LogicalName == "msdyn_hours").SetSealedPropertyValue("DisplayName", new Label("Hours", 1033));
                        entityMetadata.Attributes.First(a => a.LogicalName == "msdyn_hours").SetSealedPropertyValue("IsCustomAttribute", true);
                        entityMetadata.Attributes.First(a => a.LogicalName == "msdyn_resourcerequirementdetailid").SetSealedPropertyValue("DisplayName", new Label("Resource Requirement Detail", 1033));
                        break;
                    case SupportMethods.ThemeLogicalName:
                        entityMetadata.DisplayName = new Label(SupportMethods.ThemeDisplayName, 1033);
                        entityMetadata.SetSealedPropertyValue("PrimaryNameAttribute", "name");
                        entityMetadata.Attributes.First(a => a.LogicalName == "themeid").SetSealedPropertyValue("DisplayName", new Label("Theme", 1033));
                        entityMetadata.Attributes.First(a => a.LogicalName == "isdefaulttheme").SetSealedPropertyValue("DisplayName", new Label("Default Theme", 1033));
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

            IOrganizationService fakedService = fakedContext.GetOrganizationService();

            DataBuilder DataBuilder = new DataBuilder(fakedService);
            DataBuilder.AppendData(SupportMethods.GetAllEntities_SomePluginsDisabled_MixedIdentifiersThemeFetch());
            DataBuilder.AppendData(SupportMethods.GetAllEntities_SomePluginsDisabled_MixedIdentifiersIncidentFetch());
            DataBuilder.SetPluginsDisabled("incident", true);
            DataBuilder.AppendData(SupportMethods.GetAllEntities_SomePluginsDisabled_MixedIdentifiersKnowledgeArticleFetch());
            DataBuilder.SetIdentifier(SupportMethods.KnowledgeArticleLogicalName, "articlepublicnumber");
            DataBuilder.SetPluginsDisabled("knowledgearticle", true);
            DataBuilder.AppendData(SupportMethods.GetAllEntities_SomePluginsDisabled_MixedIdentifiersResourceRequirementDetailFetch());
            DataBuilder.AppendData(SupportMethods.GetAllEntities_SomePluginsDisabled_MixedIdentifiersPurchaseOrderProductFetch());
            DataBuilder.AppendData(SupportMethods.GetAllEntities_SomePluginsDisabled_MixedIdentifiersInvoiceFetch());
            DataBuilder.AppendData(SupportMethods.GetAllEntities_SomePluginsDisabled_MixedIdentifiersApprovalFetch());
            Assert.AreEqual(
                DataBuilder.BuildSchemaXML().InnerXml,
                SupportMethods.GetAllEntities_SomePluginsDisabled_MixedIdentifiersExpectedSchema());
        }

        [TestMethod]
        public void AllEntities_SomePluginsDisabled_MixedIdentifiers_SchemaImported()
        {
            XrmFakedContext fakedContext = new XrmFakedContext();
            DataBuilder db = new DataBuilder();
            db.AppendData(SupportMethods.GetAllEntities_SomePluginsDisabled_MixedIdentifiersExpectedData(), SupportMethods.GetAllEntities_SomePluginsDisabled_MixedIdentifiersExpectedSchema());

            Assert.AreEqual(
                db.BuildSchemaXML().InnerXml,
                SupportMethods.GetAllEntities_SomePluginsDisabled_MixedIdentifiersExpectedSchema());
        }

        [TestMethod]
        public void AllEntities_SomePluginsDisabled_MixedIdentifiers_DataImported()
        {
            XrmFakedContext fakedContext = new XrmFakedContext();
            DataBuilder db = new DataBuilder();
            db.AppendData(SupportMethods.GetAllEntities_SomePluginsDisabled_MixedIdentifiersExpectedData(), SupportMethods.GetAllEntities_SomePluginsDisabled_MixedIdentifiersExpectedSchema());

            Assert.AreEqual(
                db.BuildDataXML().InnerXml,
                SupportMethods.GetAllEntities_SomePluginsDisabled_MixedIdentifiersExpectedData());
        }

        [TestMethod]
        public void SingleEntity_MoreThan5000Records()
        {
            XrmFakedContext fakedContext = SupportMethods.SetupPrimitiveFakedService(
                SupportMethods.ThemeLogicalName,
                SupportMethods.ThemeDisplayName,
                SupportMethods.GetSingleEntity_MoreThan5000RowsEntity().ToArray());
            IOrganizationService fakedService = fakedContext.GetOrganizationService();

            DataBuilder DataBuilder = new DataBuilder(fakedService);
            DataBuilder.AppendData(SupportMethods.GetBooleanTypeFetch_NoTopOrCount());

            var innerXml = DataBuilder.BuildDataXML();
            Assert.IsTrue(innerXml.SelectSingleNode("//entities/entity/records").ChildNodes.Count > 5000);
        }

        [TestMethod]
        public void SingleEntity_MoreThan5000RecordsWithTopIsOne_ReturnsOne()
        {
            XrmFakedContext fakedContext = SupportMethods.SetupPrimitiveFakedService(
                SupportMethods.ThemeLogicalName,
                SupportMethods.ThemeDisplayName,
                SupportMethods.GetSingleEntity_MoreThan5000RowsEntity().ToArray());
            IOrganizationService fakedService = fakedContext.GetOrganizationService();

            DataBuilder DataBuilder = new DataBuilder(fakedService);
            DataBuilder.AppendData(SupportMethods.GetBooleanTypeFetch_TopIsOne());

            var innerXml = DataBuilder.BuildDataXML();
            Assert.IsTrue(innerXml.SelectSingleNode("//entities/entity/records").ChildNodes.Count == 1);
        }


        [TestMethod]
        public void SingleEntity_TenRecords_WithCountAsOne_ReturnsTen()
        {
            XrmFakedContext fakedContext = SupportMethods.SetupPrimitiveFakedService(
                SupportMethods.ThemeLogicalName,
                SupportMethods.ThemeDisplayName,
                SupportMethods.GetSingleEntity_TenRowsEntity().ToArray());
            IOrganizationService fakedService = fakedContext.GetOrganizationService();

            DataBuilder DataBuilder = new DataBuilder(fakedService);
            DataBuilder.AppendData(SupportMethods.GetBooleanTypeFetch_CountIsOne());

            var innerXml = DataBuilder.BuildDataXML();
            Assert.IsTrue(innerXml.SelectSingleNode("//entities/entity/records").ChildNodes.Count == 10);
        }

        [TestMethod]
        public void MultipleSourcesOfMetadata_DataImported()
        {
            var fakedContext = new XrmFakedContext();
            fakedContext.InitializeMetadata(typeof(CrmEarlyBound.CrmServiceContext).Assembly);

            Guid targetGuid = Guid.Parse("f0205357-124a-4b5a-9163-5cca2b3d3e8d");
            Entity AccountWithExternalLookup = new Entity("account", Guid.Parse("832193cb-f381-47c0-b059-662bdfcf1261"));
            AccountWithExternalLookup["createdby"] = new EntityReference("systemuser", targetGuid);

            Entity SystemUser = new Entity("systemuser", Guid.Parse("16b8a7ea-7d3f-4856-85a1-59f98ffcfafe"));
            SystemUser["firstname"] = "Andrew";
            SystemUser["lastname"] = "Vogel";

            fakedContext.Initialize(new List<Entity>() { AccountWithExternalLookup, SystemUser });
            fakedContext.AddExecutionMock<RetrieveEntityRequest>(req =>
            {
                var logicalName = ((RetrieveEntityRequest)req).LogicalName;
                var entityMetadata = fakedContext.GetEntityMetadataByName(logicalName);

                switch (entityMetadata.LogicalName)
                {
                    case SupportMethods.AccountLogicalName:
                        entityMetadata.DisplayName = new Label(SupportMethods.AccountDisplayName, 1033);
                        entityMetadata.Attributes.First(a => a.LogicalName == "createdby").SetSealedPropertyValue("DisplayName", new Label("Created By", 1033));
                        entityMetadata.Attributes.First(a => a.LogicalName == "createdby").SetSealedPropertyValue("Targets", new String[] { "contact" });
                        entityMetadata.Attributes.First(a => a.LogicalName == "accountid").SetSealedPropertyValue("DisplayName", new Label("Account", 1033));
                        break;
                    case SupportMethods.UserLogicalName:
                        entityMetadata.DisplayName = new Label(SupportMethods.UserDisplayName, 1033);
                        entityMetadata.Attributes.First(a => a.LogicalName == "firstname").SetSealedPropertyValue("DisplayName", new Label("First Name", 1033));
                        entityMetadata.Attributes.First(a => a.LogicalName == "lastname").SetSealedPropertyValue("DisplayName", new Label("Last Name", 1033));
                        entityMetadata.Attributes.First(a => a.LogicalName == "systemuserid").SetSealedPropertyValue("DisplayName", new Label("User", 1033));
                        entityMetadata.ManyToManyRelationships.First(m => m.SchemaName == "systemuserroles_association").SetSealedPropertyValue("IntersectEntityName", "systemuserroles");
                        entityMetadata.ManyToManyRelationships.First(m => m.SchemaName == "systemuserroles_association").SetSealedPropertyValue("Entity2LogicalName", "role");
                        entityMetadata.ManyToManyRelationships.First(m => m.SchemaName == "systemuserroles_association").SetSealedPropertyValue("Entity2IntersectAttribute", "roleid");
                        break;
                    case SupportMethods.ThemeLogicalName:
                        entityMetadata.DisplayName = new Label(SupportMethods.ThemeDisplayName, 1033);
                        break;
                    case SupportMethods.IncidentLogicalName:
                        entityMetadata.DisplayName = new Label(SupportMethods.IncidentDisplayName, 1033);
                        break;
                    case SupportMethods.ResourceRequirementDetailLogicalName:
                        entityMetadata.DisplayName = new Label(SupportMethods.ResourceRequirementDetailDisplayName, 1033);
                        break;
                    case SupportMethods.PurchaseOrderProductLogicalName:
                        entityMetadata.DisplayName = new Label(SupportMethods.PurchaseOrderProductDisplayName, 1033);
                        break;
                    case SupportMethods.InvoiceLogicalName:
                        entityMetadata.DisplayName = new Label(SupportMethods.InvoiceDisplayName, 1033);
                        break;
                    case SupportMethods.ApprovalLogicalName:
                        entityMetadata.DisplayName = new Label(SupportMethods.ApprovalDisplayName, 1033);
                        break;
                    case SupportMethods.SecurityRoleLogicalName:
                        entityMetadata.DisplayName = new Label(SupportMethods.SecurityRoleDisplayName, 1033);
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

            IOrganizationService fakedService = fakedContext.GetOrganizationService();

            DataBuilder db = new DataBuilder(fakedService);
            db.AppendData("<fetch><entity name='account'><attribute name='accountid'/><attribute name='createdby'/></entity></fetch>");

            db.AppendData(SupportMethods.GetBooleanTypeExpectedData(), SupportMethods.GetBooleanTypeExpectedSchema());
            db.AppendData(SupportMethods.GetCustomerTypeExpectedData(), SupportMethods.GetCustomerTypeExpectedSchema());
            db.AppendData(SupportMethods.GetDateTimeTypeExpectedData(), SupportMethods.GetDateTimeTypeExpectedSchema());
            db.AppendData(SupportMethods.GetDecimalTypeExpectedData(), SupportMethods.GetDecimalTypeExpectedSchema());
            db.AppendData(SupportMethods.GetDoubleTypeExpectedData(), SupportMethods.GetDoubleTypeExpectedSchema());
            db.AppendData(SupportMethods.GetIntegerTypeExpectedData(), SupportMethods.GetIntegerTypeExpectedSchema());
            db.AppendData(SupportMethods.GetLookupTypeExpectedData(), SupportMethods.GetLookupTypeExpectedSchema());
            db.AppendData(SupportMethods.GetMemoTypeExpectedData(), SupportMethods.GetMemoTypeExpectedSchema());
            db.AppendData(SupportMethods.GetMoneyTypeExpectedData(), SupportMethods.GetMoneyTypeExpectedSchema());
            db.AppendData(SupportMethods.GetOwnerTypeExpectedData(), SupportMethods.GetOwnerTypeExpectedSchema());
            db.AppendData(SupportMethods.GetPartyListTypeExpectedData(), SupportMethods.GetPartyListTypeExpectedSchema());
            db.AppendData(SupportMethods.GetPicklistTypeExpectedData(), SupportMethods.GetPicklistTypeExpectedSchema());
            db.AppendData(SupportMethods.GetStateTypeExpectedData(), SupportMethods.GetStateTypeExpectedSchema());
            db.AppendData(SupportMethods.GetStatusTypeExpectedData(), SupportMethods.GetStatusTypeExpectedSchema());
            db.AppendData(SupportMethods.GetStringTypeExpectedData(), SupportMethods.GetStringTypeExpectedSchema());
            db.AppendData(SupportMethods.GetUniqueIdentifierTypeExpectedData(), SupportMethods.GetUniqueIdentifierTypeExpectedSchema());
            db.AppendData(SupportMethods.Getm2mRelationshipTypeExpectedData(), SupportMethods.Getm2mRelationshipTypeExpectedSchema());

            db.AppendData("<fetch><entity name='systemuser'><attribute name='systemuserid'/><attribute name='firstname'/><attribute name='lastname'/></entity></fetch>");

            db.SetIdentifier("systemuser", "systemuserid");

            Assert.AreEqual(
                db.BuildDataXML().InnerXml,
                SupportMethods.LoadXmlFile("../../lib/Configurations/MultipleSourcesOfMetadata_data.xml"));
        }

        [TestMethod]
        public void MultipleSourcesOfMetadata_SchemaImported()
        {
            var fakedContext = new XrmFakedContext();
            fakedContext.InitializeMetadata(typeof(CrmEarlyBound.CrmServiceContext).Assembly);

            Guid targetGuid = Guid.Parse("f0205357-124a-4b5a-9163-5cca2b3d3e8d");
            Entity AccountWithExternalLookup = new Entity("account", Guid.Parse("832193cb-f381-47c0-b059-662bdfcf1261"));
            AccountWithExternalLookup["createdby"] = new EntityReference("systemuser", targetGuid);

            Entity SystemUser = new Entity("systemuser", Guid.Parse("16b8a7ea-7d3f-4856-85a1-59f98ffcfafe"));
            SystemUser["firstname"] = "Andrew";
            SystemUser["lastname"] = "Vogel";

            fakedContext.Initialize(new List<Entity>() { AccountWithExternalLookup, SystemUser });
            fakedContext.AddExecutionMock<RetrieveEntityRequest>(req =>
            {
                var logicalName = ((RetrieveEntityRequest)req).LogicalName;
                var entityMetadata = fakedContext.GetEntityMetadataByName(logicalName);

                switch (entityMetadata.LogicalName)
                {
                    case SupportMethods.AccountLogicalName:
                        entityMetadata.DisplayName = new Label(SupportMethods.AccountDisplayName, 1033);
                        entityMetadata.Attributes.First(a => a.LogicalName == "createdby").SetSealedPropertyValue("DisplayName", new Label("Created By", 1033));
                        entityMetadata.Attributes.First(a => a.LogicalName == "createdby").SetSealedPropertyValue("Targets", new String[] { "contact" });
                        entityMetadata.Attributes.First(a => a.LogicalName == "accountid").SetSealedPropertyValue("DisplayName", new Label("Account", 1033));
                        break;
                    case SupportMethods.UserLogicalName:
                        entityMetadata.DisplayName = new Label(SupportMethods.UserDisplayName, 1033);
                        entityMetadata.Attributes.First(a => a.LogicalName == "firstname").SetSealedPropertyValue("DisplayName", new Label("First Name", 1033));
                        entityMetadata.Attributes.First(a => a.LogicalName == "lastname").SetSealedPropertyValue("DisplayName", new Label("Last Name", 1033));
                        entityMetadata.Attributes.First(a => a.LogicalName == "systemuserid").SetSealedPropertyValue("DisplayName", new Label("User", 1033));
                        entityMetadata.ManyToManyRelationships.First(m => m.SchemaName == "systemuserroles_association").SetSealedPropertyValue("IntersectEntityName", "systemuserroles");
                        entityMetadata.ManyToManyRelationships.First(m => m.SchemaName == "systemuserroles_association").SetSealedPropertyValue("Entity2LogicalName", "role");
                        entityMetadata.ManyToManyRelationships.First(m => m.SchemaName == "systemuserroles_association").SetSealedPropertyValue("Entity2IntersectAttribute", "roleid");
                        break;
                    case SupportMethods.ThemeLogicalName:
                        entityMetadata.DisplayName = new Label(SupportMethods.ThemeDisplayName, 1033);
                        entityMetadata.SetSealedPropertyValue("PrimaryNameAttribute", "name");
                        entityMetadata.Attributes.First(a => a.LogicalName == "themeid").SetSealedPropertyValue("DisplayName", new Label("Theme", 1033));
                        entityMetadata.Attributes.First(a => a.LogicalName == "isdefaulttheme").SetSealedPropertyValue("DisplayName", new Label("Default Theme", 1033));
                        break;
                    case SupportMethods.IncidentLogicalName:
                        entityMetadata.DisplayName = new Label(SupportMethods.IncidentDisplayName, 1033);
                        entityMetadata.SetSealedPropertyValue("PrimaryNameAttribute", "title");
                        entityMetadata.Attributes.First(a => a.LogicalName == "incidentid").SetSealedPropertyValue("DisplayName", new Label("Case", 1033));
                        entityMetadata.Attributes.First(a => a.LogicalName == "customerid").SetSealedPropertyValue("DisplayName", new Label("Customer", 1033));
                        entityMetadata.Attributes.First(a => a.LogicalName == "customerid").SetSealedPropertyValue("AttributeType", Sdk.Metadata.AttributeTypeCode.Customer);
                        entityMetadata.Attributes.First(a => a.LogicalName == "customerid").SetSealedPropertyValue("Targets", new String[] { "account", "contact" });
                        break;
                    case SupportMethods.ResourceRequirementDetailLogicalName:
                        entityMetadata.DisplayName = new Label(SupportMethods.ResourceRequirementDetailDisplayName, 1033);
                        entityMetadata.SetSealedPropertyValue("PrimaryNameAttribute", "msdyn_name");
                        entityMetadata.Attributes.First(a => a.LogicalName == "msdyn_hours").SetSealedPropertyValue("DisplayName", new Label("Hours", 1033));
                        entityMetadata.Attributes.First(a => a.LogicalName == "msdyn_hours").SetSealedPropertyValue("IsCustomAttribute", true);
                        entityMetadata.Attributes.First(a => a.LogicalName == "msdyn_resourcerequirementdetailid").SetSealedPropertyValue("DisplayName", new Label("Resource Requirement Detail", 1033));
                        break;
                    case SupportMethods.PurchaseOrderProductLogicalName:
                        entityMetadata.DisplayName = new Label(SupportMethods.PurchaseOrderProductDisplayName, 1033);
                        entityMetadata.SetSealedPropertyValue("PrimaryNameAttribute", "msdyn_name");
                        entityMetadata.Attributes.First(a => a.LogicalName == "msdyn_quantity").SetSealedPropertyValue("DisplayName", new Label("Quantity", 1033));
                        entityMetadata.Attributes.First(a => a.LogicalName == "msdyn_quantity").SetSealedPropertyValue("IsCustomAttribute", true);
                        entityMetadata.Attributes.First(a => a.LogicalName == "msdyn_purchaseorderproductid").SetSealedPropertyValue("DisplayName", new Label("Purchase Order Product", 1033));
                        break;
                    case SupportMethods.InvoiceLogicalName:
                        entityMetadata.DisplayName = new Label(SupportMethods.InvoiceDisplayName, 1033);
                        entityMetadata.SetSealedPropertyValue("PrimaryNameAttribute", "name");
                        entityMetadata.Attributes.First(a => a.LogicalName == "invoiceid").SetSealedPropertyValue("DisplayName", new Label("Invoice", 1033));
                        entityMetadata.Attributes.First(a => a.LogicalName == "totaltax").SetSealedPropertyValue("DisplayName", new Label("Total Tax", 1033));
                        break;
                    case SupportMethods.ApprovalLogicalName:
                        entityMetadata.DisplayName = new Label(SupportMethods.ApprovalDisplayName, 1033);
                        entityMetadata.SetSealedPropertyValue("PrimaryNameAttribute", "subject");
                        entityMetadata.Attributes.First(a => a.LogicalName == "activityid").SetSealedPropertyValue("DisplayName", new Label("Activity", 1033));
                        entityMetadata.Attributes.First(a => a.LogicalName == "customers").SetSealedPropertyValue("DisplayName", new Label("Customers", 1033));
                        entityMetadata.Attributes.First(a => a.LogicalName == "customers").SetSealedPropertyValue("AttributeType", Sdk.Metadata.AttributeTypeCode.PartyList);
                        break;
                    case SupportMethods.SecurityRoleLogicalName:
                        entityMetadata.DisplayName = new Label(SupportMethods.SecurityRoleDisplayName, 1033);
                        entityMetadata.Attributes.First(a => a.LogicalName == "roleid").SetSealedPropertyValue("DisplayName", new Label("Role", 1033));
                        break;
                    case SupportMethods.KnowledgeArticleLogicalName:
                        entityMetadata.DisplayName = new Label(SupportMethods.KnowledgeArticleDisplayName, 1033);
                        entityMetadata.SetSealedPropertyValue("PrimaryNameAttribute", "title");
                        entityMetadata.Attributes.First(a => a.LogicalName == "keywords").SetSealedPropertyValue("DisplayName", new Label("Keywords", 1033));
                        entityMetadata.Attributes.First(a => a.LogicalName == "keywords").SetSealedPropertyValue("AttributeType", Sdk.Metadata.AttributeTypeCode.Memo);
                        entityMetadata.Attributes.First(a => a.LogicalName == "knowledgearticleid").SetSealedPropertyValue("DisplayName", new Label("Knowledge Article", 1033));
                        entityMetadata.Attributes.First(a => a.LogicalName == "publishon").SetSealedPropertyValue("DisplayName", new Label("Publish On", 1033));
                        entityMetadata.Attributes.First(a => a.LogicalName == "expirationstateid").SetSealedPropertyValue("DisplayName", new Label("Expiration State Id", 1033));
                        entityMetadata.Attributes.First(a => a.LogicalName == "previousarticlecontentid").SetSealedPropertyValue("DisplayName", new Label("Previous Article Content ID", 1033));
                        entityMetadata.Attributes.First(a => a.LogicalName == "previousarticlecontentid").SetSealedPropertyValue("Targets", new String[] { "knowledgearticle" });
                        entityMetadata.Attributes.First(a => a.LogicalName == "ownerid").SetSealedPropertyValue("DisplayName", new Label("Owner", 1033));
                        entityMetadata.Attributes.First(a => a.LogicalName == "ownerid").SetSealedPropertyValue("AttributeType", Sdk.Metadata.AttributeTypeCode.Owner);
                        entityMetadata.Attributes.First(a => a.LogicalName == "expiredreviewoptions").SetSealedPropertyValue("DisplayName", new Label("Expired Review Options", 1033));
                        entityMetadata.Attributes.First(a => a.LogicalName == "statecode").SetSealedPropertyValue("DisplayName", new Label("Status", 1033));
                        entityMetadata.Attributes.First(a => a.LogicalName == "statuscode").SetSealedPropertyValue("DisplayName", new Label("Status Reason", 1033));
                        entityMetadata.Attributes.First(a => a.LogicalName == "stageid").SetSealedPropertyValue("DisplayName", new Label("Stage Id", 1033));
                        entityMetadata.Attributes.First(a => a.LogicalName == "stageid").SetSealedPropertyValue("AttributeType", Sdk.Metadata.AttributeTypeCode.Uniqueidentifier);
                        entityMetadata.Attributes.First(a => a.LogicalName == "description").SetSealedPropertyValue("DisplayName", new Label("Short Description", 1033));
                        entityMetadata.Attributes.First(a => a.LogicalName == "articlepublicnumber").SetSealedPropertyValue("DisplayName", new Label("Article Public Number", 1033));
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

            IOrganizationService fakedService = fakedContext.GetOrganizationService();

            DataBuilder db = new DataBuilder(fakedService);
            db.AppendData("<fetch><entity name='account'><attribute name='accountid'/><attribute name='createdby'/></entity></fetch>");

            db.AppendData(SupportMethods.GetBooleanTypeExpectedData(), SupportMethods.GetBooleanTypeExpectedSchema());
            db.AppendData(SupportMethods.GetCustomerTypeExpectedData(), SupportMethods.GetCustomerTypeExpectedSchema());
            db.AppendData(SupportMethods.GetDateTimeTypeExpectedData(), SupportMethods.GetDateTimeTypeExpectedSchema());
            db.AppendData(SupportMethods.GetDecimalTypeExpectedData(), SupportMethods.GetDecimalTypeExpectedSchema());
            db.AppendData(SupportMethods.GetDoubleTypeExpectedData(), SupportMethods.GetDoubleTypeExpectedSchema());
            db.AppendData(SupportMethods.GetIntegerTypeExpectedData(), SupportMethods.GetIntegerTypeExpectedSchema());
            db.AppendData(SupportMethods.GetLookupTypeExpectedData(), SupportMethods.GetLookupTypeExpectedSchema());
            db.AppendData(SupportMethods.GetMemoTypeExpectedData(), SupportMethods.GetMemoTypeExpectedSchema());
            db.AppendData(SupportMethods.GetMoneyTypeExpectedData(), SupportMethods.GetMoneyTypeExpectedSchema());
            db.AppendData(SupportMethods.GetOwnerTypeExpectedData(), SupportMethods.GetOwnerTypeExpectedSchema());
            db.AppendData(SupportMethods.GetPartyListTypeExpectedData(), SupportMethods.GetPartyListTypeExpectedSchema());
            db.AppendData(SupportMethods.GetPicklistTypeExpectedData(), SupportMethods.GetPicklistTypeExpectedSchema());
            db.AppendData(SupportMethods.GetStateTypeExpectedData(), SupportMethods.GetStateTypeExpectedSchema());
            db.AppendData(SupportMethods.GetStatusTypeExpectedData(), SupportMethods.GetStatusTypeExpectedSchema());
            db.AppendData(SupportMethods.GetStringTypeExpectedData(), SupportMethods.GetStringTypeExpectedSchema());
            db.AppendData(SupportMethods.GetUniqueIdentifierTypeExpectedData(), SupportMethods.GetUniqueIdentifierTypeExpectedSchema());
            db.AppendData(SupportMethods.Getm2mRelationshipTypeExpectedData(), SupportMethods.Getm2mRelationshipTypeExpectedSchema());

            db.AppendData("<fetch><entity name='systemuser'><attribute name='systemuserid'/><attribute name='firstname'/><attribute name='lastname'/></entity></fetch>");

            db.SetIdentifier("systemuser", "systemuserid");

            Assert.AreEqual(
                db.BuildSchemaXML().InnerXml,
                SupportMethods.LoadXmlFile("../../lib/Configurations/MultipleSourcesOfMetadata_schema.xml"));
        }
    }
}