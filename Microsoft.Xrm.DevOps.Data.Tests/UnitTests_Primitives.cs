using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk;
using System;

namespace Microsoft.Xrm.DevOps.Data.Tests
{
    [TestClass]
    public class Primitives
    {
        [TestMethod]
        public void BooleanType()
        {
            // BooleanType          readyforreview                     knowledgearticle 
            // <fetch distinct='false' no-lock='false' top='50' mapping='logical'><entity name='knowledgearticle'><attribute name='readyforreview'/></entity></fetch>
            DataBuilder DataBuilder = new DataBuilder();
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void CustomerType()
        {
            // CustomerType         customerid                         incident                                  
            DataBuilder DataBuilder = new DataBuilder();
            Assert.IsTrue(true);
        }
        
        [TestMethod]
        public void DateTimeType()
        {
            // DateTimeType         publishon                          knowledgearticle
            DataBuilder DataBuilder = new DataBuilder();
            Assert.IsTrue(true);
        }
        
        [TestMethod]
        public void DecimalType()
        {
            // DecimalType          msdyn_hours                        msdyn_resourcerequirementdetail           
            DataBuilder DataBuilder = new DataBuilder();
            Assert.IsTrue(true);
        }
        
        [TestMethod]
        public void DoubleType()
        {
            // DoubleType           msdyn_quantity                     msdyn_purchaseorderproduct                
            DataBuilder DataBuilder = new DataBuilder();
            Assert.IsTrue(true);
        }
        
        [TestMethod]
        public void ImageType()
        {
            // ImageType            entityimage                        contract
            DataBuilder DataBuilder = new DataBuilder();
            Assert.IsTrue(true);
        }
        
        [TestMethod]
        public void IntegerType()
        {
            // IntegerType          expirationstateid                  knowledgearticle
            DataBuilder DataBuilder = new DataBuilder();
            Assert.IsTrue(true);
        }
        
        [TestMethod]
        public void LookupType()
        {
            // LookupType           previousarticlecontentid           knowledgearticle
            DataBuilder DataBuilder = new DataBuilder();
            Assert.IsTrue(true);
        }
        
        [TestMethod]
        public void MemoType()
        {
            // MemoType             keywords                           knowledgearticle
            DataBuilder DataBuilder = new DataBuilder();
            Assert.IsTrue(true);
        }
        
        [TestMethod]
        public void MoneyType()
        {
            // MoneyType            totaltax                           invoice                                   
            DataBuilder DataBuilder = new DataBuilder();
            Assert.IsTrue(true);
        }
        
        [TestMethod]
        public void OwnerType()
        {
            // OwnerType            ownerid                            knowledgearticle                           
            DataBuilder DataBuilder = new DataBuilder();
            Assert.IsTrue(true);
        }
        
        [TestMethod]
        public void PartyListType()
        {
            // PartyListType        customers                          msdyn_approval
            DataBuilder DataBuilder = new DataBuilder();
            Assert.IsTrue(true);
        }
        
        [TestMethod]
        public void PicklistType()
        {
            // PicklistType         expiredreviewoptions               knowledgearticle
            DataBuilder DataBuilder = new DataBuilder();
            Assert.IsTrue(true);
        }
        
        [TestMethod]
        public void StateType()
        {
            // StateType            statecode                          knowledgearticle  
            DataBuilder DataBuilder = new DataBuilder();
            Assert.IsTrue(true);
        }
        
        [TestMethod]
        public void StatusType()
        {
            // StatusType           statuscode                         knowledgearticle
            DataBuilder DataBuilder = new DataBuilder();
            Assert.IsTrue(true);
        }
        
        [TestMethod]
        public void StringType()
        {
            // StringType           description                        knowledgearticle
            DataBuilder DataBuilder = new DataBuilder();
            Assert.IsTrue(true);
        }
        
        [TestMethod]
        public void UniqueidentifierType()
        {
            // UniqueidentifierType stageid                            knowledgearticle
            DataBuilder DataBuilder = new DataBuilder();
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void m2mRelationshipType()
        {
            // UniqueidentifierType stageid                            knowledgearticle
            DataBuilder DataBuilder = new DataBuilder();
            Assert.IsTrue(true);
        }
    }
}
