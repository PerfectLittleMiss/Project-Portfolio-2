using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinaoleLau_ConvertedData
{
    class Card
    {
        public string suit { get; set; }
        public string name { get; set; }
        public int points { get; set; }
        public string color { get; set; }
        public string background = "White";

        public Card(string suit, string name, int points, string color)
        {

        }
    }
}
