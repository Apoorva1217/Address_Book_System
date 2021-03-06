﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AddressBookDB
{
    public class AddressBookRepo
    {
        /// <summary>
        /// Ability to create a AddressBook service database and have C# program connect to database
        /// </summary>
        public static string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Address_Book_Service;Integrated Security=True";
        SqlConnection sqlconnection = new SqlConnection(connectionString);

        /// <summary>
        /// Ability for the AddressBook Service to rerieve all the Entries from the DB
        /// </summary>
        public bool GetAllDetails()
        {
            SqlConnection sqlconnection = new SqlConnection(connectionString);
            try
            {
                AddressBookModel employeeModel = new AddressBookModel();
                using (this.sqlconnection)
                {
                    string query = @"SELECT *
                                    FROM Address_Book;";
                    SqlCommand sqlCommand = new SqlCommand(query, this.sqlconnection);

                    this.sqlconnection.Open();

                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {
                            employeeModel.First_Name = sqlDataReader.GetString(0);
                            employeeModel.Last_Name = sqlDataReader.GetString(1);
                            employeeModel.Person_Address = sqlDataReader.GetString(2);
                            employeeModel.City = sqlDataReader.GetString(3);
                            employeeModel.State = sqlDataReader.GetString(4);
                            employeeModel.Zip_Code = sqlDataReader.GetString(5);
                            employeeModel.Phone_Number = sqlDataReader.GetString(6);
                            employeeModel.Email = sqlDataReader.GetString(7);
                            employeeModel.Address_Book_Name = sqlDataReader.GetString(8);
                            employeeModel.Address_Book_Type = sqlDataReader.GetString(9);

                            Console.WriteLine("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}",
                                employeeModel.First_Name, employeeModel.Last_Name, employeeModel.Person_Address, employeeModel.City,
                                employeeModel.State, employeeModel.Zip_Code, employeeModel.Phone_Number, employeeModel.Email,
                                employeeModel.Address_Book_Name, employeeModel.Address_Book_Type);
                            Console.WriteLine("\n");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No data found");
                    }
                    sqlDataReader.Close();
                    this.sqlconnection.Close();
                    return true;
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
            finally
            {
                this.sqlconnection.Close();
            }
        }

        /// <summary>
        /// Ability to update the Contact Information in the address book for a person and ensure that the Contact Information in the memory is in Sync with the DB
        /// </summary>
        /// <param name="addressBookModel"></param>
        public bool UpdateContact(string firstName, string lastName)
        {
            sqlconnection = new SqlConnection(connectionString);
            try
            {
                AddressBookModel employeeModel = new AddressBookModel();
                using (this.sqlconnection)
                {
                    string query = @"UPDATE Address_Book SET Person_Address='Ghansoli',City='NaviMumbai',State='Maharashtra' 
                                    WHERE First_Name='Aayush' AND Last_Name='Kadam';";
                    SqlCommand sqlCommand = new SqlCommand(query, this.sqlconnection);

                    this.sqlconnection.Open();

                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {
                            employeeModel.First_Name = sqlDataReader.GetString(0);
                            employeeModel.Last_Name = sqlDataReader.GetString(1);
                            employeeModel.Person_Address = sqlDataReader.GetString(2);
                            employeeModel.City = sqlDataReader.GetString(3);
                            employeeModel.State = sqlDataReader.GetString(4);


                            Console.WriteLine("{0},{1},{2},{3},{4}",
                                employeeModel.First_Name, employeeModel.Last_Name, employeeModel.Person_Address, employeeModel.City,
                                employeeModel.State);
                            Console.WriteLine("\n");
                        }
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("No data found");
                    }
                    sqlDataReader.Close();
                    this.sqlconnection.Close();
                    return false;
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
            finally
            {
                this.sqlconnection.Close();
            }
        }

        /// <summary>
        /// Ability to Retrieve Contacts from the Database that were added in a particular period
        /// </summary>
        /// <param name="start_Date"></param>
        /// <param name="end_Date"></param>
        public bool GetContactsByDateRange(string start_Date, string end_Date)
        {
            SqlConnection sqlconnection = new SqlConnection(connectionString);
            try
            {
                AddressBookModel employeeModel = new AddressBookModel();
                DateTime Start_Date = Convert.ToDateTime(start_Date);
                DateTime End_Date = Convert.ToDateTime(end_Date);
                using (this.sqlconnection)
                {
                    SqlCommand sqlCommand = new SqlCommand("SpGetContactsByDateRange", this.sqlconnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@PersonId", employeeModel.PersonId);
                    sqlCommand.Parameters.AddWithValue("@Start_Date", Start_Date);
                    sqlCommand.Parameters.AddWithValue("@End_Date", End_Date);

                    this.sqlconnection.Open();

                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {
                            employeeModel.First_Name = sqlDataReader.GetString(0);
                            employeeModel.Last_Name = sqlDataReader.GetString(1);
                            employeeModel.Person_Address = sqlDataReader.GetString(2);
                            employeeModel.City = sqlDataReader.GetString(3);
                            employeeModel.State = sqlDataReader.GetString(4);
                            employeeModel.Zip_Code = sqlDataReader.GetString(5);
                            employeeModel.Phone_Number = sqlDataReader.GetString(6);
                            employeeModel.Email = sqlDataReader.GetString(7);
                            employeeModel.Address_Book_Name = sqlDataReader.GetString(8);
                            employeeModel.Address_Book_Type = sqlDataReader.GetString(9);

                            Console.WriteLine("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}",
                                employeeModel.First_Name, employeeModel.Last_Name, employeeModel.Person_Address, employeeModel.City,
                                employeeModel.State, employeeModel.Zip_Code, employeeModel.Phone_Number, employeeModel.Email,
                                employeeModel.Address_Book_Name, employeeModel.Address_Book_Type);
                            Console.WriteLine("\n");
                        }
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("No data found");
                        
                    }
                    sqlDataReader.Close();
                    this.sqlconnection.Close();
                    return false;
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
            finally
            {
                this.sqlconnection.Close();
            }
        }

        /// <summary>
        /// Ability to Retrieve number of Contacts in the Database by City or State
        /// </summary>
        public void GetCountByCityOrState()
        {
            sqlconnection = new SqlConnection(connectionString);
            try
            {
                AddressBookModel employeeModel = new AddressBookModel();
                using (sqlconnection)
                {
                    string query = @"SELECT COUNT(City),City 
                                    FROM Address INNER JOIN People
                                    ON Address.PersonId=People.PersonId
                                    WHERE City='Pune' 
                                    GROUP BY City;

                                    SELECT COUNT(State),State 
                                    FROM Address INNER JOIN People
                                    ON Address.PersonId=People.PersonId
                                    WHERE State='Maharashtra' 
                                    GROUP BY State;";
                    SqlCommand sqlCommand = new SqlCommand(query, this.sqlconnection);

                    this.sqlconnection.Open();

                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    int COUNT = 0;
                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {
                            COUNT = sqlDataReader.GetInt32(0);
                            employeeModel.City = sqlDataReader.GetString(1);
                            employeeModel.State = sqlDataReader.GetString(2);

                            Console.WriteLine("Count:" + COUNT + "\nCity:" + employeeModel.City + "\nState:" + employeeModel.State);
                        }
                    }
                    else
                    {
                        Console.WriteLine("No data found");
                    }
                    sqlDataReader.Close();
                    this.sqlconnection.Close();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
            finally
            {
                this.sqlconnection.Close();
            }
        }

        /// <summary>
        /// Ability to Add new Contact to the Address Book Database
        /// </summary>
        /// <param name="contact"></param>
        /// <returns></returns>
        public bool AddContact(AddressBookModel addressBookModel)
        {
            sqlconnection = new SqlConnection(connectionString);
            try
            {
                using (sqlconnection)
                {
                    SqlCommand sqlCommand = new SqlCommand("SpAddContact", this.sqlconnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@First_Name", addressBookModel.First_Name);
                    sqlCommand.Parameters.AddWithValue("@Last_Name", addressBookModel.Last_Name);
                    sqlCommand.Parameters.AddWithValue("@Person_Address", addressBookModel.Person_Address);
                    sqlCommand.Parameters.AddWithValue("@City", addressBookModel.City);
                    sqlCommand.Parameters.AddWithValue("@State", addressBookModel.State);
                    sqlCommand.Parameters.AddWithValue("@Zip_Code", addressBookModel.Zip_Code);
                    sqlCommand.Parameters.AddWithValue("@Phone_Number", addressBookModel.Phone_Number);
                    sqlCommand.Parameters.AddWithValue("@Email", addressBookModel.Email);
                    sqlCommand.Parameters.AddWithValue("@Address_Book_Name", addressBookModel.Address_Book_Name);
                    sqlCommand.Parameters.AddWithValue("@Address_Book_Type", addressBookModel.Address_Book_Type);

                    this.sqlconnection.Open();
                    var result = sqlCommand.ExecuteNonQuery();
                    this.sqlconnection.Close();

                    if (result != 0)
                    {
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("No data found");
                        return false;
                    }
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
            finally
            {
                this.sqlconnection.Close();
            }
        }

        /// <summary>
        /// Add Person Contact to Address Book with Thread
        /// </summary>
        /// <param name="addressBookModel"></param>
        public void AddPersonToAddressBookWithThread(List<AddressBookModel> addressBookModel)
        {
            addressBookModel.ForEach(addressBookData =>
            {
                Task thread = new Task(() =>
                {
                    Console.WriteLine("Employee Being Added: " + addressBookData.First_Name);
                    this.AddContact(addressBookData);
                    Console.WriteLine("Employee Added: " + addressBookData.First_Name);
                });
                thread.Start();
            });
        }
    }
}
