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
using System.Linq;

namespace BankApp.Modules.Client.ViewModels
{
    public class TransactionViewModel : DialogViewModelBase
    {
        private readonly ITransactionManager<IAccount> _transactionManager;
        private readonly IClientService _clientService;
        private readonly IClient _owner;


        public TransactionViewModel(ITransactionManager<IAccount> transactionManager, IClientService clientService)
        {
            _transactionManager = transactionManager;
            _clientService = clientService;
        }

        private IAccount _fromAccount;
        public IAccount FromAccount
        {
            get { return _fromAccount; }
            set { SetProperty(ref _fromAccount, value); }
        }

        private ObservableCollection<IAccount> _ownerAccounts;
        public ObservableCollection<IAccount> OwnerAccounts
        {
            get { return _ownerAccounts; }
            set { SetProperty(ref _ownerAccounts, value); }
        }

        private ObservableCollection<IAccount> _recipientAccounts;
        public ObservableCollection<IAccount> RecipientAccounts
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

        public override void OnDialogOpened(IDialogParameters parameters)
        {
            
        }

    }
}
