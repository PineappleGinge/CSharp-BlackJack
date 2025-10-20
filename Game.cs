using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Blackjack
{
    public class Game
    {
        
        // making the hands
        private List<Card> playerHand;
        private List<Card> dealerHand;
        private Deck deck;
        

        public int chips = 5000;
        public int chipNum;

        
        public Game()
        {
            playerHand = new List<Card>();
            dealerHand = new List<Card>();
            deck = new Deck();
        }

        // for gambling
        public static int Gamble(Game game)
        {
            
            Console.Write($"How much are willing to gamble? You have {game.chips} to use : ");
            game.chipNum = int.Parse(Console.ReadLine());
            checkingChips(game);
            game.chips -= game.chipNum;
            Console.WriteLine($"You have chosen to gamble {game.chipNum} chips you have {game.chips} chips left!");

            return game.chips;
        }



        // this checks thaf you dont enter chips out the range of what you have
        // it also checks if you have 0 chips and asks to enter your bank details 
        public static void checkingChips(Game game)
        {
            while (game.chipNum > game.chips || game.chipNum < game.chips)
            {
                Console.WriteLine("You are betting out of your balance!");
                Console.Write($"How much are willing to gamble? You have {game.chips} to use : ");
                game.chipNum = int.Parse(Console.ReadLine());
               
                if (game.chips == 0)
                {
                    Console.WriteLine("\nYour balance is now 0");
                    Console.WriteLine("To continue please enter your credit card information");
                    Console.Write("Do yuo want to continue? (Y/N) : ");
                    string Next = Console.ReadLine().ToUpper();
                    
                    if (Next == "Y")
                    {
                        CardDeatiils();
                        break;

                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

//----------------------------------------------------
        public static void CardDeatiils()
        {
            Console.WriteLine("=== Credit Card Details Input ===");

            // Cardholder Name
            Console.Write("Enter Cardholder Name: ");
            string cardholderName = Console.ReadLine();

            // Card Number
            Console.Write("Enter Card Number (16 digits): ");
            string cardNumber = Console.ReadLine();

            // Expiry Date
            Console.Write("Enter Expiry Date (MM/YY): ");
            string expiryDate = Console.ReadLine();

            // CVV
            Console.Write("Enter CVV (3 or 4 digits): ");
            string cvv = Console.ReadLine();

            // Validate inputs
            if (IsValidInput(cardholderName, cardNumber, expiryDate, cvv))
            {
                Console.WriteLine("\nCredit card details submitted successfully!");
            }
            else
            {
                Console.WriteLine("\nInvalid credit card details. Please try again.");
            }
        }

        static bool IsValidInput(string cardholderName, string cardNumber, string expiryDate, string cvv)
        {
            // Cardholder Name validation
            if (string.IsNullOrWhiteSpace(cardholderName) || cardholderName.Length < 3)
            {
                Console.WriteLine("Invalid Cardholder Name.");
                return false;
            }

            // Card Number validation (16 digits)
            if (!Regex.IsMatch(cardNumber, @"^\d{16}$"))
            {
                Console.WriteLine("Invalid Card Number.");
                return false;
            }

            // Expiry Date validation (MM/YY)
            if (!Regex.IsMatch(expiryDate, @"^(0[1-9]|1[0-2])\/\d{2}$"))
            {
                Console.WriteLine("Invalid Expiry Date. Use MM/YY format.");
                return false;
            }

            // CVV validation (3 or 4 digits)
            if (!Regex.IsMatch(cvv, @"^\d{3,4}$"))
            {
                Console.WriteLine("Invalid CVV.");
                return false;
            }

            return true;
        }
 //----------------------------------------------------


        // the main program that runs
        public void Start()
        {
            Console.Write("\nWould you like to gamble? (Y/N) : ");
            string choice = Console.ReadLine().ToUpper();

            if (choice == "Y")
            {
                Gamble(this);
                Gameplay();
            }
            else if (choice == "N") 
            {
                Console.WriteLine("\nChicken");
                Gameplay();
            }
            else if (choice != "Y" &&  choice != "N")
            {
                Console.WriteLine("Invalid Option");
                Gameplay();
            }
            
        }

        // main components for the game in 1 method
        private void Gameplay()
        {
            AddCard(playerHand, dealerHand, deck);

            Console.WriteLine("\nYour Hand:");
            DisplayHand(playerHand);
            Console.WriteLine($"Your Score : {ScoreCalculate(playerHand)}");

            Console.WriteLine("\nDealers Hand");
            Console.WriteLine(dealerHand[0]); // shows dealer 1st card
            Console.WriteLine("[Hidden Card]"); // keeps second card hidden

            PlayerTurn();
            if (ScoreCalculate(playerHand) > 21)
            {
                Console.WriteLine("\nYou Busted, Dealer Wins");
                return;
            }

            DealerTurn();
            Console.WriteLine("\nDealers Hand.");
            DisplayHand(dealerHand);

            Win_Losr_Tie();
        }

        // adds cards to the hands
        private void AddCard(List<Card>playerHand, List<Card>dealerHand, Deck deck)
        {
            playerHand.Add(deck.DealCard());
            dealerHand.Add(deck.DealCard());
            playerHand.Add(deck.DealCard());
            dealerHand.Add(deck.DealCard());
        }

        // players turn
        private void PlayerTurn()
        {
            while (ScoreCalculate(playerHand) <= 21)
            {
                Console.Write("\nDo you want to (H)it or (S)tand : ");
                string hit_stand = Console.ReadLine().ToUpper();
                
                if (hit_stand == "H")
                {
                    playerHand.Add(deck.DealCard());
                    Console.WriteLine("You Drew");
                    DisplayHand(new List<Card> { playerHand[^1] });
                    Console.WriteLine($"Your Score: {ScoreCalculate(playerHand)} ");
                }
                else if (hit_stand == "S")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid Option.");
                    PlayerTurn();
                }
            }
        }
        
        // dealers turn
        private void DealerTurn()
        {
            Console.WriteLine("\nDealers Turn.");
            DisplayHand(dealerHand);

            while(ScoreCalculate(dealerHand) < 17)
            {
                dealerHand.Add(deck.DealCard());
                Console.WriteLine("Dealer drew:");
                DisplayHand(new List<Card> { dealerHand[^1] }); // the ^1 is another way of saying (example.Count - 1)

            }
        }

        // decides the outcome of the game
        private void Win_Losr_Tie()
        {
            int playerScore = ScoreCalculate(playerHand);
            int dealerScore = ScoreCalculate(dealerHand);

            Console.WriteLine($"\nYour Score {playerScore}");
            Console.WriteLine($"\nDealers Score {dealerScore}");

            if (dealerScore > 21 || playerScore > dealerScore)
            {
                Console.WriteLine("You Win");
                chips += chipNum * 2;
                Console.WriteLine($"You gambled {chipNum} chips, now you have {chips} chips");
            }
            else if (playerScore < dealerScore)
            {
                Console.WriteLine("Dealer Win");
                Console.WriteLine($"You gambled {chipNum} chips, now you have {chips} chips");
            }
            else
            {
                Console.WriteLine("Both Drew");
                chips += chipNum;
                Console.WriteLine($"You have {chips} chips, you drew so nothing changed");
            }
        }

        // loops through the hand and displays them
        private static void DisplayHand(List<Card> hand)
        {
            foreach (var card in hand)
            {
                Console.WriteLine(card);
            }
        }

        // calculates the score
        private static int ScoreCalculate(List<Card> hand)
        {
            int total = 0;
            int aceCount = 0;


            foreach (Card card in hand)
            {
                total += card.Value;
                if (card.IsAce)
                {
                    aceCount++;
                }

            }

            while (total > 21 && aceCount > 0)
            {
                total -= 10;
                aceCount--;
            }

            return total;
        } 
    }
}