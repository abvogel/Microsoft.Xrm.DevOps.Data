using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;

namespace Microsoft.Xrm.DevOps.Data
{
    public class DataBuilder
    {
        private Dictionary<String, OrganizationResponse> _OrganizationResponses { get; set; }
        private Dictionary<String, EntityMetadata> _EntityMetadata { get; set; }

        public DataBuilder()
        {
            _OrganizationResponses = new Dictionary<String, OrganizationResponse>();
            _EntityMetadata = new Dictionary<String, EntityMetadata>();
        }

        public void AddOrganizationResponse(string LogicalName, OrganizationResponse OrganizationResponse)
        {
            _OrganizationResponses.Add(LogicalName, OrganizationResponse);
        }

        public void AddEntityMetadata(EntityMetadata EntityMetadata)
        {
            _EntityMetadata.Add(EntityMetadata.LogicalName, EntityMetadata);
        }

        public String[] ToList()
        {
            List<String> Responses = new List<string>();

            foreach (var _OrganizationResponse in _OrganizationResponses)
            {
                if (!_EntityMetadata.ContainsKey(_OrganizationResponse.Key))
                {
                    throw new Exception("Metadata missing from DataBuilder");
                }
            }

            return Responses.ToArray();
        }

        public override String ToString()
        {
            StringBuilder Response = new StringBuilder();
            Response.Append(ToList());
            return Response.ToString();
        }
    }
}
