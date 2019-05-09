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

        public string OutputJsonToFile()
        {
            // Save the output path in a string
            string outputPath = @"../../Output";
            // Create output directory if it doesn't exist
            Directory.CreateDirectory(outputPath);
            // Create path for file with filename and save to string
            string jsonFilePath = Path.Combine(outputPath, "KinaoleLau_ConvertedData.json");

            // Create file or overwrite file if exists at file path and write current date and time
            File.WriteAllText(jsonFilePath, "### " + DateTime.Now.ToString("yyyy'-'MM'-'dd' 'HH':'mm") + " ###");

            // Tell user the file was created, the file name, and the file location
            Console.WriteLine("The file was created!");
            Console.WriteLine($"File Name: {Path.GetFileName(jsonFilePath)}");
            Console.WriteLine($"File Saved To: {Path.GetDirectoryName(jsonFilePath)}");

            using (StreamWriter sw = File.AppendText(jsonFilePath))
            {
                
            }
        }

        private string SqlToJson(Dictionary<int ,Dictionary<string, string>> dbTable)
        {
            string jsonString = "[\n";
            
            foreach (int key in dbTable.Keys)
            {

            }
        }
    }
}
