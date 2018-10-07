using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HairSalon.Models;
namespace HairSalon.Tests
{
    [TestClass]
    public class ClientTests : IDisposable
    {
        public ClientTests()
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
            int count = Client.GetAllClient().Count;
            Assert.AreEqual(0, count);
        }
        [TestMethod]
        public void Equals_TrueForSameName_Client()
        {
            Client ClientOne = new Client("BillyBob", 0);
            Client ClientTwo = new Client("BillyBob", 0);
            Assert.AreEqual(ClientOne, ClientTwo);
        }
        [TestMethod]
        public void Save_PatientsSaveToDatabase_PatientsList()
        {
            Client testClient = new Client("BillyBob", 0);
            testClient.Save();
            List<Client> result = Client.GetAllClient();
            List<Client> testlist = new List<Client> { testClient };
            CollectionAssert.AreEqual(testlist, result);
        }
        [TestMethod]
        public void Save_AssignsIdToObject_id()
        {
            Client testClient = new Client("BillyBob", 0);
            testClient.Save();
            Client savedClient = Client.GetAllClient()[0];
            int result = savedClient.GetClientId();
            int testId = testClient.GetClientId(); 
            Assert.AreEqual(testId, result);
        }
        [TestMethod]
        public void Find_FindsClientInDatabase_Client()
        {
            Client testClient = new Client("BillyBob", 0);
            testClient.Save();
            Client result = Client.Find(testClient.GetClientId());
            Assert.AreEqual(testClient, result);
        }
    }
}