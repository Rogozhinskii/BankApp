using BankLibrary.Model.AccountModel.Interfaces;
using Newtonsoft.Json;
using System;

namespace BankLibrary.Model.AccountModel
{
    public abstract class BankAccount:IAccount
    {
       
        
        private AccountType _accountType;
        private float _balance;

        public Guid Id { get; set; }
        public float Balance => _balance;

        [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public AccountType AccountType => _accountType;

        

        public BankAccount()
        {
            
        }

        public BankAccount(AccountType type)
        {
            _accountType = type;
        }

        [JsonConstructor]
        public BankAccount(Guid id, float balance, AccountType type)
        {
            Id = id;
            _balance = balance;
            _accountType = type;
            
        }
    
        public bool CanReduceBalance(float count) =>
            Balance >= count;
        

        public virtual bool ReduceBalance(float count)
        {
            bool flag = default;
            if (CanReduceBalance(count))
            {
                _balance -= count;
                return true;
            }
            return flag;
        }

        public virtual void IncreaseBalance(float count) =>
            _balance += count;
        
    }
}
