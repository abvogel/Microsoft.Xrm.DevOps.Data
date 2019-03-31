using FakeXrmEasy;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using System;

namespace Microsoft.Xrm.DevOps.Data.Tests
{
    public partial class SupportMethods
    {
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
    }
}