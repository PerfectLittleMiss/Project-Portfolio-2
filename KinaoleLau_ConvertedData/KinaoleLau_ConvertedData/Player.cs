using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinaoleLau_ConvertedData
{
    public class Player
    {
        private string name;
        private List<string> hand;
        public int totalValue { get; set; }

        public Player(string _name)
        {
            name = _name;
        }

        public string GetName()
        {
            return name;
        }

        public List<string> GetHand()
        {
            return hand;
        }
        public void SetHand(List<string> _hand)
        {
            hand = _hand;
        }
    }
}
