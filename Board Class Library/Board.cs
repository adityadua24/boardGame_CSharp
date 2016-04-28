using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Square_Class_Library;

namespace Board_Class_Library
{

    public static class Board
    {

        public const int TOTAL_SQUARES = 56;
        public const int START_SQUARE = 0;
        public const int END_SQUARE = 55;
        public const int PLAYING_SQUARE = 54;

        private static Square[] gameBoard = new Square[TOTAL_SQUARES];


        public static void SetUpBoard()
        {
            for (int i = (START_SQUARE + 1); i < TOTAL_SQUARES; i++)
            {

                if ((i % 10) == 0)
                {
                    gameBoard[i] = new Lose_Square(i.ToString(), i);
                }
                else if ((i % 5 == 0) && ((i % 10) != 0))
                {
                    gameBoard[i] = new Win_Square(i.ToString(), i);
                }
                else if (i % 6 == 0)
                {
                    gameBoard[i] = new Chance_Square(i.ToString(), i);
                }
                else
                {
                    gameBoard[i] = new Square(i.ToString(), i);
                }

            }

            gameBoard[START_SQUARE] = new Square("Start", START_SQUARE);
            gameBoard[END_SQUARE] = new Square("Finish", END_SQUARE);


        }
        public static Square GetGameBoardSquare(int x)
        {
            if ((x >= 0) && (x <= 55))
            {
                return gameBoard[x];
            }
            else
            {
                throw new ArgumentException("Invalid Value");
            }
        }
        public static Square StartSquare()
        {
            return gameBoard[0];
        }
        public static Square NextSquare(int x)
        {
            if ((x >= START_SQUARE) && (x < END_SQUARE))
            {
                return gameBoard[x + 1];
            }
            else
            {
                throw new ArgumentException("Inavlid Value");
            }

        }

    }
}
