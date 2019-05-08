using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinaoleLau_ConvertedData
{
    class Program
    {
        static void Main(string[] args)
        {

            //Database Location
            //string cs = @"server= 127.0.0.1;userid=root;password=root;database=SampleRestaurantDatabase;port=8889";
            //Output Location
            //string _directory = @"../../output/";


        }

        public void PrintMenu()
        {
            Console.WriteLine("Hello Admin, What Would You Like To Do Today?");
            // List top menu options only
            Console.WriteLine("\n[1] Convert the restaurant reviews database from SQL to JSON");
            Console.WriteLine("[2] Showcase our 5 star rating system");
            Console.WriteLine("[3] Showcase our animated bar graph review system");
            Console.WriteLine("[4] Play a card game");
            Console.WriteLine("[5] Exit");
        }
    }
}
