using FakeXrmEasy;
using FakeXrmEasy.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using System;
using System.Linq;

namespace Microsoft.Xrm.DevOps.Data.Tests
{
    [TestClass]
    public class Exceptions : TestBase
    {
        [TestMethod]
        public void InvalidAttributeTypeCode_Throws()
        {
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
                entityMetadata.Attributes.First(a => a.LogicalName == "customerid").SetSealedPropertyValue("AttributeType", (Sdk.Metadata.AttributeTypeCode)21);
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

            var ex = Assert.ThrowsException<Exception>(() => DataBuilder.BuildSchemaXML());
            Assert.AreEqual(ex.Message, "GetFieldNodeType: Unknown Field Node Type - incident:customerid - LookupType:21");
        }

        [TestMethod]
        public void UnsupportedTypeCode_SkipsField()
        {
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
                entityMetadata.Attributes.First(a => a.LogicalName == "customerid").SetSealedPropertyValue("AttributeType", Sdk.Metadata.AttributeTypeCode.CalendarRules);
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

            var XML = DataBuilder.BuildDataXML();
            Assert.AreEqual(1, XML.SelectSingleNode("//records/record").ChildNodes.Count);
            Assert.AreEqual("incidentid", XML.SelectSingleNode("//records/record/field/@name").Value);
        }

        [TestMethod]
        public void BrokenConnection_Throws()
        {
            XrmFakedContext fakedContext = SupportMethods.SetupPrimitiveFakedService(
                SupportMethods.IncidentLogicalName,
                SupportMethods.IncidentDisplayName,
                SupportMethods.GetCustomerTypeEntity());

            IOrganizationService fakedService = new Client.Services.OrganizationService(new Client.CrmConnection()); // fakedContext.GetOrganizationService();

            DataBuilder DataBuilder = new DataBuilder(fakedService);
            
            var ex = Assert.ThrowsException<Exception>(() => DataBuilder.AppendData(SupportMethods.GetCustomerTypeFetch()));
        }

        [TestMethod]
        public void MissingMetadata_Throws()
        {
            var fakedContext = new XrmFakedContext();
            fakedContext.InitializeMetadata(typeof(CrmEarlyBound.CrmServiceContext).Assembly);
            fakedContext.Initialize(SupportMethods.GetCustomerTypeEntity());
            fakedContext.AddExecutionMock<RetrieveEntityRequest>(req =>
            {
                var entityMetadata = fakedContext.GetEntityMetadataByName(String.Empty);
                var response = new RetrieveEntityResponse()
                {
                    Results = new ParameterCollection
                        {
                            { "EntityMetadata", entityMetadata }
                        }
                };
                return response;
            });

            IOrganizationService fakedService = new Client.Services.OrganizationService(new Client.CrmConnection()); // fakedContext.GetOrganizationService();

            DataBuilder DataBuilder = new DataBuilder(fakedService);

            var ex = Assert.ThrowsException<Exception>(() => DataBuilder.AppendData(SupportMethods.GetCustomerTypeFetch()));
        }

        [TestMethod]
        public void NoData_ReturnsValidDataXML()
        {
            XrmFakedContext fakedContext = SupportMethods.SetupPrimitiveFakedService(
                SupportMethods.ThemeLogicalName,
                SupportMethods.ThemeDisplayName,
                SupportMethods.GetBooleanTypeEntity());
            IOrganizationService fakedService = fakedContext.GetOrganizationService();

            DataBuilder DataBuilder = new DataBuilder(fakedService);
            DataBuilder.AppendData("<fetch><entity name='systemuser'></entity></fetch>");

            Assert.AreEqual(
                DataBuilder.BuildDataXML().InnerXml,
                "<entities xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" />");
        }

        [TestMethod]
        public void NoM2MData_ReturnsValidDataXML()
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
                var entityMetadata = fakedContext.GetEntityMetadataByName(SupportMethods.UserLogicalName);

                entityMetadata.DisplayName = new Label(SupportMethods.UserDisplayName, 1033);
                entityMetadata.SetSealedPropertyValue("PrimaryNameAttribute", "fullname");
                entityMetadata.Attributes.First(a => a.LogicalName == "systemuserid").SetSealedPropertyValue("DisplayName", new Label("User", 1033));
                entityMetadata.ManyToManyRelationships.First(m => m.SchemaName == "systemuserroles_association").SetSealedPropertyValue("IntersectEntityName", "systemuserroles");
                entityMetadata.ManyToManyRelationships.First(m => m.SchemaName == "systemuserroles_association").SetSealedPropertyValue("Entity2LogicalName", "role");
                entityMetadata.ManyToManyRelationships.First(m => m.SchemaName == "systemuserroles_association").SetSealedPropertyValue("Entity2IntersectAttribute", "roleid");

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
            DataBuilder.AppendData("<fetch><entity name='systemuser'><attribute name='systemuserid'/><link-entity name='systemuserroles' from='systemuserid' to='systemuserid' intersect='true'><link-entity name='role' from='roleid' to='roleid'/><attribute name='roleid'/></link-entity><filter><condition attribute='systemuserid' operator='eq' value='00e7b0b9-1ace-e711-a970-000d3a192315'/></filter></entity></fetch>");

            Assert.AreEqual(
                DataBuilder.BuildDataXML().InnerXml,
                "<entities xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" />");
        }

        [TestMethod]
        public void NoData_ReturnsValidSchemaXML()
        {
            XrmFakedContext fakedContext = SupportMethods.SetupPrimitiveFakedService(
                SupportMethods.ThemeLogicalName,
                SupportMethods.ThemeDisplayName,
                SupportMethods.GetBooleanTypeEntity());
            IOrganizationService fakedService = fakedContext.GetOrganizationService();

            DataBuilder DataBuilder = new DataBuilder(fakedService);
            DataBuilder.AppendData("<fetch><entity name='systemuser'></entity></fetch>");

            Assert.AreEqual(
                DataBuilder.BuildSchemaXML().InnerXml,
                "<entities />");
        }

        [TestMethod]
        public void NoM2MData_ReturnsValidSchemaXML()
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
                var entityMetadata = fakedContext.GetEntityMetadataByName(SupportMethods.UserLogicalName);

                entityMetadata.DisplayName = new Label(SupportMethods.UserDisplayName, 1033);
                entityMetadata.SetSealedPropertyValue("PrimaryNameAttribute", "fullname");
                entityMetadata.Attributes.First(a => a.LogicalName == "systemuserid").SetSealedPropertyValue("DisplayName", new Label("User", 1033));
                entityMetadata.ManyToManyRelationships.First(m => m.SchemaName == "systemuserroles_association").SetSealedPropertyValue("IntersectEntityName", "systemuserroles");
                entityMetadata.ManyToManyRelationships.First(m => m.SchemaName == "systemuserroles_association").SetSealedPropertyValue("Entity2LogicalName", "role");
                entityMetadata.ManyToManyRelationships.First(m => m.SchemaName == "systemuserroles_association").SetSealedPropertyValue("Entity2IntersectAttribute", "roleid");

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
            DataBuilder.AppendData("<fetch><entity name='systemuser'><attribute name='systemuserid'/><link-entity name='systemuserroles' from='systemuserid' to='systemuserid' intersect='true'><link-entity name='role' from='roleid' to='roleid'/><attribute name='roleid'/></link-entity><filter><condition attribute='systemuserid' operator='eq' value='00e7b0b9-1ace-e711-a970-000d3a192315'/></filter></entity></fetch>");

            Assert.AreEqual(
                DataBuilder.BuildSchemaXML().InnerXml,
                "<entities />");
        }
    }
}
