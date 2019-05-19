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
        public static int Login(string first, string last, string password)
        {
            // variables to hold the db values
            string dbFirst;
            string dbLast;
            string dbPassword;

            // variable to hold the user id
            int userId = 0;

            // Use try catch to connect to database, get and save data to list
            try
            {
                MySqlConnection conn = new MySqlConnection(GetConnString());
                conn.Open();

                MySqlDataReader rdr = null;

                string stm = "Select * from time_tracker_users";

                MySqlCommand cmd = new MySqlCommand(stm, conn);
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    // Save each category name into the list
                    dbFirst = rdr["user_firstname"].ToString();
                    dbLast = rdr["user_lastname"].ToString();
                    dbPassword = rdr["user_password"].ToString();

                    if(first == dbFirst && last == dbLast && password == dbPassword)
                    {
                        string userIdString = rdr["user_id"].ToString();
                        userId = int.Parse(userIdString);
                    }
                }

                conn.Close();

            }
            catch (MySqlException e)
            {
                string msg = e.ToString();
                Console.WriteLine(msg);
            }
            return userId;
        }

        public static void EnterActivity(int userId, int day, string date, int weekDay, string category, string activity, double time)
        {
            int dateId = GetDateId(date);
            int categoryId = GetCategoryId(category);
            int activtyId = GetActivityId(activity);
            int timeId = GetTimeId(time);

            // Use try catch to connect to database, get and save data to list
            try
            {
                MySqlConnection conn = new MySqlConnection(GetConnString());

                MySqlDataAdapter adr = new MySqlDataAdapter();

                conn.Open();

                string stm = "Insert into activity_log (user_id, calendar_day, calendar_date, day_name, category_description, activity_description, time_spent_on_activity) values " +
                    "(@userId, @dayId, @dateId, @weekDayId, @categoryId, @activityId, @timeId)";

                MySqlCommand cmd = new MySqlCommand(stm, conn);

                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.Parameters.AddWithValue("@dayId", day);
                cmd.Parameters.AddWithValue("@dateId", dateId);
                cmd.Parameters.AddWithValue("@weekDayId", weekDay);
                cmd.Parameters.AddWithValue("@categoryId", categoryId);
                cmd.Parameters.AddWithValue("@activityId", activtyId);
                cmd.Parameters.AddWithValue("@timeId", timeId);

                adr.InsertCommand = cmd;
                adr.InsertCommand.ExecuteNonQuery();

                Console.WriteLine("Activity saved to the database.");

                conn.Close();

            }
            catch (MySqlException e)
            {
                string msg = e.ToString();
                Console.WriteLine(msg);
            }
        }

        private static int GetTimeId(double time)
        {
            int id = 0;

            // Use try catch to connect to database, get and save data to list
            try
            {
                MySqlConnection conn = new MySqlConnection(GetConnString());
                conn.Open();

                MySqlDataReader rdr = null;

                string stm = "Select activity_time_id from activity_times where time_spent_on_activity = @time";

                MySqlCommand cmd = new MySqlCommand(stm, conn);

                cmd.Parameters.AddWithValue("@time", time);

                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    // Save each category name into the list
                    string idString = rdr["activity_time_id"].ToString();
                    id = int.Parse(idString);
                }

                conn.Close();

            }
            catch (MySqlException e)
            {
                string msg = e.ToString();
                Console.WriteLine(msg);
            }
            return id;
        }

        private static int GetActivityId(string activity)
        {
            int id = 0;

            // Use try catch to connect to database, get and save data to list
            try
            {
                MySqlConnection conn = new MySqlConnection(GetConnString());
                conn.Open();

                MySqlDataReader rdr = null;

                string stm = "Select activity_description_id from activity_descriptions where activity_description = @activity";

                MySqlCommand cmd = new MySqlCommand(stm, conn);

                cmd.Parameters.AddWithValue("@activity", activity);

                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    // Save each category name into the list
                    string idString = rdr["activity_description_id"].ToString();
                    id = int.Parse(idString);
                }

                conn.Close();

            }
            catch (MySqlException e)
            {
                string msg = e.ToString();
                Console.WriteLine(msg);
            }
            return id;
        }

        private static int GetCategoryId(string category)
        {
            int id = 0;

            // Use try catch to connect to database, get and save data to list
            try
            {
                MySqlConnection conn = new MySqlConnection(GetConnString());
                conn.Open();

                MySqlDataReader rdr = null;

                string stm = "Select activity_category_id from activity_categories where category_description = @category";

                MySqlCommand cmd = new MySqlCommand(stm, conn);

                cmd.Parameters.AddWithValue("@category", category);

                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    // Save each category name into the list
                    string idString = rdr["activity_category_id"].ToString();
                   id = int.Parse(idString);
                }

                conn.Close();

            }
            catch (MySqlException e)
            {
                string msg = e.ToString();
                Console.WriteLine(msg);
            }
            return id;
        }

        private static int GetDateId(string date)
        {
            int id = 0;

            // Use try catch to connect to database, get and save data to list
            try
            {
                MySqlConnection conn = new MySqlConnection(GetConnString());
                conn.Open();

                MySqlDataReader rdr = null;

                string getDateId = "Select calendar_date_id from tracked_calendar_dates where calendar_date = @date";

                MySqlCommand cmd = new MySqlCommand(getDateId, conn);

                cmd.Parameters.AddWithValue("@date", date);

                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    // Save each category name into the list
                    string idString = rdr["calendar_date_id"].ToString();
                    id = int.Parse(idString);
                }

                conn.Close();

            }
            catch (MySqlException e)
            {
                string msg = e.ToString();
                Console.WriteLine(msg);
            }
            return id;
        }

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
