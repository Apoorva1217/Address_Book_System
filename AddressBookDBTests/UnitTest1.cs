using AddressBookDB;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySql.Data.MySqlClient.Memcached;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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

        /// <summary>
        /// Ability to Add multiple entries to the AddressBook JSON Server
        /// </summary>
        [TestMethod]
        public void GivenMultipleAddressBookEntries_OnPost_ShouldReturnCount()
        {
            List<AddressBookModel> addressBook = new List<AddressBookModel>();
            addressBook.Add(new AddressBookModel { PersonId= 4,First_Name= "Swara",Last_Name= "Kadam",Person_Address= "Aundh",
                City= "Pune",State= "Maharashtra",Zip_Code= "411234",Phone_Number= "9087906709",Email= "Swara98@gmail.com",
                Address_Book_Name="FriendsList",Address_Book_Type= "Friend",Start_Date="2018-03-11",End_Date="2019-07-12" });

            addressBook.Add(new AddressBookModel { PersonId= 5,First_Name= "Manaswi",Last_Name= "Kokare",Person_Address= "Baner",
                City= "Pune",State= "Maharashtra",Zip_Code= "411678",Phone_Number= "9012435421",Email= "Manu23@gmail.com",
                Address_Book_Name="PersonalInfo",Address_Book_Type= "Personal",Start_Date="2018-06-08",End_Date="2020-09-04" });

            addressBook.ForEach(employeeData =>
            {
                ///Arrange
                RestRequest restRequest = new RestRequest("/addressBook", Method.POST);
                JObject jObject = new JObject();
                jObject.Add("PersonId", employeeData.PersonId);
                jObject.Add("First_Name", employeeData.First_Name);
                jObject.Add("Last_Name", employeeData.Last_Name);
                jObject.Add("Person_Address", employeeData.Person_Address);
                jObject.Add("City", employeeData.City);
                jObject.Add("State", employeeData.State);
                jObject.Add("Zip_Code", employeeData.Zip_Code);
                jObject.Add("Phone_Number", employeeData.Phone_Number);
                jObject.Add("Email", employeeData.Email); 
                jObject.Add("Address_Book_Name", employeeData.Address_Book_Name);
                jObject.Add("Address_Book_Type", employeeData.Address_Book_Type);
                jObject.Add("Start_Date", employeeData.Start_Date);
                jObject.Add("End_Date", employeeData.End_Date);

                restRequest.AddParameter("application/json", jObject, ParameterType.RequestBody);

                ///Act
                IRestResponse response = client.Execute(restRequest);

                ///Assert
                Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.Created);
                
            });
            IRestResponse restResponse = GetAddressBookList();

            ///Assert
            Assert.AreEqual(restResponse.StatusCode, System.Net.HttpStatusCode.OK);
            List<AddressBookModel> addressBooklist = JsonConvert.DeserializeObject<List<AddressBookModel>>(restResponse.Content);
            Assert.AreEqual(5, addressBooklist.Count);
        }

        /// <summary>
        /// Ability to Update entry in AddressBook JSON Server
        /// </summary>
        [TestMethod]
        public void GivenEntry_OnUpdate_ShouldReturnUpdatedAddressBook()
        {
            ///Arrange
            RestRequest restRequest = new RestRequest("/addressBook/5", Method.PUT);
            JObject jObject = new JObject();
            jObject.Add("First_Name", "Manaswi");
            jObject.Add("Last_Name", "Kokare");
            jObject.Add("Person_Address","Varoli");
            jObject.Add("City","Mumbai");
            jObject.Add("State","Maharashtra");
            jObject.Add("Zip_Code","402123");
            jObject.Add("Phone_Number","9080709890");
            jObject.Add("Email", "Manu23@gmail.com");
            jObject.Add("Address_Book_Name", "PersonalInfo");
            jObject.Add("Address_Book_Type", "Personal");
            jObject.Add("Start_Date", "2018-06-08");
            jObject.Add("End_Date", "2020-09-04");

            restRequest.AddParameter("application/json", jObject, ParameterType.RequestBody);

            var response = client.Execute(restRequest);

            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.OK);
            AddressBookModel addressBook = JsonConvert.DeserializeObject<AddressBookModel>(response.Content);
            Assert.AreEqual("Manaswi", addressBook.First_Name);
            Assert.AreEqual("Kokare", addressBook.Last_Name);
            Assert.AreEqual("Varoli", addressBook.Person_Address);
            Assert.AreEqual("Mumbai", addressBook.City);
            Assert.AreEqual("Maharashtra", addressBook.State);
            Assert.AreEqual("402123", addressBook.Zip_Code);
            Assert.AreEqual("9080709890", addressBook.Phone_Number);
            Assert.AreEqual("Manu23@gmail.com", addressBook.Email); 
            Assert.AreEqual("PersonalInfo", addressBook.Address_Book_Name);
            Assert.AreEqual("Personal", addressBook.Address_Book_Type);
            Assert.AreEqual("2018-06-08", addressBook.Start_Date);
            Assert.AreEqual("2020-09-04", addressBook.End_Date);
            System.Console.WriteLine(response.Content);
        }

        /// <summary>
        /// Ability to Delete entry from AddressBook JSON Server
        /// </summary>
        [TestMethod]
        public void GivenPersonId_OnDelete_ShouldReturnSuccessStatus()
        {
            ///Arrange
            RestRequest restRequest = new RestRequest("/addressBook/5", Method.DELETE);

            ///Act
            IRestResponse response = client.Execute(restRequest);

            ///Assert
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.OK);
            System.Console.WriteLine(response.Content);
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
