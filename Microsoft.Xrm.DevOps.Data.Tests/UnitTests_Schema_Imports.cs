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
    public class SchemaImports
    {
        [TestMethod]
        public void BooleanTypeData()
        {
            DataBuilder db = new DataBuilder();
            db.AppendData(SupportMethods.GetBooleanTypeExpectedData(), SupportMethods.GetBooleanTypeExpectedSchema());

            Assert.AreEqual(
                db.BuildSchemaXML().InnerXml,
                SupportMethods.GetBooleanTypeExpectedSchema());
        }

        [TestMethod]
        public void CustomerType()
        {
            DataBuilder db = new DataBuilder();
            db.AppendData(SupportMethods.GetCustomerTypeExpectedData(), SupportMethods.GetCustomerTypeExpectedSchema());

            Assert.AreEqual(
                db.BuildSchemaXML().InnerXml,
                SupportMethods.GetCustomerTypeExpectedSchema());
        }

        [TestMethod]
        public void DateTimeType()
        {
            DataBuilder db = new DataBuilder();
            db.AppendData(SupportMethods.GetDateTimeTypeExpectedData(), SupportMethods.GetDateTimeTypeExpectedSchema());

            Assert.AreEqual(
                db.BuildSchemaXML().InnerXml,
                SupportMethods.GetDateTimeTypeExpectedSchema());
        }

        [TestMethod]
        public void DecimalType()
        {
            DataBuilder db = new DataBuilder();
            db.AppendData(SupportMethods.GetDecimalTypeExpectedData(), SupportMethods.GetDecimalTypeExpectedSchema());

            Assert.AreEqual(
                db.BuildSchemaXML().InnerXml,
                SupportMethods.GetDecimalTypeExpectedSchema());
        }

        [TestMethod]
        public void DoubleType()
        {
            DataBuilder db = new DataBuilder();
            db.AppendData(SupportMethods.GetDoubleTypeExpectedData(), SupportMethods.GetDoubleTypeExpectedSchema());

            Assert.AreEqual(
                db.BuildSchemaXML().InnerXml,
                SupportMethods.GetDoubleTypeExpectedSchema());
        }

        [TestMethod]
        public void IntegerType()
        {
            DataBuilder db = new DataBuilder();
            db.AppendData(SupportMethods.GetIntegerTypeExpectedData(), SupportMethods.GetIntegerTypeExpectedSchema());

            Assert.AreEqual(
                db.BuildSchemaXML().InnerXml,
                SupportMethods.GetIntegerTypeExpectedSchema());
        }

        [TestMethod]
        public void LookupType()
        {
            DataBuilder db = new DataBuilder();
            db.AppendData(SupportMethods.GetLookupTypeExpectedData(), SupportMethods.GetLookupTypeExpectedSchema());

            Assert.AreEqual(
                db.BuildSchemaXML().InnerXml,
                SupportMethods.GetLookupTypeExpectedSchema());
        }

        [TestMethod]
        public void MemoType()
        {
            DataBuilder db = new DataBuilder();
            db.AppendData(SupportMethods.GetMemoTypeExpectedData(), SupportMethods.GetMemoTypeExpectedSchema());

            Assert.AreEqual(
                db.BuildSchemaXML().InnerXml,
                SupportMethods.GetMemoTypeExpectedSchema());
        }

        [TestMethod]
        public void MoneyType()
        {
            DataBuilder db = new DataBuilder();
            db.AppendData(SupportMethods.GetMoneyTypeExpectedData(), SupportMethods.GetMoneyTypeExpectedSchema());

            Assert.AreEqual(
                db.BuildSchemaXML().InnerXml,
                SupportMethods.GetMoneyTypeExpectedSchema());
        }

        [TestMethod]
        public void OwnerType()
        {
            DataBuilder db = new DataBuilder();
            db.AppendData(SupportMethods.GetOwnerTypeExpectedData(), SupportMethods.GetOwnerTypeExpectedSchema());

            Assert.AreEqual(
                db.BuildSchemaXML().InnerXml,
                SupportMethods.GetOwnerTypeExpectedSchema());
        }

        [TestMethod]
        public void PartyListType()
        {
            DataBuilder db = new DataBuilder();
            db.AppendData(SupportMethods.GetPartyListTypeExpectedData(), SupportMethods.GetPartyListTypeExpectedSchema());

            Assert.AreEqual(
                db.BuildSchemaXML().InnerXml,
                SupportMethods.GetPartyListTypeExpectedSchema());
        }

        [TestMethod]
        public void PicklistType()
        {
            DataBuilder db = new DataBuilder();
            db.AppendData(SupportMethods.GetPicklistTypeExpectedData(), SupportMethods.GetPicklistTypeExpectedSchema());

            Assert.AreEqual(
                db.BuildSchemaXML().InnerXml,
                SupportMethods.GetPicklistTypeExpectedSchema());
        }

        [TestMethod]
        public void StateType()
        {
            DataBuilder db = new DataBuilder();
            db.AppendData(SupportMethods.GetStateTypeExpectedData(), SupportMethods.GetStateTypeExpectedSchema());

            Assert.AreEqual(
                db.BuildSchemaXML().InnerXml,
                SupportMethods.GetStateTypeExpectedSchema());
        }

        [TestMethod]
        public void StatusType()
        {
            DataBuilder db = new DataBuilder();
            db.AppendData(SupportMethods.GetStatusTypeExpectedData(), SupportMethods.GetStatusTypeExpectedSchema());

            Assert.AreEqual(
                db.BuildSchemaXML().InnerXml,
                SupportMethods.GetStatusTypeExpectedSchema());
        }

        [TestMethod]
        public void StringType()
        {
            DataBuilder db = new DataBuilder();
            db.AppendData(SupportMethods.GetStringTypeExpectedData(), SupportMethods.GetStringTypeExpectedSchema());

            Assert.AreEqual(
                db.BuildSchemaXML().InnerXml,
                SupportMethods.GetStringTypeExpectedSchema());
        }

        [TestMethod]
        public void UniqueidentifierType()
        {
            DataBuilder db = new DataBuilder();
            db.AppendData(SupportMethods.GetUniqueIdentifierTypeExpectedData(), SupportMethods.GetUniqueIdentifierTypeExpectedSchema());

            Assert.AreEqual(
                db.BuildSchemaXML().InnerXml,
                SupportMethods.GetUniqueIdentifierTypeExpectedSchema());
        }

        [TestMethod]
        public void m2mRelationshipType()
        {
            DataBuilder db = new DataBuilder();
            db.AppendData(SupportMethods.Getm2mRelationshipTypeExpectedData(), SupportMethods.Getm2mRelationshipTypeExpectedSchema());

            Assert.AreEqual(
                db.BuildSchemaXML().InnerXml,
                SupportMethods.Getm2mRelationshipTypeExpectedSchema());
        }
    }
}
