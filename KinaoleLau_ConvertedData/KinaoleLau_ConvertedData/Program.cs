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

            bool running = true;

            while(running)
            {
                PrintMenu();

                string choice = Validation.GetString("Choose an option: ").ToLower();

                switch(choice)
                {
                    case "1":
                    case "convert the restaurant reviews database from sql to json":
                    case "convert":

                        break;

                    case "2":
                    case "showcase our 5 star rating system":
                    case "showcase rating system":
                        Console.WriteLine("Option not available yet.");

                        break;

                    case "3":
                    case "showcase our animated bar graph review system":
                    case "showcase review system":
                        Console.WriteLine("Option not available yet.");

                        break;

                    case "4":
                    case "play a card game":
                    case "play game":
                        Console.WriteLine("Option not available yet.");

                        break;

                    case "5":
                    case "exit":
                    case "quit":
                        running = false;
                        break;
                }

                Console.WriteLine("Press any key to return to the menu...");
                Console.Read();
            }

            Console.WriteLine("Goodbye Admin.");
            Console.WriteLine("Press any key to exit...");
            Console.Read();

        }

        public static void PrintMenu()
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
