using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinaoleLau_TimeTrackerApp
{
    class EnterActivity
    {
        public static void ActivityMenu()
        {

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
            }
        }
    }
}
