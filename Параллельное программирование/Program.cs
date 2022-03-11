using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Параллельное_программирование
{
   // Сформировать массив случайных целых чисел(размер задается пользователем).
   // Вычислить сумму чисел массива и максимальное число в массиве.
   // Реализовать решение  задачи с  использованием механизма  задач продолжения.


    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите размер массива");
            int n = Convert.ToInt32(Console.ReadLine());
            Func<object, int[]> func1 = new Func<object, int[]>(Getarray);
            Task<int[]> task1 = new Task<int[]>(func1, n);

            Func<Task<int[]>, int[]> func2 = new Func<Task<int[]>, int[]>(Sumarray);
            Task<int[]> task2 = task1.ContinueWith<int[]>(func2);

            Func<Task<int[]>, int[]> func3 = new Func<Task<int[]>, int[]>(Maxarray);
            Task<int[]> task3 = task2.ContinueWith<int[]>(func3);

            task1.Start();

            Console.ReadKey();
        }

        static int[] Getarray(object a)
        {
            int n = (int)a;
            int[] arr = new int[n];
            
            Random random = new Random();
            for (int i = 0; i < n; i++)
            {
                arr[i] = random.Next(0, 10);
                Console.Write($"{arr[i]}" + " ");
            }
            Console.WriteLine();
            return arr;
        }

       

        static int[] Sumarray(Task<int[]> task)
        {
            int[] arr = task.Result;
            int sum = 0;

            for (int i = 0; i < arr.Count(); i++)
            {
               
                sum += arr[i];

            }
            Console.WriteLine(sum);
            return arr;
        }

        static int[] Maxarray(Task<int[]> task)
        {
            int[] arr = task.Result;
            int max = 0;
            for (int i = 0; i < arr.Count(); i++)
            {
                max = arr[0];

                    if (max<arr[i])
                    {
                        max = arr[i];
                    }
                
            }
            Console.WriteLine(max);
            return arr;
        }

       
    }
}
