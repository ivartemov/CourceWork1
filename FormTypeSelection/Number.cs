using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormTypeSelection
{
    public class Number
    {
        int[] num3digits = new int[4];

        public int[] Num3digits { get; set; }

        public Number(int number, int notation)
        {
            Num3digits = num3digits;
            for (int i = 0; i < 4; i++)
            {
                Num3digits[i] = number % notation;
                number /= notation;
            }
            Num3digits = Num3digits.Reverse().ToArray();
        }
        
        private static Random Rnd(int seed)
        {
            return new Random(seed);
        }
    }
}
