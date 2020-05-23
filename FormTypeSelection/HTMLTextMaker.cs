using System;
using System.Collections.Generic;

namespace FormTypeSelection
{
    public class HTMLTextMaker
    {
        private List<string> tasks;
        private List<string> answers;
        private List<long> seeds;
        private int TypeNum { get; set; }

        /// <summary>
        /// Конструктор забирающий данные о заданиях
        /// </summary>
        /// <param name="tasks"></param>
        /// <param name="answers"></param>
        /// <param name="typeNum"></param>
        public HTMLTextMaker(List<string> tasks, List<string> answers, List<long> seeds, int typeNum)
        {
            this.tasks = tasks;
            this.answers = answers;
            this.seeds = seeds;
            TypeNum = typeNum;
        }

        private readonly string intro = "<!DOCTYPE html>\r<html>\r\t<head>\r\t\t<meta http-equiv=\"Content-Type\" content=\"text/html; charset=windows-1251\">\r";
        private readonly string outro = "\r\t\t</section>\r\t</body>\r</html>";

        private readonly string style = "\t\t<style>\r\t\t\tsection { margin: 50px; font-family: \"Palatino Linotype\", \"Book Antiqua\", Palatino, serif;" +
                "font-size: 17px; \r\t\t\tletter-spacing: 2px; word-spacing: -2px; color: #000000; font-weight: normal;\r\t\t\t" +
                "text-decoration: none; font-style: normal; font-variant: normal; text-transform: none; }\r\t\t</style>\r\t</head>\r\t<body>\r\t\t";
               
        private string Section(long seed, string task, string answer) => $"<section>\r\t\t\t\t<h2>Задание № {seed}</h2>\r\t\t\t\t<p>{task}</p>\r\t\t\t\t<h1>Ответ: {answer}</h1>\r\t\t\t</section>";

        public string OnlyTasks()
        {
            string res = intro + style + $"<header>\r\t\t\t<h1>Тип {TypeNum}</h1>\r\t\t</header>\r\t\t<section>\r\t\t\t" +
            $"<h2>Задачи по теме \"Системы счисления\". Формат ЕГЭ(№ 16)</h2>\r\t\t</section>\r\t\t<section>\r\t\t\t"; ;

            for (int i = 0; i < tasks.Count; i++)
            {
                res += Section(seeds[i], tasks[i], "");
            }            
            return res + outro;
        }

        public string TaskAndAnswers()
        {
            string res = intro + style + $"<header>\r\t\t\t<h1>Тип {TypeNum}</h1>\r\t\t</header>\r\t\t<section>\r\t\t\t" +
            $"<h2>Задачи по теме \"Системы счисления\". Формат ЕГЭ(№ 16)</h2>\r\t\t</section>\r\t\t<section>\r\t\t\t"; ;

            for (int i = 0; i < tasks.Count; i++)
            {
                res += Section(seeds[i], tasks[i], answers[i]+".");
            }
            return res + outro;
        }
    }
}
