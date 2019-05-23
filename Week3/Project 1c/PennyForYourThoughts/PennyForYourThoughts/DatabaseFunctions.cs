using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PennyForYourThoughts
{
    class DatabaseFunctions
    {
        private static string GetConnString()
        {
            //Empty until imported from fle
            string ip = "";

            using (StreamReader sr = new StreamReader("c:/VFW/connection.txt"))
            {
                // Sets ip value to line in file
                ip = sr.ReadLine();
            }
            //Set conString with ip from file
            string conString = $"Server={ip};userid=root;password=root;database=KinaoleLau_Database;port=8889";

            return conString;
        }

        public static string CheckUsername(string username)
        {
            // Returns user password to check in login class if username exists, else returns password variable with value of false
            string password = null;

            // Use try catch to connect to database, get data into datatable and save data to dictionary
            try
            {
                MySqlConnection conn = new MySqlConnection(GetConnString());
                conn.Open();

                MySqlDataReader rdr = null;

                string stm = "Select * from users where username = @username";

                MySqlCommand cmd = new MySqlCommand(stm, conn);

                cmd.Parameters.AddWithValue("@username", username);

                rdr = cmd.ExecuteReader();


                while (rdr.Read())
                {
                    // Save password to password variable
                    password = rdr["password"].ToString();
                }

                //if no username existed in the database the password variable will remain null so set it to false
                if(string.IsNullOrWhiteSpace(password))
                {
                    password = "false";
                }
                
                //else there was a user with the username already in the database so do NOT change the password value

                conn.Close();

            }
            catch (MySqlException e)
            {
                string msg = e.ToString();
                Console.WriteLine(msg);
            }
            // return the variable password
            return password;
        }

        public static string CheckUsernameAndPassword(string username, string password)
        {
            // Returns user password to check in login class if username exists, else returns password variable with value of false
            string name = null;

            // Use try catch to connect to database, get data into datatable and save data to dictionary
            try
            {
                MySqlConnection conn = new MySqlConnection(GetConnString());
                conn.Open();

                MySqlDataReader rdr = null;

                string stm = "Select * from users where username = @username and password = @password";

                MySqlCommand cmd = new MySqlCommand(stm, conn);

                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);

                rdr = cmd.ExecuteReader();


                while (rdr.Read())
                {
                    // Save name to name variable
                    name = rdr["name"].ToString();
                }

                //if the username and password in the database don't match the variable will remain null so set it to false
                if (string.IsNullOrWhiteSpace(password))
                {
                    name = "false";
                }

                //else the username and password do match so leave the name variable alone

                conn.Close();

            }
            catch (MySqlException e)
            {
                string msg = e.ToString();
                Console.WriteLine(msg);
            }
            // return the variable name
            return name;
        }

        public static void AddUser(string username, string password, string name)
        {
            // Use try catch to connect to database, get data into datatable and save data to dictionary
            try
            {
                MySqlConnection conn = new MySqlConnection(GetConnString());
                conn.Open();

                MySqlDataReader rdr = null;

                // Insert new user into database
                string stm = "Insert into users (name, username, password) values (@name, @username, @password)";

                MySqlCommand cmd = new MySqlCommand(stm, conn);

                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);
                cmd.Parameters.AddWithValue("@name", name);

                rdr = cmd.ExecuteReader();

                // check that user was successfully inserted
                stm = "Select * from users where username = @username and password = @password and name = @name";

                cmd = new MySqlCommand(stm, conn);

                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);
                cmd.Parameters.AddWithValue("@name", name);

                rdr = cmd.ExecuteReader();

                string nameTest = null;

                while (rdr.Read())
                {
                    // Save name to name variable
                    nameTest = rdr["name"].ToString();
                }

                // if no name was returned then the insert failed
                if (string.IsNullOrWhiteSpace(nameTest))
                {
                    Console.WriteLine("Insert statement failed.");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("User created successfully!");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }

                conn.Close();

            }
            catch (MySqlException e)
            {
                string msg = e.ToString();
                Console.WriteLine(msg);
            }
        }
    }
}
