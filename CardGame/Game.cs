using System;
using System.Collections.Generic;
using System.Linq;

namespace CardGame
{
    class Game
    {
        
        public List<string> cards = new List<string>() { "Y1", "Y2", "Y3", "Y4", "Y5", "R1", "R2", "R3", "R4",
            "R5", "B1", "B2", "B3", "B4", "B5", "SC", "SC", "SC" };

        public Player[] players = new Player[3]; 

        public static string curCard;

        public static int turnCount = 0;
        public static int pasCount = 0;

        Random random = new Random();

        // Deals the cards to the players
        public void Deal() 
        {
            for(int i = 0; i < 3; i++)
            {
                for(int j = 0; j < 6; j++)
                {
                    int rand = random.Next(0, cards.Count);
                    players[i].hand.Add(cards[rand]);
                    cards.RemoveAt(rand);
                }
            }
        }

        // Creates players and setup play order.
        public void PreparePlayers()
        {
            Player player1 = new Player();
            player1.playerName = "Player1"; // Controled by player

            Player player2 = new Player();
            player2.playerName = "Player2"; // Controled by CPU

            Player player3 = new Player();
            player3.playerName = "Player3"; // Controled by CPU

            players[0] = player1;   
            players[1] = player2;   
            players[2] = player3;   

            PlayOrder();
        }

        // Determines who starts first and play order.
        public void PlayOrder() 
        {
            int rand = random.Next(0, 3);
      
            players = players.OrderBy(x => random.Next()).ToArray();

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(players[0].playerName + " Game Begins! \n");
        }
    }
}
