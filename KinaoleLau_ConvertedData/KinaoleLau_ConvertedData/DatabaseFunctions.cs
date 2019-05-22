using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using MySql.Data.MySqlClient;
using System.Data;

namespace KinaoleLau_ConvertedData
{
    class DatabaseFunctions
    {
        //Database Location
        //string cs = @"server= 127.0.0.1;userid=root;password=root;database=SampleRestaurantDatabase;port=8889";
        //Output Location
        //string _directory = @"../../output/";

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
            string conString = $"Server={ip};userid=root;password=root;database=samplerestaurantdatabase;port=8889";

            return conString;
        }

        public static Dictionary<string, string> GetRestaurantAverageReviewScore()
        {
            //Dictionary to store the restaurant name and average review score
            Dictionary<string, string> avgReviewScores = new Dictionary<string, string>();

            // Use try catch to connect to database, get data into datatable and save data to dictionary
            try
            {
                MySqlConnection conn = new MySqlConnection(GetConnString());
                conn.Open();

                MySqlDataReader rdr = null;

                string stm = "Select RestaurantName, ReviewScore from RestaurantReviews join RestaurantProfiles on RestaurantReviews.RestaurantId = RestaurantProfiles.id";

                MySqlCommand cmd = new MySqlCommand(stm, conn);
                rdr = cmd.ExecuteReader();
                

                while (rdr.Read())
                {
                    // Save name to name variable and review score to review score variable
                    string name = rdr["RestaurantName"].ToString();
                    string reviewScore = rdr["ReviewScore"].ToString().Trim();
                    int reviewScoreInt = 0;

                    if(string.IsNullOrEmpty(reviewScore) || reviewScore.ToLower() == "null")
                    {
                        reviewScore = "null";
                    }
                    else
                    {
                        reviewScoreInt = int.Parse(reviewScore);
                    }

                    // Check if dictionary contains restaurant name already, if so add reviewScore to existing reviewScore else create new key value pair
                    if(avgReviewScores.ContainsKey(name))
                    {
                        // if reviewScore is null
                        if(avgReviewScores[name] == "null")
                        {
                            if(reviewScore == "null")
                            {
                                //Do nothing
                            }
                            else
                            {
                                //Change the reviewScore to the value
                                avgReviewScores[name] = reviewScore;
                            }
                        }
                        // else reviewScore contains a numeric value
                        else
                        {
                            if(reviewScore == "null")
                            {
                                // Do nothing
                            }
                            else
                            {
                                // Add the new review score value to the current review score value
                                int currentReviewScore = int.Parse(avgReviewScores[name]);
                                currentReviewScore += reviewScoreInt;

                                // Add the new review score back in to the dictionary
                                avgReviewScores[name] = currentReviewScore.ToString();
                            }
                            
                        }
                    }
                    else
                    {
                        // Dictionary does not contain restaurant name already so add name and value
                        avgReviewScores.Add(name, reviewScore);
                    }
                }

                List<string> names = new List<string>(avgReviewScores.Keys);

                //Average out the review scores
                foreach(string name in names)
                {
                    if(avgReviewScores[name] == "null")
                    {
                        //Do nothing
                        continue;
                    }
                    else
                    {
                        int reviewScoreSum = int.Parse(avgReviewScores[name]);
                        //Get the average of 10
                        double reviewScoreAvgof10 = (reviewScoreSum / 100) / 10;
                        //Round it to a whole number
                        int avgOf10Rounded = (int)Math.Round(reviewScoreAvgof10);
                        //Save avg value back into dictionary
                        avgReviewScores[name] = avgOf10Rounded.ToString();
                        continue;
                    }
                }

                conn.Close();

            }
            catch (MySqlException e)
            {
                string msg = e.ToString();
                Console.WriteLine(msg);
            }

            return avgReviewScores;
        }

