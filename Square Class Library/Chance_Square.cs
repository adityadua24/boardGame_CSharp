using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Player_Class_Library;
using Board_Class_Library;

namespace Square_Class_Library
{
    public class Chance_Square : Square
    {
        private const int ADVANCE = 5;
        private const int FALLBACK = -5;
        Random rd = new Random();
        public Chance_Square(string name, int number)
            : base(name, number)
        {
        }
        public override void EffectOnPlayer(Player who)
        {

            if (((rd.Next(1, 10)) % 2) == 0)
            {
                who.Add(50);
                if ((who.Location.Number + ADVANCE) > 55)
                {
                    who.Location = Board.GetGameBoardSquare(55);
                }
                else
                {
                    who.Location = Board.GetGameBoardSquare(who.Location.Number + ADVANCE);
                }
            }
            else if (((rd.Next(1, 10)) % 2) == 1)
            {
                who.Deduct(50);
                who.Location = Board.GetGameBoardSquare(who.Location.Number + FALLBACK);
            }
        }
    }
}
