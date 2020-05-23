using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormTypeSelection
{
    public class Ex_1
    {
        public static List<string> Tasks { get; set; } = new List<string>();
        public static List<string> Answers { get; set; } = new List<string>();
        public static List<long> Seeds { get; set; } = new List<long>();


        private static readonly Random rnd = new Random();

        private static Random MyRnd(long seed)
        {
            return new Random((int)seed);
        }

        private static long ChangeSeed(long oldSeed, int coef) => oldSeed * 10 * coef + rnd.Next(1 * coef, 10 * coef);

        private static bool CoefsCheckWasSuccessful(int a, int x, int c, int n, out int y)
        {
            // Ур-е вида   p + y  ==  q;  y - надо найти
            int p = (a / 100 * x * x) + ((a % 100) / 10 * x) + a % 10;
            int q = (c / 100 * n * n) + ((c % 100) / 10 * n) + c % 10;
            y = q - p;
            if (Math.Abs(y) > 0 && Math.Abs(y) < 300)
                return true;
            else
                return false;
        }

        private static List<int> GenerateCoefs(ref long seed)
        {
            List<int> coefs;
            while (true)
            {
                // уравнение вида  145(x)  +  24(10)  =  127(9)
                // P.s. В скобках система счисления
                int x = MyRnd(seed).Next(7, 17); // В примере - X
                seed = ChangeSeed(seed, 10);
                int notation = MyRnd(seed).Next(6, 10); // В примере - 9
                seed = ChangeSeed(seed, 1);
                int a = MyRnd(seed).Next(70, 350); // В примере 145
                seed = ChangeSeed(seed, 10); 
                int c = MyRnd(seed).Next(100, 500); // В примере 127
                if (CoefsCheckWasSuccessful(a, x, c, notation, out int b))
                {
                    coefs = new List<int>() { a, b, c, x, notation };
                    break;
                }
                else
                    continue;
            }
            return coefs;
        }

        public static void GenerateExercise(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                long seed = rnd.Next(1, 100); // seed будет изменяться и в итоге будет состоять из > 6 цифр
                List<int> coefs = GenerateCoefs(ref seed);
                Seeds.Add(seed);
                string textTask = $"Дано выражение:  {coefs[0]}<sub>x</sub> ";
                if (coefs[1] >= 0)
                    textTask += "+";
                else
                {
                    coefs[1] *= -1;
                    textTask += "-";
                }
                textTask += $" {coefs[1]} = {coefs[2]}<sub>{coefs[4]}</sub><br>Найдите систему счисления (x).";
                string textAnswer = $"{ coefs[3]}";
                Tasks.Add(textTask);
                Answers.Add(textAnswer);
            }
        }
    }
}

