using BankUI.Core.Common;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BankApp.Modules.NotificationTools.ViewModels
{
    public class StatusBarViewModel : BindableBase
    {
        private readonly IDialogService _dialogService;

        public StatusBarViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService;
        }

        private DelegateCommand _showLogCommand;
        public DelegateCommand ShowLogCommand =>
            _showLogCommand ?? (_showLogCommand = new DelegateCommand(ExecuteShowLogCommand));

        void ExecuteShowLogCommand()
        {
            _dialogService.Show(CommonTypesPrism.LogForm, null, result =>
              {

              });
        }
    }
}
