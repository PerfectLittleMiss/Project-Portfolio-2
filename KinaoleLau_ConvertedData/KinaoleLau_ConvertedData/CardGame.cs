using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinaoleLau_ConvertedData
{
    class CardGame
    {
        public static void Menu()
        {
            //bool to determine if running
            bool running = true;

            while(running)
            {
                // create a list to hold the players
                List<string> players = DatabaseFunctions.GetPlayers();

                // create dictionary to hold the players with their scores
                Dictionary<int, string> playersAndScores = new Dictionary<int, string>();

                // get the deck
                Dictionary<string, int> cards = CreateDeck();


            }
        }

        // create the card deck
        private static Dictionary<string, int> CreateDeck()
        {
            //create the dictionary to hold the card names and point values
            Dictionary<string, int> cards = new Dictionary<string, int>();

            //Loop through adding the cards so each type only needs to be typed out once
            for(int i = 0; i < 4; i++)
            {
                string suit = null;

                if(i == 0)
                {
                    suit = " of hearts";
                }
                else if(i == 1)
                {
                    suit = " of spades";
                }
                else if (i == 2)
                {
                    suit = " of diamonds";
                }
                else
                {
                    suit = " of clubs";
                }
                
                cards.Add("2" + suit, 2);
                cards.Add("3" + suit, 3);
                cards.Add("4" + suit, 4);
                cards.Add("5" + suit, 5);
                cards.Add("6" + suit, 6);
                cards.Add("7" + suit, 7);
                cards.Add("8" + suit, 8);
                cards.Add("9" + suit, 9);
                cards.Add("10" + suit, 10);
                cards.Add("Jack" + suit, 12);
                cards.Add("Queen" + suit, 12);
                cards.Add("King" + suit, 12);
                cards.Add("Ace" + suit, 15);
            }

            return cards;
        }
    }
}
