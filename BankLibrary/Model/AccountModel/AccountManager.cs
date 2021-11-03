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

        public AccountManager(IRepositoryManager repositoryManager){
            this.repositoryManager = repositoryManager ?? throw new ArgumentNullException($"{nameof(repositoryManager)} не может быть null");           
        }


        public AccountManager() { }
        public void CloseAccount(){
            throw new NotImplementedException();
        }

        public IAccount CreateNewAccount(AccountType type){
            IAccount account = type switch
            {
                AccountType.NonDeposit => new NonDepositAccount(Guid.NewGuid(),0.0f,client,AccountType.NonDeposit),
                _=>throw new NotSupportedException("Не известный тип аккаунта")
           };

            return account;
        }

        public bool SendMoney(Guid fromAccountId, Guid toAccountId, float count)
        {
            bool flag = default;
            var fromAccaunt = repositoryManager.GetAccountById(fromAccountId);
            var toAccaunt = repositoryManager.GetAccountById(toAccountId);
            if (fromAccaunt != null && toAccaunt != null)
            {
                
                if (fromAccaunt.CanReduceBalance(count))
                {
                    flag=fromAccaunt.ReduceBalance(count);
                    toAccaunt.IncreaseBalance(count);                    
                    return flag;
                }

            }
            return flag;

        }

       
    }
}
