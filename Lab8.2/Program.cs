using System;
using System.IO;

namespace Lab8._2
{
    class Program
    {
        public static int[] computePrefixFunction(string pattern)
        {
            int length = pattern.Length;
            int[] pi = new int[length];
            int j = 0;
            pi[0] = 0;
            for (int i = 1; i < length; i++)
            {
                if (pattern[i] == pattern[j])
                {
                    pi[i] = j + 1;
                    i++;
                    j++;
                }
                else if (pattern[i] != pattern[j])
                {
                    if (j == 0)
                    {
                        pi[i] = 0;
                        i++;
                    }
                    else
                        j = pi[j - 1];
                }
                
            }
            return pi;
        }
       public static int KMPsearch (string pattern, string text, out TimeSpan interval, out int comparisons)
        {
            DateTime StartTime = DateTime.Now;
            int N = -1;
            comparisons = 0;   
            int k = 0, l = 0;
            int[] prefix = computePrefixFunction(pattern);
            while (k <= text.Length)
            {
                comparisons++;
                if (text[k] == pattern[l])
                {
                    k++;
                    l++;

                    if (l == pattern.Length)
                    {
                        N = k - l;
                        break;
                    }
                }
                else if (l == 0)
                {
                    k++;
                    if (k == text.Length)
                        break;
                }
                else
                    l = prefix[l - 1];
            }
            DateTime EndTime = DateTime.Now;
            interval = EndTime - StartTime;
            return N;

        }
        public static int [] BadCharactersTable (string pattern)
        {
            int [] badShift = new int[256];
             for (int i = 0; i < badShift.Length;i++)
            {
                badShift[i] = pattern.Length;
            }
             for (int i = 0;i<pattern.Length-1;i++)
            {
                badShift[(int)pattern[i]] = pattern.Length - i - 1;
            }
            return badShift;
        }
       
        public static int BMsearch(string pattern, string text, out TimeSpan interval, out int comparisons)
        {
            DateTime StartTime = DateTime.Now;
            int N = -1;
            comparisons = 1;
            int[] offset = BadCharactersTable(pattern);
            int i = pattern.Length - 1;
            int j = i;
            int k = i;
            while (j >= 0 && i <= text.Length - 1)
            {
                j = pattern.Length - 1;
                k = i;
                while (j >= 0 && text[k] == pattern[j])
                {
                    k--;
                    j--;
                    comparisons++;
                }
                i += offset[text[i]];
            }
            comparisons++;
            if (k >= text.Length - pattern.Length)
                N = -1;
            else
                N = k + 1;
            DateTime EndTime = DateTime.Now;
            interval = EndTime - StartTime;
            return N;

        }

        public static int Search(string pattern, string text, out TimeSpan interval, out int comparisons)
        {
            int N = -1;
            comparisons = 0;
            DateTime StartTime = DateTime.Now;
            int j = 0;
            for (int i = 0; i < text.Length; i++)
            {
                comparisons++;
                if (text[i] == pattern[j])
                {

                    if (j == pattern.Length - 1)
                    {
                        N = i - j;
                        break;
                    }
                    j++;
                }
                else
                    j = 0;
            }
            DateTime EndTime = DateTime.Now;
            interval = EndTime - StartTime;
            return N;

        }
        static void Main(string[] args)
        {
            Console.WriteLine("Введите строку, которую необходимо найти:");
            string pattern = Console.ReadLine();
            string path = @"C:\Users\alisa\source\repos\Lab8.2\text.txt";
            StreamReader readFromFile = new StreamReader(File.Open(path, FileMode.Open));
            string text = readFromFile.ReadToEnd();
            readFromFile.Close();

            int N = KMPsearch(pattern, text, out TimeSpan interval, out int comparisons);
            if (N == -1)
                Console.WriteLine("Алгоритм Кнута-Мориса-Пратта:\nНе найдено\nВремя поиска: {0}\nКоличество сравнений: {1}", interval, comparisons);
            else
                Console.WriteLine("Алгоритм Кнута-Мориса-Пратта:\nИндекс первого вхождения искомой строки: {0}\nВремя поиска: {1}\nКоличество сравнений: {2}", N, interval, comparisons);
            Console.WriteLine();

            N = BMsearch(pattern, text, out TimeSpan interval1, out int comparisons1);
            if (N == -1)
                Console.WriteLine("Алгоритм Бойера-Мура:\nНе найдено\nВремя поиска: {0}\nКоличество сравнений: {1}", interval1, comparisons1);
            else
                Console.WriteLine("Алгоритм Бойера-Мура:\nИндекс первого вхождения искомой строки: {0}\nВремя поиска: {1}\nКоличество сравнений: {2}", N, interval1, comparisons1);
            Console.WriteLine();

            N = Search(pattern, text, out TimeSpan interval2, out int comparisons2);
            if (N == -1)
                Console.WriteLine("Алгоритм простого поиска подстроки:\nНе найдено\nВремя поиска: {0}\nКоличество сравнений: {1}", interval2, comparisons2);
            else
                Console.WriteLine("Алгоритм простого поиска подстроки:\nИндекс первого вхождения искомой строки: {0}\nВремя поиска: {1}\nКоличество сравнений: {2}", N, interval2, comparisons2);
            Console.ReadKey();
        }
    }
}
