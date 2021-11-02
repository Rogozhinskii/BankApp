using BankLibrary.AccountModel;
using BankLibrary.AccountModel.Interfaces;
using System;

namespace BankLibrary.Model.AccountModel
{
    public class AccountManager : IAccountManager
    {
        private Client client;
        public AccountManager(Client client){
            this.client = client;
        }

        public AccountManager() { }
        public void CloseAccount(){
            throw new NotImplementedException();
        }

        public void CreateNewAccount(AccountType type){
            switch (type)
            {
                case AccountType.Deposit:  break;
                case AccountType.NonDeposit: break;
            }
        }

        public void SendMoney(Guid fromAccountId, Guid toAccountId, float count)
        {
            throw new NotImplementedException();
        }
    }
}
