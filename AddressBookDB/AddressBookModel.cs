using System;
using System.Collections.Generic;
using System.Text;

namespace AddressBookDB
{
    public class AddressBookModel
    {
        public int PersonId { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Person_Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip_Code { get; set; }
        public string Phone_Number { get; set; }
        public string Email { get; set; }
        public string Address_Book_Name { get; set; }
        public string Address_Book_Type { get; set; }
        public string Start_Date { get; set; }
        public string End_Date { get; set; }
    }
}