        public static Dictionary<string, decimal?> GetRestaurantRatings()
        {
            //Empty dictionary to hold the index/id, restaurant name, and the rating
            Dictionary<string, decimal?> namesAndRatings = new Dictionary<string, decimal?>();

            // Use try catch to connect to database, get data into datatable and save data to dictionary
            try
            {
                MySqlConnection conn = new MySqlConnection(GetConnString());
                conn.Open();

                MySqlDataReader rdr = null;

                string stm = "Select RestaurantName, OverallRating from RestaurantProfiles";

                MySqlCommand cmd = new MySqlCommand(stm, conn);
                rdr = cmd.ExecuteReader();

                int index = 0;
                while(rdr.Read())
                {
                    // Save row data into variables
                    // Parse rating data into an int
                    // Add name and rating of each row to the dictionary
                    string name = rdr["RestaurantName"].ToString();
                    string ratingString = rdr["OverallRating"].ToString();

                    decimal? rating;

                    // Check that the rating value isn't null
                    if(string.IsNullOrWhiteSpace(ratingString))
                    {
                        rating = null;
                    }
                    else
                    {
                        rating = Convert.ToDecimal(ratingString);
                    }

                    namesAndRatings.Add(name, rating);
                    index++;
                }

                conn.Close();

            }
            catch (MySqlException e)
            {
                string msg = e.ToString();
                Console.WriteLine(msg);
            }

            return namesAndRatings;
        }

        public static Dictionary<int, Dictionary<string, string>> GetRestaurantProfilesDBInfo()
        {
            //Empty dictionary to hold the row number as the key and the column name and value for that row as the value
            // Values will be converted back later
            Dictionary<int, Dictionary<string, string>> DBInfo = new Dictionary<int, Dictionary<string, string>>();
            
            // Use try catch to connect to database, get data into datatable and save data to dictionary
            try
            {
                MySqlConnection conn = new MySqlConnection();
                conn.ConnectionString = GetConnString();
                conn.Open();
                
                string stm = "Select * from RestaurantProfiles";

                MySqlDataAdapter adapter = new MySqlDataAdapter(stm, conn);
                DataTable dbInfo = new DataTable();

                adapter.SelectCommand.CommandType = CommandType.Text;
                adapter.Fill(dbInfo);

                DataColumnCollection dbCols = dbInfo.Columns;

                //Index to keep track of row number
                int index = 0;

                foreach (DataRow row in dbInfo.Rows)
                {
                    // Empty dictionary to contain the column names and values for each row
                    Dictionary<string, string> rowValues = new Dictionary<string, string>();

                    foreach (DataColumn col in dbCols)
                    {
                        //Get the column name and value for each column in the row
                        string key = col.ColumnName;
                        string value = row[col].ToString();

                        // Add the values to the dictionary containing the row data
                        rowValues.Add(key, value);
                    }

                    // Add the row number, the index, and the rowValues dictionary to the dictionary containing the entire databases's info
                    DBInfo.Add(index, rowValues);

                    index++;
                }
                
                conn.Close();

            }
            catch (MySqlException e)
            {
                string msg = e.ToString();
                Console.WriteLine(msg);
            }

            return DBInfo;
        }

        public static Dictionary<int, Dictionary<string, string>> GetRestaurantReviewersDBInfo()
        {
            //Empty dictionary to hold the row number as the key and the column name and value for that row as the value
            // Values will be converted back later
            Dictionary<int, Dictionary<string, string>> DBInfo = new Dictionary<int, Dictionary<string, string>>();

            // Use try catch to connect to database, get data into datatable and save data to dictionary
            try
            {
                MySqlConnection conn = new MySqlConnection();
                conn.ConnectionString = GetConnString();
                conn.Open();

                string stm = "Select * from RestaurantReviewers";

                MySqlDataAdapter adapter = new MySqlDataAdapter(stm, conn);
                DataTable dbInfo = new DataTable();

                adapter.SelectCommand.CommandType = CommandType.Text;
                adapter.Fill(dbInfo);

                DataColumnCollection dbCols = dbInfo.Columns;

                //Index to keep track of row number
                int index = 0;

                foreach (DataRow row in dbInfo.Rows)
                {
                    // Empty dictionary to contain the column names and values for each row
                    Dictionary<string, string> rowValues = new Dictionary<string, string>();

                    foreach (DataColumn col in dbCols)
                    {
                        //Get the column name and value for each column in the row
                        string key = col.ColumnName;
                        string value = row[col].ToString();

                        // Add the values to the dictionary containing the row data
                        rowValues.Add(key, value);
                    }

                    // Add the row number, the index, and the rowValues dictionary to the dictionary containing the entire databases's info
                    DBInfo.Add(index, rowValues);

                    index++;
                }

                conn.Close();

            }
            catch (MySqlException e)
            {
                string msg = e.ToString();
                Console.WriteLine(msg);
            }

            return DBInfo;
        }


    }
}
