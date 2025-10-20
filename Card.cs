using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    public class Card
    {
        public string Name { get; set; }
        public int Value { get; set; }
        public string Suit { get; set; }
        public bool IsAce => Name == "Ace";
        

        public Card(string name, int value, string suit)
        {
            Name = name;
            Value = value;
            Suit = suit;
        }

        public override string ToString()
        {
            return $"{Name} of {Suit}";
        }
    }
}