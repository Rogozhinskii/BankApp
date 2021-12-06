using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace BankUI.Core.Common
{
    /// <summary>
    /// Для реализации иерархического отображения навигационных параметров (Перемещение по папкам в BankApp.Modules.Client.Controls.ClientGroup)
    /// </summary>
    public class NavigationItem
    {
        /// <summary>
        /// Заголовок
        /// </summary>
        public string Caption { get; set; }
        /// <summary>
        /// Навигационный путь
        /// </summary>
        public string NavigationPath { get; set; }
        /// <summary>
        /// Коллекция вложенных элементов
        /// </summary>
        public ObservableCollection<NavigationItem> Items { get; set; }

        /// <summary>
        /// флаг на раскрытие в TreeView
        /// </summary>
        public bool IsExpanded { get; set; } = false;

        /// <summary>
        /// флаг на свойство IsSelected в TreeView
        /// </summary>
        public bool IsSelected { get; set; } = false;
        public NavigationItem()
        {
            Items = new ObservableCollection<NavigationItem>();
        }

    }
}
