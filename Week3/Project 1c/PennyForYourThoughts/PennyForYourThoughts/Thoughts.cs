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
                    case "search":
                        SearchThoughts(userId);
                        break;
                    case "2":
                    case "select thought":
                    case "select":
                        SelectThought(DatabaseFunctions.GetAllThoughts(userId), userId);
                        break;
                    case "3":
                    case "create thought":
                    case "create":
                        Create(userId);
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

        private static void SearchThoughts(int userId)
        {
            string search = Validation.GetString("What would you like to search for: ");

            Dictionary<int, string> idAndPreview = DatabaseFunctions.SearchThoughts(userId, search.Trim());

            Console.WriteLine("Here's what we found:");

            foreach(KeyValuePair<int, string> idPreview in idAndPreview)
            {
                Console.WriteLine("ThoughtId: {0} Preview: {1}", idPreview.Key, idPreview.Value);
            }

            if(idAndPreview.Count < 1)
            {
                Console.WriteLine("Nothing found.");
                Console.WriteLine("Press any key to return to the thoughts menu...");
                Console.ReadKey();
            }
            else
            {
                // bool to check if user is here
                bool running = true;
                while(running)
                {
                    Console.WriteLine("[1] Select a thought");
                    Console.WriteLine("[2] Back");

                    string choice = Validation.GetString("Enter your choice: ").ToLower();

                    switch (choice)
                    {
                        case "1":
                        case "select a thought":
                            SelectThought(idAndPreview, userId);
                            running = false;
                            break;
                        case "2":
                        case "back":
                            running = false;
                            Console.WriteLine("Returning to the thoughts menu. Press any key to continue...");
                            Console.ReadKey();
                            break;
                        default:
                            Console.WriteLine("Invalid command. Press any key to continue...");
                            Console.ReadKey();
                            break;
                    }
                }
            }
        }

        private static void SelectThought(Dictionary<int, string> thoughtList, int userId)
        {
            bool running = true;
            while(running)
            {
                Console.Clear();
                Console.WriteLine("Select a thought by its thought id:");
                Console.WriteLine("Type 0 and enter to go back.");

                foreach(KeyValuePair<int, string> thought in thoughtList)
                {
                    Console.WriteLine("ThoughtId: {0} Preview: {1}", thought.Key, thought.Value);
                }

                int choice = Validation.GetInt("Enter the thought id of your chosen thought (0 to go back): ");

                if(thoughtList.ContainsKey(choice))
                {
                    // display the thought
                    string content = DatabaseFunctions.GetThoughtContent(choice);

                    Console.WriteLine("ThoughtId: {0} Content:\n{1}\n", choice, content);

                    // ask user if they want to edit or delete the thought
                    EditDeleteMenu(userId, choice);
                    running = false;
                }
                else if (choice == 0)
                {
                    running = false;
                    Console.WriteLine("Returning to the thoughts menu. Press any key to continue...");
                    Console.ReadKey();
                }
                else
                {
                    // tell the user the id was invalid
                    Console.WriteLine("Invalid thought id. Press any key to continue...");
                    Console.ReadKey();
                }
            }
        }

        private static void EditDeleteMenu(int userId, int thoughtId)
        {
            bool running = true;
            while(running)
            {
                Console.WriteLine("Would you like to edit or delete this thought?");
                Console.WriteLine("[1] Edit");
                Console.WriteLine("[2] Delete");
                Console.WriteLine("[3] Back");

                string choice = Validation.GetString("Enter your choice: ").ToLower();

                switch(choice)
                {
                    case "1":
                    case "edit":
                        Edit(thoughtId);
                        running = false;
                        break;
                    case "2":
                    case "delete":
                        Console.WriteLine("Are you sure you want to delete this thought?");
                        string answer = Validation.GetString("Enter yes or no: ").ToLower();
                        if(answer == "yes" || answer == "y")
                        {
                            Console.WriteLine("Deleting...");
                            DatabaseFunctions.DeleteThought(thoughtId);
                            running = false;
                        }
                        else if (answer == "no" || answer == "n")
                        {
                            Console.WriteLine("The thought was NOT deleted. Press any key to continue...");
                            Console.ReadKey();
                        }
                        break;
                    case "3":
                    case "back":
                        running = false;
                        Console.WriteLine("Returning to the thoughts menu. Press any key to continue...");
                        Console.ReadKey();
                        break;
                    default:
                        Console.WriteLine("Invalid command. Press any key to continue...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private static void Edit(int thoughtId)
        {
            Console.WriteLine("Current thought content:\n{0}", DatabaseFunctions.GetThoughtContent(thoughtId));
            string newContent = Validation.GetString("\nEnter the new or edited content here (Press enter when done):\n");
            string preview = newContent.Substring(0, 20);
            string now = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            DatabaseFunctions.UpdateThought(thoughtId, newContent, preview, now);
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

        private static void Create(int userId)
        {
            Console.WriteLine("You have chosen to create a new thought.");
            string content = Validation.GetString("Enter your thought:\n");
            string preview = content.Substring(0, 20);
            string now = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            DatabaseFunctions.CreateThought(content, preview, now);
        }
    }
}
