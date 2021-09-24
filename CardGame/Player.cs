using System;
using System.Collections.Generic;
using System.Linq;

namespace CardGame
{
    class Player
    {
        public string playerName;
        public string input;

        public List<string> hand = new List<string>();

        private List<string> validCards = new List<string>();
        private List<char> validColors = new List<char>();

        Random random = new Random();
        public void PlayerTurn()
        {
            
            if(playerName != "Player1") // Plays the CPU if it is not player's turn.
            {
                Console.WriteLine("{0}'s turn", playerName);

                System.Threading.Thread.Sleep(1100); // Small delay for realism purposes

                Move();

                Console.WriteLine("{0} has {1} cards.", playerName, hand.Count());
            }
        }

        // Finds the valid cards for CPU
        private void FindValidCards() 
        {
            foreach (var card in hand)
            {
                if(Game.curCard != null) // Finds valid cards if it is not first turn
                {
                    if (card.StartsWith(Game.curCard[0]) || card.EndsWith(Game.curCard[1]))
                    {
                        validCards.Add(card);
                    }
                    else if (card == "SC")
                    {
                        if (validCards.Count != 0)
                        {
                            break;
                        }
                        else
                        {
                            validCards.Add(card);
                        }
                    }
                }
                else // Finds a valid card for first turn
                {       
                    if (card != "SC")
                    {
                        validCards.Add(card);
                    }
                }
            }
        }

        // Determines proper input for CPU
        private string Input()
        {
            if (validCards.Count == 0) // Passes the turn if no valid vard available
            {
                return "pass";
            }
            else // Chooses a valid vard
            {
                int rand = random.Next(0, validCards.Count);

                return hand.Find(card => card == validCards[rand]);
            }
        }

        public void Move() // Plays a move for CPU according to input 
        {
            FindValidCards();
            input = Input();

            if(input == "pass")
            {
                Game.pasCount++;
                Console.WriteLine("Passes");
            }
            else if(input == "SC") 
            {
                Game.pasCount = 0;
                ChangeColor();
            }
            else
            {
                Game.pasCount = 0;
                Game.curCard = input;
                Console.WriteLine("{0} played {1}.", playerName, input);
                hand.Remove(input);
            }
            System.Threading.Thread.Sleep(1000); // Small delay for realism purposes
            validCards.Clear();
        }

        // Changes the current card's to the choosen color for CPU
        public void ChangeColor()
        {
            validColors.Clear();
            List<string> rd = new List<string>() { "SC" };
            if (!hand.Except(rd).Any()) // Changes the current card's to a random color if CPU has only 'RD' card
            {   

                validColors.Add('Y');
                validColors.Add('B');
                validColors.Add('R');

                int rand = random.Next(0, 3);
                Console.WriteLine("{0} transformed {1} to {2}.",playerName, Game.curCard, validColors[rand].ToString() + Game.curCard[1]);
                Game.curCard = validColors[rand].ToString() + Game.curCard[1];
                hand.Remove("SC");

            }
            else 
            {
                foreach (var card in hand)
                {
                    if (card != "SC")
                    {
                        if (!validColors.Contains(card[0])) {
                            validColors.Add(card[0]);
                        }
                    }
                }

                int rand = random.Next(0, validColors.Count);
                Console.WriteLine("{0} transformed {1} to {2}.", playerName, Game.curCard, validColors[rand].ToString() + Game.curCard[1]);
                Game.curCard = validColors[rand].ToString() + Game.curCard[1];
                hand.Remove("SC");
            }
        }


    }
}
