using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinaoleLau_TimeTrackerApp
{
    class ViewTrackedData
    {
        public static void MainMenu(int userId)
        {
            // keep track of if the user is on this menu
            bool running = true;

            while(running)
            {
                PrintCommands();
                string choice = Validation.GetString("Enter your choice: ").ToLower();

                switch(choice)
                {
                    case "1":
                    case "select by date":
                    case "date":
                        DateMenu(userId);
                        break;
                    case "2":
                    case "select by category":
                    case "category":
                        CategoryMenu(userId);
                        break;
                    case "3":
                    case "select by description":
                    case "description":
                        DescriptionMenu(userId);
                        break;
                    case "4":
                    case "back":
                        running = false;
                        Console.WriteLine("Returning to the main menu. Press any key to continue...");
                        Console.ReadKey();
                        break;
                    default:
                        Console.WriteLine("Invalid command. Press any key to continue...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private static void DescriptionMenu(int userId)
        {
            // bool to check if user is on this menu
            bool running = true;

            while (running)
            {
                List<string> descriptions = DatabaseFunctions.GetDescriptions();

                int count = descriptions.Count();
                int num = 1;

                while (num <= count + 1 && running)
                {
                    PrintDescriptionCommands();
                    string choice = Validation.GetString("Enter your choice: ").ToLower();

                    int choiceNum = 0;

                    int.TryParse(choice, out choiceNum);

                    if (num < count + 1 && ((choiceNum > 0 && choiceNum <= descriptions.Count()) || descriptions.Contains(choice, StringComparer.OrdinalIgnoreCase)))
                    {
                        // saves the users selected menu choice
                        if (int.TryParse(choice, out int test))
                        {
                            choice = descriptions[test - 1];
                        }
                        else
                        {
                            int index = descriptions.FindIndex(x => x.Equals(choice, StringComparison.OrdinalIgnoreCase));
                            choice = descriptions[index];
                        }

                        // show all categories, dates performed, total time per activity and total time per description
                        List<string> categories = DatabaseFunctions.GetCategoriesForDescription(choice, userId);

                        foreach (string category in categories)
                        {
                            double totalTime = DatabaseFunctions.GetCategoryTotalTimeForDescription(choice, category, userId);
                            List<string> dates = DatabaseFunctions.GetCategoryDatesForActivity(choice, category, userId);

                            Console.WriteLine("The activity {0} was performed in the category: {1} for a total time of {2} hours this month.",choice, category, totalTime);
                            
                            if (dates.Count < 1)
                            {
                                Console.WriteLine("There are no dates logged with this activity.");
                            }
                            else
                            {
                                Console.WriteLine("It was performed on the following dates:");
                                foreach (string date in dates)
                                {
                                    Console.WriteLine("- {0}", date);
                                }
                            }
                        }

                        if (categories.Count < 1)
                        {
                            Console.WriteLine("You don't have anything logged for this description yet.");
                        }

                        // bool to determine if user is on this sub menu
                        bool subMenu = true;

                        while (subMenu)
                        {

                            string subChoice = Validation.GetString("Enter 1 or back when you would like to return to the select description menu: ");

                            switch (subChoice)
                            {
                                case "1":
                                case "back":
                                    Console.WriteLine("Returning to the select by description menu. Press any key to continue...");
                                    Console.ReadKey();
                                    subMenu = false;
                                    break;
                                default:
                                    Console.WriteLine("Invalid command. Press any key to continue...");
                                    Console.ReadKey();
                                    break;
                            }
                        }
                    }
                    else if (choice == (count + 1).ToString() || choice == "back")
                    {
                        Console.WriteLine("Returning to the view tracked data menu.");
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        running = false;
                    }
                    else
                    {
                        Console.WriteLine("Invalid command. Press any key to continue...");
                        Console.ReadKey();
                    }
                    num++;
                }
            }

        }

        private static void CategoryMenu(int userId)
        {
            // bool to check if user is on this menu
            bool running = true;

            while (running)
            {
                List<string> categories = DatabaseFunctions.GetCategories();

                int count = categories.Count();
                int num = 1;

                while (num <= count + 1 && running)
                {
                    PrintCategoryCommands();
                    string choice = Validation.GetString("Enter your choice: ").ToLower();

                    int choiceNum = 0;

                    int.TryParse(choice, out choiceNum);

                    if (num < count + 1 && ((choiceNum > 0 && choiceNum <= categories.Count()) || categories.Contains(choice, StringComparer.OrdinalIgnoreCase)))
                    {
                        // saves the users selected menu choice
                        if (int.TryParse(choice, out int test))
                        {
                            choice = categories[test-1];
                        }
                        else
                        {
                            int index = categories.FindIndex(x => x.Equals(choice, StringComparison.OrdinalIgnoreCase));
                            choice = categories[index];
                        }

                        // show all activities, dates performed, total time per activity and total time per category
                        List<string> activities = DatabaseFunctions.GetActivitiesForCategory(choice, userId);

                        foreach(string activity in activities)
                        {
                            double totalTime = DatabaseFunctions.GetActivityTotalTimeForCategory(activity, choice, userId);
                            List<string> dates = DatabaseFunctions.GetActivityDatesForCategory(activity, choice, userId);

                            Console.WriteLine("The activity: {0} was performed for a total time of {1} hours this month.", activity, totalTime);
                            
                            if(dates.Count < 1)
                            {
                                Console.WriteLine("There are no dates logged with this activity.");
                            }
                            else
                            {
                                Console.WriteLine("It was performed on the following dates:");
                                foreach (string date in dates)
                                {
                                    Console.WriteLine("- {0}", date);
                                }
                            }
                        }

                        if(activities.Count < 1)
                        {
                            Console.WriteLine("You don't have any activities logged for this category yet.");
                        }

                        // bool to determine if user is on this sub menu
                        bool subMenu = true;

                        while (subMenu)
                        {
                            string subChoice = Validation.GetString("Enter 1 or back when you would like to return to the select category menu: ");

                            switch (subChoice)
                            {
                                case "1":
                                case "back":
                                    Console.WriteLine("Returning to the select by category menu. Press any key to continue...");
                                    Console.ReadKey();
                                    subMenu = false;
                                    break;
                                default:
                                    Console.WriteLine("Invalid command. Press any key to continue...");
                                    Console.ReadKey();
                                    break;
                            }
                        }
                    }
                    else if (choice == (count + 1).ToString() || choice == "back")
                    {
                        Console.WriteLine("Returning to the view tracked data menu.");
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        running = false;
                    }
                    else
                    {
                        Console.WriteLine("Invalid command. Press any key to continue...");
                        Console.ReadKey();
                    }
                    num++;
                }
            }

        }

        private static void DateMenu(int userId)
        {
            // bool to check if user is on this menu
            bool running = true;
            
            while(running)
            {

                List<string> dates = GetDates();

                int count = dates.Count();
                int num = 1;

                while (num <= count + 1 && running)
                {
                    PrintDateCommands();
                    string choice = Validation.GetString("Enter your choice: ").ToLower();

                    int choiceNum = 0;

                    int.TryParse(choice, out choiceNum);

                    if (num < count + 1 && ((choiceNum > 0 && choiceNum <= dates.Count()) || dates.Contains(choice)))
                    {
                        // saves the users selected menu choice
                        if(int.TryParse(choice, out int test))
                        {
                            choice = dates[test-1];
                        }
                        else
                        {
                            int index = dates.IndexOf(choice);
                            choice = dates[index];
                        }

                        // show tracked and untracked hours
                        string trackedTimesString = DatabaseFunctions.GetDateTrackedTimes(choice, userId);
                        double untrackedTimes = 0;

                        double trackedTimes;

                        if(double.TryParse(trackedTimesString, out trackedTimes))
                        {
                            untrackedTimes = 24 - trackedTimes;
                        }

                        if (trackedTimesString == null || trackedTimesString == "0")
                        {
                            Console.WriteLine("You have no tracked hours for this date.");
                            untrackedTimes = 24;
                        }
                        else
                        {
                            Console.WriteLine("You have a total of {0} tracked hours for the date: {1}.", trackedTimes, choice);
                        }
                        
                        if(untrackedTimes > 0)
                        {
                            Console.WriteLine("You have a total of {0} untracked hours for the date: {1}.", untrackedTimes, choice);
                        }
                        else
                        {
                            Console.WriteLine("You have no untracked hours for that date.");
                        }

                        // bool to determine if user is on this sub menu
                        bool subMenu = true;

                        while(subMenu && running)
                        {
                            // ask user if they want to enter activity for this day
                            Console.WriteLine("- [1] Enter activity for this day?");
                            Console.WriteLine("- [2] Back");
                            string subChoice = Validation.GetString("Enter your choice: ").ToLower();

                            switch (subChoice)
                            {
                                case "1":
                                case "enter activity for this day":
                                    EnterActivity.ActivityMenuWithDate(userId, choice);
                                    running = false;
                                    break;
                                case "2":
                                case "back":
                                    Console.WriteLine("Returning to the select by date menu. Press any key to continue...");
                                    Console.ReadKey();
                                    subMenu = false;
                                    break;
                                default:
                                    Console.WriteLine("Invalid command. Press any key to continue...");
                                    Console.ReadKey();
                                    break;
                            }
                        }
                    }
                    else if (choice == (count + 1).ToString() || choice == "back")
                    {
                        Console.WriteLine("Returning to the view tracked data menu.");
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        running = false;
                    }
                    else
                    {
                        Console.WriteLine("Invalid command. Press any key to continue...");
                        Console.ReadKey();
                    }
                    num++;
                }
            }
            
        }

        private static void PrintDescriptionCommands()
        {
            Console.Clear();

            List<string> descriptions = DatabaseFunctions.GetDescriptions();

            Console.WriteLine("Which description would you like to view?");

            // Create index variable to keep track of activity number
            int index = 1;

            // Loop through activities and print them as options to the screen
            foreach (string description in descriptions)
            {
                Console.WriteLine("- [{0}] {1}", index, description);
                index++;
            }

            Console.WriteLine("- [{0}] Back", index);
        }

        private static void PrintCategoryCommands()
        {
            Console.Clear();

            List<string> categories = DatabaseFunctions.GetCategories();

            Console.WriteLine("Which category would you like to view?");

            // Create index variable to keep track of activity number
            int index = 1;

            // Loop through categories and print them as options to the screen
            foreach (string category in categories)
            {
                Console.WriteLine("- [{0}] {1}", index, category);
                index++;
            }

            Console.WriteLine("- [{0}] Back", index);
        }

        private static void PrintDateCommands()
        {
            Console.Clear();

            List<string> dates = GetDates();

            Console.WriteLine("Which date would you like to view?");

            // Create index variable to keep track of activity number
            int index = 1;

            // Loop through activities and print them as options to the screen
            foreach (string date in dates)
            {
                Console.WriteLine("- [{0}] {1}", index, date);
                index++;
            }

            Console.WriteLine("- [{0}] Back", index);
        }

        private static List<string> GetDates()
        {
            Dictionary<string, int> datesAndDays = DatabaseFunctions.GetDatesAndDays();

            // store only the dates
            List<string> dates = new List<string>();
            foreach (KeyValuePair<string, int> date in datesAndDays)
            {
                dates.Add(date.Key);
            }

            return dates;
        }

        private static void PrintCommands()
        {
            Console.Clear();
            Console.WriteLine("[1] Select by Date");
            Console.WriteLine("[2] Select by Category");
            Console.WriteLine("[3] Select by Description");
            Console.WriteLine("[4] Back");
        }
    }
}
