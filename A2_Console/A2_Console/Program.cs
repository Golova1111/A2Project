using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A2_Console
{
    class Program
    {

        public static bool transfercheck = false;

        public static int InsertCoin = 1;
        public static int DeleteCoin = 1;
        public static int ReplaceCoin = 1;
        public static int TransferCoin = 1;

        //public static List<string> dictionary;

        static void Main()
        {

            //dictionary = new List<string>();
            //StreamReader strread = new StreamReader(Environment.CurrentDirectory + "\\zdf-win.txt");

            //string line;
            //while ((line = strread.ReadLine()) != null)
            //{
            //    dictionary.Add(line);
            //}


            Console.WriteLine("-=====================-");
            Console.WriteLine("Редакционное расстояние Дамерау–Левенштейна");
            Console.WriteLine("-=====================-");
            Console.WriteLine("Справка: README.pdf");
            Console.WriteLine("Запуск: dldist слово1 слово2");
            Console.WriteLine("Запуск (из файла): dldistf файл1 файл2");
            Console.WriteLine("Тест на память: memtest");
            Console.WriteLine("Тест на скорость: speedtest");
            Console.WriteLine("Выход: exit");
            Console.WriteLine("-=====================-");

            while (true)
            {
                string command = Console.ReadLine();

                string[] answer = command.Split(' ');

                switch (answer[0])
                {
                    case "dldist":

                        #region
                        try
                        {
                            if (answer.Length < 3 || answer[1].Length > 100000 || answer[2].Length > 100000)
                                Console.WriteLine("Некорректный ввод");
                            else
                                Calculate(answer[1], answer[2]);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;

                        #endregion

                        


                    case "dldistf":

                        #region
                        try
                        {
                            if (answer.Length < 3)
                                Console.WriteLine("Некорректный ввод");
                            else
                            {
                                StreamReader stread3 = new StreamReader(Environment.CurrentDirectory + "\\" + answer[1], System.Text.Encoding.Default);
                                StreamReader stread4 = new StreamReader(Environment.CurrentDirectory + "\\" + answer[2], System.Text.Encoding.Default);
                                string str3 = stread3.ReadToEnd();
                                string str4 = stread4.ReadToEnd();

                                if (str3.Length > 100000 || str4.Length > 100000)
                                {
                                    Console.WriteLine("Некорректный ввод");
                                    break;
                                }

                                Calculate(str3, str4);
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }

                        break;
                    #endregion


                    

                    case "memtest":

                        #region

                        StreamReader stread1;
                        StreamReader stread2;
                        string str1;
                        string str2;

                        double memuse;

                        string array1;
                        string array2;

                        Stopwatch time;
                        int[,] AnswerMatrix;
                        int ld_Dist;

                        MLApp.MLApp matlab;

                        try
                        {
                            Console.WriteLine();
                            Console.WriteLine("Исходные затраты памяти : " + GC.GetTotalMemory(true) / 1024 + " Kb");
                            Console.WriteLine();

                            array1 = "Y = [";
                            array2 = "X = [";

                            for (int i = 1; i <= 20; i++)
                            {
                                stread1 = new StreamReader(Environment.CurrentDirectory + "\\Tests\\l--" + 100 * i + ".txt", System.Text.Encoding.Default);
                                stread2 = new StreamReader(Environment.CurrentDirectory + "\\Tests\\g--" + 100 * i + ".txt", System.Text.Encoding.Default);
                                str1 = stread1.ReadToEnd();
                                str2 = stread2.ReadToEnd();

                                if (str1.Length > 100000 || str2.Length > 100000)
                                {
                                    Console.WriteLine("Некорректный ввод");
                                    break;
                                }

                                AnswerMatrix = new int[str1.Length + 2, str2.Length + 2];

                                for (int j = 0; j < 3; j++)
                                {

                                    ld_Dist = Levenshtein(AnswerMatrix, str1, str2);

                                    memuse = GC.GetTotalMemory(true) / 1024;
                                    Console.WriteLine("Память для (" + 100 * i + " x " + 100 * i + ") : " + memuse + " Kb");

                                    array1 = array1 + " " + memuse;
                                    array2 = array2 + " " + 100 * i * 100 * i;


                                }
                                Console.WriteLine();
                            }

                            array1 += "];";
                            array2 += "]";

                            matlab = new MLApp.MLApp();

                            matlab.Execute(array1);
                            matlab.Execute(array2);
                            matlab.Execute("set(0, 'DefaultAxesFontSize', 14, 'DefaultAxesFontName', 'Times New Roman');");
                            matlab.Execute("set(0, 'DefaultTextFontSize', 14, 'DefaultTextFontName', 'Times New Roman');");
                            matlab.Execute("plot(X,Y,'bx','Color',[.1 .4 .1])");
                            matlab.Execute("title('Затраты памяти');");
                            matlab.Execute("xlabel('Кол-во символов, n*m');");
                            matlab.Execute("ylabel('Затраченная память, Кб');");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }

                        break;

                    #endregion




                    case "speedtest":

                        #region
                        try
                        {
                            long timeuse;

                            array1 = "Y = [";
                            array2 = "X = [";

                            for (int i = 1; i <= 20; i++)
                            {
                                stread1 = new StreamReader(Environment.CurrentDirectory + "\\Tests\\l--" + 100 * i + ".txt", System.Text.Encoding.Default);
                                stread2 = new StreamReader(Environment.CurrentDirectory + "\\Tests\\g--" + 100 * i + ".txt", System.Text.Encoding.Default);
                                str1 = stread1.ReadToEnd();
                                str2 = stread2.ReadToEnd();

                                if (str1.Length > 100000 || str2.Length > 100000)
                                {
                                    Console.WriteLine("Некорректный ввод");
                                    break;
                                }

                                AnswerMatrix = new int[str1.Length + 2, str2.Length + 2];

                                for (int j = 0; j < 3; j++)
                                {
                                    time = new Stopwatch();

                                    time.Start();
                                    ld_Dist = Levenshtein(AnswerMatrix, str1, str2);
                                    time.Stop();

                                    timeuse = time.ElapsedMilliseconds;
                                    Console.WriteLine("Время для (" + 100 * i + " x " + 100 * i + ") : " + timeuse + " ms");

                                    array1 = array1 + " " + timeuse;
                                    array2 = array2 + " " + 100 * i * 100 * i;


                                }
                                Console.WriteLine();
                            }

                            array1 += "];";
                            array2 += "]";

                            matlab = new MLApp.MLApp();

                            matlab.Execute(array1);
                            matlab.Execute(array2);
                            matlab.Execute("set(0, 'DefaultAxesFontSize', 14, 'DefaultAxesFontName', 'Times New Roman');");
                            matlab.Execute("set(0, 'DefaultTextFontSize', 14, 'DefaultTextFontName', 'Times New Roman');");
                            matlab.Execute("plot(X,Y,'bx','Color',[.1 .4 .1])");
                            matlab.Execute("title('Затраты времени');");
                            matlab.Execute("xlabel('Кол-во символов, n*m');");
                            matlab.Execute("ylabel('Затраченное время, мс');");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }

                        break;

                    #endregion


                    case "correcttest":

                        #region
                        try
                        { 
                            Calculate("cat", "dog");
                            Console.WriteLine("Правильный ответ: 3");

                            Calculate("cat", "cc");
                            Console.WriteLine("Правильный ответ: 2");

                            Calculate("cat", "");
                            Console.WriteLine("Правильный ответ: 3");

                            Calculate("11113411", "11114311");
                            Console.WriteLine("Правильный ответ: 1");

                            Calculate("ca", "abc");
                            Console.WriteLine("Правильный ответ: 2");
                        }
                                catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                   


                break;
                    #endregion


                    case "exit":
                        Environment.Exit(0);
                        break;

                    default:
                        break;
                }
            }
        }



        private static int Levenshtein(int[,] AnswerMatrix, string str1, string str2)
        {


            Dictionary<char, int> FirstWord = new Dictionary<char, int>();
            Dictionary<char, int> SecondWord = new Dictionary<char, int>();


            int i_sh;
            int j_sh;
            int temp_transf;

            transfercheck = false;



            for (int i = 0; i < str1.Length; i++)
            {
                if (!FirstWord.ContainsKey(str1[i]))
                    FirstWord.Add(str1[i], i + 1);
            }


            for (int i = 0; i < str2.Length; i++)
            {
                if (!SecondWord.ContainsKey(str2[i]))
                    SecondWord.Add(str2[i], i + 1);
            }



            // База рекурсии

            int INF = 9999;
            AnswerMatrix[0, 0] = INF;

            for (int i = 1; i <= str1.Length + 1; i++)
            {
                AnswerMatrix[i, 0] = INF;
                AnswerMatrix[i, 1] = i - 1;
            }

            for (int i = 1; i <= str2.Length + 1; i++)
            {
                AnswerMatrix[0, i] = INF;
                AnswerMatrix[1, i] = i - 1;
            }



            // Построение матрицы

            for (int i = 1; i <= str1.Length; i++)
            {
                for (int j = 1; j <= str2.Length; j++)
                {
                    if (str1[i - 1] == str2[j - 1])
                    {
                        AnswerMatrix[i + 1, j + 1] = AnswerMatrix[i, j];
                        // last = j;
                    }

                    else
                    {
                        int temp_k = Math.Min(

                                        AnswerMatrix[i + 1, j] + InsertCoin,
                                        Math.Min(
                                            AnswerMatrix[i, j + 1] + DeleteCoin,
                                            AnswerMatrix[i, j] + ReplaceCoin)

                        );


                        temp_transf = 9999;
                        if (SecondWord.ContainsKey(str1[i - 1]) && FirstWord.ContainsKey(str2[j - 1]))
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



        private static void Calculate(string string1, string string2)
        {
            int[,] AnswerMatrix = new int[string1.Length + 2, string2.Length + 2];
            Stopwatch time = new Stopwatch();

            time.Start();
            int ld_Dist = Levenshtein(AnswerMatrix, string1, string2);
            time.Stop();

            Console.WriteLine();
            Console.WriteLine("Строка 1: " + string1);
            Console.WriteLine("Строка 2: " + string2);
            Console.WriteLine("Расстояние Дамерау-Левенштейна: " + ld_Dist);
            Console.WriteLine("Время работы программы: " + time.ElapsedMilliseconds + "ms. \n");
        }
    }
}
