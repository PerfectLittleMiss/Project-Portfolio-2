using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PennyForYourThoughts
{
    class Login
    {
        public static void LoginMenu()
        {
            Console.WriteLine("Welcome to Penny For Your Thoughts!");
            Console.WriteLine("Please login or create an account to get started.");

            // bool to determine if user is on this menu
            bool running = true;

            while(running)
            {
                PrintCommands();
                string choice = Validation.GetString("Enter your choice: ").ToLower();

                switch(choice)
                {
                    case "1":
                    case "login":

                        break;

                    case "2":
                    case "create a new account":

                        break;

                    case "3":
                    case "exit":
                    case "quit":
                        running = false;
                        Console.WriteLine("It's sad to see you go.");
                        Console.WriteLine("Byee~");
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

        private static void PrintCommands()
        {
            Console.WriteLine("[1] Login");
            Console.WriteLine("[2] Create a new account");
            Console.WriteLine("[3] Exit");
        }

        private static void LoginUser()
        {
            // bool to determine if user is still here
            bool running = true;
            while(running)
            {
                Console.WriteLine("To login we will need your username and password.");
                Console.WriteLine("Enter 0 and the return key to go back");
                Console.WriteLine("Otherwise please fill in your username and password");
                string choice = Validation.GetString("Enter your username: ").ToLower();

                if(choice == "0")
                {
                    running = false;
                    Console.WriteLine("Returning you to the welcome screen. Press any key to continue...");
                    Console.ReadKey();
                }
                else
                {

                }
            }
        }
    }
}
