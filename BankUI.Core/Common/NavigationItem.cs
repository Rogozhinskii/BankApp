using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace BankLibrary.Common
{
    public class NavigationItem
    {
        public string Caption { get; set; }
        public string NavigationPath { get; set; }
        public ObservableCollection<NavigationItem> Items { get; set; }

        public bool IsExpanded { get; set; } = false;
        public bool IsSelected { get; set; } = false;
        public NavigationItem()
        {
            Items = new ObservableCollection<NavigationItem>();
        }

    }
}
