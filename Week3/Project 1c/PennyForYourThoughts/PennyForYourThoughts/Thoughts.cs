using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PennyForYourThoughts
{
    class Thoughts
    {
        public static void Menu(int userId)
        {
            // bool to determine if user is on page
            bool running = true;
            while(running)
            {
                PrintCommands();
                string choice = Validation.GetString("Enter your choice: ").ToLower();

                switch(choice)
                {
                    case "1":
                    case "search thoughts":

                        break;
                    case "2":
                    case "select thought":

                        break;
                    case "3":
                    case "create thought":

                        break;
                    case "4":
                    case "view profile":
                        running = false;
                        Console.WriteLine("Returning to your profile. Press any key to continue...");
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

            Console.WriteLine("Here lies your thoughts:");
            Console.WriteLine("[1] Search Thoughts");
            Console.WriteLine("[2] Select Thought");
            Console.WriteLine("[3] Create Thought");
            Console.WriteLine("[4] View Profile");
        }
    }
}
