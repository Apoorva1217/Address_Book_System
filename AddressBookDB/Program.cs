using System;

namespace AddressBookDB
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Address Book Datbase!");
            AddressBookRepo addressBookRepo = new AddressBookRepo();
            addressBookRepo.GetAllDetails();
        }
    }
}
