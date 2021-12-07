using BankLibrary.Model.AccountModel;
using BankLibrary.Model.AccountModel.Interfaces;
using BankLibrary.Model.ClientModel.Interfaces;
using BankLibrary.Model.DataRepository.Interfaces;
using BankUI.Core.Common;
using BankUI.Core.Services.Interfaces;
using Prism.Commands;
using Prism.Services.Dialogs;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace BankApp.Modules.Client.ViewModels
{
    /// <summary>
    /// ViewModel диалогового окна открытия счетов
    /// </summary>
    public class AccountViewModel : DialogViewModelBase,IDataErrorInfo
    {
        /// <summary>
        /// сервис открытия счетов
        /// </summary>
        private readonly IAccountService _accountService;

        /// <summary>
        /// Сервис переводов средств между счетами
        /// </summary>
        private readonly ITransactionManager<IAccount> _transactionManager;  
        
        /// <summary>
        /// Хозяин счета
        /// </summary>
        private IStorableDoc _owner;

        public AccountViewModel(IAccountService accountService, ITransactionManager<IAccount> transactionManager,IClientService clientService)
        {
            _accountService = accountService;
            _transactionManager = transactionManager;            
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

        #region Обработка ошибок при вводе значений в TextBox
        public string Error => "";

        public string this[string columnName]
        {
            get
            {
                string error = string.Empty;
                switch (columnName)
                {
                    case "Balance":
                        if (FromAccount != null && FromAccount.Balance < _balance)
                            error = "Не достаточно средств";
                        break;
                }
                return error;
            }
        }

        #endregion

        #region Commands

        private DelegateCommand _saveNewAccount;

        /// <summary>
        /// Выполняет сохранение нового счета, по результату окно закрывается
        /// </summary>
        public DelegateCommand SaveNewAccount =>
            _saveNewAccount ?? (_saveNewAccount = new DelegateCommand(ExecuteSaveAccountCommand));



        void ExecuteSaveAccountCommand()
        {
            IDialogResult dialogResult = new DialogResult();
            var accountManager = _accountService.GetAccountManager(AccountType);
            var newAccount = accountManager.CreateNewAccount();
            newAccount.ClientType = ((IClient)_owner).ClientType;
            if (FromAccount != null)
            {
                _transactionManager.SendMoneyToAccount(FromAccount, newAccount, Balance);
            }
            if (Balance > 0 && FromAccount == null)
            {
                _transactionManager.SendMoneyToAccount(newAccount, Balance);
            }
            if (newAccount is DepositAccount depositAccount)
            {
                depositAccount.Term = Term;
            }
            dialogResult.Parameters.Add(CommonTypesPrism.ParameterNewAccount, newAccount);
            RaiseRequestClose(dialogResult);
        }
        #endregion


        /// <summary>
        /// Отвечает за возможность закрытия диалогового окна
        /// </summary>
        /// <returns></returns>
        public override bool CanCloseDialog()
        {
            return true;
        }


        /// <summary>
        /// Вызываетс
        /// </summary>
        /// <param name="parameters"></param>
        public override void OnDialogOpened(IDialogParameters parameters)
        {
            _owner = parameters.GetValue<IStorableDoc>(CommonTypesPrism.ParameterOwner);
            Accounts = new ReadOnlyCollection<IAccount>(((IClient)_owner).Accounts);            
        }
    }
}
