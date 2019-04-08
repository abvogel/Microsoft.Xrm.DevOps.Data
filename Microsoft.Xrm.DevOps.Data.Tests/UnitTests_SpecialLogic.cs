using FakeXrmEasy;
using FakeXrmEasy.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Xrm.DevOps.Data.Tests
{
    [TestClass]
    public class SpecialLogic
    {
        [TestMethod]
        public void m2mRelationshipType()
        {
            // UniqueidentifierType stageid                            knowledgearticle
            // XrmFakedContext fakedContext = SupportMethods.SetupPrimitiveFakedService(
            //     SupportMethods.KnowledgeArticleLogicalName,
            //     SupportMethods.KnowledgeArticleDisplayName, 
            //     SupportMethods.GetUniqueIdentifierTypeEntity());
            // IOrganizationService fakedService = fakedContext.GetOrganizationService();

            // DataBuilder DataBuilder = new DataBuilder(fakedService);
            // DataBuilder.AppendData(SupportMethods.GetUniqueIdentifierTypeFetch());
            // Assert.AreEqual(
            //     DataBuilder.BuildDataXML().InnerXml, 
            //     SupportMethods.GetUniqueIdentifierTypeExpectedData());
            throw new NotImplementedException();
        }

        [TestMethod]
        public void InternalLookup_Data()
        {
            // Verify that a lookup to a schema already existing DOES add a record
            var fakedContext = new XrmFakedContext();
            fakedContext.InitializeMetadata(typeof(CrmEarlyBound.CrmServiceContext).Assembly);

            Guid targetGuid = Guid.NewGuid();
            Entity AccountWithExternalLookup = new Entity("account", Guid.NewGuid());
            AccountWithExternalLookup["createdby"] = new EntityReference("systemuser", targetGuid);

            Entity SystemUser = new Entity("systemuser", Guid.NewGuid());
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
            DataBuilder.AppendData("<fetch><entity name='account'><attribute name='accountid'/><attribute name='createdby'/></entity></fetch>");
            DataBuilder.AppendData("<fetch><entity name='systemuser'><attribute name='systemuserid'/><attribute name='firstname'/><attribute name='lastname'/></entity></fetch>");

            Assert.IsFalse(DataBuilder.BuildDataXML().SelectSingleNode("entities/entity[@name='systemuser']/records/record[@id='" + targetGuid + "']") == null);
        }

        [TestMethod]
        public void ExternalLookup_Schema()
        {
            // Verify that a lookup external to schema doesn't add anything.
            var fakedContext = new XrmFakedContext();
            fakedContext.InitializeMetadata(typeof(CrmEarlyBound.CrmServiceContext).Assembly);
            Entity AccountWithExternalLookup = new Entity("account", Guid.NewGuid());
            AccountWithExternalLookup["createdby"] = new EntityReference("systemuser", Guid.NewGuid());

            fakedContext.Initialize(AccountWithExternalLookup);
            fakedContext.AddExecutionMock<RetrieveEntityRequest>(req =>
            {
                var entityMetadata = fakedContext.GetEntityMetadataByName(SupportMethods.AccountLogicalName);
                entityMetadata.DisplayName = new Label(SupportMethods.AccountDisplayName, 1033);
                entityMetadata.Attributes.First(a => a.LogicalName == "createdby").SetSealedPropertyValue("DisplayName", new Label("Created By", 1033));
                entityMetadata.Attributes.First(a => a.LogicalName == "createdby").SetSealedPropertyValue("Targets", new String[] { "contact" });
                entityMetadata.Attributes.First(a => a.LogicalName == "accountid").SetSealedPropertyValue("DisplayName", new Label("Account", 1033));

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
            DataBuilder.AppendData("<fetch><entity name='account'><attribute name='accountid'/><attribute name='createdby'/></entity></fetch>");

            Assert.IsTrue(DataBuilder.BuildSchemaXML().SelectSingleNode("entities/entity[@name='contact']") == null);
        }

        [TestMethod]
        public void ExternalLookup_Data()
        {
            // Verify that a lookup external to schema doesn't add anything.
            var fakedContext = new XrmFakedContext();
            fakedContext.InitializeMetadata(typeof(CrmEarlyBound.CrmServiceContext).Assembly);
            Entity AccountWithExternalLookup = new Entity("account", Guid.NewGuid());
            AccountWithExternalLookup["createdby"] = new EntityReference("systemuser", Guid.NewGuid());

            fakedContext.Initialize(AccountWithExternalLookup);
            fakedContext.AddExecutionMock<RetrieveEntityRequest>(req =>
            {
                var entityMetadata = fakedContext.GetEntityMetadataByName(SupportMethods.AccountLogicalName);
                entityMetadata.DisplayName = new Label(SupportMethods.AccountDisplayName, 1033);

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
            DataBuilder.AppendData("<fetch><entity name='account'><attribute name='accountid'/><attribute name='createdby'/></entity></fetch>");

            Assert.IsTrue(DataBuilder.BuildDataXML().SelectSingleNode("entities/entity[@name='contact']") == null);
        }

        [TestMethod]
        public void ReflexiveRelationship_Data()
        {
            // LookupType           previousarticlecontentid           knowledgearticle
            XrmFakedContext fakedContext = SupportMethods.SetupPrimitiveFakedService(
                SupportMethods.KnowledgeArticleLogicalName,
                SupportMethods.KnowledgeArticleDisplayName,
                SupportMethods.GetLookupTypeEntity());
            IOrganizationService fakedService = fakedContext.GetOrganizationService();

            DataBuilder DataBuilder = new DataBuilder(fakedService);
            DataBuilder.AppendData(SupportMethods.GetLookupTypeFetch());

            Assert.IsTrue(DataBuilder.BuildDataXML().SelectSingleNode("entities/entity[@name='knowledgearticle']/records/record[@id='" + ((EntityReference)SupportMethods.GetLookupTypeEntity()["previousarticlecontentid"]).Id.ToString() + "']") != null);
        }
    }
}