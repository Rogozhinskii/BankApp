using BankLibrary.Model;
using BankLibrary.Model.AccountModel;
using BankLibrary.Model.AccountModel.Interfaces;
using BankLibrary.Model.DataRepository.Interfaces;
using System;
using System.Text.Json.Serialization;

namespace BankLibrary.AccountModel
{
    public class BankAccount:IAccount
    {
        private Guid id;
        private float balance;
        private Client owner;
        private AccountType accountType;
        public Guid Id => id;

        public float Balance => balance;

        public AccountType AccountType => accountType;

        public Client Owner => owner;

       
        public BankAccount(Guid id, float balance, Client owner, AccountType accountType)
        {
            this.id = id;
            this.balance = balance;
            this.owner = owner;
            this.accountType = accountType;
        }
    

        public IStorableDoc Clone()
        {
            throw new NotImplementedException();
        }

        public bool CanReduceBalance(float count) =>
            balance >= count;
        

        public bool ReduceBalance(float count)
        {
            bool flag = default;
            if (CanReduceBalance(count))
            {
                balance -= count;
                return true;
            }
            return flag;
        }

        public void IncreaseBalance(float count) =>
            balance += count;
        
    }
}
