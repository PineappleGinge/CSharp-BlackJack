using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    public class Deck
    {
        List<Card> deck = new List<Card>();

        public Deck()
        {
            
            string[] card_ranks = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King", "Ace" };
            int[] card_value = { 2, 3, 4, 5, 6, 7, 8, 9, 10, 10, 10, 10, 11 };
            string[] suits = { "Hearts", "Diamonds", "Clubs", "Spades" };

            foreach (var suit in suits)
            {
                for (int i = 0; i < card_ranks.Length; i++)
                {
                    deck.Add(new Card(card_ranks[i], card_value[i], suit));
                }
            }
        }

        
        public Card DealCard()
        {
            Random rng = new Random();
            int randomNumber = rng.Next(0, deck.Count);
            Card randomCard = deck[randomNumber];
            deck.Remove(randomCard);
            return randomCard;
        }
    }
}