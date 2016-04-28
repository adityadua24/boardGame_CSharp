using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Square_Class_Library;
using System.Drawing;
using Board_Class_Library;
using Die_Class_Library;

namespace Player_Class_Library
{

    public class Player
    {
        private string name;
        private Square location;
        private Image playerTokenImage;
        private Brush playerTokenColour;
        private int money;
        private bool hasWon;
        private Die d1;
        private Die d2;
        private Square lastLocation;
        public Player()
        {
            throw new ArgumentException("Inavlid Use");
        }
        public Player(string name, Square location)
        {
            this.name = name;
            this.location = location;
            this.money = 100;
            //this.lastLocation = Board.StartSquare();
        }
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }
        public Square Location
        {
            get
            {
                return location;
            }
            set
            {
                location = value;
            }
        }
        public Square LastLocation
        {
            get
            {
                return lastLocation;
            }
            set
            {
                lastLocation = value;
            }
        }
        public Image PlayerTokenImage
        {
            get
            {
                return playerTokenImage;
            }
            set
            {
                playerTokenImage = value;
            }
        }
        public Brush PlayerTokenColour
        {
            get
            {
                return playerTokenColour;
            }
            set
            {
                playerTokenColour = value;
                playerTokenImage = new Bitmap(1, 1);
                using (Graphics g = Graphics.FromImage(PlayerTokenImage))
                {
                    g.FillRectangle(playerTokenColour, 0, 0, 1, 1);
                }
            }
        }
        public int Money
        {
            get
            {
                return money;
            }
            set
            {
                money = value;
            }
        }
        public bool HasWon
        {
            get
            {
                return hasWon;
            }
            set
            {
                hasWon = value;
            }

        }
        public void RollDice(Die die1, Die die2)
        {
            d1 = die1;
            d2 = die2;
            int faceOne = die1.Roll();
            int faceTwo = die2.Roll();
            int moveBy = faceOne + faceTwo;
            int moveTo = Location.Number + moveBy;
            if (moveTo <= 55)
            {
                Location = Board.GetGameBoardSquare(moveTo);
            }
            else if (moveTo > 55)
            {
                Location = Board.GetGameBoardSquare(55);
            }
            this.location.EffectOnPlayer(this);
        }
        public void Add(int amount)
        {
            money = money + amount;
        }
        public void Deduct(int amount)
        {
            if ((money - amount) < 0)
            {
                money = 0;
            }
            else
            {
                money = money - amount;
            }

        }
        public void RollAgain()
        {
            RollDice(d1, d2);
        }

    }
}

