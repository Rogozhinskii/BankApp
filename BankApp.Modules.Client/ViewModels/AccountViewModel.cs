using BankLibrary.Model.AccountModel;
using BankLibrary.Model.AccountModel.Interfaces;
using BankUI.Core.Services.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace BankApp.Modules.Client.ViewModels
{
    public class AccountViewModel : BindableBase, IDialogAware,IDataErrorInfo
    {
        

        public AccountViewModel()
        {
                      
        }

        public string Title => "Account Dialog";

        public event Action<IDialogResult> RequestClose;

        private AccountType _accountType;
        public AccountType AccountType
        {
            get { return _accountType; }
            set { SetProperty(ref _accountType, value); }
        }

        private ReadOnlyObservableCollection<IAccount> _accounts;
        public ReadOnlyObservableCollection<IAccount> Accounts
        {
            get { return _accounts; }
            set { SetProperty(ref _accounts, value); }
        }

        private IAccount _fromAccount;
        public IAccount FromAccount
        {
            get { return _fromAccount; }
            set { SetProperty(ref _fromAccount, value); }
        }

        private float _balance;
        public float Balance
        {
            get { return _balance; }
            set { SetProperty(ref _balance, value); }
        }


        private DelegateCommand _saveNewAccount;
        public DelegateCommand SaveNewAccount =>
            _saveNewAccount ?? (_saveNewAccount = new DelegateCommand(ExecuteSaveAccountCommand));

        public string Error => throw new NotImplementedException();

        public string this[string columnName]
        {
            get
            {
                string error = string.Empty;
                switch (columnName)
                {
                    case "Balance":
                        if (FromAccount!=null && FromAccount.Balance < _balance)
                            error = "Не достаточно средств";
                        break;
                }
                return error;
            }
        }

        void ExecuteSaveAccountCommand()
        {
            IDialogResult dialogResult = new DialogResult();
            var account = GetNewAccount(AccountType);
            dialogResult.Parameters.Add("newAccount", account);
            RequestClose?.Invoke(dialogResult);
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
            Accounts = new ReadOnlyObservableCollection<IAccount>(parameters.GetValue<ObservableCollection<IAccount>>("accounts"));                
        }

        private IAccount GetNewAccount(AccountType accountType)
        {
            //switch (accountType)
            //{
            //    case AccountType.Deposit:
            //        return new DepositAccount();
            //    case AccountType.NonDeposit:
            //        return new BankAccount();
            //    default:
            //        return null;
            //}

            IAccount result = accountType switch
            {
                AccountType.Deposit => new DepositAccount(),
                AccountType.Savings => new BankAccount(),
                _ => null
            };

            return result;
        }
    }
}
