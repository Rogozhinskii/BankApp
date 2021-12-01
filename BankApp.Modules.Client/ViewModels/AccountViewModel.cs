using BankLibrary.Model.AccountModel;
using BankLibrary.Model.AccountModel.Interfaces;
using BankUI.Core.Services.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BankApp.Modules.Client.ViewModels
{
    public class AccountViewModel : BindableBase, IDialogAware
    {
        private readonly IClientService _clientService;

        public AccountViewModel(IClientService clientService)
        {
            _clientService = clientService;
        }

        public string Title => "Account Dialog";

        public event Action<IDialogResult> RequestClose;

        private IAccount _account;
        public IAccount Account
        {
            get { return _account; }
            set { SetProperty(ref _account, value); }
        }

        private AccountType _accountType;
        public AccountType AccountType
        {
            get { return _accountType; }
            set { SetProperty(ref _accountType, value); }
        }

        private DelegateCommand _saveNewAccount;
        public DelegateCommand SaveNewAccount =>
            _saveNewAccount ?? (_saveNewAccount = new DelegateCommand(ExecuteSaveAccountCommand));

        void ExecuteSaveAccountCommand()
        {
            
        }

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
            
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            var account = parameters.GetValue<IAccount>("account");
            if (account == null)
                Account = account;
        }
    }
}
