using BankLibrary.Model.AccountModel;
using BankLibrary.Model.AccountModel.Interfaces;
using BankLibrary.Model.ClientModel.Interfaces;
using BankLibrary.Model.DataRepository.Interfaces;
using BankLibrary.Model.Exceptions;
using BankUI.Core.Common;
using BankUI.Core.Common.Log;
using BankUI.Core.EventAggregator;
using BankUI.Core.Services.Interfaces;
using Prism.Commands;
using Prism.Events;
using Prism.Services.Dialogs;
using System;
using System.Collections.ObjectModel;

namespace BankApp.Modules.Client.ViewModels
{
    /// <summary>
    /// ViewModel диалогового окна открытия счетов
    /// </summary>
    public class AccountViewModel : DialogViewModelBase
    {
        /// <summary>
        /// сервис открытия счетов
        /// </summary>
        private readonly IAccountService<IAccount> _accountService;
        private readonly IEventAggregator _eventAggregator;
        private readonly IDialogService _dialogService;

        /// <summary>
        /// Хозяин счета
        /// </summary>
        private IStorableDoc _owner;

        public AccountViewModel(IAccountService<IAccount> accountService,IEventAggregator eventAggregator, IDialogService dialogService)
        {
            _accountService = accountService;
            _eventAggregator = eventAggregator;
            _dialogService = dialogService;
        }

        #region Свойства

        public override string Title => "Account Dialog";


        private AccountType _accountType;
        public AccountType AccountType
        {
            get { return _accountType; }
            set { SetProperty(ref _accountType, value); }
        }

        private ReadOnlyCollection<IAccount> _accounts;

        /// <summary>
        /// Коллекция счетов пользователя
        /// </summary>
        public ReadOnlyCollection<IAccount> Accounts
        {
            get { return _accounts; }
            set { SetProperty(ref _accounts, value); }
        }

        private int _term;

        /// <summary>
        /// Срок дпозита 
        /// </summary>
        public int Term
        {
            get { return _term; }
            set { SetProperty(ref _term, value); }
        }

        private IAccount _fromAccount;

        /// <summary>
        /// Счет с которого переводятся средства
        /// </summary>
        public IAccount FromAccount
        {
            get { return _fromAccount; }
            set { SetProperty(ref _fromAccount, value); }
        }

        private float _balance;

        /// <summary>
        /// Сумма зачислений на вновь открытый счет
        /// </summary>
        public float Balance
        {
            get { return _balance; }
            set { SetProperty(ref _balance, value); }
        }
        #endregion

        #region Commands

        private DelegateCommand _saveNewAccount;

        /// <summary>
        /// Выполняет сохранение нового счета, по результату окно закрывается
        /// </summary>
        public DelegateCommand SaveNewAccount =>
            _saveNewAccount ??=_saveNewAccount = new DelegateCommand(ExecuteSaveAccountCommand);



        void ExecuteSaveAccountCommand()
        {
            IDialogResult dialogResult = new DialogResult(); 
            var newAccount = _accountService.CreateNewAccount(AccountType);
            newAccount.ClientType = ((IClient)_owner).ClientType;
            LogRecord record = new();
            bool result = default;
            if (FromAccount != null){                
                try{
                    result = FromAccount.Transaction(newAccount, Balance);
                }
                catch (NotEnoughBalanceException ex){
                    var dialogParameters = new DialogParameters
                    {
                        { CommonTypesPrism.ErrorMessage, ex.Message }
                    };
                    _dialogService.ShowDialog(CommonTypesPrism.ErrorDialog, dialogParameters, result => { });
                    RaiseRequestClose(dialogResult);
                }
            }
            if (Balance > 0 && FromAccount == null){
                result = newAccount.Transaction(Balance);  
            }
            if (newAccount is DepositAccount depositAccount){
                depositAccount.Term = Term;
            }
            GetLogRecord(result, FromAccount, newAccount, Balance,out record);
            if (record.LogRecordLevel != LogRecordLevel.None)
                _eventAggregator.GetEvent<LogEvent>().Publish(record);            
            dialogResult.Parameters.Add(CommonTypesPrism.ParameterNewAccount, newAccount);
            dialogResult.Parameters.Add(CommonTypesPrism.ParameterOwner, _owner);
            RaiseRequestClose(dialogResult);
        }
        #endregion

        
        /// <summary>
        /// Возвращает лог запись при переводе средств с одного счета на другой
        /// </summary>
        /// <param name="result">результат операции перевода</param>
        /// <param name="fromAccount"></param>
        /// <param name="newAccount"></param>
        /// <param name="balance"></param>
        /// <returns></returns>
        private void GetLogRecord(bool result, IAccount fromAccount, IAccount newAccount, float balance,out LogRecord logRecord)
        {
            LogRecord record = new();
            if (fromAccount == null){
                GetLogRecord(result, newAccount, balance ,out record);
            }
            else{
                if (fromAccount != null && result){
                    record.LogRecordLevel = LogRecordLevel.Info;
                    record.Message = $"Время: {DateTime.Now}-->Сумма перевода: {balance}. Со счета: {fromAccount.Id} на счет: {newAccount.Id} ";
                }
                else{
                    record.LogRecordLevel = LogRecordLevel.Error;
                    record.Message = $"Время: {DateTime.Now}-->Сумма перевода: {balance}. Со счета: {fromAccount.Id} на счет: {newAccount.Id} " +
                        $"завершился ошибкой";
                }
            }            
            logRecord = record;
        }

        /// <summary>
        /// Возвращает лог запись при переводе средств на счет
        /// </summary>
        /// <param name="result">результат операции перевода</param>
        /// <param name="toAccount"></param>
        /// <param name="balance"></param>
        /// <returns></returns>
        private void GetLogRecord(bool result, IAccount toAccount, float balance,out LogRecord logRecord)
        {
            LogRecord record = new();
            if (result){
                record.LogRecordLevel = LogRecordLevel.Info;
                record.Message = $"Время: {DateTime.Now}-->Сумма перевода: {balance}.На счет: {toAccount.Id} ";
            }
            else{
                record.LogRecordLevel = LogRecordLevel.Error;
                record.Message = $"Время: {DateTime.Now}-->Сумма перевода: {balance}. На счет: {toAccount.Id} " +
                    $"завершился ошибкой";
            }
            logRecord = record;
        }

        /// <summary>
        /// Отвечает за возможность закрытия диалогового окна
        /// </summary>
        /// <returns></returns>
        public override bool CanCloseDialog()
            => true;
        


        /// <summary>
        /// Вызываетс
        /// </summary>
        /// <param name="parameters"></param>
        public override void OnDialogOpened(IDialogParameters parameters){
            _owner = parameters.GetValue<IStorableDoc>(CommonTypesPrism.ParameterOwner);
            if(_owner is not null)
                Accounts = new ReadOnlyCollection<IAccount>(((IClient)_owner).Accounts);            
        }
    }
}
