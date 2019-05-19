using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinaoleLau_TimeTrackerApp
{
    class EnterActivity
    {
        public static void ActivityMenu(int userId)
        {
            // create bool to determine if user is still on menu
            bool running = true;

            while(running)
            {
                // Get the list of categories
                List<string> categories = DatabaseFunctions.GetCategories();
                // Get the count of the list of categories
                int categoryCount = categories.Count();

                // Get list of activities
                List<string> activities = DatabaseFunctions.GetDescriptions();
                // Get activities count
                int activityCount = activities.Count();

                // Get dates
                Dictionary<string, int> datesAndDays = DatabaseFunctions.GetDatesAndDays();
                // Get dates count
                int datesCount = datesAndDays.Count();

                // Get times
                List<double> times = DatabaseFunctions.GetTimes();
                // Get times count
                int timesCount = times.Count();

                // Print the menu for the category submenu
                PrintCommands();
                // Get the users selected category
                string category = StringListSubMenu(categoryCount, categories);
                // If false user chose to go back so tell program to exit this menu
                if(category == "false")
                {
                    running = false;
                }
                else
                {
                    // bool to determine if user wants to be on activity menu
                    bool activityMenu = true;

                    while(activityMenu && running)
                    {
                        // user chose a category so display activity menu
                        PrintActivityCommands();
                        // get users choice
                        string activity = StringListSubMenu(activityCount, activities);
                        // if false user chose to go back so break to go back to the main while loop
                        if (activity == "false")
                        {
                            break;
                        }
                        else
                        {
                            while(activityMenu && running)
                            {
                                // user chose an activity so display date menu
                                PrintDateCommands();
                                // get users choice
                                string date = DateSubMenu(datesAndDays, datesCount);
                                // if false user chose to go back to the activity menu so break to go back to the activity while loop
                                if(date == "false")
                                {
                                    break;
                                }
                                else
                                {
                                    while(activityMenu && running)
                                    {
                                        // set the day automatically based on the date chosen
                                        int day = datesAndDays[date];

                                        // user chose a date so display the time menu
                                        PrintTimeCommands();
                                        // get users choice
                                        double time = TimeSubMenu(timesCount, times);
                                        // if 0 user chose to go back to the date menu so break to go back to the date while loop
                                        if (time == 0)
                                        {
                                            break;
                                        }
                                        else
                                        {
                                            //user chose a time so display the days of the week
                                            PrintWeekCommands();
                                            // get users choice
                                            int weekDay = WeekSubMenu();
                                            // if 8 user chose to go back to the time menu so continue to restart the time while loop
                                            if(weekDay == 8)
                                            {
                                                continue;
                                            }
                                            else
                                            {
                                                // save user feedback to the database activity log
                                                DatabaseFunctions.EnterActivity(userId, day, date, weekDay, category, activity, time);
                                                Console.WriteLine("Press any key to continue...");
                                                Console.ReadKey();

                                                // give user option to return to menu or enter another activity
                                                int choice = MainOrEnterAgain();
                                                if(choice == 1)
                                                {
                                                    // user wants to enter another activity so take user back to top while loop
                                                    activityMenu = false;
                                                    Console.WriteLine("Redirecting you to the enter activity menu. Press any key to continue...");
                                                    Console.ReadKey();
                                                }
                                                else if (choice == 2)
                                                {
                                                    // user wants to return to main menu so break out of all while loops
                                                    running = false;
                                                    Console.WriteLine("Redirecting you to the main menu. Press any key to continue...");
                                                    Console.ReadKey();
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

            }
        }

        private static int MainOrEnterAgain()
        {
            int choice = 0;

            while(choice < 1)
            {
                // Clear the console
                Console.Clear();

                Console.WriteLine("[1] Enter Another Activity");
                Console.WriteLine("[2] Back to Main Menu");

                string choiceString = Validation.GetString("Enter your choice: ").ToLower();

                switch(choiceString)
                {
                    case "1":
                    case "enter another activity":
                    case "enter activity":
                        choice = 1;
                        break;
                    case "2":
                    case "back to main menu":
                    case "back to menu":
                        choice = 2;
                        break;
                    default:
                        Console.WriteLine("Invalid command. Press any key to continue...");
                        Console.ReadKey();
                        break;
                }
            }
            return choice;
        }

        private static int WeekSubMenu()
        {
            string choice = Validation.GetString("Enter your choice: ").ToLower();

            int weekDay = 0;
            while(weekDay < 1 || weekDay > 8)
            {
                switch (choice)
                {
                    case "1":
                    case "monday":
                        weekDay = 1;
                        break;

                    case "2":
                    case "tuesday":
                        weekDay = 2;
                        break;

                    case "3":
                    case "wednesday":
                        weekDay = 3;
                        break;
                    case "4":
                    case "thursday":
                        weekDay = 4;
                        break;
                    case "5":
                    case "friday":
                        weekDay = 5;
                        break;
                    case "6":
                    case "saturday":
                        weekDay = 6;
                        break;
                    case "7":
                    case "sunday":
                        weekDay = 7;
                        break;
                    default:
                        Console.WriteLine("Invalid command. Press any key to continue...");
                        Console.ReadKey();
                        break;
                }
            }
            return weekDay;
        }

        private static void PrintWeekCommands()
        {
            // Clear console
            Console.Clear();

            Console.WriteLine("Pick the day of the week:");

            Console.WriteLine("[1] Monday");
            Console.WriteLine("[2] Tuesday");
            Console.WriteLine("[3] Wednesday");
            Console.WriteLine("[4] Thursday");
            Console.WriteLine("[5] Friday");
            Console.WriteLine("[6] Saturday");
            Console.WriteLine("[7] Sunday");
            Console.WriteLine("[8] Back");
        }

        private static double TimeSubMenu(int count, List<double> list)
        {
            double userSelectedItem = -1;

            while (userSelectedItem < 0)
            {
                string choice = Validation.GetString("Enter your choice: ").ToLower();

                for (int num = 1; num <= count + 1; num++)
                {
                    if (num < count + 1 && (choice == num.ToString() || choice == list[num - 1].ToString()))
                    {
                        // saves the users selected menu choice
                        userSelectedItem = list[num - 1];
                        break;
                    }
                    else if (num == count + 1)
                    {
                        userSelectedItem = 0;
                        break;
                    }
                }

                Console.WriteLine("Invalid command. Press any key to continue...");
                Console.ReadKey();
            }
            return userSelectedItem;
        }

        private static string DateSubMenu(Dictionary<string, int> dates, int count)
        {
            string userSelectedItem = "";

            while (!string.IsNullOrWhiteSpace(userSelectedItem))
            {
                string choice = Validation.GetString("Enter your choice: ").ToLower();

                for (int num = 1; num <= count + 1; num++)
                {
                    foreach(KeyValuePair<string, int> date in dates)
                    {
                        if(choice == date.Value.ToString() || choice == date.Key.ToLower())
                        {
                            userSelectedItem = date.Key;
                            break;
                        }
                    }
                    if (num == count + 1)
                    {
                        userSelectedItem = "false";
                        break;
                    }
                    else if(!string.IsNullOrWhiteSpace(userSelectedItem))
                    {
                        break;
                    }
                }

                Console.WriteLine("Invalid command. Press any key to continue...");
                Console.ReadKey();
            }
            return userSelectedItem;
        }

        private static string StringListSubMenu(int count, List<string> list)
        {
            string userSelectedItem = "";

            while (!string.IsNullOrWhiteSpace(userSelectedItem))
            {
                string choice = Validation.GetString("Enter your choice: ").ToLower();

                for (int num = 1; num <= count + 1; num++)
                {
                    if (num < count + 1 && (choice == num.ToString() || choice == list[num - 1].ToLower()))
                    {
                        // saves the users selected menu choice
                        userSelectedItem = list[num - 1];
                        break;
                    }
                    else if (num == count + 1)
                    {
                        userSelectedItem = "false";
                        break;
                    }
                }

                Console.WriteLine("Invalid command. Press any key to continue...");
                Console.ReadKey();
            }
            return userSelectedItem;
        }

        private static void PrintTimeCommands()
        {
            // Clear console
            Console.Clear();

            Console.WriteLine("How long did you perform that activity (15 minutes = 0.25):");

            List<double> times = DatabaseFunctions.GetTimes();

            // Create index variable to keep track of activity number
            int index = 1;

            // Loop through times and print them as options to the screen
            foreach (double time in times)
            {
                Console.WriteLine($"- [{0}] {1}", index, time);
                index++;
            }

            Console.WriteLine($"- [{0}] Back", index + 1);
        }

        private static void PrintDateCommands()
        {
            // Clear console
            Console.Clear();

            Console.WriteLine("Which date did you perform the activity on:");

            Dictionary<string, int> dates = DatabaseFunctions.GetDatesAndDays();

            // Loop through dates and print them as options to the screen
            foreach (KeyValuePair<string, int> date in dates)
            {
                Console.WriteLine($"- [{0}] {1}", date.Value, date.Key);
            }

            Console.WriteLine($"- [{0}] Back", dates.Count() + 1);
        }

        private static void PrintActivityCommands()
        {
            // Clear console
            Console.Clear();

            Console.WriteLine("Pick an activity description:");

            List<string> activities = DatabaseFunctions.GetDescriptions();

            // Create index variable to keep track of activity number
            int index = 1;

            // Loop through activities and print them as options to the screen
            foreach (string activity in activities)
            {
                Console.WriteLine($"- [{0}] {1}", index, activity);
                index++;
            }

            Console.WriteLine($"- [{0}] Back", index + 1);
        }

        private static void PrintCommands()
        {
            // Clear console
            Console.Clear();

            Console.WriteLine("Pick a category of activity:");

            List<string> categories = DatabaseFunctions.GetCategories();

            // Create index variable to keep track of category number
            int index = 1;

            // Loop through categories and print them as options to the screen
            foreach(string category in categories)
            {
                Console.WriteLine($"- [{0}] {1}", index, category);
                index++;
            }

            Console.WriteLine($"- [{0}] Back", index + 1);
        }
    }
}
