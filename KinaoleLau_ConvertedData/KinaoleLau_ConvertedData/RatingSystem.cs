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
                        ListReverseAlphbetical();
                        Console.WriteLine("Press any key to the rating menu...");
                        Console.ReadKey();
                        break;

                    case "3":
                    case "sort restaurants from best to worst":
                    case "sort best to worst":
                        SortBestToWorst();
                        Console.WriteLine("Press any key to the rating menu...");
                        Console.ReadKey();
                        break;

                    case "4":
                    case "sort restaurants from worst to best":
                    case "sort worst to best":
                        SortWorstToBest();
                        Console.WriteLine("Press any key to the rating menu...");
                        Console.ReadKey();
                        break;

                    case "5":
                    case "show only x and up":
                    case "show only":
                        ShowOnlyMenu();
                        break;

                    case "6":
                    case "exit":
                    case "quit":
                        running = false;
                        Console.WriteLine("Press any key to return to the main menu...");
                        Console.ReadKey();
                        break;

                    default:
                        Console.WriteLine("This is an invalid command. Press any key to continue...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private static void ShowOnlyMenu()
        {
            // bool to determine if user is still on menu
            bool running = true;

            while(running)
            {
                ShowOnlyCommands();
                string choice = Validation.GetString("Enter your choice: ").ToLower();

                switch(choice)
                {
                    case "1":
                    case "show the best":
                    case "best":
                        ShowBest();
                        Console.WriteLine("Press any key to return to the Show Only menu...");
                        Console.ReadKey();
                        break;

                    case "2":
                    case "show 4 stars and up":
                    case "show 4":
                        Show4AndUp();
                        Console.WriteLine("Press any key to return to the Show Only menu...");
                        Console.ReadKey();
                        break;

                    case "3":
                    case "show 3 stars and up":
                    case "show 3":
                        Show3AndUp();
                        Console.WriteLine("Press any key to return to the Show Only menu...");
                        Console.ReadKey();
                        break;

                    case "4":
                    case "show the worst":
                    case "worst":
                        ShowWorst();
                        Console.WriteLine("Press any key to return to the Show Only menu...");
                        Console.ReadKey();
                        break;

                    case "5":
                    case "show unrated":
                    case "unrated":
                        ShowUnrated();
                        Console.WriteLine("Press any key to return to the Show Only menu...");
                        Console.ReadKey();
                        break;

                    case "6":
                    case "back":
                    case "exit":
                        running = false;
                        Console.WriteLine("Press any key to return to the rating system menu...");
                        Console.ReadKey();
                        break;

                    default:
                        Console.WriteLine("Invalid command. Press any key to continue...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private static void ShowUnrated()
        {
            Dictionary<string, decimal?> restaurants = GetRestaurantRatingStrings();

            Console.WriteLine("Unrated".PadLeft(40, ' '));

            Console.WriteLine("".PadRight(85, '_'));

            // Loop through the sorted restaurants
            foreach (KeyValuePair<string, decimal?> restaurant in restaurants.OrderBy(value => value.Value))
            {
                if (restaurant.Value == null)
                {
                    Console.WriteLine(restaurant.Key);
                }
            }
            Console.WriteLine("".PadRight(85, '_'));
        }

        private static void ShowWorst()
        {
            Dictionary<string, decimal?> restaurants = GetRestaurantRatingStrings();

            Console.WriteLine("The Worst (1 Stars)".PadLeft(40, ' '));

            Console.WriteLine("".PadRight(85, '_'));

            // Loop through the sorted restaurants
            foreach (KeyValuePair<string, decimal?> restaurant in restaurants.OrderBy(value => value.Value))
            {
                if (restaurant.Value <= 1)
                {
                    Console.WriteLine(restaurant.Key);
                }
            }
            Console.WriteLine("".PadRight(85, '_'));
        }

        private static void Show3AndUp()
        {
            Dictionary<string, decimal?> restaurants = GetRestaurantRatingStrings();

            Console.WriteLine("3 Stars and Up".PadLeft(40, ' '));

            Console.WriteLine("".PadRight(85, '_'));

            // Loop through the sorted restaurants
            foreach (KeyValuePair<string, decimal?> restaurant in restaurants.OrderBy(value => value.Value))
            {
                if (restaurant.Value >= 3)
                {
                    Console.WriteLine(restaurant.Key);
                }
            }
            Console.WriteLine("".PadRight(85, '_'));
        }

        private static void Show4AndUp()
        {
            Dictionary<string, decimal?> restaurants = GetRestaurantRatingStrings();

            Console.WriteLine("4 Stars and Up".PadLeft(40, ' '));

            Console.WriteLine("".PadRight(85, '_'));

            // Loop through the sorted restaurants
            foreach (KeyValuePair<string, decimal?> restaurant in restaurants.OrderBy(value => value.Value))
            {
                if (restaurant.Value >= 4)
                {
                    Console.WriteLine(restaurant.Key);
                }
            }
            Console.WriteLine("".PadRight(85, '_'));
        }

        private static void ShowBest()
        {
            Dictionary<string, decimal?> restaurants = GetRestaurantRatingStrings();

            Console.WriteLine("The Best (5 Stars)".PadLeft(40, ' '));

            Console.WriteLine("".PadRight(85, '_'));

            // Loop through the sorted restaurants
            foreach (KeyValuePair<string, decimal?> restaurant in restaurants.OrderBy(value => value.Value))
            {
                if(restaurant.Value >= 5)
                {
                    Console.WriteLine(restaurant.Key);
                }
            }
            Console.WriteLine("".PadRight(85, '_'));
        }

        private static void ShowOnlyCommands()
        {
            Console.Clear();

            Console.WriteLine("-- [1] Show the Best (5 Stars)");
            Console.WriteLine("-- [2] Show 4 Stars and Up");
            Console.WriteLine("-- [3] Show 3 Stars and Up");
            Console.WriteLine("-- [4] Show the Worst (1 Stars)");
            Console.WriteLine("-- [5] Show Unrated");
            Console.WriteLine("-- [6] Back");
        }

        private static void SortWorstToBest()
        {
            Dictionary<string, decimal?> restaurants = GetRestaurantRatingStrings();

            Console.WriteLine("".PadRight(85, '_'));

            // Loop through the sorted restaurants
            foreach (KeyValuePair<string, decimal?> restaurant in restaurants.OrderBy(value => value.Value))
            {
                Console.WriteLine(restaurant.Key);
            }
            Console.WriteLine("".PadRight(85, '_'));
        }

        private static void SortBestToWorst()
        {
            Dictionary<string, decimal?> restaurants = GetRestaurantRatingStrings();

            Console.WriteLine("".PadRight(85, '_'));

            // Loop through the sorted restaurants
            foreach (KeyValuePair<string, decimal?> restaurant in restaurants.OrderByDescending(value => value.Value))
            {
                Console.WriteLine(restaurant.Key);
            }
            Console.WriteLine("".PadRight(85, '_'));
        }

        private static void ListReverseAlphbetical()
        {
            // Create a dictionary to hold the unsorted restaurant ratings
            Dictionary<string, List<int?>> unsorted = GetRestaurantRatings();

            Console.WriteLine("".PadRight(85, '_'));

            // Loop through the sorted restaurants
            foreach (KeyValuePair<string, List<int?>> restaurant in unsorted.OrderByDescending(key => key.Key))
            {
                List<int?> values = restaurant.Value;

                int? stars;
                int? starFragments;

                // Save the stars and star fragments to variables
                stars = values[0];
                starFragments = values[1];

                string rating = "Rating: ";

                string ofStars;

                int wordLength = rating.Count();

                // Convert remainder into a fraction
                string fragments = "";
                if (starFragments > 0)
                {
                    switch (starFragments)
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
                    ofStars = rating.PadRight(wordLength + stars.Value, '*') + fragments + " of 5 stars";
                    Console.WriteLine("| Restaurant: {0}| {1} |", restaurant.Key.PadRight(40, ' '), ofStars.PadRight(27, ' '));
                }
                else if (stars == 0)
                {
                    ofStars = "Rating: 0 of 5 stars";
                    Console.WriteLine("| Restaurant: {0}| {1} |", restaurant.Key.PadRight(40, ' '), ofStars.PadRight(27, ' '));
                }
                else if (stars == null && starFragments == null)
                {
                    ofStars = "No Rating Available";
                    Console.WriteLine("| Restaurant: {0}| {1} |", restaurant.Key.PadRight(40, ' '), ofStars.PadRight(27, ' '));
                }
                else
                {
                    ofStars = rating.PadRight(wordLength + stars.Value, '*') + " of 5 stars";
                    Console.WriteLine("| Restaurant: {0}| {1} |", restaurant.Key.PadRight(40, ' '), ofStars.PadRight(27, ' '));
                }
            }
            Console.WriteLine("".PadRight(85, '_'));
        }

        private static void ListAlphbetically()
        {
            // Create a dictionary to hold the unsorted restaurant ratings
            Dictionary<string, List<int?>> unsorted = GetRestaurantRatings();

            Console.WriteLine("".PadRight(85, '_'));

            // Loop through the sorted restaurants
            foreach (KeyValuePair<string, List<int?>> restaurant in unsorted.OrderBy(key => key.Key))
            {
                List<int?> values = restaurant.Value;

                int? stars;
                int? starFragments;

                // Save the stars and star fragments to variables
                stars = values[0];
                starFragments = values[1];

                string rating = "Rating: ";

                string ofStars;

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
                    ofStars = rating.PadRight(wordLength + stars.Value, '*') + fragments + " of 5 stars";
                    Console.WriteLine("| Restaurant: {0}| {1} |", restaurant.Key.PadRight(40, ' '), ofStars.PadRight(27, ' '));
                }
                else if (stars == 0)
                {
                    ofStars = "Rating: 0 of 5 stars";
                    Console.WriteLine("| Restaurant: {0}| {1} |", restaurant.Key.PadRight(40, ' '), ofStars.PadRight(27, ' '));
                }
                else if (stars == null && starFragments == null)
                {
                    ofStars = "No Rating Available";
                    Console.WriteLine("| Restaurant: {0}| {1} |", restaurant.Key.PadRight(40, ' '), ofStars.PadRight(27, ' '));
                }
                else
                {
                    ofStars = rating.PadRight(wordLength + stars.Value, '*') + " of 5 stars";
                    Console.WriteLine("| Restaurant: {0}| {1} |", restaurant.Key.PadRight(40, ' '), ofStars.PadRight(27, ' '));
                }
            }
            Console.WriteLine("".PadRight(85, '_'));
        }

        private static Dictionary<string, List<int?>> GetRestaurantRatings()
        {
            // Store the raw data of names and ratings in a dictionary
            Dictionary<string, decimal?> namesAndRatings = DatabaseFunctions.GetRestaurantRatings();

            // Create an empty dictionary to hold each restaurant and it's star rating (int = whole stars, double = star fragments)
            Dictionary<string, List<int?>> restaurantRatings = new Dictionary<string, List<int?>>();
            

            foreach (string name in namesAndRatings.Keys)
            {
                // Create a list to hold the list for each restaurant
                decimal? rating = namesAndRatings[name];

                List<int?> starValues = new List<int?>();

                if(rating == null)
                {
                    starValues.Add(null);
                    starValues.Add(null);
                }
                else
                {
                    // Find the number of total stars (whole numbers)
                    int stars = Convert.ToInt32(Math.Floor(rating.Value));

                    // Find the remaining star fragments (decimal) and convert to a whole number
                    int remaining = decimal.ToInt32((rating.Value - stars) * 100);

                    starValues = GetStars(remaining, stars);
                }

                // Add the name and the star value to the restaurant ratings dictionary
                restaurantRatings.Add(name, starValues);
            }

            return restaurantRatings;
        }

        private static Dictionary<string, decimal?> GetRestaurantRatingStrings()
        {
            // Create a dictionary to hold the unsorted restaurant ratings
            Dictionary<string, List<int?>> unsorted = GetRestaurantRatings();

            // Create a dictionary to hold the restaurant names, ratings, and output strings
            Dictionary<string, decimal?> restaurantStrings = new Dictionary<string, decimal?>();

            // Loop through the sorted restaurants
            foreach (KeyValuePair<string, List<int?>> restaurant in unsorted)
            {
                string output;

                List<int?> values = restaurant.Value;

                int? stars;
                int? starFragments;

                // Save the stars and star fragments to variables
                stars = values[0];
                starFragments = values[1];

                string rating = "Rating: ";

                string ofStars;

                int wordLength = rating.Count();

                // Convert remainder into a fraction
                string fragments = "";
                if (starFragments > 0)
                {
                    switch (starFragments)
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
                    ofStars = rating.PadRight(wordLength + stars.Value, '*') + fragments + " of 5 stars";
                    output = $"| Restaurant: {restaurant.Key.PadRight(40, ' ')}| {ofStars.PadRight(27, ' ')} |";
                }
                else if (stars == 0)
                {
                    ofStars = "Rating: 0 of 5 stars";
                    output = $"| Restaurant: {restaurant.Key.PadRight(40, ' ')}| {ofStars.PadRight(27, ' ')} |";
                }
                else if (stars == null && starFragments == null)
                {
                    ofStars = "No Rating Available";
                    output = $"| Restaurant: {restaurant.Key.PadRight(40, ' ')}| {ofStars.PadRight(27, ' ')} |";
                }
                else
                {
                    ofStars = rating.PadRight(wordLength + stars.Value, '*') + " of 5 stars";
                    output = $"| Restaurant: {restaurant.Key.PadRight(40, ' ')}| {ofStars.PadRight(27, ' ')} |";
                }

                decimal? ratingTotal;

                // Check if rating is null
                if (stars == null)
                {
                    ratingTotal = null;
                }
                else
                {
                    ratingTotal = (decimal)stars.Value + ((decimal)starFragments / 100m);
                }
                

                restaurantStrings.Add(output, ratingTotal);
            }
            return restaurantStrings;
        }

        private static List<int?> GetStars(int? remaining, int? stars)
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
            List<int?> starValue = new List<int?>();

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
