using FakeXrmEasy;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using System;
using System.Globalization;
using System.Xml;

namespace Microsoft.Xrm.DevOps.Data.Tests
{
    public partial class SupportMethods
    {
        public const String AccountLogicalName = "account";
        public const String AccountDisplayName = "Account";
        public const String ActivityPartyLogicalName = "activityparty";
        public const String ActivityPartyDisplayName = "Activity Party";
        public const String AgentScriptActionLogicalName = "msdyusd_agentscriptaction";
        public const String AgentScriptActionDisplayName = "Action Call";
        public const String ContactLogicalName = "contact";
        public const String ContactDisplayName = "Contact";
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
        public const String PurchaseOrderProductLogicalName = "msdyn_purchaseorderproduct";
        public const String PurchaseOrderProductDisplayName = "Purchase Order Product";
        public const String ContractLogicalName = "contract";
        public const String ContractDisplayName = "Contract";
        public const String ApprovalLogicalName = "msdyn_approval";
        public const String ApprovalDisplayName = "Approval";
        public const String UserLogicalName = "systemuser";
        public const String UserDisplayName = "User";
        public const String SecurityRoleLogicalName = "role";
        public const String SecurityRoleDisplayName = "Security Role";

        public static XrmFakedContext SetupPrimitiveFakedService(string LogicalName, string DisplayName, Entity Entity)
        {
            var fakedContext = new XrmFakedContext();
            fakedContext.InitializeMetadata(typeof(CrmEarlyBound.CrmServiceContext).Assembly);
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
            return fakedContext;
        }

        public static XrmFakedContext SetupPrimitiveFakedService(string LogicalName, string DisplayName, Entity[] Entity)
        {
            var fakedContext = new XrmFakedContext();
            fakedContext.InitializeMetadata(typeof(CrmEarlyBound.CrmServiceContext).Assembly);
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
            return fakedContext;
        }

        public static String LoadXmlFile(string Path) {
            XmlDocument doc = new XmlDocument();
            doc.Load(Path);

            XmlNode timestampNode = doc.SelectSingleNode("/*[@timestamp]");
            if (timestampNode != null)
                doc.FirstChild.Attributes.RemoveNamedItem("timestamp");

            return doc.InnerXml;
        }

        /// <summary>
        /// Test data contains decimal numbers separated with either dot or comma as when data is being exported the delimiter will depend on cultre.
        /// </summary>
        public enum Separator { Dot, Comma }

        /// <summary>
        /// If separator value not set and current culrute number decimal separator is comma, use comm. Or when Separator is set to Comma.
        /// </summary>
        /// <param name="separator"></param>
        /// <returns></returns>
        static bool UseCommaSeparatedData(Separator? separator) => separator == Separator.Comma || (separator == null && CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator == ",");
    }
}

namespace FakeXrmEasy.Extensions
{
    public static class EntityMetadataExtensions
    {
        public static void SetSealedPropertyValue(this Microsoft.Xrm.Sdk.Metadata.ManyToManyRelationshipMetadata manyToManyRelationshipMetadata, string sPropertyName, object value)
        {
            manyToManyRelationshipMetadata.GetType().GetProperty(sPropertyName).SetValue(manyToManyRelationshipMetadata, value, null);
        }
    }
}