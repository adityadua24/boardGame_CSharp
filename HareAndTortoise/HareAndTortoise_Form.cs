using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Board_Class_Library;
using Square_Class_Library;
using Player_Class_Library;
using System.Diagnostics;

namespace HareAndTortoise
{
    public partial class HareAndTortoise_Form : Form
    {

        const int NUM_OF_ROWS = 8;
        const int NUM_OF_COLUMNS = 7;


        public HareAndTortoise_Form()
        {
            InitializeComponent();
            HareAndTortoise_Game.SetUpGame();
            ResizeGameBoard();
            SetUpGuiGameBoard();
            dataGridView1.DataSource = HareAndTortoise_Game.Players;
            MapPlayers(true);
            Trace.Listeners.Add(new ListBoxTraceListener(listBox));
            comboBox1.SelectedIndex = comboBox1.FindString("6");
        }

        private void SetUpGuiGameBoard()
        {

            for (int i = Board.START_SQUARE; i <= Board.END_SQUARE; i++)
            {
                Square sq = Board.GetGameBoardSquare(i);
                SquareControl squareControl = new SquareControl(sq, HareAndTortoise_Game.Players);
                if (sq.Number == Board.START_SQUARE || sq.Number == Board.END_SQUARE)
                {
                    squareControl.BackColor = Color.BurlyWood;
                }
                else
                {
                    //Donot Set colour
                }
                int row;
                int column;
                MapSquareToTablePanel(i, out row, out column);
                gameBoardPanel.Controls.Add(squareControl, column, row);

            }

        }//end SetUpGuiGameBoard()

        private static void MapSquareToTablePanel(int number, out int row, out int column)
        {
            row = 0;
            column = 0;
            column = (number % 7);
            row = (number / 7);
            if ((row % 2) != 0)
            {
                column = 6 - (number % 7);
            }
            else
            {
                //Do Nothing
            }
            row = 7 - row;

        }

        private void ResizeGameBoard()
        {
            const int SQUARE_SIZE = SquareControl.SQUARE_SIZE;
            int currentHeight = gameBoardPanel.Size.Height;
            int currentWidth = gameBoardPanel.Size.Width;
            int desiredHeight = SQUARE_SIZE * NUM_OF_ROWS;
            int desiredWidth = SQUARE_SIZE * NUM_OF_COLUMNS;
            int increaseInHeight = desiredHeight - currentHeight;
            int increaseInWidth = desiredWidth - currentWidth;
            this.Size += new Size(increaseInWidth, increaseInHeight);
            gameBoardPanel.Size = new Size(desiredWidth, desiredHeight);
        } //end ResizeGameBoard

