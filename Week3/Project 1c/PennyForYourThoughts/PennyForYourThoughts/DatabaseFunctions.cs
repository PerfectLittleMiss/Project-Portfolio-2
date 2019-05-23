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

                conn.Close();
                conn.Open();

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

        public static int GetThoughtCount(string username)
        {
            // create int to hold the thought count
            int count = 0;

            // Use try catch to connect to database, get data into datatable and save data to dictionary
            try
            {
                MySqlConnection conn = new MySqlConnection(GetConnString());
                conn.Open();

                MySqlDataReader rdr = null;

                string stm = "Select count(thoughtId) from thoughts where userId = @userId";

                MySqlCommand cmd = new MySqlCommand(stm, conn);

                int userId = GetUserId(username);

                cmd.Parameters.AddWithValue("@userId", userId);

                rdr = cmd.ExecuteReader();


                while (rdr.Read())
                {
                    // Save password to password variable
                    count = int.Parse(rdr["count(thoughtId)"].ToString());
                }

                conn.Close();

            }
            catch (MySqlException e)
            {
                string msg = e.ToString();
                Console.WriteLine(msg);
            }
            // return the variable count
            return count;
        }

        public static int GetUserId(string username)
        {
            // create int to hold user id
            int userId = 0;

            // Use try catch to connect to database, get data into datatable and save data to dictionary
            try
            {
                MySqlConnection conn = new MySqlConnection(GetConnString());
                conn.Open();

                MySqlDataReader rdr = null;

                string stm = "Select userId from users where username = @username";

                MySqlCommand cmd = new MySqlCommand(stm, conn);

                cmd.Parameters.AddWithValue("@username", username);

                rdr = cmd.ExecuteReader();


                while (rdr.Read())
                {
                    // Save password to password variable
                    userId = int.Parse(rdr["userId"].ToString());
                }

                conn.Close();

            }
            catch (MySqlException e)
            {
                string msg = e.ToString();
                Console.WriteLine(msg);
            }
            // return the variable password
            return userId;
        }

        public static Dictionary<int, string> SearchThoughts(int userId, string searchTerm)
        {
            //Dictionary to hold the thought id and preview of thoughts that match the search term
            Dictionary<int, string> idAndPreview = new Dictionary<int, string>();

            // Use try catch to connect to database, get data into datatable and save data to dictionary
            try
            {
                MySqlConnection conn = new MySqlConnection(GetConnString());
                conn.Open();

                MySqlDataReader rdr = null;

                string stm = "Select thoughtId, preview from thoughts where userId = @userId and content like @searchTerm";

                MySqlCommand cmd = new MySqlCommand(stm, conn);

                searchTerm = "%" + searchTerm + "%";

                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.Parameters.AddWithValue("@searchTerm", searchTerm);

                rdr = cmd.ExecuteReader();


                while (rdr.Read())
                {
                    // Save id to thoughtId and content to content
                    int thoughtId = int.Parse(rdr["thoughtId"].ToString());
                    string preview = rdr["preview"].ToString();

                    // add id and content to the dictionary
                    idAndPreview.Add(thoughtId, preview);
                }

                conn.Close();

            }
            catch (MySqlException e)
            {
                string msg = e.ToString();
                Console.WriteLine(msg);
            }
            // return the variable password
            return idAndPreview;
        }

        public static string GetThoughtContent(int thoughtId)
        {
            string content = null;

            // Use try catch to connect to database, get data into datatable and save data to dictionary
            try
            {
                MySqlConnection conn = new MySqlConnection(GetConnString());
                conn.Open();

                MySqlDataReader rdr = null;

                string stm = "Select content from thoughts where thoughtId = @thoughtId";

                MySqlCommand cmd = new MySqlCommand(stm, conn);

                cmd.Parameters.AddWithValue("@thoughtId", thoughtId);

                rdr = cmd.ExecuteReader();


                while (rdr.Read())
                {
                    // Save password to password variable
                    content = rdr["content"].ToString();
                }

                conn.Close();

            }
            catch (MySqlException e)
            {
                string msg = e.ToString();
                Console.WriteLine(msg);
            }
            // return the variable password
            return content;
        }

        public static void UpdateThought(int thoughtId, string newContent, string preview, string updated)
        {
            // Use try catch to connect to database, get data into datatable and save data to dictionary
            try
            {
                MySqlConnection conn = new MySqlConnection(GetConnString());
                conn.Open();

                MySqlDataReader rdr = null;

                string stm = "Update thoughts set preview = @preview, content = @content, updated = @updated where thoughtId = @thoughtId";

                MySqlCommand cmd = new MySqlCommand(stm, conn);

                cmd.Parameters.AddWithValue("@thoughtId", thoughtId);
                cmd.Parameters.AddWithValue("@preview", preview);
                cmd.Parameters.AddWithValue("@content", newContent);
                cmd.Parameters.AddWithValue("@updated", updated);

                rdr = cmd.ExecuteReader();

                conn.Close();
                conn.Open();

                stm = "Select * from thoughts where preview = @preview and content = @content and updated = @updated and thoughtId = @thoughtId";

                cmd = new MySqlCommand(stm, conn);

                cmd.Parameters.AddWithValue("@thoughtId", thoughtId);
                cmd.Parameters.AddWithValue("@preview", preview);
                cmd.Parameters.AddWithValue("@content", newContent);
                cmd.Parameters.AddWithValue("@updated", updated);

                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    // If rdr has a row then it worked so just tell the user that their thought has been updated
                    Console.WriteLine("Your thought has been successfully updated!");
                    Console.WriteLine("Returning to the thoughts menu. Press any key to continue...");
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

        public static void DeleteThought(int thoughtId)
        {
            // Use try catch to connect to database, get data into datatable and save data to dictionary
            try
            {
                MySqlConnection conn = new MySqlConnection(GetConnString());
                conn.Open();

                MySqlDataReader rdr = null;

                string stm = "Delete from thoughts where thoughtId = @thoughtId";

                MySqlCommand cmd = new MySqlCommand(stm, conn);

                cmd.Parameters.AddWithValue("@thoughtId", thoughtId);

                rdr = cmd.ExecuteReader();

                conn.Close();

                conn.Open();

                stm = "Select * from thoughts where thoughtId = @thoughtId";

                cmd = new MySqlCommand(stm, conn);

                cmd.Parameters.AddWithValue("@thoughtId", thoughtId);

                rdr = cmd.ExecuteReader();


                while (rdr.Read())
                {
                    // if there's a row that means the delete failed
                    Console.WriteLine("Delete failed.");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }

                Console.WriteLine("Thought successfully deleted. Press any key to return to the thoughts menu...");
                Console.ReadKey();

                conn.Close();

            }
            catch (MySqlException e)
            {
                string msg = e.ToString();
                Console.WriteLine(msg);
            }
        }

        public static Dictionary<int, string> GetAllThoughts(int userId)
        {
            //Dictionary to hold the thought id and preview of thoughts that match the search term
            Dictionary<int, string> idAndPreview = new Dictionary<int, string>();

            // Use try catch to connect to database, get data into datatable and save data to dictionary
            try
            {
                MySqlConnection conn = new MySqlConnection(GetConnString());
                conn.Open();

                MySqlDataReader rdr = null;

                string stm = "Select thoughtId, preview from thoughts where userId = @userId";

                MySqlCommand cmd = new MySqlCommand(stm, conn);

                cmd.Parameters.AddWithValue("@userId", userId);

                rdr = cmd.ExecuteReader();


                while (rdr.Read())
                {
                    // Save id to thoughtId and content to content
                    int thoughtId = int.Parse(rdr["thoughtId"].ToString());
                    string preview = rdr["preview"].ToString();

                    // add id and content to the dictionary
                    idAndPreview.Add(thoughtId, preview);
                }

                conn.Close();

            }
            catch (MySqlException e)
            {
                string msg = e.ToString();
                Console.WriteLine(msg);
            }
            // return the variable password
            return idAndPreview;
        }

        public static void CreateThought(string content, string preview, string updated, int userId)
        {
            // Use try catch to connect to database, get data into datatable and save data to dictionary
            try
            {
                MySqlConnection conn = new MySqlConnection(GetConnString());
                conn.Open();

                MySqlDataReader rdr = null;

                string stm = "Insert into thoughts (userId, preview, content, updated) values (@userId, @preview, @content, @updated)";

                MySqlCommand cmd = new MySqlCommand(stm, conn);
                
                cmd.Parameters.AddWithValue("@preview", preview);
                cmd.Parameters.AddWithValue("@content", content);
                cmd.Parameters.AddWithValue("@updated", updated);
                cmd.Parameters.AddWithValue("@userId", userId);

                rdr = cmd.ExecuteReader();

                conn.Close();
                conn.Open();

                stm = "Select * from thoughts where preview = @preview and content = @content and updated = @updated";

                cmd = new MySqlCommand(stm, conn);
                
                cmd.Parameters.AddWithValue("@preview", preview);
                cmd.Parameters.AddWithValue("@content", content);
                cmd.Parameters.AddWithValue("@updated", updated);

                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    // If rdr has a row then it worked so just tell the user that their thought has been created
                    Console.WriteLine("Your thought has been successfully created!");
                    Console.WriteLine("Returning to the thoughts menu. Press any key to continue...");
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
