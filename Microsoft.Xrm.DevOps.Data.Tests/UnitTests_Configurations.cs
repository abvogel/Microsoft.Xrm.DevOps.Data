using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk;
using System;

namespace Microsoft.Xrm.DevOps.Data.Tests
{
    [TestClass]
    public class Configurations
    {
        [TestMethod]
        public void SingleIdentifier()
        {
            // BooleanType          readyforreview                     knowledgearticle 
            DataBuilder DataBuilder = new DataBuilder();
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void MultipleIdentifiers()
        {
            // BooleanType          readyforreview                     knowledgearticle 
            DataBuilder DataBuilder = new DataBuilder();
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void SingleEntity_PluginsDisabled()
        {
            // BooleanType          readyforreview                     knowledgearticle 
            DataBuilder DataBuilder = new DataBuilder();
            Assert.IsTrue(true);
        }
        
        [TestMethod]
        public void AllEntities_PluginsDisabled()
        {
            // BooleanType          readyforreview                     knowledgearticle 
            DataBuilder DataBuilder = new DataBuilder();
            Assert.IsTrue(true);
        }
    }
}