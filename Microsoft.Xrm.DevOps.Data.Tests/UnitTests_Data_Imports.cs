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
    public class DataImports
    {
        [TestMethod]
        public void BooleanTypeData()
        {
            XrmFakedContext fakedContext = new XrmFakedContext();
            DataBuilder db = new DataBuilder();
            db.AppendData(SupportMethods.GetBooleanTypeExpectedData(), SupportMethods.GetBooleanTypeExpectedSchema());

            Assert.AreEqual(
                db.BuildDataXML().InnerXml,
                SupportMethods.GetBooleanTypeExpectedData());
        }

        [TestMethod]
        public void CustomerType()
        {
            XrmFakedContext fakedContext = new XrmFakedContext();
            DataBuilder db = new DataBuilder();
            db.AppendData(SupportMethods.GetCustomerTypeExpectedData(), SupportMethods.GetCustomerTypeExpectedSchema());

            Assert.AreEqual(
                db.BuildDataXML().InnerXml,
                SupportMethods.GetCustomerTypeExpectedData());
        }

        [TestMethod]
        public void DateTimeType()
        {
            XrmFakedContext fakedContext = new XrmFakedContext();
            DataBuilder db = new DataBuilder();
            db.AppendData(SupportMethods.GetDateTimeTypeExpectedData(), SupportMethods.GetDateTimeTypeExpectedSchema());

            Assert.AreEqual(
                db.BuildDataXML().InnerXml,
                SupportMethods.GetDateTimeTypeExpectedData());
        }

        [TestMethod]
        public void DecimalType()
        {
            XrmFakedContext fakedContext = new XrmFakedContext();
            DataBuilder db = new DataBuilder();
            db.AppendData(SupportMethods.GetDecimalTypeExpectedData(), SupportMethods.GetDecimalTypeExpectedSchema());

            Assert.AreEqual(
                db.BuildDataXML().InnerXml,
                SupportMethods.GetDecimalTypeExpectedData());
        }

        [TestMethod]
        public void DoubleType()
        {
            XrmFakedContext fakedContext = new XrmFakedContext();
            DataBuilder db = new DataBuilder();
            db.AppendData(SupportMethods.GetDoubleTypeExpectedData(), SupportMethods.GetDoubleTypeExpectedSchema());

            Assert.AreEqual(
                db.BuildDataXML().InnerXml,
                SupportMethods.GetDoubleTypeExpectedData());
        }

        [TestMethod]
        public void IntegerType()
        {
            XrmFakedContext fakedContext = new XrmFakedContext();
            DataBuilder db = new DataBuilder();
            db.AppendData(SupportMethods.GetIntegerTypeExpectedData(), SupportMethods.GetIntegerTypeExpectedSchema());

            Assert.AreEqual(
                db.BuildDataXML().InnerXml,
                SupportMethods.GetIntegerTypeExpectedData());
        }

        [TestMethod]
        public void LookupType()
        {
            XrmFakedContext fakedContext = new XrmFakedContext();
            DataBuilder db = new DataBuilder();
            db.AppendData(SupportMethods.GetLookupTypeExpectedData(), SupportMethods.GetLookupTypeExpectedSchema());

            Assert.AreEqual(
                db.BuildDataXML().InnerXml,
                SupportMethods.GetLookupTypeExpectedData());
        }

        [TestMethod]
        public void MemoType()
        {
            XrmFakedContext fakedContext = new XrmFakedContext();
            DataBuilder db = new DataBuilder();
            db.AppendData(SupportMethods.GetMemoTypeExpectedData(), SupportMethods.GetMemoTypeExpectedSchema());

            Assert.AreEqual(
                db.BuildDataXML().InnerXml,
                SupportMethods.GetMemoTypeExpectedData());
        }

        [TestMethod]
        public void MoneyType()
        {
            XrmFakedContext fakedContext = new XrmFakedContext();
            DataBuilder db = new DataBuilder();
            db.AppendData(SupportMethods.GetMoneyTypeExpectedData(), SupportMethods.GetMoneyTypeExpectedSchema());

            Assert.AreEqual(
                db.BuildDataXML().InnerXml,
                SupportMethods.GetMoneyTypeExpectedData());
        }

        [TestMethod]
        public void OwnerType()
        {
            XrmFakedContext fakedContext = new XrmFakedContext();
            DataBuilder db = new DataBuilder();
            db.AppendData(SupportMethods.GetOwnerTypeExpectedData(), SupportMethods.GetOwnerTypeExpectedSchema());

            Assert.AreEqual(
                db.BuildDataXML().InnerXml,
                SupportMethods.GetOwnerTypeExpectedData());
        }

        [TestMethod]
        public void PartyListType()
        {
            XrmFakedContext fakedContext = new XrmFakedContext();
            DataBuilder db = new DataBuilder();
            db.AppendData(SupportMethods.GetPartyListTypeExpectedData(), SupportMethods.GetPartyListTypeExpectedSchema());

            Assert.AreEqual(
                db.BuildDataXML().InnerXml,
                SupportMethods.GetPartyListTypeExpectedData());
        }

        [TestMethod]
        public void PicklistType()
        {
            XrmFakedContext fakedContext = new XrmFakedContext();
            DataBuilder db = new DataBuilder();
            db.AppendData(SupportMethods.GetPicklistTypeExpectedData(), SupportMethods.GetPicklistTypeExpectedSchema());

            Assert.AreEqual(
                db.BuildDataXML().InnerXml,
                SupportMethods.GetPicklistTypeExpectedData());
        }

        [TestMethod]
        public void StateType()
        {
            XrmFakedContext fakedContext = new XrmFakedContext();
            DataBuilder db = new DataBuilder();
            db.AppendData(SupportMethods.GetStateTypeExpectedData(), SupportMethods.GetStateTypeExpectedSchema());

            Assert.AreEqual(
                db.BuildDataXML().InnerXml,
                SupportMethods.GetStateTypeExpectedData());
        }

        [TestMethod]
        public void StatusType()
        {
            XrmFakedContext fakedContext = new XrmFakedContext();
            DataBuilder db = new DataBuilder();
            db.AppendData(SupportMethods.GetStatusTypeExpectedData(), SupportMethods.GetStatusTypeExpectedSchema());

            Assert.AreEqual(
                db.BuildDataXML().InnerXml,
                SupportMethods.GetStatusTypeExpectedData());
        }

        [TestMethod]
        public void StringType()
        {
            XrmFakedContext fakedContext = new XrmFakedContext();
            DataBuilder db = new DataBuilder();
            db.AppendData(SupportMethods.GetStringTypeExpectedData(), SupportMethods.GetStringTypeExpectedSchema());

            Assert.AreEqual(
                db.BuildDataXML().InnerXml,
                SupportMethods.GetStringTypeExpectedData());
        }

        [TestMethod]
        public void UniqueidentifierType()
        {
            XrmFakedContext fakedContext = new XrmFakedContext();
            DataBuilder db = new DataBuilder();
            db.AppendData(SupportMethods.GetUniqueIdentifierTypeExpectedData(), SupportMethods.GetUniqueIdentifierTypeExpectedSchema());

            Assert.AreEqual(
                db.BuildDataXML().InnerXml,
                SupportMethods.GetUniqueIdentifierTypeExpectedData());
        }

        [TestMethod]
        public void m2mRelationshipType()
        {
            XrmFakedContext fakedContext = new XrmFakedContext();
            DataBuilder db = new DataBuilder();
            db.AppendData(SupportMethods.Getm2mRelationshipTypeExpectedData(), SupportMethods.Getm2mRelationshipTypeExpectedSchema());

            Assert.AreEqual(
                db.BuildDataXML().InnerXml,
                SupportMethods.Getm2mRelationshipTypeExpectedData());
        }
    }
}
