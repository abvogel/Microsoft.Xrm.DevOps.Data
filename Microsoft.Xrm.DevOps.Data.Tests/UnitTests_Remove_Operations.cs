using FakeXrmEasy;
using FakeXrmEasy.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using System;
using System.Linq;
using System.Reflection;

namespace Microsoft.Xrm.DevOps.Data.Tests
{
    [TestClass]
    public class RemoveOperations : TestBase
    {
        [TestMethod]
        public void RemoveEntityWithSingleField_RemovesField()
        {
            DataBuilder db = new DataBuilder();
            db.AppendData(SupportMethods.GetIntegerTypeExpectedData(), SupportMethods.GetIntegerTypeExpectedSchema());
            db.AppendData(SupportMethods.GetLookupTypeExpectedData(), SupportMethods.GetLookupTypeExpectedSchema());

            db.RemoveData(SupportMethods.GetLookupTypeEntity());

            Assert.AreEqual(
                db.BuildDataXML().InnerXml,
                SupportMethods.GetIntegerTypeExpectedData());
        }
        
        [TestMethod]
        public void RemoveEntityWithNonExistentField_RemovesNothing()
        {
            DataBuilder db = new DataBuilder();
            db.AppendData(SupportMethods.GetIntegerTypeExpectedData(), SupportMethods.GetIntegerTypeExpectedSchema());

            db.RemoveData(SupportMethods.GetLookupTypeEntity());

            Assert.AreEqual(
                db.BuildDataXML().InnerXml,
                SupportMethods.GetIntegerTypeExpectedData());
        }

        [TestMethod]
        public void RemoveDataXMLWithNonExistentField_RemovesNothing()
        {
            DataBuilder db = new DataBuilder();
            db.AppendData(SupportMethods.GetIntegerTypeExpectedData(), SupportMethods.GetIntegerTypeExpectedSchema());

            db.RemoveData(SupportMethods.GetLookupTypeExpectedData(), SupportMethods.GetLookupTypeExpectedSchema());

            Assert.AreEqual(
                db.BuildDataXML().InnerXml,
                SupportMethods.GetIntegerTypeExpectedData());
        }

        [TestMethod]
        public void RemoveDataXMLWithPartialM2MRelationship_RemovesPartOfRelationship()
        {
            DataBuilder db = new DataBuilder();
            db.AppendData(SupportMethods.Getm2mRelationshipTypeExpectedData(), SupportMethods.Getm2mRelationshipTypeExpectedSchema());
            db.AppendData(SupportMethods.Getm2mRelationshipTypeExpectedData2(), SupportMethods.Getm2mRelationshipTypeExpectedSchema());

            db.RemoveData(SupportMethods.Getm2mRelationshipTypeExpectedData2(), SupportMethods.Getm2mRelationshipTypeExpectedSchema());

            Assert.AreEqual(
                db.BuildDataXML().InnerXml,
                SupportMethods.Getm2mRelationshipTypeExpectedData());
        }

        [TestMethod]
        public void RemoveDataXMLWithM2MRelationship_RemovesEntireRelationship()
        {
            DataBuilder db = new DataBuilder();
            db.AppendData(SupportMethods.GetIntegerTypeExpectedData(), SupportMethods.GetIntegerTypeExpectedSchema());
            db.AppendData(SupportMethods.Getm2mRelationshipTypeExpectedData(), SupportMethods.Getm2mRelationshipTypeExpectedSchema());
            db.AppendData(SupportMethods.Getm2mRelationshipTypeExpectedData2(), SupportMethods.Getm2mRelationshipTypeExpectedSchema());

            db.RemoveData(SupportMethods.Getm2mRelationshipTypeExpectedData2(), SupportMethods.Getm2mRelationshipTypeExpectedSchema());
            db.RemoveData(SupportMethods.Getm2mRelationshipTypeExpectedData(), SupportMethods.Getm2mRelationshipTypeExpectedSchema());

            Assert.AreEqual(
                db.BuildDataXML().InnerXml,
                SupportMethods.GetIntegerTypeExpectedData());
        }

        [TestMethod]
        public void BooleanRemove()
        {
            DataBuilder db = new DataBuilder();
            db.AppendData(SupportMethods.GetIntegerTypeExpectedData(), SupportMethods.GetIntegerTypeExpectedSchema());
            db.AppendData(SupportMethods.GetBooleanTypeExpectedData(), SupportMethods.GetBooleanTypeExpectedSchema());

            db.RemoveData(SupportMethods.GetBooleanTypeExpectedData(), SupportMethods.GetBooleanTypeExpectedSchema());

            Assert.AreEqual(
                db.BuildDataXML().InnerXml,
                SupportMethods.GetIntegerTypeExpectedData());
        }

