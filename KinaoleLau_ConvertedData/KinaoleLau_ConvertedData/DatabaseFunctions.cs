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

        private string GetConnString()
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

        public Dictionary<int, Dictionary<string, string>> GetRestaurantProfilesDBInfo()
        {
            //Empty dictionary to hold the row number as the key and the column name and value for that row as the value
            // Values will be converted back later
            Dictionary<int, Dictionary<string, string>> DBInfo = new Dictionary<int, Dictionary<string, string>>();
            
            // Use try catch to connect to database, get data into datatable and save data to dictionary
            try
            {
                MySqlConnection conn = null;
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
    }
}
