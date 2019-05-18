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
        public static List<double> GetTimes()
        {
            // Create empty list to hold the times
            List<double> times = new List<double>();

            // Use try catch to connect to database, get and save data to list
            try
            {
                MySqlConnection conn = new MySqlConnection(GetConnString());
                conn.Open();

                MySqlDataReader rdr = null;

                string stm = "Select time_spent_on_activity from activity_times";

                MySqlCommand cmd = new MySqlCommand(stm, conn);
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    // Save each category name into the list
                    string timeString = rdr["time_spent_on_activity"].ToString();
                    double time = double.Parse(timeString);

                    times.Add(time);
                }

                conn.Close();

            }
            catch (MySqlException e)
            {
                string msg = e.ToString();
                Console.WriteLine(msg);
            }
            return times;
        }

        public static Dictionary<string, int> GetDatesAndDays()
        {
            // Create empty dictionary to hold the dates and corresponding month day
            Dictionary<string, int> datesAndDays = new Dictionary<string, int>();

            // Use try catch to connect to database, get and save data to dictionary
            try
            {
                MySqlConnection conn = new MySqlConnection(GetConnString());
                conn.Open();

                MySqlDataReader rdr = null;

                string stm = "Select calendar_date, calendar_date_id from tracked_calendar_dates";

                MySqlCommand cmd = new MySqlCommand(stm, conn);
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    // Save each date and day into the list
                    string date = rdr["calendar_date"].ToString();
                    string dayString = rdr["calendar_date_id"].ToString();
                    int day = int.Parse(dayString);


                    datesAndDays.Add(date, day);
                }

                conn.Close();

            }
            catch (MySqlException e)
            {
                string msg = e.ToString();
                Console.WriteLine(msg);
            }
            return datesAndDays;
        }

        public static List<string> GetDescriptions()
        {
            // Create empty list to hold the descriptions
            List<string> descriptions = new List<string>();

            // Use try catch to connect to database, get and save data to list
            try
            {
                MySqlConnection conn = new MySqlConnection(GetConnString());
                conn.Open();

                MySqlDataReader rdr = null;

                string stm = "Select activity_description from activity_descriptions";

                MySqlCommand cmd = new MySqlCommand(stm, conn);
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    // Save each activity name into the list
                    string name = rdr["activity_description"].ToString();

                    descriptions.Add(name);
                }

                conn.Close();

            }
            catch (MySqlException e)
            {
                string msg = e.ToString();
                Console.WriteLine(msg);
            }
            return descriptions;
        }

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
