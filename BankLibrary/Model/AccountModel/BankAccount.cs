using BankLibrary.Model;
using BankLibrary.Model.AccountModel;
using BankLibrary.Model.AccountModel.Interfaces;
using BankLibrary.Model.DataRepository.Interfaces;
using Newtonsoft.Json;
using System;
using System.Text.Json.Serialization;

namespace BankLibrary.AccountModel
{
    public class BankAccount:IAccount
    {
       
        
        private AccountType accountType;
        public Guid Id { get; set; }

        public float Balance { get; set; }

        public AccountType AccountType { get; set; }



        public BankAccount()
        {

        }

        [JsonConstructor]
        public BankAccount(Guid id, float balance, AccountType type)
        {
            Id = id;
            Balance = balance;
            AccountType = type;
            
        }
    

        public IStorableDoc Clone()
        {
            throw new NotImplementedException();
        }

        public bool CanReduceBalance(float count) =>
            Balance >= count;
        

        public bool ReduceBalance(float count)
        {
            bool flag = default;
            if (CanReduceBalance(count))
            {
                Balance -= count;
                return true;
            }
            return flag;
        }

        public virtual void IncreaseBalance(float count) =>
            Balance += count;
        
    }
}
