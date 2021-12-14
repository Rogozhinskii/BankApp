using BankLibrary.Model.AccountModel;
using BankLibrary.Model.AccountModel.Interfaces;
using BankLibrary.Model.ClientModel.Interfaces;
using BankLibrary.Model.DataRepository.Interfaces;
using BankUI.Core.Common;
using BankUI.Core.Common.Log;
using BankUI.Core.EventAggregator;
using BankUI.Core.Services.Interfaces;
using Prism.Commands;
using Prism.Events;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace BankApp.Modules.Client.ViewModels
{
    public class TransactionViewModel : DialogViewModelBase
    {
        
        /// <summary>
        /// Сервис доступа к хранилищу клиентов
        /// </summary>
        private readonly IClientService _clientService;
        /// <summary>
        /// Сервис диалоговых окон
        /// </summary>
        private readonly IDialogService _dialogService;
        private readonly IEventAggregator _eventAgreggator;

        /// <summary>
        /// Отправитель
        /// </summary>
        private IClient _owner;


        public TransactionViewModel(IClientService clientService,
                                    IDialogService dialogService,
                                    IEventAggregator eventAgreggator)
        {           
            _clientService = clientService;
            _dialogService = dialogService;
            _eventAgreggator = eventAgreggator;
        }

        private float _amount;
        /// <summary>
        /// Сумма перевода
        /// </summary>
        public float Amount
        {
            get { return _amount; }
            set { SetProperty(ref _amount, value); }
        }

        private IAccount _fromAccount;

        /// <summary>
        /// Счет с которого делается перевод
        /// </summary>
        public IAccount FromAccount
        {
            get { return _fromAccount; }
            set { SetProperty(ref _fromAccount, value); }
        }

        /// <summary>
        /// Коллекция счетов пользователя
        /// </summary>
        public ObservableCollection<IAccount> OwnerAccounts => GetOwnerAccounts();
       
        /// <summary>
        /// Вернет коллекцию счетов пользователя
        /// </summary>
        /// <returns></returns>
        private ObservableCollection<IAccount> GetOwnerAccounts()
        {
            var result = new ObservableCollection<IAccount>();
            if (_owner != null)
            {
                result = new ObservableCollection<IAccount>(_owner.Accounts.Where(x => x.AccountType != AccountType.Deposit));
            }
                
            return result;
        }

        private IClient _selectedRecipient;

        /// <summary>
        /// Получатель перевода
        /// </summary>
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

        /// <summary>
        /// Коллекция счетов получателя
        /// </summary>
        public ReadOnlyCollection<IAccount> RecipientAccounts
        {
            get { return _recipientAccounts; }
            set { SetProperty(ref _recipientAccounts, value); }
        }

        private IAccount _toAccount;

        /// <summary>
        /// Счет на который делается перевод
        /// </summary>
        public IAccount ToAccount
        {
            get { return _toAccount; }
            set { SetProperty(ref _toAccount, value); }
        }

        private List<IClient> _recipients=new();

        /// <summary>
        /// Коллекция возможных получателей
        /// </summary>
        public List<IClient> Recipients
        {
            get { return _recipients; }
            set { SetProperty(ref _recipients, value); }
        }

        private DelegateCommand _sendMoneyCommand;

        /// <summary>
        /// Выполняет перевод средств с выбранного счета на указанный счет
        /// </summary>
        public DelegateCommand SendMoneyCommand =>
            _sendMoneyCommand ??=_sendMoneyCommand = new DelegateCommand(ExecuteSendMoneyCommand);

        void ExecuteSendMoneyCommand()
        {            
            var result = FromAccount.Transaction(ToAccount, Amount);
            _eventAgreggator.GetEvent<LogEvent>().Publish(GetLogRecord(result));
            RaisePropertyChanged(nameof(OwnerAccounts));
            ShowDialog(result);
            Amount = 0f;
        }

        /// <summary>
        /// Возвращает лог запись о транзакции между счетами клиентов
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        private LogRecord GetLogRecord(bool result)
        {
            LogRecord record = new();
            if (result){
                record.LogRecordLevel = LogRecordLevel.Info;
                record.Message = $"{DateTime.Now}-->Слиент:{((IStorableDoc)_owner).Id} выполнил перевод со счета {FromAccount.Id} " +
                    $" на {ToAccount.Id} сумму {Amount}";
            }else{
                record.LogRecordLevel = LogRecordLevel.Error;
                record.Message = $"{DateTime.Now}-->Ошибка перевода. Со счета {FromAccount.Id} Слиент:{((IStorableDoc)_owner).Id}" +
                    $" на счет {ToAccount.Id} сумма{Amount}";
            }
            return record;
        }

        private void ShowDialog(bool result)
        {
            var dialogParameters = new DialogParameters();
            if (result){
                string notifyMessage = "Перевод выполнен успешно";
                dialogParameters.Add(CommonTypesPrism.NotificationMessage, notifyMessage);
                _dialogService.ShowDialog(CommonTypesPrism.NotificationDialog, dialogParameters, result => { });

            }
            else{
                string errorMessage = "Перевод выполнить не возможно";
                dialogParameters.Add(CommonTypesPrism.ErrorMessage, errorMessage);
                _dialogService.ShowDialog(CommonTypesPrism.ErrorDialog, dialogParameters, result => { });
            }
        }

        private DelegateCommand _exitCommand;

        /// <summary>
        /// Закрывает диалоговое окно
        /// </summary>
        public DelegateCommand ExitCommand =>
            _exitCommand ??=_exitCommand = new DelegateCommand(ExecuteExitCommand);

        

        void ExecuteExitCommand()
        {
            IDialogResult dialogResult =new DialogResult(); //на случай необходимости обработки callback            
            RaiseRequestClose(dialogResult);
        }

        public override void OnDialogOpened(IDialogParameters parameters){
            _owner = parameters.GetValue<IClient>(CommonTypesPrism.ParameterOwner);
            RaisePropertyChanged(nameof(OwnerAccounts));
            var tempList = new List<IClient>(_clientService.GetRegularClients());
            tempList.AddRange(_clientService.GetSpecialClients());            
            Recipients = new List<IClient>(tempList);
            RaisePropertyChanged(nameof(RecipientsFilteredCollection));
        }

        /// <summary>
        /// Возвращает true если элемент соотвутсвует условии фильтрации, иначе false
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        private bool RecipientsFilter(IClient client)
        {
            if (string.IsNullOrWhiteSpace(_recipientsfilteredText))
                return true;
            if (client.Name == null){                
                return false;
            }
            if (client.Name.Contains(_recipientsfilteredText, StringComparison.OrdinalIgnoreCase)) return true;
            if (client.Surname.Contains(_recipientsfilteredText, StringComparison.OrdinalIgnoreCase)) return true;
            return false;
        }

        private string _recipientsfilteredText;

        /// <summary>
        /// Текста для фильтрации
        /// </summary>
        public string RecipientsFilteredText
        {
            get { return _recipientsfilteredText; }
            set { SetProperty(ref _recipientsfilteredText, value); 
                  RaisePropertyChanged(nameof(RecipientsFilteredCollection));
                }
        }

        /// <summary>
        /// Отфильтрованная коллекция клиентов
        /// </summary>
        public ObservableCollection<IClient> RecipientsFilteredCollection
        {
            get
            {
                return new ObservableCollection<IClient>(Recipients.Where(i => RecipientsFilter(i)));                
            }
        }

    }
}
