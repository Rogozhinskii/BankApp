using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Task2_Parallel
{
    internal class Program
    {
        /// <summary>
        /// начальное значение переменной цикла
        /// </summary>
        private static int _start = 1_000_000_000;
        /// <summary>
        /// начальное значение переменной цикла
        /// </summary>
        private static int _stop = 2_000_000_000;
        /// <summary>
        /// счетчик вхождений
        /// </summary>
        private static int _count = 0;
        /// <summary>
        /// объект синхронизации
        /// </summary>
        private static object _lockObj = new();
        static void Main(string[] args)
        {
            Stopwatch sw = new();
            sw.Start();
            Parallel.For(_start, _stop, Numerator);
            sw.Stop();
            Console.WriteLine($"Количество чисел:{_count}, Время: {sw.Elapsed.TotalSeconds}");
        }

        /// <summary>
        /// Увеличивает счетчик, если сумма цифр числе кратна последней цифре числа
        /// </summary>
        /// <param name="number"></param>
        private static void Numerator(int number)
        {
            int[] numArr=number.ToString().Select(x=>int.Parse(x.ToString()))
                                          .ToArray();
            int sum=numArr.Sum();
            if (numArr[^1] == 0)
                return;
            if (sum % numArr[^1] == 0)
            {
                lock (_lockObj)
                {
                    _count++;
                }
            }
        }
    }
}
