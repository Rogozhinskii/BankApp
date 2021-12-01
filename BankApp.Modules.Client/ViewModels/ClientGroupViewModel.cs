using BankUI.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using BankUI.Core.Common;

namespace BankApp.Modules.Client.ViewModels
{
    public class ClientGroupViewModel : BindableBase
    {
        private readonly IApplicationCommands _applicationCommand;
        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private DelegateCommand<NavigationItem> _selectedCommand;
        public DelegateCommand<NavigationItem> SelectedCommand =>
            _selectedCommand ?? (_selectedCommand = new DelegateCommand<NavigationItem>(SelectedItemNavigateCommand));

        void SelectedItemNavigateCommand(NavigationItem navigationItem)
        {
            if (navigationItem != null)
            {
                _applicationCommand.NavigateCommand.Execute(navigationItem.NavigationPath);
            }
        }

        private ObservableCollection<NavigationItem> _items;
        public ObservableCollection<NavigationItem> Items
        {
            get { return _items; }
            set { SetProperty(ref _items, value); }
        }
        public ClientGroupViewModel(IApplicationCommands applicationCommands)
        {            
            GenerateMenu();
            _applicationCommand = applicationCommands;
        }

        private void GenerateMenu()
        {
            Items = new ObservableCollection<NavigationItem>();
            var root = new NavigationItem()
            {
                Caption = "Группы клиентов",
                IsExpanded = true
            };
            root.Items.Add(new NavigationItem()
            {
                Caption = Resources.Folder_Regular,
                IsSelected=true,
                NavigationPath= GetNavigationPath(FolderParameters.Regular)

            });
            root.Items.Add(new NavigationItem()
            {
                Caption = Resources.Folder_Special,
                NavigationPath=GetNavigationPath(FolderParameters.Special)                
            });

            Items.Add(root);
        }

        private string GetNavigationPath(string folder)
        {
            return $"ClientList?{FolderParameters.FolderKey}={folder}";
        }

    }
}
