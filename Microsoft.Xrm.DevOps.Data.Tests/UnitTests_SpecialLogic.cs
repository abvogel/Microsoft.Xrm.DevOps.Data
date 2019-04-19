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
            var fakedContext = new XrmFakedContext();
            fakedContext.InitializeMetadata(typeof(CrmEarlyBound.CrmServiceContext).Assembly);

            var userId = new Guid("00e7b0b9-1ace-e711-a970-000d3a192311");
            var businessId = Guid.NewGuid();

            var testUser = new Entity("systemuser")
            {
                Id = userId
            };

            var testRole = new Entity("role", new Guid("CAD52A75-568C-E611-80D4-00155D42A122"));
            testRole["Name"] = "Test Role";
            testRole["BusinessUnitId"] = new EntityReference("businessunit", businessId);

            fakedContext.Initialize(new Entity[] { testUser, testRole });

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

            fakedContext.AddRelationship("systemuserroles_association", new XrmFakedRelationship
            {
                IntersectEntity = "systemuserroles",
                Entity1LogicalName = "systemuser",
                Entity1Attribute = "systemuserid",
                Entity2LogicalName = "role",
                Entity2Attribute = "roleid"
            });

            var request = new AssociateRequest()
            {
                Target = testUser.ToEntityReference(),
                RelatedEntities = new EntityReferenceCollection()
                {
                    new EntityReference("role", testRole.Id),
                },
                Relationship = new Relationship("systemuserroles_association")
            };

            IOrganizationService fakedService = fakedContext.GetOrganizationService();
            fakedService.Execute(request);


            DataBuilder DataBuilder = new DataBuilder(fakedService);
            DataBuilder.AppendData("<fetch><entity name='systemuser'><attribute name='systemuserid'/><link-entity name='systemuserroles' from='systemuserid' to='systemuserid' intersect='true'><link-entity name='role' from='roleid' to='roleid'/><attribute name='roleid'/></link-entity><filter><condition attribute='systemuserid' operator='eq' value='00e7b0b9-1ace-e711-a970-000d3a192311'/></filter></entity></fetch>");

            var schemaXml = DataBuilder.BuildSchemaXML();
            var dataXml = DataBuilder.BuildDataXML();

            Assert.AreEqual(
                schemaXml.InnerXml,
                "<entities><entity name=\"systemuser\" displayname=\"User\" etc=\"8\" primaryidfield=\"systemuserid\" disableplugins=\"false\"><fields /><relationships><relationship isreflexive=\"False\" m2mTargetEntity=\"role\" m2mTargetEntityPrimaryKey=\"roleid\" manyToMany=\"true\" name=\"systemuserroles\" relatedEntityName=\"systemuserroles\" /></relationships></entity></entities>");

            Assert.AreEqual(
                dataXml.InnerXml,
                "<entities xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"><entity name=\"systemuser\" displayname=\"User\"><records><record id=\"00e7b0b9-1ace-e711-a970-000d3a192311\"><field name=\"systemuserid\" value=\"00e7b0b9-1ace-e711-a970-000d3a192311\" /></record></records><m2mrelationships><m2mrelationship m2mrelationshipname=\"systemuserroles\" sourceid=\"00e7b0b9-1ace-e711-a970-000d3a192311\" targetentityname=\"role\" targetentitynameidfield=\"roleid\"><targetids><targetid>cad52a75-568c-e611-80d4-00155d42a122</targetid></targetids></m2mrelationship></m2mrelationships></entity></entities>");
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