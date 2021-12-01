using BankLibrary.Model;
using BankLibrary.Model.AccountModel.Interfaces;
using BankLibrary.Model.ClientModel;
using BankLibrary.Model.DataRepository.Interfaces;
using BankUI.Core;
using BankUI.Core.Common;
using BankUI.Core.Services.Interfaces;
using Prism.Commands;
using Prism.Regions;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace BankApp.Modules.Client.ViewModels
{
    class ClientListViewModel: ViewModelBase
    {
        private string _message;
        private readonly IClientService _clientService;

        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        private IStorableDoc _client;
        public IStorableDoc Client
        {
            get { return _client; }
            set { SetProperty(ref _client, value); Accounts = new ObservableCollection<IAccount>(_client.Accounts); }
        }

        private ObservableCollection<IStorableDoc> _bankClients;
        public ObservableCollection<IStorableDoc> BankClients
        {
            get { return _bankClients; }
            set { SetProperty(ref _bankClients, value); }
        }

        private ObservableCollection<IAccount> _accounts;
        public ObservableCollection<IAccount> Accounts
        {
            get { return _accounts; }
            set { SetProperty(ref _accounts, value); }
        }

        private IAccount _selectedAccount;
        public IAccount SelectedAccount
        {
            get { return _selectedAccount; }
            set { SetProperty(ref _selectedAccount, value); }
        }

        private DelegateCommand _deleteAccountCommand;
        public DelegateCommand DeleteAccountCommand =>
            _deleteAccountCommand ?? (_deleteAccountCommand = new DelegateCommand(ExecuteDeleteAccountCommand));

        void ExecuteDeleteAccountCommand()
        {
            if (_selectedAccount != null)
            {
                Accounts.Remove(_selectedAccount);
            }
        }


        public ClientListViewModel(IClientService clientService)
        {
            _clientService = clientService;
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            string tt = navigationContext.Parameters.GetValue<string>(FolderParameters.FolderKey);
            LoadClients(tt);
            Client = BankClients.FirstOrDefault();

        }

        private void LoadClients(string folder)
        {
            switch (folder)
            {
                case FolderParameters.Regular:
                    {
                        BankClients = new ObservableCollection<IStorableDoc>(_clientService.GetRegularClients());
                        break;
                    }
                case FolderParameters.Special:
                    {
                        BankClients = new ObservableCollection<IStorableDoc>(_clientService.GetSpecialClients());
                        break;
                    }               
                default:
                    break;
            }
        }
    }
}
