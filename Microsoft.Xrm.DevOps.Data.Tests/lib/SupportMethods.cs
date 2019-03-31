using FakeXrmEasy;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using System;
using System.Xml;

namespace Microsoft.Xrm.DevOps.Data.Tests
{
    public partial class SupportMethods
    {
        public const String ThemeLogicalName = "theme";
        public const String ThemeDisplayName = "Theme";
        public const String IncidentLogicalName = "incident";
        public const String IncidentDisplayName = "Case";
        public const String InvoiceLogicalName = "invoice";
        public const String InvoiceDisplayName = "Invoice";
        public const String KnowledgeArticleLogicalName = "knowledgearticle";
        public const String KnowledgeArticleDisplayName = "Knowledge Article";
        public const String ResourceRequirementDetailLogicalName = "msdyn_resourcerequirementdetail";
        public const String ResourceRequirementDetailDisplayName = "Resource Requirement Detail";
        public const String PurchaseOrderProductLogicalName = "msdyn_purchasorderproduct";
        public const String PurchaseOrderProductDisplayName = "Purchase Order Product";
        public const String ContractLogicalName = "contract";
        public const String ContractDisplayName = "Contract";
        public const String ApprovalLogicalName = "msdyn_approval";
        public const String ApprovalDisplayName = "Approval";

        public static IOrganizationService SetupPrimitiveFakedService(string LogicalName, string DisplayName, Entity Entity)
        {
            var fakedContext = new XrmFakedContext();
            fakedContext.InitializeMetadata(typeof(CrmEarlyBound.CrmServiceContext).Assembly);
            var fakedService = fakedContext.GetOrganizationService();
            fakedContext.Initialize(Entity);
            fakedContext.AddExecutionMock<RetrieveEntityRequest>(req =>
            {
                var entityMetadata = fakedContext.GetEntityMetadataByName(LogicalName);
                entityMetadata.DisplayName = new Label(DisplayName, 1033);
                var response = new RetrieveEntityResponse()
                {
                    Results = new ParameterCollection
                        {
                            { "EntityMetadata", entityMetadata }
                        }
                };
                return response;
            });
            return fakedService;
        }

        public static String LoadXmlFile(string Path) {
            XmlDocument doc = new XmlDocument();
            doc.Load(Path);
            doc.FirstChild.Attributes.RemoveNamedItem("timestamp");
            return doc.InnerXml;
        }
    }
}