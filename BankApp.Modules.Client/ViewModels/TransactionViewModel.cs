using BankLibrary.Model.AccountModel;
using BankLibrary.Model.AccountModel.Interfaces;
using BankLibrary.Model.ClientModel.Interfaces;
using BankUI.Core.Common;
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
    public class TransactionViewModel : DialogViewModelBase, IDataErrorInfo
    {
        private readonly ITransactionManager<IAccount> _transactionManager;
        private readonly IClientService _clientService;
        private readonly IDialogService _dialogService;
        private IClient _owner;


        public TransactionViewModel(ITransactionManager<IAccount> transactionManager, IClientService clientService,IDialogService dialogService)
        {
            _transactionManager = transactionManager;
            _clientService = clientService;
            _dialogService = dialogService;
        }

        private float _amount;
        public float Amount
        {
            get { return _amount; }
            set { SetProperty(ref _amount, value); }
        }

        private IAccount _fromAccount;
        public IAccount FromAccount
        {
            get { return _fromAccount; }
            set { SetProperty(ref _fromAccount, value); }
        }


        public ObservableCollection<IAccount> OwnerAccounts => GetOwnerAccounts();
       
        private ObservableCollection<IAccount> GetOwnerAccounts()
        {
            var result = new ObservableCollection<IAccount>();
            if (_owner != null)
                result = new ObservableCollection<IAccount>(_owner.Accounts.Where(x=>x.AccountType!=AccountType.Deposit));
            return result;
        }

        private IClient _selectedRecipient;
        public IClient SelectedRecipient
        {
            get { return _selectedRecipient; }
            set 
            { 
                SetProperty(ref _selectedRecipient, value);
                if (_selectedRecipient != null)
                {
                    RecipientAccounts = new ReadOnlyCollection<IAccount>(_selectedRecipient.Accounts);
                }
                
            }
        }

        private ReadOnlyCollection<IAccount> _recipientAccounts;
        public ReadOnlyCollection<IAccount> RecipientAccounts
        {
            get { return _recipientAccounts; }
            set { SetProperty(ref _recipientAccounts, value); }
        }

        private IAccount _toAccount;
        public IAccount ToAccount
        {
            get { return _toAccount; }
            set { SetProperty(ref _toAccount, value); }
        }

        private ReadOnlyCollection<IClient> _recipients;
        public ReadOnlyCollection<IClient> Recipients
        {
            get { return _recipients; }
            set { SetProperty(ref _recipients, value); }
        }

        public string Error => "";

        public string this[string columnName]
        {
            get
            {
                string error = string.Empty;
                switch (columnName)
                {
                    case "Amount":
                        if (FromAccount != null && FromAccount.Balance < _amount)
                            error = "Не достаточно средств";
                        break;
                }
                return error;
            }
        }

        private DelegateCommand _sendMoneyCommand;
        public DelegateCommand SendMoneyCommand =>
            _sendMoneyCommand ?? (_sendMoneyCommand = new DelegateCommand(ExecuteSendMoneyCommand));

        void ExecuteSendMoneyCommand()
        {
            var result=_transactionManager.SendMoneyToAccount(FromAccount, ToAccount, Amount);
            RaisePropertyChanged(nameof(OwnerAccounts));
            ShowDialog(result);
            Amount = 0f;
        }

        private void ShowDialog(bool result)
        {
            var dialogParameters = new DialogParameters();
            if (result)
            {
                string notifyMessage = "Перевод выполнен успешно";
                dialogParameters.Add(CommonTypesPrism.NotificationMessage, notifyMessage);
                _dialogService.ShowDialog(CommonTypesPrism.NotificationDialog, dialogParameters, result => { });

            }
            else
            {
                string errorMessage = "Перевод выполнить не возможно";
                dialogParameters.Add(CommonTypesPrism.ErrorMessage, errorMessage);
                _dialogService.ShowDialog(CommonTypesPrism.ErrorDialog, dialogParameters, result => { });
            }
        }

        private DelegateCommand _exitCommand;
        public DelegateCommand ExitCommand =>
            _exitCommand ?? (_exitCommand = new DelegateCommand(ExecuteExitCommand));

        

        void ExecuteExitCommand()
        {
            IDialogResult dialogResult =new DialogResult(); //на случай необходимости обработки callback            
            RaiseRequestClose(dialogResult);
        }

        public override void OnDialogOpened(IDialogParameters parameters)
        {
            _owner = parameters.GetValue<IClient>(CommonTypesPrism.ParameterOwner);
            RaisePropertyChanged(nameof(OwnerAccounts));
            var tempList = new List<IClient>(_clientService.GetRegularClients());
            tempList.AddRange(_clientService.GetSpecialClients());
            tempList.Remove(_owner);
            Recipients = new ReadOnlyCollection<IClient>(tempList);
           
        }

    }
}
