using System;
using System.IO;

namespace Lab8._1
{
    class Program
    {
        static int Search (int elem,int [] array, out TimeSpan interval, out int comparisons)
        {
            comparisons = 0;
            DateTime StartTime;
            StartTime = DateTime.Now;
            int i = 0;
            int N=-1;
            while ( i < array.Length)
            {
                comparisons++;
                if (array[i] == elem)
                {       
                    N = i;
                    break;
                }
                else i++;
            }           
            DateTime EndTime = DateTime.Now;
            interval = EndTime - StartTime;
            return N;
            
        }
         static int BinarySearch(int elem, int[] array, out TimeSpan interval, out int comparisons)
        {
            comparisons = 0;
            DateTime StartTime;
            StartTime = DateTime.Now;
            int l = 0, r = array.Length - 1;
            int N = -1;
            while (r>=l)
            {

                comparisons++;
                int mid = (l + r) / 2;
                if (array[mid] == elem)
                {
                    N = mid;
                    break;
                }
                if (array[mid] < elem)
                    r = mid - 1;
                else
                    l = mid + 1;
            }
            DateTime EndTime = DateTime.Now;
            interval = EndTime - StartTime;
            return N;
        }
         static int InterpSearch(int elem, int[] array, out TimeSpan interval, out int comparisons)
        {
            comparisons = 0;
            DateTime StartTime;
            StartTime = DateTime.Now;
            int l = 0, r = array.Length - 1;
            int N = -1;
            while( r >= l)
            {              
                int mid = l + (r - l) * (elem - array[l]) / (array[r] - array[l]);
                comparisons++;
                if (elem > array[mid])
                    r = mid - 1;
                else if (elem < array[mid])
                    l = mid + 1;
                else
                {
                    N = mid;
                    break;
                }
            }
            DateTime EndTime = DateTime.Now;
            interval = EndTime - StartTime;
            return N;
        }
        static void Main(string[] args)
        {
            string path = @"C:\Users\alisa\source\repos\Lab8.1\sorted.dat";
            StreamReader readFromFile = new StreamReader(File.Open(path, FileMode.Open));
            Console.WriteLine("Enter the number:");
            int N = int.Parse(Console.ReadLine());
            string line = readFromFile.ReadLine();
            string[] arrayOfString = line.Split(' ');
            int[] arrayOfNumbers = new int[1000];
            for (int i=0; i<arrayOfString.Length-1;i++)
            {
                arrayOfNumbers[i] = int.Parse(arrayOfString[i]);
            }
            readFromFile.Close();
            int position = Search(N, arrayOfNumbers, out TimeSpan interval, out int comparisons);
            if (position==-1)
              Console.WriteLine("Линейный поиск:\nНе найдено\nвремя работы: {0}\nколичество сравнений: {1}",interval, comparisons);
            else
              Console.WriteLine("Линейный поиск:\nпозиция искомого элемента: {0}\nвремя работы: {1}\nколичество сравнений: {2}",position, interval, comparisons);
            Console.WriteLine();

            position = BinarySearch(N, arrayOfNumbers, out TimeSpan interval1, out int comparisons1);
            if (position == -1)
                Console.WriteLine("Бинарный поиск:\nНе найдено\nвремя работы: {0}\nколичество сравнений: {1}", interval1, comparisons1);
            else
                Console.WriteLine("Бинарный поиск:\nпозиция искомого элемента: {0}\nвремя работы: {1}\nколичество сравнений: {2}", position, interval1, comparisons1);
            Console.WriteLine();

            position = InterpSearch(N, arrayOfNumbers, out TimeSpan interval2, out int comparisons2);
            if (position == -1)
                Console.WriteLine("Интерполяционный поиск:\nНе найдено\nвремя работы: {0}\nколичество сравнений: {1}", interval2, comparisons2);
            else
                Console.WriteLine("Интерполяционный поиск:\nпозиция искомого элемента: {0}\nвремя работы: {1}\nколичество сравнений: {2}", position, interval2, comparisons2);
            Console.ReadKey();
        }
    }
}
