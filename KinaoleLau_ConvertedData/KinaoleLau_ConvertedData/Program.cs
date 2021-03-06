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

            // Set program running variable to true as long as user is using the program
            bool running = true;

            while(running)
            {
                PrintMenu();

                // Gets input and validates the input isn't null or whitespace then converts the input to lowercase
                string choice = Validation.GetString("Choose an option: ").ToLower();

                switch(choice)
                {
                    case "1":
                    case "convert the restaurant reviews database from sql to json":
                    case "convert":
                        DBToFile.OutputJsonToFile();
                        Console.WriteLine("The database has been converted to json and written to the file.");
                        Pause();
                        break;

                    case "2":
                    case "showcase our 5 star rating system":
                    case "showcase rating system":
                        RatingSystem.RatingProgram();
                        break;

                    case "3":
                    case "showcase our animated bar graph review system":
                    case "showcase review system":
                        ReviewSystem.MainMenu();
                        break;

                    case "4":
                    case "play a card game":
                    case "play game":
                        CardGame.Menu();
                        break;

                    case "5":
                    case "exit":
                    case "quit":
                        running = false;
                        // Bid user goodbye and wait for user input to exit
                        Console.WriteLine("Goodbye Admin.");
                        Console.WriteLine("Press any key to exit...");
                        Console.ReadKey();
                        break;

                    default:
                        Console.WriteLine("That is an invalid command.");
                        Pause();
                        break;
                }
            }
        }

        public static void Pause()
        {
            // Wait for user input to return to the menu
            Console.WriteLine("Press any key to return to the menu...");
            Console.ReadKey();
        }

        public static void PrintMenu()
        {
            // Clear console for neat look
            Console.Clear();

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
