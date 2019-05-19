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
            int userId = 0;

            // bool to determine if program is running
            bool running = true;

            while(running)
            {
                userId = Login(running);

                MainMenu(userId);
            }
            
        }

        public static void MainMenu(int userId)
        {
            // bool to determine if program is running
            bool running = true;

            while (running)
            {
                PrintCommands();
                string choice = Validation.GetString("Enter your choice: ").ToLower();

                switch (choice)
                {
                    case "1":
                    case "enter activity":
                        EnterActivity.ActivityMenu(userId);
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

        public static int Login(bool running)
        {
            int userId = 0;

            Console.Clear();

            Console.WriteLine("Welcome to the Time Tracker App!");
            Console.WriteLine("In order to get started you need to login.");

            while (userId < 1)
            {
                string choice = Validation.GetString("Enter the number 0 if you wish to skip logging in and exit the program: ");
                if(choice.Trim() == "0")
                {
                    running = false;

                    Console.WriteLine("Goodbye.");
                    Console.WriteLine("Press any key to exit...");
                    Console.ReadKey();
                }
                else
                {
                    string first = Validation.GetString("Please enter your first name: ");
                    string last = Validation.GetString("Please enter your last name: ");
                    string password = Validation.GetString("Please enter your password: ");

                    userId = DatabaseFunctions.Login(first, last, password);

                    if (userId == 0)
                    {
                        Console.WriteLine("Invalid login. Please try again.");
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();

                        Console.Clear();
                    }
                }
            }
            return userId;
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
