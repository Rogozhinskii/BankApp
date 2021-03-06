using BankLibrary.Model.AccountModel.Interfaces;
using BankLibrary.Model.ClientModel.Interfaces;
using BankLibrary.Model.DataRepository.Interfaces;
using BankUI.Core.Common;
using BankUI.Core.Common.Log;
using BankUI.Core.EventAggregator;
using BankUI.Core.Services.Interfaces;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace BankApp.Modules.Client.ViewModels
{
    /// <summary>
    /// ViewModel отображаемого списка клентов и их счетов
    /// </summary>
    class ClientListViewModel: ViewModelBase
    {
        /// <summary>
        /// Серввис доступа к хранилищу клиентов
        /// </summary>
        private readonly IClientService _clientService;

        /// <summary>
        /// Сервис диалоговых окон
        /// </summary>
        private readonly IDialogService _dialogService;
        private readonly IEventAggregator _eventAgregator;

        /// <summary>
        /// Выбранный элемент в навигационном боковом баре
        /// </summary>
        private string _currentFolder = FolderParameters.Regular;


        public ClientListViewModel(IClientService clientService, IDialogService dialogService,IEventAggregator eventAgregator)
        {
            _clientService = clientService;
            _dialogService = dialogService;
            _eventAgregator = eventAgregator;
        }

        #region Свойства
        private IClient _client;

        /// <summary>
        /// Выбранный клиент
        /// </summary>
        public IClient Client
        {
            get { return _client; }
            set
            {
                SetProperty(ref _client, value);
                if (_client != null)
                {
                    RaisePropertyChanged(nameof(Accounts)); //дергаем зависимое свойство
                }
            }
        }


        private ObservableCollection<IClient> _bankClients;

        /// <summary>
        /// Коллекция отображаемых клиентов
        /// </summary>
        public ObservableCollection<IClient> BankClients
        {
            get { return _bankClients; }
            set { SetProperty(ref _bankClients, value); }
        }


        /// <summary>
        /// Коллекция счетов выбранного клиента
        /// </summary>
        public ObservableCollection<IAccount> Accounts => GetClientAccounts();

        /// <summary>
        /// Вернет коллекцию счетов выбранного клиента
        /// </summary>
        /// <returns></returns>
        private ObservableCollection<IAccount> GetClientAccounts()
        {
            var result = new ObservableCollection<IAccount>();
            if (_client != null)
                result = new ObservableCollection<IAccount>(_client.Accounts);
            return result;
        }

        private IAccount _selectedAccount;

        /// <summary>
        /// Выбранный счет клиента
        /// </summary>
        public IAccount SelectedAccount
        {
            get { return _selectedAccount; }
            set { SetProperty(ref _selectedAccount, value); }
        }

        #endregion

        #region 
        private DelegateCommand _deleteAccountCommand;

        /// <summary>
        /// Реализует закрытие выбранного счета
        /// </summary>
        public DelegateCommand DeleteAccountCommand =>
            _deleteAccountCommand ?? (_deleteAccountCommand = new DelegateCommand(ExecuteDeleteAccountCommand));

        void ExecuteDeleteAccountCommand()
        {
            if (_selectedAccount != null)
            {
                if (_selectedAccount.Balance == CommonTypesPrism.zeroValue)
                {
                    _client.Accounts.Remove(_selectedAccount);                    
                }
                else
                {
                    var dialogParameters = new DialogParameters();
                    string errorMessage = $"Счет {_selectedAccount.Id} не может быть закрыт. На счету имеются средства: {_selectedAccount.Balance}. " +
                                          $"Для закрытия счета переведите средства на другой счет";
                    _eventAgregator.GetEvent<LogEvent>().Publish(new LogRecord {LogRecordLevel=LogRecordLevel.Error, Message=$"Время {DateTime.Now}-->" +
                        $" Ошибка закрытия счета: {_selectedAccount.Id}" });
                    dialogParameters.Add(CommonTypesPrism.ErrorMessage, errorMessage);
                    _dialogService.ShowDialog(CommonTypesPrism.ErrorDialog, dialogParameters, (result) =>
                    {

                    });
                }
                RaisePropertyChanged(nameof(Accounts));
            }
        }



        private DelegateCommand _showAccountInfoCommand;

        /// <summary>
        /// Реализует вызов диалогового окна с общей инфомрацией о выбранном счете
        /// </summary>
        public DelegateCommand ShowAccountInfoCommand =>
            _showAccountInfoCommand ?? (_showAccountInfoCommand = new DelegateCommand(ExecuteCommandName));

        void ExecuteCommandName()
        {
            var dialogParameters = new DialogParameters();
            dialogParameters.Add(CommonTypesPrism.SelectedAccount, SelectedAccount);
            _dialogService.ShowDialog(CommonTypesPrism.AccountInfoView, dialogParameters, null);

        }


        private DelegateCommand _createNewAccount;

        /// <summary>
        /// Реализует открытие нового счета клиента
        /// </summary>
        public DelegateCommand CreateNewAccount =>
            _createNewAccount ?? (_createNewAccount = new DelegateCommand(ExecuteCreateNewAccount));

        void ExecuteCreateNewAccount()
        {
            var dialogParameters = new DialogParameters()
            {
                {CommonTypesPrism.ParameterAccounts, Accounts },
                {CommonTypesPrism.ParameterOwner, Client }
            };
            _dialogService.Show(CommonTypesPrism.AccountView, dialogParameters, (result) =>
            {
                var newAcc = result.Parameters.GetValue<IAccount>(CommonTypesPrism.ParameterNewAccount);
                var owner = result.Parameters.GetValue<IClient>(CommonTypesPrism.ParameterOwner);
                if (newAcc != null)
                {
                    var isSaved = _clientService.SaveNewAccount(((IStorableDoc)Client).Id, newAcc);
                    ShowDialog(isSaved);
                    _eventAgregator.GetEvent<LogEvent>().Publish(GetLogRecord(isSaved,newAcc,owner));
                }
                RaisePropertyChanged(nameof(Accounts));
            });
        }


        private DelegateCommand _consolidateAccountsCommand;
        public DelegateCommand ConsolidateAccountsCommand =>
            _consolidateAccountsCommand ?? (_consolidateAccountsCommand = new DelegateCommand(ExecuteConsolidateAccountsCommand));

        void ExecuteConsolidateAccountsCommand()
        {
            var dialogParameters = new DialogParameters()
            {              
                {CommonTypesPrism.ParameterOwner, Client }
            };

            _dialogService.Show(CommonTypesPrism.AccountConsolidationView,dialogParameters, result=> 
            {
                RaisePropertyChanged(nameof(Accounts));
            });
        }

        /// <summary>
        /// Запись по результатам создания счете
        /// </summary>
        /// <param name="result"></param>
        /// <param name="newAccount"></param>
        /// <param name="client"></param>
        /// <returns></returns>
        private LogRecord GetLogRecord(bool result,IAccount newAccount,IClient client){
            LogRecord record = new LogRecord();
            if (result){
                record.LogRecordLevel = LogRecordLevel.Info;
                record.Message = $"Время: {DateTime.Now}-->Счет номер: {newAccount.Id} создан. Баланс: {newAccount.Balance}$. " +
                    $"Владелец {client.Name} {client.Surname} номер:{((IStorableDoc)client).Id}";
            }
            else{
                record.LogRecordLevel = LogRecordLevel.Error;
                record.Message = $"Время: {DateTime.Now}--> Ошибка открытия счета";
            }
            return record;
        }


        /// <summary>
        /// Вызывает диалоговое окна, в зависимости от рзультата создания счета
        /// </summary>
        /// <param name="result"></param>
        private void ShowDialog(bool result)
        {
            var dialogParameters = new DialogParameters();
            if (result)
            {
                string notifyMessage = "Счет открыт!";
                dialogParameters.Add(CommonTypesPrism.NotificationMessage, notifyMessage);
                _dialogService.ShowDialog(CommonTypesPrism.NotificationDialog, dialogParameters, result => { });

            }
            else
            {
                string errorMessage = "Ошибка открытия счета!";
                dialogParameters.Add(CommonTypesPrism.ErrorMessage, errorMessage);
                _dialogService.ShowDialog(CommonTypesPrism.ErrorDialog, dialogParameters, result => { });
            }
        }


        private DelegateCommand _sendMoneyCommand;

        /// <summary>
        /// Вызывает диалоговое окна переводов средств
        /// </summary>
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
                RaisePropertyChanged(nameof(Accounts));
            });
        }

        #endregion


        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            _currentFolder = navigationContext.Parameters.GetValue<string>(FolderParameters.FolderKey);
            LoadClients(_currentFolder);
            Client = BankClients.FirstOrDefault();            
        }

        /// <summary>
        /// Получает коллекцию клиентов выбранного в навигационном баре типа
        /// </summary>
        /// <param name="folder"></param>
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
