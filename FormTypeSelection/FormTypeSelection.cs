using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormTypeSelection
{
    public partial class FormTypeSelection : Form
    {
        public FormTypeSelection()
        {
            InitializeComponent();
            this.CenterToScreen();
            this.Text = "Генератор задач";
            buttonGoHome.Hide();
            buttonAnswersMode.Hide();
            buttonSaveFile.Hide();
            labelAmountOfTasks.Hide();
            labelAnswersMode.Hide();
            labelChooseTypeOfTask.Hide();
            textBoxAmount.Hide();
            buttonTask1.Hide();
            buttonTask2.Hide();
            labelAnswersMode.Text = "Показывать ответы?";
            labelChooseTypeOfTask.Text = "Выберите тип задания: ";
            labelAmountOfTasks.Text = "Сколько задач сгенерировать?";
            textBoxAmount.Text = "5";
            buttonAnswersMode.Text = "Да";


            //#region initialising events for buttons

            buttonTask1.Click += ButtonTaskClick;
            buttonTask1.Click += Task1Choosed;
            buttonTask2.Click += ButtonTaskClick;
            buttonTask2.Click += Task2Choosed;
            buttonStart.Click += StartEvent;

            buttonAnswersMode.Click += AnswersModeChange;

            buttonSaveFile.Click += SaveButtonClick;


            //#endregion


            Tasks = new List<string>();
            Answers = new List<string>();
            ChoosedTask = 0;
            AmountOfTasks = 0;
        }

        private bool AnswersMode { get; set; } = true;

        private List<string> Tasks;
        private List<string> Answers;
        private List<long> Seeds;
        private int ChoosedTask { get; set; }
        private string FilePath { get; set; }
        private int AmountOfTasks { get; set; }

        
        private void StartEvent(object sender, EventArgs e)
        {
            buttonStart.Hide();
            buttonGoHome.Show();
            buttonTask1.Show();
            buttonTask2.Show();
            labelChooseTypeOfTask.Show();
        }

        private void SaveButtonClick(object sender, EventArgs e)
        {
            if (!int.TryParse((textBoxAmount.Text), out int input) || input < 1 || input > 99)
            {
                MessageBox.Show("Введите число от 1 до 99", "Количество задач");
            }
            else
            {
                AmountOfTasks = input;
                switch (ChoosedTask)
                {
                    case 0:
                        MessageBox.Show("Неизвестная ошибка. Нужно выбрать тип задачи прежде, чем сохранять ее.");
                        break;
                    case 1:
                        GenerateEx_1(sender, e);
                        break;
                    case 2:
                        GenerateEx_2(sender, e);
                        break;
                }
                FileSavingEvent(sender, e);
            }
        }

        private void FileSavingEvent(object sender, EventArgs e)
        {
            string tasksText = CreateTextOfTasks();
            SavingFile(tasksText);
            if (FilePath != null)
            {
                HtmlCustomLaunch();
            }
            GoToMenu();
        }

        private void GenerateEx_1(object sender, EventArgs e)
        {
            Ex_1.GenerateExercise(AmountOfTasks);
            Tasks = Ex_1.Tasks;
            Answers = Ex_1.Answers;
            Seeds = Ex_1.Seeds;
        }

        private void GenerateEx_2(object sender, EventArgs e)
        {
            Ex_2.GenerateExercise(AmountOfTasks);
            Tasks = Ex_2.Tasks;
            Answers = Ex_2.Answers;
            Seeds = Ex_2.Seeds;
        }

        //private void GenerateEx_3(object sender, EventArgs e)
        //{
        //    Ex_3.GenerateExercise(AmountOfTasks);
        //    Tasks = Ex_3.Tasks;
        //    Answers = Ex_3.Answers;
        //    Seeds = Ex_3.Seeds;
        //}

        private string CreateTextOfTasks()
        {
            HTMLTextMaker maker;

            maker = new HTMLTextMaker(Tasks, Answers, Seeds, ChoosedTask);

            return AnswersMode ? maker.TaskAndAnswers() : maker.OnlyTasks();
        }

        private void AnswersModeChange(object sender, EventArgs e)
        {
            AnswersMode = !AnswersMode;
            buttonAnswersMode.Text = AnswersMode ? "Да" : "Нет";
        }

        private void ButtonTaskClick(object sender, EventArgs e)
        {
            buttonSaveFile.Show();
            buttonAnswersMode.Show();
            labelAnswersMode.Show();
            textBoxAmount.Show();
            labelAmountOfTasks.Show();
        }

        private void Task1Choosed(object sender, EventArgs e)
        {
            // показать пример задачки
            ChoosedTask = 1;
            buttonTask1.BackColor = Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(104)))), ((int)(((byte)(175)))));
            buttonTask2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(201)))), ((int)(((byte)(234)))));
        }
        // 158, 104, 175
        private void Task2Choosed(object sender, EventArgs e)
        {
            // показать пример задачки
            ChoosedTask = 2;
            buttonTask1.BackColor = Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(201)))), ((int)(((byte)(234)))));
            buttonTask2.BackColor = Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(104)))), ((int)(((byte)(175)))));
        }

        private void buttonGoHome_Click(object sender, EventArgs e)
        {
            GoToMenu();
        }

        private void GoToMenu()
        {
            //foreach (var lbl in Controls.OfType<Label>())
            //    lbl.Hide();
            //foreach (var bttn in Controls.OfType<Button>())
            //    bttn.Hide();
            //foreach (var tBox in Controls.OfType<TextBox>())
            //    tBox.Hide();
            foreach (var el in Controls.OfType<Control>())    // хорошая альтернатива трем циклам выше
                el.Hide();
            buttonStart.Show();

            buttonTask1.BackColor = Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(201)))), ((int)(((byte)(234)))));
            buttonTask2.BackColor = Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(201)))), ((int)(((byte)(234)))));


            Tasks = new List<string>();
            Answers = new List<string>();
            Seeds = new List<long>();
            AmountOfTasks = 0;
            ChoosedTask = 0;
            textBoxAmount.Text = "5";
            FilePath = null;
            AnswersMode = true;
            buttonAnswersMode.Text = "Да";
        }

        public void SavingFile(string tasksText)
        {
            using (SaveFileDialog save = new SaveFileDialog())
            {
                save.Filter = "HTML files (*.html)|*.html|All files (*.*)|*.*";
                save.FileName = $"Задачи {ChoosedTask} типа (1)";
                save.FilterIndex = 1;
                save.RestoreDirectory = true;
                if (save.ShowDialog() == DialogResult.OK)
                {
                    FilePath = save.FileName;
                    FileWriting(tasksText, FilePath);
                }
            }
        }

        private void FileWriting(string text, string path)
        {
            try
            {
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    Encoding win1251 = Encoding.GetEncoding("windows-1251");
                    var bytes = win1251.GetBytes(text);
                    stream.Write(bytes, 0, bytes.Length);
                }
            }
            catch (IOException)
            {
                MessageBox.Show(
                    "Error occured while writing down a file...",
                    "",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show(
                    "You do not have the rights to write a file. Try launching the application again as an administrator",
                    "",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            catch (Exception)
            {
                MessageBox.Show(
                    "Unknown",
                    "",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void HtmlCustomLaunch() => System.Diagnostics.Process.Start(FilePath);
    }
}
