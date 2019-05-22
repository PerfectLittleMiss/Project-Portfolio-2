using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinaoleLau_TimeTrackerApp
{
    class Calculations
    {
        public static void MainMenu(int userId)
        {
            // bool to determine if user is on this menu
            bool running = true;

            while(running)
            {
                PrintMenu();
                string choice = Validation.GetString("Enter your choice: ").ToLower();

                switch(choice)
                {
                    case "1":
                    case "total time spent on work":
                        TotalTimeOnWork(userId);
                        break;
                    case "2":
                    case "total time spent on project & portfolio 2":
                        TotalTimeOnClass(userId);
                        break;
                    case "3":
                    case "total time spent relaxing":
                        TotalTimeRelaxing(userId);
                        break;
                    case "4":
                    case "total time spent on other things":
                        TotalTimeOnOther(userId);
                        break;
                    case "5":
                    case "percentage of time on school vs total month":
                        TotalTimeOnClass(userId);
                        break;
                    case "6":
                    case "percentage of time on work vs total month":
                        TotalTimeOnWork(userId);
                        break;
                    case "7":
                    case "percentage of time relaxing vs total month":
                        PercentageRelaxingToTotal(userId);
                        break;
                    case "8":
                    case "percentage of time on other things vs total month":
                        PercentageOtherToTotal(userId);
                        break;
                    case "9":
                    case "percentage of time sleeping vs total month":
                        PercentageSleepingToTotal(userId);
                        break;
                    case "10":
                    case "percentage of time troubleshooting vs total month":
                        PercentageTroubleshootingToTotal(userId);
                        break;
                    case "11":
                    case "percentage of time on school & work vs total month":
                        PercentageSchoolAndWorkToTotal(userId);
                        break;
                    case "12":
                    case "back":
                        running = false;
                        Console.WriteLine("Returning to the main menu. Press any key to continue...");
                        Console.ReadKey();
                        break;
                    default:
                        Console.WriteLine("Invalid command. Press any key to command...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private static void TotalTimeOnOther(int userId)
        {
            double time = DatabaseFunctions.TotalTimeSpentOnCategory(4, userId);
            int hours = DatabaseFunctions.GetHoursOnly(time);
            int minutes = DatabaseFunctions.GetMinutesOnly(time);
            Console.WriteLine("Total Time Spent on Other Things: {0} hours and {1} minutes.", hours, minutes);

            Wait();
        }

        private static void TotalTimeRelaxing(int userId)
        {
            double time = DatabaseFunctions.TotalTimeSpentOnCategory(2, userId);
            int hours = DatabaseFunctions.GetHoursOnly(time);
            int minutes = DatabaseFunctions.GetMinutesOnly(time);
            Console.WriteLine("Total Time Spent Relaxing: {0} hours and {1} minutes.", hours, minutes);

            Wait();
        }

        private static void TotalTimeOnClass(int userId)
        {
            double time = DatabaseFunctions.TotalTimeSpentOnCategory(3, userId);
            int hours = DatabaseFunctions.GetHoursOnly(time);
            int minutes = DatabaseFunctions.GetMinutesOnly(time);
            Console.WriteLine("Total Time Spent on Project & Portfolio 2: {0} hours and {1} minutes.", hours, minutes);

            Wait();
        }

        private static void TotalTimeOnWork(int userId)
        {
            double time = DatabaseFunctions.TotalTimeSpentOnCategory(1, userId);
            int hours = DatabaseFunctions.GetHoursOnly(time);
            int minutes = DatabaseFunctions.GetMinutesOnly(time);
            Console.WriteLine("Total Time Spent on Work: {0} hours and {1} minutes.", hours, minutes);

            Wait();
        }

        private static void PercentageSchoolToTotal(int userId)
        {
            double totalMonth = DatabaseFunctions.GetTotalTimeForMonth(userId);
            double totalSchool = DatabaseFunctions.TotalTimeSpentOnCategory(3, userId);

            double percentage = Math.Round((totalSchool / totalMonth) * 100);

            if(double.IsNaN(percentage))
            {
                percentage = 0;
            }

            Console.WriteLine("You spent {0}% of the time tracked for the month on School.", percentage);

            Wait();
        }

        private static void PercentageWorkToTotal(int userId)
        {
            double totalMonth = DatabaseFunctions.GetTotalTimeForMonth(userId);
            double totalWork = DatabaseFunctions.TotalTimeSpentOnCategory(1, userId);

            double percentage = Math.Round((totalWork / totalMonth) * 100);

            if (double.IsNaN(percentage))
            {
                percentage = 0;
            }

            Console.WriteLine("You spent {0}% of the time tracked for the month on Work.", percentage);

            Wait();
        }

        private static void PercentageRelaxingToTotal(int userId)
        {
            double totalMonth = DatabaseFunctions.GetTotalTimeForMonth(userId);
            double totalRelaxing = DatabaseFunctions.TotalTimeSpentOnCategory(2, userId);

            double percentage = Math.Round((totalRelaxing / totalMonth) * 100);

            if (double.IsNaN(percentage))
            {
                percentage = 0;
            }

            Console.WriteLine("You spent {0}% of the time tracked for the month Relaxing.", percentage);

            Wait();
        }

        private static void PercentageOtherToTotal(int userId)
        {
            double totalMonth = DatabaseFunctions.GetTotalTimeForMonth(userId);
            double totalOther = DatabaseFunctions.TotalTimeSpentOnCategory(4, userId);

            double percentage = Math.Round((totalOther / totalMonth) * 100);

            if (double.IsNaN(percentage))
            {
                percentage = 0;
            }

            Console.WriteLine("You spent {0}% of the time tracked for the month on Other Things.", percentage);

            Wait();
        }

        private static void PercentageSleepingToTotal(int userId)
        {
            double totalMonth = DatabaseFunctions.GetTotalTimeForMonth(userId);
            double totalSleep = DatabaseFunctions.TotalTimeSpentOnActivity(16, userId);

            double percentage = Math.Round((totalSleep / totalMonth) * 100);

            if (double.IsNaN(percentage))
            {
                percentage = 0;
            }

            Console.WriteLine("You spent {0}% of the time tracked for the month Sleeping.", percentage);

            Wait();
        }

        private static void PercentageTroubleshootingToTotal(int userId)
        {
            double totalMonth = DatabaseFunctions.GetTotalTimeForMonth(userId);
            double totalDebug = DatabaseFunctions.TotalTimeSpentOnActivity(17, userId);

            double percentage = Math.Round((totalDebug / totalMonth) * 100);

            if (double.IsNaN(percentage))
            {
                percentage = 0;
            }

            Console.WriteLine("You spent {0}% of the time tracked for the month on Troubleshooting.", percentage);

            Wait();
        }

        private static void PercentageSchoolAndWorkToTotal(int userId)
        {
            double totalMonth = DatabaseFunctions.GetTotalTimeForMonth(userId);
            double totalWork = DatabaseFunctions.TotalTimeSpentOnCategory(1, userId);
            double totalSchool = DatabaseFunctions.TotalTimeSpentOnCategory(3, userId);
            double totalSchoolAndWork = totalWork + totalSchool;

            double percentage = Math.Round((totalSchoolAndWork / totalMonth) * 100);

            if (double.IsNaN(percentage))
            {
                percentage = 0;
            }

            Console.WriteLine("You spent {0}% of the time tracked for the month on School and Work.", percentage);

            Wait();
        }

        private static void Wait()
        {
            Console.WriteLine("Press any key to return to the calculations menu...");
            Console.ReadKey();
        }

        private static void PrintMenu()
        {
            Console.Clear();

            Console.WriteLine("[1] Total Time Spent on Work");
            Console.WriteLine("[2] Total Time Spent on Project & Portfolio 2");
            Console.WriteLine("[3] Total Time Spent Relaxing");
            Console.WriteLine("[4] Total Time Spent on Other Things");
            Console.WriteLine("[5] Percentage of Time on School vs Total Month");
            Console.WriteLine("[6] Percentage of Time on Work vs Total Month");
            Console.WriteLine("[7] Percentage of Time Relaxing vs Total Month");
            Console.WriteLine("[8] Percentage of Time on Other Things vs Total Month");
            Console.WriteLine("[9] Percentage of Time Sleeping vs Total Month");
            Console.WriteLine("[10] Percentage of Time Troubleshooting vs Total Month");
            Console.WriteLine("[11] Percentage of Time on School & Work vs Total Month");
            Console.WriteLine("[12] Back");
        }
    }
}
