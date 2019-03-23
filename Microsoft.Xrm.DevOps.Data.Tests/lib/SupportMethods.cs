using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk;
using System;

namespace Microsoft.Xrm.DevOps.Data.Tests
{
    public class SupportMethods
    {
        public static OrganizationResponse GetOrganizationResponse_Contact()
        {
            ParameterCollection Results = new ParameterCollection();
            Results.Add("exchangerate", 1.0000000000);
            Results.Add("owningbusinessunit", new EntityReference("businessunit", Guid.NewGuid()));
            Results.Add("lastname", "Vogel");
            Results.Add("statuscode", new OptionSetValue(1));
            Results.Add("overriddencreatedon", new DateTime(1513158819000));
            Results.Add("transactioncurrencyid", new EntityReference("transactioncurrency", Guid.NewGuid()));
            Results.Add("parentcustomerid", new EntityReference("account", Guid.NewGuid()));
            Results.Add("contactid", new EntityReference("contact", Guid.NewGuid()));

            OrganizationResponse Response = new OrganizationResponse();
            Response.Results = Results;

            return Response;
        }
    }
}