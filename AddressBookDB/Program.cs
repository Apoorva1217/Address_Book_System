using System;

namespace AddressBookDB
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Address Book Datbase!");
            AddressBookRepo addressBookRepo = new AddressBookRepo();
            AddressBookModel addressBookModel = new AddressBookModel();  
            addressBookRepo.GetAllDetails();
            addressBookRepo.UpdateContact(addressBookModel);
            addressBookRepo.GetContactsByDateRange("2019-02-03","2020-10-22");
            addressBookRepo.GetCountByCityOrState();
            addressBookRepo.AddContact(addressBookModel);
        }
    }
}
