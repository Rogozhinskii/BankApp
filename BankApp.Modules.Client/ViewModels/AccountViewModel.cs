using BankLibrary.Model.AccountModel;
using BankLibrary.Model.AccountModel.Interfaces;
using BankLibrary.Model.DataRepository.Interfaces;
using BankUI.Core.Common;
using BankUI.Core.Services.Interfaces;
using Prism.Commands;
using Prism.Services.Dialogs;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace BankApp.Modules.Client.ViewModels
{
    public class AccountViewModel : DialogViewModelBase,IDataErrorInfo
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

        public override string Title => "Account Dialog";

       
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
            dialogResult.Parameters.Add(CommonTypesPrism.ParameterNewAccount, newAccount);
            dialogResult.Parameters.Add(CommonTypesPrism.ParameterOwner, _owner);
            RaiseRequestClose(dialogResult);            
        }

        public override bool CanCloseDialog()
        {
            return true;
        }

        public override void OnDialogOpened(IDialogParameters parameters)
        {
            _owner = parameters.GetValue<IStorableDoc>(CommonTypesPrism.ParameterOwner);
            Accounts = new ReadOnlyObservableCollection<IAccount>(parameters.GetValue<ObservableCollection<IAccount>>(CommonTypesPrism.ParameterAccounts));
        }
    }
}
