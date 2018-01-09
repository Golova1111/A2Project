using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace A2_Project
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int InsertCoin = 1;
        public int DeleteCoin = 1;
        public int ReplaceCoin = 1;
        public int TransferCoin = 1;

        public bool DontCheckRegisr = false;
        bool transfercheck = false;

        public int[,] AnswerMatrix;

        int answer = 0;
        string[] str1Answer;
        string[] str2Answer;
        private int MAX_INT = 9999999;

        List<string> dictionary;

        public MainWindow()
        {
            InitializeComponent();

            dictionary = new List<string>();

            StreamReader strread = new StreamReader(Environment.CurrentDirectory + "\\zdf-win.txt");

            string line;
            while ((line = strread.ReadLine()) != null)
            {
                dictionary.Add(line);
            }
        }


        private void Calculate_Click(object sender, RoutedEventArgs e)
        {
            string str1 = String1TextBox.Text;
            string str2 = String2TextBox.Text;

            VisualResult();

            Stopwatch time = new Stopwatch();
            time.Start();

            //Крайние случаи
            if (str1.Length == 0)
            {
                LevenshteinAnswer.Content = "Расстояние Левенштейна: " + str2.Length;
                AnswerLabel.Content = "Строка 1 пустая, соответственно растояние Левенштейна – длина 2ой строки \n";
                time.Stop();
                AnswerLabel.Content += "Время работы программы: " + time.Elapsed.TotalMilliseconds + " ms";
                VisualError();

                return;
            }
            else if (str2.Length == 0)
            {
                LevenshteinAnswer.Content = "Расстояние Левенштейна: " + str1.Length;
                AnswerLabel.Content = "Строка 2 пустая, соответственно растояние Левенштейна – длина 1ой строки. \n";
                time.Stop();
                AnswerLabel.Content += "Время работы программы: " + time.Elapsed.TotalMilliseconds + " ms";
                VisualError();
                return;
            }
            else if (str1 == str2 || (str1.ToUpper() == str2.ToUpper() && DontCheckRegisr))
            {
                LevenshteinAnswer.Content = "Расстояние Левенштейна: 0";
                AnswerLabel.Content = "Строки совпадают \n";
                AnswerLabel.Content += "Время работы программы: " + time.Elapsed.TotalMilliseconds + " ms";
                time.Stop();
                VisualError();

                return;
            }
            else if (str1.Length > 40000 && str2.Length > 40000)
            {
                LevenshteinAnswer.Content = "Расстояние Левенштейна: 0";
                AnswerLabel.Content = "Размер одной из строк превышает допустимое значение \n";
                AnswerLabel.Content += "Время работы программы: " + time.Elapsed.TotalMilliseconds + " ms";
                time.Stop();
                VisualError();

                return;
            }

            //Создание матрицы ответов
            AnswerMatrix = new int[str1.Length + 2, str2.Length + 2];

            //Заполнение матрицы ответов
            if (DontCheckRegisr)
            {
                answer = Levenshtein(AnswerMatrix, str1.ToLower(), str2.ToLower());
                str1 = str1.ToLower();
                str2 = str2.ToLower();
            }
            else
                answer = Levenshtein(AnswerMatrix, str1, str2);

            time.Stop();

            LevenshteinAnswer.Content = "Расстояние Дамерау-Левенштейна: " + answer;
            LevenshteinCriteria.Content = "Схожесть: " + Convert.ToInt32( ((1.0 - ((answer * 1.0) / Math.Max(String1TextBox.Text.Length,String2TextBox.Text.Length)))  * 100)) + " %";


            #region Восстановление ответа

            AnswerLabel.Content = "";

            if (transfercheck == false)
            {

                str1Answer = new string[answer + 3];
                str2Answer = new string[answer + 3];

                str1Answer[0] = str1;
                str2Answer[0] = str2;

                int str1lenght = str1.Length;
                int str2lenght = str2.Length;


                int count = 1;

                while (AnswerMatrix[str1lenght + 1, str2lenght + 1] != 0)
                {

                    int replace = str1lenght >= 1 && str2lenght >= 1 ? AnswerMatrix[str1lenght + 1, str2lenght + 1] : MAX_INT;
                    int remove2 = str2lenght >= 1 ? AnswerMatrix[str1lenght + 1, str2lenght] : MAX_INT;
                    int remove1 = str1lenght >= 1 ? AnswerMatrix[str1lenght, str2lenght + 1] : MAX_INT;


                    int minimum = Math.Min(replace, Math.Min(remove2, remove1));




                    if (minimum == replace)
                    {
                        if (str1[str1lenght - 1] == str2[str2lenght - 1])
                        {
                            AnswerLabel.Content += "Окончания строчек одинаковые...\n\n";
                        }
                        else
                        {
                            AnswerLabel.Content += count + ". Замена символа [" + str1lenght + "] в строчке 1 на символ [" + str2lenght + "] в строчке 2 \n";



                            str1 = str1.Substring(0, str1lenght - 1) + str2[str2lenght - 1] + str1.Substring(str1lenght);

                            str1Answer[count] = str1;
                            str2Answer[count] = str2;
                            ++count;
                            AnswerLabel.Content += str1 + "/" + str2 + "\n\n";
                        }

                        --str1lenght;
                        --str2lenght;
                    }





                    else if (minimum == remove2)
                    {

                        AnswerLabel.Content += count + ". Вставка символа [" + str2[str2lenght - 1] + "] в 1ую строчку \n";


                        if (str1lenght > 0)
                            str1 = str1.Substring(0, str1lenght) + str2[str2lenght - 1] + str1.Substring(str1lenght);
                        else
                            str1 = str2[str2lenght - 1] + str1.Substring(str1lenght);


                        --str2lenght;

                        str1Answer[count] = str1;
                        str2Answer[count] = str2;
                        ++count;
                        AnswerLabel.Content += str1 + "/" + str2 + "\n\n";
                    }



                    else if (minimum == remove1)
                    {
                        AnswerLabel.Content += count + ". Удаление символа [" + str1lenght + "] в 1ой строчке \n";
                        str1 = str1.Substring(0, str1lenght - 1) + str1.Substring(str1lenght);

                        --str1lenght;

                        str1Answer[count] = str1;
                        str2Answer[count] = str2;
                        ++count;
                        AnswerLabel.Content += str1 + "/" + str2 + "\n\n";
                    }
                }

            }

            AnswerLabel.Content += "Время работы программы: " + time.Elapsed.TotalMilliseconds + " ms";
            #endregion


            if (String1TextBox.Text.Length < 40 && String1TextBox.Text.Length < 40 && !transfercheck)
            {
                answerstep.Content = answer;
                VisualAnswer();
                PicChange(0);
            }
            else
            {
                VisualError();
            }

        }

        private int Levenshtein(int[,] AnswerMatrix, string str1, string str2)
        {


            Dictionary<char, int> FirstWord = new Dictionary<char, int>();
            Dictionary<char, int> SecondWord = new Dictionary<char, int>();


            int i_sh;
            int j_sh;
            int temp_transf;

            transfercheck = false;

            // Загрузка словаря


            for (int i = 0; i < str1.Length; i++)
            {
                if (!FirstWord.ContainsKey(str1[i]))
                    FirstWord.Add(str1[i], i+1);
            }


            for (int i = 0; i < str2.Length; i++)
            {
                if (!SecondWord.ContainsKey(str2[i]))
                    SecondWord.Add(str2[i], i + 1);
            }



            // База рекурсии

            AnswerMatrix[0, 0] = MAX_INT;

            for (int i = 1; i <= str1.Length + 1; i++)
            {
                AnswerMatrix[i, 0] = MAX_INT;
                AnswerMatrix[i, 1] = i-1;
            }

            for (int i = 1; i <= str2.Length + 1; i++)
            {
                AnswerMatrix[0, i] = MAX_INT;
                AnswerMatrix[1, i] = i-1;
            }



            // Построение матрицы

            for (int i = 1; i <= str1.Length; i++)
            {
                for (int j = 1; j <= str2.Length; j++)
                {


                    if (str1[i - 1] == str2[j - 1])
                    {
                        AnswerMatrix[i + 1, j + 1] = AnswerMatrix[i, j];
                    }

                    else
                    {
                        int temp_k = Math.Min(

                                        AnswerMatrix[i + 1, j] + InsertCoin,
                                        Math.Min(
                                            AnswerMatrix[i, j + 1] + DeleteCoin,
                                            AnswerMatrix[i, j] + ReplaceCoin)
                                        
                        );


                        temp_transf = MAX_INT;
                        if (SecondWord.ContainsKey(str1[i-1]) && FirstWord.ContainsKey(str2[j - 1]))
                        {
                            i_sh = FirstWord[str2[j - 1]];
                            j_sh = SecondWord[str1[i - 1]];

                            if (i > i_sh && j > j_sh)
                                temp_transf = AnswerMatrix[i_sh, j_sh] + (i - i_sh - 1) * DeleteCoin + TransferCoin + (j - j_sh - 1) * InsertCoin;
                        }

                        AnswerMatrix[i + 1, j + 1] = Math.Min(temp_k, temp_transf);

                        SecondWord[str2[j - 1]] = j;
                        FirstWord[str1[i - 1]] = i;

                        if (temp_transf < temp_k)
                                   transfercheck = true;
                    }
                }
            }


            return AnswerMatrix[str1.Length + 1, str2.Length + 1];

        }

        private int LevLenght(int[,] Matrix, string str1, string str2, int L1, int L2)
        {
            if (Matrix[L1, L2] != 0)
                return Matrix[L1, L2];

            else if (L1 == 0 && L2 == 0)
            {
                Matrix[L1, L2] = 0;
                return Matrix[L1, L2];
            }

            else if (L1 == 0)
            {
                Matrix[L1, L2] = L2 * InsertCoin;
                return Matrix[L1, L2];
            }



            else if (L2 == 0)
            {
                Matrix[L1, L2] = L1 * InsertCoin;
                return Matrix[L1, L2];
            }



            else if (str1[L1 - 1] == str2[L2 - 1])
            {
                Matrix[L1, L2] = LevLenght(Matrix, str1, str2, L1 - 1, L2 - 1);
                return Matrix[L1, L2];
            }



            else if (L1 > 1 && L2 > 1 && str1[L1 - 1] == str2[L2 - 2] && str1[L1 - 2] == str2[L2 - 1])
            {
                Matrix[L1, L2] =
                    Math.Min(

                        Math.Min(LevLenght(Matrix, str1, str2, L1, L2 - 1) + DeleteCoin,
                                 LevLenght(Matrix, str1, str2, L1 - 2, L2 - 2) + TransferCoin),

                        Math.Min(LevLenght(Matrix, str1, str2, L1 - 1, L2) + DeleteCoin,
                                 LevLenght(Matrix, str1, str2, L1 - 1, L2 - 1) + ReplaceCoin)
                        );

                return Matrix[L1, L2];
            }



            else Matrix[L1, L2] =
                    Math.Min( (LevLenght(Matrix, str1, str2, L1, L2 - 1) + DeleteCoin),

                            Math.Min(LevLenght(Matrix, str1, str2, L1 - 1, L2) + DeleteCoin,
                                     LevLenght(Matrix, str1, str2, L1 - 1, L2 - 1) + ReplaceCoin)
                     );

            return Matrix[L1, L2];
        }


        #region

        int currentstep = 1;

        private void StepBack_Click(object sender, RoutedEventArgs e)
        {
            PicChange(currentstep - 1);
        }

        

        private void StepFront_Click(object sender, RoutedEventArgs e)
        {
            PicChange(currentstep + 1);
        }

        void PicChange(int currentstep)
        {
            this.currentstep = currentstep;

            StepFront.IsEnabled = true;
            StepBack.IsEnabled = true;

            currstep.Content = currentstep;

            string1current.Content = str1Answer[currentstep];
            string2current.Content = str2Answer[currentstep];

            if (currentstep == 0) StepBack.IsEnabled = false;
            if (currentstep == answer) StepFront.IsEnabled = false;
        }

        public void VisualError()
        {
            string1current.Visibility = Visibility.Hidden;
            string2current.Visibility = Visibility.Hidden;
            StepBack.Visibility = Visibility.Hidden;
            StepFront.Visibility = Visibility.Hidden;
            currstep.Visibility = Visibility.Hidden;
            answerstep.Visibility = Visibility.Hidden;
            VLabel1.Visibility = Visibility.Hidden;
            VLabel2.Visibility = Visibility.Hidden;
            VLabel3.Visibility = Visibility.Hidden;

            VErrorLabel.Visibility = Visibility.Visible;
        }

        public void VisualAnswer()
        {
            string1current.Visibility = Visibility.Visible;
            string2current.Visibility = Visibility.Visible;
            StepBack.Visibility = Visibility.Visible;
            StepFront.Visibility = Visibility.Visible;
            currstep.Visibility = Visibility.Visible;
            answerstep.Visibility = Visibility.Visible;
            VLabel1.Visibility = Visibility.Visible;
            VLabel2.Visibility = Visibility.Visible;
            VLabel3.Visibility = Visibility.Visible;

            VErrorLabel.Visibility = Visibility.Hidden;



        }

        #endregion

        private void MatrixOpen_Click(object sender, RoutedEventArgs e)
        {
            Matrix mx = new Matrix(this);
            mx.ShowInTaskbar = false;
            mx.ShowDialog();
        }  //Матрица

        private void Dictionary1_Click(object sender, RoutedEventArgs e)
        {
            Random rand = new Random();

            for (int i = 0; i < 1000; i++)
            {
                int rand1 = Convert.ToInt32(Math.Floor(dictionary.Count * rand.NextDouble()));
                int rand2 = Convert.ToInt32(Math.Floor(dictionary.Count * rand.NextDouble()));

                String1TextBox.Text = dictionary[rand1];
                String2TextBox.Text = dictionary[rand2];

                Calculate_Click(sender, e);
            }

        } //Словарь

        private void FromFile1_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog opd = new OpenFileDialog();
            opd.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";


            if (opd.ShowDialog() == true)
            {
                StreamReader stread = new StreamReader(opd.FileName, System.Text.Encoding.Default);
                if (sender.Equals(FromFile1))
                    String1TextBox.Text = stread.ReadToEnd();
                else
                    String2TextBox.Text = stread.ReadToEnd();
            }
        } // Загрузка из файла

        private void SettingButton_Click(object sender, RoutedEventArgs e) // Окно настроек
        {
            Setting set = new Setting(this);
            set.ShowDialog();
        }

        private void VisualResult()
        {
            LevenshteinAnswer_Copy.Visibility = Visibility.Visible;
            rect1.Visibility = Visibility.Visible;
            rect2.Visibility = Visibility.Visible;
            LevenshteinCriteria.Visibility = Visibility.Visible;
            MatrixOpen.Visibility = Visibility.Visible;
            LevenshteinAnswer.Visibility = Visibility.Visible;
            AnswerLabel.Visibility = Visibility.Visible;
            scroll.Visibility = Visibility.Visible;
        }
    }
}