        [TestMethod]
        public void CustomerRemove()
        {
            DataBuilder db = new DataBuilder();
            db.AppendData(SupportMethods.GetIntegerTypeExpectedData(), SupportMethods.GetIntegerTypeExpectedSchema());
            db.AppendData(SupportMethods.GetCustomerTypeExpectedData(), SupportMethods.GetCustomerTypeExpectedSchema());

            db.RemoveData(SupportMethods.GetCustomerTypeExpectedData(), SupportMethods.GetCustomerTypeExpectedSchema());

            Assert.AreEqual(
                db.BuildDataXML().InnerXml,
                SupportMethods.GetIntegerTypeExpectedData());
        }

        [TestMethod]
        public void DateTimeRemove()
        {
            DataBuilder db = new DataBuilder();
            db.AppendData(SupportMethods.GetIntegerTypeExpectedData(), SupportMethods.GetIntegerTypeExpectedSchema());
            db.AppendData(SupportMethods.GetDateTimeTypeExpectedData(), SupportMethods.GetDateTimeTypeExpectedSchema());

            db.RemoveData(SupportMethods.GetDateTimeTypeExpectedData(), SupportMethods.GetDateTimeTypeExpectedSchema());

            Assert.AreEqual(
                db.BuildDataXML().InnerXml,
                SupportMethods.GetIntegerTypeExpectedData());
        }

        [TestMethod]
        public void DecimalRemove()
        {
            DataBuilder db = new DataBuilder();
            db.AppendData(SupportMethods.GetIntegerTypeExpectedData(), SupportMethods.GetIntegerTypeExpectedSchema());
            db.AppendData(SupportMethods.GetDecimalTypeExpectedData(), SupportMethods.GetDecimalTypeExpectedSchema());

            db.RemoveData(SupportMethods.GetDecimalTypeExpectedData(), SupportMethods.GetDecimalTypeExpectedSchema());

            Assert.AreEqual(
                db.BuildDataXML().InnerXml,
                SupportMethods.GetIntegerTypeExpectedData());
        }

        [TestMethod]
        public void DoubleRemove()
        {
            DataBuilder db = new DataBuilder();
            db.AppendData(SupportMethods.GetIntegerTypeExpectedData(), SupportMethods.GetIntegerTypeExpectedSchema());
            db.AppendData(SupportMethods.GetDoubleTypeExpectedData(), SupportMethods.GetDoubleTypeExpectedSchema());

            db.RemoveData(SupportMethods.GetDoubleTypeExpectedData(), SupportMethods.GetDoubleTypeExpectedSchema());

            Assert.AreEqual(
                db.BuildDataXML().InnerXml,
                SupportMethods.GetIntegerTypeExpectedData());
        }

        [TestMethod]
        public void IntegerRemove()
        {
            DataBuilder db = new DataBuilder();
            db.AppendData(SupportMethods.GetIntegerTypeExpectedData(), SupportMethods.GetIntegerTypeExpectedSchema());
            db.AppendData(SupportMethods.GetDoubleTypeExpectedData(), SupportMethods.GetDoubleTypeExpectedSchema());

            db.RemoveData(SupportMethods.GetIntegerTypeExpectedData(), SupportMethods.GetIntegerTypeExpectedSchema());

            Assert.AreEqual(
                db.BuildDataXML().InnerXml,
                SupportMethods.GetDoubleTypeExpectedData());
        }

        [TestMethod]
        public void LookupRemove()
        {
            DataBuilder db = new DataBuilder();
            db.AppendData(SupportMethods.GetIntegerTypeExpectedData(), SupportMethods.GetIntegerTypeExpectedSchema());
            db.AppendData(SupportMethods.GetLookupTypeExpectedData(), SupportMethods.GetLookupTypeExpectedSchema());

            db.RemoveData(SupportMethods.GetLookupTypeExpectedData(), SupportMethods.GetLookupTypeExpectedSchema());

            Assert.AreEqual(
                db.BuildDataXML().InnerXml,
                SupportMethods.GetIntegerTypeExpectedData());
        }

        [TestMethod]
        public void MemoRemove()
        {
            DataBuilder db = new DataBuilder();
            db.AppendData(SupportMethods.GetIntegerTypeExpectedData(), SupportMethods.GetIntegerTypeExpectedSchema());
            db.AppendData(SupportMethods.GetMemoTypeExpectedData(), SupportMethods.GetMemoTypeExpectedSchema());

            db.RemoveData(SupportMethods.GetMemoTypeExpectedData(), SupportMethods.GetMemoTypeExpectedSchema());

            Assert.AreEqual(
                db.BuildDataXML().InnerXml,
                SupportMethods.GetIntegerTypeExpectedData());
        }

        [TestMethod]
        public void MoneyRemove()
        {
            DataBuilder db = new DataBuilder();
            db.AppendData(SupportMethods.GetIntegerTypeExpectedData(), SupportMethods.GetIntegerTypeExpectedSchema());
            db.AppendData(SupportMethods.GetMoneyTypeExpectedData(), SupportMethods.GetMoneyTypeExpectedSchema());

            db.RemoveData(SupportMethods.GetMoneyTypeExpectedData(), SupportMethods.GetMoneyTypeExpectedSchema());

            Assert.AreEqual(
                db.BuildDataXML().InnerXml,
                SupportMethods.GetIntegerTypeExpectedData());
        }

