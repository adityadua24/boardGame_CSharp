using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Player_Class_Library;
using Board_Class_Library;
using System.Drawing;
using Square_Class_Library;
using Die_Class_Library;
using System.Diagnostics;

namespace HareAndTortoise
{
    public static class HareAndTortoise_Game
    {

        private static String[] defaultName = { "One", "Two", "Three", "Four", "Five", "Six" };
        private static Brush[] colours = { Brushes.Black, Brushes.Red, Brushes.Gold, Brushes.GreenYellow, Brushes.Fuchsia, Brushes.BlueViolet };
        private static BindingList<Player> players = new BindingList<Player>();
        public static int numberOfPlayers = 6;
        private static Die die1 = new Die();
        private static Die die2 = new Die();

        public static BindingList<Player> Players
        {
            get
            {
                return players;
            }
        }
        public static void setUpPlayers()
        {
            for (int i = 0; i < numberOfPlayers; i++)
            {
                Player player = new Player(defaultName[i], Board.StartSquare());
                player.PlayerTokenColour = colours[i];
                players.Add(player);
            }
        }

        public static void SetUpGame()
        {

            Board.SetUpBoard();
            setUpPlayers();
        }// end SetUpGame


        /// <summary>
        /// rolls dice for every player and performs winner check when one of the players is on Final Square
        /// </summary>
        public static void playOneRound()
        {
            bool hasReachedFinal = false;
            for (int i = 0; i < numberOfPlayers; i++)
            {
                players[i].LastLocation = players[i].Location;
                players[i].RollDice(die1, die2);
                if (players[i].Location.Number == 55)
                {
                    hasReachedFinal = true;
                }
            }
            if (hasReachedFinal == true)
            {
                winnerCheck();
            }

        }
        public static void OutputAllPlayerDetails()
        {
            for (int i = 0; i < numberOfPlayers; i++)
            {
                OutputIndividualDetails(Players[i]);
            }
        } // end OutputAllPlayerDetails
        /// <summary>
        /// Outputs a player's current location and amount of money
        /// pre: player's object to display
        /// post: displayed the player's location and amount
        /// </summary>
        /// <param name="who">the player to display</param>
        public static void OutputIndividualDetails(Player who)
        {
            Square playerLocation = who.Location;
            Trace.WriteLine(String.Format("Player {0} on square {1} with {2:C}",
            who.Name, playerLocation.Name, who.Money));
        }// end OutputIndividualDetails
        
        /// <summary>
        /// loops through players finds one with max money
        /// loops again to find player with same money but advanced location and declares that player winner (if finds any).
        /// if same money same location player is found then declares that winner too
        /// </summary>
        public static void winnerCheck()
        {
            Player winner;
            winner = players[0];
            for (int i = 0; i < numberOfPlayers; i++)
            {
                if (players[i].Money > winner.Money)
                {
                    winner = players[i];
                }
            }
            for (int i = 0; i < numberOfPlayers; i++)
            {
                if (players[i].Money == winner.Money)
                {
                    if (players[i].Location.Number > winner.Location.Number)
                    {
                        for (int x = 0; x < numberOfPlayers; x++)
                        {
                            players[x].HasWon = false;
                        }
                        winner = players[i];

                    }
                    else if (players[i].Location.Number == winner.Location.Number)
                    {
                        players[i].HasWon = true;
                        winner.HasWon = true;
                    }
                }
            }
        }
       
        /// <summary>
        /// Checks if any player has won yet
        /// </summary>
        /// <returns>return true if yes, false if no</returns>
        public static bool endCheck()
        {
            for (int i = 0; i < numberOfPlayers; i++)
            {
                if (players[i].HasWon == true)
                {
                    return true;
                }
            }
            return false;
        }

    }//end class
}//end namespace
