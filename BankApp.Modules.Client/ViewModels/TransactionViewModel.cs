﻿using BankLibrary.Model;
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
using System.Windows.Data;

namespace BankApp.Modules.Client.ViewModels
{
    public class TransactionViewModel : DialogViewModelBase, IDataErrorInfo
    {
        /// <summary>
        /// Сервис перевода средств между счетами клентов
        /// </summary>
        private readonly ITransactionManager<IAccount> _transactionManager;

        /// <summary>
        /// Серввис доступа к хранилищу клиентов
        /// </summary>
        private readonly IClientService _clientService;
        /// <summary>
        /// Сервис диалоговых окон
        /// </summary>
        private readonly IDialogService _dialogService;

        /// <summary>
        /// Отправитель
        /// </summary>
        private IClient _owner;


        public TransactionViewModel(ITransactionManager<IAccount> transactionManager, IClientService clientService,IDialogService dialogService)
        {
            _transactionManager = transactionManager;
            _clientService = clientService;
            _dialogService = dialogService;
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

        private List<IClient> _recipients=new List<IClient>();

        /// <summary>
        /// Коллекция возможных получателей
        /// </summary>
        public List<IClient> Recipients
        {
            get { return _recipients; }
            set { SetProperty(ref _recipients, value); }
        }

        #region Обработка ошибок ввода данных
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

        #endregion


        private DelegateCommand _sendMoneyCommand;

        /// <summary>
        /// Выполняет перевод средств с выбранного счета на указанный счет
        /// </summary>
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

        /// <summary>
        /// Закрывает диалоговое окно
        /// </summary>
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
            if (client.Name == null)
            {                
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
