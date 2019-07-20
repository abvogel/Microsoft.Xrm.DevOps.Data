using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using System;
using System.Collections.Generic;

namespace Microsoft.Xrm.DevOps.Data.Tests
{
    public partial class SupportMethods
    {
        public static Entity[] Getm2mReflexiveRelationshipTypeEntities()
        {
            Entity[] entities = new Entity[]
            {
                new Entity("msdyusd_agentscriptaction")
                {
                    Id = Guid.Parse("1f422d2e-4a2f-e711-8113-5065f38a0a21")
                },
                new Entity("msdyusd_agentscriptaction")
                {
                    Id = Guid.Parse("c710eaf1-7981-e911-a97a-000d3a1d2344")
                },
                new Entity("msdyusd_agentscriptaction")
                {
                    Id = Guid.Parse("c9b0d574-7a81-e911-a97a-000d3a1d2344")
                },
                new Entity("msdyusd_agentscriptaction")
                {
                    Id = Guid.Parse("ac631683-7b81-e911-a97a-000d3a1d2344")
                },
            };

            return entities;
        }

        public static AssociateRequest[] Getm2mReflexiveRelationshipTypeAssociateRequests()
        {
            Guid sourceId1 = Guid.Parse("1f422d2e-4a2f-e711-8113-5065f38a0a21");
            AssociateRequest request1 = new AssociateRequest()
            {
                Target = new EntityReference("msdyusd_agentscriptaction", sourceId1),
                RelatedEntities = new EntityReferenceCollection()
                {
                    new EntityReference("msdyusd_agentscriptaction", Guid.Parse("c710eaf1-7981-e911-a97a-000d3a1d2344")),
                    new EntityReference("msdyusd_agentscriptaction", Guid.Parse("c9b0d574-7a81-e911-a97a-000d3a1d2344")),
                    new EntityReference("msdyusd_agentscriptaction", Guid.Parse("ac631683-7b81-e911-a97a-000d3a1d2344"))
                },
                Relationship = new Relationship("msdyusd_subactioncalls")
            };

            Guid sourceId2 = Guid.Parse("ac631683-7b81-e911-a97a-000d3a1d2344");
            AssociateRequest request2 = new AssociateRequest()
            {
                Target = new EntityReference("msdyusd_agentscriptaction", sourceId2),
                RelatedEntities = new EntityReferenceCollection()
                {
                    new EntityReference("msdyusd_agentscriptaction", Guid.Parse("c9b0d574-7a81-e911-a97a-000d3a1d2344"))
                },
                Relationship = new Relationship("msdyusd_subactioncalls")
            };

            return new AssociateRequest[] { request1, request2 };
        }

        public static String Getm2mReflexiveRelationshipTypeFetch()
        {
            return @"
                <fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='true' >
                  <entity name='msdyusd_agentscriptaction' >
                    <attribute name='msdyusd_agentscriptactionid' />
                    <link-entity name='msdyusd_subactioncalls' from='msdyusd_agentscriptactionidtwo' to='msdyusd_agentscriptactionid' visible='false' intersect='true' >
                      <link-entity name='msdyusd_agentscriptaction' from='msdyusd_agentscriptactionid' to='msdyusd_agentscriptactionidone' alias='ac' >
                        <attribute name='msdyusd_agentscriptactionid' />
                      </link-entity>
                    </link-entity>
                  </entity>
                </fetch>";
        }

        public static String Getm2mReflexiveRelationshipTypeExpectedData()
        {
            return LoadXmlFile(@"../../lib/PrimitiveTypes/m2mReflexiveRelationshipTypedata.xml");
        }

        public static String Getm2mReflexiveRelationshipTypeExpectedSchema()
        {
            return LoadXmlFile(@"../../lib/PrimitiveTypes/m2mReflexiveRelationshipTypedata_schema.xml");
        }
    }
}