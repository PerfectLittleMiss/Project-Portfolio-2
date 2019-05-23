using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PennyForYourThoughts
{
    class UserProfile
    {
        public static void ProfileMenu(string name, string username)
        {
            //bool to determine if user is here
            bool running = true;

            while(running)
            {
                PrintCommands(name, username);
                string choice = Validation.GetString("Enter your choice: ").ToLower();

                switch(choice)
                {
                    case "1":
                    case "view/edit thoughts":

                        break;

                    case "2":
                    case "log out":
                        running = false;
                        Console.WriteLine("Logging you out. Press any key to continue...");
                        Console.ReadKey();
                        break;

                    default:
                        Console.WriteLine("Invalid command. Press any key to continue...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private static void PrintCommands(string name, string username)
        {
            Console.Clear();

            Console.WriteLine("Hello {0}, this is your user profile page.", username);
            Console.WriteLine("Name: {0}", name);
            Console.WriteLine("Username: {0}", username);
            int count = DatabaseFunctions.GetThoughtCount(username);
            Console.WriteLine("Thought Count: {0}", count);
            Console.WriteLine("\nWhat would you like to do now?");
            Console.WriteLine("[1] View/Edit Thoughts");
            Console.WriteLine("[2] Log Out");
        }
    }
}
