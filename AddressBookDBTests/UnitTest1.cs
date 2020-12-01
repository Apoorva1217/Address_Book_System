using AddressBookDB;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySql.Data.MySqlClient.Memcached;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;

namespace AddressBookDBTests
{

    [TestClass]
    public class UnitTest1
    {
        RestClient client;

        /// <summary>
        /// Initialize Rest client with localhost:4000
        /// </summary>
        [TestInitialize]
        public void Setup()
        {
            client = new RestClient("http://localhost:4000");
        }

        /// <summary>
        /// Ability to Read entries of AddressBook from JSON Server
        /// </summary>
        [TestMethod]
        public void OnCallingList_ReturnAddressBookList()
        {
            IRestResponse restResponse = GetAddressBookList();

            ///Assert
            Assert.AreEqual(restResponse.StatusCode, System.Net.HttpStatusCode.OK);
            List<AddressBookModel> addressBook = JsonConvert.DeserializeObject<List<AddressBookModel>>(restResponse.Content);
            Assert.AreEqual(3, addressBook.Count);
        }

        /// <summary>
        /// Interface to get List of Details
        /// </summary>
        /// <returns></returns>
        private IRestResponse GetAddressBookList()
        {
            ///Arrange
            RestRequest restRequest = new RestRequest("/addressBook", Method.GET);

            ///Act
            IRestResponse response = client.Execute(restRequest);
            return response;
        }

        [TestMethod]
        public void GivenRetrieveData_ShouldReturnTrue()
        {
            AddressBookRepo addressBookRepo = new AddressBookRepo();
            bool result = addressBookRepo.GetAllDetails();
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GivenFirstNameAndLastName_WhenMatch_ShouldReturnTrue()
        {
            AddressBookRepo addressBookRepo = new AddressBookRepo();
            bool result = addressBookRepo.UpdateContact("Aayush", "Kadam");
            Assert.AreEqual(result,true);
        }

        [TestMethod]
        public void GivenFirstNameAndLastName_WhenNotMatch_ShouldReturnFalse()
        {
            AddressBookRepo addressBookRepo = new AddressBookRepo();
            bool result = addressBookRepo.UpdateContact("Yash", "Rasal");
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GivenStartDateAndEndDate_WhenMatch_ShouldReturnTrue()
        {
            AddressBookRepo addressBookRepo = new AddressBookRepo();
            bool result = addressBookRepo.GetContactsByDateRange("2019-02-03", "2020-10-22");
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GivenStartDateAndEndDate_WhenImproper_ShouldReturnFalse()
        {
            AddressBookRepo addressBookRepo = new AddressBookRepo();
            bool result = addressBookRepo.GetContactsByDateRange("2020-10-16", "2020-05-11");
            Assert.IsFalse(result);
        }
    }
}
