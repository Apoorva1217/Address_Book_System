using AddressBookDB;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AddressBookDBTests
{
    [TestClass]
    public class UnitTest1
    {
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
            bool result = addressBookRepo.UpdateContact("Aayush","Kadam");
            Assert.IsTrue(result);
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
