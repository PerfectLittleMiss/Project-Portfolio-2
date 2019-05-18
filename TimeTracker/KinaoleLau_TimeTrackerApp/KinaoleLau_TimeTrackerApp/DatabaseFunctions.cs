using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinaoleLau_TimeTrackerApp
{
    class DatabaseFunctions
    {
        public static List<string> GetCategories()
        {
            // Create empty list to hold the categories
            List<string> categories = new List<string>();

            // Use try catch to connect to database, get and save data to list
            try
            {
                MySqlConnection conn = new MySqlConnection(GetConnString());
                conn.Open();

                MySqlDataReader rdr = null;

                string stm = "Select category_description from activity_categories";

                MySqlCommand cmd = new MySqlCommand(stm, conn);
                rdr = cmd.ExecuteReader();
                
                while (rdr.Read())
                {
                    // Save each category name into the list
                    string name = rdr["category_description"].ToString();

                    categories.Add(name);
                }

                conn.Close();

            }
            catch (MySqlException e)
            {
                string msg = e.ToString();
                Console.WriteLine(msg);
            }
            return categories;
        }

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
            string conString = $"Server={ip};userid=root;password=root;database=kinaolelau_mdv229_database_201905;port=8889";

            return conString;
        }
    }
}
