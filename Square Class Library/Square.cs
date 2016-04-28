using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Player_Class_Library;


namespace Square_Class_Library
{

    public class Square
    {

        private string name;
        private int number;

        public Square()
        {
            throw new ArgumentException("Invalid use");
        }
        public Square(string name, int number)
        {
            this.name = name;
            this.number = number;
        }
        public string Name
        {
            get
            {
                return name;
            }

        }
        public int Number
        {
            get
            {
                return number;
            }
        }
        public virtual void EffectOnPlayer(Player who)
        {

        }

    }
}
