using BankLibrary.Model.AccountModel;
using BankLibrary.Model.AccountModel.Interfaces;
using BankUI.Core.Common;
using Prism.Commands;
using Prism.Services.Dialogs;
using System;

namespace BankApp.Modules.Client.ViewModels
{
    public class AccountInfoViewModel : DialogViewModelBase
    {
        private IAccount _account; 
        
        public AccountInfoViewModel()
        {
        }

        private Guid _id;
        public Guid Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }

        private float _balance;
        public float Balance
        {
            get { return _balance; }
            set { SetProperty(ref _balance, value); }
        }

        private AccountType _accountType;
        public AccountType AccountType
        {
            get { return _accountType; }
            set { SetProperty(ref _accountType, value); }
        }


        private float _totalIncome;
        public float TotalIncome
        {
            get { return _totalIncome; }
            set { SetProperty(ref _totalIncome, value); }
        }

        private float _rate;
        public float Rate
        {
            get { return _rate; }
            set { SetProperty(ref _rate, value); }
        }

        private float _term;
        public float Term
        {
            get { return _term; }
            set { SetProperty(ref _term, value); }
        }

        private DelegateCommand _closeCommand;
        public DelegateCommand CloseCommand =>
            _closeCommand ?? (_closeCommand = new DelegateCommand(ExecuteCommandName));

        void ExecuteCommandName()
        {
            IDialogResult dialogResult = new DialogResult();
            RaiseRequestClose(dialogResult);
        }

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
