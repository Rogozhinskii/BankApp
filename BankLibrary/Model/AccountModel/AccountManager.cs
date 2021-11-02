using BankLibrary.AccountModel;
using BankLibrary.AccountModel.Interfaces;
using BankLibrary.Model.AccountModel.Interfaces;
using BankLibrary.Model.DataRepository;
using BankLibrary.Model.DataRepository.Interfaces;
using System;

namespace BankLibrary.Model.AccountModel
{
    public class AccountManager : IAccountManager
    {
        private Client client;
        private IRepositoryManager repositoryManager;
        public AccountManager(IRepositoryManager repositoryManager,Client client){
            this.repositoryManager = repositoryManager;
            this.client = client;
        }
       

        public AccountManager() { }
        public void CloseAccount(){
            throw new NotImplementedException();
        }

        public IAccount CreateNewAccount(AccountType type){
            IAccount account = type switch
            {
                AccountType.NonDeposit => new NonDepositAccount(),
                _=>throw new NotSupportedException("Не известный тип аккаунта")
           };

            return account;
        }

        public void SendMoney(Guid fromAccountId, Guid toAccountId, float count)
        {
            throw new NotImplementedException();
        }
    }
}
