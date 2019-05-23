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
            Console.WriteLine("\n[1] View Thoughts");
            Console.WriteLine("[2] Log Out");
        }
    }
}
