using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HairSalon.Models;

namespace HairSalon.Tests
{
    [TestClass]
    public class StylistTests : IDisposable
    {
        public StylistTests()
        {
            DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=ahmed_khokar_test;";
        }

        public void Dispose()
        {
            Stylist.DeleteAll();
            Client.DeleteAll();
        }

        [TestMethod]
        public void GetAll_DBStartsEmpty_Empty()
        {
            //Arrange
            int count = Stylist.GetAllStylist().Count;

            //Assert
            Assert.AreEqual(0, count);
        }

        [TestMethod]
        public void Equals_TrueForSameName_Stylist()
        {
            //Arrange
            Stylist StylistOne = new Stylist("Ahmed");
            Stylist StylistTwo = new Stylist("Ahmed");

            //Assert
            Assert.AreEqual(StylistOne, StylistTwo);
        }

        [TestMethod]
        public void Save_StylistsSaveToDatabase_StylistList()
        {
            //Arrange
            Stylist testStylist = new Stylist("Ahmed");
            testStylist.Save();

            //Act
            List<Stylist> result = Stylist.GetAllStylist();
            List<Stylist> testlist = new List<Stylist> { testStylist };

            //Assert
            CollectionAssert.AreEqual(testlist, result);
        }

        [TestMethod]
        public void Save_AssignsIdToObject_id()
        {
            //Arrange
            Stylist testStylist = new Stylist("Ahmed");
            testStylist.Save();

            //Act
            Stylist savedStylist = Stylist.GetAllStylist()[0];

            int result = savedStylist.GetStylistId();
            int testId = testStylist.GetStylistId();

            //Assert 
            Assert.AreEqual(testId, result);
        }
        [TestMethod]
        public void Find_FindsStylistInDatabase_Stylist()
        {
            //Arrange
            Stylist testStylist = new Stylist("Ahmed");
            testStylist.Save();

            //Act
            Stylist result = Stylist.Find(testStylist.GetStylistId());

            //Assert

            Assert.AreEqual(testStylist, result);

        }
    }
}