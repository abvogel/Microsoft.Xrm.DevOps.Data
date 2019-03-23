using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk;
using System;

namespace Microsoft.Xrm.DevOps.Data.Tests
{
    [TestClass]
    public class SpecialLogic
    {
        [TestMethod]
        public void InternalLookup()
        {
            // BooleanType          readyforreview                     knowledgearticle 
            DataBuilder DataBuilder = new DataBuilder();
            Assert.IsTrue(true);
        }
        
        [TestMethod]
        public void ExternalLookup()
        {
            // BooleanType          readyforreview                     knowledgearticle 
            DataBuilder DataBuilder = new DataBuilder();
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void ReflexiveRelationship()
        {
            // BooleanType          readyforreview                     knowledgearticle 
            DataBuilder DataBuilder = new DataBuilder();
            Assert.IsTrue(true);
        }
    }
}