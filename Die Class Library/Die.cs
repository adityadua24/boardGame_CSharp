using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Die_Class_Library {
    
    /// <summary>
    /// Die.cs models a many sided die with each face 
    /// having a distinct and unique value between 1 
    /// and the number of faces.
    /// 
    /// default is a 6 sided die.
    /// 
    /// Mike Roggenkamp April 2009
    /// 
    /// Modifications: Set the Random object to static so that multiple dice 
    /// are more likely to have diffent faceValues.  July 2009 MGR
    /// 
    /// </summary>
    public class Die {
        private const int MIN_FACES = 4;
        private const int DEFAULT_FACE_VALUE = 1;
        private const int SIX_SIDED = 6;

        // number of sides on the die
        private int numOfFaces;
        public int NumOfFaces {
            get {
                return numOfFaces;
            }
        }

        // current value showing on the die
        private int faceValue;
        public int FaceValue {
            get {
                return faceValue;
            }
        }

        private int initialFaceValue; //use only in Reset()

        private static Random random = new Random();


        //-----------------------------------------------------------------
        // Parameterless Constructor
        // defaults to a six-sided die with a default initial face value.
        // Pre:  none
        // Post: the die is initialised.
        //-----------------------------------------------------------------
        public Die() {
            numOfFaces = SIX_SIDED;
            faceValue = DEFAULT_FACE_VALUE;
            initialFaceValue = faceValue;
        }

        //-----------------------------------------------------------------
        // Explicitly sets the size of the die. Defaults to a size of
        // six if the parameter is invalid.  Face value is randomly chosen
        // Pre:  none
        // Post: the die is initialised.
        //-----------------------------------------------------------------
        public Die(int faces) {

            if (faces < MIN_FACES) {
                numOfFaces = SIX_SIDED;
            } else {
                numOfFaces = faces;
            }

            faceValue = Roll();
            initialFaceValue = faceValue;
        }

        //-----------------------------------------------------------------
        // Rolls the die and returns the result.
        // Pre:  none
        // Post: a random side of the die has been selected.
        //-----------------------------------------------------------------
        public int Roll() {
            faceValue = random.Next(NumOfFaces) + 1;
            return FaceValue;
        }


    }//end class Die

}
