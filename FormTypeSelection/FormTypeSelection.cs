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
            labelSeed.Hide();
            labelAmountOfTasks.Hide();
            labelAnswersMode.Hide();
            labelChooseTypeOfTask.Hide();
            textBoxSeed.Hide();
            textBoxAmount.Hide();
            buttonTask1.Hide();
            buttonTask2.Hide();
            labelAnswersMode.Text = "Показывать ответы?";
            labelChooseTypeOfTask.Text = "Выберите тип задания: ";
            labelAmountOfTasks.Text = "Сколько задач сгенерировать?";
            buttonAnswersMode.Text = "Да";
            
            ChoosedTask = 0;
            AmountOfTasks = 1;
            textBoxSeed.Text = "0";
            textBoxAmount.Text = "1";
            CustomSeed = 0;
            Tasks = new List<string>();
            Answers = new List<string>();

            //#region initialising events for buttons

            buttonTask1.Click += ButtonTaskClick;
            buttonTask1.Click += Task1Choosed;
            buttonTask2.Click += ButtonTaskClick;
            buttonTask2.Click += Task2Choosed;
            buttonStart.Click += StartEvent;

            buttonAnswersMode.Click += AnswersModeChange;

            buttonSaveFile.Click += SaveButtonClick;


            //#endregion
        }

        private bool AnswersMode { get; set; } = true;
        private bool GenWithCustomSeed { get; set; } = false;

        private List<string> Tasks;
        private List<string> Answers;
        private List<long> Seeds;
        private int ChoosedTask { get; set; }
        private string FilePath { get; set; }
        private int AmountOfTasks { get; set; }
        private long CustomSeed { get; set; }


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
            if (!int.TryParse((textBoxAmount.Text), out int inputAmount) || inputAmount < 1 || inputAmount > 100)
            {
                MessageBox.Show("Введите число от 1 до 100", "Количество заданий");
            }
            else if (!long.TryParse((textBoxSeed.Text), out long inputSeed) || (inputSeed < 100000 && inputSeed != 0) || (inputSeed > 999999 && inputSeed < 10000000000) || inputSeed > 99999999999)
            {
                MessageBox.Show("Введите число, состоящее из 6 цифр либо из 11", "Номер задания");
            }
            else if (inputSeed != 0 && inputAmount != 1)
            {
                MessageBox.Show("Вы ввели номер задания. Кол-во заданий: 1.\rЕсли хотите создать случайные задания, поставьте номер задания: 0", "Количество заданий");
                textBoxAmount.Text = "1";
                inputAmount = 1;
            }
            else if (inputAmount > 0)
            {
                AmountOfTasks = inputAmount;
                CustomSeed = inputSeed;
                if (CustomSeed == 0)
                    GenWithCustomSeed = false;
                else
                    GenWithCustomSeed = true;
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
            bool flag = GenWithCustomSeed;
            Ex_1.GenerateExercise(AmountOfTasks, CustomSeed, ref flag);
            if (!flag)
            {
                MessageBox.Show("Такого задания не существует, сгенерируйте задания случайно и сохраните их номера.", "Номер задания");
                GoToMenu();
                return;
            }
            Tasks = Ex_1.Tasks;
            Answers = Ex_1.Answers;
            Seeds = Ex_1.Seeds;
        }

        private void GenerateEx_2(object sender, EventArgs e)
        {
            bool flag = GenWithCustomSeed;
            Ex_2.GenerateExercise(AmountOfTasks, CustomSeed, ref flag);
            if (!flag)
            {
                MessageBox.Show("Такого задания не существует, сгенерируйте задания случайно и сохраните их номера.", "Номер задания");
                GoToMenu();
                return;
            }
            Tasks = Ex_2.Tasks;
            Answers = Ex_2.Answers;
            Seeds = Ex_2.Seeds;
        }

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
            labelAmountOfTasks.Show();
            labelSeed.Show();
            textBoxAmount.Show();
            textBoxSeed.Show();
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
            AmountOfTasks = 1;
            ChoosedTask = 0;
            textBoxAmount.Text = "1";
            FilePath = null;
            AnswersMode = true;
            buttonAnswersMode.Text = "Да";
            GenWithCustomSeed = false;
            textBoxSeed.Text = "0";
            CustomSeed = 0;
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
