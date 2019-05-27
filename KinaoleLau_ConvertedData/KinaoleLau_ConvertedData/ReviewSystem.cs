using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace KinaoleLau_ConvertedData
{
    class ReviewSystem
    {
        // Create timer variable
        private static System.Timers.Timer myAnimationTimer;
        // Create timer counter
        static int myTimerCounter = 0;
        //Work around for not being able to add parameters to the ontimedevent functions
        static int reviewScore = 0;

        public static void MainMenu()
        {
            // create bool to determine if user is on this menu
            bool running = true;

            while(running)
            {
                PrintCommands();
                string choice = Validation.GetString("Enter your choice: ").ToLower();

                switch(choice)
                {
                    case "1":
                    case "show average of reviews for restaurants":
                        ShowAverage();
                        break;
                    case "2":
                    case "dinner spinner":
                        DinnerSpinner();
                        break;
                    case "3":
                    case "top 10 restaurants":
                        Top10();
                        break;
                    case "4":
                    case "back to main menu":
                        running = false;
                        Console.WriteLine("Returning to main menu. Press any key to continue...");
                        Console.ReadKey();
                        break;
                    default:
                        Console.WriteLine("Invalid command. Press any key to continue...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private static void Top10()
        {
            //get the restaurants that don't have null as a value
            Dictionary<string, int> restaurantsWONull = GetRestaurantsWONullValues();

            int i = 0;
            foreach(KeyValuePair<string, int> restaurant in restaurantsWONull.OrderByDescending(key => key.Value))
            {
                if(i <= 10)
                {
                    string rating = restaurant.Value + "/10";
                    SetTimerAnimated(restaurant.Value, restaurant.Key, rating);
                    Thread.Sleep(2000);
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                else
                {
                    break;
                }
                i++;
            }
            Program.Pause();
        }

        private static Dictionary<string, int> GetRestaurantsWONullValues()
        {
            // Get the dictionary with the name and string review scores
            Dictionary<string, string> avgReviewScores = DatabaseFunctions.GetRestaurantAverageReviewScore();
            // Create the dictionary to hold the restaurants with no null values
            Dictionary<string, int> restaurantsWONull = new Dictionary<string, int>();

            foreach(string name in avgReviewScores.Keys)
            {
                if(avgReviewScores[name] == "null")
                {
                    // do nothing
                    continue;
                }
                else if(int.TryParse(avgReviewScores[name], out int reviewScore))
                {
                    //add the name and review score to the new dictionary
                    restaurantsWONull.Add(name, reviewScore);
                }
                else
                {
                    Console.WriteLine("Error.");
                }
            }

            return restaurantsWONull;
        }

        private static void DinnerSpinner()
        {
            // Get the dictionary of restaurants
            Dictionary<string, string> avgReviewScores = DatabaseFunctions.GetRestaurantAverageReviewScore();
            // Create list of restaurant names
            List<string> restaurantNames = avgReviewScores.Keys.ToList();
            // Get random number for the random restaurant
            Random rand = new Random();
            int chosenNameInt = rand.Next(0, restaurantNames.Count());
            string chosenName = restaurantNames[chosenNameInt];
            // Get the review score from the dictionary
            if(avgReviewScores[chosenName] == "null")
            {
                Console.WriteLine("{0} - Rating: NULL Bar Graph: NULL", chosenName);
            }
            else if(int.TryParse(avgReviewScores[chosenName], out int reviewScore))
            {
                string rating = reviewScore + "/10";
                SetTimerAnimated(reviewScore, chosenName, rating);
            }
            else
            {
                Console.WriteLine("Error.");
            }
            Thread.Sleep(2000);
            Console.BackgroundColor = ConsoleColor.Black;
            Program.Pause();
        }

        private static void ShowAverage()
        {
            Dictionary<string, string> avgReviewScores = DatabaseFunctions.GetRestaurantAverageReviewScore();

            foreach(string name in avgReviewScores.Keys)
            {
                if(avgReviewScores[name] == "null")
                {
                    Console.WriteLine("{0} - Rating: NULL Bar Graph: NULL", name);
                }
                else if (int.TryParse(avgReviewScores[name], out int reviewScore))
                {
                    string rating = reviewScore + "/10";
                    SetTimer(reviewScore, name, rating);
                }
                else
                {
                    Console.WriteLine("Error");
                }
                Console.WriteLine("");
            }

            Program.Pause();

        }

        private static void SetTimer(int reviewScore, string name, string rating)
        {
            //Start the function every 50/1000 seconds
            myAnimationTimer = new System.Timers.Timer(50);

            ReviewSystem.reviewScore = reviewScore;

            // Run the right graph based on scores out of 10
            if (reviewScore <= 3)
            {
                Console.Write("{0} - Rating: {1} Bar Graph: ", name, rating);

                //At 50/1000, run this method "OnTimedEvent"
                //Every time it elapses, do it
                myAnimationTimer.Elapsed += Bad;

                //Reset timer again after 50/1000, over and over again
                myAnimationTimer.AutoReset = false;

                //The timer is enabled so it will work
                myAnimationTimer.Enabled = true;
            }
            else if (reviewScore < 7)
            {
                Console.Write("{0} - Rating: {1} Bar Graph: ", name, rating);

                //At 50/1000, run this method "OnTimedEvent"
                //Every time it elapses, do it
                myAnimationTimer.Elapsed += Average;

                //Reset timer again after 50/1000, over and over again
                myAnimationTimer.AutoReset = false;

                //The timer is enabled so it will work
                myAnimationTimer.Enabled = true;
            }
            else if (reviewScore <= 10)
            {
                Console.Write("{0} - Rating: {1} Bar Graph: ", name, rating);

                //At 50/1000, run this method "OnTimedEvent"
                //Every time it elapses, do it
                myAnimationTimer.Elapsed += Good;

                //Reset timer again after 50/1000, over and over again
                myAnimationTimer.AutoReset = false;

                //The timer is enabled so it will work
                myAnimationTimer.Enabled = true;
            }
            else
            {
                Console.WriteLine("Error");
            }
            Thread.Sleep(500);

        }

        private static void Good(Object source, ElapsedEventArgs e)
        {
            //Setting the bar graph colors
            var myBackgroundColor = ConsoleColor.Gray;
            var myBarGraphColor = ConsoleColor.Green;

            //Once the animation is over, redraw the bar graph one more time with the actual bar graph data
            //Set color for the bar graph
            Console.BackgroundColor = myBarGraphColor;

            //Create bar graph, not the bar graph background
            for (int ii = 0; ii <= reviewScore; ii++)
            {
                //This creates a colored bar graph of spaces
                Console.Write(" ");
            }

            //Set bar graph background color
            Console.BackgroundColor = myBackgroundColor;

            //Draw bar graph background
            for (int iii = reviewScore; iii <= 10; iii++)
            {
                //This creates a colored background of spaces
                Console.Write(" ");
            }

            Console.BackgroundColor = ConsoleColor.Black;

            //Move the cursor down and away from the artwork/bar graph to have menu options, text, etc.
            for (int x = 0; x < 2; x++)
            {
                Console.WriteLine("");
            }

            //Show the cursor again so the user can do what you need them to do.
            Console.CursorVisible = true;
        }

        private static void Average(Object source, ElapsedEventArgs e)
        {
            //Setting the bar graph colors
            var myBackgroundColor = ConsoleColor.Gray;
            var myBarGraphColor = ConsoleColor.Yellow;

            //Once the animation is over, redraw the bar graph one more time with the actual bar graph data
            //Set color for the bar graph
            Console.BackgroundColor = myBarGraphColor;

            //Create bar graph, not the bar graph background
            for (int ii = 0; ii <= reviewScore; ii++)
            {
                //This creates a colored bar graph of spaces
                Console.Write(" ");
            }

            //Set bar graph background color
            Console.BackgroundColor = myBackgroundColor;

            //Draw bar graph background
            for (int iii = reviewScore; iii <= 10; iii++)
            {
                //This creates a colored background of spaces
                Console.Write(" ");
            }

            Console.BackgroundColor = ConsoleColor.Black;

            //Move the cursor down and away from the artwork/bar graph to have menu options, text, etc.
            for (int x = 0; x < 2; x++)
            {
                Console.WriteLine("");
            }

            //Show the cursor again so the user can do what you need them to do.
            Console.CursorVisible = true;
        }

        private static void Bad(Object source, ElapsedEventArgs e)
        {
            //Setting the bar graph colors
            var myBackgroundColor = ConsoleColor.Gray;
            var myBarGraphColor = ConsoleColor.Red;

            //Once the animation is over, redraw the bar graph one more time with the actual bar graph data
            //Set color for the bar graph
            Console.BackgroundColor = myBarGraphColor;

            //Create bar graph, not the bar graph background
            for (int ii = 0; ii <= reviewScore; ii++)
            {
                //This creates a colored bar graph of spaces
                Console.Write(" ");
            }

            //Set bar graph background color
            Console.BackgroundColor = myBackgroundColor;

            //Draw bar graph background
            for (int iii = reviewScore; iii <= 10; iii++)
            {
                //This creates a colored background of spaces
                Console.Write(" ");
            }

            Console.BackgroundColor = ConsoleColor.Black;

            //Move the cursor down and away from the artwork/bar graph to have menu options, text, etc.
            for (int x = 0; x < 2; x++)
            {
                Console.WriteLine("");
            }

            //Show the cursor again so the user can do what you need them to do.
            Console.CursorVisible = true;
        }

        //Set Timer Properties
        private static void SetTimerAnimated(int reviewScore, string name, string rating)
        {
            //Start the function every 50/1000 seconds
            myAnimationTimer = new System.Timers.Timer(50);

            myTimerCounter = 0;

            ReviewSystem.reviewScore = reviewScore;

            // Run the right graph based on scores out of 10
            if(reviewScore <= 3)
            {
                Console.WriteLine("{0} - Rating: {1} Bar Graph: ", name, rating);
                Console.WriteLine("");

                //At 50/1000, run this method "OnTimedEvent"
                //Every time it elapses, do it
                myAnimationTimer.Elapsed += AnimatedBad;

                //Reset timer again after 50/1000, over and over again
                myAnimationTimer.AutoReset = true;

                //The timer is enabled so it will work
                myAnimationTimer.Enabled = true;
            }
            else if (reviewScore < 7)
            {
                Console.WriteLine("{0} - Rating: {1} Bar Graph: ", name, rating);
                Console.WriteLine("");

                //At 50/1000, run this method "OnTimedEvent"
                //Every time it elapses, do it
                myAnimationTimer.Elapsed += AnimatedAverage;

                //Reset timer again after 50/1000, over and over again
                myAnimationTimer.AutoReset = true;

                //The timer is enabled so it will work
                myAnimationTimer.Enabled = true;
            }
            else if (reviewScore <= 10)
            {
                Console.WriteLine("{0} - Rating: {1} Bar Graph: ", name, rating);
                Console.WriteLine("");

                //At 50/1000, run this method "OnTimedEvent"
                //Every time it elapses, do it
                myAnimationTimer.Elapsed += AnimatedGood;

                //Reset timer again after 50/1000, over and over again
                myAnimationTimer.AutoReset = true;

                //The timer is enabled so it will work
                myAnimationTimer.Enabled = true;
            }
            else
            {
                Console.WriteLine("Error");
            }

            Console.BackgroundColor = ConsoleColor.Black;
        }

        //Timer Method that runs every time the timer elapses
        private static void AnimatedGood(Object source, ElapsedEventArgs e)
        {
            //Setting the bar graph colors
            var myBackgroundColor = ConsoleColor.Gray;
            var myBarGraphColor = ConsoleColor.Green;

            //Add one to the timer counter every time it elapses
            myTimerCounter++;

            //Random number for the bar graph animation
            Random myRandomNumber = new Random();

            //This randomly selects a number from 0 to 10 and assigns it to a variable.
            var theRating = myRandomNumber.Next(0, 11);

            //Set color for the bar graph
            Console.BackgroundColor = myBarGraphColor;

            //Create bar graph, not the bar graph background
            for (int ii = 0; ii <= theRating; ii++)
            {
                //This creates a colored bar graph of spaces
                Console.Write(" ");
            }

            //Set total number for the length of the bar graph, which is also the background
            int myTotalNumber = 10;

            //Set bar graph background color
            Console.BackgroundColor = myBackgroundColor;

            //Draw bar graph background
            for (int iii = theRating; iii <= myTotalNumber; iii++)
            {
                //This creates a colored background of spaces
                Console.Write(" ");
            }

            //Move the cursor back to the left, where it started to draw the animation to begin with.
            Console.CursorLeft = 0;

            //After a bit of time, stop the animation
            if (myTimerCounter == 50)
            {
                //Stop Timer
                myAnimationTimer.Stop();
                
                //Once the animation is over, redraw the bar graph one more time with the actual bar graph data
                //Set color for the bar graph
                Console.BackgroundColor = myBarGraphColor;

                //Create bar graph, not the bar graph background
                for (int ii = 0; ii <= reviewScore; ii++)
                {
                    //This creates a colored bar graph of spaces
                    Console.Write(" ");
                }

                //Set bar graph background color
                Console.BackgroundColor = myBackgroundColor;

                //Draw bar graph background
                for (int iii = reviewScore; iii <= myTotalNumber; iii++)
                {
                    //This creates a colored background of spaces
                    Console.Write(" ");
                }

                //Move the cursor down and away from the artwork/bar graph to have menu options, text, etc.
                for (int x = 0; x < 2; x++)
                {
                    Console.WriteLine("");
                }

                //Show the cursor again so the user can do what you need them to do.
                Console.CursorVisible = true;

            }
        }

        private static void AnimatedAverage(Object source, ElapsedEventArgs e)
        {
            //Setting the bar graph colors
            var myBackgroundColor = ConsoleColor.Gray;
            var myBarGraphColor = ConsoleColor.Yellow;

            //Add one to the timer counter every time it elapses
            myTimerCounter++;

            //Random number for the bar graph animation
            Random myRandomNumber = new Random();

            //This randomly selects a number from 0 to 10 and assigns it to a variable.
            var theRating = myRandomNumber.Next(0, 11);

            //Set color for the bar graph
            Console.BackgroundColor = myBarGraphColor;

            //Create bar graph, not the bar graph background
            for (int ii = 0; ii <= theRating; ii++)
            {
                //This creates a colored bar graph of spaces
                Console.Write(" ");
            }

            //Set total number for the length of the bar graph, which is also the background
            int myTotalNumber = 10;

            //Set bar graph background color
            Console.BackgroundColor = myBackgroundColor;

            //Draw bar graph background
            for (int iii = theRating; iii <= myTotalNumber; iii++)
            {
                //This creates a colored background of spaces
                Console.Write(" ");
            }

            //Move the cursor back to the left, where it started to draw the animation to begin with.
            Console.CursorLeft = 0;

            //After a bit of time, stop the animation
            if (myTimerCounter >= 20)
            {
                //Stop Timer
                myAnimationTimer.Stop();

                //Once the animation is over, redraw the bar graph one more time with the actual bar graph data
                //Set color for the bar graph
                Console.BackgroundColor = myBarGraphColor;

                //Create bar graph, not the bar graph background
                for (int ii = 0; ii <= reviewScore; ii++)
                {
                    //This creates a colored bar graph of spaces
                    Console.Write(" ");
                }

                //Set bar graph background color
                Console.BackgroundColor = myBackgroundColor;

                //Draw bar graph background
                for (int iii = reviewScore; iii <= myTotalNumber; iii++)
                {
                    //This creates a colored background of spaces
                    Console.Write(" ");
                }

                //Move the cursor down and away from the artwork/bar graph to have menu options, text, etc.
                for (int x = 0; x < 2; x++)
                {
                    Console.WriteLine("");
                }

                //Show the cursor again so the user can do what you need them to do.
                Console.CursorVisible = true;

            }
        }

        private static void AnimatedBad(Object source, ElapsedEventArgs e)
        {
            //Setting the bar graph colors
            var myBackgroundColor = ConsoleColor.Gray;
            var myBarGraphColor = ConsoleColor.Red;

            //Add one to the timer counter every time it elapses
            myTimerCounter++;

            //Random number for the bar graph animation
            Random myRandomNumber = new Random();

            //This randomly selects a number from 0 to 10 and assigns it to a variable.
            var theRating = myRandomNumber.Next(0, 11);

            //Set color for the bar graph
            Console.BackgroundColor = myBarGraphColor;

            //Create bar graph, not the bar graph background
            for (int ii = 0; ii <= theRating; ii++)
            {
                //This creates a colored bar graph of spaces
                Console.Write(" ");
            }

            //Set total number for the length of the bar graph, which is also the background
            int myTotalNumber = 10;

            //Set bar graph background color
            Console.BackgroundColor = myBackgroundColor;

            //Draw bar graph background
            for (int iii = theRating; iii <= myTotalNumber; iii++)
            {
                //This creates a colored background of spaces
                Console.Write(" ");
            }

            //Move the cursor back to the left, where it started to draw the animation to begin with.
            Console.CursorLeft = 0;

            //After a bit of time, stop the animation
            if (myTimerCounter == 50)
            {
                //Stop Timer
                myAnimationTimer.Stop();

                //Once the animation is over, redraw the bar graph one more time with the actual bar graph data
                //Set color for the bar graph
                Console.BackgroundColor = myBarGraphColor;

                //Create bar graph, not the bar graph background
                for (int ii = 0; ii <= reviewScore; ii++)
                {
                    //This creates a colored bar graph of spaces
                    Console.Write(" ");
                }

                //Set bar graph background color
                Console.BackgroundColor = myBackgroundColor;

                //Draw bar graph background
                for (int iii = reviewScore; iii <= myTotalNumber; iii++)
                {
                    //This creates a colored background of spaces
                    Console.Write(" ");
                }

                //Move the cursor down and away from the artwork/bar graph to have menu options, text, etc.
                for (int x = 0; x < 2; x++)
                {
                    Console.WriteLine("");
                }

                //Show the cursor again so the user can do what you need them to do.
                Console.CursorVisible = true;

            }
        }

        private static void PrintCommands()
        {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Black;

            Console.WriteLine("[1] Show Average of Reviews for Restaurants");
            Console.WriteLine("[2] Dinner Spinner");
            Console.WriteLine("[3] Top 10 Restaurants");
            Console.WriteLine("[4] Back to Main Menu");
        }
    }
}
