using BankLibrary.Model.AccountModel.Interfaces;
using BankLibrary.Model.ClientModel.Interfaces;
using BankLibrary.Model.DataRepository.Interfaces;
using BankUI.Core.Common;
using BankUI.Core.Services.Interfaces;
using Prism.Commands;
using Prism.Regions;
using Prism.Services.Dialogs;
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


        public ClientListViewModel(IClientService clientService, IDialogService dialogService)
        {
            _clientService = clientService;
            _dialogService = dialogService;
        }


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
                    Accounts = new ObservableCollection<IAccount>(_client.Accounts); //костыль
                }
            }
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
                if (_selectedAccount.Balance == CommonTypesPrism.zeroValue)
                {
                    Accounts.Remove(_selectedAccount);
                }
                else
                {
                    var dialogParameters = new DialogParameters();
                    string errorMessage = $"Счет {_selectedAccount.Id} не может быть закрыт. На счету имеются средства: {_selectedAccount.Balance}. " +
                                          $"Для закрытия счета переведите средства на другой счет";
                    dialogParameters.Add(CommonTypesPrism.ErrorMessage, errorMessage);
                    _dialogService.ShowDialog(CommonTypesPrism.ErrorDialog, dialogParameters, (result) =>
                    {

                    });
                }
                                
            }
        }

        private DelegateCommand _createNewAccount;
        public DelegateCommand CreateNewAccount =>
            _createNewAccount ?? (_createNewAccount = new DelegateCommand(ExecuteCreateNewAccount));

        void ExecuteCreateNewAccount()
        {
            var dialogParameters = new DialogParameters()
            {
                {CommonTypesPrism.ParameterAccounts, Accounts },
                {CommonTypesPrism.ParameterOwner, Client }
            };            
            _dialogService.Show(CommonTypesPrism.AccountView,dialogParameters, (result)=>
            {
                var newAcc=result.Parameters.GetValue<IAccount>(CommonTypesPrism.ParameterNewAccount);
                var owner = result.Parameters.GetValue<IClient>(CommonTypesPrism.ParameterOwner);
                _clientService.SaveNewAccount(((IStorableDoc)Client).Id,newAcc);
                LoadClients(_currentFolder);
                Client = owner;                
            });
        }

        private DelegateCommand _sendMoneyCommand;
        public DelegateCommand SendMoneyCommand =>
            _sendMoneyCommand ?? (_sendMoneyCommand = new DelegateCommand(ExecuteSendMoneyCommand));

        void ExecuteSendMoneyCommand()
        {
            var dialogParameters = new DialogParameters()
            {              
                {CommonTypesPrism.ParameterOwner, Client }
            };

            _dialogService.Show(CommonTypesPrism.TransactionView, dialogParameters, (result) =>
             {

             });
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
