using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WpfApp1
{
    class CalcMath
    {
        private float val1 = 0.0f;
        private float val2 = 0.0f;

        public enum CALCTASK
        {
            ADDITION,
            SUBTRACTION,
            MULTIPLICATION,
            DIVISION
        }

        public CalcMath()
        {
            //default constructor
        }

        public bool DoTask(string val1ToParse, string val2ToParse, out float valToReturn,CALCTASK todo)
        {
            valToReturn = 0.0f;
            if ((float.TryParse(val1ToParse, out val1))//first val has to parse
                && (float.TryParse(val2ToParse, out val2))//as well as the second
                )
            {//both parsed
                switch (todo)
                {
                    case CALCTASK.ADDITION:
                        valToReturn = val1 + val2;
                        return true;

                    case CALCTASK.SUBTRACTION:
                        valToReturn = val1 - val2;
                        return true;

                    case CALCTASK.MULTIPLICATION:
                        valToReturn = val1 * val2;
                        return true;

                    case CALCTASK.DIVISION:
                        valToReturn = val1 / val2;
                        return true;

                    default:
                        return false;
                }
            }
            else
            {
                return false;
            }
        }
        
    }
}
