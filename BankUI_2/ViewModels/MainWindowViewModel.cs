using BankUI.Core.Common;
using BankUI.Interfaces;
using Prism.Commands;
using Prism.Regions;
using System;

namespace BankUI.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly IRegionManager _regionManager;
        private string _title = "blblblblbl";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private DelegateCommand<string> _navigationCommand;
        public DelegateCommand<string> NavigationCommand =>
            _navigationCommand ?? (_navigationCommand = new DelegateCommand<string>(ExecuteNavigationCommand));

        private void ExecuteNavigationCommand(string navigationPath)
        {
            if (string.IsNullOrEmpty(navigationPath))
                throw new ArgumentNullException(nameof(navigationPath));
            _regionManager.RequestNavigate(CommonTypesPrism.ContentRegion, navigationPath);
        }

        public MainWindowViewModel(IRegionManager regionManager,IApplicationCommands applicationCommands)
        {
            _regionManager = regionManager;
            applicationCommands.NavigateCommand.RegisterCommand(NavigationCommand);

        }
    }
}
