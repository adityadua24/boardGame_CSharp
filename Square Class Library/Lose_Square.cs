using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Player_Class_Library;
using Board_Class_Library;

namespace Square_Class_Library
{
    public class Lose_Square : Square
    {
        private const int FALLBACK = -3;
        public Lose_Square(string name, int number)
            : base(name, number)
        {

        }
        public override void EffectOnPlayer(Player who)
        {
            who.Deduct(25);
            who.Location = Board.GetGameBoardSquare((who.Location.Number) + FALLBACK);
        }

    }
}