        [TestMethod]
        public void OwnerRemove()
        {
            DataBuilder db = new DataBuilder();
            db.AppendData(SupportMethods.GetIntegerTypeExpectedData(), SupportMethods.GetIntegerTypeExpectedSchema());
            db.AppendData(SupportMethods.GetOwnerTypeExpectedData(), SupportMethods.GetOwnerTypeExpectedSchema());

            db.RemoveData(SupportMethods.GetOwnerTypeExpectedData(), SupportMethods.GetOwnerTypeExpectedSchema());

            Assert.AreEqual(
                db.BuildDataXML().InnerXml,
                SupportMethods.GetIntegerTypeExpectedData());
        }

        [TestMethod]
        public void PartyListRemove()
        {
            DataBuilder db = new DataBuilder();
            db.AppendData(SupportMethods.GetIntegerTypeExpectedData(), SupportMethods.GetIntegerTypeExpectedSchema());
            db.AppendData(SupportMethods.GetPartyListTypeExpectedData(), SupportMethods.GetPartyListTypeExpectedSchema());

            db.RemoveData(SupportMethods.GetPartyListTypeExpectedData(), SupportMethods.GetPartyListTypeExpectedSchema());

            Assert.AreEqual(
                db.BuildDataXML().InnerXml,
                SupportMethods.GetIntegerTypeExpectedData());
        }

        [TestMethod]
        public void PicklistRemove()
        {
            DataBuilder db = new DataBuilder();
            db.AppendData(SupportMethods.GetIntegerTypeExpectedData(), SupportMethods.GetIntegerTypeExpectedSchema());
            db.AppendData(SupportMethods.GetPicklistTypeExpectedData(), SupportMethods.GetPicklistTypeExpectedSchema());

            db.RemoveData(SupportMethods.GetPicklistTypeExpectedData(), SupportMethods.GetPicklistTypeExpectedSchema());

            Assert.AreEqual(
                db.BuildDataXML().InnerXml,
                SupportMethods.GetIntegerTypeExpectedData());
        }

        [TestMethod]
        public void StateRemove()
        {
            DataBuilder db = new DataBuilder();
            db.AppendData(SupportMethods.GetIntegerTypeExpectedData(), SupportMethods.GetIntegerTypeExpectedSchema());
            db.AppendData(SupportMethods.GetStateTypeExpectedData(), SupportMethods.GetStateTypeExpectedSchema());

            db.RemoveData(SupportMethods.GetStateTypeExpectedData(), SupportMethods.GetStateTypeExpectedSchema());

            Assert.AreEqual(
                db.BuildDataXML().InnerXml,
                SupportMethods.GetIntegerTypeExpectedData());
        }

        [TestMethod]
        public void StatusRemove()
        {
            DataBuilder db = new DataBuilder();
            db.AppendData(SupportMethods.GetIntegerTypeExpectedData(), SupportMethods.GetIntegerTypeExpectedSchema());
            db.AppendData(SupportMethods.GetStatusTypeExpectedData(), SupportMethods.GetStatusTypeExpectedSchema());

            db.RemoveData(SupportMethods.GetStatusTypeExpectedData(), SupportMethods.GetStatusTypeExpectedSchema());

            Assert.AreEqual(
                db.BuildDataXML().InnerXml,
                SupportMethods.GetIntegerTypeExpectedData());
        }

        [TestMethod]
        public void StringRemove()
        {
            DataBuilder db = new DataBuilder();
            db.AppendData(SupportMethods.GetIntegerTypeExpectedData(), SupportMethods.GetIntegerTypeExpectedSchema());
            db.AppendData(SupportMethods.GetStringTypeExpectedData(), SupportMethods.GetStringTypeExpectedSchema());

            db.RemoveData(SupportMethods.GetStringTypeExpectedData(), SupportMethods.GetStringTypeExpectedSchema());

            Assert.AreEqual(
                db.BuildDataXML().InnerXml,
                SupportMethods.GetIntegerTypeExpectedData());
        }

        [TestMethod]
        public void UniqueIdentifierRemove()
        {
            DataBuilder db = new DataBuilder();
            db.AppendData(SupportMethods.GetIntegerTypeExpectedData(), SupportMethods.GetIntegerTypeExpectedSchema());
            db.AppendData(SupportMethods.GetUniqueIdentifierTypeExpectedData(), SupportMethods.GetUniqueIdentifierTypeExpectedSchema());

            db.RemoveData(SupportMethods.GetUniqueIdentifierTypeExpectedData(), SupportMethods.GetUniqueIdentifierTypeExpectedSchema());

            Assert.AreEqual(
                db.BuildDataXML().InnerXml,
                SupportMethods.GetIntegerTypeExpectedData());
        }
    }
}