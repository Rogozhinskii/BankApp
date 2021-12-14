using System;

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
            Console.WriteLine("Hello World!");
        }
    }
}
