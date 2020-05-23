using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormTypeSelection
{
    public class Ex_2
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

        /// <summary>
        /// Переводит число в другую систему счисления
        /// </summary>
        /// <param name="seed"></param>
        /// <param name="num"></param>
        /// <param name="notation"> Новая система счисления </param>
        /// <returns> Возвращает число в системе счисления notation </returns>
        private static int TranslateToAnotherNotation(ref long seed, int num, out int notation)
        {
            bool minus;
            if (num < 0)
                minus = true;
            else
                minus = false;
            num = Math.Abs(num);
            List<int> digits = new List<int>();
            seed = ChangeSeed(seed, 1);
            if (num > 200)
            {
                notation = MyRnd(seed).Next(13, 17);
            }
            else if (num > 100)
            {
                notation = MyRnd(seed).Next(11, 13);
            }
            else if (num > 30)
            {
                notation = MyRnd(seed).Next(7, 10);
            }
            else
            {
                notation = MyRnd(seed).Next(5, 7);
            }
            while (num > 0)
            {
                digits.Add(num % notation);

                num /= notation;
            }
            digits.Reverse();
            num = 0;
            foreach (int digit in digits)
            {
                num = num * 10 + digit;
            }
            if (minus)
                return -1 * num;
            else
                return num;
        }

        private static bool CoefsCheckWasSuccessful(int a, out int b, int c, int x, int y)
        {
            // Ур-е вида   left + b  ==  right;  b - надо найти
            int left = (a / 100 * x * x) + ((a % 100) / 10 * x) + a % 10;
            int right = (c / 100 * y * y) + ((c % 100) / 10 * y) + c % 10;
            b = right - left;
            if (Math.Abs(b) > 0 && Math.Abs(b) < 300)
                return true;
            else
                return false;
        }

        private static List<int> GenerateCoefs(ref long seed)
        {
            List<int> coefs;
            while (true)
            {
                // уравнение вида  105(x)  +  19(10)  =  36(x+2)
                // P.s. В скобках система счисления
                // P.p.s. 19(10) потом будет переводиться в другую систему счисления;
                int x = MyRnd(seed).Next(7, 17); // В примере - X
                seed = ChangeSeed(seed, 10);
                int y = x + MyRnd(seed).Next(1, 4); // В примере - x+2
                seed = ChangeSeed(seed, 1);
                int a = MyRnd(seed).Next(100, 500); // В примере 105
                seed = ChangeSeed(seed, 10);
                int c = MyRnd(seed).Next(70, 320); // В примере 36
                if (CoefsCheckWasSuccessful(a, out int b, c, x, y))
                {
                    coefs = new List<int>() { a, b, c, x, y };
                    break;
                }
                else
                    continue;
            }
            coefs[1] = TranslateToAnotherNotation(ref seed, coefs[1], out int notation3); // notation3 - третья сис счисления (которая при b)
            coefs.Add(notation3);
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
                textTask += $" {coefs[1]}<sub>{coefs[5]}</sub>  = {coefs[2]}<sub>x+{(coefs[4] - coefs[3])}</sub><br>Найдите систему счисления (x).";
                string textAnswer = $"{ coefs[3]}";
                Tasks.Add(textTask);
                Answers.Add(textAnswer);
            }
        }
    }
}

