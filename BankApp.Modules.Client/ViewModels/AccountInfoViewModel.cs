using BankLibrary.Model.AccountModel;
using BankLibrary.Model.AccountModel.Interfaces;
using BankUI.Core.Common;
using Prism.Commands;
using Prism.Services.Dialogs;
using System;

namespace BankApp.Modules.Client.ViewModels
{
    /// <summary>
    /// Viewmodel диалогового окна с общей информации о счете
    /// </summary>
    public class AccountInfoViewModel : DialogViewModelBase
    {
        private  IAccount _account;

        public AccountInfoViewModel() { }

        #region Свойства

        /// <summary>
        /// уникальный номер счета
        /// </summary>
        private Guid _id;
        public Guid Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }

        /// <summary>
        /// Доступные средства
        /// </summary>
        private float _balance;
        public float Balance
        {
            get { return _balance; }
            set { SetProperty(ref _balance, value); }
        }

        /// <summary>
        /// тип счета
        /// </summary>
        private AccountType _accountType;
        public AccountType AccountType
        {
            get { return _accountType; }
            set { SetProperty(ref _accountType, value); }
        }

        
        private float _totalIncome;

        /// <summary>
        /// Итоговый дох для депозитного счета
        /// </summary>
        public float TotalIncome
        {
            get { return _totalIncome; }
            set { SetProperty(ref _totalIncome, value); }
        }

        private float _rate;

        /// <summary>
        /// Процентная ставка по депозиту
        /// </summary>
        public float Rate
        {
            get { return _rate; }
            set { SetProperty(ref _rate, value); }
        }

        private float _term;

        /// <summary>
        /// Период вклада
        /// </summary>
        public float Term
        {
            get { return _term; }
            set { SetProperty(ref _term, value); }
        }
        #endregion

        #region Команды

        private DelegateCommand _closeCommand;

        /// <summary>
        /// Закрывает диалоговое окно 
        /// </summary>
        public DelegateCommand CloseCommand =>
            _closeCommand ?? (_closeCommand = new DelegateCommand(ExecuteCloseCommand));

        void ExecuteCloseCommand()
        {
            IDialogResult dialogResult = new DialogResult();
            RaiseRequestClose(dialogResult);
        }
        #endregion

        /// <summary>
        /// Вызывается при открытии окна
        /// </summary>
        /// <param name="parameters">параметры диалогового окна</param>
        public override void OnDialogOpened(IDialogParameters parameters)
        {
            _account = parameters.GetValue<IAccount>(CommonTypesPrism.SelectedAccount);
            if (_account == null)
                return;
            Id = _account.Id;
            Balance = _account.Balance;
            AccountType = _account.AccountType;
            if(_account is DepositAccount depositAccount)
            {
                TotalIncome = depositAccount.TotalIncome;
                Rate = depositAccount.Rate*100;
                Term = depositAccount.Term;
            }
        }
    }
}
