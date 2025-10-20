using System;
using System.Collections.Generic;
using System.Globalization;

namespace Blackjack
{
    class Program
    {
        static void Main(string[] args)
        {
            string play = "";
            Console.WriteLine("Welcome to BlackJack");
            Console.WriteLine("All your gambling needs can be accomplished");
            Console.Write("\nWould you like to play? (Y/N) : ");
            play = Console.ReadLine().ToUpper();

            Game game = new Game();


            while (play != "N")
            {
                
                game.Start();
                

                Console.Write("\nWould you like to play again? (Y/N) : ");
                play = Console.ReadLine();
            }
        }  
    }
}
