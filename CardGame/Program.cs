using System;
using System.Collections.Generic;

namespace CardGame
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Game game = new Game();
            Player player1;

            string input = "";
            bool nextTurn = false;
            bool isGameOver = false;

            game.PreparePlayers(); 
            game.Deal(); 
            

            while (true) // Main game loop
            {
                foreach (var player in game.players) // Turn-based game loop
                {
                    SetTextColor(player.playerName);
                    Game.turnCount++;

                    if(player.playerName != "Player1") // CPU's turn
                    {
                        player.PlayerTurn();
                    }
                    else // Player's turn
                    {
                        Console.WriteLine("Your Turn.");
                        player1 = player; 

                        ShowHand(player1.hand);

                        if (Game.curCard != null) // Shows current card
                        {
                            Console.WriteLine("Current card: " + Game.curCard);
                        }

                        Console.WriteLine("Which card will you play? (type 'pass' to pass your turn):");
                        PlayCard();
                        player1.hand.Remove(input);

                    }
                    nextTurn = false;

                    Console.WriteLine("------------------------------------------------------------------------ \n");

                    if (CheckWinner(player)) // Checks if there is a winner and makes game over if so.
                    {   
                        isGameOver = true;
                        break;
                    }
                }
               
                if (isGameOver) // Break main loop
                {
                    Console.ReadLine();
                    break;
                }
            }

            // Plays a move according to player's input
            void PlayCard()
            {
                if (!nextTurn) 
                {
                    input = Console.ReadLine().ToUpper();
                    CheckInput(input);

                    if (input == "SC") 
                    {
                        Game.pasCount = 0;
                        ChangeColor();
                        nextTurn = true;
                        
                    }
                    else if (input == "PASS") 
                    {
                        Game.pasCount++;
                        Console.WriteLine("You passed your turn.");
                        nextTurn = true;
                    }
                    else 
                    {
                        Game.pasCount = 0;
                        Console.WriteLine("You played {0}", input);
                        Game.curCard = input;
                        nextTurn = true;
                    }
                }
            }

            // Checks for right input
            void CheckInput(string card) 
            {
                while (true)
                {
                    if (Game.curCard == null) // Checks input for the first turn in the game
                    {
                        if (!player1.hand.Contains(card))
                        {
                            if (card == "PASS")
                            {
                                input = card;
                                break;
                            }
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Please choose a card in your hand:");
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            card = Console.ReadLine().ToUpper();
                            continue;
                        }
                        else 
                        {
                            if(card == "SC") 
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("You can't play special card on your first turn. Please choose a different card:");
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                card = Console.ReadLine().ToUpper();
                                continue;
                            }
                            else 
                            {
                                input = card;
                                break;
                            }
                            
                        }
                    }
                    else // Check input for the rest of the turns
                    {
                        if (card.StartsWith(Game.curCard[0]) || card.EndsWith(Game.curCard[1]) || (card == "SC" && player1.hand.Contains(card)))
                        {
                            input = card;
                            break;
                        }
                        else if (card == "PASS")
                        {
                            input = card;
                            break;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Invalid input. Please choose a  valid card according to rules"); 
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            card = Console.ReadLine().ToUpper();
                            continue;
                        }
                    }
                    
                }
            }

            // Changes the color of the current card to selected color
            void ChangeColor() 
            {
                Console.WriteLine("What color will you change the color of current card(Y, B, R):");
                input = Console.ReadLine().ToUpper(); 

                while (true)
                {
                    if (input != "Y" && input != "B" && input != "R")
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Please choose a valid color;(S, M, K):");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        input = Console.ReadLine().ToUpper();
                    }
                    else
                    {
                        Console.WriteLine("{0} transformed to {1} ", Game.curCard, input + Game.curCard[1]);
                        Game.curCard = input + Game.curCard[1];
                        player1.hand.Remove("SC");
                        break;
                    }
                }
            }

            // Shows the player's cards
            void ShowHand(List<string> hand) 
            {
                Console.WriteLine("Your cards;");
                foreach (var card in hand)
                {
                    Console.Write(card + " ");
                }
                Console.WriteLine();
            }


            // Checks if there is a winner, if so return true
            bool CheckWinner(Player player) 
            {
                
                if (player.hand.Count == 0)
                {
                    if(player.playerName == "Player1")
                    {
                        Console.WriteLine("Congrats! You won.");
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("{0} won the game.", player.playerName);
                        return true;
                    }
                }
                else
                {
                    if (Game.pasCount == 3)  // Declares draw if all players passes thier turn 
                    {
                        Console.WriteLine("All players passed thier turn game is draw.");
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                    
                }
            }

            // Sets different text color for each player
            void SetTextColor(string playerName) 
            {
                if (playerName == "Player1")
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                }
                else if (playerName == "Player2")
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                }
            }
        }
    }
}
