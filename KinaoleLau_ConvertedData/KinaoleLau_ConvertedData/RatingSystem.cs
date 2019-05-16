using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinaoleLau_ConvertedData
{
    class RatingSystem
    {
        public static void RatingProgram()
        {
            // Create bool to determine if user is still using this menu
            bool running = true;

            while(running)
            {
                // Display commands to the user, get and validate the users choice, and convert the choice to lowercase
                PrintCommands();
                string choice = Validation.GetString("Enter your choice: ").ToLower();

                switch(choice)
                {
                    case "1":
                    case "list restaurants alphabetically":
                    case "list alphabetically":
                        ListAlphbetically();
                        Console.WriteLine("Press any key to the rating menu...");
                        Console.ReadKey();
                        break;

                    case "2":
                    case "list restaurants in reverse alphabetical":
                    case "list in reverse alphabetical":

                        Console.WriteLine("Press any key to the rating menu...");
                        Console.ReadKey();
                        break;

                    case "3":
                    case "sort restaurants from best to worst":
                    case "sort best to worst":

                        Console.WriteLine("Press any key to the rating menu...");
                        Console.ReadKey();
                        break;

                    case "4":
                    case "sort restaurants from worst to best":
                    case "sort worst to best":

                        Console.WriteLine("Press any key to the rating menu...");
                        Console.ReadKey();
                        break;

                    case "5":
                    case "show only x and up":
                    case "show only":

                        break;

                    case "6":
                    case "exit":
                    case "quit":
                        running = false;
                        Console.WriteLine("Press any key to return to the main menu...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private static void ListAlphbetically()
        {
            // Create a dictionary to hold the unsorted restaurant ratings
            Dictionary<string, List<int>> unsorted = GetRestaurantRatings();

            // Loop through the sorted restaurants
            foreach (KeyValuePair<string, List<int>> restaurant in unsorted.OrderBy(key => key.Key))
            {
                List<int> values = restaurant.Value;
                // Save the stars and star fragments to variables
                int stars = values[0];
                int starFragments = values[1];

                string rating = "Rating: ";

                int wordLength = rating.Count();

                // Convert remainder into a fraction
                string fragments = "";
                if (starFragments > 0)
                {
                    switch(starFragments)
                    {
                        case 25:
                            fragments = "1/4";
                            break;

                        case 50:
                            fragments = "1/2";
                            break;

                        case 75:
                            fragments = "3/4";
                            break;
                    }
                }

                Console.WriteLine("Restaurant: {0} - {1} {2}", restaurant.Key, rating.PadRight(wordLength + stars, '*'), fragments);
            }
        }

        private static Dictionary<string, List<int>> GetRestaurantRatings()
        {
            // Store the raw data of names and ratings in a dictionary
            Dictionary<int, Dictionary<string, int?>> namesAndRatings = DatabaseFunctions.GetRestaurantRatings();

            // Create an empty dictionary to hold each restaurant and it's star rating (int = whole stars, double = star fragments)
            Dictionary<string, List<int>> restaurantRatings = new Dictionary<string, List<int>>();

            // Create a dictionary to hold the name, number of ratings, and total rating of each Restaurant before calculations
            // The list will contain the ratings converted into a rating out of 5
            Dictionary<string, List<int>> namesAndTotals = new Dictionary<string, List<int>>();

            // Loop through the data in the dictionary of names and ratings
            foreach (int id in namesAndRatings.Keys)
            {
                // Create a dictionary to hold the row info
                Dictionary<string, int?> rowInfo = namesAndRatings[id];
                GetUniqueNames(namesAndTotals, rowInfo);
            }

            foreach (string name in namesAndTotals.Keys)
            {
                // Create a list to hold the list for each restaurant
                List<int> ratings = namesAndTotals[name];

                List<int> starValues;

                // Workaround for if restaurant has 0 ratings
                if (ratings.Count <= 0)
                {
                    // Create a variable to hold the sum of the rating for each restaurant
                    int sum = 0;
                    // Create a variable to keep track of the number of ratings
                    int count = 0;

                    foreach (int rating in ratings)
                    {
                        sum += rating;
                        count++;
                    }

                    // Create a variable to hold the average of the ratings
                    decimal average = sum / count;

                    // Create a variable to hold the average of 5
                    decimal averageOf5 = (average / 100) * 5;

                    // Find the number of total stars (whole numbers)
                    int stars = Convert.ToInt32(Math.Floor(averageOf5));

                    // Find the remaining star fragments (decimal) and convert to a whole number
                    int remaining = decimal.ToInt32((averageOf5 - stars) * 100);

                    starValues = GetStars(remaining, stars);
                }
                else
                {
                    starValues = new List<int>();
                    starValues.Add(0);
                }

                // Add the name and the star value to the restaurant ratings dictionary
                restaurantRatings.Add(name, starValues);
            }

            return restaurantRatings;
        }

        private static List<int> GetStars(int remaining, int stars)
        {
            // Round the remaing to nearest quarter
            int remainingRounded = 0;
            if (remaining <= 25)
            {
                if (remaining < 12.5)
                {
                    remainingRounded = 0;
                }
                else
                {
                    remainingRounded = 25;
                }
            }
            else if (remaining <= 50)
            {
                if (remaining < 37.5)
                {
                    remainingRounded = 25;
                }
                else
                {
                    remainingRounded = 50;
                }
            }
            else if (remaining <= 75)
            {
                if (remaining < 62.5)
                {
                    remainingRounded = 50;
                }
                else
                {
                    remainingRounded = 75;
                }
            }
            else if (remaining <= 100)
            {
                if (remaining < 87.5)
                {
                    remainingRounded = 75;
                }
                else
                {
                    remainingRounded = 100;
                }
            }

            // Create a dictionary to hold the stars and remainingRounded values
            List<int> starValue = new List<int>();

            starValue.Add(stars);
            starValue.Add(remainingRounded);

            return starValue;
        }

        private static void GetUniqueNames(Dictionary<string, List<int>> namesAndTotals, Dictionary<string, int?> rowInfo)
        {
            string name = null;

            // Get name and rating
            foreach(KeyValuePair<string, int?> nameRating in rowInfo)
            {
                name = nameRating.Key;
                int? value = nameRating.Value;
                if (namesAndTotals.ContainsKey(name))
                {

                    if (rowInfo[name] != null)
                    {
                        // Add the rating for this row to the list
                        namesAndTotals[name].Add(rowInfo[name].Value);
                    }
                }
                else
                {
                    // Create a list to contain the number of ratings given and the total rating
                    // Give the list the current row values
                    List<int> ratings = new List<int>();

                    if (rowInfo[name] != null)
                    {
                        ratings.Add(rowInfo[name].Value);
                        // Add the name and list with rating values to the dictionary
                        namesAndTotals.Add(name, ratings);
                    }
                    else
                    {
                        // Add the name and list with rating values to the dictionary
                        namesAndTotals.Add(name, ratings);
                    }
                }
            }
        }

        private static void PrintCommands()
        {
            // Clear the console
            Console.Clear();

            Console.WriteLine("Hello Admin, How would you like to sort the data:");
            Console.WriteLine("- [1] List Restaurants Alphabetically");
            Console.WriteLine("- [2] List Restaurants in Reverse Alphabetical");
            Console.WriteLine("- [3] Sort Restaurants From Best to Worst");
            Console.WriteLine("- [4] Sort Restaurants From Worst to Best");
            Console.WriteLine("- [5] Show Only X and Up");
            Console.WriteLine("- [6] Exit");
        }
    }
}
