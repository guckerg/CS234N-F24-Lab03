using NUnit.Framework;

using MMABooksBusiness;
using MMABooksProps;
using MMABooksDB;

using DBCommand = MySql.Data.MySqlClient.MySqlCommand;
using System.Data;

using System.Collections.Generic;
using System;
using MySql.Data.MySqlClient;

namespace MMABooksTests
{
    [TestFixture]
    public class ProductTests
    {

        [SetUp]
        public void TestResetDatabase()
        {
            ProductDB db = new ProductDB();
            DBCommand command = new DBCommand();
            command.CommandText = "usp_testingResetProductData";
            command.CommandType = CommandType.StoredProcedure;
            db.RunNonQueryProcedure(command);
        }

        [Test]
        public void TestNewProductConstructor()
        {
            // not in Data Store - no code
            Product p = new Product();
            Assert.AreEqual(string.Empty, p.ProductCode);
            Assert.AreEqual(0m, p.UnitPrice);
            Assert.IsTrue(p.IsNew);
            Assert.IsFalse(p.IsValid);
        }


        [Test]
        public void TestRetrieveFromDataStoreContructor()
        {
            //unsure how to write this test

            // retrieves from Data Store
            Product p = new Product();
            Assert.AreEqual("A4CS", p.ProductCode);
            Assert.IsTrue(p.ProductCode.Length > 0);
            Assert.IsFalse(p.IsNew);
            Assert.IsTrue(p.IsValid);
        }

        [Test]
        public void TestSaveToDataStore()
        {
            Product p = new Product();
            p.ProductCode = "ABC1";
            p.Description = "This is a test";
            p.UnitPrice = 1234.56m;
            p.OnHandQuantity = 789;
            
            p.Save(); //save cannot run due to UniPrice and Quantity failing validation
            Product p2 = new Product();
            Assert.AreEqual(p2.ProductCode, p.ProductCode);
            Assert.AreEqual(p2.Description, p.Description);
        }

        [Test]
        public void TestUpdate()
        {
            Product p = new Product();
            p.ProductCode = "A2B1";
            p.Description = "TestUpdateDescription";
            p.UnitPrice = 12.23m;
            p.OnHandQuantity = 700;

            p.Save(); //save cannot run due to unitPrice and Quantity failing validation

            Product p2 = new Product();
            Assert.AreEqual(p2.ProductCode, p.ProductCode);
            Assert.AreEqual(p2.Description, p.Description);
        }

        [Test]
        public void TestDelete()
        {
            Product p = new Product();
            p.Delete();
            p.Save();
            Assert.Throws<Exception>(() => new Product());
        }

        [Test]
        public void TestGetList()
        {
            Product p = new Product(2);
            List<Product> products = (List<Product>)p.GetList();
            Assert.AreEqual(16, products.Count);
            Assert.AreEqual("A4VB", p.ProductCode);
            Assert.AreEqual(56.50, p.UnitPrice);
        }

        [Test]
        public void TestNoRequiredPropertiesNotSet()
        {
            Product p = new Product();
            Assert.Throws<Exception>(() => p.Save());
        }

        [Test]
        public void TestSomeRequiredPropertiesNotSet()
        {
            Product p = new Product();
            Assert.Throws<Exception>(() => p.Save());
            p.ProductCode = "????";
            Assert.Throws<Exception>(() => p.Save());
        }

        [Test]
        public void TestInvalidPropertySet()
        {
            Product p = new Product();
            Assert.Throws<ArgumentOutOfRangeException>(() => p.ProductCode = "01234567891");
        }

        [Test]
        public void TestConcurrencyIssue()
        {
            Product p1 = new Product();
            Product p2 = new Product();

            p1.ProductCode = "Updated first";
            p1.Save();

            p2.ProductCode = "Updated second";
            Assert.Throws<Exception>(() => p2.Save());
        }
    }
}