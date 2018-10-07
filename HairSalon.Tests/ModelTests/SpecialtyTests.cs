using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HairSalon.Models;
namespace HairSalon.Tests
{
    [TestClass]
    public class SpecialtyTests : IDisposable
    {
        public SpecialtyTests()
        {
            DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=ahmed_khokar_test;";
        }
        public void Dispose()
        {
            Stylist.DeleteAll();
            Client.DeleteAll();
            Specialty.DeleteAll();
        }
        [TestMethod]
        public void GetAll_DBStartsEmpty_Empty()
        {
            int count = Specialty.GetAll().Count;
            Assert.AreEqual(0, count);
        }
        [TestMethod]
        public void Equals_TrueForSameName_Specialty()
        {
            Specialty SpecialtyOne = new Specialty("Mohawk", 0);
            Specialty SpecialtyTwo = new Specialty("Mohawk", 0);
            Assert.AreEqual(SpecialtyOne, SpecialtyTwo);
        }
        [TestMethod]
        public void Save_PatientsSaveToDatabase_PatientsList()
        {
            Specialty testSpecialty = new Specialty("Mohawk", 0);
            testSpecialty.Save();         
            List<Specialty> result = Specialty.GetAll();
            List<Specialty> testlist = new List<Specialty> { testSpecialty };
            CollectionAssert.AreEqual(testlist, result);
        }
        [TestMethod]
        public void Save_AssignsIdToObject_id()
        {
            Specialty testSpecialty = new Specialty("Mohawk", 0);
            testSpecialty.Save();        
            Specialty savedSpecialty = Specialty.GetAll()[0];
            int result = savedSpecialty.GetId();
            int testId = testSpecialty.GetId();
            Assert.AreEqual(testId, result);
        }
        [TestMethod]
        public void Find_FindsSpecialtyInDatabase_Specialty()
        {
            Specialty testSpecialty = new Specialty("Mohawk", 0);
            testSpecialty.Save();         
            Specialty result = Specialty.Find(testSpecialty.GetId());
            Assert.AreEqual(testSpecialty, result);
        }
    }
}