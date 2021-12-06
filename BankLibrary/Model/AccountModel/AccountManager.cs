using BankLibrary.AccountModel;
using BankLibrary.AccountModel.Interfaces;
using BankLibrary.Model.AccountModel.Interfaces;
using BankLibrary.Model.ClientModel;
using BankLibrary.Model.DataRepository;
using BankLibrary.Model.DataRepository.Interfaces;
using System;

namespace BankLibrary.Model.AccountModel
{
    public class AccountManager<T> : IAccountManager<T> where T:IAccount, new()
    {
        
        private IRepositoryManager repositoryManager;        
        
        public AccountManager(IRepositoryManager repositoryManager){
            this.repositoryManager = repositoryManager ?? throw new ArgumentNullException($"{nameof(repositoryManager)} не может быть null");           
        }


        public AccountManager() { }
        public void CloseAccount(){
            
        }

       
        public bool SendMoney(Guid fromAccountId, Guid toAccountId, float count)
        {
            bool flag = default;
            var fromAccaunt = repositoryManager.GetAccountById(fromAccountId);
            var toAccaunt = repositoryManager.GetAccountById(toAccountId);
            if (fromAccaunt != null && toAccaunt != null)
            {
                
                if (fromAccaunt.CanReduceBalance(count)){
                    flag=fromAccaunt.ReduceBalance(count);
                    toAccaunt.IncreaseBalance(count);                    
                    return flag;
                }

            }
            return flag;

        }

        public T CreateNewAccount(ClientType type,float sum)
        {
            T acc = CreateNewAccount(type);
            acc.IncreaseBalance(sum);
            return acc;
        }

        public T CreateNewAccount(ClientType type)
        {
            T acc = new T();
            acc.Id = Guid.NewGuid();
            acc.ClientType = type;            
            return acc;
        }

    }
}
