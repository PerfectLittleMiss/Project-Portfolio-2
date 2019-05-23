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
                        LoginUser();
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
            Console.Clear();

            Console.WriteLine("[1] Login");
            Console.WriteLine("[2] Create a new account");
            Console.WriteLine("[3] Exit");
        }

        private static void CreateUser()
        {
            // bool to determine if user is still here
            bool running = true;
            while(running)
            {
                Console.WriteLine("You have chosen to create a new user.");
                Console.WriteLine("If you wish to go back to the main menu type 0 and the return key.");
                string name = Validation.GetString("Enter your name: ");

                if(name == "0")
                {
                    running = false;
                    Console.WriteLine("Returning to the main menu. Press any key to continue...");
                    Console.ReadKey();
                }
                else
                {
                    string username = Validation.GetString("Enter a username (0 and return key if you want to go back to the main menu): ");

                    if(username == "0")
                    {
                        running = false;
                        Console.WriteLine("Returning to the main menu. Press any key to continue...");
                        Console.ReadKey();
                    }
                    else
                    {
                        string checkUsername = DatabaseFunctions.CheckUsername(username);

                        if(checkUsername == "false" || string.IsNullOrWhiteSpace(checkUsername))
                        {
                            // the username does not already exist so ask for a password
                            string password = Validation.GetString("Enter a password (0 and return key to go back to the main menu): ");

                            if(password == "0")
                            {
                                running = false;
                                Console.WriteLine("Returning to the main menu. Press any key to continue...");
                                Console.ReadKey();
                            }
                            else
                            {
                                // add user to the database then take user to the profile page
                                DatabaseFunctions.AddUser(username, password, name);
                            }
                        }
                        else
                        {
                            // the username already exists
                            Console.WriteLine("That username already exists. To login go back to the main menu.");
                            Console.WriteLine("Press any key to continue...");
                            Console.ReadKey();
                        }
                    }
                }
            }
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
                string username = Validation.GetString("Enter your username: ").ToLower();

                if(username == "0")
                {
                    running = false;
                    Console.WriteLine("Returning you to the welcome screen. Press any key to continue...");
                    Console.ReadKey();
                }
                else
                {
                    string password = DatabaseFunctions.CheckUsername(username);

                    if(password == "false" || string.IsNullOrWhiteSpace(password))
                    {
                        Console.WriteLine("That username does not exist. If you'd like to create an account please go back to the main menu.");
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                    }
                    else
                    {
                        string name = DatabaseFunctions.CheckUsernameAndPassword(username, password);

                        if(name == "false" || string.IsNullOrWhiteSpace(name))
                        {
                            Console.WriteLine("Invalid login. Please try again.");
                            Console.WriteLine("Press any key to continue...");
                            Console.ReadKey();
                        }
                        else
                        {
                            //Login was successful so take the user to the user profile page
                        }
                    }
                }
            }
        }
    }
}
