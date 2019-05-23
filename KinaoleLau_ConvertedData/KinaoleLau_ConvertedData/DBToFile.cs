using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace KinaoleLau_ConvertedData
{
    class DBToFile
    {
        //Database Location
        //string cs = @"server= 127.0.0.1;userid=root;password=root;database=SampleRestaurantDatabase;port=8889";
        //Output Location
        //string _directory = @"../../output/";

        public static void OutputJsonToFile()
        {
            // Save the output path in a string
            string outputPath = @"../../Output";
            // Create output directory if it doesn't exist
            Directory.CreateDirectory(outputPath);
            // Create path for file with filename and save to string
            string jsonFilePath = Path.Combine(outputPath, "KinaoleLau_ConvertedData-" + DateTime.Now.ToString("yyyy'-'MM'-'dd' 'HH'-'mm") + ".json");

            // Create file or overwrite file if exists at file path
            File.WriteAllText(jsonFilePath, "");

            // Tell user the file was created, the file name, and the file location
            Console.WriteLine("The file was created!");
            Console.WriteLine($"File Name: {Path.GetFileName(jsonFilePath)}");
            Console.WriteLine($"File Saved To: {Path.GetDirectoryName(jsonFilePath)}");

            // Create a class instnace to access the non static function sqltojson
            DBToFile instance = new DBToFile();

            // Save the output of the sqltojson function for each db table into a string list
            List<string> tableOneInfo = instance.SqlToJson(DatabaseFunctions.GetRestaurantProfilesDBInfo());
            List<string> tableTwoInfo = instance.SqlToJson(DatabaseFunctions.GetRestaurantReviewersDBInfo());
            List<string> tableThreeInfo = instance.SqlToJson(DatabaseFunctions.GetRestaurantReviewsDBInfo());

            // Write the string lists to the file
            using (StreamWriter sw = File.AppendText(jsonFilePath))
            {
                // Create large array to hold table arrays
                sw.WriteLine("[");

                foreach(string line in tableOneInfo)
                {
                    sw.WriteLine(line);
                }
                // Include comma to separate arrays within array
                sw.Write(",");

                foreach (string line in tableTwoInfo)
                {
                    sw.WriteLine(line);
                }
                // Include comma to separate arrays within array
                sw.Write(",");

                foreach (string line in tableThreeInfo)
                {
                    sw.WriteLine(line);
                }

                sw.WriteLine("]");
            }
        }

        private List<string> SqlToJson(Dictionary<int ,Dictionary<string, string>> dbTable)
        {
            // Start of string list that will contain the json for one table
            // [ indicates beginning of an array and \n indicates new line
            List<string> jsonString = new List<string>();
            jsonString.Add("[");

            // Loop through rows in database table
            foreach (int key in dbTable.Keys)
            {
                // { indicates start of json object
                jsonString.Add("{");

                // Saves the current database containing the row data in the table to a new dictionary
                Dictionary<string, string> colNamesAndValues = dbTable[key];

                // Create index to keep track of where the end of the dictionary is
                int index = 1;

                // Loop through the dictionary containg the row data
                foreach (string colName in colNamesAndValues.Keys)
                {
                    // Create a string to hold field name and value in one line to add to string list
                    // Add an indent before each field for formatting
                    string colString = "\t";

                    // Check if value is an int, a decimal, or a string and convert to correct datatype
                    string colValue = colNamesAndValues[colName];
                    int colValueInt;
                    decimal colValueDecimal;

                    if (int.TryParse(colValue, out colValueInt))
                    {
                        colString += $"\"{colName}\":{colValueInt}";
                    }
                    else if (decimal.TryParse(colValue, out colValueDecimal))
                    {
                        colString += $"\"{colName}\":{colValueDecimal}";
                    }
                    else
                    {
                        colString += $"\"{colName}\":\"{colValue}\"";
                    }

                    // If the current item index is less than the count it is not the last item so add a comma
                    if (index < colNamesAndValues.Count)
                    {
                        colString += ",";
                    }
                    // The current item is the last item so don't add a comma

                    // Add the string containing the current field name and value to the list
                    jsonString.Add(colString);

                    index++;
                }

                // Check if this row is the last or not
                if (key + 1 < dbTable.Count)
                {
                    // This is not the last row so add a comma
                    jsonString.Add("},");
                }
                else
                {
                    // This is the last row so don't add a comma
                    jsonString.Add("}");
                }
            }

            // This is the end of the data in this table so close the array
            jsonString.Add("]");

            // Return the entire string
            return jsonString;
        }
    }
}
