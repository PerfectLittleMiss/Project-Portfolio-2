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
            //Create a dictionary to hold the player names and numbers
            Dictionary<string, int> playerNamesAndNumbers = new Dictionary<string, int>();

            // Get the players with their data
            List<Player> players = DealDeck();

            int playerNumber = 1;
            foreach(Player player in players)
            {
                playerNamesAndNumbers.Add(player.GetName(), playerNumber);
                playerNumber++;
            }

            players.Sort((x, y) => x.totalValue.CompareTo(y.totalValue));

            string cards;

            foreach(string card in players[])

            Console.WriteLine("1st Place: Player {0} - {1}: {2} Score: {3}", playerNamesAndNumbers[players[0].GetName()], players[0].GetName(), players[0].GetHand(), players[0].totalValue);

            Console.WriteLine("Press any key to return to the main menu...");
            Console.ReadKey();
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

        private static List<Player> DealDeck()
        {
            // create a list to hold the player names
            List<string> playerNames = DatabaseFunctions.GetPlayers();

            // get the deck
            Dictionary<string, int> cards = CreateDeck();

            // Create a list to hold the players themselves
            List<Player> players = new List<Player>();


            Random rand = new Random();
            //convert the keys of the cards dictionary to a list
            List<string> keys = cards.Keys.ToList();

            foreach (string name in playerNames)
            {
                // create a new Player
                Player newPlayer = new Player(name);
                //create a list to hold the cards for the player
                List<string> playerCards = new List<string>();
                // create an int to hold the value of all the cards
                int valueOfCards = 0;

                for (int i = 0; i < 13; i++)
                {
                    int randomNumber = rand.Next(0, keys.Count);
                    string cardName = keys[randomNumber];
                    int cardValue = cards[cardName];
                    keys.Remove(cardName);

                    playerCards.Add(cardName);
                    valueOfCards += cardValue;
                }

                newPlayer.SetHand(playerCards);
                newPlayer.totalValue = valueOfCards;

                players.Add(newPlayer);
            }

            return players;
        }
    }
}
