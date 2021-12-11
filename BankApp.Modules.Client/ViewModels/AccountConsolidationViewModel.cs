using BankLibrary.Model.AccountModel;
using BankLibrary.Model.AccountModel.Interfaces;
using BankLibrary.Model.ClientModel.Interfaces;
using BankLibrary.Model.Exceptions;
using BankUI.Core.Common;
using BankUI.Core.Common.Log;
using BankUI.Core.EventAggregator;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace BankApp.Modules.Client.ViewModels
{
    /// <summary>
    /// ViewModel диалогового окна слияния счетов
    /// </summary>
    public class AccountConsolidationViewModel : DialogViewModelBase
    {   
        
        /// <summary>
        /// Владелец счетов
        /// </summary>
        private IClient _owner;
        private readonly IDialogService _dialogService;
        private readonly IEventAggregator _eventAgreggator;
        
        
        private IAccount _firstAccount;

        /// <summary>
        /// Первый счет 
        /// </summary>
        public IAccount FirstAccount
        {
            get { return _firstAccount; }
            set { SetProperty(ref _firstAccount, value); }
        }

        private IAccount _secondAccount;

        //Второй счет
        public IAccount SecondAccount
        {
            get { return _secondAccount; }
            set { SetProperty(ref _secondAccount, value); }
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

        public AccountConsolidationViewModel(IDialogService dialogService, IEventAggregator eventAgreggator)
        {
            _dialogService = dialogService;
            _eventAgreggator = eventAgreggator;
        }


        private DelegateCommand _exitCommand;

        /// <summary>
        /// Закрывает диалоговое окно
        /// </summary>
        public DelegateCommand ExitCommand =>
            _exitCommand ?? (_exitCommand = new DelegateCommand(ExecuteExitCommand));



        void ExecuteExitCommand()
        {
            IDialogResult dialogResult = new DialogResult(); //на случай необходимости обработки callback            
            RaiseRequestClose(dialogResult);
        }

        private DelegateCommand _consolidateAccounts;
        
        /// <summary>
        /// Выполняет объединения счетов пользователя
        /// </summary>
        public DelegateCommand ConsolidateAccounts =>
            _consolidateAccounts ?? (_consolidateAccounts = new DelegateCommand(ExecuteConsolidateAccounts));

        void ExecuteConsolidateAccounts()
        {
            DialogParameters parameters = new DialogParameters();
            LogRecord logRecord = new LogRecord();
            try
            {
                var newAccount = (SavingAccount)FirstAccount + (SavingAccount)SecondAccount;
                _owner.Accounts.Remove(FirstAccount);
                _owner.Accounts.Remove(SecondAccount);
                _owner.Accounts.Add(newAccount);
                var msg = $"Счета {FirstAccount.Id} и {FirstAccount.Id}. Успешно объединены";
                parameters.Add(CommonTypesPrism.NotificationMessage, msg);
                logRecord.LogRecordLevel = LogRecordLevel.Info; logRecord.Message = msg;
                _dialogService.Show(CommonTypesPrism.NotificationDialog, parameters, null);
            }
            catch(SameAccountsException ex)
            {
                parameters.Add(CommonTypesPrism.ErrorMessage, ex.Message);
                logRecord.LogRecordLevel = LogRecordLevel.Error;
                logRecord.Message = $"Попытка слияния счетов завершилась ошибкой. Счета {FirstAccount.Id} и {FirstAccount.Id}. " +
                                    $"Сообщение {ex.Message}";
                _dialogService.Show(CommonTypesPrism.ErrorDialog, parameters, null);
            }
            RaisePropertyChanged(nameof(OwnerAccounts));
            _eventAgreggator.GetEvent<LogEvent>().Publish(logRecord);
        }

        public override void OnDialogOpened(IDialogParameters parameters)
        {
            parameters.TryGetValue<IClient>(CommonTypesPrism.ParameterOwner,out _owner);
            if (_owner != null)
            {
                RaisePropertyChanged(nameof(OwnerAccounts));
            }
        }


    }
}
