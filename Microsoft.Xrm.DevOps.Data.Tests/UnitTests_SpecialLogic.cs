using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk;
using System;

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
        public void InternalLookup()
        {
            // BooleanType          readyforreview                     knowledgearticle 
            throw new NotImplementedException();
        }

        [TestMethod]
        public void ExternalLookup()
        {
            // BooleanType          readyforreview                     knowledgearticle 
            throw new NotImplementedException();
        }

        [TestMethod]
        public void ReflexiveRelationship()
        {
            // BooleanType          readyforreview                     knowledgearticle 
            throw new NotImplementedException();
        }
    }
}