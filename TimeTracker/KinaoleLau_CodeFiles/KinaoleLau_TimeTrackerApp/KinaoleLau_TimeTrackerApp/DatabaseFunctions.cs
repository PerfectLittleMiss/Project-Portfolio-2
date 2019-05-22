using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinaoleLau_TimeTrackerApp
{
    class DatabaseFunctions
    {
        public static double TotalTimeSpentOnActivity(int activityId, int userId)
        {
            // Create double to hold total time of specified activity in specified category
            double totalTime = 0;

            // Use try catch to connect to database, get and save data to dictionary
            try
            {
                MySqlConnection conn = new MySqlConnection(GetConnString());
                conn.Open();

                string stm = "Select SUM(activity_times.time_spent_on_activity) from activity_log join activity_times on activity_log.time_spent_on_activity = activity_times.activity_time_id where activity_log.activity_description = @activityId and user_id = @userId";

                MySqlCommand cmd = new MySqlCommand(stm, conn);
                
                cmd.Parameters.AddWithValue("@activityId", activityId);
                cmd.Parameters.AddWithValue("@userId", userId);

                string timeCheck = cmd.ExecuteScalar().ToString().ToLower();

                if (double.TryParse(timeCheck, out totalTime))
                {
                    totalTime = Convert.ToDouble(timeCheck);
                }
                else
                {
                    totalTime = 0;
                }

                conn.Close();

            }
            catch (MySqlException e)
            {
                string msg = e.ToString();
                Console.WriteLine(msg);
            }
            return totalTime;
        }

        public static double GetTotalTimeForMonth(int userId)
        {
            // Create double to hold total time of specified activity in specified category
            double totalTime = 0;

            // Use try catch to connect to database, get and save data to dictionary
            try
            {
                MySqlConnection conn = new MySqlConnection(GetConnString());
                conn.Open();

                string stm = "Select SUM(activity_times.time_spent_on_activity) from activity_log join activity_times on activity_log.time_spent_on_activity = activity_times.activity_time_id where user_id = @userId";

                MySqlCommand cmd = new MySqlCommand(stm, conn);

                cmd.Parameters.AddWithValue("@userId", userId);

                string timeCheck = cmd.ExecuteScalar().ToString().ToLower();

                if (double.TryParse(timeCheck, out totalTime))
                {
                    totalTime = Convert.ToDouble(timeCheck);
                }
                else
                {
                    totalTime = 0;
                }

                conn.Close();

            }
            catch (MySqlException e)
            {
                string msg = e.ToString();
                Console.WriteLine(msg);
            }
            return totalTime;
        }

        public static double TotalTimeSpentOnCategory(int categoryId, int userId)
        {
            double time = 0;

            // Use try catch to connect to database, get and save data to dictionary
            try
            {
                MySqlConnection conn = new MySqlConnection(GetConnString());
                conn.Open();

                string stm = "Select SUM(activity_times.time_spent_on_activity) from activity_log join activity_times on activity_log.time_spent_on_activity = activity_times.activity_time_id where activity_log.category_description = @categoryId and user_id = @userId";

                MySqlCommand cmd = new MySqlCommand(stm, conn);

                cmd.Parameters.AddWithValue("@categoryId", categoryId);
                cmd.Parameters.AddWithValue("@userId", userId);

                string timeCheck = cmd.ExecuteScalar().ToString().ToLower();

                if(double.TryParse(timeCheck, out time))
                {
                    time = Convert.ToDouble(timeCheck);
                }
                else
                {
                    time = 0;
                }

                conn.Close();

            }
            catch (MySqlException e)
            {
                string msg = e.ToString();
                Console.WriteLine(msg);
            }
            return time;
        }

        public static int GetMinutesOnly(double time)
        {
            int minutes = (int)Math.Round(((time - GetHoursOnly(time)) * 100));

            switch(minutes)
            {
                case 0:
                    minutes = 0;
                    break;
                case 25:
                    minutes = 15;
                    break;
                case 50:
                    minutes = 30;
                    break;
                case 75:
                    minutes = 45;
                    break;
            }

            return minutes;
        }

        public static int GetHoursOnly(double time)
        {
            int hours = (int)Math.Truncate(time);

            return hours;
        }

        public static List<string> GetCategoryDatesForActivity(string activity, string category, int userId)
        {
            // Create empty list to hold the activity dates for the given activity and category
            List<string> dates = new List<string>();

            // Use try catch to connect to database, get and save data to dictionary
            try
            {
                MySqlConnection conn = new MySqlConnection(GetConnString());
                conn.Open();

                MySqlDataReader rdr = null;

                string stm = "Select tracked_calendar_dates.calendar_date from activity_log join tracked_calendar_dates on activity_log.calendar_date = tracked_calendar_dates.calendar_date_id where activity_log.category_description = @categoryId and activity_log.activity_description = @activityId and user_id = @userId";

                MySqlCommand cmd = new MySqlCommand(stm, conn);

                int categoryId = GetCategoryId(category);
                int activityId = GetActivityId(activity);

                cmd.Parameters.AddWithValue("@categoryId", categoryId);
                cmd.Parameters.AddWithValue("@activityId", activityId);
                cmd.Parameters.AddWithValue("@userId", userId);

                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    string date = Convert.ToDateTime(rdr["calendar_date"]).ToString("MM-dd-yyyy");

                    dates.Add(date);
                }

                conn.Close();

            }
            catch (MySqlException e)
            {
                string msg = e.ToString();
                Console.WriteLine(msg);
            }
            return dates;
        }

        public static double GetCategoryTotalTimeForDescription(string activity, string category, int userId)
        {
            // Create double to hold total time of specified activity in specified category
            double totalTime = 0;

            // Use try catch to connect to database, get and save data to dictionary
            try
            {
                MySqlConnection conn = new MySqlConnection(GetConnString());
                conn.Open();

                string stm = "Select SUM(activity_times.time_spent_on_activity) from activity_log join activity_times on activity_log.time_spent_on_activity = activity_times.activity_time_id where activity_log.category_description = @categoryId and activity_log.activity_description = @activityId and user_id = @userId";

                MySqlCommand cmd = new MySqlCommand(stm, conn);

                int categoryId = GetCategoryId(category);
                int activityId = GetActivityId(activity);

                cmd.Parameters.AddWithValue("@categoryId", categoryId);
                cmd.Parameters.AddWithValue("@activityId", activityId);
                cmd.Parameters.AddWithValue("@userId", userId);

                string timeCheck = cmd.ExecuteScalar().ToString();

                if (double.TryParse(timeCheck, out totalTime))
                {
                    totalTime = Convert.ToDouble(timeCheck);
                }
                else
                {
                    totalTime = 0;
                }

                conn.Close();

            }
            catch (MySqlException e)
            {
                string msg = e.ToString();
                Console.WriteLine(msg);
            }
            return totalTime;
        }

        public static List<string> GetCategoriesForDescription(string description, int userId)
        {
            // Create empty list to hold all categories used with given description
            List<string> categories = new List<string>();

            // Use try catch to connect to database, get and save data to dictionary
            try
            {
                MySqlConnection conn = new MySqlConnection(GetConnString());
                conn.Open();

                MySqlDataReader rdr = null;

                string stm = "Select activity_categories.category_description from activity_log join activity_categories on activity_log.category_description = activity_categories.activity_category_id where activity_description = @activityId and user_id = @userId";

                MySqlCommand cmd = new MySqlCommand(stm, conn);

                int activityId = GetActivityId(description);

                cmd.Parameters.AddWithValue("@activityId", activityId);
                cmd.Parameters.AddWithValue("@userId", userId);

                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    // Save each date and day into the list
                    string activity = rdr["category_description"].ToString();

                    if (categories.Contains(activity))
                    {
                        continue;
                    }
                    else
                    {
                        categories.Add(activity);
                    }
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

        public static List<string> GetActivityDatesForCategory(string activity, string category, int userId)
        {
            // Create empty list to hold the activity dates for the given activity and category
            List<string> dates = new List<string>();

            // Use try catch to connect to database, get and save data to dictionary
            try
            {
                MySqlConnection conn = new MySqlConnection(GetConnString());
                conn.Open();

                MySqlDataReader rdr = null;

                string stm = "Select tracked_calendar_dates.calendar_date from activity_log join tracked_calendar_dates on activity_log.calendar_date = tracked_calendar_dates.calendar_date_id where activity_log.category_description = @categoryId and activity_log.activity_description = @activityId and user_id = @userId";

                MySqlCommand cmd = new MySqlCommand(stm, conn);

                int categoryId = GetCategoryId(category);
                int activityId = GetActivityId(activity);

                cmd.Parameters.AddWithValue("@categoryId", categoryId);
                cmd.Parameters.AddWithValue("@activityId", activityId);
                cmd.Parameters.AddWithValue("@userId", userId);

                rdr = cmd.ExecuteReader();

                while(rdr.Read())
                {
                    string date = Convert.ToDateTime(rdr["calendar_date"]).ToString("MM-dd-yyyy");

                    dates.Add(date);
                }

                conn.Close();

            }
            catch (MySqlException e)
            {
                string msg = e.ToString();
                Console.WriteLine(msg);
            }
            return dates;
        }

        public static double GetActivityTotalTimeForCategory(string activity, string category, int userId)
        {
            // Create double to hold total time of specified activity in specified category
            double totalTime = 0;

            // Use try catch to connect to database, get and save data to dictionary
            try
            {
                MySqlConnection conn = new MySqlConnection(GetConnString());
                conn.Open();

                string stm = "Select SUM(activity_times.time_spent_on_activity) from activity_log join activity_times on activity_log.time_spent_on_activity = activity_times.activity_time_id where activity_log.category_description = @categoryId and activity_log.activity_description = @activityId and user_id = @userId";

                MySqlCommand cmd = new MySqlCommand(stm, conn);

                int categoryId = GetCategoryId(category);
                int activityId = GetActivityId(activity);

                cmd.Parameters.AddWithValue("@categoryId", categoryId);
                cmd.Parameters.AddWithValue("@activityId", activityId);
                cmd.Parameters.AddWithValue("@userId", userId);

                string timeCheck = cmd.ExecuteScalar().ToString().ToLower();

                if (double.TryParse(timeCheck, out totalTime))
                {
                    totalTime = Convert.ToDouble(timeCheck);
                }
                else
                {
                    totalTime = 0;
                }

                conn.Close();

            }
            catch (MySqlException e)
            {
                string msg = e.ToString();
                Console.WriteLine(msg);
            }
            return totalTime;
        }

        public static List<string> GetActivitiesForCategory(string category, int userId)
        {
            // Create empty list to hold all activities within given category
            List<string> activities = new List<string>();

            // Use try catch to connect to database, get and save data to dictionary
            try
            {
                MySqlConnection conn = new MySqlConnection(GetConnString());
                conn.Open();

                MySqlDataReader rdr = null;

                string stm = "Select activity_descriptions.activity_description from activity_log join activity_descriptions on activity_log.activity_description = activity_descriptions.activity_description_id where category_description = @categoryId and user_id = @userId";

                MySqlCommand cmd = new MySqlCommand(stm, conn);

                int categoryId = GetCategoryId(category);

                cmd.Parameters.AddWithValue("@categoryId", categoryId);
                cmd.Parameters.AddWithValue("@userId", userId);

                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    // Save each date and day into the list
                    string activity = rdr["activity_description"].ToString();

                    if(activities.Contains(activity))
                    {
                        continue;
                    }
                    else
                    {
                        activities.Add(activity);
                    }
                }

                conn.Close();

            }
            catch (MySqlException e)
            {
                string msg = e.ToString();
                Console.WriteLine(msg);
            }
            return activities;
        }

        public static string GetDateTrackedTimes(string date, int userId)
        {
            // Create empty double to hold sum of tracked times for the given date
            string trackedTimes = null;

            // Use try catch to connect to database, get and save data to double
            try
            {
                MySqlConnection conn = new MySqlConnection(GetConnString());
                conn.Open();

                string stm = "Select SUM(activity_times.time_spent_on_activity) from activity_log Join activity_times on activity_log.time_spent_on_activity = activity_times.activity_time_id Where activity_log.calendar_date = @dateId and user_id = @userId";

                MySqlCommand cmd = new MySqlCommand(stm, conn);

                //Get date id of date
                int dateId = GetDateId(date);

                cmd.Parameters.AddWithValue("@dateId", dateId);
                cmd.Parameters.AddWithValue("@userId", userId);

                MySqlDataReader rdr = cmd.ExecuteReader();

                while(rdr.Read())
                {
                    trackedTimes = rdr[0].ToString();
                }
                conn.Close();

            }
            catch (MySqlException e)
            {
                string msg = e.ToString();
                Console.WriteLine(msg);
            }
            return trackedTimes;
        }

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
            DateTime datetime = DateTime.ParseExact(date, "MM-dd-yyyy", null);
            string dateString = datetime.ToString("yyyy-MM-dd");
            int dateId = GetDateId(dateString);
            int categoryId = GetCategoryId(category);
            int activtyId = GetActivityId(activity);
            int timeId = GetTimeId(time);

            // Use try catch to connect to database, get and save data to list
            try
            {
                MySqlConnection conn = new MySqlConnection(GetConnString());

                MySqlDataAdapter adr = new MySqlDataAdapter();

                conn.Open();

                string stm = "Insert into activity_log " +
                    "(user_id, calendar_day, calendar_date, day_name, category_description, activity_description, time_spent_on_activity) values " +
                    "((select user_id from time_tracker_users where user_id = @userId), " +
                    "(select calendar_day_id from tracked_calendar_days where calendar_day_id = @dayId), " +
                    "(select calendar_date_id from tracked_calendar_dates where calendar_date_id = @dateId), " +
                    "(select day_id from days_of_week where day_id = @weekDayId), " +
                    "(select activity_category_id from activity_categories where activity_category_id = @categoryId), " +
                    "(select activity_description_id from activity_descriptions where activity_description_id = @activityId), " +
                    "(select activity_time_id from activity_times where activity_time_id = @timeId))";

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
                    id = Convert.ToInt32(idString);
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
                    id = Convert.ToInt32(idString);
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
                   id = Convert.ToInt32(idString);
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

            string dateString;

            DateTime datetime;
            DateTimeStyles styles = DateTimeStyles.AssumeLocal;

            if (DateTime.TryParseExact(date, "MM-dd-yyyy", null, styles, out datetime))
            {
                dateString = datetime.ToString("yyyy-MM-dd");
            }
            else
            {
                dateString = date;
            }

            // Use try catch to connect to database, get and save data to list
            try
            {
                MySqlConnection conn = new MySqlConnection(GetConnString());
                conn.Open();

                MySqlDataReader rdr = null;

                string getDateId = "Select calendar_date_id from tracked_calendar_dates where calendar_date = @date";

                MySqlCommand cmd = new MySqlCommand(getDateId, conn);

                cmd.Parameters.AddWithValue("@date", dateString);

                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    // Save each category name into the list
                    string idString = rdr["calendar_date_id"].ToString();
                    id = Convert.ToInt32(idString);
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
                    string timeCheck = rdr["time_spent_on_activity"].ToString().ToLower();
                    double time;

                    if (double.TryParse(timeCheck, out time))
                    {
                        time = Convert.ToDouble(timeCheck);
                    }
                    else
                    {
                        time = 0;
                    }

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

                string stm = "Select calendar_date from tracked_calendar_dates";

                MySqlCommand cmd = new MySqlCommand(stm, conn);
                rdr = cmd.ExecuteReader();

                int index = 1;
                while (rdr.Read())
                {
                    // Save each date and day into the list
                    string date = Convert.ToDateTime(rdr["calendar_date"]).ToString("MM-dd-yyyy");
                    int day = index;

                    datesAndDays.Add(date, day);

                    index++;
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