        /// <summary>
        /// gets square control objects of every player based on their location 
        /// and makes them visible
        /// by changing ContainsPlayer property value for that particular player
        /// </summary>
        /// <param name="status"> True status makes playerTokens visible whereas False make them invisible</param>
        public void MapPlayers(bool status)
        {
            BindingList<Player> playerList;
            playerList = HareAndTortoise_Game.Players;
            for (int i = 0; i < HareAndTortoise_Game.numberOfPlayers; i++)
            {
                Square location = playerList[i].Location;
                int row, column;
                int number = location.Number;
                MapSquareToTablePanel(number, out row, out column);
                SquareControl sqControl = (SquareControl)gameBoardPanel.GetControlFromPosition(column, row);
                if (status == true)
                {
                    sqControl.ContainsPlayers[i] = true;
                    gameBoardPanel.Invalidate(true);
                }
                else if (status == false)
                {
                    sqControl.ContainsPlayers[i] = false;
                    gameBoardPanel.Invalidate(true);
                }
            }
        }
        /// <summary>
        /// Moves playerTokens one by one and square by square from their previous location to the new one.  
        /// This function had to be made more complex just to tackle the situation when after a roll dice a player moves few positions back rather than forward.
        /// </summary>
        public void MapOnePlayer()
        {
            BindingList<Player> playerList;
            playerList = HareAndTortoise_Game.Players;
            for (int i = 0; i < HareAndTortoise_Game.numberOfPlayers; i++)
            {
                int a = playerList[i].LastLocation.Number;
                int b = playerList[i].Location.Number;
                int counts = System.Math.Abs(a - b);
                while (counts != 0)
                {
                    int row, column;
                    MapSquareToTablePanel(a, out row, out column);
                    SquareControl sqControl = (SquareControl)gameBoardPanel.GetControlFromPosition(column, row);
                    sqControl.ContainsPlayers[i] = false;
                    gameBoardPanel.Invalidate(true);
                    Application.DoEvents();
                    int y;
                    if (a < b)  // moves y one step towards current Location by adding to a
                    {
                        y = a + 1;
                    }
                    else        // moves y one step towards current Location by subtracting from  a
                    {
                        y = a - 1;
                    }
                    MapSquareToTablePanel(y, out row, out column);
                    sqControl = (SquareControl)gameBoardPanel.GetControlFromPosition(column, row);
                    sqControl.ContainsPlayers[i] = true;
                    gameBoardPanel.Invalidate(true);
                    Application.DoEvents();
                    if (a < b)         // This if else decides to keep increasing the counter or decreasing
                    {
                        a++;
                    }
                    else
                    {
                        a--;
                    }
                    counts--;
                }

            }

        }

        private void buttonRollDice_Click(object sender, EventArgs e)
        {
            RollDiceActions();

        }
        private void RollDiceActions()
        {
            comboBox1.Enabled = false;
            HareAndTortoise_Game.playOneRound();
            buttonRollDice.Enabled = false;
            MapOnePlayer();
            MapPlayers(false);
            MapPlayers(true);
            if (HareAndTortoise_Game.endCheck())
            {
                comboBox1.Enabled = true;
                buttonRollDice.Enabled = false;
            }
            else
            {
                buttonRollDice.Enabled = true;
            }
            OutputPlayersDetails();
            UpdateDataGridView();
        }
        private void OutputPlayersDetails()
        {
            HareAndTortoise_Game.OutputAllPlayerDetails();
            listBox.Items.Add("");
            listBox.SelectedIndex = listBox.Items.Count - 1;
        }
        private void UpdateDataGridView()
        {
            HareAndTortoise_Game.Players.ResetBindings();
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            var exitButton = MessageBox.Show("Do you really want to exit", " Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (exitButton == DialogResult.Yes)
            {
                this.Close();

            }
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            MapPlayers(false);
            ResetGame();
            MapPlayers(true);
            dataGridView1.Refresh();
            comboBox1.Enabled = true;
            buttonRollDice.Enabled = true;
            gameBoardPanel.Invalidate(true);
        }


        /// <summary>
        /// resets money, location and hasWon values for every player 
        /// that has been modified ( Affected by roll dice actions)
        /// </summary>
        public static void ResetGame()
        {
            BindingList<Player> playerList;
            playerList = HareAndTortoise_Game.Players;
            for (int i = 0; i < HareAndTortoise_Game.numberOfPlayers; i++)
            {
                playerList[i].Location = Board.StartSquare();
                playerList[i].Money = 100; //100 is default Money value
                playerList[i].HasWon = false;
            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            MapPlayers(false);
            int getIndex = comboBox1.SelectedIndex;
            PlayersPlaying(getIndex);
            MapPlayers(true);
            gameBoardPanel.Invalidate(true);


        }

        private void PlayersPlaying(int index)
        {
            int playersOn;
            switch (index)
            {
                case 0: playersOn = 2;
                    break;
                case 1: playersOn = 3;
                    break;
                case 2: playersOn = 4;
                    break;
                case 3: playersOn = 5;
                    break;
                case 4: playersOn = 6;
                    break;
                default: playersOn = 6;
                    break;
            }
            HareAndTortoise_Game.numberOfPlayers = playersOn;

        }


    }//end class 
}//end namespace
