using System;
using System.Collections.Generic;
using System.Text;

namespace BankLibrary.Model.DataRepository.Data
{
    /// <summary>
    /// Хранит коллекции имен и фамилий
    /// </summary>
    public class RandomData
    {
        /// <summary>
        /// Коллекция имен сотрудников
        /// </summary>
        private readonly static string[] names;
        /// <summary>
        /// Коллекция фамилий сотрудников
        /// </summary>
        private readonly static string[] surnames;
        /// <summary>
        /// Генератор псевдослучайных чисел
        /// </summary>
        private static Random rnd;

        static RandomData()
        {
            rnd = new Random();
            names = new string[]
            {
                "Диана",
                "Давид",
                "Даниил",
                "Дарья",
                "Дмитрий",
                "Ирина",
                "Иван",
                "Игорь",
                "Илья",
                "Инна",
                "Павел",
                "Петр",
                "Полина",
                "Прохор",
                "Ян",
                "Яков",
                "Ядвига",
                "Глория",
                "Гавриил",
                "Георгий",
                "Галина",
                "Герман",
                "Любовь",
                "Лиана",
                "Лидия",
                "Леонид",
                "Лариса",
                "Роман",
                "Роза",
                "Рудольф",
                "Регина",
                "Руслана",
            };
            surnames = new string[]
            {
                "Абакумов",
                "Абакшин",
                "Абалакин",
                "Абалаков",
                "Абалдуев",
                "Абалки",
                "Баборыко",
                "Бабулин",
                "Бабунин",
                "Бабурин",
                "Бабухин",
                "Багаев",
                "Бадаев",
                "Гагин",
                "Гаевский",
                "Галаев",
                "Галанкин",
                "Галашев",
                "Галигузов",
                "Радзинский",
                "Радилов",
                "Радяев",
                "Разгилдеев",
                "Хаин",
                "Хаит",
                "Хайкин",
                "Хайт",
                "Хайдуков",
                "Хаймин",
                "Якимычев",
                "Якиров",
                "Яковкин",
                "Яковцев",
                "Якобец",
                "Яковенко",
                "Якобсон"

            };
        }


        /// <summary>
        /// Возвращает случайное имя сотрудника
        /// </summary>
        /// <returns></returns>
        public static string GetRandomName()
        {
            return names[rnd.Next(names.Length - 1)];
        }

        /// <summary>
        /// Возвращает случайную фамилию сотрудника
        /// </summary>
        /// <returns></returns>
        public static string GetRandomSurname()
        {
            return surnames[rnd.Next(surnames.Length - 1)];
        }
    }
}
