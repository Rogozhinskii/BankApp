using BankLibrary.Model.AccountModel;
using BankLibrary.Model.AccountModel.Interfaces;
using BankLibrary.Model.ClientModel.Interfaces;
using BankLibrary.Model.DataRepository.Interfaces;
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
        private readonly IAccountService _accountService;
        private readonly ITransactionManager<IAccount> _transactionManager;
        private readonly IClientService _clientService;
        private IStorableDoc _owner;

        public AccountViewModel(IAccountService accountService, ITransactionManager<IAccount> transactionManager,IClientService clientService)
        {
            _accountService = accountService;
            _transactionManager = transactionManager;
            _clientService = clientService;
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
            var accountManager = _accountService.GetAccountManager(AccountType);
            var newAccount = accountManager.CreateNewAccount();
            _transactionManager.SendMoneyToAccount(FromAccount, newAccount, Balance);
            _clientService.UpdateAccount(_owner.Id,FromAccount);
            dialogResult.Parameters.Add("newAccount", newAccount);
            dialogResult.Parameters.Add("owner", _owner);
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
            _owner = parameters.GetValue<IStorableDoc>("owner");
            Accounts = new ReadOnlyObservableCollection<IAccount>(parameters.GetValue<ObservableCollection<IAccount>>("accounts"));
        }

        private IAccount GetNewAccount(AccountType accountType)
        {
            IAccount result = accountType switch
            {
                AccountType.Deposit => new DepositAccount(),
                AccountType.Savings => new SavingAccount(),
                _ => null
            };

            return result;
        }
    }
}
