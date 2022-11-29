using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using static C_Sharp_Delegate.Program;

namespace C_Sharp_Delegate
{
    internal class Program
    {

        //Клас із статичними методами для роботи із масивами
        class WWArray
        {
            ////Функція заповнення масиву значеннями в межі МІН-МАХ
            public static void GenerateArray(ref int[] arr, int min, int max)
            {
                Random rnd = new();

                for (int i = 0; i < arr.Length; i++)
                {
                    arr[i] = rnd.Next(min, max);
                }
            }
            ////Функція друку масиву на екран
            public static void PrintArray(int[] arr)
            {
                string separator = new('-', arr.Length * 3);
                Console.WriteLine(separator);
                for (int i = 0; i < arr.Length; i++)
                {
                    Console.Write($"{arr[i]} ");
                }
                Console.WriteLine($"\n{separator}");
            }

            public static int[] Even(int[] array)
            {
                List<int> values = new();

                foreach (int item in array)
                {
                    if (item % 2 == 0 && item != 0)
                        values.Add(item);
                }
                return values.ToArray();
            }

            public static int[] Odd(int[] array)
            {
                List<int> values = new();

                foreach (int item in array)
                {
                    if (item % 2 != 0)
                        values.Add(item);
                }
                return values.ToArray();
            }

            public static int[] SimplyNum(int[] array)
            {
                List<int> values = new();

                foreach (int item in array)
                {
                    if (item > 1)
                    {
                        int count = 0;
                        for (int i = 1; i <= item; i++)
                            if (item % i == 0) count++;

                        if (count == 2) values.Add(item);
                    }

                }
                return values.ToArray();
            }

            public static int[] FibonacciReturn(int[] arr)
            {
                List<int> values = new();

                //генерируем масив Фібаначі
                int[] array = new int[30];
                for (int i = 0; i < 20; ++i)
                    array[i] = i < 2 ? 1 : array[i - 2] + array[i - 1];
                
                //порівнюємо наш масив із масивом Фібаначі
                for (int i = 0; i < arr.Length; i++)
                    if (array.Contains(arr[i]))
                        if (!values.Contains(arr[i]))                       
                            values.Add(arr[i]);
                
                if(values.Count > 0) 
                { 
                    values.Sort();
                    return values.ToArray(); 
                }
                else 
                {
                    Console.WriteLine("В масиву чисел Фібоначі не знайдено");
                    return values.ToArray();
                }
                
            }
        }

        
        class Triangle
        {
            private double a;
            private double b;
            private double c;
            private double p; 

            private void SetA(double num) { if (num > 0) a = num; else a = 1; }
            private void SetB(double num) { if (num > 0) b = num; else b = 1; }
            private void SetC(double num) { if (num > 0) c = num; else c = 1; }

            public Triangle()
            {
                Console.WriteLine("Введіть довжину сторони А");
                double a = double.Parse(Console.ReadLine());
                Console.WriteLine("Введіть довжину сторони B");
                double b = double.Parse(Console.ReadLine());
                Console.WriteLine("Введіть довжину сторони C");
                double c = double.Parse(Console.ReadLine());

                SetA(a); SetB(b); SetC(c);
                p = (a + b + c) / 2;
            }

            public void ShowPerimetr() { Console.WriteLine($"Періметр = {p}"); }
            public double ShowSquare() 
            { 
              return Math.Round(Math.Sqrt(p * (p - a) * (p - b) * (p - c)), 2);
            }
        }

        static void SquareReactangle(double a, double b) => Console.WriteLine($"Площа прямокутника = {a*b}");


        ////Делегати
        public delegate void DelegateArrayVoid(int[] a);
        public delegate int[] DelegateArray(int[] a);

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;

            DelegateArrayVoid show = WWArray.PrintArray;
            
            int[] arr = new int[30];
            WWArray.GenerateArray(ref arr, -10, 34);

            Console.WriteLine("Рандомно згенерований масив");
            show(arr);

            Console.WriteLine("Чьотні числа масиву");
            DelegateArray arrWork = WWArray.Even;
            show(arrWork(arr));

            Console.WriteLine("Не чьотні числа масиву");
            arrWork += WWArray.Odd;
            show(arrWork(arr));

            Console.WriteLine("Прості числа масиву");
            arrWork += WWArray.SimplyNum;
            show(arrWork(arr));

            Console.WriteLine("Числа Фібоначі у масиву");
            arrWork += WWArray.FibonacciReturn;
            show(arrWork(arr));

            Console.ReadKey();
            Console.Clear();

            Console.WriteLine("Відображення поточного часу");
            Action ShowDataTime = new Action(()=> Console.WriteLine($"\t{DateTime.Now.ToShortTimeString()}"));
            ShowDataTime();

            Console.WriteLine("Відображення поточної дати");
            ShowDataTime = new Action(() => Console.WriteLine($"\t{DateTime.Now.ToShortDateString()}"));
            ShowDataTime();

            Console.WriteLine("Відображення поточного дня тижня");
            ShowDataTime = new Action(() => Console.WriteLine("\t{0}", DateTime.Now.DayOfWeek));
            ShowDataTime();

            Console.ReadKey();
            Console.Clear();

            Console.WriteLine("Відображення площі трикутника");
            Triangle t = new Triangle();


            Action Square = new Action(() => Console.WriteLine($"Площа трикутника: {t.ShowSquare()}"));
            Square();

            Console.WriteLine("Відображення площі прямокутника");
            Console.WriteLine("Введіть довжину сторони прямокутника ");
            double a = double.Parse(Console.ReadLine());
            Console.WriteLine("Введіть довжину іншої сторони прямокутника ");
            double b = double.Parse(Console.ReadLine());
            void ShowSquare(double a, double b, Action<double, double> op) => op(a, b);
            ShowSquare(a, b, SquareReactangle);

        }
    }
    
}
