using BankLibrary.Model.AccountModel.Interfaces;
using BankLibrary.Model.ClientModel;
using BankLibrary.Model.Exceptions;
using Newtonsoft.Json;
using System;

namespace BankLibrary.Model.AccountModel
{
    /// <summary>
    /// Абстрактный класс некого счета
    /// </summary>
    public abstract class BankAccount:IAccount
    {
        private readonly AccountType _accountType;
        private float _balance;
        public Guid Id { get; set; }
        public float Balance => _balance;

        [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public AccountType AccountType => _accountType;

        public ClientType ClientType { get; set; }

        public BankAccount() { }

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
    
        private bool CanReduceBalance(float count) =>
            Balance >= count;
        

        public virtual bool ReduceBalance(float count)
        {           
            if (CanReduceBalance(count)){
                _balance -= count;
                return true;
            }
            else{
                throw new NotEnoughBalanceException();
            }
            
        }

        [Obsolete]
        public virtual bool IncreaseBalance(float count)
        {
            _balance += count;
            return true;
        }
            


        public override bool Equals(object obj)
        {
            if(obj !=null && obj is IAccount acc)
            {
                return acc.Id == this.Id;
            }
            return false;            
        }

    }
}
