using BankLibrary.Model;
using BankLibrary.Model.AccountModel.Interfaces;
using BankLibrary.Model.ClientModel;
using BankLibrary.Model.ClientModel.Interfaces;
using BankLibrary.Model.DataRepository.Interfaces;
using BankUI.Core;
using BankUI.Core.Common;
using BankUI.Core.Services.Interfaces;
using Prism.Commands;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace BankApp.Modules.Client.ViewModels
{
    class ClientListViewModel: ViewModelBase
    {
        private string _message;
        private readonly IClientService _clientService;
        private readonly IDialogService _dialogService;
        private string _currentFolder = FolderParameters.Regular;

        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        private IClient _client;
        public IClient Client
        {
            get { return _client; }
            set 
            { 
                SetProperty(ref _client, value);
                if (_client != null)
                {
                    Accounts = new ObservableCollection<IAccount>(_client.Accounts);
                }
            }
        }

        /// <summary>
        /// Жуткий костыль. RaisePropertyChanged не сработал
        /// </summary>
        /// <returns></returns>
        private ObservableCollection<IAccount> GetAccountCollection()
        {
            return new ObservableCollection<IAccount>(_client.Accounts);
        }

        private ObservableCollection<IClient> _bankClients;
        public ObservableCollection<IClient> BankClients
        {
            get { return _bankClients; }
            set { SetProperty(ref _bankClients, value);}
        }

        private ObservableCollection<IAccount> _accounts;
        public ObservableCollection<IAccount> Accounts
        {
            get { return _accounts; }
            set {  SetProperty(ref _accounts, value); }
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

        private DelegateCommand _createNewAccount;
        public DelegateCommand CreateNewAccount =>
            _createNewAccount ?? (_createNewAccount = new DelegateCommand(ExecuteCreateNewAccount));

        void ExecuteCreateNewAccount()
        {
            var dialogParameters = new DialogParameters();
            dialogParameters.Add("accounts", Accounts);
            dialogParameters.Add("owner", Client);
            _dialogService.Show("AccountView",dialogParameters, (result)=>
            {
                var newAcc=result.Parameters.GetValue<IAccount>("newAccount");
                var owner = result.Parameters.GetValue<IClient>("owner");
                _clientService.SaveNewAccount(((IStorableDoc)Client).Id,newAcc);
                LoadClients(_currentFolder);
                Client = owner;
                //Accounts = GetAccountCollection();
            });
        }

        public ClientListViewModel(IClientService clientService,IDialogService dialogService)
        {
            _clientService = clientService;
            _dialogService = dialogService;
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            _currentFolder = navigationContext.Parameters.GetValue<string>(FolderParameters.FolderKey);
            LoadClients(_currentFolder);
            Client = BankClients.FirstOrDefault();            
        }

        private void LoadClients(string folder)
        {
            switch (folder)
            {
                case FolderParameters.Regular:
                    {
                        BankClients = new ObservableCollection<IClient>(_clientService.GetRegularClients());
                        break;
                    }
                case FolderParameters.Special:
                    {
                        BankClients = new ObservableCollection<IClient>(_clientService.GetSpecialClients());
                        break;
                    }               
                default:
                    break;
            }
        }
    }
}
