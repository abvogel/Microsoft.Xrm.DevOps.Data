using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk;
using System;

namespace Microsoft.Xrm.DevOps.Data.Tests
{
    [TestClass]
    public class Exceptions
    {
        [TestMethod]
        public void ObjectConstructor_Constructs()
        {
            DataBuilder DataBuilder = new DataBuilder();

            Assert.IsInstanceOfType(DataBuilder, typeof(Microsoft.Xrm.DevOps.Data.DataBuilder));
        }
    }
}
