using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinaoleLau_ConvertedData
{
    class Player
    {
        private string name;
        private List<string> hand;
        private int totalValue;

        public Player(string _name)
        {
            name = _name;
        }

        public List<string> GetHand()
        {
            return hand;
        }
        public void SetHand(List<string> _hand)
        {
            hand = _hand;
        }

        public int GetValue()
        {
            return totalValue;
        }
        public void SetValue(int value)
        {
            totalValue = value;
        }
    }
}
