using System;
using System.Collections.Generic;

namespace AddressBookDB
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Address Book Database!");
            AddressBookRepo addressBookRepo = new AddressBookRepo();
            AddressBookModel addressBookModel = new AddressBookModel();
            List<AddressBookModel> addressBookModels = new List<AddressBookModel>();
            addressBookRepo.GetAllDetails();
            addressBookRepo.UpdateContact("Aayush","Kadam");
            addressBookRepo.GetContactsByDateRange("2019-02-03","2020-10-22");
            addressBookRepo.GetCountByCityOrState();

            addressBookModel.First_Name = "Manaswi";
            addressBookModel.Last_Name = "Patil";
            addressBookModel.Person_Address = "Baner";
            addressBookModel.City = "Pune";
            addressBookModel.State = "Maharashtra";
            addressBookModel.Zip_Code = "43526";
            addressBookModel.Phone_Number = "9080769856";
            addressBookModel.Email = "Manu@gmail.com";
            addressBookModel.Address_Book_Name = "FriendList";
            addressBookModel.Address_Book_Type = "Friends";

            addressBookRepo.AddContact(addressBookModel);
            addressBookRepo.AddPersonToAddressBookWithThread(addressBookModels);
        }
    }
}
