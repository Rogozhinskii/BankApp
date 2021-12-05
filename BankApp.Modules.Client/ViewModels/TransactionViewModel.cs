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
        private IClient _owner;


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

        private ReadOnlyCollection<IAccount> _ownerAccounts;
        public ReadOnlyCollection<IAccount> OwnerAccounts
        {
            get { return _ownerAccounts; }
            set { SetProperty(ref _ownerAccounts, value); }
        }

        private IClient _selectedRecipient;
        public IClient SelectedRecipient
        {
            get { return _selectedRecipient; }
            set 
            { 
                SetProperty(ref _selectedRecipient, value);
                RecipientAccounts = new ReadOnlyCollection<IAccount>(_selectedRecipient.Accounts);
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


        public override void OnDialogOpened(IDialogParameters parameters)
        {
            _owner = parameters.GetValue<IClient>(CommonTypesPrism.ParameterOwner);
            OwnerAccounts = new ReadOnlyCollection<IAccount>(_owner.Accounts);
            var tempList = new List<IClient>(_clientService.GetRegularClients());
            tempList.AddRange(_clientService.GetSpecialClients());
            Recipients = new ReadOnlyCollection<IClient>(tempList);
           
        }

    }
}
