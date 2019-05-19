using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinaoleLau_TimeTrackerApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // bool to determine if program is running
            bool running = true;

            while(running)
            {
                PrintCommands();
                string choice = Validation.GetString("Enter your choice: ").ToLower();

                switch(choice)
                {
                    case "1":
                    case "enter activity":

                        break;

                    case "2":
                    case "view tracked data":
                    case "view data":

                        break;

                    case "3":
                    case "run calculations":

                        break;

                    case "4":
                    case "exit":
                    case "quit":
                        running = false;
                        Console.WriteLine("You are now exiting the program.");
                        Console.WriteLine("Goodbye!");
                        Console.WriteLine("Press any key to exit...");
                        Console.ReadKey();
                        break;

                    default:
                        Console.WriteLine("Invalid command. Press any key to continue...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        public static void PrintCommands()
        {
            // Clear the console
            Console.Clear();

            Console.WriteLine("[1] Enter Activity");
            Console.WriteLine("[2] View Tracked Data");
            Console.WriteLine("[3] Run Calculations");
            Console.WriteLine("[4] Exit");
        }
    }
}
