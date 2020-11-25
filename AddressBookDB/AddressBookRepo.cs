using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

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
        public void GetAllDetails()
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
        public void UpdateContact(AddressBookModel addressBookModel)
        {
            SqlConnection sqlconnection = new SqlConnection(connectionString);
            try
            {
                AddressBookModel employeeModel = new AddressBookModel();
                using (this.sqlconnection)
                {
                    string query = @"UPDATE Address_Book SET Person_Address='Vashi',City='NaviMumbai',State='Maharashtra' 
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
    }
}
