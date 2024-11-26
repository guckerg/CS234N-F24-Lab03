using NUnit.Framework;

using MMABooksProps;
using MMABooksDB;

using DBCommand = MySql.Data.MySqlClient.MySqlCommand;
using System.Data;

using System.Collections.Generic;
using System;
using MySql.Data.MySqlClient;

namespace MMABooksTests
{
    public class ProductDBTests
    {
        ProductDB db;

        [SetUp]
        public void ResetData()
        {
            db = new ProductDB();
            DBCommand command = new DBCommand();
            command.CommandText = "usp_testingResetData";
            command.CommandType = CommandType.StoredProcedure;
            db.RunNonQueryProcedure(command);
        }

        [Test]
        public void TestRetrieve()
        {
            ProductProps p = (ProductProps)db.Retrieve(1057);
            Assert.AreEqual(1057, p.ProductID);
            Assert.AreEqual("A4CS", p.Description);
        }
        
        [Test]
        public void TestRetrieveAll()
        {
            List<ProductProps> list = (List<ProductProps>)db.RetrieveAll();
            Assert.AreEqual(16, list.Count);
        }
        
        [Test]
        public void TestDelete() //TODO: Fix ProductID keeps changing
        {
            ProductProps p = (ProductProps)db.Retrieve(1153);
            Assert.True(db.Delete(p));
            Assert.Throws<Exception>(() => db.Retrieve(1153));
        }
        
        [Test]
        public void TestUpdate() //TODO: fix argument exception
        {
            ProductProps p = (ProductProps)db.Retrieve(1185);
            p.ProductCode = "XYZ9";
            Assert.True(db.Update(p));
            p = (ProductProps)db.Retrieve(1185);
            Assert.AreEqual("XYZ9", p.ProductCode);
        }
        
        [Test]
        public void TestCreate() //TODO: fix argument exception
        {
            ProductProps p = new ProductProps();
            p.ProductCode = "A2B2";
            p.Description = "Test Description for A2B2";
            p.UnitPrice = 10.20m;
            p.OnHandQuantity = 2000;
            db.Create(p);
            ProductProps p2 = (ProductProps)db.Retrieve(p.ProductID);
            Assert.AreEqual(p.GetState(), p2.GetState());
        }
    }
}
